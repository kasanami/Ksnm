using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Units.SI;
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class TemperatureTests
    {
        [TestMethod()]
        public void TemperatureTest()
        {
            var celsius = new DegreeCelsius<BigDecimal>(36.5m);
            var kelvin = new Kelvin<BigDecimal>(celsius);
            Assert.AreEqual(36.5m + 273.15m, kelvin.Value);
            kelvin.Value += 1;
            celsius = new DegreeCelsius<BigDecimal>(kelvin);
            Assert.AreEqual(37.5m, celsius.Value);
        }
    }
}