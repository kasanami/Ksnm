using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using complex = System.Numerics.Complex;

namespace Ksnm.ExtensionMethods.System.Complex.Tests
{
    [TestClass()]
    public class ComplexExtensionsTests
    {
        [TestMethod()]
        public void IsPositiveTest()
        {
            complex sample = 1;
            Assert.IsTrue(sample.IsPositive());
            sample = 0;
            Assert.IsFalse(sample.IsPositive());
            sample = -1;
            Assert.IsFalse(sample.IsPositive());
        }

        [TestMethod()]
        public void IsNegativeTest()
        {
            complex sample = 1;
            Assert.IsFalse(sample.IsNegative());
            sample = 0;
            Assert.IsFalse(sample.IsNegative());
            sample = -1;
            Assert.IsTrue(sample.IsNegative());
        }

        [TestMethod()]
        public void IsInfinityTest()
        {
            complex sample = 1;
            Assert.IsFalse(sample.IsInfinity());
            sample = 0;
            Assert.IsFalse(sample.IsInfinity());
            sample = double.PositiveInfinity;
            Assert.IsTrue(sample.IsInfinity());
            sample = double.NegativeInfinity;
            Assert.IsTrue(sample.IsInfinity());
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
            // 以下はtrueになる
            complex sample = 0f;
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
            sample = double.Epsilon;
            Assert.IsFalse(sample.IsInteger());
            for (int i = 1; i < 100000; i *= 10)
            {
                sample = i + 0.1f; Assert.IsFalse(sample.IsInteger(), $"i={i}");
            }
            // 無限大はfalse
            sample = double.PositiveInfinity;
            Assert.IsFalse(sample.IsInteger());
            sample = double.NegativeInfinity;
            Assert.IsFalse(sample.IsInteger());
        }
    }
}