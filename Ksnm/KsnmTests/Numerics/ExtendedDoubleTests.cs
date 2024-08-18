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
                Assert.AreEqual(0, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
            }
            // 1.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = -1.0;
                Assert.AreEqual(0b1_01111111111_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(1, extendedDouble.SignBit);
                Assert.AreEqual(-1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
            }
            // 1.23
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 1.23;
                Assert.AreEqual(0b0_01111111111_0011101011100001010001111010111000010100011110101110ul, extendedDouble.Bits);
                Assert.AreEqual(0, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b01111111111, extendedDouble.ExponentBits);
                Assert.AreEqual(0, extendedDouble.Exponent);
                Assert.AreEqual(0b0011101011100001010001111010111000010100011110101110ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10011101011100001010001111010111000010100011110101110ul, extendedDouble.Mantissa);
            }
            // 2.00
            {
                ExtendedDouble extendedDouble = new ExtendedDouble();
                extendedDouble.Value = 2.0;
                Assert.AreEqual(0b0_10000000000_0000000000000000000000000000000000000000000000000000ul, extendedDouble.Bits);
                Assert.AreEqual(0, extendedDouble.SignBit);
                Assert.AreEqual(+1, extendedDouble.Sign);
                Assert.AreEqual(0b10000000000, extendedDouble.ExponentBits);
                Assert.AreEqual(1, extendedDouble.Exponent);
                Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000ul, extendedDouble.MantissaBits);
                Assert.AreEqual(0b10000000000000000000000000000000000000000000000000000ul, extendedDouble.Mantissa);
            }
        }
    }
}