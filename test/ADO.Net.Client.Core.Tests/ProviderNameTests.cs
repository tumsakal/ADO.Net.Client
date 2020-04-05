#region Using Statements
using NUnit.Framework;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    [TestFixture]
    public class ProviderNameTests : BaseTests
    {
        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            _factory = new DbObjectFactory("MySqlConnector");
        }
    }
}
