using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.ExtensionMethods.System.Tests
{
    [TestClass()]
    public class ComparableTests
    {
        [TestMethod()]
        public void ClampTest()
        {
            int sample = 2;
            Assert.AreEqual(1, sample.Clamp(0, 1));
            Assert.AreEqual(3, sample.Clamp(3, 4));
        }
    }
}