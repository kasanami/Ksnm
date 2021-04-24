using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using Ksnm.Units.NonSI;
using static Ksnm.Units.SIUnits<Ksnm.Numerics.Decimal>;
using static Ksnm.Units.NonSIUnits<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class VelocityTests
    {
        [TestMethod()]
        public void VelocityTest()
        {
            var len = 12 * Metre;
            // 秒速
            {
                var time = 4 * Second;
                var velocity = len / time;
                Assert.AreEqual("3m/s", velocity.ToString());
            }
            // 分速
            {
                var time = 4 * Minute;
                var velocity = len / time;
                Assert.AreEqual("3m/min", velocity.ToString());
            }
            // 時速
            {
                var time = 4 * Hour;
                var velocity = len / time;
                Assert.AreEqual("3m/h", velocity.ToString());
            }
            // キロメートル毎時
            {
                var velocity = 300 * KiloMetrePerHour;
                Assert.AreEqual("300km/h", velocity.ToString());
                var velocity2 = (MetrePerSecond<Decimal>)velocity;
                Assert.AreEqual("83.33333333333333333333333333m/s", velocity2.ToString());
            }
            // メートル毎時
            {
                var velocity = 123 * Metre / Hour;
                Assert.AreEqual("123m/h", velocity.ToString());
            }
            // ノット
            {
                var velocity = 1 * Knot;
                Assert.AreEqual("1kt", velocity.ToString());

                var velocity2 = (MetrePerSecond<Decimal>)velocity;
                Assert.AreEqual("0.5144444444444444444444444444m/s", velocity2.ToString());

                velocity = NauticalMile / Hour;
                Assert.AreEqual("1kt", velocity.ToString());

                velocity2 = (MetrePerSecond<Decimal>)velocity;
                Assert.AreEqual("0.5144444444444444444444444444m/s", velocity2.ToString());
            }
        }
    }
}