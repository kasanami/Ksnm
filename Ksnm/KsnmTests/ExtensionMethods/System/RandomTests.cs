using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Randoms;

namespace Ksnm.ExtensionMethods.System.Tests
{
    [TestClass()]
    public class RandomTests
    {
        /// <summary>
        /// テスト回数
        /// 全ての生成値をテストできないが、しょうがない。
        /// </summary>
        const int TestCount = 10000000;

        [TestMethod()]
        public void NextLongTest()
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.NextLong();
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value < long.MaxValue);
                if (value == 0) { isMinAppeared = true; }
                if (value == long.MaxValue - 1) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void NextLongTest(long max)
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.NextLong(max);
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value < max);
                if (value == 0) { isMinAppeared = true; }
                if (value == max - 1) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }

        [TestMethod()]
        [DataRow(0, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 3)]
        [DataRow(-1, 1)]
        [DataRow(-2, 2)]
        [DataRow(-3, 3)]
        public void NextLongTest(long min, long max)
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.NextLong(max);
                Assert.IsTrue(value >= min);
                Assert.IsTrue(value < max);
                if (value == min) { isMinAppeared = true; }
                if (value == max - 1) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }

        [TestMethod()]
        public void UnitIntervalTest()
        {
            var random = new IncrementRandom(0, TestCount);
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.UnitInterval();
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value <= 1);
            }
        }

        [TestMethod()]
        [DataRow(0, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 3)]
        [DataRow(-1, 1)]
        [DataRow(-2, 2)]
        [DataRow(-3, 3)]
        public void RangeTest_Int(int min, int max)
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(min, max);
                Assert.IsTrue(value >= min);
                Assert.IsTrue(value <= max);
                if (value == min) { isMinAppeared = true; }
                if (value == max) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }

        [TestMethod()]
        [DataRow(0, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 3)]
        [DataRow(-1, 1)]
        [DataRow(-2, 2)]
        [DataRow(-3, 3)]
        public void RangeTest_Float(float min, float max)
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(min, max);
                Assert.IsTrue(value >= min);
                Assert.IsTrue(value <= max);
                if (value == min) { isMinAppeared = true; }
                if (value == max) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }

        [TestMethod()]
        [DataRow(0, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 3)]
        [DataRow(-1, 1)]
        [DataRow(-2, 2)]
        [DataRow(-3, 3)]
        public void RangeTest_Double(double min, double max)
        {
            var random = new IncrementRandom(0, TestCount);
            var isMinAppeared = false;
            var isMaxAppeared = false;
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(min, max);
                Assert.IsTrue(value >= min);
                Assert.IsTrue(value <= max);
                if (value == min) { isMinAppeared = true; }
                if (value == max) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared);
            Assert.IsTrue(isMaxAppeared);
        }
    }
}