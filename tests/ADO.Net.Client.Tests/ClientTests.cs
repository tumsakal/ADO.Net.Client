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
    public partial class ClientTests
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
        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Mock<ISqlQuery> mockQuery = new Mock<ISqlQuery>();

            mockQuery.Setup(x => x.ShouldBePrepared).Returns(_faker.Random.Bool());
            mockQuery.Setup(x => x.CommandTimeout).Returns(_faker.Random.Int());
            mockQuery.Setup(x => x.QueryText).Returns(_faker.Random.String());
            mockQuery.Setup(x => x.QueryType).Returns(_faker.PickRandomParam(CommandType.StoredProcedure, CommandType.Text, CommandType.TableDirect));
            mockQuery.Setup(x => x.Parameters).Returns(GetDbParameters());

            realQuery = mockQuery.Object;
        }
        #endregion
        #region Helper Methods        
        /// <summary>
        /// Gets the database parameters.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DbParameter> GetDbParameters()
        {
            List<CustomDbParameter> parameters = new List<CustomDbParameter>();
            int number = _faker.Random.Int(1, 10);
            
            //Loop through all numbers
            for(int i = 0; i < number; i++)
            {
                CustomDbParameter param = new CustomDbParameter();

                param.ParameterName = $"Parameter{i}";
                param.Value = _faker.Random.AlphaNumeric(20);

                parameters.Add(param);
            }

            return parameters;
        }
        #endregion
    }
}