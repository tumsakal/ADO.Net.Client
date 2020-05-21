#region Licenses
/*MIT License
Copyright(c) 2020
Robert Garrison

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion
#region Using Statements
using ADO.Net.Client.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Mapper class that maps fields in a record in a database to a .NET object
    /// </summary>
    /// <seealso cref="IDataMapper" />
    public class DataMapper : IDataMapper
    {
#if !NET45
        /// <summary>
        /// Maps the result set one at a time by streaming the result from the server asynchronously
        /// </summary>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="reader"/></typeparam>
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains a result set of records that needs to be mapped</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="reader"/></returns>
        public async IAsyncEnumerable<T> MapResultSetStreamAsync<T>(DbDataReader reader, [EnumeratorCancellation] CancellationToken token = default)
        {
            //Keep looping through the result set
            while (await reader.ReadAsync(token).ConfigureAwait(false) == true)
            {
                //Return this object
                yield return MapRecord<T>(reader);
            }
        }
#endif
        /// <summary>
        /// Maps an entire result set in the <paramref name="reader"/>
        /// </summary>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="reader"/></typeparam>
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains a result set of records that needs to be mapped</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="reader"/></returns>
        public async Task<IEnumerable<T>> MapResultSetAsync<T>(DbDataReader reader, CancellationToken token = default)
        {
            List<T> returnList = new List<T>();

            //Keep looping through the result set
            while (await reader.ReadAsync(token).ConfigureAwait(false) == true)
            {
                //Return this object
                returnList.Add(MapRecord<T>(reader));
            }

            return returnList;
        }
        /// <summary>
        /// Maps the result set one at a time by streaming the result from the server
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="reader"/></typeparam>
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains a result set of records that needs to be mapped</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="reader"/></returns>
        public IEnumerable<T> MapResultSetStream<T>(DbDataReader reader)
        {
            //Keep looping through the result set
            while (reader.Read() == true)
            {
                //Return this object
                yield return MapRecord<T>(reader);
            }
        }
        /// <summary>
        /// Maps an entire result set in the <paramref name="reader"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="reader"/></typeparam>
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains a result set of records that needs to be mapped</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="reader"/></returns>
        public IEnumerable<T> MapResultSet<T>(DbDataReader reader)
        {
            List<T> returnList = new List<T>();

            //Keep looping through the result set
            while (reader.Read() == true)
            {
                //Return this object
                returnList.Add(MapRecord<T>(reader));
            }

            return returnList;
        }
        /// <summary>
        /// Maps the passed in <paramref name="record"/> to an instance of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="record"/></typeparam>
        /// <param name="record">A record from a result set of data</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="record"/></returns>
        public T MapRecord<T>(IDataRecord record)
        {
            //Get an instance of the object passed in
            T returnType = Activator.CreateInstance<T>();
            IEnumerable<PropertyInfo> writeableProperties = returnType.GetType().GetProperties().Where(x => x.CanWrite == true).Where(x => x.GetCustomAttributes(false).Contains(typeof(DbFieldIgnore)) == false);

            //Loop through all the properties
            foreach (PropertyInfo p in writeableProperties)
            {
                object[] customAttributes = p.GetCustomAttributes(false);
                DbField field = customAttributes.Where(x => x is DbField).FirstOrDefault() as DbField;

                string fieldName = (field != null && string.IsNullOrEmpty(field.DatabaseFieldName) == false) ? field.DatabaseFieldName : p.Name;
                int ordinalIndex = record.GetOrdinal(fieldName);
                object value = record.GetValue(ordinalIndex);

                //Check if DBNull
                if (field != null && value == DBNull.Value)
                {
                    //Set new value
                    value = field.DefaultValueIfNull;
                }

                if (Utilities.IsNullableGenericType(p.PropertyType) == true)
                {
                    if (value == null)
                    {
                        p.SetValue(returnType, null, null);
                    }
                    else
                    {
                        p.SetValue(returnType, Convert.ChangeType(value, Nullable.GetUnderlyingType(p.PropertyType)), null);
                    }
                }
                else if (p.PropertyType.GetTypeInfo().IsEnum == true && value != null)
                {
                    p.SetValue(returnType, Enum.Parse(p.PropertyType, value.ToString()), null);
                }
                else
                {
                    //This is a normal property
                    p.SetValue(returnType, Convert.ChangeType(value, p.PropertyType), null);
                }
            }

            //Return this back to the caller
            return returnType;
        }
        #region Helper Methods
        #endregion
    }
}