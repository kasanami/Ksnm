namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class Float128Tests
    {
        [TestMethod()]
        public void IsZero()
        {
            Float128 float128 = new Float128();
            Assert.IsTrue(float128.IsZero);
            float128 = new Float128(1);
            Assert.IsFalse(float128.IsZero);
        }
        [TestMethod()]
        public void IsNegative()
        {
            Float128 float128 = new Float128();
            Assert.IsFalse(float128.IsNegative);
            float128 = new Float128(0x8000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsTrue(float128.IsNegative);
        }
        [TestMethod()]
        public void Sign()
        {
            Float128 float128 = new Float128();
            Assert.AreEqual(1, float128.Sign);
            float128 = new Float128(0x8000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.AreEqual(-1, float128.Sign);
        }
        [TestMethod()]
        public void IsInfinity()
        {
            Float128 float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsTrue(float128.IsInfinity);
            float128 = new Float128(0x7FFE_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsInfinity);
        }
        [TestMethod()]
        public void IsNaN()
        {
            Float128 float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsNaN);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsNaN);
        }
        [TestMethod()]
        public void IsSubnormal()
        {
            Float128 float128 = new Float128(0x0000_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsSubnormal);
            float128 = new Float128(0x0000_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsSubnormal);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsSubnormal);
        }
        [TestMethod()]
        public void IsNormal()
        {
            Float128 float128 = new Float128(0x0001_0000_0000_0000, 0x0000_0000_0000_0001);
            Assert.IsTrue(float128.IsNormal);
            float128 = new Float128(0x7FFF_0000_0000_0000, 0x0000_0000_0000_0000);
            Assert.IsFalse(float128.IsNormal);
        }
    }
}