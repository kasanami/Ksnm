/*
The zlib License

Copyright (c) 2017 Takahiro Kasanami

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
namespace Ksnm.ExtensionMethods.System
{
    using global::System;
    using Original = global::System.Double;
    /// <summary>
    /// Doubleの拡張メソッド
    /// </summary>
    public static class Double_
    {
        /// <summary>
        /// 指定した数値が負または正の無限大と評価されるかどうかを示す値を返します。
        /// </summary>
        public static bool IsInfinity(this Original value)
        {
            return Original.IsInfinity(value);
        }
        /// <summary>
        /// 64 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 64 ビット符号付き整数。</returns>
        public static long ToInt64Bits(this Original value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 64 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価の値を持つ 64 ビット符号なし整数。</returns>
        public static ulong ToUInt64Bits(this Original value)
        {
            return (ulong)BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        public static byte GetSignBits(this Original value)
        {
            return (byte)(value.ToUInt64Bits() >> 63);
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        public static ushort GetExponentBits(this Original value)
        {
            return (ushort)((value.ToUInt64Bits() >> 52) & 0x7FF);
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public static ulong GetFractionBits(this Original value)
        {
            return value.ToUInt64Bits() & 0x000F_FFFF_FFFF_FFFF;
        }
#if false// セットできるわけではないので、一時的に非公開
        /// <summary>
        /// 符号ビットを設定
        /// </summary>
        public static Original SetSignBits(this Original value, int sign)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x7FFF_FFFF_FFFF_FFFF;
            bits |= (ulong)sign << 63;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 指数部を設定
        /// </summary>
        public static Original SetExponentBits(this Original value, int exponent)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x800F_FFFF_FFFF_FFFF;
            bits |= (ulong)exponent << 52;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 仮数部を設定
        /// </summary>
        public static Original SetFractionBits(this Original value, ulong fraction)
        {
            var bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            bits &= 0xFFF0_0000_0000_0000;
            bits |= fraction;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
#endif
    }
}
