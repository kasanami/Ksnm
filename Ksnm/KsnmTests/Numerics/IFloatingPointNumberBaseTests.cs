using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class IFloatingPointNumberBaseTests
    {
        [TestMethod()]
        public void ToDoubleTest()
        {
            ExtendedDouble d;
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(0.0f);
            Assert.AreEqual<double>(0.0, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(0.125f);
            Assert.AreEqual<double>(0.125, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(0.25f);
            Assert.AreEqual<double>(0.25, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(0.5f);
            Assert.AreEqual<double>(0.5, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(1.0f);
            Assert.AreEqual<double>(1.0, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(2.0f);
            Assert.AreEqual<double>(2.0, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(4.0f);
            Assert.AreEqual<double>(4.0, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(8.0f);
            Assert.AreEqual<double>(8.0, d);

            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(-1.0f);
            Assert.AreEqual<double>(-1.0, d);
            d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, uint>(-2.0f);
            Assert.AreEqual<double>(-2.0, d);
        }
        [TestMethod()]
        public void ShiftMantissaTest()
        {
            uint mantissa;
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1, 1, 1);
            Assert.AreEqual(0b1u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1, 1, 2);
            Assert.AreEqual(0b10u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1, 1, 3);
            Assert.AreEqual(0b100u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1, 1, 32);
            Assert.AreEqual(0x8000_0000u, mantissa);

            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1010_1010, 8, 1);
            Assert.AreEqual(0b1u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1010_1010, 8, 2);
            Assert.AreEqual(0b10u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1010_1010, 8, 3);
            Assert.AreEqual(0b101u, mantissa);
            mantissa = IFloatingPointNumberBase.ShiftMantissa<uint, uint>(0b1010_1010, 8, 4);
            Assert.AreEqual(0b1010u, mantissa);
        }
    }
}