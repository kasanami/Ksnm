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
using System.Linq;
using Self = System.Int32;
using Bits = System.UInt32;
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Int32
{
    /// <summary>
    /// Int32の拡張メソッド
    /// </summary>
    public static class Int32Extensions
    {
        #region Is*
        /// <summary>
        /// 正数なら true を返します。0 なら false を返します。
        /// </summary>
        public static bool IsPositive(this Self value)
        {
            return value > 0;
        }
        /// <summary>
        /// 負数なら true を返します。0 なら false を返します。
        /// </summary>
        public static bool IsNegative(this Self value)
        {
            return value < 0;
        }
        /// <summary>
        /// 負または正の無限大と評価されるかどうかを示す値を返します。
        /// </summary>
        public static bool IsInfinity(this Self value)
        {
            return false;
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this Self value)
        {
            return true;
        }
        /// <summary>
        /// 0 なら true を返します。
        /// </summary>
        public static bool IsZero(this Self value)
        {
            return value == 0;
        }
        #endregion Is*

        #region To*
        /// <summary>
        /// 32 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 32 ビット符号付き整数。</returns>
        public static int ToInt32Bits(this Self value)
        {
            return value;
        }
        /// <summary>
        /// 32 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 32 ビット符号なし整数。</returns>
        public static Bits ToUInt32Bits(this Self value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt32(bytes, 0);
        }
        /// <summary>
        /// 指数形式ではなく小数形式の文字列に変換する。
        /// </summary>
        public static string ToDecimalString(this Self value)
        {
            return value.ToString();
        }
        /// <summary>
        /// 符号なし整数に変換します。
        /// </summary>
        private static Bits _ToBits(this Self value)
        {
            return ToUInt32Bits(value);
        }
        /// <summary>
        /// 指定した数を、数の列に変換する。上位の位が先頭になります。
        /// </summary>
        /// <param name="value">変換する数値</param>
        /// <param name="radix">基数</param>
        /// <returns>数の列</returns>
        public static IEnumerable<int> ToDigits(this Self value, int radix)
        {
            return ToReversedDigits(value, radix).Reverse();
        }
        /// <summary>
        /// 指定した数を、数の列に変換する。1の位が先頭になります。
        /// </summary>
        /// <param name="value">変換する数値</param>
        /// <param name="radix">基数</param>
        /// <returns>数の列</returns>
        public static IEnumerable<int> ToReversedDigits(this Self value, int radix)
        {
            if (radix <= 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(radix)}={radix}");
            }
            if (value == 0)
            {
                yield return 0;
                yield break;
            }
            while (value != 0)
            {
                yield return value % radix;
                value /= radix;
            }
        }
        #endregion To*
    }
}