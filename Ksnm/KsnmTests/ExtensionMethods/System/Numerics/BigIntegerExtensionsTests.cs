using Ksnm.ExtensionMethods.System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Ksnm.ExtensionMethods.System.Numerics.Tests
{
    [TestClass()]
    public class BigIntegerExtensionsTests
    {
        [TestMethod()]
        public void IsEvenTest()
        {
            // 以下はtrueになる
            BigInteger sample = 0;
            Assert.IsTrue(sample.IsEven());
            sample = 2;
            Assert.IsTrue(sample.IsEven());
            sample = -2;
            Assert.IsTrue(sample.IsEven());

            // 以下はfalseになる
            sample = 1;
            Assert.IsFalse(sample.IsEven());
            sample = 3;
            Assert.IsFalse(sample.IsEven());
            sample = -1;
            Assert.IsFalse(sample.IsEven());
        }

        [TestMethod()]
        public void IsOddTest()
        {
            // 以下はfalseになる
            BigInteger sample = 0;
            Assert.IsFalse(sample.IsOdd());
            sample = 2;
            Assert.IsFalse(sample.IsOdd());
            sample = -2;
            Assert.IsFalse(sample.IsOdd());

            // 以下はtrueになる
            sample = 1;
            Assert.IsTrue(sample.IsOdd());
            sample = 3;
            Assert.IsTrue(sample.IsOdd());
            sample = -1;
            Assert.IsTrue(sample.IsOdd());
        }

        [TestMethod()]
        public void RoundTest()
        {
            var midpointRounding = MidpointRounding.AwayFromZero;
            for (int ex = 0; ex < 5; ex++)
            {
                for (int i = 0; i < 100; i++)
                {
                    var actual = new BigInteger(i);
                    actual = actual.Round(ex, midpointRounding);
                    var expected = decimal.Round((decimal)i / Math.Pow(10, ex), 0, midpointRounding);
                    Assert.AreEqual(expected, (decimal)actual, $"i={i}");
                }
            }
            midpointRounding = MidpointRounding.ToEven;
            for (int ex = 0; ex < 5; ex++)
            {
                for (int i = 0; i < 100; i++)
                {
                    var actual = new BigInteger(i);
                    actual = actual.Round(ex, midpointRounding);
                    var expected = decimal.Round((decimal)i / Math.Pow(10, ex), 0, midpointRounding);
                    Assert.AreEqual(expected, (decimal)actual, $"i={i}");
                }
            }
        }
    }
}