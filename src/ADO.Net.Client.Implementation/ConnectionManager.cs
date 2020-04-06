﻿#region Licenses
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
    /// <seealso cref="IConnectionManager" />
    public class ConnectionManager : IConnectionManager
    {
        #region Fields/Properties
        private readonly DbConnection _connection;
        private readonly DbConnectionStringBuilder _builder;


        public DbConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        #endregion
        #region Constructors		        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionManager"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="builder">The builder.</param>
        public ConnectionManager(DbConnection connection, DbConnectionStringBuilder builder)
        {
            _connection = connection;
            _builder = builder;
        }
        #endregion
        #region Utility Methods
        public void AddConnectionStringProperty(string name, object value)
        {
            throw new System.NotImplementedException();
        }

        public void ClearConnectionString()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveConnectionStringProperty(string name)
        {
            throw new System.NotImplementedException();
        }
        public void ConfigureConnectionString(IDictionary<string, object> properties)
        {
            throw new System.NotImplementedException();
        }

        public bool ConnectionStringAllowsKey(string keyword)
        {
            throw new System.NotImplementedException();
        }

        public object GetConnectionStringPropertyValue(string name)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}