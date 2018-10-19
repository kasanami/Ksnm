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
    public class EnumerableTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            var ary = new[] { 1 };
            Assert.AreEqual(1, ary.Product());
            ary = new[] { 1, 2 };
            Assert.AreEqual(2, ary.Product());
            ary = new[] { 1, 2, 3 };
            Assert.AreEqual(6, ary.Product());
        }
    }
}