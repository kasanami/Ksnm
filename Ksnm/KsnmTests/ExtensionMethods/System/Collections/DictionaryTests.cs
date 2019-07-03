using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Collections.Tests
{
    [TestClass()]
    public class DictionaryTests
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
    }
}