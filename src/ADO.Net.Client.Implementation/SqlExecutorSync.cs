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
#region Using Declarations
using ADO.Net.Client.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Implementation
{
    public partial class SqlExecutor
    {
        #region Data Retrieval
        /// <summary>
        /// Gets an instance of <see cref="DataSet"/>
        /// </summary>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The parameters that are associated with a database query</param>
        /// <param name="query">SQL query to use to build a <see cref="DataSet"/></param>
        /// <returns>Returns an instance of <see cref="DataSet"/> based on the <paramref name="query"/> passed into the routine</returns>
        public DataSet GetDataSet(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters)
        {
            //Wrap this automatically to dispose of resources
            using (DbDataAdapter adap = _factory.GetDbDataAdapter())
            {
                //Wrap this automatically to dispose of resources
                using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
                {
                    DataSet set = new DataSet();

                    //Fill out the dataset
                    adap.SelectCommand = command;
                    adap.Fill(set);

                    //Return this back to the caller
                    return set;
                }
            }
        }
        /// <summary>
        /// Gets an instance of <see cref="DataTable"/>
        /// </summary>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">SQL query to use to build a result set</param>
        /// <returns>Returns an instance of <see cref="DataTable"/></returns>
        public DataTable GetDataTable(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters)
        {
            //Return this back to the caller
            using (DbDataReader reader = GetDbDataReader(query, queryCommandType, parameters))
            {
                DataTable dt = new DataTable();

                //Load in the result set
                dt.Load(reader);

                //Return this back to the caller
                return dt;
            }
        }
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants created from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a database query</param>
        /// <returns>Returns an instance of the <typeparamref name="T"/> based on the fields in the passed in query.  Returns the default value for the <typeparamref name="T"/> if a record is not found</returns>
        public T GetDataObject<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = GetDbDataReader(query, queryCommandType, parameters, CommandBehavior.SingleRow))
            {
                //Get the field name and value pairs out of this query
                IEnumerable<IDictionary<string, object>> results = Utilities.GetDynamicResultsEnumerable(reader);
                IEnumerator<IDictionary<string, object>> enumerator = results.GetEnumerator();
                bool canMove = enumerator.MoveNext();

                //Check if we need to return the default for the type
                if (canMove == false)
                {
                    //Return the default for the type back to the caller
                    return default;
                }
                else
                {
                    //Return this back to the caller
                    return Utilities.GetSingleDynamicType<T>(enumerator.Current);
                }
            }
        }
        /// <summary>
        /// Gets a <see cref="List{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants created from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public List<T> GetDataObjectList<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = GetDbDataReader(query, queryCommandType, parameters, CommandBehavior.SingleResult))
            {
                //Return this back to the caller
                return Utilities.GetDynamicTypeList<T>(Utilities.GetDynamicResultsList(reader));
            }
        }
        /// <summary>
        /// Gets a list of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants created from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The parameters associated with a database query</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public IEnumerable<T> GetDataObjectEnumerable<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = GetDbDataReader(query, queryCommandType, parameters, CommandBehavior.SingleResult))
            {
                //Get the field name and value pairs out of this query
                IEnumerable<IDictionary<string, object>> results = Utilities.GetDynamicResultsEnumerable(reader);
                IEnumerator<IDictionary<string, object>> enumerator = results.GetEnumerator();

                //Keep moving through the enumerator
                while (enumerator.MoveNext() == true)
                {
                    //Return this back to the caller
                    yield return Utilities.GetSingleDynamicType<T>(enumerator.Current);
                }
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="DbDataReader"/> object
        /// </summary>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.CloseConnection"/></param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns an instance of <see cref="DbDataReader"/> object, the caller is responsible for handling closing the DataReader</returns>
        public DbDataReader GetDbDataReader(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CommandBehavior behavior = CommandBehavior.CloseConnection)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Return this back to the caller
                return command.ExecuteReader(behavior);
            }
        }
        /// <summary>
        /// Utility method for returning a scalar value from the database
        /// </summary>
        /// <param name="parameters">The parameters associated with a database query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the value of the first column in the first row returned from the passed in query as an object</returns>
        public object GetScalarValue(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Return this back to the caller
                return command.ExecuteScalar();
            }
        }
        #endregion
        #region Data Modification
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure without a transaction
        /// </summary>
        /// <param name="parameters">The parameters associated with a database query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query</returns>
        public int ExecuteNonQuery(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters)
        {
            //Wrap this in a using statement to automatically handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Return the amount of records affected by this query back to the caller
                return command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Utility method for executing a query or stored procedure in a SQL transaction
        /// </summary>
        /// <param name="transact">An instance of a <see cref="DbTransaction"/> class</param>
        /// <param name="parameters">The parameters associated with a database query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query</returns>
        public int ExecuteTransactedNonQuery(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, DbTransaction transact)
        {
            //Wrap this in a using statement to automatically handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout, transact))
            {
                //Now execute the query
                return command.ExecuteNonQuery();
            }
        }
        #endregion
        #region Helper Methods
        #endregion
    }
}