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
        [Category("DbType")]
        public void MapsObjectCorrectlyy()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Object))) == DbType.Object);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsTimeCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Time))) == DbType.Time);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsStringCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.NormalString))) == DbType.String);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapStringFixedLengthCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.StringFixedLength))) == DbType.StringFixedLength);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsANSIStringCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.AnsiString))) == DbType.AnsiString);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsANSIStringFixedLengthCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.AnsiStrinFixedLength))) == DbType.AnsiStringFixedLength);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsDateTimeCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.NormalDateTime))) == DbType.DateTime);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsDateTimeOffsetCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.DateTimeOffset))) == DbType.DateTimeOffset);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsDateTime2Correctly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.DateTime2))) == DbType.DateTime2);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsBinaryCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.ByteArray))) == DbType.Binary);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsFloatCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Float))) == DbType.Single);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsBoolCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Bool))) == DbType.Boolean);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsByteCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Byte))) == DbType.Byte);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsDecimalCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Decimal))) == DbType.Decimal);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsDoubleCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Double))) == DbType.Double);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsSByteCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.SByte))) == DbType.SByte);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsNativeGuidCorrectly()
        {
            DbParameterFormatter formatter = new DbParameterFormatter(true);

            Assert.That(formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Guid))) == DbType.Guid);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Category("DbType")]
        public void MapsNonNativeGuidCorrectly()
        {
            Assert.That(_formatter.MapDbType(typeof(BasicModel).GetProperty(nameof(BasicModel.Guid))) == DbType.String);
        }
        /// <summary>
        /// Getses the return value direction.
        /// </summary>
        [Test]
        [Category("MapDirection")]
        public void GetsReturnValueDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.ReturnValue));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.ReturnValue);
        }
        /// <summary>
        /// Getses the input direcion.
        /// </summary>
        [Test]
        [Category("MapDirection")]
        public void GetsInputDirecion()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.Input));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.Input);
        }
        /// <summary>
        /// Getses the output direction.
        /// </summary>
        [Test]
        [Category("MapDirection")]
        public void GetsOutputDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.Output));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.Output);
        }
        /// <summary>
        /// Getses the output direction.
        /// </summary>
        [Test]
        [Category("MapDirection")]
        public void GetsInputOutputDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.InputOutput));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.InputOutput);
        }
        [Test]
        [Category("MapDirection")]
        public void GetNoSetDirection()
        {
            PropertyInfo info = typeof(DirectionModel).GetProperty(nameof(DirectionModel.NoDirection));

            Assert.That(_formatter.MapParameterDirection(info) == ParameterDirection.InputOutput);
        }
        #endregion
    }
}