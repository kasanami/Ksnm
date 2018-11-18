using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.Randoms.Tests
{
    [TestClass()]
    public class PrototypeTests : Base
    {
        [TestMethod()]
        public void NextTest()
        {
            var random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextTest(random);
        }

        [TestMethod()]
        public void NextUInt32Test()
        {
            var random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextUInt32Test(random);
        }

        [TestMethod()]
        public void NextInt64Test()
        {
            var random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextInt64Test(random);
        }

        [TestMethod()]
        public void NextUInt64Test()
        {
            var random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextUInt64Test(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            var random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextDoubleTest(random);
        }
    }
}