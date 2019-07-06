using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.Randoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Randoms.Tests
{
    [TestClass()]
    public class FixedRandomTests
    {
        /// <summary>
        /// テスト回数
        /// 全ての生成値をテストしようとすると時間がかかりすぎるので程々の回数にする。
        /// </summary>
        const int TestCount = 100;

        [TestMethod()]
        public void GenerateUInt32Test()
        {
            var sample = new FixedRandom(0);
            for (int i = 0; i < TestCount; i++)
            {
                Assert.AreEqual(0U, sample.GenerateUInt32());
            }
            sample.value = 1;
            for (int i = 0; i < TestCount; i++)
            {
                Assert.AreEqual(1U, sample.GenerateUInt32());
            }
        }

        [TestMethod()]
        public void GenerateUInt64Test()
        {
            var sample = new FixedRandom(0);
            for (int i = 0; i < TestCount; i++)
            {
                Assert.AreEqual(0UL, sample.GenerateUInt64());
            }
            sample.value = 1;
            for (int i = 0; i < TestCount; i++)
            {
                Assert.AreEqual(1UL, sample.GenerateUInt64());
            }
        }
    }
}