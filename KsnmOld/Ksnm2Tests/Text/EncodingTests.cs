using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Text.Tests
{
    [TestClass()]
    public class EncodingTests
    {
        [TestMethod()]
        public void PropertiesTest()
        {
            // .NET Coreで Shift-JISを扱うためのおまじない
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var expectedEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            var actualEncoding = Encoding.Shift_JIS;
            Assert.AreEqual(expectedEncoding, actualEncoding);
        }
    }
}
