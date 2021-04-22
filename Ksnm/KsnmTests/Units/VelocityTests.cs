using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.SIUnits<Ksnm.Numerics.Decimal>;

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
            // メートル毎時
            {
                var velocity = 123 * Metre / Hour;
                Assert.AreEqual("123m/h", velocity.ToString());
            }
        }
    }
}