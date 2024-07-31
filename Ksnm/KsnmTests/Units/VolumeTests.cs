using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.Constants<decimal>;
using static Ksnm.Units.SIPrefixes;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class VolumeTests
    {
        [TestMethod()]
        public void VolumeTest()
        {
            // 立方メートル→リットル
            var volume1 = 123 * cubicMetre;
            var volume2 = (Litre<decimal>)volume1;
            Assert.AreEqual("123000L", volume2.ToString());

            // リットル→立方メートル
            var volume3 = 123 * litre;
            var volume4 = (CubicMetre<decimal>)volume3;
            Assert.AreEqual("0.123m^3", volume4.ToString());
        }
    }
}