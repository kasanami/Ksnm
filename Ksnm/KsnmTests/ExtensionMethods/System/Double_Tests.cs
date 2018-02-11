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
    public class Double_Tests
    {
        [TestMethod()]
        public void IsInfinityTest()
        {
            Assert.IsTrue(double.PositiveInfinity.IsInfinity());
            Assert.IsTrue(double.NegativeInfinity.IsInfinity());
            Assert.IsFalse((1.0).IsInfinity());
        }

        [TestMethod()]
        public void GetSignBitsTest()
        {
            double sample = 1;
            Assert.AreEqual<int>(sample.GetSignBits(), 0);
            sample = -1;
            Assert.AreEqual<int>(sample.GetSignBits(), 1);
        }

        [TestMethod()]
        public void GetExponentBitsTest()
        {
            double sample = 1;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1023);
            sample = -1;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1023);

            sample = 2;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1024);
            sample = -2;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1024);

            sample = 0.5;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1022);
            sample = -0.5;
            Assert.AreEqual<int>(sample.GetExponentBits(), 1022);
        }

        [TestMethod()]
        public void GetFractionBitsTest()
        {
            double sample = 1;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0);
            sample = -1;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0);

            sample = 2;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0);
            sample = -2;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0);

            sample = 3;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0x0008_0000_0000_0000);
            sample = -3;
            Assert.AreEqual<ulong>(sample.GetFractionBits(), 0x0008_0000_0000_0000);
        }

    }
}