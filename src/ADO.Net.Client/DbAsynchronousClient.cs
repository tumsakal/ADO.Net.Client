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

namespace ADO.Net.Client
{
    public partial class DbClient
    {
        #region Data Retrieval
        /// <summary>
        /// Gets an instance of <see cref="DataTable"/> asynchronously
        /// </summary>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="query">SQL query to use to build a <see cref="DataTable"/></param>
        /// <returns>Returns a <see cref="Task{DataTable}"/> of datatable</returns>
        public override async Task<DataTable> GetDataTableAsync(ISqlQuery query, CancellationToken token = default)
        {
            DataTable dt = new DataTable();

            dt.Load(await GetDbDataReaderAsync(query, CommandBehavior.SingleResult, token).ConfigureAwait(false));

            //Return this back to the caller
            return dt;
        }
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from query passed into procedure</typeparam>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <returns>Gets an instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine.
        /// Or the default value of <typeparamref name="T"/> if there are no search results
        /// </returns>
        public override async Task<T> GetDataObjectAsync<T>(ISqlQuery query, CancellationToken token = default) where T : class
        {
            //Return this back to the caller
            return await _executor.GetDataObjectAsync<T>(query.QueryText, query.QueryType, query.Parameters, token).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets a list of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public override async Task<List<T>> GetDataObjectListAsync<T>(ISqlQuery query, CancellationToken token = default) where T : class
        {
            //Return this back to the caller
            return await _executor.GetDataObjectListAsync<T>(query.QueryText, query.QueryType, query.Parameters, token).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets a list of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="IAsyncEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public override async IAsyncEnumerable<T> GetDataObjectEnumerableAsync<T>(ISqlQuery query, [EnumeratorCancellation] CancellationToken token = default) where T : class
        {
            //Return this back to the caller
            await foreach (T type in _executor.GetDataObjectEnumerableAsync<T>(query.QueryText, query.QueryType, query.Parameters, token).ConfigureAwait(false))
            {
                yield return type;
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{DbDataReader}"/> object created from the passed in query
        /// </summary>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>A <see cref="Task{DbDataReader}"/> object, the caller is responsible for handling closing the <see cref="DbDataReader"/>.  Once the data reader is closed, the database connection will be closed as well</returns>
        public override async Task<DbDataReader> GetDbDataReaderAsync(ISqlQuery query, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default)
        {
            //Return this back to the caller
            return await _executor.GetDbDataReaderAsync(query.QueryText, query.QueryType, query.Parameters, behavior, token).ConfigureAwait(false);
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{Object}"/> value from the database
        /// </summary>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns the value of the first column in the first row as <see cref="Task"/></returns>
        public override async Task<object> GetScalarValueAsync(ISqlQuery query, CancellationToken token = default)
        {
            //Return this back to the caller
            return await _executor.GetScalarValueAsync(query.QueryText, query.QueryType, query.Parameters, token).ConfigureAwait(false);
        }
        #endregion
        #region Data Modification        
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure without a transaction
        /// </summary>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>
        /// Returns the number of rows affected by this query as a <see cref="T:System.Threading.Tasks.Task`1" />
        /// </returns>
        public override async Task<int> ExecuteNonQueryAsync(ISqlQuery query, CancellationToken token = default)
        {
            return await _executor.ExecuteNonQueryAsync(query.QueryText, query.QueryType, query.Parameters, token).ConfigureAwait(false);
        }
#if !NET461 && !NETSTANDARD2_0        
        /// <summary>
        /// Utility method for executing an Ad-Hoc query or stored procedure with a transaction
        /// </summary>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="transact">An instance of a <see cref="T:System.Data.Common.DbTransaction" /> class</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>
        /// Returns the number of rows affected by this query as a <see cref="T:System.Threading.Tasks.Task`1" />
        /// </returns>
        public override async Task<int> ExecuteTransactedNonQueryAsync(ISqlQuery query, DbTransaction transact, CancellationToken token = default)
        {
            return await _executor.ExecuteTransactedNonQueryAsync(query.QueryText, query.QueryType, query.Parameters, transact, token).ConfigureAwait(false);
        }
#endif
        #endregion
    }
}