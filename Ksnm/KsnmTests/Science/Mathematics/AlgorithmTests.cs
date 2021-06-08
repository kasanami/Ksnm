using Ksnm.Science.Mathematics;
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

        [TestMethod()]
        public void PrimeTest()
        {
            // 0番目は使用しない
            var primes = new[]
            {
                1, 2, 3, 5, 7, 11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,
                73,79,83,89,97,101,103,107,109,113,127,131,137,139,149,151,157,163,167,173
            };

            for (int i = 1; i < primes.Length; i++)
            {
                Assert.AreEqual(primes[i], Algorithm.Prime(i), $"{i}");
            }
        }

        [TestMethod()]
        public void PrimeCountTest()
        {
            Assert.AreEqual(0 + 1, Algorithm._PrimeCountPlusOne(1), "1");
            Assert.AreEqual(1 + 1, Algorithm._PrimeCountPlusOne(2), "2");
            Assert.AreEqual(2 + 1, Algorithm._PrimeCountPlusOne(3), "3");
            Assert.AreEqual(2 + 1, Algorithm._PrimeCountPlusOne(4), "4");
            Assert.AreEqual(3 + 1, Algorithm._PrimeCountPlusOne(5), "5");
            Assert.AreEqual(3 + 1, Algorithm._PrimeCountPlusOne(6), "6");
            Assert.AreEqual(4 + 1, Algorithm._PrimeCountPlusOne(7), "7");
            Assert.AreEqual(4 + 1, Algorithm._PrimeCountPlusOne(8), "8");
            Assert.AreEqual(4 + 1, Algorithm._PrimeCountPlusOne(9), "9");

            Assert.AreEqual(4 + 1, Algorithm._PrimeCountPlusOne(10), "10");
            Assert.AreEqual(5 + 1, Algorithm._PrimeCountPlusOne(11), "11");
            Assert.AreEqual(5 + 1, Algorithm._PrimeCountPlusOne(12), "12");
            Assert.AreEqual(6 + 1, Algorithm._PrimeCountPlusOne(13), "13");
            Assert.AreEqual(6 + 1, Algorithm._PrimeCountPlusOne(14), "14");
            Assert.AreEqual(6 + 1, Algorithm._PrimeCountPlusOne(15), "15");
            Assert.AreEqual(6 + 1, Algorithm._PrimeCountPlusOne(16), "16");
            Assert.AreEqual(7 + 1, Algorithm._PrimeCountPlusOne(17), "17");
            Assert.AreEqual(7 + 1, Algorithm._PrimeCountPlusOne(18), "18");
            Assert.AreEqual(8 + 1, Algorithm._PrimeCountPlusOne(19), "19");

            Assert.AreEqual(8 + 1, Algorithm._PrimeCountPlusOne(20), "20");
            Assert.AreEqual(8 + 1, Algorithm._PrimeCountPlusOne(21), "21");
            Assert.AreEqual(8 + 1, Algorithm._PrimeCountPlusOne(22), "22");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(23), "23");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(24), "24");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(25), "25");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(26), "26");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(27), "27");
            Assert.AreEqual(9 + 1, Algorithm._PrimeCountPlusOne(28), "28");
            Assert.AreEqual(10 + 1, Algorithm._PrimeCountPlusOne(29), "29");
        }

        [TestMethod()]
        public void PrimeToOneTest()
        {
            Assert.AreEqual(1, Algorithm._PrimeToOne(1), "1");
            Assert.AreEqual(1, Algorithm._PrimeToOne(2), "2");
            Assert.AreEqual(1, Algorithm._PrimeToOne(3), "3");
            Assert.AreEqual(0, Algorithm._PrimeToOne(4), "4");
            Assert.AreEqual(1, Algorithm._PrimeToOne(5), "5");
            Assert.AreEqual(0, Algorithm._PrimeToOne(6), "6");
            Assert.AreEqual(1, Algorithm._PrimeToOne(7), "7");
            Assert.AreEqual(0, Algorithm._PrimeToOne(8), "8");
            Assert.AreEqual(0, Algorithm._PrimeToOne(9), "9");

            Assert.AreEqual(0, Algorithm._PrimeToOne(10), "10");
            Assert.AreEqual(1, Algorithm._PrimeToOne(11), "11");
            Assert.AreEqual(0, Algorithm._PrimeToOne(12), "12");
            Assert.AreEqual(1, Algorithm._PrimeToOne(13), "13");
            Assert.AreEqual(0, Algorithm._PrimeToOne(14), "14");
            Assert.AreEqual(0, Algorithm._PrimeToOne(15), "15");
            Assert.AreEqual(0, Algorithm._PrimeToOne(16), "16");
            Assert.AreEqual(1, Algorithm._PrimeToOne(17), "17");
            Assert.AreEqual(0, Algorithm._PrimeToOne(18), "18");
            Assert.AreEqual(1, Algorithm._PrimeToOne(19), "19");

            Assert.AreEqual(0, Algorithm._PrimeToOne(20), "20");
            Assert.AreEqual(0, Algorithm._PrimeToOne(21), "21");
            Assert.AreEqual(0, Algorithm._PrimeToOne(22), "22");
            Assert.AreEqual(1, Algorithm._PrimeToOne(23), "23");
            Assert.AreEqual(0, Algorithm._PrimeToOne(24), "24");
            Assert.AreEqual(0, Algorithm._PrimeToOne(25), "25");
            Assert.AreEqual(0, Algorithm._PrimeToOne(26), "26");
            Assert.AreEqual(0, Algorithm._PrimeToOne(27), "27");
            Assert.AreEqual(0, Algorithm._PrimeToOne(28), "28");
            Assert.AreEqual(1, Algorithm._PrimeToOne(29), "29");
        }
    }
}