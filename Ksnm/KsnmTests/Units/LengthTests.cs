using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units;
using Ksnm.Units.SI;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class LengthTests
    {
        [TestMethod()]
        public void LengthTest()
        {
            // 1海里は1852メートル
            {
                var length1 = 1 * nauticalMile;
                var length2 = (Metre<decimal>)length1;
                Assert.AreEqual("1852m", length2.ToString());
            }
        }
    }
}