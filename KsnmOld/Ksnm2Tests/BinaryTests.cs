﻿using Ksnm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ksnm.Tests
{
    [TestClass()]
    public class BinaryTests
    {

        [TestMethod()]
        public void MaxValuesTest()
        {
            for (int i = 0; i < Binary.MaxValues.Count; i++)
            {
                var maxValue = System.Math.Pow(2, i) - 1;
                Assert.IsTrue(Binary.MaxValues[i] == maxValue);
            }
        }

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
        public void IsPowerOfTwoTest()
        {
            Assert.IsTrue(Binary.IsPowerOfTwo(0));
            Assert.IsTrue(Binary.IsPowerOfTwo(1));
            Assert.IsTrue(Binary.IsPowerOfTwo(0b10));
            Assert.IsTrue(Binary.IsPowerOfTwo(0b100));
            Assert.IsTrue(Binary.IsPowerOfTwo(0b1000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x10));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x100));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x10_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x100_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x10_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x100_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x10_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x100_0000_0000_0000));
            Assert.IsTrue(Binary.IsPowerOfTwo(0x1000_0000_0000_0000));
            Assert.IsFalse(Binary.IsPowerOfTwo(0b11));
            Assert.IsFalse(Binary.IsPowerOfTwo(0b101));
            Assert.IsFalse(Binary.IsPowerOfTwo(0b1001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x11));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x101));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x10_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x100_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x10_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x100_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x10_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x100_0000_0000_0001));
            Assert.IsFalse(Binary.IsPowerOfTwo(0x1000_0000_0000_0001));
        }

        [TestMethod]
        public void CeilingPowerOfTwoTest()
        {
            Assert.AreEqual(0, Binary.CeilingPowerOfTwo(0));

            Assert.AreEqual(0b0001, Binary.CeilingPowerOfTwo(0b0001));
            Assert.AreEqual(0b0010, Binary.CeilingPowerOfTwo(0b0010));
            Assert.AreEqual(0b0100, Binary.CeilingPowerOfTwo(0b0011));
            Assert.AreEqual(0b0100, Binary.CeilingPowerOfTwo(0b0100));
            Assert.AreEqual(0b1000, Binary.CeilingPowerOfTwo(0b0101));
            Assert.AreEqual(0b1000, Binary.CeilingPowerOfTwo(0b0110));
            Assert.AreEqual(0b1000, Binary.CeilingPowerOfTwo(0b0111));
            Assert.AreEqual(0b1000, Binary.CeilingPowerOfTwo(0b1000));

            Assert.AreEqual(0b0001_0000, Binary.CeilingPowerOfTwo(0b0000_1001));
            Assert.AreEqual(0b0010_0000, Binary.CeilingPowerOfTwo(0b0001_0001));
            Assert.AreEqual(0b0100_0000, Binary.CeilingPowerOfTwo(0b0010_0001));
            Assert.AreEqual(0b1000_0000, Binary.CeilingPowerOfTwo(0b0100_0001));

            Assert.AreEqual(0x8000_0000, Binary.CeilingPowerOfTwo(0x4000_0001u));
            Assert.AreEqual(0x8000_0000_0000_0000, Binary.CeilingPowerOfTwo(0x4000_0000_0000_0001uL));
        }

        [TestMethod]
        public void FloorPowerOfTwoTest()
        {
            Assert.AreEqual(0, Binary.FloorPowerOfTwo(0));

            Assert.AreEqual(0b0001, Binary.FloorPowerOfTwo(0b0001));
            Assert.AreEqual(0b0010, Binary.FloorPowerOfTwo(0b0010));
            Assert.AreEqual(0b0010, Binary.FloorPowerOfTwo(0b0011));
            Assert.AreEqual(0b0100, Binary.FloorPowerOfTwo(0b0100));
            Assert.AreEqual(0b0100, Binary.FloorPowerOfTwo(0b0101));
            Assert.AreEqual(0b0100, Binary.FloorPowerOfTwo(0b0110));
            Assert.AreEqual(0b0100, Binary.FloorPowerOfTwo(0b0111));
            Assert.AreEqual(0b1000, Binary.FloorPowerOfTwo(0b1000));

            Assert.AreEqual(0b0000_1000, Binary.FloorPowerOfTwo(0b0000_1001));
            Assert.AreEqual(0b0001_0000, Binary.FloorPowerOfTwo(0b0001_0001));
            Assert.AreEqual(0b0010_0000, Binary.FloorPowerOfTwo(0b0010_0001));
            Assert.AreEqual(0b0100_0000, Binary.FloorPowerOfTwo(0b0100_0001));
            Assert.AreEqual(0b1000_0000, Binary.FloorPowerOfTwo(0b1000_0001));

            Assert.AreEqual(0x8000_0000, Binary.FloorPowerOfTwo(0x8000_0001u));
            Assert.AreEqual(0x8000_0000_0000_0000, Binary.FloorPowerOfTwo(0x8000_0000_0000_0001uL));
            Assert.AreEqual(0x8000_0000_0000_0000, Binary.FloorPowerOfTwo(ulong.MaxValue));
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

        [TestMethod()]
        public void RotateLeftTest()
        {
            ulong result;
            for (int i = 0; i < 32; i++)
            {
                result = Binary.RotateLeft(0, i);
                Assert.AreEqual(0ul, result);
            }
            for (int i = 0; i < 32; i++)
            {
                result = Binary.RotateLeft(0xFFFFFFFF, i);
                Assert.AreEqual(0xFFFFFFFFul, result);
            }
            for (int i = 0; i < 64; i++)
            {
                result = Binary.RotateLeft(0, i);
                Assert.AreEqual(0ul, result);
            }
            for (int i = 0; i < 64; i++)
            {
                result = Binary.RotateLeft(0xFFFFFFFFFFFFFFFF, i);
                Assert.AreEqual(0xFFFFFFFFFFFFFFFF, result);
            }

            Assert.AreEqual(0b10000000000000000000000000000001u, Binary.RotateLeft(0b10000000000000000000000000000001, 0));
            Assert.AreEqual(0b00000000000000000000000000000011u, Binary.RotateLeft(0b10000000000000000000000000000001, 1));
            Assert.AreEqual(0b00000000000000000000000000000110u, Binary.RotateLeft(0b10000000000000000000000000000001, 2));
            Assert.AreEqual(0b01100000000000000000000000000000u, Binary.RotateLeft(0b10000000000000000000000000000001, 30));
            Assert.AreEqual(0b11000000000000000000000000000000u, Binary.RotateLeft(0b10000000000000000000000000000001, 31));

            Assert.AreEqual(0b1000000000000000000000000000000000000000000000000000000000000001u, Binary.RotateLeft(0b1000000000000000000000000000000000000000000000000000000000000001, 0));
            Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000000000000011u, Binary.RotateLeft(0b1000000000000000000000000000000000000000000000000000000000000001, 1));
            Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000000000000110u, Binary.RotateLeft(0b1000000000000000000000000000000000000000000000000000000000000001, 2));
            Assert.AreEqual(0b0110000000000000000000000000000000000000000000000000000000000000u, Binary.RotateLeft(0b1000000000000000000000000000000000000000000000000000000000000001, 62));
            Assert.AreEqual(0b1100000000000000000000000000000000000000000000000000000000000000u, Binary.RotateLeft(0b1000000000000000000000000000000000000000000000000000000000000001, 63));
        }

        [TestMethod()]
        public void RotateRightTest()
        {
            ulong result;
            for (int i = 0; i < 32; i++)
            {
                result = Binary.RotateRight(0, i);
                Assert.AreEqual(0ul, result);
            }
            for (int i = 0; i < 32; i++)
            {
                result = Binary.RotateRight(0xFFFFFFFF, i);
                Assert.AreEqual(0xFFFFFFFFul, result);
            }
            for (int i = 0; i < 64; i++)
            {
                result = Binary.RotateRight(0ul, i);
                Assert.AreEqual(0ul, result);
            }
            for (int i = 0; i < 64; i++)
            {
                result = Binary.RotateRight(0xFFFFFFFFFFFFFFFF, i);
                Assert.AreEqual(0xFFFFFFFFFFFFFFFF, result);
            }

            Assert.AreEqual(0b10000000000000000000000000000001u, Binary.RotateRight(0b10000000000000000000000000000001, 0));
            Assert.AreEqual(0b11000000000000000000000000000000u, Binary.RotateRight(0b10000000000000000000000000000001, 1));
            Assert.AreEqual(0b01100000000000000000000000000000u, Binary.RotateRight(0b10000000000000000000000000000001, 2));
            Assert.AreEqual(0b00000000000000000000000000000110u, Binary.RotateRight(0b10000000000000000000000000000001, 30));
            Assert.AreEqual(0b00000000000000000000000000000011u, Binary.RotateRight(0b10000000000000000000000000000001, 31));

            Assert.AreEqual(0b1000000000000000000000000000000000000000000000000000000000000001u, Binary.RotateRight(0b1000000000000000000000000000000000000000000000000000000000000001, 0));
            Assert.AreEqual(0b1100000000000000000000000000000000000000000000000000000000000000u, Binary.RotateRight(0b1000000000000000000000000000000000000000000000000000000000000001, 1));
            Assert.AreEqual(0b0110000000000000000000000000000000000000000000000000000000000000u, Binary.RotateRight(0b1000000000000000000000000000000000000000000000000000000000000001, 2));
            Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000000000000110u, Binary.RotateRight(0b1000000000000000000000000000000000000000000000000000000000000001, 62));
            Assert.AreEqual(0b0000000000000000000000000000000000000000000000000000000000000011u, Binary.RotateRight(0b1000000000000000000000000000000000000000000000000000000000000001, 63));
        }

        [TestMethod()]
        public void ToInt16Test()
        {
            Assert.AreEqual((Int16)0x1234, Binary.ToInt16(0x12, 0x34));
        }

        [TestMethod()]
        public void ToUInt16Test()
        {
            Assert.AreEqual((UInt16)0x1234, Binary.ToUInt16(0x12, 0x34));
        }

        [TestMethod()]
        public void ToInt32Test()
        {
            Assert.AreEqual((Int32)0x12345678, Binary.ToInt32(0x1234, 0x5678));
        }

        [TestMethod()]
        public void ToUInt32Test()
        {
            Assert.AreEqual((UInt32)0x12345678, Binary.ToUInt32(0x1234, 0x5678));
        }

        [TestMethod()]
        public void ToInt64Test()
        {
            Assert.AreEqual((Int64)0x123456789ABCDEF0, Binary.ToInt64(0x12345678, 0x9ABCDEF0));
        }

        [TestMethod()]
        public void ToUInt64Test()
        {
            Assert.AreEqual((UInt64)0x123456789ABCDEF0, Binary.ToUInt64(0x12345678, 0x9ABCDEF0));
        }

        [TestMethod()]
        public void ToScaledDoubleTest()
        {
            var sample = Binary.ToScaledDouble(0);
            Assert.AreEqual(0.0, sample);
            sample = Binary.ToScaledDouble(ulong.MaxValue);
            Assert.IsTrue(sample > 0.9999999999);
            Assert.IsTrue(sample < 1.0);
        }

        [TestMethod()]
        public void RightShiftTest()
        {
            int[] sample;
            int[] expected;
            sample = new int[] { 0x1111_1111, 0x2222_2222, 0x3333_3333, 0x4444_4444 };
            expected = new int[] { 0x2111_1111, 0x3222_2222, 0x4333_3333, 0x0444_4444 };
            Binary.RightShift(sample, 4);
            Assert.IsTrue(sample.SequenceEqual(expected));

            sample = new int[] { 0x1111_1111, 0x2222_2222, 0x3333_3333, 0x4444_4444 };
            expected = new int[] { 0x2211_1111, 0x3322_2222, 0x4433_3333, 0x0044_4444 };
            Binary.RightShift(sample, 8);
            Assert.IsTrue(sample.SequenceEqual(expected));

            sample = new int[] { 0x1111_1111, 0x2222_2222, 0x3333_3333, 0x4444_4444 };
            expected = new int[] { 0x2222_2222, 0x3333_3333, 0x4444_4444, 0x0000_0000 };
            Binary.RightShift(sample, 32);
            Assert.IsTrue(sample.SequenceEqual(expected));

            sample = new int[] { 0x1111_1111, 0x2222_2222, 0x3333_3333, 0x4444_4444 };
            expected = new int[] { 0x3222_2222, 0x4333_3333, 0x0444_4444, 0x0000_0000 };
            Binary.RightShift(sample, 32 + 4);
            Assert.IsTrue(sample.SequenceEqual(expected));

            sample = new int[] { 0x1111_1111, 0x2222_2222, 0x3333_3333, 0x4444_4444 };
            expected = new int[] { 0x3333_3333, 0x4444_4444, 0x0000_0000, 0x0000_0000 };
            Binary.RightShift(sample, 64);
            Assert.IsTrue(sample.SequenceEqual(expected));
        }
    }
}
