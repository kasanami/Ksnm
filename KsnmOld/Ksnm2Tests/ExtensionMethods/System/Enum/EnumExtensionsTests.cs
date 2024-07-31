using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.ExtensionMethods.System.Enum.Tests
{
    [TestClass()]
    public class EnumExtensionsTests
    {
        [Flags]
        enum Hoge
        {
            A = 0b0001,
            B = 0b0010,
            C = 0b0100,
        }
        [TestMethod()]
        public void AllTest()
        {
            Hoge hoge = Hoge.A | Hoge.B;
            Assert.IsTrue(hoge.All(Hoge.A));
            Assert.IsTrue(hoge.All(Hoge.B));
            Assert.IsTrue(hoge.All(Hoge.A, Hoge.B));

            Assert.IsFalse(hoge.All(Hoge.A, Hoge.C));
            Assert.IsFalse(hoge.All(Hoge.B, Hoge.C));
            Assert.IsFalse(hoge.All(Hoge.A, Hoge.B, Hoge.C));

            Assert.IsFalse(hoge.All(Hoge.C));
        }

        [TestMethod()]
        public void AnyTest()
        {
            Hoge hoge = Hoge.A | Hoge.B;
            Assert.IsTrue(hoge.Any(Hoge.A));
            Assert.IsTrue(hoge.Any(Hoge.B));
            Assert.IsTrue(hoge.Any(Hoge.A, Hoge.B));

            Assert.IsTrue(hoge.Any(Hoge.A, Hoge.C));
            Assert.IsTrue(hoge.Any(Hoge.B, Hoge.C));
            Assert.IsTrue(hoge.Any(Hoge.A, Hoge.B, Hoge.C));

            Assert.IsFalse(hoge.Any(Hoge.C));
        }
    }
}