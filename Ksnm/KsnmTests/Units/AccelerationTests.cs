using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units.GS;
using Ksnm.Units.SI;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class AccelerationTests
    {
        [TestMethod()]
        public void AccelerationTest()
        {
            {
                var g = 1 * standardGravity;
                var g_ms = (MetrePerSecondSquared<decimal>)g;
                Assert.AreEqual("9.80665m/s^2", g_ms.ToString());

                var g2 = (StandardGravity<decimal>)g_ms;
                Assert.AreEqual("1G", g2.ToString());
            }

            {
                // 2s で 0m/s → 3m/s になる加速度
                var acceleration = (3 * metrePerSecond) / (2 * second);
                Assert.AreEqual("1.5m/s^2", acceleration.ToString());
                // ↑加速度が 4s 経つと速度が 6m/s になる
                var velocity = acceleration * (4 * second);
                Assert.AreEqual("6.0m/s", velocity.ToString());
            }
        }
    }
}