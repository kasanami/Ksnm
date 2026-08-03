using Ksnm.Ecc.FiniteField;

namespace Ksnm.Ecc.FiniteField.Tests
{
    [TestClass()]
    public class Gf256Tests
    {
        [TestMethod()]
        public void OperationsTest()
        {
            Assert.AreEqual(new Gf256(0), new Gf256(5) + new Gf256(5));
            Assert.AreEqual(new Gf256(1), new Gf256(5) / new Gf256(5));
            Assert.AreEqual(new Gf256(0), new Gf256(0) * new Gf256(123));
        }
    }
}