#region Using Statements
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
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Gets an instance of <typeparamref name="T"/> based on the <paramref name="query"/> passed into the routine.
        /// Or the default value of <typeparamref name="T"/> if there are no search results
        /// </returns>
        public async Task<T> GetDataObjectAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, commandTimeout, shouldBePrepared, CommandBehavior.SingleRow, token).ConfigureAwait(false))
            {
                //Check if the reader has rows
                if (reader.HasRows == true)
                {
                    //Move to the first record in the result set
                    await reader.ReadAsync(token).ConfigureAwait(false);

                    //Return this back to the caller
                    return _mapper.MapRecord<T>(reader);
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
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="IAsyncEnumerable{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public async IAsyncEnumerable<T> GetDataObjectsStreamAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, [EnumeratorCancellation] CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, commandTimeout, shouldBePrepared, CommandBehavior.SingleResult, token).ConfigureAwait(false))
            {
                //Check if the reader has rows first
                if (reader.HasRows == true)
                {
                    //Keep iterating
                    await foreach (T t in _mapper.MapResultSetStreamAsync<T>(reader, token).ConfigureAwait(false))
                    {
                        //Return this back to the caller
                        yield return t;
                    }
                }

                //Nothing to do here
                yield break;
            }
        }
        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of the type parameter object that creates an object based on the query passed into the routine
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">The wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <typeparam name="T">An instance of the type caller wants create from the query passed into procedure</typeparam>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns a <see cref="List{T}"/> based on the results of the passed in <paramref name="query"/></returns>
        public async Task<IEnumerable<T>> GetDataObjectsAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default) where T : class
        {
            //Wrap this to automatically handle disposing of resources
            using (DbDataReader reader = await GetDbDataReaderAsync(query, queryCommandType, parameters, commandTimeout, shouldBePrepared, CommandBehavior.SingleResult, token).ConfigureAwait(false))
            {
                return await _mapper.MapResultSetAsync<T>(reader, token).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{DbDataReader}"/> object
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">Gets or sets the wait time in seconds before terminating the attempt to execute a command and generating an error</param>
        /// <param name="behavior">Provides a description of the results of the query and its effect on the database.  Defaults to <see cref="CommandBehavior.Default"/></param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <returns>Returns an instance of <see cref="DbDataReader"/> object, the caller is responsible for handling closing the DataReader</returns>
        public async Task<DbDataReader> GetDbDataReaderAsync(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, commandTimeout))
            {
                if (shouldBePrepared == true)
                {
                    await command.PrepareAsync(token).ConfigureAwait(false);
                }

                //Get the data reader
                return await command.ExecuteReaderAsync(behavior, token).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Utility method for returning a <see cref="Task{Object}"/> value from the database
        /// </summary>
        /// <param name="shouldBePrepared">Indicates if the current <paramref name="query"/> needs to be prepared (or compiled) version of the command on the data source.</param>
        /// <param name="commandTimeout">Gets or sets the wait time in seconds before terminating the attempt to execute a command and generating an errors</param>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <param name="parameters">The database parameters associated with a query</param>
        /// <param name="query">The query command text or name of stored procedure to execute against the data store</param>
        /// <param name="queryCommandType">Represents how a command should be interpreted by the data provider</param>
        /// <typeparam name="T">The data type to return from data value returned from the query</typeparam>
        /// <returns>Returns the value of the first column in the first row as an instance of <typeparamref name="T"/></returns>
        public async Task<T> GetScalarValueAsync<T>(string query, CommandType queryCommandType, IEnumerable<DbParameter> parameters, int commandTimeout, bool shouldBePrepared = false, CancellationToken token = default)
        {
            //Wrap this in a using statement to handle disposing of resources
            using (DbCommand command = _factory.GetDbCommand(queryCommandType, query, parameters, _manager.Connection, commandTimeout))
            {
                if (shouldBePrepared == true)
                {
                    await command.PrepareAsync(token).ConfigureAwait(false);
                }

                //Return this back to the caller
                return Utilities.GetTypeFromValue<T>(await command.ExecuteScalarAsync(token).ConfigureAwait(false));
            }
        }
        #endregion
    }
}
