using Ksnm.ExtensionMethods.System.Decimal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static System.Math;

namespace Ksnm.ExtensionMethods.System.Decimal.Tests
{
    using Int32 = global::System.Int32;

    [TestClass()]
    public class DecimalExtensionsTests
    {
        [TestMethod()]
        public void IsPositiveTest()
        {
            decimal sample = 1;
            Assert.IsTrue(sample.IsPositive());
            sample = -1;
            Assert.IsFalse(sample.IsPositive());
        }

        [TestMethod()]
        public void IsNegativeTest()
        {
            decimal sample = 1;
            Assert.IsFalse(sample.IsNegative());
            sample = -1;
            Assert.IsTrue(sample.IsNegative());
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
            // 以下はtrueになる
            decimal sample = 1;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MaxValue;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MinValue;
            Assert.IsTrue(sample.IsInteger());
            sample = short.MaxValue;
            Assert.IsTrue(sample.IsInteger());
            sample = short.MinValue;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MaxValue;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MinValue;
            Assert.IsTrue(sample.IsInteger());
            for (int i = 1; i < 100000; i *= 10)
            {
                sample = i; Assert.IsTrue(sample.IsInteger(), $"i={i}");
            }
            // 以下は少数を含むためfalse
            sample = 1.1m;
            Assert.IsFalse(sample.IsInteger());
            sample = 0.1m;
            Assert.IsFalse(sample.IsInteger());
            sample = -0.1m;
            Assert.IsFalse(sample.IsInteger());
            for (int i = 1; i < 100000; i *= 10)
            {
                sample = i + 0.1m; Assert.IsFalse(sample.IsInteger(), $"i={i}");
            }
        }

        [TestMethod()]
        public void IsEvenTest()
        {
            // 以下はtrueになる
            decimal sample = 0;
            Assert.IsTrue(sample.IsEven());
            sample = 2;
            Assert.IsTrue(sample.IsEven());
            sample = -2;
            Assert.IsTrue(sample.IsEven());

            // 以下はfalseになる
            sample = 1;
            Assert.IsFalse(sample.IsEven());
            sample = 1.2m;
            Assert.IsFalse(sample.IsEven());
            sample = -1m;
            Assert.IsFalse(sample.IsEven());
        }

        [TestMethod()]
        public void IsOddTest()
        {
            // 以下はfalseになる
            decimal sample = 0;
            Assert.IsFalse(sample.IsOdd());
            sample = 2;
            Assert.IsFalse(sample.IsOdd());
            sample = -2;
            Assert.IsFalse(sample.IsOdd());

            // 以下はtrueになる
            sample = 1;
            Assert.IsTrue(sample.IsOdd());
            sample = 1.2m;
            Assert.IsTrue(sample.IsOdd());
            sample = -1m;
            Assert.IsTrue(sample.IsOdd());
        }

        [TestMethod()]
        public void ToClampedSByteTest()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((sbyte)sample, sample.ToClampedSByte());
            Assert.AreEqual(sbyte.MinValue, minSample.ToClampedSByte());
            Assert.AreEqual(sbyte.MaxValue, maxSample.ToClampedSByte());
        }

        [TestMethod()]
        public void ToClampedByteTest()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((byte)sample, sample.ToClampedByte());
            Assert.AreEqual(byte.MinValue, minSample.ToClampedByte());
            Assert.AreEqual(byte.MaxValue, maxSample.ToClampedByte());
        }

        [TestMethod()]
        public void ToClampedInt16Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int16)sample, sample.ToClampedInt16());
            Assert.AreEqual(Int16.MinValue, minSample.ToClampedInt16());
            Assert.AreEqual(Int16.MaxValue, maxSample.ToClampedInt16());
        }

        [TestMethod()]
        public void ToClampedUInt16Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt16)sample, sample.ToClampedUInt16());
            Assert.AreEqual(UInt16.MinValue, minSample.ToClampedUInt16());
            Assert.AreEqual(UInt16.MaxValue, maxSample.ToClampedUInt16());
        }

        [TestMethod()]
        public void ToClampedInt32Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int32)sample, sample.ToClampedInt32());
            Assert.AreEqual(Int32.MinValue, minSample.ToClampedInt32());
            Assert.AreEqual(Int32.MaxValue, maxSample.ToClampedInt32());
        }

        [TestMethod()]
        public void ToClampedUInt32Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt32)sample, sample.ToClampedUInt32());
            Assert.AreEqual(UInt32.MinValue, minSample.ToClampedUInt32());
            Assert.AreEqual(UInt32.MaxValue, maxSample.ToClampedUInt32());
        }

        [TestMethod()]
        public void ToClampedInt64Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int64)sample, sample.ToClampedInt64());
            Assert.AreEqual(Int64.MinValue, minSample.ToClampedInt64());
            Assert.AreEqual(Int64.MaxValue, maxSample.ToClampedInt64());
        }

        [TestMethod()]
        public void ToClampedUInt64Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt64)sample, sample.ToClampedUInt64());
            Assert.AreEqual(UInt64.MinValue, minSample.ToClampedUInt64());
            Assert.AreEqual(UInt64.MaxValue, maxSample.ToClampedUInt64());
        }

        [TestMethod()]
        public void GetSignBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0, sample.GetSignBits());
            sample = -1;
            Assert.AreEqual(1, sample.GetSignBits());
        }

        [TestMethod()]
        public void GetSignTest()
        {
            decimal sample = 1;
            Assert.AreEqual(+1, sample.GetSign());
            sample = -1;
            Assert.AreEqual(-1, sample.GetSign());
        }

        [TestMethod()]
        public void GetExponentBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0, sample.GetExponentBits());
            sample = -1;
            Assert.AreEqual(0, sample.GetExponentBits());

            sample = 2;
            Assert.AreEqual(0, sample.GetExponentBits());
            sample = -2;
            Assert.AreEqual(0, sample.GetExponentBits());

            sample = 0.5m;
            Assert.AreEqual(1, sample.GetExponentBits());
            sample = -0.5m;
            Assert.AreEqual(1, sample.GetExponentBits());
        }

        [TestMethod()]
        public void GetExponentTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = -1;
            Assert.AreEqual(0, sample.GetExponent());

            sample = 2;
            Assert.AreEqual(0, sample.GetExponent());
            sample = -2;
            Assert.AreEqual(0, sample.GetExponent());

            sample = 0.5m;
            Assert.AreEqual(-1, sample.GetExponent());
            sample = -0.5m;
            Assert.AreEqual(-1, sample.GetExponent());

            sample = 0.01m;
            Assert.AreEqual(-2, sample.GetExponent());
            sample = -0.01m;
            Assert.AreEqual(-2, sample.GetExponent());
        }

        [TestMethod()]
        public void GetMantissaBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(1, sample.GetMantissaBits()[0]);
            sample = -1;
            Assert.AreEqual(1, sample.GetMantissaBits()[0]);

            sample = 2;
            Assert.AreEqual(2, sample.GetMantissaBits()[0]);
            sample = -2;
            Assert.AreEqual(2, sample.GetMantissaBits()[0]);

            sample = 3;
            Assert.AreEqual(3, sample.GetMantissaBits()[0]);
            sample = -3;
            Assert.AreEqual(3, sample.GetMantissaBits()[0]);

            sample = 0.5m;
            Assert.AreEqual(5, sample.GetMantissaBits()[0]);
            sample = -0.5m;
            Assert.AreEqual(5, sample.GetMantissaBits()[0]);

            sample = 0.123m;
            Assert.AreEqual(123, sample.GetMantissaBits()[0]);
            sample = -0.123m;
            Assert.AreEqual(123, sample.GetMantissaBits()[0]);
        }

        [TestMethod()]
        public void GetMantissaTest()
        {
            decimal sample = 1;
            Assert.AreEqual(1, sample.GetMantissa());
            sample = -1;
            Assert.AreEqual(1, sample.GetMantissa());

            sample = 2;
            Assert.AreEqual(2, sample.GetMantissa());
            sample = -2;
            Assert.AreEqual(2, sample.GetMantissa());

            sample = 3;
            Assert.AreEqual(3, sample.GetMantissa());
            sample = -3;
            Assert.AreEqual(3, sample.GetMantissa());

            sample = 0.5m;
            Assert.AreEqual(5, sample.GetMantissa());
            sample = -0.5m;
            Assert.AreEqual(5, sample.GetMantissa());

            sample = 0.123m;
            Assert.AreEqual(123, sample.GetMantissa());
            sample = -0.123m;
            Assert.AreEqual(123, sample.GetMantissa());
        }

        [TestMethod()]
        public void ToHexadecimalStringTest()
        {
            decimal sample;
            sample = 0m; Assert.AreEqual("00000000_00000000_00000000_00000000", sample.ToHexadecimalString("_"));
            sample = 0.125m; Assert.AreEqual("00030000_00000000_00000000_0000007D", sample.ToHexadecimalString("_"));
            sample = 0.25m; Assert.AreEqual("00020000_00000000_00000000_00000019", sample.ToHexadecimalString("_"));
            sample = 0.5m; Assert.AreEqual("00010000_00000000_00000000_00000005", sample.ToHexadecimalString("_"));
            sample = 000001m; Assert.AreEqual("00000000_00000000_00000000_00000001", sample.ToHexadecimalString("_"));
            sample = 000002m; Assert.AreEqual("00000000_00000000_00000000_00000002", sample.ToHexadecimalString("_"));
            sample = 000004m; Assert.AreEqual("00000000_00000000_00000000_00000004", sample.ToHexadecimalString("_"));
            sample = 000008m; Assert.AreEqual("00000000_00000000_00000000_00000008", sample.ToHexadecimalString("_"));
            sample = 000016m; Assert.AreEqual("00000000_00000000_00000000_00000010", sample.ToHexadecimalString("_"));
            sample = 000032m; Assert.AreEqual("00000000_00000000_00000000_00000020", sample.ToHexadecimalString("_"));
            sample = 000064m; Assert.AreEqual("00000000_00000000_00000000_00000040", sample.ToHexadecimalString("_"));
            sample = 000128m; Assert.AreEqual("00000000_00000000_00000000_00000080", sample.ToHexadecimalString("_"));
            sample = 000256m; Assert.AreEqual("00000000_00000000_00000000_00000100", sample.ToHexadecimalString("_"));
            sample = 000512m; Assert.AreEqual("00000000_00000000_00000000_00000200", sample.ToHexadecimalString("_"));
            sample = 001024m; Assert.AreEqual("00000000_00000000_00000000_00000400", sample.ToHexadecimalString("_"));
            sample = 002048m; Assert.AreEqual("00000000_00000000_00000000_00000800", sample.ToHexadecimalString("_"));
            sample = 004096m; Assert.AreEqual("00000000_00000000_00000000_00001000", sample.ToHexadecimalString("_"));
            sample = 008192m; Assert.AreEqual("00000000_00000000_00000000_00002000", sample.ToHexadecimalString("_"));
            sample = 016384m; Assert.AreEqual("00000000_00000000_00000000_00004000", sample.ToHexadecimalString("_"));
            sample = 032768m; Assert.AreEqual("00000000_00000000_00000000_00008000", sample.ToHexadecimalString("_"));
            sample = 065536m; Assert.AreEqual("00000000_00000000_00000000_00010000", sample.ToHexadecimalString("_"));
            sample = 131072m; Assert.AreEqual("00000000_00000000_00000000_00020000", sample.ToHexadecimalString("_"));
            sample = 262144m; Assert.AreEqual("00000000_00000000_00000000_00040000", sample.ToHexadecimalString("_"));
            sample = 524288m; Assert.AreEqual("00000000_00000000_00000000_00080000", sample.ToHexadecimalString("_"));
        }

        [TestMethod()]
        public void LogTest()
        {
            const int Count = 100000;

            var expecteds = new[]
            {
                -1m   ,// 0
                0m   ,// 1
                0.6931418055994530941723212145817656807550013436026m   ,// 2
                1.098612886681096913952452369225257046474905578228m    ,// 3
                1.386293611198906188344642429163531361510002687205m    ,// 4
                1.609439124341003746007593332261876395256013542685m    ,// 5
                1.79175469228055000812477358380702272722990692183m     ,// 6
                1.945911490553133051053527434431797296370847295819m    ,// 7
                2.079445416798359282516963643745297042265004030808m    ,// 8
                2.197225773362193827904904738450514092949811156455m    ,// 9
                2.3025850929940456840179914546843642076011014886288m    ,// 10
            };

            for (int i = 0; i < expecteds.Length; i++)
            {
                var expected = expecteds[i];
                expected = Round(expected, 4, MidpointRounding.AwayFromZero);
                var actual = ((decimal)i).Log(Count);
                actual = Round(actual, 4, MidpointRounding.AwayFromZero);
                Assert.AreEqual(expected, actual, $"log{i}");
            }
        }

        [TestMethod()]
        public void RoundBottomTest()
        {
            {
                var sample = 1.9999999999999999999999999999m;
                var actual = sample.RoundBottom();
                var expected = 2m;
                Assert.AreEqual(expected, actual);
            }
            {
                var sample = 19.999999999999999999999999999m;
                var actual = sample.RoundBottom();
                var expected = 20m;
                Assert.AreEqual(expected, actual);
            }
            {
                var sample = 1m;
                var actual = sample.RoundBottom();
                var expected = 1m;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
