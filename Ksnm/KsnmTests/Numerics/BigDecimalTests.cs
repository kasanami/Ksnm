using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            // 切り上げ
            for (decimal i = 0.5m; i < 1.0m; i+= 0.1m)
            {
                var actual = new BigDecimal(i);
                actual.RoundBottom();
                var expected = new BigDecimal(1.0m);
                Assert.AreEqual(expected, actual);
            }
            // 切り下げ
            for (decimal i = 0.0m; i < 0.5m; i += 0.1m)
            {
                var actual = new BigDecimal(i);
                actual.RoundBottom();
                var expected = new BigDecimal(0.0m);
                Assert.AreEqual(expected, actual);
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

            for (decimal i = 0m; i < 10; i++)
            {
                var sample = new BigDecimal(i);
                for (decimal j = 1; j < 10; j++)
                {
                    var sample2 = new BigDecimal(j);
                    // +
                    Assert.AreEqual(i + j, (sample + sample2).ToDecimal());
                    // -
                    Assert.AreEqual(i - j, (sample - sample2).ToDecimal());
                    // *
                    Assert.AreEqual(i * j, (sample * sample2).ToDecimal());
                    // /
                    Assert.AreEqual(i / j, (sample / sample2).ToDecimal());
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
    }
}