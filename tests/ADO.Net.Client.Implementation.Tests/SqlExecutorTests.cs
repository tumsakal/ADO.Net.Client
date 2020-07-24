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
using ADO.Net.Client.Tests.Common;
using Bogus;
using Moq;
using NUnit.Framework;
using System.Data;
#endregion

namespace ADO.Net.Client.Implementation.Tests
{
    [TestFixture]
    public partial class SqlExecutorTests
    {
        #region Fields/Properties
        private ISqlQuery realQuery;
        private IConnectionManager _manager;
        private IDataMapper _mapper;
        private Mock<IDbObjectFactory> _factory;
        private readonly Faker _faker = new Faker();
        private Mock<CustomDbCommand> _command = new Mock<CustomDbCommand>();
        #endregion
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecutorTests"/> class.
        /// </summary>
        public SqlExecutorTests()
        {
        }
        #endregion
        #region Setup/Teardown
        [SetUp]
        public void Setup()
        {
            Mock<ISqlQuery> mockQuery = new Mock<ISqlQuery>();
            _mapper = new Mock<IDataMapper>().Object;
            _manager = new Mock<IConnectionManager>().Object;
            _factory = new Mock<IDbObjectFactory>();
            _command = new Mock<CustomDbCommand>();

            mockQuery.Setup(x => x.CommandTimeout).Returns(_faker.Random.Int());
            mockQuery.Setup(x => x.QueryText).Returns(_faker.Random.String());
            mockQuery.Setup(x => x.QueryType).Returns(_faker.PickRandomParam(CommandType.StoredProcedure, CommandType.Text, CommandType.TableDirect));
            mockQuery.Setup(x => x.ShouldBePrepared).Returns(_faker.Random.Bool());
            //mockQuery.Setup(x => x.Parameters).Returns(null);

            realQuery = mockQuery.Object;

            SetupDbCommand();
        }
        #endregion
        #region Helper Methods
        public void SetupDbCommand()
        {
            //Need to setup the reader function
            _factory.Setup(x => x.GetDbCommand(realQuery.QueryType, realQuery.QueryText, realQuery.Parameters, null, realQuery.CommandTimeout, null)).Returns(_command.Object).Verifiable();
        }
        #endregion
    }
}
