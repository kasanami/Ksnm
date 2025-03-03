using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ExtendedDoubleTests
    {
        [TestMethod()]
        public void PropertyTest()
        {
            // 1.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 1.0;
                Assert.AreEqual(0b0_01111111111_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(0u, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111u, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDouble.Radix, extendedDouble.Exponent - ExtendedDouble.MantissaLength), extendedDouble.Scale);
            }
            // -1.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = -1.0;
                Assert.AreEqual(0b1_01111111111_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(1u, extendedDouble.SignBit);
                Assert.AreEqual(-1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111u, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDouble.Radix, extendedDouble.Exponent - ExtendedDouble.MantissaLength), extendedDouble.Scale);
            }
            // 1.23
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 1.23;
                Assert.AreEqual(0b0_01111111111_0011101011100001010001111010111000010100011110101110ul, extendedDouble.Bits);
                Assert.AreEqual(0u, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111u, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0011101011100001010001111010111000010100011110101110ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10011101011100001010001111010111000010100011110101110ul, extendedDouble.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDouble.Radix, extendedDouble.Exponent - ExtendedDouble.MantissaLength), extendedDouble.Scale);
            }
            // 2.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 2.0;
                Assert.AreEqual(0b0_10000000000_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(0u, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b10000000000u, extendedDouble.ExponentBits);
                Assert.AreEqual(1, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDouble.Radix, extendedDouble.Exponent - ExtendedDouble.MantissaLength), extendedDouble.Scale);
            }
            // 4.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 4.0;
                Assert.AreEqual(0b0_10000000001_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(0u, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b10000000001u, extendedDouble.ExponentBits);
                Assert.AreEqual(2, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDouble.Radix, extendedDouble.Exponent - ExtendedDouble.MantissaLength), extendedDouble.Scale);
            }
        }
        [TestMethod()]
        public void SetTest()
        {
            ExtendedDouble extendedDouble = new ExtendedDouble();
            extendedDouble.Value = 0;

            extendedDouble.SignBit = 0;
            Assert.AreEqual(0ul, extendedDouble.SignBit);
            Assert.AreEqual(0ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);
            extendedDouble.SignBit = 1;
            Assert.AreEqual(1ul, extendedDouble.SignBit);
            Assert.AreEqual(0ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);

            extendedDouble.ExponentBits = 0;
            Assert.AreEqual(1ul, extendedDouble.SignBit);
            Assert.AreEqual(0ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);
            extendedDouble.ExponentBits = 0b11111111111;
            Assert.AreEqual(1ul, extendedDouble.SignBit);
            Assert.AreEqual(0b11111111111ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);

            extendedDouble.MantissaBits = 0;
            Assert.AreEqual(1ul, extendedDouble.SignBit);
            Assert.AreEqual(0b11111111111ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);
            extendedDouble.MantissaBits = 0b1111_1111111111111111_1111111111111111_1111111111111111;
            Assert.AreEqual(1ul, extendedDouble.SignBit);
            Assert.AreEqual(0b11111111111ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0b1111_1111111111111111_1111111111111111_1111111111111111ul, extendedDouble.MantissaBits);

            extendedDouble.SignBit = 0;
            Assert.AreEqual(0ul, extendedDouble.SignBit);
            Assert.AreEqual(0b11111111111ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0b1111_1111111111111111_1111111111111111_1111111111111111ul, extendedDouble.MantissaBits);
            extendedDouble.ExponentBits = 0;
            Assert.AreEqual(0ul, extendedDouble.SignBit);
            Assert.AreEqual(0ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0b1111_1111111111111111_1111111111111111_1111111111111111ul, extendedDouble.MantissaBits);
            extendedDouble.MantissaBits = 0;
            Assert.AreEqual(0ul, extendedDouble.SignBit);
            Assert.AreEqual(0ul, extendedDouble.ExponentBits);
            Assert.AreEqual(0ul, extendedDouble.MantissaBits);
        }
    }
}