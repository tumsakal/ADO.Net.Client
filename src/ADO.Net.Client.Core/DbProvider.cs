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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Base class that implements the basic functionality of an ADO.NET driver
    /// </summary>
    /// <seealso cref="IDbProvider"/>
    public abstract class DbProvider : IDbProvider
    {
        #region Events        
        /// <summary>
        /// Sets the state change event handler.  This event occurs when the <see cref="DbConnection.State"/> changes
        /// </summary>
        /// <value>
        /// The state change handler delegate
        /// </value>
        public abstract event StateChangeEventHandler StateChange;
        #endregion
        #region Fields/Properties        
        /// <summary>
        /// Gets or sets a value indicating whether this instance connection to database is open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the connecion to the database is open; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsOpen { get; }
        /// <summary>
        /// Whether or not the passed in provider is capable of creating a data source enumerator
        /// </summary>
        public abstract bool CanCreateDataSourceEnumerator { get; }
        /// <summary>
        /// Whether or not the <see cref="ConnectionString"/> is readonly
        /// </summary>
        public abstract bool ConnectionStringReadonly { get; }
        /// <summary>
        /// Whether the <see cref="ConnectionString"/> has a fixed size
        /// </summary>
        public abstract bool ConnectionStringFixedSize { get; }
        /// <summary>
        /// Gets the current number of keys that are contained within the <see cref="ConnectionString"/> property
        /// </summary>
        public abstract int ConnectionStringKeyCount { get; }
        /// <summary>
        /// Gets a string that represents the version of the server to which the object is connected
        /// </summary>
        public abstract string ServerVersion { get; }
        /// <summary>
        /// The current <see cref="ConnectionState"/> of the <see cref="DbConnection"/>
        /// </summary>
        public abstract ConnectionState State { get; }
        /// <summary>
        /// Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the <see cref="DbConnection"/> is opened.
        /// </summary>
        public abstract string Database { get; }
        /// <summary>
        /// Gets the name of the database server to which to connect.
        /// </summary>
        public abstract string DataSource { get; }
        /// <summary>
        /// ConnectionString as a <see cref="string"/> to use when creating a <see cref="DbConnection"/>
        /// </summary>
        public abstract string ConnectionString { get; set; }
        /// <summary>
        /// The character symbol to use when binding a variable in a given providers SQL query
        /// </summary>
        public abstract string VariableBinder { get; set; }
        /// <summary>
        /// Represents how a command should be interpreted by the data provider.  Default value is <see cref="CommandType.Text"/>
        /// </summary>
        public abstract CommandType QueryCommandType { get; set; }
        /// <summary>
        /// Gets or sets the wait time in seconds before terminating the attempt to execute a command and generating an error.  Default value is 30
        /// </summary>
        public abstract int CommandTimeout { get; set; }
        /// <summary>
        /// Gets the time to wait in seconds while trying to establish a connection before terminating the attempt and generating an error.
        /// </summary>
        public abstract int ConnectionTimeout { get; }
        #endregion
        #region Constructors
        #endregion
        #region Connection String Methods        
        /// <summary>
        /// Adds a property name and value to the current connection string
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        /// <param name="value">The value to use with the connection string property</param>
        public abstract void AddConnectionStringProperty(string name, object value);
        /// <summary>
        /// Removes a connection string property from the connection string by name
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        public abstract void RemoveConnectionStringProperty(string name);
        /// <summary>
        /// Retrieves a connection string property value as an object
        /// </summary>
        /// <param name="name">The name of the connection string property</param>
        /// <returns>
        /// Returns a connection string property as an <see cref="object" />
        /// </returns>
        public abstract object GetConnectionStringPropertyValue(string name);
        /// <summary>
        /// Configures the connection string with the key value pairs passed into the routine
        /// </summary>
        /// <param name="properties">Key value pairs of connection string property names and values</param>
        public abstract void ConfigureConnectionString(IDictionary<string, object> properties);
        /// <summary>
        /// Clears the contents of the connection string
        /// </summary>
        public abstract void ClearConnectionString();
        /// <summary>
        /// Checks if passed in <paramref name="keyword" /> is an allowable keyword in a connection string by the current provider
        /// </summary>
        /// <param name="keyword">The keyword to check in the providers allowable connection string keywords</param>
        /// <returns>
        /// Returns a <see cref="bool" /> indicating if the providers connection string allows the passed in keyword
        /// </returns>
        public abstract bool ConnectionStringAllowsKey(string keyword);
        #endregion
    }
}