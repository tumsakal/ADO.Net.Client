#region Using Statements
using System;
using System.Data;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// Contract class that maps data from a record in a database to a .NET object
    /// </summary>
    public interface IDataMapper
    {
        #region Utility Methods
        /// <summary>
        /// Maps the passed in <paramref name="record"/> to an instance of the <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> the caller wants created from the passed in <paramref name="record"/></typeparam>
        /// <param name="record">A record from a result set of data</param>
        /// <returns></returns>
        T MapRecord<T>(IDataRecord record);
        #endregion
    }
}