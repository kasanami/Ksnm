using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class ForceTests
    {
        [TestMethod()]
        public void ForceTest()
        {
            var newton = 123 * Newton;
            var kilogramForce = (GS.KilogramForce<Ksnm.Numerics.Decimal>)newton;
            var newton2 = (SI.Newton<Ksnm.Numerics.Decimal>)kilogramForce;
            Assert.AreEqual(newton, newton2);
        }
    }
}