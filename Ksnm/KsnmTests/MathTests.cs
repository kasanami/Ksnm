using Ksnm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System.Numerics;
using Ksnm.Numerics;
using SMath = System.Math;
using System.Collections.Generic;
using System;
using Ksnm.ExtensionMethods.System.Double;
using Ksnm.Science.Mathematics;

namespace Ksnm.Tests
{
    [TestClass()]
    public class MathTests
    {
        /// <summary>
        /// 許容値
        /// AreEqualのdelta
        /// </summary>
        double Tolerance = 0.00000_00001;
        /// <summary>
        /// 小数点以下100桁の円周率の文字列
        /// </summary>
        static readonly string E100 = "2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274";
        /// <summary>
        /// 小数点以下100桁の円周率の文字列
        /// </summary>
        static readonly string Pi100 = "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117068";

        [TestMethod()]
        public void CalculateETest()
        {
            // double
            Assert.AreEqual(double.E, Math.CalculateE<double>(), 0.000000000000001);
            // decimal
            Assert.AreEqual(2.71828182845904523536028747135m, Math.CalculateE(Math.DecimalEpsilon, 27));
            // BigDecimal
            {
                BigDecimal.DefaultMinExponent = -105;// 四捨五入のため調整
                var tolerance = new BigDecimal(1, -100);
                var e = Math.CalculateE<BigDecimal>(tolerance);
                e = BigDecimal.Round(e, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(E100, e.ToString());
            }
        }
        [TestMethod()]
        public void CalculatePiTest()
        {
            // double
            Assert.AreEqual(double.Pi, Math.CalculatePi<double>(), 0.000000000000001);
            // decimal
            Assert.AreEqual(Math.DecimalPi, Math.CalculatePi<decimal>(Math.DecimalEpsilon, 30), 0.00000000000000000000000001m);
            // BigDecimal
            {
                BigDecimal.DefaultMinExponent = -105;// 四捨五入のため調整
                var tolerance = new BigDecimal(1, -100);
                var e = Math.CalculatePi<BigDecimal>(tolerance);
                e = BigDecimal.Round(e, 100, System.MidpointRounding.AwayFromZero);
                Assert.AreEqual(Pi100, e.ToString());
            }
        }

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

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.Ramp(x);
                var actual = Math.Ramp<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void UnitStepTest()
        {
            Assert.AreEqual(Math.UnitStep(+2), 1);
            Assert.AreEqual(Math.UnitStep(+1), 1);
            Assert.AreEqual(Math.UnitStep(00), 1);
            Assert.AreEqual(Math.UnitStep(-1), 0);
            Assert.AreEqual(Math.UnitStep(-2), 0);

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.UnitStep(x);
                var actual = Math.UnitStep<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void HeavisideStepTest()
        {
            Assert.AreEqual(Math.HeavisideStep(+2), +1);
            Assert.AreEqual(Math.HeavisideStep(+1), +1);
            Assert.AreEqual(Math.HeavisideStep(0f), 0.5f);
            Assert.AreEqual(Math.HeavisideStep(0f, 0.1f), 0.1f);
            Assert.AreEqual(Math.HeavisideStep(-1), 0);
            Assert.AreEqual(Math.HeavisideStep(-2), 0);

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.HeavisideStep(x);
                var actual = Math.HeavisideStep<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
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
                var f = Math.Sigmoid((float)i, 0.5f, double.Epsilon);
                Assert.AreEqual(d, (double)f, 0.0000001);
            }
        }

        [TestMethod()]
        public void StandardSigmoidTest()
        {
            // ゲインなしの標準版
            Assert.AreEqual(0.0, Math.StandardSigmoid(-6.0), 0.01);
            Assert.AreEqual(0.5, Math.StandardSigmoid(0.0));
            Assert.AreEqual(1.0, Math.StandardSigmoid(+6.0), 0.01);
            // float版のチェック
            for (double i = -1; i < +1; i += 0.25)
            {
                var d = Math.StandardSigmoid(i);
                var f = Math.StandardSigmoid((float)i);
                Assert.AreEqual(d, (double)f, 0.0000001);
            }

            Assert.AreEqual(0.88, Math.StandardSigmoid(+2), 0.01);
            Assert.AreEqual(0.73, Math.StandardSigmoid(+1), 0.01);
            Assert.AreEqual(0.5, Math.StandardSigmoid(0), 0.01);
            Assert.AreEqual(0.26, Math.StandardSigmoid(-1), 0.01);
            Assert.AreEqual(0.11, Math.StandardSigmoid(-2), 0.01);

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.StandardSigmoid(x);
                var actual = Math.StandardSigmoid<double>(x, double.Epsilon);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void DerSigmoidTest()
        {
            Assert.AreEqual(0.00, Math.DerStandardSigmoid(Math.StandardSigmoid(-10.0)), 0.0001);
            Assert.AreEqual(0.25, Math.DerStandardSigmoid(Math.StandardSigmoid(0.0)), 0.0001);
            Assert.AreEqual(0.00, Math.DerStandardSigmoid(Math.StandardSigmoid(+10.0)), 0.0001);
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

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.ReLU(x);
                var actual = Math.ReLU<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void DerReLUTest()
        {
            Assert.AreEqual(0, Math.DerReLU(-2));
            Assert.AreEqual(0, Math.DerReLU(-1));
            Assert.AreEqual(0, Math.DerReLU(0));
            Assert.AreEqual(1, Math.DerReLU(+1));
            Assert.AreEqual(1, Math.DerReLU(+2));

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.DerReLU(x);
                var actual = Math.DerReLU<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void TanhTest()
        {
            // Half
            {
                Assert.AreEqual(-0.9950547, (double)Math.Tanh((Half)(-3.0)), 0.001);
                Assert.AreEqual(-0.7615942, (double)Math.Tanh((Half)(-1.0)), 0.001);
                Assert.AreEqual(+0.0000000, (double)Math.Tanh((Half)(+0.0)), 0.001);
                Assert.AreEqual(+0.7615942, (double)Math.Tanh((Half)(+1.0)), 0.001);
                Assert.AreEqual(+0.9950547, (double)Math.Tanh((Half)(+3.0)), 0.001);
            }
            // float
            {
                Assert.AreEqual(-0.9950547, Math.Tanh(-3.0f), 0.0000001);
                Assert.AreEqual(-0.7615942, Math.Tanh(-1.0f), 0.0000001);
                Assert.AreEqual(+0.0000000, Math.Tanh(+0.0f), 0.0000001);
                Assert.AreEqual(+0.7615942, Math.Tanh(+1.0f), 0.0000001);
                Assert.AreEqual(+0.9950547, Math.Tanh(+3.0f), 0.0000001);
            }
            // double
            {
                Assert.AreEqual(-0.9950547, Math.Tanh(-3.0), 0.0000001);
                Assert.AreEqual(-0.7615942, Math.Tanh(-1.0), 0.0000001);
                Assert.AreEqual(+0.0000000, Math.Tanh(+0.0), 0.0000001);
                Assert.AreEqual(+0.7615942, Math.Tanh(+1.0), 0.0000001);
                Assert.AreEqual(+0.9950547, Math.Tanh(+3.0), 0.0000001);
            }
            // decimal
            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.Tanh(x);
                var actual = (double)Math.Tanh<decimal>((decimal)x, (decimal)Tolerance);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void DerTanhTest()
        {
            Assert.AreEqual(0.0098, Math.DerTanh(-0.9950547), 0.0001);
            Assert.AreEqual(0.4199, Math.DerTanh(-0.7615942), 0.0001);
            Assert.AreEqual(1.0000, Math.DerTanh(+0.0000000), 0.0001);
            Assert.AreEqual(0.4199, Math.DerTanh(+0.7615942), 0.0001);
            Assert.AreEqual(0.0098, Math.DerTanh(+0.9950547), 0.0001);

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.DerTanh(x);
                var actual = Math.DerTanh<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void LeakyReLUTest()
        {
            Assert.AreEqual(-0.02, Math.LeakyReLU(-2.0));
            Assert.AreEqual(-0.01, Math.LeakyReLU(-1.0));
            Assert.AreEqual(0.000, Math.LeakyReLU(+0.0));
            Assert.AreEqual(+1.00, Math.LeakyReLU(+1.0));
            Assert.AreEqual(+2.00, Math.LeakyReLU(+2.0));

            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.LeakyReLU(x);
                var actual = Math.LeakyReLU<double>(x);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
        }

        [TestMethod()]
        public void SoftplusTest()
        {
            Assert.AreEqual(2.0611537e-9, Math.Softplus(-20.0), 0.000001);
            Assert.AreEqual(3.1326166e-1, Math.Softplus(-01.0), 0.000001);
            Assert.AreEqual(6.9314718e-1, Math.Softplus(+00.0), 0.000001);
            Assert.AreEqual(1.3132616e+0, Math.Softplus(+01.0), 0.000001);
            Assert.AreEqual(2.0000000e+1, Math.Softplus(+20.0), 0.000001);

            //for (double x = -10; x <= 10; x += 0.25)
            //{
            //    var expected = Math.Softplus(x);
            //    var actual = Math.Softplus<double>(x, Tolerance);
            //    Assert.AreEqual(expected, (double)actual, 0.0001, $"{nameof(x)}={x}");
            //}
        }

        [TestMethod()]
        public void DerSoftplusTest()
        {
            for (double x = -10; x <= 10; x += 0.25)
            {
                var expected = Math.DerSoftplus(x);
                var actual = Math.DerSoftplus<double>(x, double.Epsilon);
                Assert.AreEqual(expected, actual, Tolerance, $"{nameof(x)}={x}");
            }
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
            Assert.AreEqual(120, Math.Factorial(5));
            Assert.AreEqual(479_001_600, Math.Factorial(12));

            Assert.AreEqual(1, Math.Factorial<long>(0));
            Assert.AreEqual(1, Math.Factorial<long>(1));
            Assert.AreEqual(2, Math.Factorial<long>(2));
            Assert.AreEqual(6, Math.Factorial<long>(3));
            Assert.AreEqual(24, Math.Factorial<long>(4));
            Assert.AreEqual(120, Math.Factorial<long>(5));
            Assert.AreEqual(479_001_600, Math.Factorial<long>(12));
            Assert.AreEqual(6_227_020_800, Math.Factorial<long>(13));
            Assert.AreEqual(87_178_291_200, Math.Factorial<long>(14));
            Assert.AreEqual(1_307_674_368_000, Math.Factorial<long>(15));
            Assert.AreEqual(6_402_373_705_728_000, Math.Factorial<long>(18));

            Assert.AreEqual(1, Math.Factorial((BigInteger)0));
            Assert.AreEqual(1, Math.Factorial((BigInteger)1));
            Assert.AreEqual(2, Math.Factorial((BigInteger)2));
            Assert.AreEqual(6, Math.Factorial((BigInteger)3));
            Assert.AreEqual(24, Math.Factorial((BigInteger)4));
            Assert.AreEqual(120, Math.Factorial((BigInteger)5));
            Assert.AreEqual(479_001_600, Math.Factorial((BigInteger)12));
            Assert.AreEqual(6_227_020_800, Math.Factorial((BigInteger)13));
            Assert.AreEqual(87_178_291_200, Math.Factorial((BigInteger)14));
            Assert.AreEqual(1_307_674_368_000, Math.Factorial((BigInteger)15));
            Assert.AreEqual(6_402_373_705_728_000, Math.Factorial((BigInteger)18));
            Assert.AreEqual(2_432_902_008_176_640_000, Math.Factorial((BigInteger)20));

            Assert.AreEqual(1.0, Math.Factorial(0.0));
            Assert.AreEqual(1.0, Math.Factorial(1.0));
            Assert.AreEqual(2.0, Math.Factorial(2.0));
            Assert.AreEqual(6.0, Math.Factorial(3.0));
            Assert.AreEqual(24.0, Math.Factorial(4.0));
            Assert.AreEqual(120.0, Math.Factorial(5.0));
            Assert.AreEqual(479_001_600.0, Math.Factorial(12.0));
        }

        [TestMethod()]
        public void RangeFactorialTest()
        {
            Assert.AreEqual(1, Math.RangeFactorial(0, 0));
            Assert.AreEqual(1, Math.RangeFactorial(0, 1));
            Assert.AreEqual(2, Math.RangeFactorial(1, 2));
            Assert.AreEqual(2 * 3, Math.RangeFactorial(2, 3));
            Assert.AreEqual(2 * 3 * 4, Math.RangeFactorial(2, 4));
            Assert.AreEqual(2 * 3 * 4 * 5, Math.RangeFactorial(2, 5));
            Assert.AreEqual(3 * 4 * 5 * 6, Math.RangeFactorial(3, 6));

            Assert.AreEqual(1.0, Math.RangeFactorial(0.0, 0.0));
            Assert.AreEqual(1.0, Math.RangeFactorial(0.0, 1.0));
            Assert.AreEqual(2.0, Math.RangeFactorial(1.0, 2.0));
            Assert.AreEqual(2 * 3, Math.RangeFactorial(2.0, 3.0));
            Assert.AreEqual(2 * 3 * 4, Math.RangeFactorial(2.0, 4.0));
            Assert.AreEqual(2 * 3 * 4 * 5, Math.RangeFactorial(2.0, 5.0));
            Assert.AreEqual(3 * 4 * 5 * 6, Math.RangeFactorial(3.0, 6.0));
        }

        [TestMethod()]
        public void GammaTest()
        {
            //Assert.AreEqual(Math.Factorial<double>(0), Math.Gamma<double>(1), 0.1);
            //Assert.AreEqual(Math.Factorial<double>(1), Math.Gamma<double>(2), 0.1);
            //Assert.AreEqual(Math.Factorial<double>(2), Math.Gamma<double>(3), 0.1);
            //Assert.AreEqual(Math.Factorial<double>(3), Math.Gamma<double>(4), 0.1);
            //Assert.AreEqual(Math.Factorial<double>(4), Math.Gamma<double>(5), 0.1);
            //Assert.AreEqual(Math.Factorial<double>(11), Math.Gamma<double>(12), 0.1);
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
                    if (n == 0) { continue; }
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
        public void PowTest_double()
        {
            double baseValue = 0;
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
                    var expected = SMath.Pow(n, e);
                    var actual = Math.Pow((double)n, e);
                    Assert.AreEqual(expected, actual, $"{n}^{e}");
                }
            }
            for (int n = -10; n <= 10; n++)
            {
                for (uint e = 0; e <= 9; e++)
                {
                    var expected = SMath.Pow(n, e);
                    var actual = Math.Pow((double)n, e);
                    Assert.AreEqual(expected, actual, $"{n}^{e}");
                }
            }
            for (double n = -10; n <= 10; n += 0.25)
            {
                for (double e = -10; e <= 10; e += 0.25)
                {
                    var expected = SMath.Pow(n, e);
                    if (double.IsNaN(expected))
                    {
                        continue;
                    }
                    if (double.IsInfinity(expected))
                    {
                        continue;
                    }
                    var actual = Math.Pow(n, e, double.Epsilon);
                    var expectedStr = expected.ToDecimalString();
                    var actualStr = actual.ToDecimalString();
                    Assert.AreEqual(expected, actual, 0.001, $"{n}^{e}");
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
                    var expected = (decimal)System.Math.Pow(n, e);
                    var actual = Math.Pow((decimal)n, e);
                    Assert.AreEqual(expected, actual, $"{n}^{e}");
                }
            }
            for (int n = -10; n <= 10; n++)
            {
                for (uint e = 0; e <= 9; e++)
                {
                    var expected = (decimal)System.Math.Pow(n, e);
                    var actual = Math.Pow((decimal)n, e);
                    Assert.AreEqual(expected, actual, $"{n}^{e}");
                }
            }
            decimal tolerance = 0.00000_00000_00000_00000_00000_1m;
            for (decimal n = -10; n <= 10; n += 0.25m)
            {
                for (decimal e = -10; e <= 10; e += 0.25m)
                {
                    var expected = System.Math.Pow((double)n, (double)e);
                    if (double.IsNaN(expected))
                    {
                        continue;
                    }
                    if (double.IsInfinity(expected))
                    {
                        continue;
                    }
                    var actual = Math.Pow(n, e, tolerance);
                    Assert.AreEqual((decimal)expected, actual, 0.00001m, $"{n}^{e}");
                }
            }
        }

        [TestMethod()]
        public void ExpTest()
        {
            for (Half i = -5; i <= (Half)5; i += (Half)0.5)
            {
                var expected = SMath.Exp((double)i);
                var actual = (double)Math.Exp(i);
                Assert.AreEqual(expected, actual, 0.5, $"i={i}");
            }
            for (float i = -5; i <= 5; i += 0.5f)
            {
                var expected = SMath.Exp(i);
                var actual = Math.Exp(i);
                Assert.AreEqual(expected, actual, 0.00001, $"i={i}");
            }
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = SMath.Exp(i);
                var actual = Math.Exp(i);
                Assert.AreEqual(expected, actual, 0.00000000001, $"i={i}");
            }
            for (decimal i = -5; i <= 5; i += 0.5m)
            {
                var expected = SMath.Exp((double)i);
                var actual = Math.Exp(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void Exp10Test()
        {
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = SMath.Pow(10, i);
                var actual = Math.Exp10(i);
                Assert.AreEqual(expected, actual, 0.000000001, $"i={i}");
            }
            for (decimal i = -5; i <= 5; i += 0.5m)
            {
                var expected = SMath.Pow(10, (double)i);
                var actual = Math.Exp10(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void Exp2Test()
        {
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = SMath.Pow(2, i);
                var actual = Math.Exp2(i);
                Assert.AreEqual(expected, actual, 0.00000000001, $"i={i}");
            }
            for (decimal i = -5; i <= 5; i += 0.5m)
            {
                var expected = SMath.Pow(2, (double)i);
                var actual = Math.Exp2(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void LogTest()
        {
            for (double i = 1; i <= 5; i += 0.5)
            {
                var expected = SMath.Log(i);
                var actual = Math.Log(i);
                Assert.AreEqual(expected, actual, 0.00000000001, $"i={i}");
            }
            for (decimal i = 1; i <= 5; i += 0.5m)
            {
                var expected = SMath.Log((double)i);
                var actual = Math.Log(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void Log10Test()
        {
            for (double i = 1; i <= 5; i += 0.5)
            {
                var expected = SMath.Log10(i);
                var actual = Math.Log10(i);
                Assert.AreEqual(expected, actual, 0.00000000001, $"i={i}");
            }
            for (decimal i = 1; i <= 5; i += 0.5m)
            {
                var expected = SMath.Log10((double)i);
                var actual = Math.Log10(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void Log2Test()
        {
            for (double i = 1; i <= 5; i += 0.5)
            {
                var expected = SMath.Log2(i);
                var actual = Math.Log2(i);
                Assert.AreEqual(expected, actual, 0.00000000001, $"i={i}");
            }
            for (decimal i = 1; i <= 5; i += 0.5m)
            {
                var expected = SMath.Log2((double)i);
                var actual = Math.Log2(i, Math.DecimalEpsilon);
                Assert.AreEqual((decimal)expected, actual, 0.00000000001m, $"i={i}");
            }
        }

        [TestMethod()]
        public void CachedPow10Test()
        {
            Assert.AreEqual(1, Math.CachedPow10(0));
            Assert.AreEqual(10, Math.CachedPow10(1));
            Assert.AreEqual(100, Math.CachedPow10(2));
            Assert.AreEqual(1000, Math.CachedPow10(3));
            Assert.AreEqual(10000, Math.CachedPow10(4));
            Assert.AreEqual(100000, Math.CachedPow10(5));
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
                var expected = BigDecimal.Pi;
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
                var actual = Math.RegularContinuedFraction<double>(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                Assert.AreEqual(expected, actual, 0.00000001);
            }
            // √2
            {
                var expected = SMath.Sqrt(2);
                var actual = Math.RegularContinuedFraction<double>(1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);
                Assert.AreEqual(expected, actual, 0.0000000001);
            }
            // √3
            {
                var expected = SMath.Sqrt(3);
                var actual = Math.RegularContinuedFraction<double>(1, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2);
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
                var tolerance = new BigDecimal(1, -100);
                var expected = BigDecimal.GoldenNumber;
                var actual = Math.MetallicNumber(BigDecimal.One, tolerance);

                expected = BigDecimal.Round(expected, 20, MidpointRounding.AwayFromZero);
                actual = BigDecimal.Round(actual, 20, MidpointRounding.AwayFromZero);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SqrtTest()
        {
            double Delta = 0.000000000000001;
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
            // System.Math.Sqrtと比較
            for (double i = -10; i <= 10; i++)
            {
                var expected = SMath.Sqrt(i);
                var actual = Math.Sqrt(i);
                if (double.IsNaN(expected))
                {
                    Assert.IsTrue(double.IsNaN(actual));
                }
                else
                {
                    Assert.AreEqual(expected, actual, Delta, $"i={i}");
                }
            }
        }

        [TestMethod()]
        public void RootTest()
        {
            double Delta = 0.000000000000001;
            // System.Math.Sqrtと比較
            for (double i = 0; i <= 10; i++)
            {
                var expected = SMath.Sqrt(i);
                var actual = Math.Root(i, 2);
                Assert.AreEqual(expected, actual, Delta, $"i={i}");
            }
            // 2乗根
            for (double i = -10; i <= 10; i += 0.5)
            {
                var expected = SMath.Sqrt(i);
                var actual = Math.Root(i, 2);
                if (double.IsNaN(expected))
                {
                    Assert.IsTrue(double.IsNaN(actual));
                }
                else
                {
                    Assert.AreEqual(expected, actual, Delta, $"i={i}");
                }
            }
            // 3乗根
            for (double i = -10; i <= 10; i += 0.5)
            {
                var expected = SMath.Cbrt(i);
                var actual = Math.Root(i, 3);
                if (double.IsNaN(expected))
                {
                    Assert.IsTrue(double.IsNaN(actual));
                }
                else
                {
                    Assert.AreEqual(expected, actual, Delta, $"i={i}");
                }
            }
        }

        [TestMethod()]
        public void SinTest()
        {
            for (double i = -10; i <= 10; i += 0.125)
            {
                var expected = SMath.Sin(i);
                var actual = Math.Sin(i);
                Assert.AreEqual(expected, actual, 0.00000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void CosTest()
        {
            for (double i = -10; i <= 10; i += 0.125)
            {
                var expected = SMath.Cos(i);
                var actual = Math.Cos(i);
                Assert.AreEqual(expected, actual, 0.00000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void TanTest()
        {
            for (double i = -10; i <= 10; i += 0.125)
            {
                var expected = SMath.Tan(i);
                var actual = Math.Tan(i);
                Assert.AreEqual(expected, actual, 0.0000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AsinTest()
        {
            for (double i = -1; i <= 1; i += 0.125)
            {
                var expected = SMath.Asin(i);
                var actual = Math.Asin(i);
                Assert.AreEqual(expected, actual, 0.0001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AcosTest()
        {
            for (double i = -1; i <= 1; i += 0.125)
            {
                var expected = SMath.Acos(i);
                var actual = Math.Acos(i);
                Assert.AreEqual(expected, actual, 0.0001, $"i={i}");
            }
        }

        [TestMethod()]
        public void AtanTest()
        {
            for (double i = -2; i <= 2; i += 0.125)
            {
                var expected = SMath.Atan(i);
                var actual = Math.Atan(i);
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }
        }

        [TestMethod()]
        public void Atan2Test()
        {
            const double Delta = 0.00000000000001;
            {
                double x = 0;
                double y = 0;
                var expected = SMath.Atan2(y, x);
                var actual = Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, Delta, $"y={y},x={x}");
            }
            {
                double x = -0.3;
                double y = -1;
                var expected = SMath.Atan2(y, x);
                var expected2 = SMath.Atan(y / x);
                var actual = Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, Delta, $"y={y},x={x}");
            }
            {
                double x = 0.9;
                double y = 1;
                var expected = SMath.Atan2(y, x);
                var expected2 = SMath.Atan(y / x);
                var actual = Math.Atan2(y, x);
                Assert.AreEqual(expected, actual, Delta, $"y={y},x={x}");
            }
            for (double x = -1; x <= 1; x += 0.125)
            {
                for (double y = -1; y <= 1; y += 0.125)
                {
                    if (y == 0 || x == 0)
                    {
                        continue;
                    }
                    var expected = SMath.Atan2(y, x);
                    var actual = Math.Atan2(y, x);
                    Assert.AreEqual(expected, actual, 0.0000001, $"y={y},x={x}");
                }
            }
        }

        [TestMethod()]
        public void NapiersConstantTest()
        {
            {
                var expected = SMath.E;
                var actual = Math.NapiersConstant(0.00000_00000_1);
                Assert.AreEqual(expected, actual, 0.00000_00000_1);
            }
        }

        [TestMethod()]
        public void TrapezoidalRuleTest()
        {
            // double
            {
                double a = 0.0;// 積分区間の下限
                double b = double.Pi;// 積分区間の上限
                int n = 10000;// 分割数
                double result = Math.TrapezoidalIntegral(a, b, n, SMath.Sin);
                Assert.AreEqual(2.0, result, 0.0000001);
            }
        }

        [TestMethod()]
        public void PermutationTest()
        {
            Assert.AreEqual(1, Math.Permutation(0, 0));
            Assert.AreEqual(1, Math.Permutation(1, 1));
            Assert.AreEqual(20, Math.Permutation(5, 2));
            Assert.AreEqual(120, Math.Permutation(6, 3));
            Assert.AreEqual(210, Math.Permutation(7, 3));
            Assert.AreEqual(720, Math.Permutation(6, 6));
            Assert.AreEqual(720, Math.Permutation(10, 3));
            Assert.AreEqual(1680, Math.Permutation(8, 4));
        }

        [TestMethod()]
        public void CombinationTest()
        {
            Assert.AreEqual(1, Math.Combination(0, 0));
            Assert.AreEqual(1, Math.Combination(1, 1));
            Assert.AreEqual(10, Math.Combination(5, 3));
            Assert.AreEqual(20, Math.Combination(6, 3));
            Assert.AreEqual(56, Math.Combination(8, 3));
        }
    }
}