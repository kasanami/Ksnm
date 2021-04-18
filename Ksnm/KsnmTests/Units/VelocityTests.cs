using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class VelocityTests
    {
        [TestMethod()]
        public void VelocityTest()
        {
            var len = new Metre<Double>(12);
            // 秒速
            {
                var time = new Second<Double>(4);
                var velocity = len / time;
                Assert.AreEqual("3m/s", velocity.ToString());
            }
            // 分速
            {
                var time = new Minute<Double>(4);
                var velocity = len / time;
                Assert.AreEqual("3m/min", velocity.ToString());
            }
            // 時速
            {
                var time = new Hour<Double>(4);
                var velocity = len / time;
                Assert.AreEqual("3m/h", velocity.ToString());
            }
        }
    }
}