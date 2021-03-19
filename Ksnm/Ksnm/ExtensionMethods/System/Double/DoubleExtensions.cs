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

namespace Ksnm.ExtensionMethods.System.Double
{
    /// <summary>
    /// Doubleの拡張メソッド
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// 指数形式ではなく小数形式に変換するためのフォーマット
        /// </summary>
        static readonly string DecimalFormat = "0." + new string('#', 338);
        /// <summary>
        /// 負または正の無限大と評価されるかどうかを示す値を返します。
        /// </summary>
        public static bool IsInfinity(this double value)
        {
            return double.IsInfinity(value);
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this double value)
        {
            return value == (long)value;
        }
        /// <summary>
        /// 64 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 64 ビット符号付き整数。</returns>
        public static long ToInt64Bits(this double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 64 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 64 ビット符号なし整数。</returns>
        public static ulong ToUInt64Bits(this double value)
        {
            return (ulong)BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 指数形式ではなく小数形式の文字列に変換する。
        /// </summary>
        public static string ToDecimalString(this double value)
        {
            return value.ToString(DecimalFormat);
        }
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        public static byte GetSignBits(this double value)
        {
            return (byte)(value.ToUInt64Bits() >> 63);
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        public static int GetSign(this double value)
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
        public static bool IsNegative(this double value)
        {
            return value.GetSignBits() == 1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        public static ushort GetExponentBits(this double value)
        {
            return (ushort)((value.ToUInt64Bits() >> 52) & 0x7FF);
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        public static int GetExponent(this double value)
        {
            return value.GetExponentBits() - 1023;
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public static ulong GetMantissaBits(this double value)
        {
            return value.ToUInt64Bits() & 0x000F_FFFF_FFFF_FFFF;
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        public static ulong GetMantissa(this double value)
        {
            return value.GetMantissaBits() | 0x0010_0000_0000_0000;
        }
#if false// セットできるわけではないので、一時的に非公開
        /// <summary>
        /// 符号ビットを設定
        /// </summary>
        public static double SetSignBits(this double value, int sign)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x7FFF_FFFF_FFFF_FFFF;
            bits |= (ulong)sign << 63;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 指数部を設定
        /// </summary>
        public static double SetExponentBits(this double value, int exponent)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x800F_FFFF_FFFF_FFFF;
            bits |= (ulong)exponent << 52;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 仮数部を設定
        /// </summary>
        public static double SetFractionBits(this double value, ulong fraction)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0xFFF0_0000_0000_0000;
            bits |= fraction;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
#endif
    }
}
