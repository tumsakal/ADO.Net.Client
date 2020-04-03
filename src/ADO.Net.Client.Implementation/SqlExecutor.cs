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
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// Utility class that provides methods for both retrieving and modifying data in a data store
    /// </summary>
    /// <seealso cref="ISqlExecutor"/>
    /// <remarks>
    /// SqlExecutor is a class that encompasses a <see cref="DbCommand"/> but the user of
    /// the class must manage the <see cref="DbConnection"/> the class will use
    /// </remarks>
    public partial class SqlExecutor : ISqlExecutor
    {
        #region Fields/Properties
        private readonly IDbObjectFactory _factory;
        private readonly IConnectionManager _manager;

        /// <summary>
        /// Gets or sets the wait time in seconds before terminating the attempt to execute a command and generating an error.
        /// </summary>
        public int CommandTimeout { get; set; } = 30;
        /// <summary>
        /// The character symbol to use when binding a variable in a given providers SQL query
        /// </summary>
        public string VariableBinder { get; set; }
        #endregion
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecutor"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="manager">The manager.</param>
        public SqlExecutor(IDbObjectFactory factory, IConnectionManager manager)
        {
            _manager = manager;
            _factory = factory;
        }
        #endregion
        #region Helper Methods
        #endregion
    }
}