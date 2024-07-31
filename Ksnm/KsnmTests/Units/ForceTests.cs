using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class ForceTests
    {
        [TestMethod()]
        public void ForceTest()
        {
            var force1 = 123 * newton;
            var force2 = (GS.KilogramForce<decimal>)force1;
            var force3 = (SI.Newton<decimal>)force2;
            Assert.AreEqual(force1, force3);
        }
    }
}