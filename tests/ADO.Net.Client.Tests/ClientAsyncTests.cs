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
using ADO.Net.Client.Implementation;
using ADO.Net.Client.Tests.Common.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Tests
{
    public partial class ClientTests
    {
        #region Read Test Methods
        [Test]
        [Category("Asynchronous Read Tests")]
        public void WhenGetDataObjects_IsCalled_ItShouldCallSqlExectuorGetDataObjectsAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetDataObjectsAsync<DbTypeModel>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared)).ReturnsAsync(new string[1]).Verifiable();

            //Make the call
            IEnumerable<DbTypeModel> values = new DbClient(mockExecutor.Object).GetDataObjects<DbTypeModel>(realQuery);

            //Verify the executor function was called
            mockExecutor.Verify(x => x.GetDataObjects<string>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared), Times.Once);
        }
        [Test]
        [Category("Asynchronous Read Tests")]
        public void WhenGetDataObject_IsCalled_ItShouldCallSqlExectuorGetDataObjectAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetDataObject<string>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared)).Returns(string.Empty).Verifiable();

            //Make the call
            string value = new DbClient(mockExecutor.Object).GetDataObject<string>(realQuery);

            //Verify the executor function was called
            mockExecutor.Verify(x => x.GetDataObject<string>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared), Times.Once);
        }
        [Test]
        [Category("Asynchronous Read Tests")]
        public void WhenGetReader_IsCalled__ItShouldCallSqlExecutorGetReaderAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();
            CommandBehavior behavior = _faker.PickRandom(CommandBehavior.SequentialAccess, CommandBehavior.CloseConnection, CommandBehavior.SingleRow, CommandBehavior.Default, CommandBehavior.SchemaOnly, CommandBehavior.KeyInfo);

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetDbDataReaderAsync(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared, behavior)).Returns(Mock.Of<DbDataReader>()).Verifiable();

            //Make the call
            DbDataReader reader = new DbClient(mockExecutor.Object).GetDbDataReaderAsync(realQuery, behavior);

            //Verify the executor was called
            mockExecutor.Verify(x => x.GetDbDataReader(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared, behavior), Times.Once);
        }
        [Test]
        [Category("Asynchronous Read Tests")]
        public void WhenGetScalar_IsCalled__ItShouldCallSqlExecutorGetScalarAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetScalarValueAsync<string>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared)).Returns(string.Empty).Verifiable();

            //Make the call
            string value = new DbClient(mockExecutor.Object).GetScalarValueAsync<string>(realQuery);

            //Verify the executor was called
            mockExecutor.Verify(x => x.GetScalarValue<string>(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared), Times.Once);
        }
        [Test]
        [Category("Asynchronous Read Tests")]
        public void WhenGetMultiResultReader_IsCalled__ItShouldCallSqlExecutorGetMultiResultReaderAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetMultiResultReaderAsync(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared)).Returns(new MultiResultReader(Mock.Of<DbDataReader>(), Mock.Of<IDataMapper>())).Verifiable();

            //Make the call
            IMultiResultReader reader = new DbClient(mockExecutor.Object).GetMultiResultReader(realQuery);

            Assert.IsNotNull(reader);
            Assert.IsInstanceOf(typeof(IMultiResultReader, reader);

            //Verify the executor was called
            mockExecutor.Verify(x => x.GetMultiResultReader(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared), Times.Once);
        }
        #endregion
        #region Write Test Methods
        [Test]
        [Category("Asynchronous Write Tests")]
        public void WhenExecuteNonQuery_IsCalled__ItShouldCallSqlExecutorExecuteNonQueryAsync()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            //Need to setup the reader function
            mockExecutor.Setup(x => x.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared)).Returns(1).Verifiable();

            //Make the call
            int records = new DbClient(mockExecutor.Object).ExecuteNonQueryAsync(realQuery);

            Assert.IsTrue(records == 1);

            //Verify the executor was called
            mockExecutor.Verify(x => x.ExecuteNonQuery(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared), Times.Once);
        }
        #endregion
    }
}