using Ksnm.Science.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Science.Mathematics.Tests
{
    [TestClass()]
    public class FormulaTests
    {
        [TestMethod()]
        public void LeibnizTest()
        {
            var pi = Formula.Leibniz(10000000) * 4;
            Assert.AreEqual(System.Math.PI, pi, 0.000001);
        }

        [TestMethod()]
        public void WallisProductTest()
        {
            var pi = Formula.WallisProduct(10000000) * 2;
            Assert.AreEqual(System.Math.PI, pi, 0.000001);
        }
    }
}
