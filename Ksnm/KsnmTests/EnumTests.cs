using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;

namespace Ksnm.Tests
{
    [TestClass()]
    public class EnumTests
    {
        enum Hoge
        {
            A,
            B,
            C,
            D,
        }

        [TestMethod()]
        public void GetValuesTest()
        {
            var hogeValues = Enum.GetValues<Hoge>();
            Assert.AreEqual("A,B,C,D", hogeValues.ToJoinedString(","));
        }
    }
}
