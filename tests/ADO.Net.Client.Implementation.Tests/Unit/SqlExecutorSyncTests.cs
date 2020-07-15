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
using Moq;
using NUnit.Framework;
#endregion

namespace ADO.Net.Client.Implementation.Tests.Unit
{
    public partial class SqlExecutorTests
    {
        #region Read Test Methods
        #endregion
        #region Write Test Methods
        [Test]
        public void WhenExecuteNonQuery_IsCalled__ItShouldCallsDbObjectFactory_GetDbCommand()
        {
            //Need to setup the reader function
            _factory.Setup(x => x.GetDbCommand(realQuery.QueryType, realQuery.QueryText, realQuery.Parameters, null, realQuery.CommandTimeout, null)).Returns(Mock.Of<CustomDbCommand>()).Verifiable();

            //Make the call
            int records = new SqlExecutor(_factory.Object, _manager, _mapper).ExecuteNonQuery(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, realQuery.ShouldBePrepared);

            //Verify the executor was called
            _factory.Verify(x => x.GetDbCommand(realQuery.QueryType, realQuery.QueryText, realQuery.Parameters, null, realQuery.CommandTimeout, null), Times.Once);
        }
        #endregion
    }
}