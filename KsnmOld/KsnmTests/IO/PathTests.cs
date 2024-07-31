using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.IO.Tests
{
    [TestClass()]
    public class PathTests
    {
        [TestMethod()]
        public void ToSafeFileNameTest()
        {
            var sample = "\n\r\t\\/:*?\"<>|";
            Assert.AreEqual("＼／：＊？”＜＞｜", Path.ToSafeFileName(sample));
        }
    }
}
