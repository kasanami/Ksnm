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
    public class Xorshift128Tests
    {
        /// <summary>
        /// テスト回数
        /// 全ての乱数をテストできないが、しょうがない。
        /// </summary>
        const int TestCount = 10000000;

        [TestMethod()]
        public void NextTest()
        {
            Random random = new Xorshift128();
            // 0以上、int.MaxValue未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.Next();
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < int.MaxValue);
            }
            // 0以上、2未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.Next(2);
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < 2);
            }
            // 1以上、3未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.Next(1, 3);
                Assert.IsTrue(sample >= 1);
                Assert.IsTrue(sample < 3);
            }
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            Random random = new Xorshift128();
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