using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;

namespace Ksnm.Units.SI.Tests
{
    [TestClass()]
    public class WattTests
    {
        [TestMethod()]
        public void WattTest()
        {
            // 単位無しで計算
            double expected = 0;
            {
                var mass = 1.0;
                var time = 2.0;
                var length = 3.0;
                var acceleration = 4.0;

                var force = mass * acceleration;
                var energy = force * length;
                var power = energy / time;

                expected = power;
            }
            // 単位付きで計算
            double actual = 0;
            {
                var mass = new Kilogram<Double>(1.0);
                var time = new Second<Double>(2.0);
                var length = new Metre<Double>(3.0);
                var acceleration = new MetrePerSecondSquared<Double>(4);

                var force = mass * acceleration;
                var energy = force * length;
                var power = energy / time;

                actual = power.Value;
            }
            Assert.AreEqual(expected, actual);
        }
    }
}