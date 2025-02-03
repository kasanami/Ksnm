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
    }
}