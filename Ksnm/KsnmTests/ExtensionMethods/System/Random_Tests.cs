using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.ExtensionMethods.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksnm.Randoms;

namespace Ksnm.ExtensionMethods.System.Tests
{
    [TestClass()]
    public class Random_Tests
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
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.NextLong();
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value <= long.MaxValue);
            }
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
        public void RangeTest_Int()
        {
            var random = new IncrementRandom(0, TestCount);
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(1, 2);
                Assert.IsTrue(value >= 1);
                Assert.IsTrue(value <= 2);
            }
        }

        [TestMethod()]
        public void RangeTest_Float()
        {
            var random = new IncrementRandom(0, TestCount);
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(1.0f, 2.0f);
                Assert.IsTrue(value >= 1);
                Assert.IsTrue(value <= 2);
            }
        }

        [TestMethod()]
        public void RangeTest_Double()
        {
            var random = new IncrementRandom(0, TestCount);
            for (int i = 0; i < TestCount; i++)
            {
                var value = random.Range(1.0, 2.0);
                Assert.IsTrue(value >= 1);
                Assert.IsTrue(value <= 2);
            }
        }
    }
}