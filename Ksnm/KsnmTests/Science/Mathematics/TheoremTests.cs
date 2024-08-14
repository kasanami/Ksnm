using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Science.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Science.Mathematics.Tests
{
    [TestClass()]
    public class TheoremTests
    {
        [TestMethod()]
        public void WilsonsTest()
        {
            Assert.IsTrue(Theorem.Wilsons(1));// 1 は例外で true となる。
            Assert.IsTrue(Theorem.Wilsons(2));
            Assert.IsTrue(Theorem.Wilsons(3));
            Assert.IsFalse(Theorem.Wilsons(4));
            Assert.IsTrue(Theorem.Wilsons(5));
            Assert.IsFalse(Theorem.Wilsons(6));
            Assert.IsTrue(Theorem.Wilsons(7));
            Assert.IsFalse(Theorem.Wilsons(8));
            Assert.IsFalse(Theorem.Wilsons(9));
            Assert.IsFalse(Theorem.Wilsons(10));
            Assert.IsTrue(Theorem.Wilsons(11));
        }

        [TestMethod()]
        public void PrimeNumberTheoremTest()
        {
            Assert.AreEqual(0, Theorem.PrimeNumberTheorem(1));
            //Assert.AreEqual(1, Theorem.PrimeNumberTheorem(2));
            Assert.AreEqual(2, Theorem.PrimeNumberTheorem(3));
            Assert.AreEqual(2, Theorem.PrimeNumberTheorem(4));
            Assert.AreEqual(3, Theorem.PrimeNumberTheorem(5));
            Assert.AreEqual(3, Theorem.PrimeNumberTheorem(6));
            //Assert.AreEqual(4, Theorem.PrimeNumberTheorem(7));
            //Assert.AreEqual(4, Theorem.PrimeNumberTheorem(8));
            Assert.AreEqual(4, Theorem.PrimeNumberTheorem(9));
            Assert.AreEqual(4, Theorem.PrimeNumberTheorem(10));
        }
    }
}