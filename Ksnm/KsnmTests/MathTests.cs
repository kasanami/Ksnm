using Ksnm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System.Numerics;
using Ksnm.Numerics;
using SMath = System.Math;
using System.Collections.Generic;
using System;

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
            // float
            Assert.AreEqual(Math.IsEven(+2f), true);
            Assert.AreEqual(Math.IsEven(+1.5f), false);
            Assert.AreEqual(Math.IsEven(+1f), false);
            Assert.AreEqual(Math.IsEven(+0.5f), false);
            Assert.AreEqual(Math.IsEven(0f), true);
            Assert.AreEqual(Math.IsEven(-0.5f), false);
            Assert.AreEqual(Math.IsEven(-1f), false);
            Assert.AreEqual(Math.IsEven(-1.5f), false);
            Assert.AreEqual(Math.IsEven(-2f), true);
            // double
            Assert.AreEqual(Math.IsEven(+2d), true);
            Assert.AreEqual(Math.IsEven(+1.5d), false);
            Assert.AreEqual(Math.IsEven(+1d), false);
            Assert.AreEqual(Math.IsEven(+0.5d), false);
            Assert.AreEqual(Math.IsEven(0d), true);
            Assert.AreEqual(Math.IsEven(-0.5d), false);
            Assert.AreEqual(Math.IsEven(-1d), false);
            Assert.AreEqual(Math.IsEven(-1.5d), false);
            Assert.AreEqual(Math.IsEven(-2d), true);
            // decimal
            Assert.AreEqual(Math.IsEven(+2m), true);
            Assert.AreEqual(Math.IsEven(+1.5m), false);
            Assert.AreEqual(Math.IsEven(+1m), false);
            Assert.AreEqual(Math.IsEven(+0.5m), false);
            Assert.AreEqual(Math.IsEven(0m), true);
            Assert.AreEqual(Math.IsEven(-0.5m), false);
            Assert.AreEqual(Math.IsEven(-1m), false);
            Assert.AreEqual(Math.IsEven(-1.5m), false);
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
            // float
            Assert.AreEqual(Math.IsOdd(+2f), false);
            Assert.AreEqual(Math.IsOdd(+1.5f), false);
            Assert.AreEqual(Math.IsOdd(+1f), true);
            Assert.AreEqual(Math.IsOdd(+0.5f), false);
            Assert.AreEqual(Math.IsOdd(0f), false);
            Assert.AreEqual(Math.IsOdd(-0.5f), false);
            Assert.AreEqual(Math.IsOdd(-1f), true);
            Assert.AreEqual(Math.IsOdd(-1.5f), false);
            Assert.AreEqual(Math.IsOdd(-2f), false);
            // double
            Assert.AreEqual(Math.IsOdd(+2d), false);
            Assert.AreEqual(Math.IsOdd(+1.5d), false);
            Assert.AreEqual(Math.IsOdd(+1d), true);
            Assert.AreEqual(Math.IsOdd(+0.5d), false);
            Assert.AreEqual(Math.IsOdd(0d), false);
            Assert.AreEqual(Math.IsOdd(-0.5d), false);
            Assert.AreEqual(Math.IsOdd(-1d), true);
            Assert.AreEqual(Math.IsOdd(-1.5d), false);
            Assert.AreEqual(Math.IsOdd(-2d), false);
            // decimal
            Assert.AreEqual(Math.IsOdd(+2m), false);
            Assert.AreEqual(Math.IsOdd(+1.5m), false);
            Assert.AreEqual(Math.IsOdd(+1m), true);
            Assert.AreEqual(Math.IsOdd(+0.5m), false);
            Assert.AreEqual(Math.IsOdd(0m), false);
            Assert.AreEqual(Math.IsOdd(-0.5m), false);
            Assert.AreEqual(Math.IsOdd(-1m), true);
            Assert.AreEqual(Math.IsOdd(-1.5m), false);
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
        public void UnitStepTest()
        {
            Assert.AreEqual(Math.UnitStep(+2), 1);
            Assert.AreEqual(Math.UnitStep(+1), 1);
            Assert.AreEqual(Math.UnitStep(00), 1);
            Assert.AreEqual(Math.UnitStep(-1), 0);
            Assert.AreEqual(Math.UnitStep(-2), 0);
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
            // ゲインあり
            Assert.AreEqual(0.0, Math.Sigmoid(-1.0, 5), 0.01);
            Assert.AreEqual(0.5, Math.Sigmoid(0.0, 5));
            Assert.AreEqual(1.0, Math.Sigmoid(1.0, 5), 0.01);
            // float版のチェック
            for (double i = -1; i < +1; i += 0.25)
            {
                var d = Math.Sigmoid(i, 0.5);
                var f = Math.Sigmoid((float)i, 0.5f);
                Assert.AreEqual(d, (double)f, 0.0000001);
            }

            // ゲインなしの標準版
            Assert.AreEqual(0.0, Math.Sigmoid(-6.0), 0.01);
            Assert.AreEqual(0.5, Math.Sigmoid(0.0));
            Assert.AreEqual(1.0, Math.Sigmoid(+6.0), 0.01);
            // float版のチェック
            for (double i = -1; i < +1; i += 0.25)
            {
                var d = Math.Sigmoid(i);
                var f = Math.Sigmoid((float)i);
                Assert.AreEqual(d, (double)f, 0.0000001);
            }
        }

        [TestMethod()]
        public void DerSigmoidTest()
        {
            Assert.AreEqual(0.00, Math.DerSigmoid(Math.Sigmoid(-10)), 0.0001);
            Assert.AreEqual(0.25, Math.DerSigmoid(Math.Sigmoid(0)), 0.0001);
            Assert.AreEqual(0.00, Math.DerSigmoid(Math.Sigmoid(+10)), 0.0001);
        }

        [TestMethod()]
        public void IdentityTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(i, Math.Identity(i));
            }
        }

        [TestMethod()]
        public void DerIdentityTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(1, Math.DerIdentity(i));
            }
        }

        [TestMethod()]
        public void ReLUTest()
        {
            Assert.AreEqual(0, Math.ReLU(-2));
            Assert.AreEqual(0, Math.ReLU(-1));
            Assert.AreEqual(0, Math.ReLU(0));
            Assert.AreEqual(1, Math.ReLU(+1));
            Assert.AreEqual(2, Math.ReLU(+2));
        }

        [TestMethod()]
        public void DerReLUTest()
        {
            Assert.AreEqual(0, Math.DerReLU(-2));
            Assert.AreEqual(0, Math.DerReLU(-1));
            Assert.AreEqual(0, Math.DerReLU(0));
            Assert.AreEqual(1, Math.DerReLU(+1));
            Assert.AreEqual(1, Math.DerReLU(+2));
        }

        [TestMethod()]
        public void TanhTest()
        {
            Assert.AreEqual(-0.9950547, Math.Tanh(-3), 0.0000001);
            Assert.AreEqual(-0.7615942, Math.Tanh(-1), 0.0000001);
            Assert.AreEqual(+0.0000000, Math.Tanh(+0), 0.0000001);
            Assert.AreEqual(+0.7615942, Math.Tanh(+1), 0.0000001);
            Assert.AreEqual(+0.9950547, Math.Tanh(+3), 0.0000001);
        }

        [TestMethod()]
        public void DerTanhTest()
        {
            Assert.AreEqual(0.0098, Math.DerTanh(-0.9950547), 0.0001);
            Assert.AreEqual(0.4199, Math.DerTanh(-0.7615942), 0.0001);
            Assert.AreEqual(1.0000, Math.DerTanh(+0.0000000), 0.0001);
            Assert.AreEqual(0.4199, Math.DerTanh(+0.7615942), 0.0001);
            Assert.AreEqual(0.0098, Math.DerTanh(+0.9950547), 0.0001);
        }

        [TestMethod()]
        public void LeakyReLUTest()
        {
            Assert.AreEqual(-0.02, Math.LeakyReLU(-2));
            Assert.AreEqual(-0.01, Math.LeakyReLU(-1));
            Assert.AreEqual(0.000, Math.LeakyReLU(+0));
            Assert.AreEqual(+1.00, Math.LeakyReLU(+1));
            Assert.AreEqual(+2.00, Math.LeakyReLU(+2));
        }

        [TestMethod()]
        public void SoftplusTest()
        {
            Assert.AreEqual(2.0611537e-9, Math.Softplus(-20), 0.0000001);
            Assert.AreEqual(3.1326166e-1, Math.Softplus(-01), 0.0000001);
            Assert.AreEqual(6.9314718e-1, Math.Softplus(+00), 0.0000001);
            Assert.AreEqual(1.3132616e+0, Math.Softplus(+01), 0.0000001);
            Assert.AreEqual(2.0000000e+1, Math.Softplus(+20), 0.0000001);
        }

        [TestMethod()]
        public void LerpTest()
        {
            Assert.AreEqual(Math.Lerp(0, 1, 0.5f), 0.5f);
            Assert.AreEqual(Math.Lerp(0, 1, 0.5d), 0.5d);
            Assert.AreEqual(Math.Lerp(0, 1, 0.5m), 0.5m);
            Assert.AreEqual(Math.Lerp(0, 10, 0.5f), 5f);
            Assert.AreEqual(Math.Lerp(0, 10, 0.5d), 5d);
            Assert.AreEqual(Math.Lerp(0, 10, 0.5m), 5m);
        }

        [TestMethod()]
        public void LerpIntegerTest()
        {
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.0f), 0);
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.1f), 1);
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.2f), 2);
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.3f), 3);
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.4f), 4);
            Assert.AreEqual(Math.LerpInteger(0, 10, 0.5f), 5);
        }

        [TestMethod()]
        public void InverseLerpTest()
        {
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5f), 0.5f);
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5d), 0.5d);
            Assert.AreEqual(Math.InverseLerp(0, 1, 0.5m), 0.5m);
            Assert.AreEqual(Math.InverseLerp(0, 10, 5f), 0.5f);
            Assert.AreEqual(Math.InverseLerp(0, 10, 5d), 0.5d);
            Assert.AreEqual(Math.InverseLerp(0, 10, 5m), 0.5m);
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
        public void FactorialTest()
        {
            Assert.AreEqual(1, Math.Factorial(0));
            Assert.AreEqual(1, Math.Factorial(1));
            Assert.AreEqual(2, Math.Factorial(2));
            Assert.AreEqual(6, Math.Factorial(3));
            Assert.AreEqual(24, Math.Factorial(4));
            Assert.AreEqual(479_001_600, Math.Factorial(12));
            Assert.AreEqual(6_227_020_800, Math.Factorial(13));
            Assert.AreEqual(87_178_291_200, Math.Factorial(14));
            Assert.AreEqual(1_307_674_368_000, Math.Factorial(15));
            Assert.AreEqual(6_402_373_705_728_000, Math.Factorial(18));
            Assert.AreEqual(2_432_902_008_176_640_000, Math.Factorial(20));

            Assert.AreEqual(1, Math.Factorial(0.0));
            Assert.AreEqual(1, Math.Factorial(1.0));
            Assert.AreEqual(2, Math.Factorial(2.0));
            Assert.AreEqual(6, Math.Factorial(3.0));
            Assert.AreEqual(24, Math.Factorial(4.0));
            Assert.AreEqual(479_001_600, Math.Factorial(12.0));
            Assert.AreEqual(6_227_020_800, Math.Factorial(13.0));
            Assert.AreEqual(87_178_291_200, Math.Factorial(14.0));
            Assert.AreEqual(1_307_674_368_000, Math.Factorial(15.0));
            Assert.AreEqual(6_402_373_705_728_000, Math.Factorial(18.0));

            Assert.AreEqual(1, Math.Factorial((BigInteger)0));
            Assert.AreEqual(1, Math.Factorial((BigInteger)1));
            Assert.AreEqual(2, Math.Factorial((BigInteger)2));
            Assert.AreEqual(6, Math.Factorial((BigInteger)3));
            Assert.AreEqual(24, Math.Factorial((BigInteger)4));
            Assert.AreEqual(479_001_600, Math.Factorial((BigInteger)12));
            Assert.AreEqual(6_227_020_800, Math.Factorial((BigInteger)13));
            Assert.AreEqual(87_178_291_200, Math.Factorial((BigInteger)14));
            Assert.AreEqual(1_307_674_368_000, Math.Factorial((BigInteger)15));
            Assert.AreEqual(6_402_373_705_728_000, Math.Factorial((BigInteger)18));
            Assert.AreEqual(2_432_902_008_176_640_000, Math.Factorial((BigInteger)20));
        }

        [TestMethod()]
        public void GreatestCommonDivisorTest()
        {
            Assert.AreEqual(Math.GreatestCommonDivisor(30, 42), 6);
            Assert.AreEqual(Math.GreatestCommonDivisor(108, 56), 4);
            Assert.AreEqual(Math.GreatestCommonDivisor(168, 180), 12);
            Assert.AreEqual(Math.GreatestCommonDivisor(1071, 1029), 21);
            Assert.AreEqual(Math.GreatestCommonDivisor(-30, 42), 6);
            Assert.AreEqual(Math.GreatestCommonDivisor(-1071, 1029), 21);
        }

        [TestMethod()]
        public void GreatestCommonDivisorTest2()
        {
            Assert.AreEqual(Math.GreatestCommonDivisor(30u, 42u), 6u);
            Assert.AreEqual(Math.GreatestCommonDivisor(108u, 56u), 4u);
            Assert.AreEqual(Math.GreatestCommonDivisor(168u, 180u), 12u);
            Assert.AreEqual(Math.GreatestCommonDivisor(1071u, 1029u), 21u);
        }

        [TestMethod()]
        public void LeastCommonMultipleTest()
        {
            Assert.AreEqual(1, Math.LeastCommonMultiple(1, 1));
            Assert.AreEqual(2, Math.LeastCommonMultiple(1, 2));
            Assert.AreEqual(6, Math.LeastCommonMultiple(2, 3));
            Assert.AreEqual(18, Math.LeastCommonMultiple(6, 9));
            Assert.AreEqual(72, Math.LeastCommonMultiple(24, 36));

            Assert.AreEqual(1u, Math.LeastCommonMultiple(1u, 1u));
            Assert.AreEqual(2u, Math.LeastCommonMultiple(1u, 2u));
            Assert.AreEqual(6u, Math.LeastCommonMultiple(2u, 3u));
            Assert.AreEqual(18u, Math.LeastCommonMultiple(6u, 9u));
            Assert.AreEqual(72u, Math.LeastCommonMultiple(24u, 36u));
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
            Assert.AreEqual(0, Math.FibonacciNumber(0));
            Assert.AreEqual(1, Math.FibonacciNumber(1));
            Assert.AreEqual(1, Math.FibonacciNumber(2));
            Assert.AreEqual(2, Math.FibonacciNumber(3));
            Assert.AreEqual(3, Math.FibonacciNumber(4));
            Assert.AreEqual(5, Math.FibonacciNumber(5));
            Assert.AreEqual(8, Math.FibonacciNumber(6));
            Assert.AreEqual(13, Math.FibonacciNumber(7));
            Assert.AreEqual(21, Math.FibonacciNumber(8));
            Assert.AreEqual(34, Math.FibonacciNumber(9));
        }

        [TestMethod()]
        public void FibonacciNumbersTest()
        {
            var fibonacciNumbers = Math.FibonacciNumbers(10);
            Assert.AreEqual(0, fibonacciNumbers.ElementAt(0));
            Assert.AreEqual(1, fibonacciNumbers.ElementAt(1));
            Assert.AreEqual(1, fibonacciNumbers.ElementAt(2));
            Assert.AreEqual(2, fibonacciNumbers.ElementAt(3));
            Assert.AreEqual(3, fibonacciNumbers.ElementAt(4));
            Assert.AreEqual(5, fibonacciNumbers.ElementAt(5));
            Assert.AreEqual(8, fibonacciNumbers.ElementAt(6));
            Assert.AreEqual(13, fibonacciNumbers.ElementAt(7));
            Assert.AreEqual(21, fibonacciNumbers.ElementAt(8));
            Assert.AreEqual(34, fibonacciNumbers.ElementAt(9));
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

        [TestMethod()]
        public void PowTest()
        {
            int baseValue = 0;
            Assert.AreEqual(1, Math.Pow(baseValue, 0));
            Assert.AreEqual(0, Math.Pow(baseValue, 1));
            Assert.AreEqual(0, Math.Pow(baseValue, 2));
            Assert.AreEqual(0, Math.Pow(baseValue, 3));
            Assert.AreEqual(0, Math.Pow(baseValue, 4));
            Assert.AreEqual(0, Math.Pow(baseValue, 5));
            baseValue = 10;
            Assert.AreEqual(1, Math.Pow(baseValue, 0));
            Assert.AreEqual(10, Math.Pow(baseValue, 1));
            Assert.AreEqual(100, Math.Pow(baseValue, 2));
            Assert.AreEqual(1000, Math.Pow(baseValue, 3));
            Assert.AreEqual(10000, Math.Pow(baseValue, 4));
            Assert.AreEqual(100000, Math.Pow(baseValue, 5));
            baseValue = 0b10;
            Assert.AreEqual(0b1, Math.Pow(baseValue, 0));
            Assert.AreEqual(0b10, Math.Pow(baseValue, 1));
            Assert.AreEqual(0b100, Math.Pow(baseValue, 2));
            Assert.AreEqual(0b1000, Math.Pow(baseValue, 3));
            Assert.AreEqual(0b10000, Math.Pow(baseValue, 4));
            Assert.AreEqual(0b100000, Math.Pow(baseValue, 5));
            baseValue = 0x10;
            Assert.AreEqual(0x1, Math.Pow(baseValue, 0));
            Assert.AreEqual(0x10, Math.Pow(baseValue, 1));
            Assert.AreEqual(0x100, Math.Pow(baseValue, 2));
            Assert.AreEqual(0x1000, Math.Pow(baseValue, 3));
            Assert.AreEqual(0x10000, Math.Pow(baseValue, 4));
            Assert.AreEqual(0x100000, Math.Pow(baseValue, 5));

            for (int n = -10; n < 10; n++)
            {
                for (int e = -9; e <= 9; e++)
                {
                    Assert.AreEqual((int)System.Math.Pow(n, e), Math.Pow(n, e), $"{n}^{e}");
                }
            }
        }

        [TestMethod()]
        public void PowTest2()
        {
            uint baseValue = 0;
            Assert.AreEqual<uint>(1, Math.Pow(baseValue, 0u));
            Assert.AreEqual<uint>(0, Math.Pow(baseValue, 1u));
            Assert.AreEqual<uint>(0, Math.Pow(baseValue, 2u));
            Assert.AreEqual<uint>(0, Math.Pow(baseValue, 3u));
            Assert.AreEqual<uint>(0, Math.Pow(baseValue, 4u));
            Assert.AreEqual<uint>(0, Math.Pow(baseValue, 5u));
            baseValue = 10;
            Assert.AreEqual<uint>(1, Math.Pow(baseValue, 0u));
            Assert.AreEqual<uint>(10, Math.Pow(baseValue, 1u));
            Assert.AreEqual<uint>(100, Math.Pow(baseValue, 2u));
            Assert.AreEqual<uint>(1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual<uint>(10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual<uint>(100000, Math.Pow(baseValue, 5u));
            baseValue = 0b10;
            Assert.AreEqual<uint>(0b1, Math.Pow(baseValue, 0u));
            Assert.AreEqual<uint>(0b10, Math.Pow(baseValue, 1u));
            Assert.AreEqual<uint>(0b100, Math.Pow(baseValue, 2u));
            Assert.AreEqual<uint>(0b1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual<uint>(0b10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual<uint>(0b100000, Math.Pow(baseValue, 5u));
            baseValue = 0x10;
            Assert.AreEqual<uint>(0x1, Math.Pow(baseValue, 0u));
            Assert.AreEqual<uint>(0x10, Math.Pow(baseValue, 1u));
            Assert.AreEqual<uint>(0x100, Math.Pow(baseValue, 2u));
            Assert.AreEqual<uint>(0x1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual<uint>(0x10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual<uint>(0x100000, Math.Pow(baseValue, 5u));

            for (uint n = 0; n < 10; n++)
            {
                for (uint e = 0; e <= 9; e++)
                {
                    Assert.AreEqual((uint)System.Math.Pow(n, e), Math.Pow(n, e), $"{n}^{e}");
                }
            }
        }

        [TestMethod()]
        public void PowTest_decimal()
        {
            decimal baseValue = 0;
            Assert.AreEqual(1, Math.Pow(baseValue, 0u));
            Assert.AreEqual(0, Math.Pow(baseValue, 1u));
            Assert.AreEqual(0, Math.Pow(baseValue, 2u));
            Assert.AreEqual(0, Math.Pow(baseValue, 3u));
            Assert.AreEqual(0, Math.Pow(baseValue, 4u));
            Assert.AreEqual(0, Math.Pow(baseValue, 5u));
            baseValue = 10;
            Assert.AreEqual(1, Math.Pow(baseValue, 0u));
            Assert.AreEqual(10, Math.Pow(baseValue, 1u));
            Assert.AreEqual(100, Math.Pow(baseValue, 2u));
            Assert.AreEqual(1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual(10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual(100000, Math.Pow(baseValue, 5u));
            baseValue = 0b10;
            Assert.AreEqual(0b1, Math.Pow(baseValue, 0u));
            Assert.AreEqual(0b10, Math.Pow(baseValue, 1u));
            Assert.AreEqual(0b100, Math.Pow(baseValue, 2u));
            Assert.AreEqual(0b1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual(0b10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual(0b100000, Math.Pow(baseValue, 5u));
            baseValue = 0x10;
            Assert.AreEqual(0x1, Math.Pow(baseValue, 0u));
            Assert.AreEqual(0x10, Math.Pow(baseValue, 1u));
            Assert.AreEqual(0x100, Math.Pow(baseValue, 2u));
            Assert.AreEqual(0x1000, Math.Pow(baseValue, 3u));
            Assert.AreEqual(0x10000, Math.Pow(baseValue, 4u));
            Assert.AreEqual(0x100000, Math.Pow(baseValue, 5u));

            for (int n = -10; n <= 10; n++)
            {
                for (int e = 0; e <= 9; e++)
                {
                    Assert.AreEqual((decimal)System.Math.Pow((double)n, e), Math.Pow((decimal)n, e), $"{n}^{e}");
                }
            }
            for (int n = -10; n <= 10; n++)
            {
                for (uint e = 0; e <= 9; e++)
                {
                    Assert.AreEqual((decimal)System.Math.Pow((double)n, e), Math.Pow((decimal)n, e), $"{n}^{e}");
                }
            }
        }

        [TestMethod()]
        public void ExpTest()
        {
            for (int i = 0; i < 5; i++)
            {
                var expected = SMath.Exp(i);
                var actual = (double)Math.Exp(i);
                Assert.AreEqual(expected, actual, 0.000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void BigIntegerPow10Test()
        {
            Assert.AreEqual(1, Math.BigIntegerPow10(0));
            Assert.AreEqual(10, Math.BigIntegerPow10(1));
            Assert.AreEqual(100, Math.BigIntegerPow10(2));
            Assert.AreEqual(1000, Math.BigIntegerPow10(3));
            Assert.AreEqual(10000, Math.BigIntegerPow10(4));
            Assert.AreEqual(100000, Math.BigIntegerPow10(5));
        }

        [TestMethod()]
        public void ContinuedFractionTest()
        {
            // 円周率
            {
                var list = new List<Tuple<double, double>>();
                for (int i = 0; i < 20; i++)
                {
                    var a = i * 2 + 1;
                    var item = new Tuple<double, double>(a * a, 6);
                    list.Add(item);
                }
                var expected = SMath.PI;
                var actual = Math.ContinuedFraction(3, list);
                Assert.AreEqual(expected, actual, 0.001);
            }
            // 円周率
            {
                var list = new List<Tuple<BigDecimal, BigDecimal>>();
                for (int i = 0; i < 40; i++)
                {
                    var a = i * 2 + 1;
                    var item = new Tuple<BigDecimal, BigDecimal>(a * a, 6);
                    list.Add(item);
                }
                var expected = BigDecimal.PI_100;
                var actual = Math.ContinuedFraction<BigDecimal>(3, list);
                expected = BigDecimal.Round(expected, 4, MidpointRounding.AwayFromZero);
                actual = BigDecimal.Round(actual, 4, MidpointRounding.AwayFromZero);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void RegularContinuedFractionTest()
        {
            // 黄金数
            {
                var expected = Math.GoldenNumber;
                var actual = Math.RegularContinuedFraction(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                Assert.AreEqual(expected, actual, 0.00000001);
            }
            // √2
            {
                var expected = SMath.Sqrt(2);
                var actual = Math.RegularContinuedFraction(1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);
                Assert.AreEqual(expected, actual, 0.0000000001);
            }
            // √3
            {
                var expected = SMath.Sqrt(3);
                var actual = Math.RegularContinuedFraction(1, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2);
                Assert.AreEqual(expected, actual, 0.0000000001);
            }

            // √2
            {
                var expected = BigDecimal.Sqrt(2);
                var actual = Math.RegularContinuedFraction<BigDecimal>(1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);

                expected = BigDecimal.Round(expected, 10, MidpointRounding.AwayFromZero);
                actual = BigDecimal.Round(actual, 10, MidpointRounding.AwayFromZero);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void MetallicNumberTest()
        {
            // 0
            {
                var expected = 1;
                var actual = Math.MetallicNumber(0);
                Assert.AreEqual(expected, actual, 0.00000001);
            }
            // 黄金数
            {
                var expected = 1.6180339887;
                var actual = Math.MetallicNumber(1);
                Assert.AreEqual(expected, actual, 0.00000001);
            }
            // 白銀数
            {
                var expected = 2.4142135623;
                var actual = Math.MetallicNumber(2);
                Assert.AreEqual(expected, actual, 0.00000001);
            }
            // 青銅数
            {
                var expected = 3.3027756377;
                var actual = Math.MetallicNumber(3);
                Assert.AreEqual(expected, actual, 0.00000001);
            }

            // 黄金数
            {
                var expected = BigDecimal.GoldenNumber_100;
                var actual = Math.MetallicNumber(BigDecimal.One);

                expected = BigDecimal.Round(expected, 20, MidpointRounding.AwayFromZero);
                actual = BigDecimal.Round(actual, 20, MidpointRounding.AwayFromZero);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SqrtTest()
        {
            // √2
            {
                var expected = 1.414213562373095048801688724m;
                var actual = Math.Sqrt(2m);
                Assert.AreEqual(expected, actual);
            }
            // √3
            {
                var expected = 1.732050807568877293527446342m;
                var actual = Math.Sqrt(3m);
                Assert.AreEqual(expected, actual);
            }
            // √4
            {
                var expected = 2m;
                var actual = Math.Sqrt(4m);
                Assert.AreEqual(expected, actual);
            }
            // √5
            {
                var expected = 2.236067977499789696409173669m;
                var actual = Math.Sqrt(5m);
                Assert.AreEqual(expected, actual);
            }
            // 2乗後に平方根
            for (decimal i = 0; i < 100; i++)
            {
                var value = i * i;
                var expected = i;
                var actual = Math.Sqrt(value);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
        }

        [TestMethod()]
        public void SinTest()
        {
            for (int i = -10; i <= 10; i++)
            {
                var expected = SMath.Sin(i);
                var actual = (double)Math.Sin(i);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void CosTest()
        {
            for (int i = -10; i <= 10; i++)
            {
                var expected = SMath.Cos(i);
                var actual = (double)Math.Cos(i);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void TanTest()
        {
            for (int i = -10; i <= 10; i++)
            {
                var expected = SMath.Tan(i);
                var actual = (double)Math.Tan(i);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AsinTest()
        {
            {
                var expected = Math.PI_Decimal / 2;
                var actual = Math.Asin(1m);
                Assert.AreEqual(expected, actual);
            }
            {
                var expected = -Math.PI_Decimal / 2;
                var actual = Math.Asin(-1m);
                Assert.AreEqual(expected, actual);
            }
            for (decimal i = -1; i <= 1; i += 0.1m)
            {
                var expected = SMath.Asin((double)i);
                var actual = (double)Math.Asin(i);
                Assert.AreEqual(expected, actual, 0.0001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AcosTest()
        {
            for (decimal i = -1; i <= 1; i += 0.1m)
            {
                var expected = SMath.Acos((double)i);
                var actual = (double)Math.Acos(i);
                Assert.AreEqual(expected, actual, 0.0001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AtanTest()
        {
            for (decimal i = -2; i <= 2; i += 0.1m)
            {
                var expected = SMath.Atan((double)i);
                var actual = (double)Math.Atan(i);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void Atan2Test()
        {
            {
                decimal x = 0;
                decimal y = 0;
                var expected = SMath.Atan2((double)y, (double)x);
                var actual = (double)Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"y={y},x={x}");
            }
            {
                decimal x = -0.3m;
                decimal y = -1;
                var expected = SMath.Atan2((double)y, (double)x);
                var expected2 = SMath.Atan((double)y / (double)x);
                var actual = (double)Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"y={y},x={x}");
            }
            {
                decimal x = 0.9m;
                decimal y = 1;
                var expected = SMath.Atan2((double)y, (double)x);
                var expected2 = SMath.Atan((double)y / (double)x);
                var actual = (double)Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"y={y},x={x}");
            }
            for (decimal x = -1; x <= 1; x += 0.1m)
            {
                for (decimal y = -1; y <= 1; y += 0.1m)
                {
                    if (y == 0 || x == 0)
                    {
                        continue;
                    }
                    var expected = SMath.Atan2((double)y, (double)x);
                    var actual = (double)Math.Atan2(y, x);
                    Assert.AreEqual(expected, actual, 0.000000000000001, $"y={y},x={x}");
                }
            }
        }
    }
}