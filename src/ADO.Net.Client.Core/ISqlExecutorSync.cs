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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// /Contract class that defines syncrhonous operations against a database
    /// </summary>
    public interface ISqlExecutorSync
    {
        #region Data Retrieval
        /// <summary>
        /// Gets an instance of <see cref="DataSet"/>
        /// </summary>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns an instance of <see cref="DataSet"/> based on the <paramref name="query"/> passed into the routine</returns>
        DataSet GetDataSet(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        /// <summary>
        /// Gets an instance of <see cref="DataTable"/>
        /// </summary>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns an instance of <see cref="DataTable"/></returns>
        DataTable GetDataTable(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns an instance of the <typeparamref name="T"/> based on the fields in the passed in query.  Returns the default value for the type if a record is not found</returns>
        T GetDataObject<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters) where T : class;
        /// <summary>
        /// Gets a <see cref="List{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants created from the query passed into procedure</typeparam>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param> <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        List<T> GetDataObjectList<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        IEnumerable<T> GetDataObjectEnumerable<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        /// <summary>
        /// Utility method for returning a DataReader object
        /// </summary>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>An instance of <see cref="DbDataReader"/> object</returns>
        DbDataReader GetDbDataReader(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CommandBehavior behavior = default);
        /// <summary>
        /// Utility method for returning a scalar value from the database
        /// </summary>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns the value of the first column in the first row returned from the passed in query as an object</returns>
        object GetScalarValue(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        #endregion
        #region Data Modification
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure without a transaction
        /// </summary>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns the number of rows affected by this query</returns>
        int ExecuteNonQuery(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters);
        /// <summary>
        /// Utility method for executing a query or stored procedure in a SQL transaction
        /// </summary>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Returns the number of rows affected by this query</returns>
        int ExecuteTransactedNonQuery(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, DbTransaction transact);
        #endregion
    }
}