using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Numerics;
using Ksnm.Units.SI;
using static Ksnm.Units.SIPrefixes;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class SIPrefixesTests
    {
        [TestMethod()]
        public void SIPrefixesTest()
        {
            Assert.AreEqual("1000000000000000000000000", Yotta.ToString());
            Assert.AreEqual("1000000000000000000000", Zetta.ToString());
            Assert.AreEqual("1000000000000000000", Exa.ToString());
            Assert.AreEqual("1000000000000000", Peta.ToString());
            Assert.AreEqual("1000000000000", Tera.ToString());
            Assert.AreEqual("1000000000", Giga.ToString());
            Assert.AreEqual("1000000", Mega.ToString());
            Assert.AreEqual("1000", Kilo.ToString());
            Assert.AreEqual("100", Hecto.ToString());
            Assert.AreEqual("10", Deca.ToString());
            Assert.AreEqual("0.1", Deci.ToString());
            Assert.AreEqual("0.01", Centi.ToString());
            Assert.AreEqual("0.001", Milli.ToString());
            Assert.AreEqual("0.000001", Micro.ToString());
            Assert.AreEqual("0.000000001", Nano.ToString());
            Assert.AreEqual("0.000000000001", Pico.ToString());
            Assert.AreEqual("0.000000000000001", Femto.ToString());
            Assert.AreEqual("0.000000000000000001", Atto.ToString());
            Assert.AreEqual("0.000000000000000000001", Zepto.ToString());
            Assert.AreEqual("0.000000000000000000000001", Yocto.ToString());
        }
    }
}