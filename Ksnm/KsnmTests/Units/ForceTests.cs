using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Numerics;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class ForceTests
    {
        [TestMethod()]
        public void ForceTest()
        {
            var newton = new SI.Newton<Decimal>(123);
            var kilogramForce = (GS.KilogramForce<Decimal>)newton;
            var newton2 = (SI.Newton<Decimal>)kilogramForce;
            Assert.AreEqual(newton, newton2);
        }
    }
}