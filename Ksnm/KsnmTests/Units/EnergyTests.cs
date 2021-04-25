using Ksnm.Numerics;
using Ksnm.Units.NonSI;
using Ksnm.Units.SI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Ksnm.Units.Constants<Ksnm.Numerics.Decimal>;

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
                var mass = 123 * Kilogram;
                var energy = (Joule<Decimal>)mass;
                var mass2 = (Kilogram<Decimal>)energy;

                Assert.AreEqual(mass.Value, mass2.Value);
            }

            // ジュール←→カロリー　変換
            {
                var calorie = 1 * Calorie;
                var joule = (Joule<Decimal>)calorie;
                Assert.AreEqual("4.184J", joule.ToString());

                var calorie2 = (Calorie<Decimal>)joule;

                Assert.AreEqual(calorie.Value, calorie2.Value);
            }
        }
    }
}