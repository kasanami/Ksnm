using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    using Metre = Metre<decimal>;
    using KiloMetre = KiloMetre<decimal>;
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            {
                var len1 = 3 * metre;
                var len2 = 4 * metre;
                var area = len1 * len2;
                Assert.AreEqual("3m", len1.ToString());
                Assert.AreEqual("4m", len2.ToString());
                Assert.AreEqual("12m^2", area.ToString());
            }

            {
                var len1 = (KiloMetre)3;
                var len2 = (KiloMetre)4;
                var area = len1 * len2;
                Assert.AreEqual("3km", len1.ToString());
                Assert.AreEqual("4km", len2.ToString());
                Assert.AreEqual("12000000m^2", area.ToString());
            }
        }
    }
}