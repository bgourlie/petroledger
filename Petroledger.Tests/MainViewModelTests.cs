using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroledger.ViewModels;

namespace Petroledger.Tests
{
    [TestClass]
    public class MainViewModelTests : SilverlightTest
    {
        [TestMethod]
        public void TestConstruction()
        {
            var mainViewModel = new MainViewModel();
            Assert.IsNotNull(mainViewModel.VehiclePages);
        }
    }
}