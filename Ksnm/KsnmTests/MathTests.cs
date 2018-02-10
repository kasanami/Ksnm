using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Tests
{
    [TestClass()]
    public class MathTests
    {
        [TestMethod()]
        public void SignTest()
        {
            Assert.AreEqual(Math.Sign(0), 0);
            Assert.AreEqual(Math.Sign(+2), +1);
            Assert.AreEqual(Math.Sign(-2), -1);
        }
    }
}