using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Double;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class DoubleTests
    {
        const double LoopIncrement = 1.0f / 8;

        [TestMethod()]
        public void EqualsTest()
        {
            for (double j = -10; j <= +10; j += LoopIncrement)
            {
                var c1 = new Double(0);
                for (double i = -10; i <= +10; i += LoopIncrement)
                {
                    var d1 = new Double(j);
                    var d2 = new Double(i);
                    Assert.AreEqual(j.Equals(i), d1.Equals(d2));
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            for (double i = -10; i <= +10; i += LoopIncrement)
            {
                var d1 = new Double(i);
                Assert.AreEqual(i.ToString(), d1.ToString());
            }
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            for (double i = -10; i <= +10; i += LoopIncrement)
            {
                var d1 = new Double(i);
                Assert.AreEqual(i.GetHashCode(), d1.GetHashCode());
            }
        }
    }
}