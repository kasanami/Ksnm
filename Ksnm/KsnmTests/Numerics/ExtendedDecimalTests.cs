using Ksnm.ExtensionMethods.System.Decimal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Formats.Asn1.AsnWriter;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ExtendedDecimalTests
    {
        [TestMethod()]
        public void ConstantTest()
        {
            Assert.AreEqual<decimal>(1, ExtendedDecimal.One);
            Assert.AreEqual<decimal>(0, ExtendedDecimal.Zero);
            Assert.AreEqual<decimal>(0.0000000000_0000000000_00000001m, ExtendedDecimal.Epsilon);
        }

        [TestMethod()]
        public void PropertyTest()
        {
            // 
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = new decimal(0x03020100, 0x07060504, 0x0B0A0908, true, 0);
                var str = extendedDecimal.Bits.ToString("X32");
                Assert.AreEqual<uint>(0x80000000, extendedDecimal.Flags);
                Assert.AreEqual<uint>(0x0B0A0908, extendedDecimal.HiBits);
                Assert.AreEqual<uint>(0x07060504, extendedDecimal.MidBits);
                Assert.AreEqual<uint>(0x03020100, extendedDecimal.LoBits);
                Assert.AreEqual(1, extendedDecimal.SignBit);
                Assert.AreEqual(-1, extendedDecimal.Sign);
                Assert.AreEqual(0, extendedDecimal.ExponentBits);
                Assert.AreEqual(0, extendedDecimal.Exponent);
                Assert.AreEqual("0B0A09080706050403020100", extendedDecimal.MantissaBits.ToString("X24"));
                Assert.AreEqual("0B0A09080706050403020100", extendedDecimal.Mantissa.ToString("X24"));
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // 1
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = 1m;
                Assert.AreEqual(0, extendedDecimal.SignBit);
                Assert.AreEqual(+1, extendedDecimal.Sign);
                Assert.AreEqual(0, extendedDecimal.ExponentBits);
                Assert.AreEqual(0, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(1, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(1, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // 1.0
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = 1.0m;
                Assert.AreEqual(0, extendedDecimal.SignBit);
                Assert.AreEqual(+1, extendedDecimal.Sign);
                Assert.AreEqual(1, extendedDecimal.ExponentBits);
                Assert.AreEqual(-1, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(10, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(10, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // -1.0
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = -1.0m;
                Assert.AreEqual(1, extendedDecimal.SignBit);
                Assert.AreEqual(-1, extendedDecimal.Sign);
                Assert.AreEqual(1, extendedDecimal.ExponentBits);
                Assert.AreEqual(-1, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(10, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(10, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // 1.23
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = 1.23m;
                Assert.AreEqual(0, extendedDecimal.SignBit);
                Assert.AreEqual(+1, extendedDecimal.Sign);
                Assert.AreEqual(2, extendedDecimal.ExponentBits);
                Assert.AreEqual(-2, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(123, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(123, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // -1.23
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = -1.23m;
                Assert.AreEqual(1, extendedDecimal.SignBit);
                Assert.AreEqual(-1, extendedDecimal.Sign);
                Assert.AreEqual(2, extendedDecimal.ExponentBits);
                Assert.AreEqual(-2, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(123, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(123, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // 10
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = 10m;
                Assert.AreEqual(0, extendedDecimal.SignBit);
                Assert.AreEqual(+1, extendedDecimal.Sign);
                Assert.AreEqual(0, extendedDecimal.ExponentBits);
                Assert.AreEqual(0, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(10, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(10, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
            // 100
            {
                ExtendedDecimal extendedDecimal = new ExtendedDecimal();
                extendedDecimal = 100m;
                Assert.AreEqual(0, extendedDecimal.SignBit);
                Assert.AreEqual(+1, extendedDecimal.Sign);
                Assert.AreEqual(0, extendedDecimal.ExponentBits);
                Assert.AreEqual(0, extendedDecimal.Exponent);
                Assert.AreEqual<UInt128>(100, extendedDecimal.MantissaBits);
                Assert.AreEqual<UInt128>(100, extendedDecimal.Mantissa);
                Assert.AreEqual(System.Math.Pow(ExtendedDecimal.Radix, extendedDecimal.Exponent), extendedDecimal.Scale);
            }
        }
        [TestMethod()]
        public void AbsTest()
        {
            ExtendedDecimal expected = 123m;
            ExtendedDecimal actual = ExtendedDecimal.Abs(-123m);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ExpTest()
        {
            //ExtendedDecimal expected = 7.38905609893m;
            //ExtendedDecimal actual = ExtendedDecimal.Exp(2);
            //Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Exp10Test()
        {
            //{
            //    ExtendedDecimal expected = 10m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp10(1);
            //    Assert.AreEqual(expected, actual);
            //}
            //{
            //    ExtendedDecimal expected = 100m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp10(2);
            //    Assert.AreEqual(expected, actual);
            //}
            //{
            //    ExtendedDecimal expected = 1000m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp10(3);
            //    Assert.AreEqual(expected, actual);
            //}
        }
        [TestMethod()]
        public void Exp2Test()
        {
            //{
            //    ExtendedDecimal expected = 2m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp2(1);
            //    Assert.AreEqual(expected, actual);
            //}
            //{
            //    ExtendedDecimal expected = 4m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp2(2);
            //    Assert.AreEqual(expected, actual);
            //}
            //{
            //    ExtendedDecimal expected = 8m;
            //    ExtendedDecimal actual = ExtendedDecimal.Exp2(3);
            //    Assert.AreEqual(expected, actual);
            //}
        }
        [TestMethod()]
        public void AcoshTest()
        {
        }
        [TestMethod()]
        public void AsinhTest()
        {
        }
        [TestMethod()]
        public void AtanhTest()
        {
        }
        [TestMethod()]
        public void CoshTest()
        {
        }
        [TestMethod()]
        public void SinhTest()
        {
        }
        [TestMethod()]
        public void TanhTest()
        {
        }
        [TestMethod()]
        public void LogTest()
        {
        }
        [TestMethod()]
        public void Log10Test()
        {
            ExtendedDecimal f10 = 10;
            ExtendedDecimal f100 = 100;
            ExtendedDecimal f1000 = 1000;
            var log10_10 = ExtendedDecimal.Log10(f10);
            var log10_100 = ExtendedDecimal.Log10(f100);
            var log10_1000 = ExtendedDecimal.Log10(f1000);
            Assert.AreEqual(1, log10_10);
            Assert.AreEqual(2, log10_100, 0.00000_00000_00000_00000_00000_1m);
            Assert.AreEqual(3, log10_1000, 0.0000000000000000001m);
        }
        [TestMethod()]
        public void Log2Test()
        {
            var delta = 0.00000_00000_00000_00000_00000_001m;
            ExtendedDecimal f2 = 2;
            ExtendedDecimal f4 = 4;
            ExtendedDecimal f8 = 8;
            var log2_2 = ExtendedDecimal.Log2(f2);
            var log2_4 = ExtendedDecimal.Log2(f4);
            var log2_8 = ExtendedDecimal.Log2(f8);
            Assert.AreEqual(1, log2_2, delta);
            Assert.AreEqual(2, log2_4, delta);
            Assert.AreEqual(3, log2_8, 0.000000000000000000000000001m);
        }
        [TestMethod()]
        public void PowTest()
        {
            //ExtendedDecimal expected = 4m;
            //ExtendedDecimal actual = ExtendedDecimal.Pow(2, 2);
            //Assert.AreEqual(expected, actual);
        }
    }
}