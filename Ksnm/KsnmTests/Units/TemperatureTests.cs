using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units.SI;
using Ksnm.Numerics;
using static Ksnm.Units.SIUnits<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class TemperatureTests
    {
        [TestMethod()]
        public void TemperatureTest()
        {
            var celsius = 36.5m * DegreeCelsius;
            var kelvin = new Kelvin<Decimal>(celsius);
            Assert.AreEqual<decimal>(36.5m + 273.15m, kelvin.Value);
            kelvin.Value += 1;
            celsius = new DegreeCelsius<Decimal>(kelvin);
            Assert.AreEqual<decimal>(37.5m, celsius.Value);
        }
    }
}