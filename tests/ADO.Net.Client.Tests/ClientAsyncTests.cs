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
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Tests
{
    public partial class ClientTests
    {
        #region Read Test Methods
        #endregion
        #region Write Test Methods
        /// <summary>
        /// Whens the execute non query async is called it should call SQL executor execute non query asynchronous.
        /// </summary>
        [Test]
        [Category("Asynchronous Write Tests")]
        public async Task WhenExecuteNonQueryAsync_IsCalled__ItShouldCallSqlExecutorExecuteNonQueryAsync()
        {
            int delay = _faker.Random.Int(1, 30);
            int returnNumber = _faker.Random.Int(1, 30);

            //Wrap this in a using to automatically dispose of resources
            using (CancellationTokenSource source = new CancellationTokenSource(delay))
            {
                Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

#if !NET45 && !NET461 && !NETCOREAPP2_0 && !NETCOREAPP2_1
                mockExecutor.Setup(x => x.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, realQuery.ShouldBePrepared, source.Token)).ReturnsAsync(returnNumber).Verifiable();
#else
                mockExecutor.Setup(x => x.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, source.Token)).ReturnsAsync(returnNumber).Verifiable();
#endif

                //Make the call
                int records = await new DbClient(mockExecutor.Object).ExecuteNonQueryAsync(realQuery, source.Token);

                Assert.IsTrue(records == returnNumber);

#if !NET45 && !NET461 && !NETCOREAPP2_0 && !NETCOREAPP2_1
                //Verify the executor was called
                mockExecutor.Verify(x => x.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, realQuery.ShouldBePrepared, source.Token), Times.Once);
#else
                //Verify the executor was called
                mockExecutor.Verify(x => x.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, source.Token), Times.Once);
#endif
            }
        }
        #endregion
    }
}