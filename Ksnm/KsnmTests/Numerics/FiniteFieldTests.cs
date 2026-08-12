using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class FiniteFieldTests
    {
        [TestMethod()]
        public void Test()
        {
            var _1 = new FiniteField<int>(1, 11);
            var _2 = new FiniteField<int>(2, 11);
            var _3 = new FiniteField<int>(3, 11);
            var _4 = new FiniteField<int>(4, 11);
            var _5 = new FiniteField<int>(5, 11);
            var _6 = new FiniteField<int>(6, 11);
            var _7 = new FiniteField<int>(7, 11);
            var _8 = new FiniteField<int>(8, 11);
            Assert.AreEqual(2, (_6 + _7));
            Assert.AreEqual(9, (_4 - _6));
            Assert.AreEqual(9, (_4 + (-_6)));
            Assert.AreEqual(8, (_5 * _6));
            Assert.AreEqual(1, (_7 * _8));
            Assert.AreEqual(1, (_7 / _7));
        }
        [TestMethod()]
        public void Test2()
        {
            FiniteField2 a = 1;
            FiniteField2 b = 1;

            Assert.AreEqual(0, a + b);
            Assert.AreEqual(0, a - b);
            Assert.AreEqual(1, a * b);
        }
        [TestMethod()]
        public void Test3()
        {
            FiniteField3 a = 2;
            FiniteField3 b = 2;

            Assert.AreEqual(1, a + b);
            Assert.AreEqual(0, a - b);
            Assert.AreEqual(1, a * b);
            Assert.AreEqual(1, a / b);
        }
    }
}
