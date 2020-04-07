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
using System.Data;
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
        [Category("DbCommandTests")]
        public void CanCreateDbCommand()
        {
            DbCommand command = _factory.GetDbCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual(typeof(MySqlCommand), command.GetType());
        }
        [Test]
        [Category("DbCommandTests")]
        public void DbCommandTimeoutSame()
        {
            int commandTimeout = 10;
            DbCommand command = _factory.GetDbCommand(commandTimeout);

            Assert.IsNotNull(command);
            Assert.AreEqual(commandTimeout, command.CommandTimeout);
            Assert.AreEqual(typeof(MySqlCommand), command.GetType());
        }
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbParameter()
        {
            DbParameter parameter = _factory.GetDbParameter();

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
        }
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbParameterNameValue()
        {
            string name = "@ParameterName";
            int value = 200;

            DbParameter parameter = _factory.GetDbParameter(name, value);

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbType()
        {
            string name = "@ParameterName";
            int value = 200;
            DbType dbType = DbType.Int32;
            ParameterDirection direction = ParameterDirection.Input;

            DbParameter parameter = _factory.GetDbParameter(name, value, dbType, direction);

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        [Test]
        [Category("DbParameterTests")]
        [TestCase(null)]
        [TestCase(10)]
        public void CanCreateVariableizeParameter(int? size)
        {
            string name = "@ParameterName";
            string value = "ParameterValue";
            DbType dbType = DbType.AnsiString;
            ParameterDirection direction = ParameterDirection.Input;

            DbParameter parameter = _factory.GetVariableSizeDbParameter(name, value, dbType, size, direction);

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
            Assert.AreEqual(size ?? 0, parameter.Size);
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        [Test]
        [Category("DbParameterTests")]
        [TestCase(null, 10)]
        [TestCase(10, null)]
        [TestCase(10, 10)]
        public void CanCreateFixedSizeParameter(byte? scale, byte? precision)
        {
            string name = "@ParameterName";
            int value = 200;
            DbType dbType = DbType.Int32;
            ParameterDirection direction = ParameterDirection.Input;

            DbParameter parameter = _factory.GetFixedSizeDbParameter(name, value, dbType, scale, precision, direction);

            Assert.IsNotNull(parameter);
            Assert.AreEqual(typeof(MySqlParameter), parameter.GetType());
            Assert.AreEqual(scale ?? 0, parameter.Scale);
            Assert.AreEqual(precision ?? 0, parameter.Precision);
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        #endregion
    }
}