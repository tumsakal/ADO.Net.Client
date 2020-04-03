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
#region Using Declarations
using ADO.Net.Client.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Implementation
{
    public partial class SqlExecutor
    {
        #region Data Retrieval
        /// <summary>
        /// Gets an instance of the <typeparamref name="T"/> parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Gets an instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine.
        /// Or the default value of <typeparamref name="T"/> if there are no search results
        /// </returns>
        public async Task<T> GetDataObjectAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, CommandBehavior.SingleRow, token).ConfigureAwait(false))
            {
                //Check if the reader has rows
                if (reader.HasRows == true)
                {
                    //Return this back to the caller
                    return Utilities.GetSingleDynamicType<T>(await Utilities.GetDynamicResultAsync(reader, token).ConfigureAwait(false));
                }
                else
                {
                    return default;
                }
            }
        }
        /// <summary>
        /// Gets an <see cref="IAsyncEnumerable{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="IAsyncEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public async IAsyncEnumerable<T> GetDataObjectEnumerableAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, [EnumeratorCancellation] CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, CommandBehavior.SingleResult, token).ConfigureAwait(false))
            {
                //Check if the reader has rows first
                if (reader.HasRows == true)
                {
                    //Keep going through the results
                    await foreach (IDictionary<string, object> kvp in Utilities.GetDynamicResultsEnumerableAsync(reader, token))
                    {
                        //Return this back to the caller
                        yield return Utilities.GetSingleDynamicType<T>(kvp);
                    }
                }

                //Nothing to do here
                yield break;
            }
        }
        /// <summary>
        /// Gets a <see cref="List{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public async Task<List<T>> GetDataObjectListAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, CommandBehavior.SingleResult, token).ConfigureAwait(false))
            {
                //Get the field name and value pairs out of this query
                List<IDictionary<string, object>> results = await Utilities.GetDynamicResultsListAsync(reader, token).ConfigureAwait(false);

                //Return this back to the caller
                return Utilities.GetDynamicTypeList<T>(results);
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{DbDataReader}"/> object
        /// </summary>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns an instance of <see cref="DbDataReader"/> object, the caller is responsible for handling closing the DataReader</returns>
        public async Task<DbDataReader> GetDbDataReaderAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = Factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Get the data reader
                return await command.ExecuteReaderAsync(behavior, token).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{Object}"/> value from the database
        /// </summary>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the value of the first column in the first row returned from the passed in query as an object</returns>
        public async Task<object> GetScalarValueAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CancellationToken token = default)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = Factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Return this back to the caller
                return await command.ExecuteScalarAsync(token).ConfigureAwait(false);
            }
        }
        #endregion
        #region Data Modification
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure without a transaction
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query as a <see cref="Task{Int32}"/></returns>
        public async Task<int> ExecuteNonQueryAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, CancellationToken token = default)
        {
            //Wrap this in a using statement to automatically handle disposing of resources
            using (DbCommand command = Factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout))
            {
                //Return this back to the caller
                return await command.ExecuteNonQueryAsync(token).ConfigureAwait(false);
            }
        }
#if !NET461 && !NETSTANDARD2_0      
        /// <summary>
        /// Utility method for executing a query or stored procedure in a SQL transaction
        /// </summary>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="transact">An instance of a <see cref="DbTransaction"/> class</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns the number of rows affected by this query</returns>
        public async Task<int> ExecuteTransactedNonQueryAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, DbTransaction transact, CancellationToken token = default)
        {
            //Wrap this in a using statement to automatically dispose of resources
            using (DbCommand command = Factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, CommandTimeout, transact))
            {
                //Get the number of records affected by this query
                return await command.ExecuteNonQueryAsync(token).ConfigureAwait(false);
            }
        }
#endif
        #endregion
        #region Helper Methods
        #endregion
    }
}