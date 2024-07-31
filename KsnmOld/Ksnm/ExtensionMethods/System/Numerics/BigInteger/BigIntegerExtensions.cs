/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

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
using System;
using bigint = System.Numerics.BigInteger;

namespace Ksnm.ExtensionMethods.System.Numerics.BigInteger
{
    /// <summary>
    /// BigIntegerの拡張メソッド
    /// </summary>
    public static class BigIntegerExtensions
    {
        #region Is*
        /// <summary>
        /// 偶数なら true を返す。
        /// </summary>
        public static bool IsEven(this bigint value)
        {
            return (value % 2) == 0;
        }
        /// <summary>
        /// 奇数なら true を返す。
        /// </summary>
        public static bool IsOdd(this bigint value)
        {
            return (value % 2) != 0;
        }
        #endregion Is*
        /// <summary>
        /// 丸め処理
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <param name="digits">桁数</param>
        /// <param name="midpointRounding">丸め処理の方法</param>
        /// <returns>丸めた後の値</returns>
        public static bigint Round(this bigint value, int digits, MidpointRounding midpointRounding)
        {
            if (digits <= 0)
            {
                return value;
            }
            var divisor = Math.BigIntegerPow10(digits);
            var half = divisor / 2;
            var remainder = value % divisor;
            // 中間を超えている時
            if (remainder > half)
            {
                value += divisor;
            }
            else if (remainder < -half)
            {
                value -= divisor;
            }
            // 桁を減らす
            value /= divisor;
            // 中間の時
            if (remainder == half || remainder == -half)
            {
                if (midpointRounding == MidpointRounding.ToEven)
                {
                    // 偶数に変更する。
                    remainder = (int)(value % 10);// 新1桁目
                                                  // 奇数なら変更する
                    if (remainder.IsEven == false)
                    {
                        if (remainder > 0)
                        {
                            value++;
                        }
                        else
                        {
                            value--;
                        }
                    }
                }
                else if (midpointRounding == MidpointRounding.AwayFromZero)
                {
                    // 普通の四捨五入
                    if (remainder > 0)
                    {
                        value++;
                    }
                    else
                    {
                        value--;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(midpointRounding), $"{midpointRounding }");
                }
            }
            return value;
        }
    }
}