#region Using Statements
using System.Data;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    interface IDataMapper
    {
        #region Utility Methods
        /// <summary>
        /// Maps the record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record">The record.</param>
        /// <returns></returns>
        object MapRecord<T>(IDataRecord record);
        #endregion
    }
}
