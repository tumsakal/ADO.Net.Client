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
using ADO.Net.Client.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// Utility class that builds out queries to be exectued against a database
    /// </summary>
    public class QueryBuilder : IQueryBuilder
    {
        #region Fields/Properties
        private List<DbParameter> _parameters;
        private StringBuilder _sqlQuery;

        /// <summary>
        /// The database parameters associated with a query
        /// </summary>
        /// <value>
        /// The parameters associated with a query as a <see cref="IEnumerable{T}"/> of <see cref="DbParameter"/>
        /// </value>
        public IEnumerable<DbParameter> Parameters 
        {
            get
            {
                return _parameters;
            }
        }
        #endregion
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        public QueryBuilder()
        {
            _parameters = new List<DbParameter>();
        }
        #endregion
        #region Utility Methods        
        /// <summary>
        /// Adds the passed in parameter to the parameters collection
        /// </summary>
        /// <param name="param">An instance of the <see cref="T:System.Data.Common.DbParameter" /> object, that is created the by the caller</param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.Common.DbParameter" />
        /// </returns>
        public void AddParameter(DbParameter param)
        {
            //Check if this parameter exists before adding to collection
            if (Contains(param.ParameterName) == true)
            {
                throw new ArgumentException($"Parameter with name {param.ParameterName} already exists", nameof(param));
            }
            else
            {
                //Check if the value passed in was null
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }

                //Add this parameter
                _parameters.Add(param);
            }
        }
        /// <summary>
        /// Adds an <see cref="IEnumerable{T}"" /> objects to the helpers underlying db parameter collection
        /// </summary>
        /// <param name="dbParams">An <see cref="IEnumerable{T}" /> to add to the underlying db parameter collection for the connection</param>
        public void AddParameterRange(IEnumerable<DbParameter> dbParams)
        {
            _parameters.AddRange(dbParams);
        }
        /// <summary>
        /// Clears all parameters from the parameters collection
        /// </summary>
        public void ClearParameters()
        {
            _parameters.Clear();
        }
        /// <summary>
        /// Checks for a paremeter in the parameters list with the passed in name
        /// </summary>
        /// <param name="parameterName">The name of the parameter to use when searching the Parameters list</param>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="parameterName"/> passed into routine is null or empty</exception>
        /// <returns>True if this parameter exists in the parameters collection, false otherwise</returns>
        public bool Contains(string parameterName)
        {
            bool parameterExists = false;

            //Check if this even has a name, caller be using a provider that doesn't support named parameters
            if (string.IsNullOrWhiteSpace(parameterName) == true)
            {
                throw new ArgumentException(nameof(parameterName) + "cannot be null or empty");
            }

            //Return this back to the caller
            foreach (DbParameter param in _parameters)
            {
                //Check for parameter name including the variable binder
                if (string.Equals(param.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    parameterExists = true;
                    break;
                }
            }

            //Return this back to the caller
            return parameterExists;
        }
        /// <summary>
        /// Checks for a paremeter in the parameters list with the passed in index
        /// </summary>
        /// <param name="index">The index of the parameter in the parameters collection to identify the parameter to remove from the collection</param>
        /// <returns>Returns true if item was found in the paramerters collection, false otherwise if item was not found in the collection</returns>
        public bool Contains(int index)
        {
            try
            {
                DbParameter param;

                //Get the parameter at this index
                param = _parameters[index];

                //We found this item, tell the caller
                return (param == null);
            }
            catch (Exception ex)
            {
                //We couldn't find this item, tell the caller
                return false;
            }
        }
        /// <summary>
        /// Retrieves a <see cref="DbParameter"/> object by using the passed in parameter name
        /// </summary>
        /// <exception cref="ArgumentException">Throws when the passed in <paramref name="parameterName"/> is <c>null</c> or <see cref="string.Empty"/></exception>
        /// <exception cref="InvalidOperationException">Thrown when the passed in parameter name is not present in the parameters collection</exception>
        /// <param name="parameterName">The name of the parameter to use to find the parameter value</param>
        /// <returns>The specified <see cref="DbParameter"/> object from the parameters collection</returns>
        public DbParameter GetParameter(string parameterName)
        {
            //Check for null or empty
            if (string.IsNullOrWhiteSpace(parameterName) == true)
            {
                throw new ArgumentException(nameof(parameterName) + " is null or empty");
            }

            //Loop through all parameters
            foreach (DbParameter param in _parameters)
            {
                //Check if the name is the same
                if (string.Equals(param.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    //Return this back to the caller
                    return param;
                }
            }

            //Return this back to the caller
            return null;
        }
        /// <summary>
        /// Retrieves a <see cref="DbParameter"/> from the parameters collection by using the index of the parameter
        /// </summary>
        /// <param name="index">The index of the parameter in the parameters collection to identify the parameter to retrieve from the collection</param>
        /// <returns>Returns the DbParameter object located at this index</returns>
        /// <exception cref="IndexOutOfRangeException">thrown when an attempt is made to access an element of an array or collection with an index that is outside the bounds of the array or less than zero.</exception>
        public DbParameter GetParameter(int index)
        {
            //Return this back to the caller
            return _parameters[index];
        }
        /// <summary>
        /// Removes a <see cref="DbParameter"/> from the parameters collection for the current <see cref="DbConnection"/> by using the parameter name
        /// </summary>
        /// <param name="parameterName">The name of the parameter to identify the parameter to remove from the collection</param>
        /// <returns>Returns true if item was successully removed, false otherwise if item was not found in the list</returns>
        public bool RemoveParameter(string parameterName)
        {
            //Return this back to the caller
            return _parameters.Remove(_parameters.Find(x => x.ParameterName == parameterName));
        }
        /// <summary>
        /// Removes a <see cref="DbParameter"/> from the parameters collection for the current <see cref="DbConnection"/> by using the index of the parameter
        /// </summary>
        /// <param name="index">The index of the parameter in the parameters collection to identify the parameter to remove from the collection</param>
        /// <returns>Returns true if item was successully removed, false otherwise if item was not found in the list</returns>
        /// <exception cref="IndexOutOfRangeException">thrown when an attempt is made to access an element of an array or collection with an index that is outside the bounds of the array or less than zero.</exception>
        public bool RemoveParameter(int index)
        {
            //Return this back to the caller
            return _parameters.Remove(_parameters[index]);
        }
        /// <summary>
        /// Replaces an existing parameter with the new <see cref="DbParameter"/> with an existing <see cref="DbParameter.ParameterName"/>
        /// </summary>
        /// <param name="parameterName">The index as a <c>string</c> to use when searching for the existing parameter</param>
        /// <param name="param">A new instance of <see cref="DbParameter"/></param>
        public void ReplaceParameter(string parameterName, DbParameter param)
        {
            //Get index of this parameter
            int index = _parameters.FindIndex(i => string.Equals(i.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase) == true);

            //Do a replace of the parameter
            _parameters[index] = param;
        }
        /// <summary>
        /// Replaces an existing parameter with the new <see cref="DbParameter"/> passed in at the <paramref name="index"/>
        /// </summary>
        /// <param name="index">The index as an <see cref="int"/> to use when searching for the existing parameter</param>
        /// <param name="param">A new instance of <see cref="DbParameter"/></param>
        /// <exception cref="IndexOutOfRangeException">thrown when an attempt is made to access an element of an array or collection with an index that is outside the bounds of the array or less than zero.</exception>
        public void ReplaceParameter(int index, DbParameter param)
        {
            //Do a replace of the parameter
            _parameters[index] = param;
        }
        /// <summary>
        /// Sets the value of an existing <see cref="DbParameter"/> by using the <paramref name="parameterName"/> and passed in <paramref name="value"/>
        /// </summary>
        /// <param name="parameterName">The name of the parameter to identify the parameter</param>
        /// <param name="value">The value of the parameter as an <see cref="object"/></param>
        public void SetParamaterValue(string parameterName, object value)
        {
            //Get index of this parameter
            int index = _parameters.FindIndex(i => string.Equals(i.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase) == true);

            //Change the value
            SetParamaterValue(index, value);
        }
        /// <summary>
        /// Sets the value of an existing <see cref="DbParameter"/> by using the <paramref name="index"/> and passed in <paramref name="value"/>
        /// </summary>
        /// <param name="index">The index of the parameter in the parameters collection to identify the parameter to retrieve from the collection</param>
        /// <param name="value">The value of the parameter as an <see cref="object"/></param>
        /// <exception cref="IndexOutOfRangeException">thrown when an attempt is made to access an element of an array or collection with an index that is outside the bounds of the array or less than zero.</exception>
        public void SetParamaterValue(int index, object value)
        {
            DbParameter param = GetParameter(index);

            //Now set the value
            param.Value = value;

            //Change the value
            _parameters[index] = param;
        }
        #endregion
    }
}