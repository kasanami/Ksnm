using Microsoft.VisualStudio.TestTools.UnitTesting;
using Float = System.Double;
using UInt = System.UInt64;

namespace Ksnm.ExtensionMethods.System.Double.Tests
{
    [TestClass()]
    public class DoubleExtensionsTests
    {
        [TestMethod()]
        public void IsPositiveTest()
        {
            Float sample = 1;
            Assert.IsTrue(sample.IsPositive());
            sample = -1;
            Assert.IsFalse(sample.IsPositive());
        }

        [TestMethod()]
        public void IsNegativeTest()
        {
            Float sample = 1;
            Assert.IsFalse(sample.IsNegative());
            sample = -1;
            Assert.IsTrue(sample.IsNegative());
        }

        [TestMethod()]
        public void IsInfinityTest()
        {
            Assert.IsTrue(Float.PositiveInfinity.IsInfinity());
            Assert.IsTrue(Float.NegativeInfinity.IsInfinity());
            Float sample = 1;
            Assert.IsFalse(sample.IsInfinity());
            sample = 0;
            Assert.IsFalse(sample.IsInfinity());
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
            // 以下はtrueになる
            Float sample = 0f;
            Assert.IsTrue(sample.IsInteger());
            sample = -0f;
            Assert.IsTrue(sample.IsInteger());
            sample = 1f;
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
            // 以下は小数を含むためfalse
            sample = 1.1f;
            Assert.IsFalse(sample.IsInteger());
            sample = 0.1f;
            Assert.IsFalse(sample.IsInteger());
            sample = -0.1f;
            Assert.IsFalse(sample.IsInteger());
            sample = Float.Epsilon;
            Assert.IsFalse(sample.IsInteger());
            for (int i = 1; i < 100000; i *= 10)
            {
                sample = i + 0.1f; Assert.IsFalse(sample.IsInteger(), $"i={i}");
            }
        }

        [TestMethod()]
        public void ToInt64BitsTest()
        {
            Float sample;
            sample = 0;
            Assert.AreEqual<long>(0, sample.ToInt64Bits());
            sample = Float.Epsilon;
            Assert.AreEqual<long>(1, sample.ToInt64Bits());
        }

        [TestMethod()]
        public void ToUInt64BitsTest()
        {
            Float sample;
            sample = 0;
            Assert.AreEqual<ulong>(0, sample.ToUInt64Bits());
            sample = Float.Epsilon;
            Assert.AreEqual<ulong>(1, sample.ToUInt64Bits());
        }

        [TestMethod()]
        public void ToDecimalStringTest()
        {
            Float sample = 0;
            Assert.AreEqual("0", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 1;
            Assert.AreEqual("1", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 0.00001;
            Assert.AreEqual("0.00001", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = Float.Epsilon;
            Assert.AreEqual("0.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000494065645841247", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
        }

        [TestMethod()]
        public void GetSignBitsTest()
        {
            Float sample = 1;
            Assert.AreEqual<int>(0, sample.GetSignBits());
            sample = -1;
            Assert.AreEqual<int>(1, sample.GetSignBits());
        }

        [TestMethod()]
        public void GetExponentBitsTest()
        {
            Float sample = 1;
            Assert.AreEqual<int>(1023, sample.GetExponentBits());
            sample = -1;
            Assert.AreEqual<int>(1023, sample.GetExponentBits());

            sample = 2;
            Assert.AreEqual<int>(1024, sample.GetExponentBits());
            sample = -2;
            Assert.AreEqual<int>(1024, sample.GetExponentBits());

            sample = 0.5;
            Assert.AreEqual<int>(1022, sample.GetExponentBits());
            sample = -0.5;
            Assert.AreEqual<int>(1022, sample.GetExponentBits());
        }

        [TestMethod()]
        public void GetMantissaBitsTest()
        {
            Float sample = 1;
            Assert.AreEqual<ulong>(0x0000_0000_0000_0000, sample.GetMantissaBits());
            sample = -1;
            Assert.AreEqual<ulong>(0x0000_0000_0000_0000, sample.GetMantissaBits());

            sample = 2;
            Assert.AreEqual<ulong>(0x0000_0000_0000_00000, sample.GetMantissaBits());
            sample = -2;
            Assert.AreEqual<ulong>(0x0000_0000_0000_0000, sample.GetMantissaBits());

            sample = 3;
            Assert.AreEqual<ulong>(0x0008_0000_0000_0000, sample.GetMantissaBits());
            sample = -3;
            Assert.AreEqual<ulong>(0x0008_0000_0000_0000, sample.GetMantissaBits());
        }

        [TestMethod()]
        public void GetSignTest()
        {
            Float sample = 1;
            Assert.AreEqual(+1, sample.GetSign());
            sample = -1;
            Assert.AreEqual(-1, sample.GetSign());
        }

        [TestMethod()]
        public void GetExponentTest()
        {
            Float sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = 2;
            Assert.AreEqual(1, sample.GetExponent());
            sample = 4;
            Assert.AreEqual(2, sample.GetExponent());
            sample = 8;
            Assert.AreEqual(3, sample.GetExponent());

            sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = 0.5;
            Assert.AreEqual(-1, sample.GetExponent());
            sample = 0.25;
            Assert.AreEqual(-2, sample.GetExponent());
            sample = 0.125;
            Assert.AreEqual(-3, sample.GetExponent());
        }

        [TestMethod()]
        public void GetMantissaTest()
        {
            Float sample = 1;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = -1;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = 2;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = -2;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = 3;
            Assert.AreEqual(0x0018_0000_0000_0000UL, sample.GetMantissa());
            sample = -3;
            Assert.AreEqual(0x0018_0000_0000_0000UL, sample.GetMantissa());
        }

        [TestMethod()]
        public void GetFractionalBitsTest()
        {
            Float sample;
            sample = 0.000; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 0.250; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 0.500; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 0.750; Assert.AreEqual<UInt>(0x0008_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 1.000; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 1.250; Assert.AreEqual<UInt>(0x0004_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 1.500; Assert.AreEqual<UInt>(0x0008_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 1.750; Assert.AreEqual<UInt>(0x000C_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 2.000; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 2.250; Assert.AreEqual<UInt>(0x0004_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 2.500; Assert.AreEqual<UInt>(0x0008_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 2.750; Assert.AreEqual<UInt>(0x000C_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 3.000; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 3.250; Assert.AreEqual<UInt>(0x0004_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 3.500; Assert.AreEqual<UInt>(0x0008_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 3.750; Assert.AreEqual<UInt>(0x000C_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 4.000; Assert.AreEqual<UInt>(0x0000_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 4.250; Assert.AreEqual<UInt>(0x0004_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 4.500; Assert.AreEqual<UInt>(0x0008_0000_0000_0000ul, sample.GetFractionalBits());
            sample = 4.750; Assert.AreEqual<UInt>(0x000C_0000_0000_0000ul, sample.GetFractionalBits());
        }
    }
}