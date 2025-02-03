namespace Ksnm.Graphics.Colors.Tests
{
    [TestClass()]
    public class Rgb888Tests
    {
        [TestMethod()]
        public void ToRgbFFFTest()
        {
            Rgb888 rgb888 = Rgb888.Red;
            RgbFFF rgbFFF = RgbFFF.Red;
            Assert.AreEqual(rgbFFF, rgb888.ToRgbFFF());
        }
    }
}