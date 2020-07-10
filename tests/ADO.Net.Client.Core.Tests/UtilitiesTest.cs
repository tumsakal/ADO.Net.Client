#region Using Statements
using ADO.Net.Client.Core.Tests.Models;
using Castle.Core.Internal;
using NUnit.Framework;
using System.Reflection;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UtilitiesTest
    {
        #region Tests        
        /// <summary>
        /// Cannots the name of the get property by.
        /// </summary>
        [Test]
        public void CannotGetPropertyByName()
        {
            PropertyInfo[] properties = typeof(DbTypeModel).GetProperties();
            PropertyInfo property = properties.GetProperty("SomePropertyName");

            Assert.IsNull(property);
        }
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        [Test]
        public void GetPropertyByName()
        {
            DbTypeModel model = new DbTypeModel();
            PropertyInfo[] properties = typeof(DbTypeModel).GetProperties();

            PropertyInfo property = properties.GetProperty(nameof(model.NormalDateTime));

            Assert.IsNotNull(property);
            Assert.That(property.Name == nameof(model.NormalDateTime));
        }
        #endregion
    }
}