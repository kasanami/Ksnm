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
            var time = new Second<Double>(4);
            var velocity = len / time;
            Assert.AreEqual("3m/s", velocity.ToString());
        }
    }
}