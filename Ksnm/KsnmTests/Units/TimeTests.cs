using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class TimeTests
    {
        [TestMethod()]
        public void TimeTest()
        {
            {
                var hour = new Hour<Double>(1);
                Assert.AreEqual(1, hour.Value);
                Minute<Double> minute = hour;
                Assert.AreEqual(60, minute.Value);
                Second<Double> second = minute;
                Assert.AreEqual(3600, second.Value);
            }

            {
                var hour = new Hour<Double>(2);
                Assert.AreEqual(2, hour.Value);
                Minute<Double> minute = hour;
                Assert.AreEqual(120, minute.Value);
                Second<Double> second = minute;
                Assert.AreEqual(7200, second.Value);
            }

            {
                var second = new Second<Double>(30);
                Assert.AreEqual<double>(30, second.Value);
                Minute<Double> minute = second;
                Assert.AreEqual<double>(0.5, minute.Value);
                minute.Value = 30;
                Hour<Double> hour = minute;
                Assert.AreEqual<double>(0.5, hour.Value);
            }
        }
    }
}