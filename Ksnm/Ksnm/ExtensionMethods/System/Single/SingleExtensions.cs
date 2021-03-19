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

namespace Ksnm.ExtensionMethods.System.Single
{
    /// <summary>
    /// Singleの拡張メソッド
    /// </summary>
    public static class SingleExtensions
    {
        #region 定数
        /// <summary>
        /// 符号部のビット数
        /// </summary>
        public const int SignLength = 1;
        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 8;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const uint ExponentBitMask = (1u << ExponentLength) - 1;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 127;
        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 23;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const uint MantissaBitMask = (1u << MantissaLength) - 1;
        /// <summary>
        /// 指数形式ではなく小数形式に変換するためのフォーマット
        /// </summary>
        static readonly string DecimalFormat = "0." + new string('#', 51);
        #endregion 定数

        #region Is*
        /// <summary>
        /// 正数なら true を返します。
        /// </summary>
        public static bool IsPositive(this float value)
        {
            return value.GetSignBits() == 0;
        }
        /// <summary>
        /// 負数なら true を返します。
        /// </summary>
        public static bool IsNegative(this float value)
        {
            return value.GetSignBits() == 1;
        }
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
            var bits = value.ToUInt32Bits();
            var exponent = _GetExponent(bits);
            // 指数が負数なら少数確定
            if (exponent < 0)
            {
                return false;
            }
            // 少数が0なら整数
            var fractionalBits = _GetFractionalBits(bits);
            return fractionalBits == 0;
        }
        #endregion Is*

        #region To*
        /// <summary>
        /// 32 ビット符号付き整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 32 ビット符号付き整数。</returns>
        public static int ToInt32Bits(this float value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 32 ビット符号なし整数に変換します。
        /// </summary>
        /// <returns>value と等価のビット値を持つ 32 ビット符号なし整数。</returns>
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
        #endregion To*

        #region 各部情報の取得
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        public static byte GetSignBits(this float value)
        {
            return _GetSignBits(value.ToUInt32Bits());
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
        /// 指数部を取得
        /// </summary>
        public static ushort GetExponentBits(this float value)
        {
            return _GetExponentBits(value.ToUInt32Bits());
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        public static int GetExponent(this float value)
        {
            return _GetExponent(value.ToUInt32Bits());
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public static uint GetMantissaBits(this float value)
        {
            return _GetMantissaBits(value.ToUInt32Bits());
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        public static uint GetMantissa(this float value)
        {
            return _GetMantissa(value.ToUInt32Bits());
        }
        /// <summary>
        /// 少数部を取得
        /// </summary>
        public static uint GetFractionalBits(this float value)
        {
            return _GetFractionalBits(value.ToUInt32Bits());
        }
        #endregion 各部情報の取得

        #region 各部情報の取得(内部用)
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        private static byte _GetSignBits(uint bits)
        {
            return (byte)(bits >> (ExponentLength + MantissaLength));
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        private static int _GetSign(uint bits)
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
        private static bool _IsNegative(uint bits)
        {
            return _GetSignBits(bits) == 1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        private static ushort _GetExponentBits(uint bits)
        {
            return (ushort)((bits >> MantissaLength) & ExponentBitMask);
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        private static int _GetExponent(uint bits)
        {
            return _GetExponentBits(bits) - ExponentBias;
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        private static uint _GetMantissaBits(uint bits)
        {
            return bits & MantissaBitMask;
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        private static uint _GetMantissa(uint bits)
        {
            // (1u << MantissaLength)は"1."を意味する
            return _GetMantissaBits(bits) | (1u << MantissaLength);
        }
        /// <summary>
        /// 少数部を取得
        /// </summary>
        private static uint _GetFractionalBits(uint bits)
        {
            var shift = _GetExponent(bits);
            if (shift > 0)
            {
                return (_GetMantissaBits(bits) << shift) & MantissaBitMask;
            }
            return _GetMantissaBits(bits);
        }
        #endregion 各部情報の取得(内部用)

    }
}
