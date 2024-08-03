using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics.Tests
{
    using Fixed = FixedPointNumber16Q8;

    [TestClass()]
    public class FixedPointNumber16Q8Tests
    {
        const float LoopIncrement = 1.0f / 8;
        [TestMethod()]
        public void ConstantTest()
        {
            var zero = (Fixed)0;
            var one = (Fixed)1;
            var two = (Fixed)2;
            var minusTwo = (Fixed)(-2);
            Assert.AreEqual(one, one + Fixed.Zero);
            Assert.AreEqual(two, one + Fixed.One);
            Assert.AreEqual(zero, one + Fixed.MinusOne);
            Assert.AreEqual(one, Fixed.MinusOne * Fixed.MinusOne);
            Assert.AreEqual(minusTwo, Fixed.MinusOne + Fixed.MinusOne);
            Assert.IsFalse(one == one + Fixed.Epsilon);
            Assert.IsFalse(one == one - Fixed.Epsilon);

            var value = Fixed.One;
            value += Fixed.One;
            Assert.AreEqual(two, value);
            value *= Fixed.MinusOne;
            Assert.AreEqual(minusTwo, value);
        }
        [TestMethod()]
        public void ConstructorTest()
        {
            var value = new Fixed();
            Assert.AreEqual(0, value.Integer);
            Assert.AreEqual(0, value.Fractional);
            value = new Fixed(123);
            Assert.AreEqual(123, value.Integer);
            Assert.AreEqual(0, value.Fractional);
            value = new Fixed(123, 123);
            Assert.AreEqual(123, value.Integer);
            Assert.AreEqual(123, value.Fractional);
        }
#if false
        [TestMethod()]
        public void SetDoubleTest()
        {
            var fx = new Fixed();
            // double
            for (double i = -10; i <= 10; i += LoopIncrement)
            {
                fx.SetDouble(i);
                Assert.AreEqual(i, (double)fx, $"double i={i} fx={fx}");
            }
            {
                var i = double.PositiveInfinity;
                fx.SetDouble(i);
                Assert.AreEqual(Fixed.MaxValue, fx, $"double i={i} fx={fx}");
            }
            {
                var i = double.NegativeInfinity;
                fx.SetDouble(i);
                Assert.AreEqual(Fixed.MinValue, fx, $"double i={i} fx={fx}");
            }
            {
                var i = double.NaN;
                fx.SetDouble(i);
                Assert.AreEqual(Fixed.Zero, fx, $"double i={i} fx={fx}");
            }
        }
#endif
        [TestMethod()]
        public void CastTest()
        {
            // byte
            for (byte i = 0; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (byte)fx, $"byte i={i} fx={fx}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (byte)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (byte)fx, $"byte i={i} fx={fx}");
            }
            // sbyte
            for (sbyte i = -10; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (sbyte)fx, $"sbyte i={i} fx={fx}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (sbyte)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (sbyte)fx, $"sbyte i={i} fx={fx}");
            }
            // short
            for (short i = -10; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (short)fx, $"short i={i} fx={fx}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (short)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (short)fx, $"short i={i} fx={fx}");
            }
            // ushort
            for (ushort i = 0; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (ushort)fx, $"ushort i={i} fx={fx}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (ushort)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (ushort)fx, $"ushort i={i} fx={fx}");
            }
            // int
            for (int i = -10; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (int)fx, $"int i={i} fx={fx}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (int)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (int)fx, $"int i={i} fx={fx}");
            }
            // uint
            for (uint i = 0; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (uint)fx, $"uint i={i} fx={fx}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (uint)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (uint)fx, $"uint i={i} fx={fx}");
            }
            // long
            for (long i = -10; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (long)fx, $"long i={i} fx={fx}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (long)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (long)fx, $"long i={i} fx={fx}");
            }
            // ulong
            for (ulong i = 0; i <= 10; i++)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (ulong)fx, $"ulong i={i} fx={fx}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (ulong)d;
                var fx = (Fixed)d;
                Assert.AreEqual(i, (ulong)fx, $"ulong i={i} fx={fx}");
            }
            // float
            for (float i = -10; i <= 10; i += LoopIncrement)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (float)fx, $"float i={i} fx={fx}");
            }
            // double
            for (double i = -10; i <= 10; i += LoopIncrement)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (double)fx, $"double i={i} fx={fx}");
            }
            // decimal
            for (decimal i = -10; i <= 10; i += 0.125M)
            {
                var fx = (Fixed)i;
                Assert.AreEqual(i, (decimal)fx, $"decimal i={i} fx={fx}");
            }
        }
        [TestMethod()]
        public void OperationsTest1()
        {
            var value = (Fixed)123;
            var expected1 = new Fixed(+123);
            var expected2 = new Fixed(-123);
            var expected3 = new Fixed(); expected3.SetBits((short)~expected1.Bits);
            Assert.AreEqual(expected1, +value, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(expected2, -value, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(expected3, ~value, Debug.GetFilePathAndLineNumber());
        }
        [TestMethod()]
        public void OperationsTest2()
        {
            var n1 = (Fixed)(-1);
            var p0_5 = (Fixed)0.5;
            var p1 = (Fixed)1;
            var p1_5 = (Fixed)1.5;
            var p2 = (Fixed)2;
            var p2_5 = (Fixed)2.5;
            var p3 = (Fixed)3;
            var p4 = (Fixed)4;
            var p6 = (Fixed)6;
            // +
            Assert.AreEqual(p3, p1 + p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p4, p2 + p2, Debug.GetFilePathAndLineNumber());
            // -
            Assert.AreEqual(p1, p3 - p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(n1, p1 - p2, Debug.GetFilePathAndLineNumber());
            // *
            Assert.AreEqual(p2, p1 * p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p6, p2 * p3, Debug.GetFilePathAndLineNumber());
            // /
            Assert.AreEqual(p2, p4 / p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p1_5, p3 / p2, Debug.GetFilePathAndLineNumber());
            // %
            Assert.AreEqual(p1, p3 % p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p0_5, p2_5 % p2, Debug.GetFilePathAndLineNumber());
            // &
            Assert.AreEqual(p2, p3 & p2, Debug.GetFilePathAndLineNumber());
            // |
            Assert.AreEqual(p3, p1 | p2, Debug.GetFilePathAndLineNumber());
            // ^
            Assert.AreEqual(p2, p3 ^ p1, Debug.GetFilePathAndLineNumber());
            // <<
            Assert.AreEqual(p2, p1 << 1, Debug.GetFilePathAndLineNumber());
            // >>
            Assert.AreEqual(p1, p2 >> 1, Debug.GetFilePathAndLineNumber());
        }
        [TestMethod()]
        public void OperationsTest3()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.IsTrue(value1 != value2);
            Assert.IsTrue(value1 == value3);
            Assert.IsTrue(value1 < value2);
            Assert.IsTrue(value1 <= value2);
            Assert.IsTrue(value2 > value1);
            Assert.IsTrue(value2 >= value1);
        }
        [TestMethod()]
        public void CompareToTest()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.IsTrue(value1.CompareTo(value2) < 0);
            Assert.IsTrue(value1.CompareTo(value3) == 0);
            Assert.IsTrue(value2.CompareTo(value1) > 0);
        }
        [TestMethod()]
        public void EqualsTest()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.IsFalse(value1.Equals(value2));
            Assert.IsTrue(value1.Equals(value3));
        }
        [TestMethod()]
        public void IsEvenTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual(Math.IsEven(f), fx.IsEven(), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void IsOddTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual(Math.IsOdd(f), fx.IsOdd(), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void AbsTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual((int)System.Math.Abs(f), (int)Fixed.Abs(fx), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void CeilingTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual((int)System.Math.Ceiling(f), (int)Fixed.Ceiling(fx), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void FloorTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual((int)System.Math.Floor(f), (int)Fixed.Floor(fx), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void RoundTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual((int)System.Math.Round(f), (int)Fixed.Round(fx), $"f={f} fx={fx}");
            }
        }
        [TestMethod()]
        public void TruncateTest()
        {
            for (float f = -3; f <= +3; f += LoopIncrement)
            {
                var fx = (Fixed)f;
                Assert.AreEqual((int)System.Math.Truncate(f), (int)Fixed.Truncate(fx), $"f={f} fx={fx}");
            }
        }

        #region INumberBase

        [TestMethod()]
        public void IsCanonicalTest()
        {
        }

        [TestMethod()]
        public void IsComplexNumberTest()
        {

        }

        [TestMethod()]
        public void IsEvenIntegerTest()
        {
        }

        [TestMethod()]
        public void IsFiniteTest()
        {
        }

        [TestMethod()]
        public void IsImaginaryNumberTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsImaginaryNumber(fx));
        }

        [TestMethod()]
        public void IsInfinityTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsInfinity(fx));
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
        }

        [TestMethod()]
        public void IsNaNTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsNaN(fx));
        }

        [TestMethod()]
        public void IsNegativeTest()
        {
        }

        [TestMethod()]
        public void IsNegativeInfinityTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsNegativeInfinity(fx));
        }

        [TestMethod()]
        public void IsNormalTest()
        {
        }

        [TestMethod()]
        public void IsOddIntegerTest()
        {
        }

        [TestMethod()]
        public void IsPositiveTest()
        {
        }

        [TestMethod()]
        public void IsPositiveInfinityTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsPositiveInfinity(fx));
        }

        [TestMethod()]
        public void IsRealNumberTest()
        {
            var fx = (Fixed)1.0;
            Assert.IsTrue(Fixed.IsRealNumber(fx));
        }

        [TestMethod()]
        public void IsSubnormalTest()
        {
        }

        [TestMethod()]
        public void IsZeroTest()
        {
            var _0 = (Fixed)0.0;
            Assert.IsTrue(Fixed.IsZero(_0));
            var _1 = (Fixed)1.0;
            Assert.IsFalse(Fixed.IsZero(_1));
            var _2 = (Fixed)2.0;
            Assert.IsFalse(Fixed.IsZero(_2));
        }

        [TestMethod()]
        public void MaxMagnitudeTest()
        {
        }

        [TestMethod()]
        public void MaxMagnitudeNumberTest()
        {
        }

        [TestMethod()]
        public void MinMagnitudeTest()
        {
        }

        [TestMethod()]
        public void MinMagnitudeNumberTest()
        {
        }

        [TestMethod()]
        public void ParseTest()
        {
            for (int i = -10; i <= 10; i++)
            {
                var str = i.ToString();
                var fx = Fixed.Parse(str, null);
                Assert.AreEqual(i, (int)fx);
            }
            for (double i = -10; i <= 10; i+=0.5)
            {
                var str = i.ToString();
                var fx = Fixed.Parse(str, null);
                Assert.AreEqual(i, (double)fx);
            }
        }

        [TestMethod()]
        public void TryParseTest()
        {
        }

        [TestMethod()]
        public void TryFormatTest()
        {
        }

        [TestMethod()]
        public void ToStringTest()
        {
        }

        [TestMethod()]
        public void TryConvertFromCheckedTest()
        {
        }

        [TestMethod()]
        public void TryConvertFromSaturatingTest()
        {
        }

        [TestMethod()]
        public void TryConvertFromTruncatingTest()
        {
        }

        [TestMethod()]
        public void TryConvertToCheckedTest()
        {
        }

        [TestMethod()]
        public void TryConvertToSaturatingTest()
        {
        }

        [TestMethod()]
        public void TryConvertToTruncatingTest()
        {
        }
        #endregion INumberBase
    }
}
