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
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数。1未満を設定すると0を返す。</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static T LeibnizFormula<T>(T tolerance, int terms = 100000000) where T : INumber<T>
        {
            T sum = T.Zero;
            for (var i = 0; i < terms; i++)
            {
                T odd = T.CreateChecked(2 * i + 1);
                T add = T.Zero;
                if (IsEven(i))
                {
                    add = T.One / odd;
                }
                else
                {
                    add = -T.One / odd;
                }
                sum += add;
                if (T.Abs(add) < tolerance)
                {
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// ライプニッツの公式
        /// </summary>
        /// <param name="terms">単項式数。1未満を設定すると0を返す。</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static T LeibnizFormula<T>(int terms = 100000000)
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return LeibnizFormula<T>(T.Epsilon, terms);
        }
        /// <summary>
        /// ウォリスの公式
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数。1未満を設定すると1を返す。</param>
        /// <returns>PI/2(円周率の2分の1)</returns>
        public static T WallisFormula<T>(int terms = 10000000) where T : INumber<T>
        {
            T product = T.One;
            for (var i = 1; i <= terms; i++)
            {
                T numerator = T.CreateChecked(2 * i);
                T denominator = T.CreateChecked(2 * i - 1);
                product *= numerator / denominator;
                denominator = T.CreateChecked(2 * i + 1);
                product *= numerator / denominator;
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
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数。1未満を設定すると0を返す。</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static T MachinsFormula<T>(T tolerance, int terms = 1000)
            where T : INumber<T>, ISignedNumber<T>
        {
            T _1 = T.One;
            T _2 = T.CreateChecked(2);
            T _4 = T.CreateChecked(4);
            T _5 = T.CreateChecked(5);
            T _239 = T.CreateChecked(239);
            T sum = T.Zero;
            T before = T.Zero;
            for (int k = 1; k <= terms; k++)
            {
                var _k = T.CreateChecked(k);
                var odd = 2 * k - 1;
                var _odd = T.CreateChecked(odd);
                var add =
                    _4 *
                    (Pow<T>(T.NegativeOne, k + 1) / _odd) *
                    Pow<T>(_1 / _5, odd) +
                    (Pow<T>(T.NegativeOne, k) / _odd) *
                    Pow<T>(_1 / _239, odd);
                sum += add;
                if (T.Abs(add) < tolerance)
                {
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// マチンの公式
        /// </summary>
        /// <param name="count">計算回数。1未満を設定すると0を返す。</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static T MachinsFormula<T>() where T : IFloatingPointIeee754<T>
        {
            return MachinsFormula(T.Epsilon);
        }
        /// <summary>
        /// チュドノフスキー(Chudnovsky)級数
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>1/PI(円周率の逆数)</returns>
        public static T ChudnovskySeries<T>(T tolerance, int terms = 100)
            where T : INumber<T>
        {
            var sum = T.Zero;
            for (int k = 0; k < terms; k++)
            {
                var add = ChudnovskyTerm<T>(k);
                sum += add;
                if (T.Abs(add) < tolerance)
                {
                    break;
                }
            }
            return T.CreateChecked(12) * sum;
        }
        public static T ChudnovskySeries<T>(int terms = 100)
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return ChudnovskySeries<T>(T.Epsilon, terms);
        }
        /// <summary>
        /// チュドノフスキー級数の項を計算する
        /// </summary>
        /// <param name="k">0から始まる項の番号</param>
        static T ChudnovskyTerm<T>(int k)
            where T : INumber<T>
        {
            BigInteger n1 = BigInteger.Pow(-1, k);
            BigInteger n2 = Factorial<BigInteger>(6 * k);
            BigInteger n3 = new BigInteger(545140134) * k + new BigInteger(13591409);

            T numerator = T.CreateChecked(n1 * n2 * n3);

            BigInteger d1 = Factorial<BigInteger>(3 * k);
            BigInteger d2 = BigInteger.Pow(Factorial<BigInteger>(k), 3);
            T d3 = T.CreateChecked(System.Math.Pow(640320.0, 3.0 * k + 3.0 / 2.0));

            T denominator = T.CreateChecked(d1 * d2) * d3;

            return numerator / denominator;
        }
    }
}