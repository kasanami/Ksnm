namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ScaledNumber8Tests
    {
        [TestMethod()]
        public void CastTest()
        {
            ScaledNumber8 scaled = ScaledNumber8.Zero;
            Assert.AreEqual(0.0, (double)scaled);
            scaled = ScaledNumber8.One;
            Assert.AreEqual(1.0, (double)scaled);

            scaled = (ScaledNumber8)0.5;
            Assert.AreEqual(0.5, (double)scaled, 0.01);

            scaled = (ScaledNumber8)0.25;
            Assert.AreEqual(0.25, (double)scaled, 0.01);
        }
        [TestMethod()]
        public void OperationsTest()
        {
            ScaledNumber8 scaled1 = ScaledNumber8.Zero;
            ScaledNumber8 scaled2 = ScaledNumber8.Zero;
            Assert.IsTrue(scaled1 == scaled2);
            Assert.IsFalse(scaled1 != scaled2);
            scaled1 = ScaledNumber8.Zero;
            scaled2 = ScaledNumber8.Epsilon;
            Assert.IsFalse(scaled1 == scaled2);
            Assert.IsTrue(scaled1 != scaled2);
            Assert.IsFalse(scaled1 >= scaled2);
            Assert.IsTrue(scaled1 <= scaled2);
            Assert.IsFalse(scaled1 > scaled2);
            Assert.IsTrue(scaled1 < scaled2);
            {
                scaled1.Bits = 1;
                scaled2.Bits = 2;
                Assert.AreEqual(scaled2, scaled1 + scaled1);
                scaled2 = ScaledNumber8.Zero;
                Assert.AreEqual(scaled2, scaled1 - scaled1);
            }
            {
                scaled1 = ScaledNumber8.Zero;
                scaled2 = ScaledNumber8.Epsilon;
                Assert.AreEqual(ScaledNumber8.Zero, scaled1 * scaled2);
                scaled2 = ScaledNumber8.One;
                Assert.AreEqual(ScaledNumber8.Zero, scaled1 * scaled2);
            }
            {
                scaled1 = ScaledNumber8.One;
                scaled2 = ScaledNumber8.One;
                Assert.AreEqual(ScaledNumber8.One, scaled1 / scaled2);
            }
        }
    }
}