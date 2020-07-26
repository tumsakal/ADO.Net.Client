#region Using Statements
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Implementation
{
    public partial class MultiResultReader
    {
        #region Utility Methods
        /// <summary>
        /// Gets an entire <see cref="IEnumerable{T}"/> of <typeparamref name="T"/> asynchronously
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns an instance of <see cref="IEnumerable{T}"/> as an entire collection of <typeparamref name="T"/></returns>
        public async Task<IEnumerable<T>> ReadObjectsAsync<T>(CancellationToken token = default) where T : class
        {
            //Keep looping through each object in enumerator
            return await _mapper.MapResultSetAsync<T>(_reader, token).ConfigureAwait(false);
        }
#if !NET45
        /// <summary>
        /// Gets an <see cref="IAsyncEnumerable{T}"/> based on the <typeparamref name="T"/> streamed from the server asynchronously
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns an instance of <see cref="IAsyncEnumerable{T}"/></returns>
        public async IAsyncEnumerable<T> ReadObjectsStreamAsync<T>([EnumeratorCancellation] CancellationToken token = default) where T : class
        {
            //Keep looping through each object in enumerator
            await foreach (T type in _mapper.MapResultSetStreamAsync<T>(_reader, token).ConfigureAwait(false))
            {
                //Keep yielding results
                yield return type;
            }

            //Nothing to do here
            yield break;
        }
#endif
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/> asynchronously
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Gets an instance of <typeparamref name="T"/></returns>
        public async Task<T> ReadObjectAsync<T>(CancellationToken token = default) where T : class
        {
            await _reader.ReadAsync(token).ConfigureAwait(false);

            return _mapper.MapRecord<T>(_reader);
        }
        /// <summary>
        /// Moves to next result set in the underlying data set asynchronously
        /// </summary>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns <c>true</c> if there's another result set in the dataset <c>false</c> otherwise</returns>
        public async Task<bool> MoveToNextResultAsync(CancellationToken token = default)
        {
            //Move to next result set
            return await _reader.NextResultAsync(token).ConfigureAwait(false);
        }
#if !NET45 && !NET461 && !NETSTANDARD2_0
        /// <summary>
        /// Closes the underlying reader object that reads records from the database asynchronously
        /// </summary>
        public async Task CloseAsync()
        {
            await _reader.CloseAsync().ConfigureAwait(false);
        }
#endif
        #endregion
#if !NET45 && !NET461 && !NETSTANDARD2_0
        #region IDisposableAsync Support 
        /// <summary>
        /// Disposes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            return new ValueTask(CloseAsync());
        }
        #endregion
#endif
    }
}
