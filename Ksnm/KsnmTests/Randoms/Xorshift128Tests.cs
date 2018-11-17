using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.Randoms.Tests
{
    [TestClass()]
    public class Xorshift128Tests : Base
    {
        [TestMethod()]
        public void NextTest()
        {
            var random = new Xorshift128();
            NextTest(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            var random = new Xorshift128();
            NextDoubleTest(random);
        }
    }
}