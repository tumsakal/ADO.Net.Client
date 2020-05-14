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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDataMapper" />
    public class DataMapper : IDataMapper
    {
#if !NET45
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <param name="token"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public async IAsyncEnumerable<T> MapResultSetAsync<T>(DbDataReader reader, [EnumeratorCancellation] CancellationToken token = default)
        { 
            //Keep looping through the result set
            while(await reader.ReadAsync(token) == true)
            {
                //Return this object
                yield return MapRecord<T>(reader);
            }
        }
#endif
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public IEnumerable<T> MapResultSet<T>(DbDataReader reader)
        {
            //Keep looping through the result set
            while(reader.Read() == true)
            {
                //Return this object
                yield return MapRecord<T>(reader);
            }
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
            object returnType = Activator.CreateInstance<T>();
            Type type = returnType.GetType();

            //Loop through all properties
            foreach (PropertyInfo p in type.GetProperties())
            {
                DbField field = null;
                bool ignoredField = false;
                bool shouldSkip = !(p.CanWrite == true);

                //Check if we can write the property
                if (shouldSkip == true)
                {
                    //Don't go any further we cannot write to this property
                    continue;
                }

                //Get the dbfield attribute
                foreach (object attribute in p.GetCustomAttributes(false))
                {
                    //Check if this is an ignored field
                    if (attribute is DbFieldIgnore)
                    {
                        //Break out we're done
                        ignoredField = true;
                        break;
                    }
                    //Check if this is a dbfield
                    else if (attribute is DbField)
                    {
                        field = (DbField)attribute;
                    }
                }

                //Check if we should go on to the next loop
                if (ignoredField == true)
                {
                    continue;
                }

                string fieldName = p.Name;

                //Check if the cast took place
                if (field != null && !string.IsNullOrEmpty(field.DatabaseFieldName))
                {
                    fieldName = field.DatabaseFieldName;
                }

                int ordinalIndex = record.GetOrdinal(fieldName);

                //Check if the field is present in the dynamic object
                if (ordinalIndex != -1)
                {
                    //Get the current property value
                    object value = record.GetValue(ordinalIndex);

                    //Check if DBNull
                    if (field != null && (value == null || value == DBNull.Value))
                    {
                        //Set new value
                        value = field.DefaultValueIfNull;
                    }

                    //Check if this is a nullable type
                    if (Utilities.IsNullableGenericType(p.PropertyType))
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
                    //Check if an enum
                    else if (p.PropertyType.GetTypeInfo().IsEnum)
                    {
                        if (value != null)
                        {
                            p.SetValue(returnType, Enum.Parse(p.PropertyType, value.ToString()), null);
                        }
                    }
                    else
                    {
                        //This is a normal property
                        p.SetValue(returnType, Convert.ChangeType(value, p.PropertyType), null);
                    }
                }
            }

            //Return this back to the caller
            return (T)returnType;
        }
        #region Helper Methods
        #endregion
    }
}