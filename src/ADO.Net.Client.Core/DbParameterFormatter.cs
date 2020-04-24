#region Using Statements
using System.Data;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbParameterFormatter" />
    public class DbParameterFormatter : IDbParameterFormatter
    {
        #region Utility Methods
        /// <summary>
        /// Gets the <see cref="DbType"/> associated with the passed in <paramref name="parameterValue"/>
        /// </summary>
        /// <param name="parameterValue">The .NET value that will be mapped to a providers native data type</param>
        /// <returns>Returns a <see cref="DbType"/> value that describes the RDBMS type of passed in <paramref name="parameterValue"/></returns>
        public DbType GetDbType(object parameterValue)
        {
            throw new System.NotImplementedException();
        }

        public object MapParameterValue(object value)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
