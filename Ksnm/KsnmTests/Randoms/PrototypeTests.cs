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
    public class PrototypeTests
    {
        /// <summary>
        /// テスト回数
        /// 全ての乱数をテストできないが、しょうがない。
        /// </summary>
        const int TestCount = 10000000;

        [TestMethod()]
        public void NextTest()
        {
            Random random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            // 0以上、int.MaxValue未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.Next();
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < int.MaxValue);
            }
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            Random random = new Prototype(0xFFFFFFFF, 12345, 1234567);
            // 0.0 以上 1.0 未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextDouble();
                Assert.IsTrue(sample >= 0.0);
                Assert.IsTrue(sample < 1.0);
            }
        }
    }
}