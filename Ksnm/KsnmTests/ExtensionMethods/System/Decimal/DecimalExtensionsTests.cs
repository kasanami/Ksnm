using Ksnm.ExtensionMethods.System.Decimal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.ExtensionMethods.System.Decimal.Tests
{
    [TestClass()]
    public class DecimalExtensionsTests
    {
        [TestMethod()]
        public void ToClampedSByteTest()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((sbyte)sample, sample.ToClampedSByte());
            Assert.AreEqual(sbyte.MinValue, minSample.ToClampedSByte());
            Assert.AreEqual(sbyte.MaxValue, maxSample.ToClampedSByte());
        }

        [TestMethod()]
        public void ToClampedByteTest()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((byte)sample, sample.ToClampedByte());
            Assert.AreEqual(byte.MinValue, minSample.ToClampedByte());
            Assert.AreEqual(byte.MaxValue, maxSample.ToClampedByte());
        }

        [TestMethod()]
        public void ToClampedInt16Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int16)sample, sample.ToClampedInt16());
            Assert.AreEqual(Int16.MinValue, minSample.ToClampedInt16());
            Assert.AreEqual(Int16.MaxValue, maxSample.ToClampedInt16());
        }

        [TestMethod()]
        public void ToClampedUInt16Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt16)sample, sample.ToClampedUInt16());
            Assert.AreEqual(UInt16.MinValue, minSample.ToClampedUInt16());
            Assert.AreEqual(UInt16.MaxValue, maxSample.ToClampedUInt16());
        }

        [TestMethod()]
        public void ToClampedInt32Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int32)sample, sample.ToClampedInt32());
            Assert.AreEqual(Int32.MinValue, minSample.ToClampedInt32());
            Assert.AreEqual(Int32.MaxValue, maxSample.ToClampedInt32());
        }

        [TestMethod()]
        public void ToClampedUInt32Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt32)sample, sample.ToClampedUInt32());
            Assert.AreEqual(UInt32.MinValue, minSample.ToClampedUInt32());
            Assert.AreEqual(UInt32.MaxValue, maxSample.ToClampedUInt32());
        }

        [TestMethod()]
        public void ToClampedInt64Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((Int64)sample, sample.ToClampedInt64());
            Assert.AreEqual(Int64.MinValue, minSample.ToClampedInt64());
            Assert.AreEqual(Int64.MaxValue, maxSample.ToClampedInt64());
        }

        [TestMethod()]
        public void ToClampedUInt64Test()
        {
            decimal sample = 42;
            decimal minSample = decimal.MinValue;
            decimal maxSample = decimal.MaxValue;
            Assert.AreEqual((UInt64)sample, sample.ToClampedUInt64());
            Assert.AreEqual(UInt64.MinValue, minSample.ToClampedUInt64());
            Assert.AreEqual(UInt64.MaxValue, maxSample.ToClampedUInt64());
        }

        [TestMethod()]
        public void GetSignBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0, sample.GetSignBits());
            sample = -1;
            Assert.AreEqual(1, sample.GetSignBits());
        }

        [TestMethod()]
        public void GetSignTest()
        {
            decimal sample = 1;
            Assert.AreEqual(+1, sample.GetSign());
            sample = -1;
            Assert.AreEqual(-1, sample.GetSign());
        }

        [TestMethod()]
        public void IsNegativeTest()
        {
            decimal sample = 1;
            Assert.AreEqual(false, sample.IsNegative());
            sample = -1;
            Assert.AreEqual(true, sample.IsNegative());
        }

        [TestMethod()]
        public void GetExponentBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0,sample.GetExponentBits());
            sample = -1;
            Assert.AreEqual(0, sample.GetExponentBits());

            sample = 2;
            Assert.AreEqual(0, sample.GetExponentBits());
            sample = -2;
            Assert.AreEqual(0, sample.GetExponentBits());

            sample = 0.5m;
            Assert.AreEqual(1, sample.GetExponentBits());
            sample = -0.5m;
            Assert.AreEqual(1, sample.GetExponentBits());
        }

        [TestMethod()]
        public void GetExponentTest()
        {
            decimal sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = -1;
            Assert.AreEqual(0, sample.GetExponent());

            sample = 2;
            Assert.AreEqual(0, sample.GetExponent());
            sample = -2;
            Assert.AreEqual(0, sample.GetExponent());

            sample = 0.5m;
            Assert.AreEqual(1, sample.GetExponent());
            sample = -0.5m;
            Assert.AreEqual(1, sample.GetExponent());
        }

        [TestMethod()]
        public void GetMantissaBitsTest()
        {
            decimal sample = 1;
            Assert.AreEqual(1, sample.GetMantissaBits()[0]);
            sample = -1;
            Assert.AreEqual(1, sample.GetMantissaBits()[0]);

            sample = 2;
            Assert.AreEqual(2, sample.GetMantissaBits()[0]);
            sample = -2;
            Assert.AreEqual(2, sample.GetMantissaBits()[0]);

            sample = 3;
            Assert.AreEqual(3, sample.GetMantissaBits()[0]);
            sample = -3;
            Assert.AreEqual(3, sample.GetMantissaBits()[0]);

            sample = 0.5m;
            Assert.AreEqual(5, sample.GetMantissaBits()[0]);
            sample = -0.5m;
            Assert.AreEqual(5, sample.GetMantissaBits()[0]);

            sample = 0.123m;
            Assert.AreEqual(123, sample.GetMantissaBits()[0]);
            sample = -0.123m;
            Assert.AreEqual(123, sample.GetMantissaBits()[0]);
        }

        [TestMethod()]
        public void GetMantissaTest()
        {
            decimal sample = 1;
            Assert.AreEqual(1, sample.GetMantissa());
            sample = -1;
            Assert.AreEqual(1, sample.GetMantissa());

            sample = 2;
            Assert.AreEqual(2, sample.GetMantissa());
            sample = -2;
            Assert.AreEqual(2, sample.GetMantissa());

            sample = 3;
            Assert.AreEqual(3, sample.GetMantissa());
            sample = -3;
            Assert.AreEqual(3, sample.GetMantissa());

            sample = 0.5m;
            Assert.AreEqual(5, sample.GetMantissa());
            sample = -0.5m;
            Assert.AreEqual(5, sample.GetMantissa());

            sample = 0.123m;
            Assert.AreEqual(123, sample.GetMantissa());
            sample = -0.123m;
            Assert.AreEqual(123, sample.GetMantissa());
        }
    }
}
