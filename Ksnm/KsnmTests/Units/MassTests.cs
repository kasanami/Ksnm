using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using static Ksnm.Units.Constants<decimal>;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class MassTests
    {
        [TestMethod()]
        public void MassTest()
        {
            var mass1 = 123m * kilogram;
            var mass2 = (SI.Gram<decimal>)mass1;
            var mass3 = (SI.Kilogram<decimal>)mass2;
            Assert.AreEqual(mass1, mass3);
        }
    }
}