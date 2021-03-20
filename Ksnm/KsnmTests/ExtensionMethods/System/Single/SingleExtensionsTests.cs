using Microsoft.VisualStudio.TestTools.UnitTesting;
using Float = System.Single;
using UInt = System.UInt32;

namespace Ksnm.ExtensionMethods.System.Single.Tests
{
    [TestClass()]
    public class SingleExtensionsTests
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
        public void ToInt32BitsTest()
        {
            Float sample;
            sample = 0;
            Assert.AreEqual<int>(0, sample.ToInt32Bits());
            sample = Float.Epsilon;
            Assert.AreEqual<int>(1, sample.ToInt32Bits());
        }

        [TestMethod()]
        public void ToUInt32BitsTest()
        {
            Float sample;
            sample = 0;
            Assert.AreEqual<uint>(0, sample.ToUInt32Bits());
            sample = Float.Epsilon;
            Assert.AreEqual<uint>(1, sample.ToUInt32Bits());
        }

        [TestMethod()]
        public void ToDecimalStringTest()
        {
            Float sample = 0;
            Assert.AreEqual("0", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 1;
            Assert.AreEqual("1", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 0.00001f;
            Assert.AreEqual("0.00001", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = Float.Epsilon;
            Assert.AreEqual("0.000000000000000000000000000000000000000000001401298", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
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
            Assert.AreEqual<int>(127, sample.GetExponentBits());
            sample = -1;
            Assert.AreEqual<int>(127, sample.GetExponentBits());

            sample = 2;
            Assert.AreEqual<int>(128, sample.GetExponentBits());
            sample = -2;
            Assert.AreEqual<int>(128, sample.GetExponentBits());

            sample = 0.5f;
            Assert.AreEqual<int>(126, sample.GetExponentBits());
            sample = -0.5f;
            Assert.AreEqual<int>(126, sample.GetExponentBits());
        }

        [TestMethod()]
        public void GetMantissaBitsTest()
        {
            Float sample = 1;
            Assert.AreEqual<uint>(0x0000_0000, sample.GetMantissaBits());
            sample = -1;
            Assert.AreEqual<uint>(0x0000_0000, sample.GetMantissaBits());

            sample = 2;
            Assert.AreEqual<uint>(0x0000_0000, sample.GetMantissaBits());
            sample = -2;
            Assert.AreEqual<uint>(0x0000_0000, sample.GetMantissaBits());

            sample = 3;
            Assert.AreEqual<uint>(0x0040_0000, sample.GetMantissaBits());
            sample = -3;
            Assert.AreEqual<uint>(0x0040_0000, sample.GetMantissaBits());
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
            sample = 0.5f;
            Assert.AreEqual(-1, sample.GetExponent());
            sample = 0.25f;
            Assert.AreEqual(-2, sample.GetExponent());
            sample = 0.125f;
            Assert.AreEqual(-3, sample.GetExponent());
        }

        [TestMethod()]
        public void GetMantissaTest()
        {
            Float sample = 1;
            Assert.AreEqual(0x0080_0000U, sample.GetMantissa());
            sample = -1;
            Assert.AreEqual(0x0080_0000U, sample.GetMantissa());
            sample = 2;
            Assert.AreEqual(0x0080_0000U, sample.GetMantissa());
            sample = -2;
            Assert.AreEqual(0x0080_0000U, sample.GetMantissa());
            sample = 3;
            Assert.AreEqual(0x00C0_0000U, sample.GetMantissa());
            sample = -3;
            Assert.AreEqual(0x00C0_0000U, sample.GetMantissa());
        }

        [TestMethod()]
        public void GetFractionalBitsTest()
        {
            Float sample;
            sample = 0.000f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 0.250f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 0.500f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 0.750f; Assert.AreEqual<UInt>(0x0040_0000u, sample.GetFractionalBits());
            sample = 1.000f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 1.250f; Assert.AreEqual<UInt>(0x0020_0000u, sample.GetFractionalBits());
            sample = 1.500f; Assert.AreEqual<UInt>(0x0040_0000u, sample.GetFractionalBits());
            sample = 1.750f; Assert.AreEqual<UInt>(0x0060_0000u, sample.GetFractionalBits());
            sample = 2.000f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 2.250f; Assert.AreEqual<UInt>(0x0020_0000u, sample.GetFractionalBits());
            sample = 2.500f; Assert.AreEqual<UInt>(0x0040_0000u, sample.GetFractionalBits());
            sample = 2.750f; Assert.AreEqual<UInt>(0x0060_0000u, sample.GetFractionalBits());
            sample = 3.000f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 3.250f; Assert.AreEqual<UInt>(0x0020_0000u, sample.GetFractionalBits());
            sample = 3.500f; Assert.AreEqual<UInt>(0x0040_0000u, sample.GetFractionalBits());
            sample = 3.750f; Assert.AreEqual<UInt>(0x0060_0000u, sample.GetFractionalBits());
            sample = 4.000f; Assert.AreEqual<UInt>(0x0000_0000u, sample.GetFractionalBits());
            sample = 4.250f; Assert.AreEqual<UInt>(0x0020_0000u, sample.GetFractionalBits());
            sample = 4.500f; Assert.AreEqual<UInt>(0x0040_0000u, sample.GetFractionalBits());
            sample = 4.750f; Assert.AreEqual<UInt>(0x0060_0000u, sample.GetFractionalBits());
        }
    }
}