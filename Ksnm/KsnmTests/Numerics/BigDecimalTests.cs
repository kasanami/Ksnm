using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ksnm.Numerics.Tests
{
    [TestClass()]
    public class BigDecimalTests
    {

        [TestMethod()]
        public void MinimizeExponentTest()
        {
            for (decimal i = 0.01m; i < 100; i *= 10)
            {
                var sample = new BigDecimal(i);
                sample.MinimizeExponent();
                Assert.AreEqual(i, sample.ToDecimal());
            }
        }

        [TestMethod()]
        public void MinimizeMantissaTest()
        {
            for (decimal i = 0.01m; i < 100; i *= 10)
            {
                var sample = new BigDecimal(i);
                sample.MinimizeExponent();
                sample.MinimizeMantissa();
                Assert.AreEqual(i, sample.ToDecimal(), $"i={i} sample={sample}");
            }
        }

        [TestMethod()]
        public void ToDecimalTest()
        {
            for (decimal i = -100; i < 100; i++)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
            for (decimal i = 1; i < 1_000_000m; i *= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
            for (decimal i = 1; i > 0.001m; i /= 2)
            {
                var sample = new BigDecimal(i);
                Assert.AreEqual(i, sample.ToDecimal());
            }
        }

        [TestMethod()]
        public void OperationsTest1()
        {
            var sample = new BigDecimal(1, 2);
            var expected1 = new BigDecimal(1, 2);
            var expected2 = new BigDecimal(-1, 2);
            Assert.AreEqual(expected1, +sample);
            Assert.AreEqual(expected2, -sample);
        }

        [TestMethod()]
        public void OperationsTest2()
        {
            for (decimal i = 0.01m; i < 100; i *= 10)
            {
                var sample = new BigDecimal(i);
                for (decimal j = 0.01m; j < 100; j *= 10)
                {
                    var sample2 = new BigDecimal(j);
                    // +
                    Assert.AreEqual(i + j, (sample + sample2).ToDecimal());
                    // -
                    Assert.AreEqual(i - j, (sample - sample2).ToDecimal());
                    // *
                    Assert.AreEqual(i * j, (sample * sample2).ToDecimal());
                    // /
                    Assert.AreEqual(i / j, (sample / sample2).ToDecimal());
                    // ==
                    Assert.AreEqual(i == j, sample == sample2);
                    // !=
                    Assert.AreEqual(i != j, sample != sample2);
                    // >
                    Assert.AreEqual(i > j, sample > sample2);
                    // <
                    Assert.AreEqual(i < j, sample < sample2);
                    // >=
                    Assert.AreEqual(i >= j, sample >= sample2);
                    // <=
                    Assert.AreEqual(i <= j, sample <= sample2);
                }
            }
        }
    }
}