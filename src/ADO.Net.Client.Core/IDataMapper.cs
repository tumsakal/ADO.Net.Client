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
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Contract class that defines the methods that map data from a record in a database to a .NET object
    /// </summary>
    public interface IDataMapper
    {
        #region Utility Methods
#if !NET45
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <param name="token"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        IAsyncEnumerable<T> MapResultSetStreamAsync<T>(DbDataReader reader, CancellationToken token = default);
#endif
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <param name="token"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> MapResultSetAsync<T>(DbDataReader reader, CancellationToken token = default);
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        IEnumerable<T> MapResultSetStream<T>(DbDataReader reader);
        /// <summary>
        /// Maps the result set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        IEnumerable<T> MapResultSet<T>(DbDataReader reader);
        /// <summary>
        /// Maps the passed in <paramref name="record"/> to an instance of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="record"/></typeparam>
        /// <param name="record">A record from a result set of data</param>
        /// <returns>Returns the instance of <typeparamref name="T"/> created from the passed in <paramref name="record"/></returns>
        T MapRecord<T>(IDataRecord record);
        #endregion
    }
}