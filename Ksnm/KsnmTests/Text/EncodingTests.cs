using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Text.Tests
{
    [TestClass()]
    public class EncodingTests
    {
        [TestMethod()]
        public void PropertiesTest()
        {
            var expectedEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            var actualEncoding = Encoding.Shift_JIS;
            Assert.AreEqual(expectedEncoding, actualEncoding);
        }
    }
}
