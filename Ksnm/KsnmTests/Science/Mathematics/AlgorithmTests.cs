using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System.Linq;

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

        [TestMethod()]
        public void SieveOfRratosthenesTest()
        {
            var primes = Algorithm.SieveOfRratosthenes(1);
            Assert.AreEqual(0, primes.Count());

            primes = Algorithm.SieveOfRratosthenes(3);
            Assert.IsTrue(primes.SequenceEqual(2, 3));

            primes = Algorithm.SieveOfRratosthenes(5);
            Assert.IsTrue(primes.SequenceEqual(2, 3, 5));

            primes = Algorithm.SieveOfRratosthenes(7);
            Assert.IsTrue(primes.SequenceEqual(2, 3, 5, 7));

            primes = Algorithm.SieveOfRratosthenes(11);
            Assert.IsTrue(primes.SequenceEqual(2, 3, 5, 7, 11));
        }
    }
}