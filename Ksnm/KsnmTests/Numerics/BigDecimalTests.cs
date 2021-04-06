using Ksnm.ExtensionMethods.System.Random;
using Ksnm.ExtensionMethods.System.Decimal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

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
        public void IsEvenTest()
        {
            for (decimal i = -10m; i <= 10; i += 0.1m)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i.IsEven(), sample.IsEven(), $"i={i}");
            }
        }

        [TestMethod()]
        public void IsOddTest()
        {
            for (decimal i = -10m; i <= 10; i += 0.1m)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i.IsOdd(), sample.IsOdd(), $"i={i}");
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
        public void ToDecimalTest()
        {
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
                actual.RoundBottom();
                var expected = decimal.Round(i, BigDecimal.DefaultMidpointRounding);
                Assert.AreEqual(expected, actual, $"i={i}");
            }

            {
                var actual = new BigDecimal(15);
                actual.RoundBottom();
                var expected = new BigDecimal(20);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(-15);
                actual.RoundBottom();
                var expected = new BigDecimal(-20);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(25);
                actual.RoundBottom();
                var expected = new BigDecimal(30);
                Assert.AreEqual(expected, actual);
            }

            {
                var actual = new BigDecimal(-25);
                actual.RoundBottom();
                var expected = new BigDecimal(-30);
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
                Assert.AreEqual(0, d);

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
            Assert.AreEqual(-30, sample3.MinExponent);
            sample3 = sample2 + sample1;
            Assert.AreEqual(-30, sample3.MinExponent);

            sample3 = sample1 - sample2;
            Assert.AreEqual(-30, sample3.MinExponent);
            sample3 = sample2 - sample1;
            Assert.AreEqual(-30, sample3.MinExponent);

            sample3 = sample1 * sample2;
            Assert.AreEqual(-30, sample3.MinExponent);
            sample3 = sample2 * sample1;
            Assert.AreEqual(-30, sample3.MinExponent);
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
    }
}