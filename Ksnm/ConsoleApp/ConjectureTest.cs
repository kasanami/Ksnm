﻿using Ksnm.Science.Mathematics;
using Math = Ksnm.Math;

namespace ConsoleApp
{
    /// <summary>
    /// 予想をテスト
    /// </summary>
    public class ConjectureTest
    {
        public static void Run()
        {

            Console.WriteLine("ゴールドバッハ予想");
            if (Conjecture.GoldbachsConjecture(4, 100))
            {
                Console.WriteLine("→4～100の範囲では成り立つ");
            }

            Console.WriteLine("コラッツの問題");
            if (CollatzProblem(100))
            {
                Console.WriteLine("→2～100の範囲では成り立つ");
            }

            Console.WriteLine("ベルトランの仮説");
            if (BertrandsPostulate())
            {
                Console.WriteLine("→成り立つ");
            }

            Console.WriteLine("ルジャンドル予想");
            if (LegendresConjecture())
            {
                Console.WriteLine("→成り立つ");
            }
        }
        /// <summary>
        /// コラッツの問題
        /// n が偶数の場合、n を 2 で割る
        /// n が奇数の場合、n に 3 をかけて 1 を足す
        /// maxValueは2以上を設定する。1以下を設定すると常にfalseを返す
        /// </summary>
        /// <returns>成り立つならtrueを返す</returns>
        static bool CollatzProblem(int maxValue)
        {
            if (maxValue <= 1) { return false; }
            for (int n = 1; n <= maxValue; n++)
            {
                if (_CollatzProblem(n) == false)
                {
                    return false;
                }
            }
            return true;
        }
        static bool _CollatzProblem(int n)
        {
            if (CollatzProblemCache.Contains(n))
            {
                return true;
            }
            List<int> cache = [];
            var n2 = n;
            while (n2 != 1)
            {
                cache.Add(n2);
                n2 = CollatzProblem_Next(n2);
                if (CollatzProblemCache.Contains(n2))
                {
                    break;
                }
            }
            CollatzProblemCache.AddRange(cache);
            return true;
        }
        static int CollatzProblem_Next(int n)
        {
            // 偶数
            if ((n & 1) == 0)
            {
                return n >> 1;
            }
            // 奇数
            return n * 3 + 1;
        }
        static List<int> CollatzProblemCache = [];
        /// <summary>
        /// ベルトランの仮説
        /// 任意の自然数 n に対して、n ＜ p ≤ 2n を満たす素数 p が存在する
        /// </summary>
        /// <returns>成り立つならtrueを返す</returns>
        static bool BertrandsPostulate()
        {
            var rng = Random.Shared;
            for (int i = 0; i < 100; i++)
            {
                int n = rng.Next(1, 1_000);
                if (BertrandsPostulate(n) == false)
                {
                    // ひとつでもfalseならfalse
                    return false;
                }
            }
            // すべてtrue
            return true;
        }
        static bool BertrandsPostulate(int n)
        {
            var maxValue = n * 2;
            for (; n <= maxValue; n++)
            {
                if (Math.IsPrime(n))
                {
                    return true;
                }
            }
            // 一つも素数がなかった
            return false;
        }
        /// <summary>
        /// ルジャンドル予想
        /// 任意の自然数 n について、n^2 と (n + 1)^2 の間には必ず素数が存在するという予想
        /// 2022年現在未解決問題
        /// </summary>
        /// <returns>成り立つならtrueを返す</returns>
        static bool LegendresConjecture()
        {
            var rng = Random.Shared;
            for (int i = 0; i < 100; i++)
            {
                int n = rng.Next(1, 1_000);
                if (LegendresConjecture(n) == false)
                {
                    // ひとつでもfalseならfalse
                    return false;
                }
            }
            // すべてtrue
            return true;
        }
        static bool LegendresConjecture(int n)
        {
            var minValue = n * n;
            var maxValue = (n + 1) * (n + 1);
            for (n = minValue; n <= maxValue; n++)
            {
                if (Math.IsPrime(n))
                {
                    return true;
                }
            }
            // 一つも素数がなかった
            return false;
        }
    }
}