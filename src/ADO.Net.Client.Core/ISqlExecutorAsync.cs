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
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Contract class that defines asynchronous operations against a database
    /// </summary>
    public interface ISqlExecutorAsync
    {
        #region Data Retrieval
#if !NET45 && !NET461 && !NETSTANDARD2_0
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Gets an instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine</returns>
        Task<T> GetDataObjectAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default) where T : class;
        /// <summary>
        /// Gets a <see cref="IAsyncEnumerable{T}"/> based on the <typeparamref name="T"/> sent into the function to create an object list based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="shouldBePrepared">>An instance of the type the caller wants to create from the query passed into procedure</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="IAsyncEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        IAsyncEnumerable<T> GetDataObjectEnumerableAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default) where T : class;
        /// <summary>
        /// Gets a <see cref="List{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        Task<List<T>> GetDataObjectListAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default) where T : class;
        /// <summary>
        /// Utility method for returning a <see cref="Task{DbDataReader}"/> object
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>A <see cref="DbDataReader"/> object, the caller is responsible for handling closing the DataReader.  Once the data reader is closed, the Database Connection will be closed as well</returns>
        Task<DbDataReader> GetDbDataReaderAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default);
        /// <summary>
        /// Utility method for returning a <see cref="Task{Object}"/> from the database
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the value of the first column in the first row as an object</returns>
        Task<object> GetScalarValueAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default);
#else
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Gets an instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine</returns>
        Task<T> GetDataObjectAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CancellationToken token = default) where T : class;
#if !NET45
        /// <summary>
        /// Gets a <see cref="IAsyncEnumerable{T}"/> based on the <typeparamref name="T"/> sent into the function to create an object list based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants to create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="IAsyncEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        IAsyncEnumerable<T> GetDataObjectEnumerableAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CancellationToken token = default) where T : class;
#endif
        /// <summary>
        /// Gets a <see cref="List{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        Task<List<T>> GetDataObjectListAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CancellationToken token = default) where T : class;
        /// <summary>
        /// Utility method for returning a <see cref="Task{DbDataReader}"/> object
        /// </summary>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>A DbDataReader object, the caller is responsible for handling closing the DataReader.  Once the data reader is closed, the Database Connection will be closed as well</returns>
        Task<DbDataReader> GetDbDataReaderAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default);
        /// <summary>
        /// Utility method for returning a <see cref="Task{Object}"/> from the database
        /// </summary>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters that are associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the value of the first column in the first row as an object</returns>
        Task<object> GetScalarValueAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CancellationToken token = default);

#endif
#endregion
        #region Data Modification
#if !NET45 && !NET461 && !NETSTANDARD2_0
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="token">Propagates notification that operations should be canceled</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query as a <see cref="Task"/></returns>
        Task<int> ExecuteNonQueryAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default);
        /// <summary>
        /// Utility method for executing a query or stored procedure in a SQL transaction
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="transact">An instance of a <see cref="DbTransaction"/> class</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query as a <see cref="Task"/></returns>
        Task<int> ExecuteTransactedNonQueryAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, DbTransaction transact, bool shouldBePrepared = false, CancellationToken token = default);
#else
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure
        /// </summary>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="parameters">The query database parameters that are associated with a query</param>
        /// <param name="token">Propagates notification that operations should be canceled</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query as a <see cref="Task"/></returns>
        Task<int> ExecuteNonQueryAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, CancellationToken token = default);

#endif
        #endregion
    }
}