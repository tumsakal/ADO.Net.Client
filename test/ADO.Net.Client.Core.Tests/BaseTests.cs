#region Using Statements
using ADO.Net.Client.Core;
using MySql.Data.MySqlClient;
using NUnit;
using NUnit.Framework;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    /// <summary>
    /// Base test class
    /// </summary>
    [TestFixture]
    public abstract class BaseTests
    {
        #region Fields/Properties
        protected IDbObjectFactory _factory;
        #endregion
        #region Setup/Teardown
        public abstract void OneTimeSetup();
        #endregion
        #region Basic Tests
        [Test]
        [Category("Basic Tests")]
        public void CanCreateDbDataAdapter()
        {
            DbDataAdapter dbDataAdapter = _factory.GetDbDataAdapter();

            Assert.IsNotNull(dbDataAdapter);
            Assert.AreEqual(typeof(MySqlDataAdapter), dbDataAdapter.GetType());
        }
        [Test]
        [Category("Basic Tests")]
        public void CanCreateDbConnection()
        {
            DbConnection connection = _factory.GetDbConnection();

            Assert.IsNotNull(connection);
            Assert.AreEqual(typeof(MySqlConnection), connection.GetType());
        }
        [Test]
        [Category("Basic Tests")]
        public void CanCreateConnectionStringBuilder()
        {
            DbConnectionStringBuilder builder = _factory.GetDbConnectionStringBuilder();

            Assert.IsNotNull(builder);
            Assert.AreEqual(typeof(MySqlCommand), builder.GetType());
        }
        [Test]
        [Category("Basic Tests")]
        public void CanCreateCommandBuilder()
        {
            DbCommandBuilder builder = _factory.GetDbCommandBuilder();
            Assert.IsNotNull(builder);
            Assert.AreEqual(typeof(MySqlCommandBuilder), builder.GetType());
        }
        [Test]
        [Category("Basic Tests")]
        public void CanCreateDbCommand()
        {

            DbCommand command = _factory.GetDbCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual(typeof(MySqlCommand), command.GetType());
        }
        [Test]
        [Category("Basic Tests")]
        public void CanCreateDbParameter()
        {
            DbParameter parameter = _factory.GetDbParameter();

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
        }
        #endregion
    }
}