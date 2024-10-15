using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units;
using Ksnm.Units.SI;
using static Ksnm.Units.Constants<decimal>;
using System.Diagnostics.Metrics;

namespace Ksnm.Units.Tests
{
    using Metre = Metre<decimal>;
    using KiloMetre = KiloMetre<decimal>;
    [TestClass()]
    public class LengthTests
    {
        [TestMethod()]
        public void LengthTest()
        {
            // 1海里は1852メートル
            {
                var length1 = 1 * nauticalMile;
                var length2 = (Metre)length1;
                Assert.AreEqual("1852m", length2.ToString());
            }
            // キロメートル
            {
                var length1 = 1234 * metre;
                var length2 = (KiloMetre)length1;
                Assert.AreEqual("1.234km", length2.ToString());
            }
            // キロメートル
            {
                var length1 = new KiloMetre(1.234m);
                var length2 = (Metre)length1;
                Assert.AreEqual("1234.000m", length2.ToString());
            }
        }
    }
}