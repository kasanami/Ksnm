using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units.SI;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class TemperatureTests
    {
        [TestMethod()]
        public void TemperatureTest()
        {
            var celsius = 36.5m * degreeCelsius;
            var kelvin = new Kelvin<decimal>(celsius);
            Assert.AreEqual<decimal>(36.5m + 273.15m, kelvin.Value);
            kelvin.Value += 1;
            celsius = new DegreeCelsius<decimal>(kelvin);
            Assert.AreEqual<decimal>(37.5m, celsius.Value);
        }
    }
}