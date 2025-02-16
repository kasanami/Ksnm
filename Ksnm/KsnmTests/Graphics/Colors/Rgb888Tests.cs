namespace Ksnm.Graphics.Colors.Tests
{
    [TestClass()]
    public class Rgb888Tests
    {
        [TestMethod()]
        public void OperationsTest()
        {
            // ==
            {
                Rgb888 rgb1 = Rgb888.Red;
                Rgb888 rgb2 = Rgb888.Red;
                Assert.IsTrue(rgb1 == rgb2);
                rgb1 = new(1, 1, 1);
                rgb2 = new(1, 1, 1);
                Assert.IsTrue(rgb1 == rgb2);
                rgb1 = new(0, 0, 0);
                rgb2 = new(0, 0, 0);
                Assert.IsTrue(rgb1 == rgb2);
                rgb2 = new(1, 0, 0);
                Assert.IsFalse(rgb1 == rgb2);
                rgb2 = new(0, 1, 0);
                Assert.IsFalse(rgb1 == rgb2);
                rgb2 = new(0, 0, 1);
                Assert.IsFalse(rgb1 == rgb2);
            }
            // !=
            {
                Rgb888 rgb1 = Rgb888.Red;
                Rgb888 rgb2 = Rgb888.Red;
                Assert.IsFalse(rgb1 != rgb2);
                rgb1 = new(1, 1, 1);
                rgb2 = new(1, 1, 1);
                Assert.IsFalse(rgb1 != rgb2);
                rgb1 = new(0, 0, 0);
                rgb2 = new(0, 0, 0);
                Assert.IsFalse(rgb1 != rgb2);
                rgb2 = new(1, 0, 0);
                Assert.IsTrue(rgb1 != rgb2);
                rgb2 = new(0, 1, 0);
                Assert.IsTrue(rgb1 != rgb2);
                rgb2 = new(0, 0, 1);
                Assert.IsTrue(rgb1 != rgb2);
            }
            // +
            {
                Rgb888 rgb1 = new(0.2, 0.4, 0.6);
                Rgb888 rgb2 = new(0.2, 0.2, 0.2);
                Rgb888 rgb3 = rgb1 + rgb2;
                Assert.AreEqual(new(0.4, 0.6, 0.8), rgb3);
            }
            // -
            {
                Rgb888 rgb1 = new(0.2, 0.4, 0.6);
                Rgb888 rgb2 = new(0.2, 0.2, 0.2);
                Rgb888 rgb3 = rgb1 - rgb2;
                Assert.AreEqual(new(0.0, 0.2, 0.4), rgb3);
            }
        }
        [TestMethod()]
        public void ToRgbFFFTest()
        {
            Rgb888 rgb888 = Rgb888.Red;
            RgbFFF rgbFFF = RgbFFF.Red;
            Assert.AreEqual(rgbFFF, rgb888.ToRgbFFF());
        }
    }
}