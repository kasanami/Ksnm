/*
The zlib License

Copyright (c) 2019 Takahiro Kasanami

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
    /// <summary>
    /// Decimalの拡張メソッド
    /// </summary>
    public static class Decimal
    {
        #region ToClamped*
        /// <summary>
        /// 指定した System.Decimal の値を、16 ビット符号付き整数の範囲に制限して変換します。
        /// </summary>
        public static sbyte ToClampedSByte(this decimal self)
        {
            return decimal.ToSByte(self.Clamp(sbyte.MinValue, sbyte.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、16 ビット符号なし整数の範囲に制限して変換します。
        /// </summary>
        public static byte ToClampedByte(this decimal self)
        {
            return decimal.ToByte(self.Clamp(byte.MinValue, byte.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、16 ビット符号付き整数の範囲に制限して変換します。
        /// </summary>
        public static short ToClampedInt16(this decimal self)
        {
            return decimal.ToInt16(self.Clamp(short.MinValue, short.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、16 ビット符号なし整数の範囲に制限して変換します。
        /// </summary>
        public static ushort ToClampedUInt16(this decimal self)
        {
            return decimal.ToUInt16(self.Clamp(ushort.MinValue, ushort.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、32 ビット符号付き整数の範囲に制限して変換します。
        /// </summary>
        public static int ToClampedInt32(this decimal self)
        {
            return decimal.ToInt32(self.Clamp(int.MinValue, int.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、32 ビット符号なし整数の範囲に制限して変換します。
        /// </summary>
        public static uint ToClampedUInt32(this decimal self)
        {
            return decimal.ToUInt32(self.Clamp(uint.MinValue, uint.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、64 ビット符号付き整数の範囲に制限して変換します。
        /// </summary>
        public static long ToClampedInt64(this decimal self)
        {
            return decimal.ToInt64(self.Clamp(long.MinValue, long.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、64 ビット符号なし整数の範囲に制限して変換します。
        /// </summary>
        public static ulong ToClampedUInt64(this decimal self)
        {
            return decimal.ToUInt64(self.Clamp(ulong.MinValue, ulong.MaxValue));
        }
        #endregion ToClamped*
    }
}
