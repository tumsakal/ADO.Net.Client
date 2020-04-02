#region Using Statements
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Contract class for a transfer object that contains the information about a query to use when querying a datastore
    /// </summary>
    public interface ISqlQuery
    {
        #region Fields/Properties        
        /// <summary>
        /// Represents how a command should be interpreted by the data provider
        /// </summary>
        CommandType QueryType { get; }
        /// <summary>
        /// The query command text or name of stored procedure to execute against the data store
        /// </summary>
        string QueryText { get; }
        /// <summary>
        /// The atabase parameters that are associated with a query
        /// </summary>
        IEnumerable<DbParameter> Parameters { get; }
        #endregion
    }
}