using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Int32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.ExtensionMethods.System.Int32.Tests
{
    [TestClass()]
    public class Int32ExtensionsTests
    {

        [TestMethod()]
        public void ToInt32BitsTest()
        {
            int sample = 12345;
            int expected = 12345;
            Assert.AreEqual(expected, sample.ToInt32Bits());
        }

        [TestMethod()]
        public void ToUInt32BitsTest()
        {
            int sample = 12345;
            uint expected = 12345;
            Assert.AreEqual(expected, sample.ToUInt32Bits());
        }

        [TestMethod()]
        public void ToDigitsTest()
        {
            int sample = 12345;
            var digits = sample.ToDigits(10);
            var expected = new[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(digits.SequenceEqual(expected));

            digits = sample.ToDigits(2).ToArray();
            expected = new[] { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1 };
            Assert.IsTrue(digits.SequenceEqual(expected));
        }

        [TestMethod()]
        public void ToReversedDigitsTest()
        {
            int sample = 12345;
            var digits = sample.ToReversedDigits(10).ToArray();
            Assert.AreEqual(5, digits[0]);
            Assert.AreEqual(4, digits[1]);
            Assert.AreEqual(3, digits[2]);
            Assert.AreEqual(2, digits[3]);
            Assert.AreEqual(1, digits[4]);
        }
    }
}