using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ExtendedSingleTests
    {
        [TestMethod()]
        public void PropertyTest()
        {
            // 1.00
            {
                ExtendedSingle extendedSingle = new ExtendedSingle();
                extendedSingle.Value = 1.0f;
                Assert.AreEqual(0b0_01111111_00000000000000000000000u, extendedSingle.Bits);
                Assert.AreEqual(0, extendedSingle.SignBit);
                Assert.AreEqual(+1, extendedSingle.Sign);
                Assert.AreEqual(0b01111111, extendedSingle.ExponentBits);
                Assert.AreEqual(-23, extendedSingle.Exponent);
                Assert.AreEqual(0b_00000000000000000000000u, extendedSingle.MantissaBits);
                Assert.AreEqual(0b100000000000000000000000u, extendedSingle.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedSingle.Radix, extendedSingle.Exponent), extendedSingle.Scale);
            }
            // -1.00
            {
                ExtendedSingle extendedSingle = new ExtendedSingle();
                extendedSingle.Value = -1.0f;
                Assert.AreEqual(0b1_01111111_00000000000000000000000u, extendedSingle.Bits);
                Assert.AreEqual(1, extendedSingle.SignBit);
                Assert.AreEqual(-1, extendedSingle.Sign);
                Assert.AreEqual(0b01111111, extendedSingle.ExponentBits);
                Assert.AreEqual(-23, extendedSingle.Exponent);
                Assert.AreEqual(0b_00000000000000000000000u, extendedSingle.MantissaBits);
                Assert.AreEqual(0b100000000000000000000000u, extendedSingle.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedSingle.Radix, extendedSingle.Exponent), extendedSingle.Scale);
            }
            // 1.23
            {
                ExtendedSingle extendedSingle = new ExtendedSingle();
                extendedSingle.Value = 1.23f;
                Assert.AreEqual(0b0_01111111_00111010111000010100100u, extendedSingle.Bits);
                Assert.AreEqual(0, extendedSingle.SignBit);
                Assert.AreEqual(+1, extendedSingle.Sign);
                Assert.AreEqual(0b01111111, extendedSingle.ExponentBits);
                Assert.AreEqual(-23, extendedSingle.Exponent);
                Assert.AreEqual(0b_00111010111000010100100u, extendedSingle.MantissaBits);
                Assert.AreEqual(0b100111010111000010100100u, extendedSingle.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedSingle.Radix, extendedSingle.Exponent), extendedSingle.Scale);
            }
            // 2.00
            {
                ExtendedSingle extendedSingle = new ExtendedSingle();
                extendedSingle.Value = 2.0f;
                Assert.AreEqual(0b0_10000000_00000000000000000000000u, extendedSingle.Bits);
                Assert.AreEqual(0, extendedSingle.SignBit);
                Assert.AreEqual(+1, extendedSingle.Sign);
                Assert.AreEqual(0b10000000, extendedSingle.ExponentBits);
                Assert.AreEqual(-22, extendedSingle.Exponent);
                Assert.AreEqual(0b_00000000000000000000000u, extendedSingle.MantissaBits);
                Assert.AreEqual(0b100000000000000000000000u, extendedSingle.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedSingle.Radix, extendedSingle.Exponent), extendedSingle.Scale);
            }
            // 4.00
            {
                ExtendedSingle extendedSingle = new ExtendedSingle();
                extendedSingle.Value = 4.0f;
                Assert.AreEqual(0b0_10000001_00000000000000000000000u, extendedSingle.Bits);
                Assert.AreEqual(0, extendedSingle.SignBit);
                Assert.AreEqual(+1, extendedSingle.Sign);
                Assert.AreEqual(0b10000001, extendedSingle.ExponentBits);
                Assert.AreEqual(-21, extendedSingle.Exponent);
                Assert.AreEqual(0b_00000000000000000000000u, extendedSingle.MantissaBits);
                Assert.AreEqual(0b100000000000000000000000u, extendedSingle.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedSingle.Radix, extendedSingle.Exponent), extendedSingle.Scale);
            }
        }
        [TestMethod()]
        public void SetTest()
        {
            ExtendedSingle extendedSingle = new ExtendedSingle();
            extendedSingle.Value = 0;

            extendedSingle.SignBit = 0;
            Assert.AreEqual(0, extendedSingle.SignBit);
            Assert.AreEqual(0, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);
            extendedSingle.SignBit = 1;
            Assert.AreEqual(1, extendedSingle.SignBit);
            Assert.AreEqual(0, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);

            extendedSingle.ExponentBits = 0;
            Assert.AreEqual(1, extendedSingle.SignBit);
            Assert.AreEqual(0, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);
            extendedSingle.ExponentBits = 0b11111111;
            Assert.AreEqual(1, extendedSingle.SignBit);
            Assert.AreEqual(0b11111111, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);

            extendedSingle.MantissaBits = 0;
            Assert.AreEqual(1, extendedSingle.SignBit);
            Assert.AreEqual(0b11111111, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);
            extendedSingle.MantissaBits = 0b111_1111111111_1111111111u;
            Assert.AreEqual(1, extendedSingle.SignBit);
            Assert.AreEqual(0b11111111, extendedSingle.ExponentBits);
            Assert.AreEqual(0b111_1111111111_1111111111u, extendedSingle.MantissaBits);

            extendedSingle.SignBit = 0;
            Assert.AreEqual(0, extendedSingle.SignBit);
            Assert.AreEqual(0b11111111, extendedSingle.ExponentBits);
            Assert.AreEqual(0b111_1111111111_1111111111u, extendedSingle.MantissaBits);
            extendedSingle.ExponentBits = 0;
            Assert.AreEqual(0, extendedSingle.SignBit);
            Assert.AreEqual(0, extendedSingle.ExponentBits);
            Assert.AreEqual(0b111_1111111111_1111111111u, extendedSingle.MantissaBits);
            extendedSingle.MantissaBits = 0;
            Assert.AreEqual(0, extendedSingle.SignBit);
            Assert.AreEqual(0, extendedSingle.ExponentBits);
            Assert.AreEqual(0ul, extendedSingle.MantissaBits);
        }
    }
}