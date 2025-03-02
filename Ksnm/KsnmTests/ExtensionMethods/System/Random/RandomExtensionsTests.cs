using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Randoms;

namespace Ksnm.ExtensionMethods.System.Random.Tests
{
    [TestClass()]
    public class RandomExtensionsTests
    {
        /// <summary>
        /// テスト回数
        /// 全ての生成値をテストしようとすると時間がかかりすぎるので程々の回数にする。
        /// </summary>
        const int TestCount = 100000;

        [TestMethod()]
        public void NextLongTest()
        {
            {
                var random = new IncrementRandom(0, TestCount);
                for (int i = 0; i < TestCount; i++)
                {
                    var value = random.NextLong();
                    Assert.IsTrue(value >= 0);
                    Assert.IsTrue(value < long.MaxValue);
                }
            }
            // 最小値・最大値が都合よく出ないので意図して出させる。
            {
                var random = new FixedRandom();
                random.value = 0;
                Assert.AreEqual(0L, random.NextLong(), Debug.GetFilePathAndLineNumber());
                // 内部で % long.MaxValue としているため、最大値が出ない
                //random.value = ulong.MaxValue;
                //Assert.AreEqual(long.MaxValue - 1, random.NextLong(), Debug.GetFilePathAndLineNumber());
            }
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
            Assert.IsTrue(isMinAppeared, $"{nameof(max)}={max}");
            Assert.IsTrue(isMaxAppeared, $"{nameof(max)}={max}");
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
                var value = random.NextLong(min, max);
                Assert.IsTrue(value >= min);
                Assert.IsTrue(value < max);
                if (value == min) { isMinAppeared = true; }
                if (value == max - 1) { isMaxAppeared = true; }
            }
            Assert.IsTrue(isMinAppeared, $"{nameof(min)}={min} {nameof(max)}={max}");
            Assert.IsTrue(isMaxAppeared, $"{nameof(min)}={min} {nameof(max)}={max}");
        }

        [TestMethod()]
        public void UnitIntervalTest()
        {
            {
                var random = new IncrementRandom(0, TestCount);
                for (int i = 0; i < TestCount * 2; i++)
                {
                    var value = random.UnitInterval();
                    Assert.IsTrue(value >= 0, Debug.GetFilePathAndLineNumber());
                    Assert.IsTrue(value <= 1, Debug.GetFilePathAndLineNumber());
                }
            }
            // 最小値・最大値が都合よく出ないので意図して出させる。
            {
                var random = new FixedRandom();
                random.value = 0;
                Assert.AreEqual(0.0, random.UnitInterval(), Debug.GetFilePathAndLineNumber());
                random.value = ulong.MaxValue;
                Assert.AreEqual(1.0, random.UnitInterval(), Debug.GetFilePathAndLineNumber());
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
            {
                var random = new IncrementRandom(0, TestCount);
                for (int i = 0; i < TestCount; i++)
                {
                    var value = random.Range(min, max);
                    Assert.IsTrue(value >= min);
                    Assert.IsTrue(value <= max);
                }
            }
            // 最小値・最大値が都合よく出ないので意図して出させる。
            {
                var random = new FixedRandom();
                random.value = 0;
                Assert.AreEqual(min, random.Range(min, max));
                random.value = ulong.MaxValue;
                Assert.AreEqual(max, random.Range(min, max));
            }
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
            {
                var random = new IncrementRandom(0, TestCount);
                for (int i = 0; i < TestCount; i++)
                {
                    var value = random.Range(min, max);
                    Assert.IsTrue(value >= min);
                    Assert.IsTrue(value <= max);
                }
            }
            // 最小値・最大値が都合よく出ないので意図して出させる。
            {
                var random = new FixedRandom();
                random.value = 0;
                Assert.AreEqual(min, random.Range(min, max));
                random.value = ulong.MaxValue;
                Assert.AreEqual(max, random.Range(min, max));
            }
        }
    }
}