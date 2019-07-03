using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.ExtensionMethods.System.Collections.Tests
{
    [TestClass()]
    public class EnumerableTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            var ary = new[] { 1 };
            Assert.AreEqual(1, ary.Product());
            ary = new[] { 1, 2 };
            Assert.AreEqual(2, ary.Product());
            ary = new[] { 1, 2, 3 };
            Assert.AreEqual(6, ary.Product());
        }
    }
}