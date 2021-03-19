using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.ExtensionMethods.System.Double.Tests
{
    [TestClass()]
    public class DoubleExtensionsTests
    {
        [TestMethod()]
        public void IsInfinityTest()
        {
            Assert.IsTrue(double.PositiveInfinity.IsInfinity());
            Assert.IsTrue(double.NegativeInfinity.IsInfinity());
            double sample = 1;
            Assert.IsFalse(sample.IsInfinity());
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
        public void GetMantissaBitsTest()
        {
            double sample = 1;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0);
            sample = -1;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0);

            sample = 2;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0);
            sample = -2;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0);

            sample = 3;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0x0008_0000_0000_0000);
            sample = -3;
            Assert.AreEqual<ulong>(sample.GetMantissaBits(), 0x0008_0000_0000_0000);
        }

        [TestMethod()]
        public void ToDecimalStringTest()
        {
            double sample = 0;
            Assert.AreEqual("0", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 1;
            Assert.AreEqual("1", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = 0.00001;
            Assert.AreEqual("0.00001", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
            sample = double.Epsilon;
            Assert.AreEqual("0.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000494065645841247", sample.ToDecimalString(), Debug.GetFilePathAndLineNumber());
        }

        [TestMethod()]
        public void GetSignTest()
        {
            double sample = 1;
            Assert.AreEqual(+1, sample.GetSign());
            sample = -1;
            Assert.AreEqual(-1, sample.GetSign());
        }

        [TestMethod()]
        public void GetExponentTest()
        {
            double sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = 2;
            Assert.AreEqual(1, sample.GetExponent());
            sample = 4;
            Assert.AreEqual(2, sample.GetExponent());
            sample = 8;
            Assert.AreEqual(3, sample.GetExponent());

            sample = 1;
            Assert.AreEqual(0, sample.GetExponent());
            sample = 0.5;
            Assert.AreEqual(-1, sample.GetExponent());
            sample = 0.25;
            Assert.AreEqual(-2, sample.GetExponent());
            sample = 0.125;
            Assert.AreEqual(-3, sample.GetExponent());
        }

        [TestMethod()]
        public void GetMantissaTest()
        {
            double sample = 1;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = -1;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = 2;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = -2;
            Assert.AreEqual(0x0010_0000_0000_0000UL, sample.GetMantissa());
            sample = 3;
            Assert.AreEqual(0x0018_0000_0000_0000UL, sample.GetMantissa());
            sample = -3;
            Assert.AreEqual(0x0018_0000_0000_0000UL, sample.GetMantissa());
        }

        [TestMethod()]
        public void IsIntegerTest()
        {
            // 以下はtrueになる
            double sample = 1;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MaxValue;
            Assert.IsTrue(sample.IsInteger());
            sample = int.MinValue;
            Assert.IsTrue(sample.IsInteger());
            // 以下は少数を含むためfalse
            sample = 1.1;
            Assert.IsFalse(sample.IsInteger());
            sample = 0.1;
            Assert.IsFalse(sample.IsInteger());
            sample = -0.1;
            Assert.IsFalse(sample.IsInteger());
            sample = double.Epsilon;
            Assert.IsFalse(sample.IsInteger());
        }
    }
}
