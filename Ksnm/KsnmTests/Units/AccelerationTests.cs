using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units.GS;
using Ksnm.Units.SI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class AccelerationTests
    {
        [TestMethod()]
        public void AccelerationTest()
        {
            var g = 1 * StandardGravity;
            var g_ms = (MetrePerSecondSquared<Decimal>)g;
            Assert.AreEqual("9.80665m/s^2", g_ms.ToString());

            var g2 = (StandardGravity<Decimal>)g_ms;
            Assert.AreEqual("1G", g2.ToString());
        }
    }
}