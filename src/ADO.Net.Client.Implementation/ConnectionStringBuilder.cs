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
using System.Collections.Generic;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IConnectionStringUtility" />
    public class ConnectionStringBuilder : IConnectionStringUtility
    {
        #region Fields/Properties
        private readonly DbConnectionStringBuilder _builder;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString
        {
            get
            {
                return _builder.ConnectionString;
            }
        }
        #endregion
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public ConnectionStringBuilder(DbConnectionStringBuilder builder)
        {
            _builder = builder;
        }
        #endregion
        #region Utility Methods        
        /// <summary>
        /// Adds a property name and value to the current connection string
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        /// <param name="value">The value to use with the connection string property</param>
        public void AddConnectionStringProperty(string name, object value)
        {
            _builder.Add(name, value);
        }
        /// <summary>
        /// Removes a connection string property from the connection string by name
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        public void RemoveConnectionStringProperty(string name)
        {
            _builder.Remove(name);
        }
        /// <summary>
        /// Retrieves a connection string property value as an object
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        /// <returns>
        /// Returns a connection string property as an <see cref="T:System.Object" /></returns>
        public object GetConnectionStringPropertyValue(string name)
        {
            object value;

            if(_builder.TryGetValue(name, out value) == true)
            {
                return value;
            }

            return null;
        }
        /// <summary>
        /// Configures the connection string with the key value pairs passed into the routine
        /// </summary>
        /// <param name="properties">Key value pairs of connection string property names and values</param>
        public void ConfigureConnectionString(IDictionary<string, object> properties)
        {
            foreach(KeyValuePair<string,object> kvp in properties)
            {
                AddConnectionStringProperty(kvp.Key, kvp.Value);
            }
        }
        /// <summary>
        /// Clears the contents of the connection string
        /// </summary>
        public void ClearConnectionString()
        {
            _builder.Clear();
        }
        /// <summary>
        /// Checks if passed in <paramref name="keyword" /> is an allowable keyword in a connection string by the current provider
        /// </summary>
        /// <param name="keyword">The keyword to check in the providers allowable connection string keywords</param>
        /// <returns>
        /// Returns a <see cref="T:System.Boolean" /> indicating if the providers connection string allows the passed in keyword
        /// </returns>
        public bool ConnectionStringAllowsKey(string keyword)
        {
            return _builder.ContainsKey(keyword);
        }
        #endregion
    }
}