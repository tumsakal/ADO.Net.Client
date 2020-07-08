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
using ADO.Net.Client.Core.Tests.Models;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class DbParameterFormatterTests
    {
        #region Fields/Properties
        private readonly DbParameterFormatter _formatter;
        #endregion
        #region Constructors
        public DbParameterFormatterTests()
        {
            _formatter = new DbParameterFormatter();
        }
        #endregion
        #region Setup/Teardown
        #endregion
        #region Tests
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MapsTimeCorrectly()
        {
            
        }
        /// <summary>
        /// Getses the return value direction.
        /// </summary>
        [Test]
        public void GetsReturnValueDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.ReturnValue));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.ReturnValue);
        }
        /// <summary>
        /// Getses the input direcion.
        /// </summary>
        [Test]
        public void GetsInputDirecion()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.Input));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.Input);
        }
        /// <summary>
        /// Getses the output direction.
        /// </summary>
        [Test]
        public void GetsOutputDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.Output));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.Output);
        }
        /// <summary>
        /// Getses the output direction.
        /// </summary>
        [Test]
        public void GetsInputOutputDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.InputOutput));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.InputOutput);
        }
        public void GetNoSetDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.NoDirection));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.InputOutput);
        }
        #endregion
    }
}