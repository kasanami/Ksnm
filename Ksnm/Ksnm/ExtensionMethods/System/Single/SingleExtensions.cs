/*
The zlib License

Copyright (c) 2017-2019 Takahiro Kasanami

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

namespace Ksnm.ExtensionMethods.System.Single
{
    /// <summary>
    /// Singleの拡張メソッド
    /// </summary>
    public static class SingleExtensions
    {
        /// <summary>
        /// 指数形式ではなく小数形式に変換するためのフォーマット
        /// </summary>
        static readonly string DecimalFormat = "0." + new string('#', 51);
        /// <summary>
        /// 負または正の無限大と評価されるかどうかを示す値を返します。
        /// </summary>
        public static bool IsInfinity(this float value)
        {
            return float.IsInfinity(value);
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this float value)
        {
            return value == (int)value;
        }
        /// <summary>
        /// 32 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 32 ビット符号付き整数。</returns>
        public static int ToInt32Bits(this float value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 32 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 32 ビット符号なし整数。</returns>
        public static uint ToUInt32Bits(this float value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt32(bytes, 0);
        }
        /// <summary>
        /// 指数形式ではなく小数形式の文字列に変換する。
        /// </summary>
        public static string ToDecimalString(this float value)
        {
            return value.ToString(DecimalFormat);
        }
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        public static byte GetSignBits(this float value)
        {
            return (byte)(value.ToUInt32Bits() >> 31);
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        public static int GetSign(this float value)
        {
            if (value.IsNegative())
            {
                return -1;
            }
            return +1;
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        public static bool IsNegative(this float value)
        {
            return value.GetSignBits() == 1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        public static ushort GetExponentBits(this float value)
        {
            return (ushort)((value.ToUInt32Bits() >> 23) & 0xFF);
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        public static int GetExponent(this float value)
        {
            return value.GetExponentBits() - 127;
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public static uint GetMantissaBits(this float value)
        {
            return value.ToUInt32Bits() & 0x007F_FFFF;
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        public static uint GetMantissa(this float value)
        {
            return value.GetMantissaBits() | 0x0080_0000;
        }
    }
}
