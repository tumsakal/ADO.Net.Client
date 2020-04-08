#region Using Statements
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
#endregion

namespace ADO.Net.Client.Core.Tests
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BaseTests" />
    [TestFixture]
    [Category("DataRowTests")]
    public class DataRowTests : BaseTests
    {
        #region Setup/Teardown        
        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
#if !NET472
            DbProviderFactories.RegisterFactory("MySqlConnector", MySqlClientFactory.Instance);
#endif
            //For regular .NET framework the drive must be installed in the Global Assembly Cache
            DataTable table = DbProviderFactories.GetFactoryClasses();
            DataRow row = (from a in table.Rows.Cast<DataRow>()
                           where a.ItemArray[2].ToString() == "MySqlConnector"
                           select a).FirstOrDefault();
            _factory = new DbObjectFactory(row);
        }
#endregion
    }
}
