using Ksnm.ExtensionMethods.System.Collections.Generic.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.List.Tests
{
    [TestClass()]
    public class ListExtensionsTests
    {
        [TestMethod()]
        public void IndexOfRandomTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            var random = new Ksnm.Randoms.IncrementRandom(0);
            for (int i = 0; i < sample.Count * 2; i++)
            {
                var index = sample.IndexOfRandom(random);
                Assert.IsTrue(index >= 0);
                Assert.IsTrue(index < sample.Count);
            }
        }

        [TestMethod()]
        public void RemoveRangeTest()
        {
            var sample = new List<int>() { 0, 1, 2, 3 };
            var expected = new List<int>() { 0, 1 };
            sample.RemoveRange(2);
            Assert.IsTrue(expected.SequenceEqual(sample));
        }

        [TestMethod()]
        public void RemoveLastTest()
        {
            var sample = new List<int>() { 0, 1, 2 };
            var expected = new List<int>() { 0, 1 };
            sample.RemoveLast();
            Assert.IsTrue(expected.SequenceEqual(sample));
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
        public void IndexOfMaxTest()
        {
            {
                var sample = new int[] { 0, 1, 2 };
                Assert.AreEqual(2, sample.IndexOfMax());
            }
            {
                var sample = new double[] { 0, 1, 2 };
                Assert.AreEqual(2, sample.IndexOfMax());
            }
        }

        [TestMethod()]
        public void IndexOfMinTest()
        {
            {
                var sample = new int[] { 0, 1, 2 };
                Assert.AreEqual(0, sample.IndexOfMin());
            }
            {
                var sample = new double[] { 0, 1, 2 };
                Assert.AreEqual(0, sample.IndexOfMin());
            }
        }
    }
}