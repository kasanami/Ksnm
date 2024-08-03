/*
The zlib License

Copyright (c) 2019ｰ2021 Takahiro Kasanami

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

using Ksnm.ExtensionMethods.System.Comparable;
using System.Linq;
using System.Text;

namespace Ksnm.ExtensionMethods.System.Decimal
{
    /// <summary>
    /// Decimalの拡張メソッド
    /// </summary>
    public static class DecimalExtensions
    {
        #region Is*
        /// <summary>
        /// 正数なら true を返します。
        /// </summary>
        public static bool IsPositive(this decimal value)
        {
            return value.GetSignBits() == 0;
        }
        /// <summary>
        /// 負数なら true を返します。
        /// </summary>
        public static bool IsNegative(this decimal value)
        {
            return value.GetSignBits() != 0;
        }
        /// <summary>
        /// 整数なら true を返します。
        /// </summary>
        public static bool IsInteger(this decimal value)
        {
            return value.GetFractional() == 0;
        }
        /// <summary>
        /// 偶数なら true を返す。
        /// </summary>
        public static bool IsEven(this decimal value)
        {
            return (value % 2) == 0;
        }
        /// <summary>
        /// 奇数なら true を返す。
        /// </summary>
        public static bool IsOdd(this decimal value)
        {
            return (value % 2) != 0;
        }
        #endregion Is*

        #region Get*
        /// <summary>
        /// 符号ビットを取得
        /// </summary>
        /// <returns>正なら 0 を返す。負なら 1 を返す。</returns>
        public static byte GetSignBits(this decimal value)
        {
            uint bits = (uint)decimal.GetBits(value)[3];
            return (byte)(bits >> 31);
        }
        /// <summary>
        /// 符号を取得
        /// </summary>
        /// <returns>正なら +1 を返す。負なら -1 を返す。</returns>
        public static int GetSign(this decimal value)
        {
            if (value < 0)
            {
                return -1;
            }
            return +1;
        }
        /// <summary>
        /// 指数部を取得
        /// </summary>
        /// <returns>指数部のビット</returns>
        public static byte GetExponentBits(this decimal value)
        {
            int[] bits = decimal.GetBits(value);
            return (byte)((bits[3] >> 16) & 0x7F);
        }
        /// <summary>
        /// 指数を取得
        /// </summary>
        /// <returns>10の基数に累乗する際の指数</returns>
        public static int GetExponent(this decimal value)
        {
            return -value.GetExponentBits();
        }
        /// <summary>
        /// 仮数部を取得
        /// </summary>
        /// <returns>仮数部のビット</returns>
        public static int[] GetMantissaBits(this decimal value)
        {
            int[] bits = decimal.GetBits(value);
            return bits.Take(3).ToArray();
        }
        /// <summary>
        /// 仮数を取得
        /// </summary>
        /// <returns>仮数のみの値</returns>
        public static decimal GetMantissa(this decimal value)
        {
            int[] bits = decimal.GetBits(value);
            return new decimal(bits[0], bits[1], bits[2], false, 0);
        }
        /// <summary>
        /// 小数部を取得
        /// </summary>
        public static decimal GetFractional(this decimal value)
        {
            return decimal.Remainder(value, 1);
        }
        #endregion Get*

        #region To*
        /// <summary>
        /// 指定した System.Decimal のインスタンスの値を、それと等価のバイナリ形式に変換します。
        /// </summary>
        public static int[] ToBits(this decimal value)
        {
            return decimal.GetBits(value);
        }

        #region ToClamped*
        /// <summary>
        /// 指定した System.Decimal の値を、8 ビット符号付き整数の範囲に制限して変換します。
        /// </summary>
        public static sbyte ToClampedSByte(this decimal self)
        {
            return decimal.ToSByte(self.Clamp(sbyte.MinValue, sbyte.MaxValue));
        }
        /// <summary>
        /// 指定した System.Decimal の値を、8 ビット符号なし整数の範囲に制限して変換します。
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
        /// <summary>
        /// 16進数の文字列に変換する
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <param name="separator">4バイト毎に挿入される区切り文字。"_"アンダーバーや、","コンマなど</param>
        public static string ToHexadecimalString(this decimal value, string separator)
        {
            var builder = new StringBuilder(35);
            var bits = decimal.GetBits(value);
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                builder.Append(bits[i].ToString("X8"));
                if (i != 0)
                {
                    builder.Append(separator);
                }
            }
            return builder.ToString();
        }
        #endregion To*

        /// <summary>
        /// 指定した数の自然 (底 e) 対数を返します。
        /// NOTE:計算回数を増やしても精度があまり上がらない、count=100000くらいがベター
        /// </summary>
        /// <param name="value">対数を求める対象の数値。</param>
        /// <param name="count">計算回数</param>
        /// <returns></returns>
        public static decimal Log(this decimal value, int count)
        {
            // 1未満の場合
            if (value < 1)
            {
                return -1;
            }
            // 1の場合
            if (value == 1)
            {
                return 0;
            }
            // 積分
            var integral = 0m;
            var div = (value - 1) / count;
            // 積分していく
            for (int i = 1; i < count - 1; i++)
            {
                integral += 1m / (i * div + 1);
            }

            integral += 1m / 2m;
            integral += 1m / value;

            return integral * div;
        }
        /// <summary>
        /// 最下位桁を丸めます。
        /// 小数点以下が無い場合は何もしません。
        /// </summary>
        /// <returns>丸められた値。</returns>
        public static decimal RoundBottom(this decimal value)
        {
            var decimals = value.GetExponentBits() - 1;
            if (decimals >= 0)
            {
                return decimal.Round(value, decimals);
            }
            return value;
        }
    }
}
