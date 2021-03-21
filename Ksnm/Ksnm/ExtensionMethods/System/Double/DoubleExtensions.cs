/*
The zlib License

Copyright (c) 2017-2021 Takahiro Kasanami

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
using Float = System.Double;
using UInt = System.UInt64;

namespace Ksnm.ExtensionMethods.System.Double
{
    /// <summary>
    /// Doubleの拡張メソッド
    /// </summary>
    public static class DoubleExtensions
    {
        #region 定数
        /// <summary>
        /// 符号部のビット数
        /// </summary>
        public const int SignLength = 1;
        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 11;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const UInt ExponentBitMask = ((UInt)1 << ExponentLength) - 1;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 1023;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const int InfinityExponent = 1024;
        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 52;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const UInt MantissaBitMask = ((UInt)1 << MantissaLength) - 1;
        /// <summary>
        /// 指数形式ではなく小数形式に変換するためのフォーマット
        /// </summary>
        static readonly string DecimalFormat = "0." + new string('#', 338);
        #endregion 定数

        #region Is*
        /// <summary>
        /// 正数なら true を返します。
        /// </summary>
        public static bool IsPositive(this Float value)
        {
            return value.GetSignBits() == 0;
        }
        /// <summary>
        /// 負数なら true を返します。
        /// </summary>
        public static bool IsNegative(this Float value)
        {
            return value.GetSignBits() == 1;
        }
        /// <summary>
        /// 負または正の無限大と評価されるかどうかを示す値を返します。
        /// </summary>
        public static bool IsInfinity(this Float value)
        {
            return Float.IsInfinity(value);
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this Float value)
        {
            // ゼロは整数で確定
            if (value == 0)
            {
                return true;
            }
#if true
            // .NET4.7Releaseビルドで、10000000回にかかる時間：44ミリ秒
            var bits = value._ToBits();
            // 指数がマイナスなら小数確定
            var exponent = _GetExponent(bits);
            if (exponent < 0)
            {
                return false;
            }
            // 無限大はfalse
            if (exponent == InfinityExponent)
            {
                return false;
            }
            // 小数部が0なら整数
            var fractionalBits = _GetFractionalBits(bits);
            return fractionalBits == 0;
#else
            // .NET4.7Releaseビルドで、10000000回にかかる時間：77ミリ秒
            return (value % 1.0) == 0;
#endif
        }
        #endregion Is*

        #region To*
        /// <summary>
        /// 64 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 64 ビット符号付き整数。</returns>
        public static long ToInt64Bits(this Float value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 64 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 64 ビット符号なし整数。</returns>
        public static UInt ToUInt64Bits(this Float value)
        {
            return (UInt)BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// 指数形式ではなく小数形式の文字列に変換する。
        /// </summary>
        public static string ToDecimalString(this Float value)
        {
            return value.ToString(DecimalFormat);
        }
        /// <summary>
        /// 符号なし整数に変換します。
        /// </summary>
        private static UInt _ToBits(this Float value)
        {
            return ToUInt64Bits(value);
        }
        #endregion To*

        #region 各部情報の取得
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        public static byte GetSignBits(this Float value)
        {
            return _GetSignBits(value._ToBits());
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        public static int GetSign(this Float value)
        {
            if (value.IsNegative())
            {
                return -1;
            }
            return +1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        public static ushort GetExponentBits(this Float value)
        {
            return _GetExponentBits(value._ToBits());
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        public static int GetExponent(this Float value)
        {
            return _GetExponent(value._ToBits());
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public static UInt GetMantissaBits(this Float value)
        {
            return _GetMantissaBits(value._ToBits());
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        public static UInt GetMantissa(this Float value)
        {
            return _GetMantissa(value._ToBits());
        }
        /// <summary>
        /// 少数部を取得
        /// </summary>
        public static UInt GetFractionalBits(this Float value)
        {
            return _GetFractionalBits(value._ToBits());
        }
        #endregion 各部情報の取得

        #region 各部情報の取得(内部用)
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        private static byte _GetSignBits(UInt bits)
        {
            return (byte)(bits >> (ExponentLength + MantissaLength));
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        private static int _GetSign(UInt bits)
        {
            if (_IsNegative(bits))
            {
                return -1;
            }
            return +1;
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        private static bool _IsNegative(UInt bits)
        {
            return _GetSignBits(bits) == 1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        private static ushort _GetExponentBits(UInt bits)
        {
            return (ushort)((bits >> MantissaLength) & ExponentBitMask);
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        private static int _GetExponent(UInt bits)
        {
            return _GetExponentBits(bits) - ExponentBias;
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        private static UInt _GetMantissaBits(UInt bits)
        {
            return bits & MantissaBitMask;
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        private static UInt _GetMantissa(UInt bits)
        {
            // ((UInt)1 << MantissaLength)は"1."を意味する
            return _GetMantissaBits(bits) | ((UInt)1 << MantissaLength);
        }
        /// <summary>
        /// 少数部を取得
        /// </summary>
        private static UInt _GetFractionalBits(UInt bits)
        {
            var shift = _GetExponent(bits);
            if (shift > 0)
            {
                return (_GetMantissaBits(bits) << shift) & MantissaBitMask;
            }
            return _GetMantissaBits(bits);
        }
        #endregion 各部情報の取得(内部用)
#if false// セットできるわけではないので、一時的に非公開
        /// <summary>
        /// 符号ビットを設定
        /// </summary>
        public static Float SetSignBits(this Float value, int sign)
        {
            var bits = (BitsType)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x7FFF_FFFF_FFFF_FFFF;
            bits |= (BitsType)sign << 63;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 指数部を設定
        /// </summary>
        public static Float SetExponentBits(this Float value, int exponent)
        {
            var bits = (BitsType)BitConverter.DoubleToInt64Bits(value);
            bits &= 0x800F_FFFF_FFFF_FFFF;
            bits |= (BitsType)exponent << 52;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
        /// <summary>
        /// 仮数部を設定
        /// </summary>
        public static Float SetFractionBits(this Float value, BitsType fraction)
        {
            var bits = (BitsType)BitConverter.DoubleToInt64Bits(value);
            bits &= 0xFFF0_0000_0000_0000;
            bits |= fraction;
            return BitConverter.Int64BitsToDouble((long)bits);
        }
#endif
    }
}