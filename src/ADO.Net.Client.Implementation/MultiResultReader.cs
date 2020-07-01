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
using ADO.Net.Client.Core;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// Reader object that reads through multiple result sets
    /// </summary>
    /// <seealso cref="IMultiResultReader"/>
    public class MultiResultReader : IMultiResultReader
    {
        #region Variables
        private bool disposedValue = false; // To detect redundant calls
        #endregion
        #region Fields/Properties
        private readonly DbDataReader _reader = null;
        private readonly IDataMapper _mapper = null;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiResultReader"/> class.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="reader">The reader.</param>
        public MultiResultReader(DbDataReader reader, IDataMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        #endregion
        #region Async Methods
        /// <summary>
        /// Gets an entire <see cref="IEnumerable{T}"/> of <typeparamref name="T"/> asynchronously
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <param name="token">Structure that propogates a notification that an operation should be cancelled</param>
        /// <returns>Returns an instance of <see cref="IEnumerable{T}"/> as an entire collection of <typeparamref name="T"/></returns>
        public async Task<IEnumerable<T>> ReadObjectsAsync<T>(CancellationToken token = default)
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
        public async IAsyncEnumerable<T> ReadObjectsStreamAsync<T>([EnumeratorCancellation] CancellationToken token = default)
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
        public async Task<T> ReadObjectAsync<T>(CancellationToken token = default)
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
        #endregion
        #region Sync Methods        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> based on the <typeparamref name="T"/> streamed from the server
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <returns>Returns an instance of <see cref="IEnumerable{T}"/></returns>
        public IEnumerable<T> ReadObjectsStream<T>()
        {
            //Keep looping through each object in enumerator
            foreach (T type in _mapper.MapResultSetStream<T>(_reader))
            {
                //Keep yielding results
                yield return type;
            }

            //Nothing to do here
            yield break;
        }
        /// <summary>
        /// Gets an entire <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <returns>Returns an instance of <see cref="IEnumerable{T}"/> as an entire collection of <typeparamref name="T"/></returns>
        public IEnumerable<T> ReadObjects<T>()
        {
            return _mapper.MapResultSet<T>(_reader);
        }
        /// <summary>
        /// Gets a single instance of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">An instance of the type the caller wants create from the query passed into procedure</typeparam>
        /// <returns>Gets an instance of <typeparamref name="T"/></returns>
        public T ReadObject<T>()
        {
            //Move to the next record
            _reader.Read();

            return _mapper.MapRecord<T>(_reader);
        }
        /// <summary>
        /// Moves to the next result in the underlying data set
        /// </summary>
        /// <returns>Returns <c>true</c> if there's another result set in the underlying data set <c>false</c> otherwise</returns>
        public bool MoveToNextResult()
        {
            //Move to next result set
            return _reader.NextResult();
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Closes the underlying reader object that reads records from the database synchronously
        /// </summary>
        public void Close()
        {
            _reader.Close();
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
        #region IDisposable Support            
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            //Check if we haven't disposed
            if (!disposedValue)
            {
                //Check if we are disposing of managed objects
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}