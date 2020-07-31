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
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace ADO.Net.Client.Implementation.Tests
{
    public partial class SqlExecutorTests
    {
        #region Read Test Methods
        /// <summary>
        /// When the get scalar value is called it should calls database object factory get database command.
        /// </summary>
        [Test]
        public async Task WhenGetScalarValueAsync_IsCalled__ItShouldCallsDbObjectFactory_GetDbCommand()
        {
            string expected = _faker.Random.AlphaNumeric(30);
            int delay = _faker.Random.Int(0, 1000);

            //Wrap this in a using statement to automatically dispose of resources
            using (CancellationTokenSource source = new CancellationTokenSource(delay))
            {
                _command.Setup(x => x.ExecuteScalarAsync(source.Token)).ReturnsAsync(expected);

#if !NET45 && !NET461 && !NETCOREAPP2_1
                string returned = await _executor.GetScalarValueAsync<string>(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, realQuery.ShouldBePrepared, source.Token);
#else
                string returned = await _executor.GetScalarValueAsync<string>(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, source.Token);
#endif

#if !NET45 && !NET461 && !NETCOREAPP2_1
                if (realQuery.ShouldBePrepared == true)
                {
                    _command.Verify(x => x.PrepareAsync(source.Token), Times.Once);
                }
                else
                {
                    _command.Verify(x => x.PrepareAsync(source.Token), Times.Never);
                }
#endif

                Assert.IsTrue(expected == returned);

                //Verify the calls were made
                _factory.Verify(x => x.GetDbCommand(realQuery.QueryType, realQuery.QueryText, realQuery.Parameters, _manager.Connection, realQuery.CommandTimeout, null), Times.Once);
                _command.Verify(x => x.ExecuteScalarAsync(source.Token), Times.Once);
            }
        }
        #endregion
        #region Write Test Methods                
        /// <summary>
        /// Whens the execute non query is called it should calls database object factory get database command.
        /// </summary>
        [Test]
        public async Task WhenExecuteNonQueryAsync_IsCalled__ItShouldCallsDbObjectFactory_GetDbCommand()
        {
            int expected = _faker.Random.Int();
            int delay = _faker.Random.Int(0, 1000);

            //Wrap this in a using statement to automatically dispose of resources
            using (CancellationTokenSource source = new CancellationTokenSource(delay))
            {
                _command.Setup(x => x.ExecuteNonQueryAsync(source.Token)).ReturnsAsync(expected);

#if !NET45 && !NET461 && !NETCOREAPP2_1
                //Make the call
                int returned = await _executor.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, realQuery.ShouldBePrepared, source.Token);
#else
                int returned = await _executor.ExecuteNonQueryAsync(realQuery.QueryText, realQuery.QueryType, realQuery.Parameters, realQuery.CommandTimeout, source.Token);
#endif

#if !NET45 && !NET461 && !NETCOREAPP2_1
                if (realQuery.ShouldBePrepared == true)
                {
                    _command.Verify(x => x.PrepareAsync(source.Token), Times.Once);
                }
                else
                {
                    _command.Verify(x => x.PrepareAsync(source.Token), Times.Never);
                }
#endif

                Assert.IsTrue(expected == returned);

                //Verify the calls were made
                _factory.Verify(x => x.GetDbCommand(realQuery.QueryType, realQuery.QueryText, realQuery.Parameters, _manager.Connection, realQuery.CommandTimeout, null), Times.Once);
                _command.Verify(x => x.ExecuteNonQueryAsync(source.Token), Times.Once);
            }
        }
        #endregion
    }
}