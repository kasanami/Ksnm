using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Mathematics.Tests
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
    }
}
