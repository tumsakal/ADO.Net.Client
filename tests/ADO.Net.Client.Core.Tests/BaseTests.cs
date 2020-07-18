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
using ADO.Net.Client.Tests.Common;
using NUnit.Framework;
using System;
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
        /// <summary>
        /// Called when [time setup].
        /// </summary>
        public abstract void OneTimeSetup();
        #endregion
        #region Basic Tests
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateDbFactoryFromProviderName()
        {
            DbProviderFactory factory = DbObjectFactory.GetProviderFactory("ADO.Net.Client.Tests.Common");

            //Needs to be a mysql client factory
            Assert.IsNotNull(factory);
            Assert.IsInstanceOf(typeof(CustomDbProviderFactory), factory);
        }
        /// <summary>
        /// Determines whether this instance [can create database data source enumerator].
        /// </summary>
        [Test]
        public void CanCreateDbDataSourceEnumerator()
        {
            DbDataSourceEnumerator dbDataSource = _factory.GetDataSourceEnumerator();

            Assert.IsNotNull(dbDataSource);
            Assert.IsInstanceOf(typeof(CustomDbDataSourceEnumerator), dbDataSource);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ThrowsArugementExceptionCantFindProvider()
        {
            Assert.Throws<ArgumentException>(() => DbObjectFactory.GetProviderFactory("ADO.Net.Client.Core"));
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateDbFactoryFromAssembly()
        {
            DbProviderFactory factory = DbObjectFactory.GetProviderFactory(new CustomDbConnection().GetType().Assembly);

            //Needs to be a mysql client factory
            Assert.IsNotNull(factory);
            Assert.IsInstanceOf(typeof(CustomDbProviderFactory), factory);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateDbDataAdapter()
        {
            DbDataAdapter dbDataAdapter = _factory.GetDbDataAdapter();

            Assert.IsNotNull(dbDataAdapter);
            Assert.IsInstanceOf(typeof(CustomDataAdapter), dbDataAdapter);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateDbConnection()
        {
            DbConnection connection = _factory.GetDbConnection();

            Assert.IsNotNull(connection);
            Assert.IsInstanceOf(typeof(CustomDbConnection), connection);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateConnectionStringBuilder()
        {
            DbConnectionStringBuilder builder = _factory.GetDbConnectionStringBuilder();

            Assert.IsNotNull(builder);
            Assert.IsInstanceOf(typeof(CustomConnectionStringBuilder), builder);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanCreateCommandBuilder()
        {
            DbCommandBuilder builder = _factory.GetDbCommandBuilder();

            Assert.IsNotNull(builder);
            Assert.IsInstanceOf(typeof(CustomDbCommandBuilder), builder);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbCommandTests")]
        public void CanCreateDbCommand()
        {
            DbCommand command = _factory.GetDbCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOf(typeof(CustomDbCommand), command);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbCommandTests")]
        public void DbCommandTimeoutSame()
        {
            int commandTimeout = 10;
            DbCommand command = _factory.GetDbCommand(commandTimeout);

            Assert.IsNotNull(command);
            Assert.AreEqual(commandTimeout, command.CommandTimeout);
            Assert.IsInstanceOf(typeof(CustomDbCommand), command);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbCommandTests")]
        public void DbCommandTimeoutConnectionTransaction()
        {
            int commandTimeout = 10;
            CustomDbConnection connection = new CustomDbConnection() { ConnectionString = string.Empty};
            CustomDbTransaction transaction = connection.BeginTransaction() as CustomDbTransaction;
            DbCommand command = _factory.GetDbCommand(connection, transaction, commandTimeout);

            Assert.IsNotNull(command);
            Assert.AreEqual(commandTimeout, command.CommandTimeout);
            Assert.AreEqual(connection, command.Connection);
            Assert.AreEqual(transaction, command.Transaction);
            Assert.IsInstanceOf(typeof(CustomDbTransaction), command.Transaction);
            Assert.IsInstanceOf(typeof(CustomDbConnection), command.Connection);
            Assert.IsInstanceOf(typeof(CustomDbCommand), command);
        }
        [Test]
        [Category("DbParameterTests")]
        public void ThrowsArguementExceptionValueType()
        {
            Assert.Throws<ArgumentException>(() => _factory.GetDbParameters(1));
        }
        [Test]
        [Category("DbParameterTests")]
        public void ThrowsArguementExceptionString()
        {
            Assert.Throws<ArgumentException>(() => _factory.GetDbParameters("Some Value"));
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbParameter()
        {
            DbParameter parameter = _factory.GetDbParameter();

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbParameterNameValue()
        {
            string name = "@ParameterName";
            int value = 200;

            DbParameter parameter = _factory.GetDbParameter(name, value);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbParameterTests")]
        public void CanCreateDbParameterNameNullValue()
        {
            string name = "@ParameterName";
            object value = null;

            DbParameter parameter = _factory.GetDbParameter(name, value);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(DBNull.Value, parameter.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        [Test]
        [TestCase(ParameterDirection.Input)]
        [TestCase(ParameterDirection.Output)]
        [TestCase(ParameterDirection.InputOutput)]
        [TestCase(ParameterDirection.ReturnValue)]
        [Category("DbParameterTests")]
        public void CanCreateParameterByDbType(ParameterDirection direction)
        {
            string name = "@ParameterName";
            int value = 200;
            DbType dbType = DbType.Int32;

            DbParameter parameter = _factory.GetDbParameter(name, value, dbType, direction);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        [Test]
        [Category("DbParameterTests")]
        [TestCase(10, ParameterDirection.Input)]
        [TestCase(10, ParameterDirection.Output)]
        [TestCase(10, ParameterDirection.InputOutput)]
        [TestCase(10, ParameterDirection.ReturnValue)]
        [TestCase(null, ParameterDirection.Input)]
        [TestCase(null, ParameterDirection.Output)]
        [TestCase(null, ParameterDirection.InputOutput)]
        [TestCase(null, ParameterDirection.ReturnValue)]
        public void CanCreateVariableSizeParameter(int? size, ParameterDirection direction)
        {
            string name = "@ParameterName";
            string value = "ParameterValue";
            DbType dbType = DbType.AnsiString;

            DbParameter parameter = _factory.GetVariableSizeDbParameter(name, value, dbType, size, direction);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
            Assert.AreEqual(size ?? 0, parameter.Size);
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
#if !NET45
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="direction"></param>
        [Test]
        [Category("DbParameterTests")]
        [TestCase(null, 10, ParameterDirection.Input)]
        [TestCase(null, 10, ParameterDirection.Output)]
        [TestCase(null, 10, ParameterDirection.InputOutput)]
        [TestCase(null, 10, ParameterDirection.ReturnValue)]
        [TestCase(10, null, ParameterDirection.Input)]
        [TestCase(10, null, ParameterDirection.Output)]
        [TestCase(10, null, ParameterDirection.InputOutput)]
        [TestCase(10, null, ParameterDirection.ReturnValue)]
        [TestCase(10, 10, ParameterDirection.Input)]
        [TestCase(10, 10, ParameterDirection.Output)]
        [TestCase(10, 10, ParameterDirection.InputOutput)]
        [TestCase(10, 10, ParameterDirection.ReturnValue)]
        public void CanCreateFixedSizeParameter(byte? scale, byte? precision, ParameterDirection direction)
        {
            string name = "@ParameterName";
            int value = 200;
            DbType dbType = DbType.Int32;

            DbParameter parameter = _factory.GetFixedSizeDbParameter(name, value, dbType, scale, precision, direction);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOf(typeof(CustomDbParameter), parameter);
            Assert.AreEqual(scale ?? 0, parameter.Scale);
            Assert.AreEqual(precision ?? 0, parameter.Precision);
            Assert.AreEqual(direction, parameter.Direction);
            Assert.AreEqual(dbType, parameter.DbType);
            Assert.AreEqual(name, parameter.ParameterName);
            Assert.AreEqual(value, parameter.Value);
        }
#endif
        #endregion
    }
}