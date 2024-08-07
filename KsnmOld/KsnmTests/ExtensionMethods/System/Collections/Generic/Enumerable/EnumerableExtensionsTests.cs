﻿using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable.Tests
{
    [TestClass()]
    public class EnumerableExtensionsTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            var ary = new[] { 1 };
            Assert.AreEqual(1, ary.Product());
            ary = new[] { 1, 2 };
            Assert.AreEqual(2, ary.Product());
            ary = new[] { 1, 2, 3 };
            Assert.AreEqual(6, ary.Product());
        }

        [TestMethod()]
        public void ToDebugStringTest()
        {
            var ary = new[] { 1, 2, 3 };
            Assert.AreEqual("[3]={1,2,3}", ary.ToDebugString());
            var list = new List<int>(ary);
            Assert.AreEqual("[3]={1,2,3}", list.ToDebugString());
        }

        [TestMethod()]
        public void ToDebugString2Test()
        {
            global::System.Globalization.CultureInfo cultureInfo = new global::System.Globalization.CultureInfo("ja-JP");
            var ary = new[] { 1, 2, 3 };
            Assert.AreEqual("[3]={0001,0002,0003}", ary.ToDebugString("X4", cultureInfo));
            var list = new List<int>(ary);
            Assert.AreEqual("[3]={0001,0002,0003}", list.ToDebugString("X4", cultureInfo));
        }

        [TestMethod()]
        public void ToJoinedStringTest()
        {
            var ary = new int[] { };
            Assert.AreEqual("", ary.ToJoinedString(","));
            ary = new int[] { 1 };
            Assert.AreEqual("1", ary.ToJoinedString(","));
            ary = new[] { 1, 2 };
            Assert.AreEqual("1,2", ary.ToJoinedString(","));
            ary = new[] { 1, 2, 3 };
            Assert.AreEqual("1,2,3", ary.ToJoinedString(","));

            var list = new List<int>(ary);
            Assert.AreEqual("1,2,3", list.ToJoinedString(","));
        }

        [TestMethod()]
        public void ToJoinedStringTest2()
        {
            var ary = new int[] { };
            Assert.AreEqual("", ary.ToJoinedString(",", "X2"));
            ary = new int[] { 0x12 };
            Assert.AreEqual("12", ary.ToJoinedString(",", "X2"));
            ary = new int[] { 0x12, 0x34 };
            Assert.AreEqual("12,34", ary.ToJoinedString(",", "X2"));
            ary = new[] { 1, 2, 3 };
            Assert.AreEqual("01,02,03", ary.ToJoinedString(",", "X2"));

            var list = new List<int>(ary);
            Assert.AreEqual("01,02,03", list.ToJoinedString(",", "X2"));
        }

        [TestMethod()]
        public void ConcatTest()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 1, 2, 3 });
            list.Add(new int[] { 4, 5, 6 });
            list.Add(new int[] { 7, 8, 9 });

            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = list.Concat();
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ForTest()
        {
            var sample = new[] { 1, 2, 3 };
            var sum = 0;
            sample.For((i, x) => sum += x);
            Assert.AreEqual(6, sum);
        }

        [TestMethod()]
        public void ForEachTest()
        {
            var sample = new[] { 1, 2, 3 };
            var sum = 0;
            sample.ForEach((x) => sum += x);
            Assert.AreEqual(6, sum);
        }
    }
}
