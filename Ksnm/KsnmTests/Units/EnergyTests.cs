using Ksnm.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class EnergyTests
    {
        [TestMethod()]
        public void EnergyTest()
        {
            var mass = new SI.Kilogram<BigDecimal>(123);
            var energy = (SI.Joule<BigDecimal>)mass;
            var mass2 = (SI.Kilogram<BigDecimal>)energy;

            Assert.AreEqual(mass.Value, mass2.Value);
        }
    }
}