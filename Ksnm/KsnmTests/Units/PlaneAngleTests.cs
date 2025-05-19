using Ksnm.Units.NonSI;
using Ksnm.Units.SI;

namespace Ksnm.Units.Tests
{
    [TestClass()]
    public class PlaneAngleTests
    {
        [TestMethod()]
        public void PlaneAngleTest()
        {
            // 度→ラジアン
            var radian = (Radian<double>)new Degree<double>(180.0);
            // ラジアン→度
            var degree = (Degree<double>)radian;

            Assert.AreEqual(180.0, degree.Value);
            Assert.AreEqual(double.Pi, radian.Value);
        }
    }
}