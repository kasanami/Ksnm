using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Units;
using Ksnm.Numerics;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class MassTests
    {
        [TestMethod()]
        public void MassTest()
        {
            var kilogram = new SI.Kilogram<Decimal>(123m);
            var gram = (SI.Gram<Decimal>)kilogram;
            var kilogram2 = (SI.Kilogram<Decimal>)gram;
            Assert.AreEqual(kilogram, kilogram2);
        }
    }
}