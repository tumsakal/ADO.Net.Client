#region Using Statements
using MySql.Data.MySqlClient;
using NUnit.Framework;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    [TestFixture]
    [Category("ConnectionTests")]
    public class ConnectionTests : BaseTests
    {
        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            _factory = new DbObjectFactory(new MySqlConnection());
        }
    }
}
