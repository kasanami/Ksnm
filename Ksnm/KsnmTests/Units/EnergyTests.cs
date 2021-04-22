using Ksnm.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Ksnm.Units.SIUnits<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class EnergyTests
    {
        [TestMethod()]
        public void EnergyTest()
        {
            var mass = 123 * Kilogram;
            var energy = (SI.Joule<Decimal>)mass;
            var mass2 = (SI.Kilogram<Decimal>)energy;

            Assert.AreEqual(mass.Value, mass2.Value);
        }
    }
}