using Newtonsoft.Json.Linq;
using System.Numerics;
using Float64 = System.Double;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class Float128Tests
    {
        [TestMethod()]
        public void Zero()
        {
            Float128 float128 = Float128.Zero;
            Assert.IsTrue(float128.IsZero);
            Assert.AreEqual(0.0, float128.ToDouble());
        }
        [TestMethod()]
        public void NegativeZero()
        {
            Float128 float128 = Float128.NegativeZero;
            Assert.IsTrue(float128.IsZero);
            Assert.AreEqual(Float64.NegativeZero, float128.ToDouble());
        }
        [TestMethod()]
        public void PositiveInfinity()
        {
            Float128 float128 = Float128.PositiveInfinity;
            Assert.IsTrue(float128.IsInfinity);
            Assert.AreEqual(Float64.PositiveInfinity, float128.ToDouble());
        }
        [TestMethod()]
        public void NegativeInfinity()
        {
            Float128 float128 = Float128.NegativeInfinity;
            Assert.IsTrue(float128.IsInfinity);
            Assert.AreEqual(Float64.NegativeInfinity, float128.ToDouble());
        }
        [TestMethod()]
        public void NaN()
        {
            Float128 float128 = Float128.NaN;
            Assert.IsTrue(float128.IsNaN);
            Assert.AreEqual(Float64.NaN, float128.ToDouble());

            // +∞ + -∞ = NaN
            {
                Float128 a = Float128.PositiveInfinity;
                Float128 b = Float128.NegativeInfinity;
                Float128 c = a + b;
                Assert.IsTrue(c.IsNaN);
            }
            //0 × ∞ = NaN
            {
                Float128 a = Float128.Zero;
                Float128 b = Float128.PositiveInfinity;
                Float128 c = a * b;
                Assert.IsTrue(c.IsNaN);
            }
            //∞ / ∞ = NaN
            {
                Float128 a = Float128.PositiveInfinity;
                Float128 b = Float128.PositiveInfinity;
                Float128 c = a / b;
                Assert.IsTrue(c.IsNaN);
            }
            //0 / 0 = NaN
            {
                Float128 a = Float128.Zero;
                Float128 b = Float128.Zero;
                Float128 c = a / b;
                Assert.IsTrue(c.IsNaN);
            }
        }
        [TestMethod()]
        public void One()
        {
            Float128 float128 = Float128.One;
            Assert.IsFalse(float128.IsInfinity);
            Assert.IsFalse(float128.IsZero);
            Assert.AreEqual(1.0, float128.ToDouble());
        }
        [TestMethod()]
        public void NegativeOne()
        {
            Float128 float128 = Float128.NegativeOne;
            Assert.IsFalse(float128.IsInfinity);
            Assert.IsFalse(float128.IsZero);
            Assert.AreEqual(-1.0, float128.ToDouble());
        }
        [TestMethod()]
        public void Two()
        {
            Float128 float128 = Float128.Two;
            Assert.IsFalse(float128.IsInfinity);
            Assert.IsFalse(float128.IsZero);
            Assert.AreEqual(2.0, float128.ToDouble());
        }
        [TestMethod()]
        public void Half()
        {
            Float128 float128 = Float128.Half;
            Assert.IsFalse(float128.IsInfinity);
            Assert.IsFalse(float128.IsZero);
            Assert.AreEqual(0.5, float128.ToDouble());
        }
        [TestMethod()]
        public void IsZero()
        {
            Float128 float128 = new Float128();
            Assert.IsTrue(float128.IsZero);
            float128 = Float128.One;
            Assert.IsFalse(float128.IsZero);
        }
        [TestMethod()]
        public void IsNegative()
        {
            Float128 float128 = new Float128();
            Assert.IsFalse(float128.IsNegative);
            float128 = new Float128(0x8000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsTrue(float128.IsNegative);
        }
        [TestMethod()]
        public void Sign()
        {
            Float128 float128 = new Float128();
            Assert.AreEqual(1, float128.Sign);
            float128 = new Float128(0x8000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.AreEqual(-1, float128.Sign);
        }
        [TestMethod()]
        public void IsInfinity()
        {
            Float128 float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsTrue(float128.IsInfinity);
            float128 = new Float128(0x7FFE_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsInfinity);
        }
        [TestMethod()]
        public void IsNaN()
        {
            Float128 float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsNaN);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsNaN);
        }
        [TestMethod()]
        public void IsSubnormal()
        {
            Float128 float128 = new Float128(0x0000_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsSubnormal);
            float128 = new Float128(0x0000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsSubnormal);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsSubnormal);
        }
        [TestMethod()]
        public void IsNormal()
        {
            Float128 float128 = new Float128(0x0001_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsNormal);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsNormal);
        }
        [TestMethod()]
        public void FromInt32()
        {
            for (int i = -100; i < 100; i++)
            {
                Float128 float128 = Float128.FromInt32(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual((double)i, float64);
            }
        }
        [TestMethod()]
        public void FromInt64()
        {
            for (long i = -100; i < 100; i++)
            {
                Float128 float128 = Float128.FromInt64(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual((double)i, float64);
            }
        }
        [TestMethod()]
        public void FromUInt64()
        {
            for (ulong i = 0; i < 100; i++)
            {
                Float128 float128 = Float128.FromUInt64(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual((double)i, float64);
            }
        }
        [TestMethod()]
        public void FromUInt128()
        {
            for (ulong i = 0; i < 100; i++)
            {
                Float128 float128 = Float128.FromUInt128(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual((double)i, float64);
            }
        }
        [TestMethod()]
        public void ToDouble()
        {
            for (double i = -100.0; i < 100.0; i += 0.5)
            {
                Float128 float128 = Float128.FromDouble(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual(i, float64);
            }
        }
        [TestMethod()]
        public void FromDouble()
        {
            for (double i = -100.0; i < 100.0; i += 0.5)
            {
                Float128 float128 = Float128.FromDouble(i);
                Float64 float64 = float128.ToDouble();
                Assert.AreEqual(i, float64);
            }
        }
        [TestMethod()]
        public void Parse()
        {
            var float128 = Float128.Parse("1");

            float128 = Float128.Parse("1.0");
            Assert.AreEqual(Float128.One, float128);

            float128 = Float128.Parse("0.5");
            Assert.AreEqual(Float128.Half, float128);

            float128 = Float128.Parse("1.5");
            Assert.AreEqual(Float128.One + Float128.Half, float128);

            float128 = Float128.Parse("2.0");
            Assert.AreEqual(Float128.Two, float128);
        }
        [TestMethod()]
        public void FromBigIntegerRatio()
        {
            {
                BigInteger numerator = BigInteger.Parse("12345");
                BigInteger denominator = BigInteger.Parse("10000");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual("1.2345", float128.ToString());
            }
            {
                BigInteger numerator = BigInteger.Parse("15");
                BigInteger denominator = BigInteger.Parse("10");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.One + Float128.Half, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("1");
                BigInteger denominator = BigInteger.Parse("1");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.One, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("2");
                BigInteger denominator = BigInteger.Parse("2");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.One, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("1");
                BigInteger denominator = BigInteger.Parse("2");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.Half, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("2");
                BigInteger denominator = BigInteger.Parse("1");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.Two, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("9");
                BigInteger denominator = BigInteger.Parse("3");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.Two + Float128.One, float128);
            }
            {
                BigInteger numerator = BigInteger.Parse("1");
                BigInteger denominator = BigInteger.Parse("4");
                Float128 float128 = Float128.FromBigIntegerRatio(false, numerator, denominator);
                Assert.AreEqual(Float128.Half * Float128.Half, float128);
            }
        }
        [TestMethod()]
        public void PackBigInteger()
        {
            var One = BigInteger.One << Float128.FractionBits;
            {
                Float128 float128 = Float128.PackBigInteger(false, -1, One);
                Assert.AreEqual(Float128.Half, float128);
            }
            {
                Float128 float128 = Float128.PackBigInteger(false, 0, One);
                Assert.AreEqual(Float128.One, float128);
            }
            {
                Float128 float128 = Float128.PackBigInteger(false, 1, One);
                Assert.AreEqual(Float128.Two, float128);
            }
            {
                Float128 float128 = Float128.PackBigInteger(false, 2, One);
                Assert.AreEqual(Float128.Two * Float128.Two, float128);
            }
        }
        [TestMethod()]
        public void GetBitLength()
        {
            {
                BigInteger bigInteger = BigInteger.Zero;
                int bitLength = Float128.GetBitLength(bigInteger);
                Assert.AreEqual(0, bitLength);
            }
            for (int i = 0; i < 200; i++)
            {
                BigInteger bigInteger = BigInteger.One << i;
                int bitLength = Float128.GetBitLength(bigInteger);
                Assert.AreEqual(i + 1, bitLength);
            }
        }
        [TestMethod()]
        public void CompareTo()
        {
            Float128 a = 0.1;
            Float128 b = 0.2;
            Assert.AreEqual(-1, a.CompareTo(b));
            Assert.AreEqual(1, b.CompareTo(a));
            Assert.AreEqual(0, a.CompareTo(a));
        }
        [TestMethod()]
        public void Equals()
        {
            Float128 a = 0.1;
            Float128 b = 0.2;
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a.Equals(a));
        }
        [TestMethod()]
        public void Negate()
        {
            Float128 a = 0.1;
            Float128 b = -0.1;
            Assert.AreEqual(b, a.Negate());
        }
        [TestMethod()]
        public void ToStringTest()
        {
            Float128 a = 1;
            Float128 b = -1;
            Float128 c = 0.5;
            Float128 d = -0.5;
            Assert.AreEqual("1", a.ToString());
            Assert.AreEqual("-1", b.ToString());
            Assert.AreEqual("0.5", c.ToString());
            Assert.AreEqual("-0.5", d.ToString());
            {
                Float128 value = Float128.Parse("123456.789");
                Assert.AreEqual("1.234568E+005", value.ToString("E", null));
                Assert.AreEqual("1.23E+005", value.ToString("E2", null));
                Assert.AreEqual("1.2345678900E+005", value.ToString("E10", null));
                Assert.AreEqual("1.23457e+005", value.ToString("e5", null));
            }
        }
        [TestMethod()]
        public void ToStringGeneral()
        {
            Float128 value = Float128.Parse("123.456789");
            //Assert.AreEqual("123.456789", value.ToString("G0", null));
            Assert.AreEqual("1.2E+02", value.ToString("G2", null));
            Assert.AreEqual("123.5", value.ToString("G4", null));
            Assert.AreEqual("123.45679", value.ToString("G8", null));
        }
        [TestMethod()]
        public void ToStringExponential()
        {
            Float128 value = Float128.Parse("123.456789");
            Assert.AreEqual("1E+002", value.ToString("E0", null));
            Assert.AreEqual("1.23E+002", value.ToString("E2", null));
            Assert.AreEqual("1.2346E+002", value.ToString("E4", null));
            Assert.AreEqual("1.23456789E+002", value.ToString("E8", null));
        }
        [TestMethod()]
        public void ToStringFixed()
        {
            Float128 value = Float128.Parse("123.456789");
            Assert.AreEqual("123", value.ToString("F0", null));
            Assert.AreEqual("123.46", value.ToString("F2", null));
            Assert.AreEqual("123.4568", value.ToString("F4", null));
            Assert.AreEqual("123.45678900", value.ToString("F8", null));
        }
        [TestMethod()]
        public void MinMaxValue()
        {
            var bits = Float128.MaxValue.ToUInt128Bits();
            Assert.AreEqual("7FFEFFFFFFFFFFFFFFFFFFFFFFFFFFFF", $"{bits:X32}");
            bits = Float128.MinValue.ToUInt128Bits();
            Assert.AreEqual("FFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFF", $"{bits:X32}");
        }
    }
}