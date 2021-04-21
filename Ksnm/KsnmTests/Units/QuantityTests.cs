using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Numerics;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class QuantityTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            var len1 = new SI.Metre<Decimal>(3);
            var len2 = new SI.Metre<Decimal>(4);
            var area = len1 * len2;
            Assert.AreEqual("3m", len1.ToString());
            Assert.AreEqual("4m", len2.ToString());
            Assert.AreEqual("12m^2", area.ToString());
        }
    }
}