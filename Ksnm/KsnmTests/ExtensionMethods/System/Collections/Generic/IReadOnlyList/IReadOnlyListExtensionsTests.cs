using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.IReadOnlyList.Tests
{
    [TestClass()]
    public class IReadOnlyListExtensionsTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            var ary = new[] { 0, 1, 2 };
            IReadOnlyList<int> list = ary;
            Assert.AreEqual(0, list.IndexOf(0));
            Assert.AreEqual(1, list.IndexOf(1));
            Assert.AreEqual(2, list.IndexOf(2));
            Assert.AreEqual(-1, list.IndexOf(3));
        }
    }
}
