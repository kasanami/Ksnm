using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Ksnm.ExtensionMethods.System.Collections;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;

namespace Ksnm.Tests
{
    [TestClass()]
    public class MathTests
    {
        [TestMethod()]
        public void IsEvenTest()
        {
            // int
            Assert.AreEqual(Math.IsEven(+2), true);
            Assert.AreEqual(Math.IsEven(+1), false);
            Assert.AreEqual(Math.IsEven(0), true);
            Assert.AreEqual(Math.IsEven(-1), false);
            Assert.AreEqual(Math.IsEven(-2), true);
            // long
            Assert.AreEqual(Math.IsEven(+2L), true);
            Assert.AreEqual(Math.IsEven(+1L), false);
            Assert.AreEqual(Math.IsEven(0L), true);
            Assert.AreEqual(Math.IsEven(-1L), false);
            Assert.AreEqual(Math.IsEven(-2L), true);
            // decimal
            Assert.AreEqual(Math.IsEven(+2m), true);
            Assert.AreEqual(Math.IsEven(+1m), false);
            Assert.AreEqual(Math.IsEven(0m), true);
            Assert.AreEqual(Math.IsEven(-1m), false);
            Assert.AreEqual(Math.IsEven(-2m), true);
        }

        [TestMethod()]
        public void IsOddTest()
        {
            // int
            Assert.AreEqual(Math.IsOdd(+2), false);
            Assert.AreEqual(Math.IsOdd(+1), true);
            Assert.AreEqual(Math.IsOdd(0), false);
            Assert.AreEqual(Math.IsOdd(-1), true);
            Assert.AreEqual(Math.IsOdd(-2), false);
            // long
            Assert.AreEqual(Math.IsOdd(+2L), false);
            Assert.AreEqual(Math.IsOdd(+1L), true);
            Assert.AreEqual(Math.IsOdd(0L), false);
            Assert.AreEqual(Math.IsOdd(-1L), true);
            Assert.AreEqual(Math.IsOdd(-2L), false);
            // decimal
            Assert.AreEqual(Math.IsOdd(+2m), false);
            Assert.AreEqual(Math.IsOdd(+1m), true);
            Assert.AreEqual(Math.IsOdd(0m), false);
            Assert.AreEqual(Math.IsOdd(-1m), true);
            Assert.AreEqual(Math.IsOdd(-2m), false);
        }

        [TestMethod()]
        public void IsPrimeTest()
        {
            int[] numbers = new[]
            {
                -2,
                -1,
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
            };
            bool[] expectes = new[]
            {
                false,//-2,
                false,//-1,
                false,//0,
                false,//1,
                true,//2,
                true,//3,
                false,//4,
                true,//5,
                false,//6,
                true,//7,
                false,//8,
                false,//9,
                false,//10,
                true,//11,
            };
            // int
            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.AreEqual(expectes[i], Math.IsPrime(numbers[i]));
            }
            // uint
            for (int i = 2; i < numbers.Length; i++)
            {
                Assert.AreEqual(expectes[i], Math.IsPrime((uint)numbers[i]));
            }
            // long
            for (int i = 2; i < numbers.Length; i++)
            {
                Assert.AreEqual(expectes[i], Math.IsPrime((long)numbers[i]));
            }
            // ulong
            for (int i = 2; i < numbers.Length; i++)
            {
                Assert.AreEqual(expectes[i], Math.IsPrime((ulong)numbers[i]));
            }
            // decimal
            for (int i = 2; i < numbers.Length; i++)
            {
                Assert.AreEqual(expectes[i], Math.IsPrime((decimal)numbers[i]));
            }
        }

        [TestMethod()]
        public void SignTest()
        {
            Assert.AreEqual(Math.Sign(+2), +1);
            Assert.AreEqual(Math.Sign(+1), +1);
            Assert.AreEqual(Math.Sign(0), 0);
            Assert.AreEqual(Math.Sign(-1), -1);
            Assert.AreEqual(Math.Sign(-2), -1);
        }

        [TestMethod()]
        public void RampTest()
        {
            Assert.AreEqual(Math.Ramp(+2), +2);
            Assert.AreEqual(Math.Ramp(+1), +1);
            Assert.AreEqual(Math.Ramp(0), 0);
            Assert.AreEqual(Math.Ramp(-1), 0);
            Assert.AreEqual(Math.Ramp(-2), 0);
        }

        [TestMethod()]
        public void HeavisideStepTest()
        {
            Assert.AreEqual(Math.HeavisideStep(+2), +1);
            Assert.AreEqual(Math.HeavisideStep(+1), +1);
            Assert.AreEqual(Math.HeavisideStep(0f), 0.5f);
            Assert.AreEqual(Math.HeavisideStep(-1), 0);
            Assert.AreEqual(Math.HeavisideStep(-2), 0);
        }

        [TestMethod()]
        public void SigmoidTest()
        {
            Assert.AreEqual(Math.Sigmoid(0, 1), 0.5f);
        }

        [TestMethod()]
        public void LerpTest()
        {
            Assert.AreEqual(Math.Lerp(0, 1, 0.5f), 0.5f);
            Assert.AreEqual(Math.Lerp(0, 1, 0.5), 0.5);
            Assert.AreEqual(Math.Lerp(0, 1, 0.5m), 0.5m);
        }

        [TestMethod()]
        public void InverseLerpTest()
        {
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5f), 0.5f);
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5), 0.5);
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5m), 0.5m);
        }

        [TestMethod()]
        public void AverageTest()
        {
            Assert.AreEqual(Math.Average(0, 1), 0.5f);
            Assert.AreEqual(Math.Average(0, 1, 2), 1);
        }

        [TestMethod()]
        public void MedianTest()
        {
            Assert.AreEqual(Math.Median(0, 1), 0.5);
            Assert.AreEqual(Math.Median(0, 1, 2), 1);
            Assert.AreEqual(Math.Median(0, 1, 2, 3), 1.5);
        }

        [TestMethod()]
        public void GreatestCommonDivisorTest()
        {
            Assert.AreEqual(Math.GreatestCommonDivisor(30, 42), 6);
            Assert.AreEqual(Math.GreatestCommonDivisor(1071, 1029), 21);
            Assert.AreEqual(Math.GreatestCommonDivisor(-30, 42), 6);
            Assert.AreEqual(Math.GreatestCommonDivisor(-1071, 1029), 21);
        }

        [TestMethod()]
        public void PrimeFactorizationTest()
        {
            var primes = Math.PrimeFactorization(0).ToArray();
            var expected = new int[] { };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(-1).ToArray();
            expected = new int[] { };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(1).ToArray();
            expected = new int[] { };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(2).ToArray();
            expected = new[] { 2 };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(3).ToArray();
            expected = new[] { 3 };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(4).ToArray();
            expected = new[] { 2, 2 };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(6).ToArray();
            expected = new[] { 2, 3 };
            CollectionAssert.AreEqual(expected, primes);

            primes = Math.PrimeFactorization(int.MaxValue).ToArray();
            expected = new[] { int.MaxValue };
            CollectionAssert.AreEqual(expected, primes);

            // 素因数分解したあと、乗算して比較する
            for (int i = 2; i < 100; i++)
            {
                primes = Math.PrimeFactorization(i).ToArray();
                var product = primes.Product();
                Assert.AreEqual(i, product);
            }
        }

        [TestMethod()]
        public void TriangularNumberTest()
        {
            Assert.AreEqual(Math.TriangularNumber(0), 0);
            Assert.AreEqual(Math.TriangularNumber(1), 1);
            Assert.AreEqual(Math.TriangularNumber(2), 3);
            Assert.AreEqual(Math.TriangularNumber(3), 6);
            Assert.AreEqual(Math.TriangularNumber(4), 10);
            Assert.AreEqual(Math.TriangularNumber(5), 15);
            Assert.AreEqual(Math.TriangularNumber(6), 21);
            Assert.AreEqual(Math.TriangularNumber(7), 28);
            Assert.AreEqual(Math.TriangularNumber(8), 36);
            Assert.AreEqual(Math.TriangularNumber(9), 45);
        }

        [TestMethod()]
        public void PolygonalNumberTest()
        {
            Assert.AreEqual(Math.PolygonalNumber(3, 0), 0);
            Assert.AreEqual(Math.PolygonalNumber(3, 1), 1);
            Assert.AreEqual(Math.PolygonalNumber(3, 2), 3);
            Assert.AreEqual(Math.PolygonalNumber(3, 3), 6);
            Assert.AreEqual(Math.PolygonalNumber(3, 4), 10);
            Assert.AreEqual(Math.PolygonalNumber(3, 5), 15);
            Assert.AreEqual(Math.PolygonalNumber(3, 6), 21);
            Assert.AreEqual(Math.PolygonalNumber(3, 7), 28);
            Assert.AreEqual(Math.PolygonalNumber(3, 8), 36);
            Assert.AreEqual(Math.PolygonalNumber(3, 9), 45);

            Assert.AreEqual(Math.PolygonalNumber(4, 0), 0);
            Assert.AreEqual(Math.PolygonalNumber(4, 1), 1);
            Assert.AreEqual(Math.PolygonalNumber(4, 2), 4);
            Assert.AreEqual(Math.PolygonalNumber(4, 3), 9);
            Assert.AreEqual(Math.PolygonalNumber(4, 4), 16);
            Assert.AreEqual(Math.PolygonalNumber(4, 5), 25);
            Assert.AreEqual(Math.PolygonalNumber(4, 6), 36);
            Assert.AreEqual(Math.PolygonalNumber(4, 7), 49);
            Assert.AreEqual(Math.PolygonalNumber(4, 8), 64);
            Assert.AreEqual(Math.PolygonalNumber(4, 9), 81);

            Assert.AreEqual(Math.PolygonalNumber(5, 0), 0);
            Assert.AreEqual(Math.PolygonalNumber(5, 1), 1);
            Assert.AreEqual(Math.PolygonalNumber(5, 2), 5);
            Assert.AreEqual(Math.PolygonalNumber(5, 3), 12);
            Assert.AreEqual(Math.PolygonalNumber(5, 4), 22);
            Assert.AreEqual(Math.PolygonalNumber(5, 5), 35);
            Assert.AreEqual(Math.PolygonalNumber(5, 6), 51);
            Assert.AreEqual(Math.PolygonalNumber(5, 7), 70);
            Assert.AreEqual(Math.PolygonalNumber(5, 8), 92);
            Assert.AreEqual(Math.PolygonalNumber(5, 9), 117);

            Assert.AreEqual(Math.PolygonalNumber(6, 0), 0);
            Assert.AreEqual(Math.PolygonalNumber(6, 1), 1);
            Assert.AreEqual(Math.PolygonalNumber(6, 2), 6);
            Assert.AreEqual(Math.PolygonalNumber(6, 3), 15);
            Assert.AreEqual(Math.PolygonalNumber(6, 4), 28);
            Assert.AreEqual(Math.PolygonalNumber(6, 5), 45);
            Assert.AreEqual(Math.PolygonalNumber(6, 6), 66);
            Assert.AreEqual(Math.PolygonalNumber(6, 7), 91);
            Assert.AreEqual(Math.PolygonalNumber(6, 8), 120);
            Assert.AreEqual(Math.PolygonalNumber(6, 9), 153);
        }

        [TestMethod()]
        public void FibonacciNumberTest()
        {
            Assert.AreEqual(Math.FibonacciNumber(0), 0);
            Assert.AreEqual(Math.FibonacciNumber(1), 1);
            Assert.AreEqual(Math.FibonacciNumber(2), 1);
            Assert.AreEqual(Math.FibonacciNumber(3), 2);
            Assert.AreEqual(Math.FibonacciNumber(4), 3);
            Assert.AreEqual(Math.FibonacciNumber(5), 5);
            Assert.AreEqual(Math.FibonacciNumber(6), 8);
            Assert.AreEqual(Math.FibonacciNumber(7), 13);
            Assert.AreEqual(Math.FibonacciNumber(8), 21);
            Assert.AreEqual(Math.FibonacciNumber(9), 34);
        }

        [TestMethod()]
        public void MosersCircleRegionsTest()
        {
            Assert.AreEqual(Math.MosersCircleRegions(1), 1);
            Assert.AreEqual(Math.MosersCircleRegions(2), 2);
            Assert.AreEqual(Math.MosersCircleRegions(3), 4);
            Assert.AreEqual(Math.MosersCircleRegions(4), 8);
            Assert.AreEqual(Math.MosersCircleRegions(5), 16);
            Assert.AreEqual(Math.MosersCircleRegions(6), 31);
        }
    }
}
