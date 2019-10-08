using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class Fraction32Tests
    {
        const float LoopIncrement = 1.0f / 8;
        [TestMethod()]
        public void CastTest()
        {
            // byte
            for (byte i = 0; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (byte)fr, $"byte i={i} fr={fr}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (byte)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (byte)fr, $"byte i={i} fr={fr}");
            }
            // sbyte
            for (sbyte i = -10; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (sbyte)fr, $"sbyte i={i} fr={fr}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (sbyte)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (sbyte)fr, $"sbyte i={i} fr={fr}");
            }
            // short
            for (short i = -10; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (short)fr, $"short i={i} fr={fr}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (short)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (short)fr, $"short i={i} fr={fr}");
            }
            // ushort
            for (ushort i = 0; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (ushort)fr, $"ushort i={i} fr={fr}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (ushort)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (ushort)fr, $"ushort i={i} fr={fr}");
            }
            // int
            for (int i = -10; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (int)fr, $"int i={i} fr={fr}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (int)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (int)fr, $"int i={i} fr={fr}");
            }
            // uint
            for (uint i = 0; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (uint)fr, $"uint i={i} fr={fr}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (uint)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (uint)fr, $"uint i={i} fr={fr}");
            }
            // long
            for (long i = -10; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (long)fr, $"long i={i} fr={fr}");
            }
            for (double d = -10; d <= 10; d += LoopIncrement)
            {
                var i = (long)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (long)fr, $"long i={i} fr={fr}");
            }
            // ulong
            for (ulong i = 0; i <= 10; i++)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (ulong)fr, $"ulong i={i} fr={fr}");
            }
            for (double d = 0; d <= 10; d += LoopIncrement)
            {
                var i = (ulong)d;
                var fr = (Fraction32)d;
                Assert.AreEqual(i, (ulong)fr, $"ulong i={i} fr={fr}");
            }
            // float
            for (float i = -10; i <= 10; i += LoopIncrement)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (float)fr, $"float i={i} fr={fr}");
            }
            // double
            for (double i = -10; i <= 10; i += LoopIncrement)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (double)fr, $"double i={i} fr={fr}");
            }
            // decimal
            for (decimal i = -10; i <= 10; i += 0.125M)
            {
                var fr = (Fraction32)i;
                Assert.AreEqual(i, (decimal)fr, $"decimal i={i} fr={fr}");
            }
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var a = new Fraction32(1, 2);// 0.5
            var b = new Fraction32(2, 3);// 0.666...
            var c = new Fraction32(4, 5);// 0.8
            var d = new Fraction32(2, 4);// 0.5

            Assert.AreEqual(-1, a.CompareTo(b));
            Assert.AreEqual(-1, a.CompareTo(c));
            Assert.AreEqual(-1, b.CompareTo(c));

            Assert.AreEqual(0, a.CompareTo(a));
            Assert.AreEqual(0, b.CompareTo(b));
            Assert.AreEqual(0, c.CompareTo(c));
            Assert.AreEqual(0, a.CompareTo(d));

            Assert.AreEqual(+1, b.CompareTo(a));
            Assert.AreEqual(+1, c.CompareTo(a));
            Assert.AreEqual(+1, c.CompareTo(b));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = new Fraction32(1, 2);// 0.5
            var b = new Fraction32(2, 3);// 0.666...
            var c = new Fraction32(4, 5);// 0.8
            var d = new Fraction32(2, 4);// 0.5

            Assert.IsTrue(a.Equals(a));
            Assert.IsTrue(a.Equals(d));

            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(b.Equals(a));
            Assert.IsFalse(b.Equals(c));
            Assert.IsFalse(c.Equals(a));
            Assert.IsFalse(c.Equals(b));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = new Fraction32(1, 2);
            var b = new Fraction32(2, 3);

            Assert.IsTrue(a.GetHashCode() == a.GetHashCode());
            Assert.IsFalse(a.GetHashCode() == b.GetHashCode());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var a = new Fraction32(1, 2);
            var b = new Fraction32(2, 3);

            Assert.AreEqual("1/2", a.ToString());
            Assert.AreEqual("2/3", b.ToString());
        }
    }
}
