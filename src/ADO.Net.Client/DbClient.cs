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

namespace ADO.Net.Client
{
    /// <summary>
    /// Represents the base class for all <see cref="DbClient"/> classes
    /// </summary>
    /// <seealso cref="DbProvider"/>
    /// <seealso cref="ISynchronousClient"/>
    /// <seealso cref="IAsynchronousClient"/>
    /// <remarks>
    /// DbClient is a utility class that encompasses both a <see cref="DbConnection"/> and a <see cref="DbCommand"/>
    /// to help query a database with minimal coding to focus on the users SQL
    /// </remarks>
    public partial class DbClient : DbProvider
    {
        #region Fields/Properties
        /// <summary>
        /// Variable To detect redundant calls of dispose
        /// </summary>
        protected bool _disposed = false;
        private readonly ISqlExecutor _executor;
        #endregion
        #region Constructors
        /// <summary>
        /// Instantiates a new instance of <see cref="DbClient"/> with an instance of <see cref="ISqlExecutor"/>
        /// </summary>
        /// <param name="executor">An instance of <see cref="ISqlExecutor"/></param>
        public DbClient(ISqlExecutor executor)
        {
            _executor = executor;
        }
        #endregion
    }
}