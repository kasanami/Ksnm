﻿/*
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
using System;
using System.Numerics;
using static Ksnm.Math;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 公式
    /// </summary>
    public static class Formula
    {
        /// <summary>
        /// ライプニッツの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static double LeibnizFormula(int count)
        {
            double sum = 0.0;
            for (var i = 0; i < count; i++)
            {
                if (IsEven(i))
                {
                    sum += 1.0 / (2.0 * i + 1);
                }
                else
                {
                    sum -= 1.0 / (2.0 * i + 1);
                }
            }
            return sum;
        }
        /// <summary>
        /// ライプニッツの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static decimal LeibnizFormulaForDecimal(int count)
        {
            decimal sum = 0;
            for (var i = 0; i < count; i++)
            {
                if (IsEven(i))
                {
                    sum += 1m / (2m * i + 1);
                }
                else
                {
                    sum -= 1m / (2m * i + 1);
                }
            }
            return sum;
        }
        /// <summary>
        /// ライプニッツの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <param name="precision">精度(小数点以下の桁数)</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static BigDecimal LeibnizFormula(int count, int precision)
        {
            if (precision < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(precision)}={precision} 範囲を超えています。");
            }
            BigDecimal sum = new BigDecimal(0, 0, -precision);
            BigDecimal one = new BigDecimal(1, 0, -precision);
            BigDecimal two = new BigDecimal(2, 0, -precision);
            for (var i = 0; i < count; i++)
            {
                if (IsEven(i))
                {
                    sum += one / (two * i + one);
                }
                else
                {
                    sum -= one / (two * i + one);
                }
            }
            return sum;
        }
        /// <summary>
        /// ウォリスの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <returns>PI/2(円周率の2分の1)</returns>
        public static double WallisFormula(int count)
        {
            count++;// 1から開始するのでインクリメント
            double product = 1;
            for (var i = 1; i < count; i++)
            {
                product *= (2.0 * i) / (2.0 * i - 1);
                product *= (2.0 * i) / (2.0 * i + 1);
            }
            return product;
        }
        /// <summary>
        /// ウォリスの公式
        /// NOTE:10000000回計算した結果:3.1415925750499818074560633566
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <returns>PI/2(円周率の2分の1)</returns>
        public static decimal WallisFormulaForDecimal(int count)
        {
            count++;// 1から開始するのでインクリメント
            decimal product = 1;
            for (var i = 1; i < count; i++)
            {
                product *= (2m * i) / (2m * i - 1);
                product *= (2m * i) / (2m * i + 1);
            }
            return product;
        }
        /// <summary>
        /// ウォリスの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <param name="precision">精度(小数点以下の桁数)</param>
        /// <returns>PI/2(円周率の2分の1)</returns>
        public static BigDecimal WallisFormula(int count, int precision)
        {
            if (precision < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(precision)}={precision} 範囲を超えています。");
            }
            BigDecimal one = new BigDecimal(1, 0, -precision);
            BigDecimal two = new BigDecimal(2, 0, -precision);
            count++;// 1から開始するのでインクリメント
            BigDecimal product = one;
            for (var i = 1; i < count; i++)
            {
                product *= (two * i) / (two * i - one);
                product *= (two * i) / (two * i + one);
            }
            return product;
        }
        /// <summary>
        /// ラマヌジャンの円周率の公式
        /// ※最下位の桁は丸められるため意図しない値の可能性があります。
        /// NOTE:小数点以下100桁の場合、計算回数は 13 回以上は結果が同じになる。
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <param name="precision">精度(小数点以下の桁数)</param>
        /// <returns>円周率の逆数</returns>
        public static BigDecimal RamanujansPiFormula(int count, int precision)
        {
            if (precision < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(precision)}={precision} 範囲を超えています。");
            }
            BigDecimal temp = 0;
            for (int i = 0; i < count; i++)
            {
                var bi = (BigInteger)i;
                var anumerator = new BigDecimal(Factorial(4 * bi) * (1103 + 26390 * i), 0, -precision);
                var denominator = BigInteger.Pow(BigInteger.Pow(4, i) * BigInteger.Pow(99, i) * Factorial(bi), 4);
                temp += anumerator / denominator;
            }
            // 2√2/99^2 の結果
            BigDecimal SquareRootOfTwo = BigDecimal.Sqrt(2, precision);
            BigDecimal multiplicand = (2 * SquareRootOfTwo) / 9801;
            var product = multiplicand * temp;
            return product;
        }
        /// <summary>
        /// マチンの公式
        /// </summary>
        /// <param name="count">計算回数。1未満を設定すると0を返す。</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static double MachinsFormula(int count)
        {
            double sum = 0;
            for (int k = 1; k <= count; k++)
            {
                sum +=
                    4 *
                    (System.Math.Pow(-1, k + 1) / (2 * k - 1)) *
                    System.Math.Pow(1.0 / 5, 2 * k - 1) +
                    (System.Math.Pow(-1, k) / (2 * k - 1)) *
                    System.Math.Pow(1.0 / 239, 2 * k - 1);
            }
            return sum;
        }
        /// <summary>
        /// マチンの公式
        /// ※最下位の桁は丸められるため意図しない値の可能性があります。
        /// NOTE:小数点以下100桁の場合、計算回数は 71 回以上は結果が同じになる。
        /// </summary>
        /// <param name="count">計算回数。1未満を設定すると0を返す。</param>
        /// <param name="precision">精度(小数点以下の桁数)</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static BigDecimal MachinsFormula(int count, int precision)
        {
            if (precision < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(precision)}={precision} 範囲を超えています。");
            }
            var sum = BigDecimal.MakeZero(precision);
            var one = BigDecimal.MakeOne(precision);
            var one_5 = one / 5;
            var one_239 = one / 239;
            var dividend1 = BigDecimal.MakeOne(precision);// 分子：+1と-1を交互に繰り返す
            var dividend2 = BigDecimal.MakeOne(precision);// 分子：dividend1の正負が逆の値
            for (int k = 1; k <= count; k++, dividend1 *= -1)
            {
#if false
                sum +=
                    4 *
                    (BigDecimal.Pow(-one, k + 1) / (2 * k - 1)) *
                    BigDecimal.Pow(one_5, 2 * k - 1) +
                    (BigDecimal.Pow(-one, k) / (2 * k - 1)) *
                    BigDecimal.Pow(one_239, 2 * k - 1);
#else
                var divisor = 2 * k - 1;// 分母：奇数が順番に入る。
                dividend2 = -dividend1;
                sum +=
                    4 *
                    (dividend1 / divisor) *
                    BigDecimal.Pow(one_5, divisor) +
                    (dividend2 / divisor) *
                    BigDecimal.Pow(one_239, divisor);
#endif
            }
            return sum;
        }
    }
}