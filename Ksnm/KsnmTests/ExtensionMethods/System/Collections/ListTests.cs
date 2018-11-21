using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.ExtensionMethods.System.Collections.Tests
{
    [TestClass()]
    public class ListTests
    {
        [TestMethod()]
        public void GetLastTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            Assert.AreEqual(2, sample.GetLast());
        }

        [TestMethod()]
        public void RemoveLastTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            sample.RemoveLast();
            Assert.AreEqual(1, sample.GetLast());
        }

        [TestMethod()]
        public void EqualsLastTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            Assert.IsTrue(sample.EqualsLast(2));
        }

        [TestMethod()]
        public void SetAllTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            sample.SetAll(3);
            Assert.AreEqual(3, sample[0]);
            Assert.AreEqual(3, sample[1]);
            Assert.AreEqual(3, sample[2]);
        }

        [TestMethod()]
        public void SwapTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            sample.Swap(0, 2);
            Assert.AreEqual(2, sample[0]);
            Assert.AreEqual(1, sample[1]);
            Assert.AreEqual(0, sample[2]);
        }

        [TestMethod()]
        public void PopTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            Assert.AreEqual(1, sample.Pop(1));
            Assert.AreEqual(0, sample[0]);
            Assert.AreEqual(2, sample[1]);
        }

        [TestMethod()]
        public void RemoveRangeTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            sample.RemoveRange(1);
            Assert.AreEqual(1, sample.Count);
        }
    }
}