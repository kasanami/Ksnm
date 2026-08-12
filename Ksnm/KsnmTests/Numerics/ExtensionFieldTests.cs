using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class ExtensionFieldTests
    {
        [TestMethod()]
        public void Test()
        {
        }
        [TestMethod()]
        public void Test4()
        {
            ExtensionField4 a = ExtensionField4.X;
            ExtensionField4 b = new ExtensionField4(3);

            Assert.AreEqual(1, a + b);
            Assert.AreEqual(1, a - b);
            Assert.AreEqual(1, a * b);
            Assert.AreEqual(3, a / b);
        }
    }
}
