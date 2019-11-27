using Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary.Tests
{
    [TestClass()]
    public class DictionaryExtensionsTests
    {
        [TestMethod()]
        public void AddIfKeyNotExistsTest()
        {
            var sample = new Dictionary<int, int>();
            sample.AddIfKeyNotExists(1, 1);
            Assert.AreEqual(1, sample[1]);

            // すでに有るので値は変わらない
            sample.AddIfKeyNotExists(1, 2);
            Assert.AreEqual(1, sample[1]);
        }

        [TestMethod()]
        public void GetValueOrDefaultTest()
        {
            var sample = new Dictionary<int, int>();
            Assert.AreEqual(0, sample.GetValueOrDefault(1));
            Assert.AreEqual(1, sample.GetValueOrDefault(1, 1));
        }

        [TestMethod()]
        public void OrderByKeyTest()
        {
            var sample = new Dictionary<string, int>()
            {
                {"A", 1 },
                {"C", 2 },
                {"B", 4 },
                {"D", 3 },
            };
            var expected = new Dictionary<string, int>()
            {
                {"A", 1 },
                {"B", 4 },
                {"C", 2 },
                {"D", 3 },
            };
            Assert.IsFalse(expected.SequenceEqual(sample));

            var sorted = sample.OrderByKey();
            Assert.IsTrue(expected.SequenceEqual(sorted));
        }

        [TestMethod()]
        public void OrderByValueTest()
        {
            var sample = new Dictionary<string, int>()
            {
                {"A", 1 },
                {"C", 2 },
                {"B", 4 },
                {"D", 3 },
            };
            var expected = new Dictionary<string, int>()
            {
                {"A", 1 },
                {"C", 2 },
                {"D", 3 },
                {"B", 4 },
            };
            Assert.IsFalse(expected.SequenceEqual(sample));

            var sorted = sample.OrderByValue();
            Assert.IsTrue(expected.SequenceEqual(sorted));
        }

        [TestMethod()]
        public void SequenceEqualTest()
        {
            var sampleA = new Dictionary<string, int>();
            var sampleB = new Dictionary<string, int>();
            Assert.IsTrue(sampleA.SequenceEqual(sampleB));

            sampleA.Add("A", 1);
            sampleB.Add("A", 1);
            Assert.IsTrue(sampleA.SequenceEqual(sampleB));

            sampleA.Add("B", 2);
            sampleB.Add("B", 3);
            Assert.IsFalse(sampleA.SequenceEqual(sampleB));

            sampleA["B"] = 3;
            Assert.IsTrue(sampleA.SequenceEqual(sampleB));

            sampleA.Add("C", 4);
            sampleB.Add("D", 4);
            Assert.IsFalse(sampleA.SequenceEqual(sampleB));
        }
    }
}
