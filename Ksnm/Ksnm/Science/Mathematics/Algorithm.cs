/*
The zlib License

Copyright (c) 2019-2021 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using Ksnm.Numerics;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 数学のアルゴリズム
    /// </summary>
    public static class Algorithm
    {
        /// <summary>
        /// ガウス＝ルジャンドルのアルゴリズム
        /// </summary>
        /// <param name="count">計算回数(3以降は同じ結果になる。)</param>
        /// <returns>円周率</returns>
        public static double GaussLegendre(int count)
        {
            double a = 1.0;
            double b = 1.0 / Sqrt(2.0);
            double t = 1.0 / 4;
            double p = 1.0;
            for (var i = 0; i < count; i++)
            {
                var beforeA = a;
                a = (a + b) / 2;
                b = Sqrt(beforeA * b);
                t -= (p * (a - beforeA) * (a - beforeA));
                p = 2 * p;
            }
            return (a + b) * (a + b) / (4 * t);
        }
        /// <summary>
        /// ガウス＝ルジャンドルのアルゴリズム
        /// ※最下位桁周辺は意図しない値の可能性があります。
        /// NOTE:小数点以下100桁の場合、計算回数は 7 回で良い
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <param name="decimals">精度</param>
        /// <returns>円周率</returns>
        public static BigDecimal GaussLegendreForBigDecimal(int count, int decimals)
        {
            BigDecimal a = 1;
            BigDecimal b = 1 / BigDecimal.Sqrt(2, decimals);
            BigDecimal t = 1 / (BigDecimal)4;
            BigDecimal p = 1;
            for (var i = 0; i < count; i++)
            {
                var beforeA = a;
                a = (a + b) / 2;
                b = BigDecimal.Sqrt(beforeA * b, decimals);
                t -= (p * (a - beforeA) * (a - beforeA));
                p = 2 * p;
            }
            return (a + b) * (a + b) / (4 * t);
        }
        /// <summary>
        /// エラトステネスの篩
        /// 指定された整数以下の全ての素数を発見する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> SieveOfRratosthenes(int value)
        {
            // 2 未満の場合は 0 個
            if (value < 2)
            {
                yield break;
            }
            // 2から処理を開始する
            var values = Enumerable.Range(2, value - 1).ToList();
            for (int i = 2; i <= value; i++)
            {
                // 今回の値が残っていれば値を返す
                if (values.Contains(i))
                {
                    yield return i;
                }
                // 今回の値の倍数を削除
                values.RemoveAll((int v) => v % i == 0);
            }
        }
        /// <summary>
        /// n番目の素数を計算する。
        /// </summary>
        /// <param name="n">1から始まる番号</param>
        /// <returns>素数</returns>
        public static int Prime(int n)
        {
            if (n < 0)
            {
                throw new System.ArgumentOutOfRangeException($"{nameof(n)}={n}");
            }
            var pow2n = Pow(2, n);
            int sum = 0;
            var exp = 1.0 / n;
            for (int m = 1; m <= pow2n; m++)
            {
                double count = _PrimeCountPlusOne(m);
                var addend = (int)Pow(n / count, exp);
                if (addend == 0)
                {
                    break;
                }
                sum += addend;
            }
            return 1 + sum;
        }
        /// <summary>
        /// m 以下の素数の数+1 (Prime の内部用関数)
        /// </summary>
        public static int _PrimeCountPlusOne(int m)
        {
            int sum = 0;
            for (int k = 1; k <= m; k++)
            {
                sum += _PrimeToOne(k);
            }
            return sum;
        }
        /// <summary>
        /// k が素数なら 1 を返し、それ以外は 0 を返す。 (Prime の内部用関数)
        /// ※ 1 は例外で 1 を返す。
        /// </summary>
        public static int _PrimeToOne(int k)
        {
#if true
            if (Theorem.Wilsons(k))
            {
                return 1;
            }
            return 0;
#else
            // k=17 で戻り値が 0 になってしまう。
            var wilsons = (Math.Factorial(k - 1.0) + 1) / k;
            var cos = System.Math.Cos(wilsons * System.Math.PI);
            return (int)System.Math.Floor(cos * cos);
#endif
        }
    }
}