#region Using Statements
using ADO.Net.Client.Core;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IConnectionManager" />
    public class ConnectionManager : IConnectionManager
    {
        #region Fields/Properties
        private readonly DbConnection _connection;
        private readonly DbConnectionStringBuilder _builder;
        #endregion
        #region Constructors		        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionManager"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="builder">The builder.</param>
        public ConnectionManager(DbConnection connection, DbConnectionStringBuilder builder)
        {
            _connection = connection;
            _builder = builder;
        }
        #endregion
        #region Utility Methods
        public void AddConnectionStringProperty(string name, object value)
        {
            throw new System.NotImplementedException();
        }

        public void ClearConnectionString()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveConnectionStringProperty(string name)
        {
            throw new System.NotImplementedException();
        }
        public void ConfigureConnectionString(IDictionary<string, object> properties)
        {
            throw new System.NotImplementedException();
        }

        public bool ConnectionStringAllowsKey(string keyword)
        {
            throw new System.NotImplementedException();
        }

        public object GetConnectionStringPropertyValue(string name)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region Connection Methods
        public void Open()
        {
            _connection.Open();
        }
        public void Close()
        {
            _connection.Close();
        }
        public async Task OpenAsync(CancellationToken token = default)
        {
            await _connection.OpenAsync(token).ConfigureAwait(false);
        }
#if NETSTANDARD2_1
        public async Task CloseAsync()
        {
            await _connection.CloseAsync().ConfigureAwait(false);
        }
#endif
#endregion
    }
}