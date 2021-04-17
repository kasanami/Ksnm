using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            var len1 = new Metre<Double>(3);
            var len2 = new Metre<Double>(4);
            var area = len1 * len2;
            Assert.AreEqual("12m^2", area.ToString());
        }
    }
}