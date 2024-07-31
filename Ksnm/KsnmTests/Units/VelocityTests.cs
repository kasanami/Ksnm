using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using Ksnm.Units.NonSI;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    using MetrePerSecond = MetrePerSecond<decimal>;
    using KiloMetrePerHour = KiloMetrePerHour<decimal>;
    using Knot = Knot<decimal>;
    [TestClass()]
    public class VelocityTests
    {
        [TestMethod()]
        public void VelocityTest()
        {
            var len = 12 * metre;
            // 秒速
            {
                var time = 4 * second;
                var velocity = len / time;
                Assert.AreEqual("3m/s", velocity.ToString());
            }
            // 分速
            {
                var time = 4 * minute;
                var velocity = len / time;
                Assert.AreEqual("3m/min", velocity.ToString());
            }
            // 時速
            {
                var time = 4 * hour;
                var velocity = len / time;
                Assert.AreEqual("3m/h", velocity.ToString());
            }
            // キロメートル毎時
            {
                var velocity = 300 * kiloMetrePerHour;
                Assert.AreEqual("300km/h", velocity.ToString());
                var velocity2 = (MetrePerSecond)velocity;
                Assert.AreEqual("83.33333333333333333333333333m/s", velocity2.ToString());
            }
            // メートル毎時
            {
                var velocity = 123 * metre / hour;
                Assert.AreEqual("123m/h", velocity.ToString());
            }
            // ノット
            {
                var velocity = 1 * knot;
                Assert.AreEqual("1kt", velocity.ToString());

                var velocity2 = (MetrePerSecond)velocity;
                Assert.AreEqual("0.5144444444444444444444444444m/s", velocity2.ToString());

                velocity = nauticalMile / hour;
                Assert.AreEqual("1kt", velocity.ToString());

                // 秒速に変換
                velocity2 = (MetrePerSecond)velocity;
                Assert.AreEqual("0.5144444444444444444444444444m/s", velocity2.ToString());

                // 時速に変換
                var velocity3 = (KiloMetrePerHour)velocity;
                Assert.AreEqual("1.852km/h", velocity3.ToString());

                // 再度ノットに変換
                velocity = (Knot)velocity3;
                Assert.AreEqual("1kt", velocity.ToString());

                // 再度ノットに変換 精度の限界で正確に1ktにはならない
                velocity = (Knot)velocity2;
                Assert.AreEqual("0.9999999999999999999999999999kt", velocity.ToString());
            }
        }
    }
}