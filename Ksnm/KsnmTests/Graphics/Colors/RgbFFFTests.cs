namespace Ksnm.Graphics.Colors.Tests
{
    [TestClass()]
    public class RgbFFFTests
    {
        [TestMethod()]
        public void ToRgb888Test()
        {
            RgbFFF rgbFFF = RgbFFF.Red;
            Rgb888 rgb888 = Rgb888.Red;
            Assert.AreEqual(rgb888, rgbFFF.ToRgb888());
        }
    }
}