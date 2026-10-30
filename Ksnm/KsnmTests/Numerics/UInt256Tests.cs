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
        public void FromUInt128()
        {
            UInt256 a = UInt256.FromUInt128(UInt128.One);

            Assert.AreEqual(1u, a.U0);
            Assert.AreEqual(0u, a.U1);
            Assert.AreEqual(0u, a.U2);
            Assert.AreEqual(0u, a.U3);

            a = UInt256.FromUInt128(new UInt128(0x123456789ABCDEF0, 0x0123456789ABCDEF));

            Assert.AreEqual(0x0123456789ABCDEFu, a.U0);
            Assert.AreEqual(0x123456789ABCDEF0u, a.U1);
            Assert.AreEqual(0u, a.U2);
            Assert.AreEqual(0u, a.U3);

            var b = new UInt128(0x123456789ABCDEF0, 0x0123456789ABCDEF);
            a = (UInt256)b;

            Assert.AreEqual(0x0123456789ABCDEFu, a.U0);
            Assert.AreEqual(0x123456789ABCDEF0u, a.U1);
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
        [TestMethod()]
        public void Operator()
        {
            {
                UInt256 a = 1;
                UInt128 b = 1;
                for (int i = 0; i < 127; i++)
                {
                    Assert.AreEqual<UInt256>((UInt256)b, a, $"Failed at shift {i}");
                    a <<= 1;
                    b <<= 1;
                }
                for (int i = 0; i < 127; i++)
                {
                    Assert.AreEqual<UInt256>((UInt256)b, a, $"Failed at shift {i}");
                    a >>= 1;
                    b >>= 1;
                }
            }
            {
                UInt256 one256 = 1;
                UInt128 one128 = 1;
                for (int i = 0; i < 127; i++)
                {
                    UInt256 a = one256 << i;
                    UInt128 b = one128 << i;
                    Assert.AreEqual<UInt256>((UInt256)b, a, $"Failed at shift {i}");
                }
                for (int i = 0; i < 127; i++)
                {
                    UInt256 a = one256 >> i;
                    UInt128 b = one128 >> i;
                    Assert.AreEqual<UInt256>((UInt256)b, a, $"Failed at shift {i}");
                }
            }
        }
        [TestMethod()]
        public void Divide()
        {
            for (int a = 0; a <= 100; a++)
            {
                for (int b = 1; b <= 100; b++)
                {
                    UInt256 ua = new UInt256((ulong)a);
                    UInt256 ub = new UInt256((ulong)b);
                    UInt256 result = UInt256.Divide(ua, ub, out UInt256 remainder);
                    Assert.AreEqual<UInt256>(new UInt256((ulong)(a / b)), result, $"Failed for a={a}, b={b}");
                    Assert.AreEqual<UInt256>(new UInt256((ulong)(a % b)), remainder, $"Failed for a={a}, b={b}");
                }
            }
        }
    }
}