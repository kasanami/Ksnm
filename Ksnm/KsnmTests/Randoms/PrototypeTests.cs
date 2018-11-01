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
            Random random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextTest(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            Random random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            NextDoubleTest(random);
        }
    }
}