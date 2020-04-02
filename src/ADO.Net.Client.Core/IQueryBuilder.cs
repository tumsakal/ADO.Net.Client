#region Using Statements
using System.Collections.Generic;
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
    }
}
