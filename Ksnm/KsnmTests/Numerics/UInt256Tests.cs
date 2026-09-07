namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class UInt256Tests
    {
        [TestMethod()]
        public void FromUInt64()
        {
            UInt256 a = UInt256.FromUInt64(1);

            Assert.AreEqual(1u, a.U0);
            Assert.AreEqual(0u, a.U1);
            Assert.AreEqual(0u, a.U2);
            Assert.AreEqual(0u, a.U3);

            a = UInt256.FromUInt64(0x123456789ABCDEF0);

            Assert.AreEqual(0x123456789ABCDEF0u, a.U0);
            Assert.AreEqual(0u, a.U1);
            Assert.AreEqual(0u, a.U2);
            Assert.AreEqual(0u, a.U3);
        }
        [TestMethod()]
        public void BitLength()
        {
            UInt256 a = UInt256.FromUInt64(1);
            UInt256 b = UInt256.FromUInt128(UInt128.One);

            Assert.AreEqual(1, a.BitLength);
            Assert.AreEqual(1, b.BitLength);

            a = UInt256.FromUInt64(2);
            b = UInt256.FromUInt128(2);

            Assert.AreEqual(2, a.BitLength);
            Assert.AreEqual(2, b.BitLength);

            a = UInt256.FromUInt64(0x8000_0000_0000_0000);
            b = UInt256.FromUInt128(UInt128.One << 127);

            Assert.AreEqual(64, a.BitLength);
            Assert.AreEqual(128, b.BitLength);
        }
    }
}