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
            Random random = new Xorshift128();
            NextTest(random);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            Random random = new Xorshift128();
            NextDoubleTest(random);
        }
    }
}