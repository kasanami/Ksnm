namespace Ksnm.Text.Tests
{
    [TestClass()]
    public class StringTests
    {
        [TestMethod()]
        public void HammingDistanceTest()
        {
            Assert.AreEqual(0, String.HammingDistance("", ""));
            Assert.AreEqual(0, String.HammingDistance("1", "1"));
            Assert.AreEqual(0, String.HammingDistance("10", "10"));

            Assert.AreEqual(1, String.HammingDistance("0", ""));
            Assert.AreEqual(1, String.HammingDistance("1", ""));
            Assert.AreEqual(1, String.HammingDistance("0", "1"));
            Assert.AreEqual(1, String.HammingDistance("1", "0"));
            Assert.AreEqual(1, String.HammingDistance("00", "01"));
            Assert.AreEqual(1, String.HammingDistance("00", "10"));

            Assert.AreEqual(2, String.HammingDistance("00", ""));
            Assert.AreEqual(2, String.HammingDistance("11", ""));
            Assert.AreEqual(2, String.HammingDistance("00", "11"));
            Assert.AreEqual(2, String.HammingDistance("11", "00"));
        }
    }
}