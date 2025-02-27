using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Numerics.Tests
{
    using Half = FloatingPointNumber16;
    [TestClass()]
    public class FloatingPointNumber16Tests
    {
        [TestMethod()]
        public void PropertiesTest()
        {
        }
        [TestMethod()]
        public void OperationsTest()
        {
        }
        [TestMethod()]
        public void ToDoubleTest()
        {
            Assert.AreEqual(0, Half.Zero.ToDouble());
            Assert.AreEqual(1.0, Half.One.ToDouble());
            Assert.AreEqual(-1.0, Half.MinusOne.ToDouble());
            Assert.AreEqual(double.PositiveInfinity, Half.PositiveInfinity.ToDouble());
            Assert.AreEqual(double.NegativeInfinity, Half.NegativeInfinity.ToDouble());
            Assert.AreEqual(double.NaN, Half.NaN.ToDouble());

            for (int i = -10; i <= 10; i++)
            {
                var doubleA = 1.0 / i;
                var halfA = Half.FromDouble(doubleA);
                Assert.AreEqual(doubleA, halfA.ToDouble(), 0.001);
            }
        }
        [TestMethod()]
        public void FromDoubleTest()
        {
            Assert.AreEqual(Half.Zero, Half.FromDouble(0));
            Assert.AreEqual(Half.One, Half.FromDouble(1));
            Assert.AreEqual(Half.MinusOne, Half.FromDouble(-1));
            Assert.AreEqual(Half.PositiveInfinity, Half.FromDouble(double.PositiveInfinity));
            Assert.AreEqual(Half.NegativeInfinity, Half.FromDouble(double.NegativeInfinity));
            Assert.AreEqual(Half.NaN, Half.FromDouble(double.NaN));
        }
        [TestMethod()]
        public void EqualsTest()
        {
            for (int i = -10; i <= 10; i++)
            {
                var doubleA = 1.0 / i;
                var doubleB = 1.0 / i;
                var halfA = Half.FromDouble(doubleA);
                var halfB = Half.FromDouble(doubleB);
                Assert.AreEqual(doubleA.Equals(doubleB), halfA.Equals(halfB));
            }
        }
    }
}
