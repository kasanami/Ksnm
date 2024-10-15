using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;

namespace Ksnm.Units.Tests
{
    using Second = Second<decimal>;
    using Minute = Minute<decimal>;
    using Hour = Hour<decimal>;
    [TestClass()]
    public class TimeTests
    {
        [TestMethod()]
        public void TimeTest()
        {
            {
                var hour = new Hour(1);
                Assert.AreEqual(1, hour.Value);
                Minute minute = (Minute)hour;
                Assert.AreEqual(60, minute.Value);
                Second second = (Second)minute;
                Assert.AreEqual(3600, second.Value);
            }

            {
                var hour = new Hour(2);
                Assert.AreEqual(2, hour.Value);
                Minute minute = (Minute)hour;
                Assert.AreEqual(120, minute.Value);
                Second second = (Second)minute;
                Assert.AreEqual(7200, second.Value);
            }

            {
                var second = new Second(30);
                Assert.AreEqual(30, second.Value);
                Minute minute = (Minute)second;
                Assert.AreEqual(0.5m, minute.Value);
                minute.Value = 30;
                Hour hour = (Hour)minute;
                Assert.AreEqual(0.5m, hour.Value);
            }
        }
    }
}