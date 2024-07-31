using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<Ksnm.Numerics.Decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class MassTests
    {
        [TestMethod()]
        public void MassTest()
        {
            var kilogram = 123m * Kilogram;
            var gram = (SI.Gram<Decimal>)kilogram;
            var kilogram2 = (SI.Kilogram<Decimal>)gram;
            Assert.AreEqual(kilogram, kilogram2);
        }
    }
}