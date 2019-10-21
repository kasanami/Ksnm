using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ksnm.Randoms.Tests
{
    public class Base
    {
        /// <summary>
        /// テスト回数
        /// 全ての生成値をテストできないが、しょうがない。
        /// </summary>
        const int TestCount = 1000000;

        public static void NextTest(Random random)
        {
            // 0以上、MaxValue未満か？
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

        public static void NextUInt32Test(RandomBase random)
        {
            // 0以上、MaxValue未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextUInt32();
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < UInt32.MaxValue);
            }
            // 0以上、2未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextUInt32(2);
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < 2);
            }
        }

        public static void NextInt64Test(RandomBase random)
        {
            // 0以上、MaxValue未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextInt64();
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < Int64.MaxValue);
            }
            // 0以上、2未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextInt64(2);
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < 2);
            }
        }

        public static void NextUInt64Test(RandomBase random)
        {
            // 0以上、MaxValue未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextUInt64();
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < UInt64.MaxValue);
            }
            // 0以上、2未満か？
            for (int i = 0; i < TestCount; i++)
            {
                var sample = random.NextUInt64(2);
                Assert.IsTrue(sample >= 0);
                Assert.IsTrue(sample < 2);
            }
        }

        public static void NextDoubleTest(Random random)
        {
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
