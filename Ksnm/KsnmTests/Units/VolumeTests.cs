﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.SIUnits<Ksnm.Numerics.Decimal>;
using static Ksnm.Units.SIPrefixes;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class VolumeTests
    {
        [TestMethod()]
        public void VolumeTest()
        {
            var cubicMetre = 123 * CubicMetre;
            var litre = (Litre<Decimal>)cubicMetre;
            Assert.AreEqual("123000L", litre.ToString());

            litre = 123 * Litre;
            cubicMetre = (CubicMetre<Decimal>)litre;
            Assert.AreEqual("0.123m^3", cubicMetre.ToString());
        }
    }
}