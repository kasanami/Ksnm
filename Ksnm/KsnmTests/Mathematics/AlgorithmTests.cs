using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Mathematics.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        [TestMethod()]
        public void GaussLegendreTest()
        {
            var pi = Algorithm.GaussLegendre(3);
            Assert.AreEqual(System.Math.PI, pi, 0.00000000000000089);
        }
    }
}
