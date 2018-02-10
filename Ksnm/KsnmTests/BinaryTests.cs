using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Tests
{
    [TestClass()]
    public class BinaryTests
    {

        [TestMethod()]
        public void FillOneFromLeadingOneToLSBTest()
        {
            Assert.AreEqual(Binary.FillOneFromLeadingOneToLSB(0b0001), 0b0001u);
            Assert.AreEqual(Binary.FillOneFromLeadingOneToLSB(0b0010), 0b0011u);
            Assert.AreEqual(Binary.FillOneFromLeadingOneToLSB(0b0101), 0b0111u);
            Assert.AreEqual(Binary.FillOneFromLeadingOneToLSB(0b1010), 0b1111u);
        }

        [TestMethod]
        public void CountOneTest()
        {
            int result;
            result = Binary.CountOne(0b100);
            Assert.AreEqual(result, 1);
            result = Binary.CountOne(0b1001);
            Assert.AreEqual(result, 2);
            result = Binary.CountOne(0b10101);
            Assert.AreEqual(result, 3);
            result = Binary.CountOne(0b1010101);
            Assert.AreEqual(result, 4);
            result = Binary.CountOne(0xFFFF_FFFF);
            Assert.AreEqual(result, 32);
            result = Binary.CountOne(0x7FFF_FFFF);
            Assert.AreEqual(result, 31);
            result = Binary.CountOne(0xFFFF_FFFF_FFFF_FFFF);
            Assert.AreEqual(result, 64);
            result = Binary.CountOne(0x7FFF_FFFF_FFFF_FFFF);
            Assert.AreEqual(result, 63);
        }

        [TestMethod]
        public void CountLeadingZeroTest()
        {
            int result;
            // 8ビット
            for (int i = 0; i <= 8; i++)
            {
                result = Binary.CountLeadingZero((byte)(0x80 >> i));
                Assert.AreEqual(result, i);
            }

            // 16ビット
            for (int i = 0; i <= 16; i++)
            {
                result = Binary.CountLeadingZero((ushort)(0x8000 >> i));
                Assert.AreEqual(result, i);
            }

            // 32ビット
            for (int i = 0; i <= 31; i++)
            {
                result = Binary.CountLeadingZero(0x8000_0000 >> i);
                Assert.AreEqual(result, i);
            }
            // 32ビットシフトは0ビットシフトと同じになるため、for文とは別にする。
            result = Binary.CountLeadingZero(0);
            Assert.AreEqual(result, 32);

            // 64ビット
            for (int i = 0; i <= 63; i++)
            {
                result = Binary.CountLeadingZero(0x8000_0000_0000_0000 >> i);
                Assert.AreEqual(result, i);
            }
            // 64ビットシフトは0ビットシフトと同じになるため、for文とは別にする。
            result = Binary.CountLeadingZero(0L);
            Assert.AreEqual(result, 64);
        }

        [TestMethod]
        public void CountTrainingZeroTest()
        {
            int result;
            for (int i = 0; i <= 31; i++)
            {
                result = Binary.CountTrainingZero((uint)1 << i);
                Assert.AreEqual(result, i);
            }
            // 32ビットシフトは0ビットシフトとおなじになるため、for文とは別にする。
            result = Binary.CountTrainingZero(0);
            Assert.AreEqual(result, 32);
        }

        [TestMethod]
        public void IsPowerOf2Test()
        {
            Assert.IsTrue(Binary.IsPowerOf2(0));
            Assert.IsTrue(Binary.IsPowerOf2(1));
            Assert.IsTrue(Binary.IsPowerOf2(0b10));
            Assert.IsTrue(Binary.IsPowerOf2(0b100));
            Assert.IsTrue(Binary.IsPowerOf2(0b1000));
            Assert.IsTrue(Binary.IsPowerOf2(0x10));
            Assert.IsTrue(Binary.IsPowerOf2(0x100));
            Assert.IsTrue(Binary.IsPowerOf2(0x1000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x10_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x100_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x10_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x100_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x10_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x100_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOf2(0x1000_0000_0000_0000));
            Assert.IsFalse(Binary.IsPowerOf2(0b11));
            Assert.IsFalse(Binary.IsPowerOf2(0b101));
            Assert.IsFalse(Binary.IsPowerOf2(0b1001));
            Assert.IsFalse(Binary.IsPowerOf2(0x11));
            Assert.IsFalse(Binary.IsPowerOf2(0x101));
            Assert.IsFalse(Binary.IsPowerOf2(0x1001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x10_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x100_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x10_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x100_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x10_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x100_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOf2(0x1000_0000_0000_0001));
        }

        [TestMethod]
        public void CeilingPowerOf2Test()
        {
            int result;
            result = Binary.CeilingPowerOf2(0b1);
            Assert.AreEqual(result, 0b1);
            result = Binary.CeilingPowerOf2(0b11);
            Assert.AreEqual(result, 0b100);
            result = Binary.CeilingPowerOf2(0b111);
            Assert.AreEqual(result, 0b1000);
            result = Binary.CeilingPowerOf2(0b101);
            Assert.AreEqual(result, 0b1000);
        }

        [TestMethod]
        public void FloorPowerOf2Test()
        {
            int result;
            result = Binary.FloorPowerOf2(0b1);
            Assert.AreEqual(result, 0b1);
            result = Binary.FloorPowerOf2(0b11);
            Assert.AreEqual(result, 0b10);
            result = Binary.FloorPowerOf2(0b111);
            Assert.AreEqual(result, 0b100);
            result = Binary.FloorPowerOf2(0b101);
            Assert.AreEqual(result, 0b100);
        }

        [TestMethod]
        public void ScaleTest()
        {
            ulong result;
            result = Binary.Scale(0b10101010, 8, 4);
            Assert.AreEqual(result, 0b1010UL);
            result = Binary.Scale(0b10101010, 8, 3);
            Assert.AreEqual(result, 0b101UL);
            result = Binary.Scale(0b1010, 4, 8);
            Assert.AreEqual(result, 0b10101010UL);
            result = Binary.Scale(0b101, 3, 8);
            Assert.AreEqual(result, 0b10110110UL);
        }
    }
}
