using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            var len1 = 3 * metre;
            var len2 = 4 * metre;
            var area = len1 * len2;
            Assert.AreEqual("3m", len1.ToString());
            Assert.AreEqual("4m", len2.ToString());
            Assert.AreEqual("12m^2", area.ToString());
        }
    }
}