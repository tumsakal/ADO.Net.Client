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
using Bogus;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#endregion

namespace ADO.Net.Client.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class ClientTests
    {
        #region Fields/Properties
        private ISqlQuery realQuery;
        private Faker _faker;
        #endregion
        #region Constructors
        public ClientTests()
        {
            _faker = new Faker();
        }
        #endregion
        #region Setup/Teardown
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Mock<ISqlQuery> mockQuery = new Mock<ISqlQuery>();

            mockQuery.Setup(x => x.CommandTimeout).Returns(_faker.Random.Int());
            mockQuery.Setup(x => x.QueryText).Returns(_faker.Random.String());
            mockQuery.Setup(x => x.QueryType).Returns(_faker.PickRandomParam(CommandType.StoredProcedure, CommandType.Text, CommandType.TableDirect));
            //mockQuery.Setup(x => x.Parameters).Returns(null);

            realQuery = mockQuery.Object;
        }
        #endregion
        #region Test Methods
        [Test]
        public void WhenGetDataObject_IsCalled_ItShouldCallSqlExectuorGetDataObject()
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
        public void WhenGetReaderIsCalled__ItShouldCallSqlExecutorGetReader()
        {
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();
            CommandBehavior behavior = _faker.PickRandom(CommandBehavior.SequentialAccess, CommandBehavior.CloseConnection, CommandBehavior.SingleRow, CommandBehavior.Default, CommandBehavior.SchemaOnly, CommandBehavior.KeyInfo);

            //Need to setup the reader function
            mockExecutor.Setup(x => x.GetDbDataReader(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared, behavior)).Returns(Mock.Of<DbDataReader>()).Verifiable();

            //Make the call
            DbDataReader reader = new DbClient(mockExecutor.Object).GetDbDataReader(realQuery, behavior);

            //Verify the executor was called
            mockExecutor.Verify(x => x.GetDbDataReader(realQuery.QueryText, realQuery.QueryType, new List<DbParameter>(), realQuery.CommandTimeout, realQuery.ShouldBePrepared, behavior), Times.Once);
        }
        #endregion
        #region Helper Methods
        #endregion
    }
}