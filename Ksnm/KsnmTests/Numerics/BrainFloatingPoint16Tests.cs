using KsnmTests.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.Numerics.Tests
{
    using Half = BrainFloatingPoint16;
    [TestClass()]
    public class BrainFloatingPoint16Tests
    {
        [TestMethod()]
        public void ConstantsTest()
        {
            Assert.AreEqual(float.E, (float)Half.E, 0.02f);
            Assert.AreEqual(float.Pi, (float)Half.Pi, 0.02f);
            Assert.AreEqual(float.Tau, (float)Half.Tau, 0.02f);
            Assert.AreEqual(1.0f, (float)Half.One);
            Assert.AreEqual(0.0f, (float)Half.Zero);
            Assert.AreEqual(-1.0f, (float)Half.NegativeOne);
            Assert.AreEqual(-0.0f, (float)Half.NegativeZero);
            Assert.AreEqual(float.Epsilon, (float)Half.Epsilon, 0.02f);
            {
                // -16はビット数の差
                ExtendedSingle s = float.MaxValue;
                Assert.AreEqual(s.Exponent, Half.MaxValue.Exponent - 16);
            }
            {
                ExtendedSingle s = float.MinValue;
                Assert.AreEqual(s.Exponent, Half.MinValue.Exponent - 16);
            }
            Assert.AreEqual(float.PositiveInfinity, (float)Half.PositiveInfinity);
            Assert.AreEqual(float.NegativeInfinity, (float)Half.NegativeInfinity);
            Assert.IsTrue(float.IsInfinity((float)Half.PositiveInfinity));
            Assert.IsTrue(float.IsInfinity((float)Half.NegativeInfinity));
            Assert.IsTrue(float.IsPositiveInfinity((float)Half.PositiveInfinity));
            Assert.IsTrue(float.IsNegativeInfinity((float)Half.NegativeInfinity));
            Assert.IsTrue(float.IsNaN((float)Half.NaN));
            Assert.IsTrue(Half.IsNaN(Half.NaN));
        }
        [TestMethod()]
        public void OperationsTest()
        {
            for (float i = -10; i < 10; i += 0.125f)
            {
                Half half1 = (Half)i;
                for (float j = -10; j < 10; j += 0.125f)
                {
                    Half half2 = (Half)j;
                    // +
                    {
                        float expected = i + j;
                        Half half = half1 + half2;
                        Assert.AreEqual(expected, (float)half);
                    }
                    // -
                    {
                        float expected = i - j;
                        Half half = half1 - half2;
                        Assert.AreEqual(expected, (float)half);
                    }
                    // *
                    {
                        float expected = i * j;
                        Half half = half1 * half2;
                        Assert.AreEqual(expected, (float)half, 0.5f, $"{i}*{j}");
                    }
                    // /
                    if (j != 0)
                    {
                        float expected = i / j;
                        Half half = half1 / half2;
                        Assert.AreEqual(expected, (float)half, 0.5f, $"{i}*{j}");
                    }
                }
            }
            // 符号反転
            {
                Half half = (Half)1;
                Assert.AreEqual(-1.0f, (float)(-half));
                half = (Half)(-1);
                Assert.AreEqual(1.0f, (float)(-half));
            }
            // 比較演算子
            {
                Half half1 = (Half)1;
                Half half2 = (Half)1;
                Assert.IsTrue(half1 == half2);
                Assert.IsFalse(half1 != half2);
                Assert.IsFalse(half1 > half2);
                Assert.IsTrue(half1 >= half2);
                Assert.IsFalse(half1 < half2);
                Assert.IsTrue(half1 <= half2);
                half1 = (Half)1;
                half2 = (Half)2;
                Assert.IsFalse(half1 == half2);
                Assert.IsTrue(half1 != half2);
                Assert.IsFalse(half1 > half2);
                Assert.IsFalse(half1 >= half2);
                Assert.IsTrue(half1 < half2);
                Assert.IsTrue(half1 <= half2);
            }
        }
        [TestMethod()]
        public void CastTest()
        {
            Half half = (Half)1.0f;
            Assert.AreEqual(1.0f, (float)half);
            Assert.AreEqual(1.0, (double)half);
            Assert.AreEqual(1, (int)half);
            half = (Half)(-1.0f);
            Assert.AreEqual(-1.0f, (float)half);
            Assert.AreEqual(-1.0, (double)half);
            Assert.AreEqual(-1, (int)half);
            half = (Half)2.0f;
            Assert.AreEqual(2.0f, (byte)half);
            Assert.AreEqual(2.0, (double)half);
            Assert.AreEqual(2, (int)half);
            half = (Half)(-2.0f);
            Assert.AreEqual(-2.0f, (float)half);
            Assert.AreEqual(-2.0, (double)half);
            Assert.AreEqual(-2, (int)half);
            half = (Half)(0.5f);
            Assert.AreEqual(0.5f, (float)half);
            Assert.AreEqual(0.5, (double)half);
            Assert.AreEqual(0, (int)half);
            half = (Half)(float.Epsilon);
            Assert.AreEqual(0.0f, (float)half);
            Assert.AreEqual(0.0, (double)half);
            Assert.AreEqual(0, (int)half);
            //half = (Half)float.MaxValue;
            //Assert.AreEqual(float.MaxValue, (float)half);
            //half = (Half)float.MinValue;
            //Assert.AreEqual(float.MinValue, (float)half);
        }
        [TestMethod()]
        public void EqualsTest()
        {
            Half half1 = (Half)1.0f;
            Half half2 = (Half)1.0f;
            Assert.AreEqual(half1, half2);

            half1 = (Half)2.0f;
            half2 = (Half)2.0f;
            Assert.AreEqual(half1, half2);
        }
    }
}
