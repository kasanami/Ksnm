using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics.Tests
{
    using Half = FloatingPointNumber16;
    [TestClass()]
    public class FloatingPointNumber16Tests
    {
        [TestMethod()]
        public void ToDoubleTest()
        {
            Assert.AreEqual(0, Half.Zero.ToDouble());
            Assert.AreEqual(1.0, Half.One.ToDouble());
            Assert.AreEqual(-1.0, Half.MinusOne.ToDouble());
            Assert.AreEqual(double.PositiveInfinity, Half.PositiveInfinity.ToDouble());
            Assert.AreEqual(double.NegativeInfinity, Half.NegativeInfinity.ToDouble());
            Assert.AreEqual(double.NaN, Half.NaN.ToDouble());
        }
    }
}