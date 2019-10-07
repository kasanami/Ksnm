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
