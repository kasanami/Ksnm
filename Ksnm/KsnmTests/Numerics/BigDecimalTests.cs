using Ksnm.ExtensionMethods.System.Random;
using Ksnm.ExtensionMethods.System.Decimal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class BigDecimalTests
    {

        [TestMethod()]
        public void ConstructorTest1()
        {
            for (int source = -10; source < 10; source += 1)
            {
                var sample = new BigDecimal(source);
                Assert.AreEqual(source, sample.ToDecimal(), $"{source} {sample}");
            }
            for (uint source = 0; source < 10; source += 1)
            {
                var sample = new BigDecimal(source);
                Assert.AreEqual(source, sample.ToDecimal(), $"{source} {sample}");
            }
            for (long source = -10; source < 10; source += 1)
            {
                var sample = new BigDecimal(source);
                Assert.AreEqual(source, sample.ToDecimal(), $"{source} {sample}");
            }
            for (ulong source = 0; source < 10; source += 1)
            {
                var sample = new BigDecimal(source);
                Assert.AreEqual(source, sample.ToDecimal(), $"{source} {sample}");
            }
            for (decimal source = -10; source < 10; source += 0.1m)
            {
                var sample = new BigDecimal(source);
                Assert.AreEqual(source, sample.ToDecimal(), $"{source} {sample}");
            }
        }

        [TestMethod()]
        public void MinimizeExponentTest()
        {
            var sample = new BigDecimal(1, 0, 0);
            sample.MinimizeExponent();
            Assert.AreEqual(1, sample.Mantissa);
            Assert.AreEqual(0, sample.Exponent);

            sample = new BigDecimal(1, 0, -5);
            sample.MinimizeExponent();
            Assert.AreEqual(100000, sample.Mantissa);
            Assert.AreEqual(-5, sample.Exponent);

            sample = new BigDecimal(1, 0, -6);
            sample.MinimizeExponent();
            Assert.AreEqual(1000000, sample.Mantissa);
            Assert.AreEqual(-6, sample.Exponent);
        }

        [TestMethod()]
        public void MinimizeMantissaTest()
        {
            for (decimal i = 0.01m; i < 100; i *= 10)
            {
                var sample = new BigDecimal(i);
                sample.MinimizeExponent();
                sample.MinimizeMantissa();
                Assert.AreEqual(i, sample.ToDecimal(), $"i={i} sample={sample}");
            }

            {
                decimal i = 0.123m;
                var sample = new BigDecimal(i);
                sample.MinimizeExponent();
                sample.MinimizeMantissa();
                Assert.AreEqual(i, sample.ToDecimal(), $"i={i} sample={sample}");
            }

            {
                // 0.123 と同値
                var sample = new BigDecimal(123000, -6);
                sample.MinimizeMantissa();
                Assert.AreEqual(123, sample.Mantissa);
                Assert.AreEqual(-3, sample.Exponent);
            }
        }

        [TestMethod()]
        public void PowTest()
        {
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    var expected = BigInteger.Pow(i, j);
                    var actual = BigDecimal.Pow(i, j);
                    Assert.AreEqual(expected, actual, $"{i} ^ {j}");
                }
            }
            for (decimal i = -10; i <= 10; i += 0.1m)
            {
                for (int j = -10; j <= 10; j++)
                {
                    if (i == 0 && j < 0) { continue; }// 結果が無限大になるのでスキップ
                    var expected = double.Pow((double)i, j);
                    var actual = (double)(BigDecimal.Pow(i, j).ToDouble());
                    Assert.AreEqual(expected, actual, 0.00001, $"{i} ^ {j}");
                }
            }
        }

        [TestMethod()]
        public void Pow10Test()
        {
            for (int i = 0; i < 3; i++)
            {
                var sample = BigDecimal.Pow10(0);
                Assert.AreEqual((System.Numerics.BigInteger)1, sample);
                sample = BigDecimal.Pow10(1);
                Assert.AreEqual((System.Numerics.BigInteger)10, sample);
                sample = BigDecimal.Pow10(2);
                Assert.AreEqual((System.Numerics.BigInteger)100, sample);
                sample = BigDecimal.Pow10(3);
                Assert.AreEqual((System.Numerics.BigInteger)1000, sample);
            }
        }

        #region ILogarithmicFunctions
        [TestMethod()]
        public void LogTest()
        {
            BigDecimal.DefaultMinExponent = -100;
            {
                var expected = 2.3025850929940456840179914547m;
                var actual2 = BigDecimal.Log(10);
                var actual = (decimal)actual2;
                Assert.AreEqual(expected, actual, ExtendedDecimal.Epsilon);
            }
            for (double i = 1; i <= 10; i += 0.5)
            {
                var expected = double.Log(i);
                var actual2 = BigDecimal.Log((BigDecimal)i);
                var actual = (double)actual2;
                Assert.AreEqual(expected, actual, 0.000000000000001, $"i={i}");
            }

        }
        [TestMethod()]
        public void Log10Test()
        {
            var f10 = new BigDecimal(10);
            var f100 = new BigDecimal(100);
            var f1000 = new BigDecimal(1000);
            var log10_10 = BigDecimal.Log10(f10);
            var log10_100 = BigDecimal.Log10(f100);
            var log10_1000 = BigDecimal.Log10(f1000);
            Assert.AreEqual(1, log10_10.ToDecimal());
            Assert.AreEqual(2, log10_100.ToDecimal(), 0.00000_00000_00000_00000_00000_001m);
            Assert.AreEqual(3, log10_1000.ToDecimal(), 0.00000_00000_00000_0001m);
        }
        [TestMethod()]
        public void Log2Test()
        {
            var delta = 0.00000_00000_00000_00000_00000_001m;
            var f2 = new BigDecimal(2);
            var f4 = new BigDecimal(4);
            var f8 = new BigDecimal(8);
            var log2_2 = BigDecimal.Log2(f2);
            var log2_4 = BigDecimal.Log2(f4);
            var log2_8 = BigDecimal.Log2(f8);
            Assert.AreEqual(1, log2_2.ToDecimal(), delta);
            Assert.AreEqual(2, log2_4.ToDecimal(), delta);
            Assert.AreEqual(3, log2_8.ToDecimal(), delta);
        }
        #endregion ILogarithmicFunctions

        #region IExponentialFunctions
        [TestMethod()]
        public void ExpTest()
        {
            BigDecimal.DefaultMinExponent = -100;
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = double.Exp(i);
                var actual2 = BigDecimal.Exp((BigDecimal)i);
                var actual = (double)actual2;
                Assert.AreEqual(expected, actual, 0.00000_000001, $"i={i}");
            }
        }
        [TestMethod()]
        public void Exp10Test()
        {
            BigDecimal.DefaultMinExponent = -100;
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = double.Exp10(i);
                var actual2 = BigDecimal.Exp10((BigDecimal)i);
                var actual = (double)actual2;
                Assert.AreEqual(expected, actual, 0.00000_000001, $"i={i}");
            }
        }
        [TestMethod()]
        public void Exp2Test()
        {
            BigDecimal.DefaultMinExponent = -100;
            for (double i = -5; i <= 5; i += 0.5)
            {
                var expected = double.Exp2(i);
                var actual2 = BigDecimal.Exp2((BigDecimal)i);
                var actual = (double)actual2;
                Assert.AreEqual(expected, actual, 0.00000_000001, $"i={i}");
            }
        }
        #endregion IExponentialFunctions

        [TestMethod()]
        public void MaxExponentTest()
        {
            for (int i = 0; i < 30; i++)
            {
                var sample = BigDecimal.Pow10(i);
                Assert.AreEqual(i, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(-100);
                Assert.AreEqual(2, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(-120);
                Assert.AreEqual(1, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(100);
                Assert.AreEqual(2, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(120);
                Assert.AreEqual(1, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(123);
                Assert.AreEqual(0, BigDecimal.MaxExponent(sample));
            }
            {
                var sample = new BigInteger(0);
                Assert.AreEqual(0, BigDecimal.MaxExponent(sample));
            }
        }

        [TestMethod()]
        public void GetFractionalTest()
        {
            for (decimal i = -10m; i <= 10; i += 0.1m)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i.GetFractional(), sample.GetFractional(), $"i={i}");
            }
        }

        [TestMethod()]
        public void ToInt32Test()
        {
            for (int i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToInt32());
            }
            for (int i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToInt32());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual((int)i, sample.ToInt32());
            }
        }

        [TestMethod()]
        public void ToUInt32Test()
        {
            for (uint i = 0; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToUInt32());
            }
            for (uint i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToUInt32());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual((uint)i, sample.ToUInt32());
            }
        }

        [TestMethod()]
        public void ToInt64Test()
        {
            for (int i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToInt64());
            }
            for (int i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToInt64());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual((long)i, sample.ToInt64());
            }
        }

        [TestMethod()]
        public void ToUInt64Test()
        {
            for (uint i = 0; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToUInt64());
            }
            for (uint i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToUInt64());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual((ulong)i, sample.ToUInt64());
            }
        }

        [TestMethod()]
        public void ToDoubleTest()
        {
            {
                var sample = new BigDecimal(0);
                Assert.AreEqual(0.0, sample.ToDouble());
            }
            {
                var sample = new BigDecimal(1, -500);
                Assert.AreEqual(+0.0, sample.ToDouble());
            }
            {
                var sample = new BigDecimal(-1, -500);
                Assert.AreEqual(-0.0, sample.ToDouble());
            }
            {
                var sample = new BigDecimal(1, 500);
                Assert.AreEqual(double.PositiveInfinity, sample.ToDouble());
            }
            {
                var sample = new BigDecimal(-1, 500);
                Assert.AreEqual(double.NegativeInfinity, sample.ToDouble());
            }
            for (double i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDouble());
            }
            for (double i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDouble());
            }
            for (double i = 1; i > 0.001; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDouble());
            }
        }

        [TestMethod()]
        public void ToDecimalTest()
        {
            {
                var sample = new BigDecimal(0);
                Assert.AreEqual(0.0m, sample.ToDecimal());
            }
            {
                var sample = new BigDecimal(1, -500);
                Assert.AreEqual(+0.0m, sample.ToDecimal());
            }
            {
                var sample = new BigDecimal(-1, -500);
                Assert.AreEqual(-0.0m, sample.ToDecimal());
            }
            {
                var sample = new BigDecimal(1, 500);
                Assert.AreEqual(decimal.MaxValue, sample.ToDecimal());
            }
            {
                var sample = new BigDecimal(-1, 500);
                Assert.AreEqual(decimal.MinValue, sample.ToDecimal());
            }
            {
                var i = BigInteger.Parse("12345678901234567890123456789");
                var sample = new BigDecimal(i, -29);
                Assert.AreEqual(0.12345678901234567890123456789m, sample.ToDecimal());
            }
            {
                var i = BigInteger.Parse("123456789012345678901234567890123456789");
                var sample = new BigDecimal(i, -39);
                Assert.AreEqual(0.12345678901234567890123456789m, sample.ToDecimal());
            }
            for (decimal i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
            for (decimal i = 1; i < 1_000_000m; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
        }

        [TestMethod()]
        public void ToBigIntegerTest()
        {
            for (BigInteger i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToBigInteger());
            }
            for (BigInteger i = 1; i < 1_000_000; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToBigInteger());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual((BigInteger)i, sample.ToBigInteger());
            }
        }

        [TestMethod()]
        public void RoundTest()
        {
            {
                var mode = MidpointRounding.AwayFromZero;
                for (decimal i = -2m; i <= 2m; i += 0.1m)
                {
                    var actual = BigDecimal.Round(i, mode);
                    var expected = decimal.Round(i, mode);
                    Assert.AreEqual(expected, actual, $"i={i}");
                }
                mode = MidpointRounding.ToEven;
                for (decimal i = -2m; i <= 2m; i += 0.1m)
                {
                    var actual = BigDecimal.Round(i, mode);
                    var expected = decimal.Round(i, mode);
                    Assert.AreEqual(expected, actual, $"i={i}");
                }
            }

            for (int j = 0; j < 10; j++)
            {
                var mode = MidpointRounding.AwayFromZero;
                for (decimal i = -2m; i <= 2m; i += 0.1111111111m)
                {
                    var actual = BigDecimal.Round(i, j, mode);
                    var expected = decimal.Round(i, j, mode);
                    Assert.AreEqual(expected, actual, $"i={i} j={j}");
                }
                mode = MidpointRounding.ToEven;
                for (decimal i = -2m; i <= 2m; i += 0.1m)
                {
                    var actual = BigDecimal.Round(i, j, mode);
                    var expected = decimal.Round(i, j, mode);
                    Assert.AreEqual(expected, actual, $"i={i} j={j}");
                }
            }

            for (decimal i = -0.9m; i < 1.0m; i += 0.1m)
            {
                var actual = new BigDecimal(i);
                actual.RoundMantissa(1);
                var expected = decimal.Round(i, BigDecimal.DefaultMidpointRounding);
                Assert.AreEqual(expected, actual, $"i={i}");
            }

            {
                var actual = new BigDecimal(15);
                actual.RoundMantissa(1);
                var expected = new BigDecimal(20);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(-15);
                actual.RoundMantissa(1);
                var expected = new BigDecimal(-20);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(25);
                actual.RoundMantissa(1);
                var expected = new BigDecimal(20);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(-25);
                actual.RoundMantissa(1);
                var expected = new BigDecimal(-20);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void TruncateTest()
        {
            for (decimal i = -2m; i <= 2m; i += 0.1m)
            {
                var actual = BigDecimal.Truncate(i);
                var expected = decimal.Truncate(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
            for (decimal i = -2m; i <= 2m; i += 0.11m)
            {
                var actual = BigDecimal.Truncate(i);
                var expected = decimal.Truncate(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
        }

        [TestMethod()]
        public void CeilingTest()
        {
            for (decimal i = -2m; i <= 2m; i += 0.1m)
            {
                var actual = BigDecimal.Ceiling(i);
                var expected = decimal.Ceiling(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
            for (decimal i = -2m; i <= 2m; i += 0.11m)
            {
                var actual = BigDecimal.Ceiling(i);
                var expected = decimal.Ceiling(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
        }

        [TestMethod()]
        public void FloorTest()
        {
            for (decimal i = -2m; i <= 2m; i += 0.1m)
            {
                var actual = BigDecimal.Floor(i);
                var expected = decimal.Floor(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
            for (decimal i = -2m; i <= 2m; i += 0.11m)
            {
                var actual = BigDecimal.Floor(i);
                var expected = decimal.Floor(i);
                Assert.AreEqual(expected, actual, $"i={i}");
            }
        }

        [TestMethod()]
        public void SqrtTest()
        {
            int precision = -BigDecimal.DefaultMinExponent;
            for (int i = 0; i < 100; i++)
            {
                var expected = new BigDecimal(i);
                var sample = expected * expected;
                Assert.AreEqual(expected, BigDecimal.Sqrt(sample, precision));
            }
            for (int i = 0; i < 100; i++)
            {
                var expected = new BigDecimal(i);
                var sample = expected * expected;
                Assert.AreEqual(expected, BigDecimal.Sqrt(sample));
            }
            {
                var expected = new BigDecimal(100);
                var sample = expected * expected;
                Assert.AreEqual(expected, BigDecimal.Sqrt(sample, precision));
            }
            {
                var actual = BigDecimal.Sqrt(2, 90);
                Assert.AreEqual("1.414213562373095048801688724209698078569671875376948073176679737990732478462107038850387534", actual.ToString());
            }
        }

        [TestMethod()]
        public void OperationsTest1()
        {
            var sample = new BigDecimal(1, 2);
            var expected1 = new BigDecimal(1, 2);
            var expected2 = new BigDecimal(-1, 2);
            Assert.AreEqual(expected1, +sample);
            Assert.AreEqual(expected2, -sample);
        }

        [TestMethod()]
        public void OperationsTest2()
        {
            // 精度不足テスト
            {
                var sample = new BigDecimal(1, 0, -1);
                var sample2 = new BigDecimal(1000, 0, 0);
                var sample3 = sample / sample2;
                var d = sample3.ToDecimal();
                Assert.AreEqual(0, d);

                sample = new BigDecimal(1, 0, -2);
                sample2 = new BigDecimal(1000, 0, 0);
                sample3 = sample / sample2;
                d = sample3.ToDecimal();
                Assert.AreEqual(0, d);

                sample = new BigDecimal(1, 0, -3);
                sample2 = new BigDecimal(1000, 0, 0);
                sample3 = sample / sample2;
                d = sample3.ToDecimal();
                Assert.AreEqual(0.001m, d);

                sample = new BigDecimal(1, 0, -4);
                sample2 = new BigDecimal(1000, 0, 0);
                sample3 = sample / sample2;
                d = sample3.ToDecimal();
                Assert.AreEqual(0.001m, d);
            }
            {
                var sample = new BigDecimal(0.01m);
                var sample2 = new BigDecimal(0.1m);
                var sample3 = sample * sample2;
                var d = sample3.ToDecimal();
                Assert.AreEqual(0.01m * 0.1m, d);
            }
            {
                var sample = new BigDecimal(0.1m);
                var sample2 = new BigDecimal(0.1m);
                var sample3 = sample + sample2;
                var d = sample3.ToDecimal();
                Assert.AreEqual(0.1m + 0.1m, d);
            }

            for (decimal i = 0.01m; i < 100; i *= 10)
            {
                var sample = new BigDecimal(i);
                for (decimal j = 0.01m; j < 100; j *= 10)
                {
                    var sample2 = new BigDecimal(j);
                    // +
                    Assert.AreEqual(i + j, (sample + sample2).ToDecimal(), $"{i} + {j}");
                    // -
                    Assert.AreEqual(i - j, (sample - sample2).ToDecimal(), $"{i} - {j}");
                    // *
                    Assert.AreEqual(i * j, (sample * sample2).ToDecimal(), $"{i} * {j}");
                    // /
                    Assert.AreEqual(i / j, (sample / sample2).ToDecimal(), $"{i} / {j}");
                    // %
                    Assert.AreEqual(i % j, (sample % sample2).ToDecimal(), $"{i} % {j}");
                    // ==
                    Assert.AreEqual(i == j, sample == sample2);
                    // !=
                    Assert.AreEqual(i != j, sample != sample2);
                    // >
                    Assert.AreEqual(i > j, sample > sample2);
                    // <
                    Assert.AreEqual(i < j, sample < sample2);
                    // >=
                    Assert.AreEqual(i >= j, sample >= sample2);
                    // <=
                    Assert.AreEqual(i <= j, sample <= sample2);
                }
            }

            for (decimal i = -10; i < 10; i++)
            {
                var sample = new BigDecimal(i);
                for (decimal j = -10; j < 10; j++)
                {
                    var sample2 = new BigDecimal(j);
                    // +
                    Assert.AreEqual(i + j, (sample + sample2).ToDecimal(), $"{i} + {j}");
                    // -
                    Assert.AreEqual(i - j, (sample - sample2).ToDecimal(), $"{i} - {j}");
                    // *
                    Assert.AreEqual(i * j, (sample * sample2).ToDecimal(), $"{i} * {j}");
                    if (j != 0)
                    {
                        // /
                        Assert.AreEqual(i / j, (sample / sample2).ToDecimal(), $"{i} / {j}");
                        // %
                        Assert.AreEqual(i % j, (sample % sample2).ToDecimal(), $"{i} % {j}");
                    }
                    // ==
                    Assert.AreEqual(i == j, sample == sample2);
                    // !=
                    Assert.AreEqual(i != j, sample != sample2);
                    // >
                    Assert.AreEqual(i > j, sample > sample2);
                    // <
                    Assert.AreEqual(i < j, sample < sample2);
                    // >=
                    Assert.AreEqual(i >= j, sample >= sample2);
                    // <=
                    Assert.AreEqual(i <= j, sample <= sample2);
                }
            }
#if true
            for (decimal i = -10; i < 10; i++)
            {
                var sample = new BigDecimal(i);
                for (int j = -10; j < 10; j++)
                {
                    // +
                    Assert.AreEqual(i + j, (sample + j).ToDecimal(), $"{i} + {j}");
                    // -
                    Assert.AreEqual(i - j, (sample - j).ToDecimal(), $"{i} - {j}");
                    // *
                    Assert.AreEqual(i * j, (sample * j).ToDecimal(), $"{i} * {j}");
                    if (j != 0)
                    {
                        // /
                        Assert.AreEqual(i / j, (sample / j).ToDecimal(), $"{i} / {j}");
                        // %
                        Assert.AreEqual(i % j, (sample % j).ToDecimal(), $"{i} % {j}");
                    }
                    // ==
                    Assert.AreEqual(i == j, sample == j);
                    // !=
                    Assert.AreEqual(i != j, sample != j);
                    // >
                    Assert.AreEqual(i > j, sample > j);
                    // <
                    Assert.AreEqual(i < j, sample < j);
                    // >=
                    Assert.AreEqual(i >= j, sample >= j);
                    // <=
                    Assert.AreEqual(i <= j, sample <= j);
                }
            }
#endif
#if true
            var random = new Ksnm.Randoms.Xorshift128();
            for (int i = 0; i < 10; i++)
            {
                var source = new decimal(random.Next(), 0, 0, random.NextBool(), (byte)random.Next(28));
                var sample = new BigDecimal(source);
                for (int j = 0; j < 10; j++)
                {
                    var source2 = new decimal(random.Next(), 0, 0, random.NextBool(), (byte)random.Next(28));
                    var sample2 = new BigDecimal(source2);
                    try
                    {
                        // +
                        Assert.AreEqual(source + source2, (sample + sample2).ToDecimal(), $"{source} + {source2}");
                        // -
                        Assert.AreEqual(source - source2, (sample - sample2).ToDecimal(), $"{source} - {source2}");
                        // *
                        Assert.AreEqual(source * source2, (sample * sample2).ToDecimal(), $"{source} * {source2}");
                        // /
                        Assert.AreEqual(source / source2, (sample / sample2).ToDecimal(), $"{source} / {source2}");
                        // %
                        Assert.AreEqual(source % source2, (sample % sample2).ToDecimal(), $"{source} % {source2}");
                    }
                    catch (OverflowException)
                    {
                        // OverflowExceptionはココでは無視
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // InvalidCastExceptionはココでは無視
                    }
                    // ==
                    Assert.AreEqual(source == source2, sample == sample2, $"{source} == {source2}");
                    // !=
                    Assert.AreEqual(source != source2, sample != sample2, $"{source} != {source2}");
                    // >
                    Assert.AreEqual(source > source2, sample > sample2, $"{source} > {source2}");
                    // <
                    Assert.AreEqual(source < source2, sample < sample2, $"{source} < {source2}");
                    // >=
                    Assert.AreEqual(source >= source2, sample >= sample2, $"{source} >= {source2}");
                    // <=
                    Assert.AreEqual(source <= source2, sample <= sample2, $"{source} <= {source2}");
                }
            }
#endif
        }

        [TestMethod()]
        public void OperationsTest3()
        {
            var sample1 = new BigDecimal(1);
            var sample2 = new BigDecimal(1, -30);
            var sample3 = sample1 + sample2;
            var expected = int.Min(sample1.MinExponent, sample2.MinExponent);
            Assert.AreEqual(expected, sample3.MinExponent);
            sample3 = sample2 + sample1;
            Assert.AreEqual(expected, sample3.MinExponent);

            sample3 = sample1 - sample2;
            Assert.AreEqual(expected, sample3.MinExponent);
            sample3 = sample2 - sample1;
            Assert.AreEqual(expected, sample3.MinExponent);

            sample3 = sample1 * sample2;
            Assert.AreEqual(expected, sample3.MinExponent);
            sample3 = sample2 * sample1;
            Assert.AreEqual(expected, sample3.MinExponent);
        }

        /// <summary>
        /// 乱数により生成した組み合わせを記述
        /// </summary>
        [TestMethod()]
        public void OperationsTest4()
        {
            {
                var source1 = -8m;
                var source2 = -7m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} * {source2}");
            }
            {
                var source1 = 0.000000000000880275091m;
                var source2 = 0.001595151972m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} * {source2}");
            }
            {
                var source1 = 0.0000000000002110104827m;
                var source2 = -120.1044625m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} * {source2}");
            }
            {
                var source1 = 0.000000001062451658m;
                var source2 = 183.8252767m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} * {source2}");
            }
            {
                var source1 = 0.000000000000077158759m;
                var source2 = 11.60083295m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 * sample2;
                var d = source1 * source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} * {source2}");
            }
            {
                var source1 = -0.00000000000000000191855991m;
                var source2 = 0.0000001862788385m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} / {source2}");
            }
            {
                var source1 = 0.000000000000000000000000001m;
                var source2 = 0.0000000000000000000000000002m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} / {source2}");
            }
            {
                var source1 = 10000000000000000000000000000m;
                var source2 = 20000000000000000000000000000m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                var d_m = d.GetMantissa();
                var d_e = d.GetExponent();
                Assert.AreEqual(d, bd, $"{source1} / {source2}");
            }
            {
                var source1 = -0.00000000000001046m;
                var source2 = -7.316m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 - sample2;
                var d = source1 - source2;
                var bd = sample3.ToDecimal();
                Assert.AreEqual(d, bd, $"{source1} {source2}");
            }
#if false// 解決できない保留
            {
                var source1 = 83877277.4m;
                var source2 = -1526.188887m;
                var sample1 = new BigDecimal(source1);
                var sample2 = new BigDecimal(source2);
                var sample3 = sample1 / sample2;
                var d = source1 / source2;
                var bd = sample3.ToDecimal();
                Assert.AreEqual(d, bd, $"{source1} {source2}");
            }
#endif
        }

        [TestMethod()]
        public void CastTest()
        {
            BigDecimal sample;
            for (sbyte source = -10; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<sbyte>(source, (sbyte)sample);
            }
            for (short source = -10; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<short>(source, (short)sample);
            }
            for (int source = -10; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<int>(source, (int)sample);
            }
            for (long source = -10; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<long>(source, (long)sample);
            }
            for (byte source = 0; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<byte>(source, (byte)sample);
            }
            for (ushort source = 0; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<ushort>(source, (ushort)sample);
            }
            for (uint source = 0; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<uint>(source, (uint)sample);
            }
            for (ulong source = 0; source < 10; source++)
            {
                sample = source;
                Assert.AreEqual<ulong>(source, (ulong)sample);
            }
            for (float source = 0; source < 10; source += 0.25f)
            {
                sample = (BigDecimal)source;
                Assert.AreEqual<float>(source, (float)sample);
            }
            for (double source = 0; source < 10; source += 0.25)
            {
                sample = (BigDecimal)source;
                Assert.AreEqual<double>(source, (double)sample);
            }
            for (decimal source = 0; source < 10; source += 0.1m)
            {
                sample = (BigDecimal)source;
                Assert.AreEqual<decimal>(source, (decimal)sample);
            }
            for (BigInteger source = 0; source < 10; source++)
            {
                sample = (BigDecimal)source;
                Assert.AreEqual<BigInteger>(source, (BigInteger)sample);
            }

            {
                BigDecimal bigDecimal = BigDecimal.Parse("1234567890123456789012345678901234567890.123456789");
                var d = bigDecimal.ToDecimal();
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var random = new Ksnm.Randoms.Xorshift128();
            for (int i = 0; i < 10; i++)
            {
                var m = random.Next();
                var e = random.Next();
                var sample1 = new BigDecimal(m, e);
                var sample2 = new BigDecimal(m, e);
                Assert.AreEqual(sample1.GetHashCode(), sample2.GetHashCode());
                var sample3 = sample1;
                Assert.AreEqual(sample3.GetHashCode(), sample2.GetHashCode());
            }
            // 内部値が不一致でも、表現する値が同じならハッシュコードも同じになる
            {
                // 0.1という値としては同じ
                var decimal1 = new decimal(1, 0, 0, false, 1);
                var decimal2 = new decimal(10, 0, 0, false, 2);
                var bigDecimal1 = new BigDecimal(1, -1);
                var bigDecimal2 = new BigDecimal(10, -2);
                Assert.AreEqual(
                    decimal1.GetHashCode() == decimal2.GetHashCode(),
                    bigDecimal1.GetHashCode() == bigDecimal2.GetHashCode(),
                    $"{bigDecimal1}:{bigDecimal2}");
            }

            {
                // 100という値としては同じ
                var bigDecimal1 = new BigDecimal(1, 2);
                var bigDecimal2 = new BigDecimal(10, 1);
                Assert.IsTrue(bigDecimal1.GetHashCode() == bigDecimal2.GetHashCode(), $"{bigDecimal1}:{bigDecimal2}");
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var sample = new BigDecimal(1m);
            Assert.AreEqual("1", sample.ToString());
            sample = new BigDecimal(12m);
            Assert.AreEqual("12", sample.ToString());
            sample = new BigDecimal(123m);
            Assert.AreEqual("123", sample.ToString());
            sample = new BigDecimal(12.3m);
            Assert.AreEqual("12.3", sample.ToString());
            sample = new BigDecimal(1.23m);
            Assert.AreEqual("1.23", sample.ToString());
            sample = new BigDecimal(0.123m);
            Assert.AreEqual("0.123", sample.ToString());
            sample = new BigDecimal(0.0123m);
            Assert.AreEqual("0.0123", sample.ToString());
            sample = new BigDecimal(0.00123m);
            Assert.AreEqual("0.00123", sample.ToString());

            sample = new BigDecimal(123, 1);
            Assert.AreEqual("1230", sample.ToString());
            sample = new BigDecimal(123, 2);
            Assert.AreEqual("12300", sample.ToString());

            sample = new BigDecimal(-1m);
            Assert.AreEqual("-1", sample.ToString());
            sample = new BigDecimal(-12m);
            Assert.AreEqual("-12", sample.ToString());
            sample = new BigDecimal(-123m);
            Assert.AreEqual("-123", sample.ToString());
            sample = new BigDecimal(-12.3m);
            Assert.AreEqual("-12.3", sample.ToString());
            sample = new BigDecimal(-1.23m);
            Assert.AreEqual("-1.23", sample.ToString());
            sample = new BigDecimal(-0.123m);
            Assert.AreEqual("-0.123", sample.ToString());
            sample = new BigDecimal(-0.0123m);
            Assert.AreEqual("-0.0123", sample.ToString());
            sample = new BigDecimal(-0.00123m);
            Assert.AreEqual("-0.00123", sample.ToString());

            sample = new BigDecimal(-123, 1);
            Assert.AreEqual("-1230", sample.ToString());
            sample = new BigDecimal(-123, 2);
            Assert.AreEqual("-12300", sample.ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            {
                var sample1 = new BigDecimal(100, 1);
                var sample2 = new BigDecimal(100, 1);
                Assert.IsTrue(sample1.Equals(sample2));
                Assert.IsTrue(sample2.Equals(sample1));
            }
            {
                var sample1 = new BigDecimal(10, 2);
                var sample2 = new BigDecimal(100, 1);
                Assert.IsTrue(sample1.Equals(sample2));
                Assert.IsTrue(sample2.Equals(sample1));
            }
            {
                var sample1 = new BigDecimal(10, -2);
                var sample2 = new BigDecimal(100, -3);
                Assert.IsTrue(sample1.Equals(sample2));
                Assert.IsTrue(sample2.Equals(sample1));
            }
        }

        [TestMethod()]
        public void CompareToTest()
        {
            {
                var sample1 = new BigDecimal(1, 1);
                var sample2 = new BigDecimal(2, 1);
                Assert.AreEqual(-1, sample1.CompareTo(sample2));
                Assert.AreEqual(+1, sample2.CompareTo(sample1));
            }
            for (decimal d1 = -1; d1 <= 1; d1 += 0.1m)
            {
                var sample1 = new BigDecimal(d1);
                for (decimal d2 = -1; d2 <= 1; d2 += 0.1m)
                {
                    var sample2 = new BigDecimal(d2);
                    Assert.AreEqual(d1.CompareTo(d2), sample1.CompareTo(sample2), $"{d1} {d2}");
                }
            }
        }

        [TestMethod()]
        public void ParseTest()
        {
            for (decimal source = -100; source <= 100; source += 0.5m)
            {
                var str = source.ToString();
                var sample = BigDecimal.Parse(str);
                Assert.AreEqual(source, sample, $"{source}");
            }
            {
                var source = "3.14159265358979323846264338327950288";
                var sample = BigDecimal.Parse(source);
                Assert.AreEqual(source, sample.ToString(), $"{source}");
                Assert.AreEqual(BigInteger.Parse("314159265358979323846264338327950288"), sample.Mantissa, $"{source}");
                Assert.AreEqual(-35, sample.Exponent, $"{source}");
            }
            {
                var source = "0.1234567890123456789012345678901234567890";
                var sample = BigDecimal.Parse(source);
                Assert.AreEqual(source, sample.ToString(), $"{source}");
                Assert.AreEqual(BigInteger.Parse("1234567890123456789012345678901234567890"), sample.Mantissa, $"{source}");
                Assert.AreEqual(-40, sample.Exponent, $"{source}");
            }
            {
                var source = "-0.123456789";
                var sample = BigDecimal.Parse(source);
                Assert.AreEqual(source, sample.ToString(), $"{source}");
                Assert.AreEqual(-123456789, sample.Mantissa, $"{source}");
                Assert.AreEqual(-9, sample.Exponent, $"{source}");
            }
        }
    }
}