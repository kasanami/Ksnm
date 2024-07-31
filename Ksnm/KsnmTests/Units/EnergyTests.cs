using Ksnm.Numerics;
using Ksnm.Units.NonSI;
using Ksnm.Units.SI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class EnergyTests
    {
        [TestMethod()]
        public void EnergyTest()
        {
            // 質量←→エネルギー　変換
            {
                var mass = 123 * kilogram;
                var energy = (Joule<decimal>)mass;
                var mass2 = (Kilogram<decimal>)energy;

                Assert.AreEqual(mass.Value, mass2.Value);
            }

            // ジュール←→カロリー　変換
            {
                var cal = 1 * calorie;
                var joule = (Joule<decimal>)cal;
                Assert.AreEqual("4.184J", joule.ToString());

                var cal2 = (Calorie<decimal>)joule;

                Assert.AreEqual(cal, cal2);
            }
        }
    }
}