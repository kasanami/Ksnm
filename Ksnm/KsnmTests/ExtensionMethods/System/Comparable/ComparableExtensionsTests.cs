using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.ExtensionMethods.System.Comparable.Tests
{
    [TestClass()]
    public class ComparableExtensionsTests
    {
        [TestMethod()]
        public void ClampTest()
        {
            int sample = 2;
            Assert.AreEqual(1, sample.Clamp(0, 1));
            Assert.AreEqual(3, sample.Clamp(3, 4));
        }
    }
}
