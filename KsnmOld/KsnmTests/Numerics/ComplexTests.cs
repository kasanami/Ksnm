using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Double;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ComplexTests
    {
        /// <summary>
        /// この値程度の誤差は発生してしまう。
        /// 0.000000000000001だとNGになる。
        /// </summary>
        const double AssertDelta = 0.00000000000001;
        const double LoopIncrement = 1.0f / 8;

        [TestMethod()]
        public void MagnitudeTest()
        {
            for (double r = -10; r <= +10; r += LoopIncrement)
            {
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    var c1 = new Complex(r, i);
                    var sc1 = new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc1.Magnitude, c1.Magnitude, AssertDelta);
                }
            }
        }

        [TestMethod()]
        public void PhaseTest()
        {
            for (double r = -10; r <= +10; r += LoopIncrement)
            {
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    var c1 = new Complex(r, i);
                    var sc1 = new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc1.Phase, c1.Phase);
                }
            }
        }

        [TestMethod()]
        public void OperatorTest()
        {
            var c1 = new Complex(1, 1);
            var sc1 = new System.Numerics.Complex(1, 1);
            for (double r = -10; r <= +10; r += LoopIncrement)
            {
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    // 加算
                    var c2 = c1 + new Complex(r, i);
                    var sc2 = sc1 + new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc2.Real, c2.Real);
                    Assert.AreEqual(sc2.Imaginary, c2.Imaginary);
                    // 減算
                    c2 = c1 - new Complex(r, i);
                    sc2 = sc1 - new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc2.Real, c2.Real);
                    Assert.AreEqual(sc2.Imaginary, c2.Imaginary);
                    // 乗算
                    c2 = c1 * new Complex(r, i);
                    sc2 = sc1 * new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc2.Real, c2.Real);
                    Assert.AreEqual(sc2.Imaginary, c2.Imaginary);
                    // 除算
                    if (r == 0 && i == 0) { continue; }
                    c2 = c1 / new Complex(r, i);
                    sc2 = sc1 / new System.Numerics.Complex(r, i);
                    Assert.AreEqual(sc2.Real, c2.Real, AssertDelta);
                    Assert.AreEqual(sc2.Imaginary, c2.Imaginary, AssertDelta);
                }
            }
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var c1 = new Complex(0, 0);
            for (double r = -10; r <= +10; r += LoopIncrement)
            {
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    c1 = new Complex(r, i);
                    var c2 = new Complex(r, i);
                    Assert.IsTrue(c1.Equals(c2));
                }
            }
            c1 = new Complex(99, 99);
            for (double r = -10; r <= +10; r += LoopIncrement)
            {
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    var c2 = new Complex(r, i);
                    Assert.IsFalse(c1.Equals(c2));
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var c1 = new Complex(0, 0);
            Assert.AreEqual("(0, 0)", c1.ToString());
            c1 = new Complex(1, -1);
            Assert.AreEqual("(1, -1)", c1.ToString());
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var c1 = new Complex(0, 0);
            var c2 = new Complex(0, 0);
            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
            c1 = new Complex(1, 0);
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode());
            c1 = new Complex(0, 1);
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode());
            c1 = new Complex(1, -1);
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode());
        }
    }
}