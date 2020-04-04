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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbProvider" />
    public abstract class DbProvider : IDbProvider
    {
        #region Data Modification
        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(ISqlQuery query);
        /// <summary>
        /// Executes the non query asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<int> ExecuteNonQueryAsync(ISqlQuery query, CancellationToken token = default);
        /// <summary>
        /// Executes the transacted non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="transact">The transact.</param>
        /// <returns></returns>
        public abstract int ExecuteTransactedNonQuery(ISqlQuery query, DbTransaction transact);
#if !NET461 && !NETSTANDARD2_0
        /// <summary>
        /// Executes the transacted non query asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="transact">The transact.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<int> ExecuteTransactedNonQueryAsync(ISqlQuery query, DbTransaction transact, CancellationToken token = default);
#endif
        #endregion
        #region Data Retrieval        
        /// <summary>
        /// Gets the data object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract T GetDataObject<T>(ISqlQuery query) where T : class;
        /// <summary>
        /// Gets the data object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<T> GetDataObjectAsync<T>(ISqlQuery query, CancellationToken token = default) where T : class;
        /// <summary>
        /// Gets the data object enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract IEnumerable<T> GetDataObjectEnumerable<T>(ISqlQuery query) where T : class;
        /// <summary>
        /// Gets the data object enumerable asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract IAsyncEnumerable<T> GetDataObjectEnumerableAsync<T>(ISqlQuery query, CancellationToken token = default) where T : class;
        /// <summary>
        /// Gets the data object list asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<List<T>> GetDataObjectListAsync<T>(ISqlQuery query, CancellationToken token = default) where T : class;
        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract DataSet GetDataSet(ISqlQuery query);
        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract DataTable GetDataTable(ISqlQuery query);
        /// <summary>
        /// Gets the data table asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<DataTable> GetDataTableAsync(ISqlQuery query, CancellationToken token = default);
        /// <summary>
        /// Gets the database data reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns></returns>
        public abstract DbDataReader GetDbDataReader(ISqlQuery query, CommandBehavior behavior = CommandBehavior.Default);
        /// <summary>
        /// Gets the database data reader asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<DbDataReader> GetDbDataReaderAsync(ISqlQuery query, CommandBehavior behavior = CommandBehavior.Default, CancellationToken token = default);
        /// <summary>
        /// Gets the scalar value.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public abstract object GetScalarValue(ISqlQuery query);
        /// <summary>
        /// Gets the scalar value asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract Task<object> GetScalarValueAsync(ISqlQuery query, CancellationToken token = default);
        #endregion
    }
}