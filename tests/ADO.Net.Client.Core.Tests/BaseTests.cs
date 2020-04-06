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
    [Category("Basic Tests")]
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
        public void CanCreateDbDataAdapter()
        {
            DbDataAdapter dbDataAdapter = _factory.GetDbDataAdapter();

            Assert.IsNotNull(dbDataAdapter);
            Assert.AreEqual(typeof(MySqlDataAdapter), dbDataAdapter.GetType());
        }
        [Test]
        public void CanCreateDbConnection()
        {
            DbConnection connection = _factory.GetDbConnection();

            Assert.IsNotNull(connection);
            Assert.AreEqual(typeof(MySqlConnection), connection.GetType());
        }
        [Test]
        public void CanCreateConnectionStringBuilder()
        {
            DbConnectionStringBuilder builder = _factory.GetDbConnectionStringBuilder();

            Assert.IsNotNull(builder);
            Assert.AreEqual(typeof(MySqlConnectionStringBuilder), builder.GetType());
        }
        [Test]
        public void CanCreateCommandBuilder()
        {
            DbCommandBuilder builder = _factory.GetDbCommandBuilder();

            Assert.IsNotNull(builder);
            Assert.AreEqual(typeof(MySqlCommandBuilder), builder.GetType());
        }
        [Test]
        public void CanCreateDbCommand()
        {
            DbCommand command = _factory.GetDbCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual(typeof(MySqlCommand), command.GetType());
        }
        [Test]
        public void CanCreateDbParameter()
        {
            DbParameter parameter = _factory.GetDbParameter();

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
        }
        #endregion
    }
}