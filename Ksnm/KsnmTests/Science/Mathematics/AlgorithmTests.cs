using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Science.Mathematics.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        [TestMethod()]
        public void GaussLegendreTest()
        {
            {
                var pi = Algorithm.GaussLegendre(3);
                Assert.AreEqual(System.Math.PI, pi, 0.00000000000000089);
            }
            {
                var pi = Algorithm.GaussLegendreForBigDecimal(7, 105);
                pi.RoundByMinExponent(-100);
                Assert.AreEqual(
                    "3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170680",
                    pi.ToString());
            }
        }
    }
}
