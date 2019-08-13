using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Tests
{
    using Fixed = FixedPointNumber32Q16;

    [TestClass()]
    public class FixedPointNumber32Q16Tests
    {
        [TestMethod()]
        public void CastTest()
        {
            var expected = new Fixed(123);
            Assert.AreEqual(expected, (byte)123);
            Assert.AreEqual(expected, (sbyte)123);
            Assert.AreEqual(expected, (short)123);
            Assert.AreEqual(expected, (Fixed)(ushort)123);
            expected = new Fixed(32767);
            Assert.AreEqual(expected, (Fixed)32767);
            Assert.AreEqual(expected, (Fixed)32767u);
            Assert.AreEqual(expected, (Fixed)32767L);
            Assert.AreEqual(expected, (Fixed)32767uL);
            Assert.AreEqual(expected, (Fixed)32767f);
            Assert.AreEqual(expected, (Fixed)32767d);
            Assert.AreEqual(expected, (Fixed)32767m);
            expected = new Fixed(-32768);
            Assert.AreEqual(expected, (Fixed)(-32768));
            Assert.AreEqual(expected, (Fixed)(-32768L));
            Assert.AreEqual(expected, (Fixed)(-32768f));
            Assert.AreEqual(expected, (Fixed)(-32768d));
            Assert.AreEqual(expected, (Fixed)(-32768m));
            expected = new Fixed(123, 1 << (Fixed.QBits - 1));
            Assert.AreEqual(expected, (Fixed)(123.5f));
            Assert.AreEqual(expected, (Fixed)(123.5d));
            Assert.AreEqual(expected, (Fixed)(123.5m));
        }
        [TestMethod()]
        public void OperationsTest1()
        {
            var value = (Fixed)123;
            var expected1 = new Fixed(+123);
            var expected2 = new Fixed(-123);
            var expected3 = new Fixed(); expected3.SetBits(~expected1.Bits);
            Assert.AreEqual(expected1, +value, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(expected2, -value, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(expected3, ~value, Debug.GetFilePathAndLineNumber());
        }
        [TestMethod()]
        public void OperationsTest2()
        {
            var n1 = (Fixed)(-1);
            var p0_5 = (Fixed)0.5;
            var p1 = (Fixed)1;
            var p1_5 = (Fixed)1.5;
            var p2 = (Fixed)2;
            var p2_5 = (Fixed)2.5;
            var p3 = (Fixed)3;
            var p4 = (Fixed)4;
            var p6 = (Fixed)6;
            // +
            Assert.AreEqual(p3, p1 + p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p4, p2 + p2, Debug.GetFilePathAndLineNumber());
            // -
            Assert.AreEqual(p1, p3 - p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(n1, p1 - p2, Debug.GetFilePathAndLineNumber());
            // *
            Assert.AreEqual(p2, p1 * p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p6, p2 * p3, Debug.GetFilePathAndLineNumber());
            // /
            Assert.AreEqual(p2, p4 / p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p1_5, p3 / p2, Debug.GetFilePathAndLineNumber());
            // %
            Assert.AreEqual(p1, p3 % p2, Debug.GetFilePathAndLineNumber());
            Assert.AreEqual(p0_5, p2_5 % p2, Debug.GetFilePathAndLineNumber());
            // &
            Assert.AreEqual(p2, p3 & p2, Debug.GetFilePathAndLineNumber());
            // |
            Assert.AreEqual(p3, p1 | p2, Debug.GetFilePathAndLineNumber());
            // ^
            Assert.AreEqual(p2, p3 ^ p1, Debug.GetFilePathAndLineNumber());
            // <<
            Assert.AreEqual(p2, p1 << 1, Debug.GetFilePathAndLineNumber());
            // >>
            Assert.AreEqual(p1, p2 >> 1, Debug.GetFilePathAndLineNumber());
        }
        [TestMethod()]
        public void OperationsTest3()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.IsTrue(value1 != value2);
            Assert.IsTrue(value1 == value3);
            Assert.IsTrue(value1 < value2);
            Assert.IsTrue(value1 <= value2);
            Assert.IsTrue(value2 > value1);
            Assert.IsTrue(value2 >= value1);
        }
        [TestMethod()]
        public void CompareToTest()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.AreEqual(-1, value1.CompareTo(value2));
            Assert.AreEqual(0, value1.CompareTo(value3));
            Assert.AreEqual(+1, value2.CompareTo(value1));
        }
        [TestMethod()]
        public void EqualsTest()
        {
            var value1 = (Fixed)1;
            var value2 = (Fixed)1.1;
            var value3 = (Fixed)1;

            Assert.IsFalse(value1.Equals(value2));
            Assert.IsTrue(value1.Equals(value3));
        }
    }
}
