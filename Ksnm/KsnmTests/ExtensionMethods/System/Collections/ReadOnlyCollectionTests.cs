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
    public class ReadOnlyCollectionTests
    {
        [TestMethod()]
        public void ElementAtRandomTest()
        {
            var sample = (new List<int>() { 0, 1, 2 }).AsReadOnly();
            var random = new Ksnm.Randoms.IncrementRandom(0);
            for (int i = 0; i < sample.Count * 2; i++)
            {
                var element = sample.ElementAtRandom(random);
                Assert.IsTrue(sample.Contains(element));
            }
        }

        [TestMethod()]
        public void IndexOfRandomTest()
        {
            var sample = (new List<int>() { 0, 1, 2 }).AsReadOnly();
            var random = new Ksnm.Randoms.IncrementRandom(0);
            for (int i = 0; i < sample.Count * 2; i++)
            {
                var index = sample.IndexOfRandom(random);
                Assert.IsTrue(index >= 0);
                Assert.IsTrue(index < sample.Count);
            }
        }
    }
}