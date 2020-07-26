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
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// Reader object that reads through multiple result sets
    /// </summary>
    /// <seealso cref="IMultiResultReader"/>
    public partial class MultiResultReader : IMultiResultReader
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
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains data to read through</param>
        public MultiResultReader(DbDataReader reader) : this(reader, new DataMapper())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiResultReader"/> class.
        /// </summary>
        /// <param name="mapper">An instance of <see cref="IDataMapper"/> that will map data contained in the <paramref name="reader"/></param>
        /// <param name="reader">An instance of <see cref="DbDataReader"/> that contains data to read through</param>
        public MultiResultReader(DbDataReader reader, IDataMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }
        #endregion
        #region Helper Methods
        #endregion
    }
}