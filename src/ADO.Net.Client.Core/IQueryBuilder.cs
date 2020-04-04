﻿#region Licenses
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
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbParameterUtility"/>
    public interface IQueryBuilder : IDbParameterUtility
    {
        #region Fields/Properties        
        /// <summary>
        /// The database parameters associated with a query
        /// </summary>
        /// <value>
        /// The parameters associated with a query as a <see cref="IEnumerable{T}"/> of <see cref="DbParameter"/>
        /// </value>
        IEnumerable<DbParameter> Parameters { get; }
        #endregion
        #region Utility Methods
        void ClearSQLQuery();
        void Append(string sql);
        void Append(string sql, DbParameter parameter);
        void Append(string sql, IEnumerable<DbParameter> parameters);
        ISqlQuery CreateSQLQuery(CommandType type);
        #endregion
    }
}