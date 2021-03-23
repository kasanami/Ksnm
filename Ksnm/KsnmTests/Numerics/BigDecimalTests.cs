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

            {
                decimal i = 0.123m;
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

        [TestMethod()]
        public void ToStringTest()
        {
            var sample = new BigDecimal(1m);
            Assert.AreEqual("1", sample.ToString());
            sample = new BigDecimal(12m);
            Assert.AreEqual("12", sample.ToString());
            sample = new BigDecimal(123m);
            Assert.AreEqual("123", sample.ToString());
            sample = new BigDecimal(12.3m);
            Assert.AreEqual("12.3", sample.ToString());
            sample = new BigDecimal(1.23m);
            Assert.AreEqual("1.23", sample.ToString());
            sample = new BigDecimal(0.123m);
            Assert.AreEqual("0.123", sample.ToString());
            sample = new BigDecimal(0.0123m);
            Assert.AreEqual("0.0123", sample.ToString());
            sample = new BigDecimal(0.00123m);
            Assert.AreEqual("0.00123", sample.ToString());

            sample = new BigDecimal(123, 1);
            Assert.AreEqual("1230", sample.ToString());
            sample = new BigDecimal(123, 2);
            Assert.AreEqual("12300", sample.ToString());
        }
    }
}