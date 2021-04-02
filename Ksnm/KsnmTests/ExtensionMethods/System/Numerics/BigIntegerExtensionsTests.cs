using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Ksnm.ExtensionMethods.System.Numerics.Tests
{
    [TestClass()]
    public class BigIntegerExtensionsTests
    {
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