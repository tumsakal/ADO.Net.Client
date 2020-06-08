#region Using Statements
using ADO.Net.Client.Core;
using Moq;
using NUnit.Framework;
#endregion

namespace ADO.Net.Client.Tests
{
    [TestFixture]
    public class ClientTests
    {
        #region Fields/Properties
        #endregion
        #region Setup/Teardown
        public void OneTimeSetup()
        {
        }
        #endregion
        #region Test Methods
        [Test]
        public void WhenGetReaderIsCalled__ItShouldCallSqlExecutorGetReader()
        {
            /* Arrange */
            Mock<ISqlExecutor> mockExecutor = new Mock<ISqlExecutor>();

            // Let's mock the method so when it is called, we handle it
            //iPrinterMock.Setup(x => x.Print(It.IsAny<int>()));

            // Create the calculator and pass the mocked printer to it
             DbClient calculator = new DbClient(mockExecutor.Object);

            /* Act */
            //calculator.Add(1, 1);

            /* Assert */
            // Let's make sure that the calculator's Add method called printer.Print. Here we are making sure it is called once but this is optional
            //iPrinterMock.Verify(x => x.Print(It.IsAny<int>()), Times.Once);

            // Or we can be more specific and ensure that Print was called with the correct parameter.
            //iPrinterMock.Verify(x => x.Print(3), Times.Once);
        }
        #endregion
        #region Helper Methods
        #endregion
    }
}