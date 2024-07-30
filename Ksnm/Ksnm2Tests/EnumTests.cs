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

        [TestMethod()]
        public void ParseTest()
        {
            var A = Enum.Parse<Hoge>("A");
            Assert.AreEqual(Hoge.A, A);
            var B = Enum.Parse<Hoge>("B");
            Assert.AreEqual(Hoge.B, B);
            var C = Enum.Parse<Hoge>("C", true);
            Assert.AreEqual(Hoge.C, C);

            // 例外テスト
            try
            {
                var c = Enum.Parse<Hoge>("c");
                Assert.AreEqual(Hoge.C, c);
            }
            catch (System.ArgumentException) { }
            try
            {
                var c = Enum.Parse<Hoge>("c", false);
                Assert.AreEqual(Hoge.C, c);
            }
            catch (System.ArgumentException) { }

            try
            {
                Enum.Parse<Hoge>(null);
            }
            catch (System.ArgumentNullException) { }
            try
            {
                Enum.Parse<Hoge>("");
            }
            catch (System.ArgumentException) { }
            try
            {
                Enum.Parse<Hoge>("Z");
            }
            catch (System.ArgumentException) { }
        }
    }
}
