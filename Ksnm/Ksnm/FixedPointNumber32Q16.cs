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
using System;
using System.Runtime.InteropServices;
using Ksnm.ExtensionMethods.System.Double;

namespace Ksnm
{
    /// <summary>
    /// 固定小数点数(全体のビット数32、小数部分のビット数16)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct FixedPointNumber32Q16 : IComparable, IComparable<FixedPointNumber32Q16>, IEquatable<FixedPointNumber32Q16>
    {
        #region 定数
        public const int QBits = 16;
        public readonly static FixedPointNumber32Q16 Zero = new FixedPointNumber32Q16() { integer = 0 };
        public readonly static FixedPointNumber32Q16 One = new FixedPointNumber32Q16() { integer = 1 };
        public readonly static FixedPointNumber32Q16 MinValue = new FixedPointNumber32Q16() { bits = int.MinValue };
        public readonly static FixedPointNumber32Q16 MaxValue = new FixedPointNumber32Q16() { bits = int.MaxValue };
        public readonly static FixedPointNumber32Q16 Epsilon = new FixedPointNumber32Q16() { bits = 1 };
        /// <summary>
        /// 1を表すビット
        /// </summary>
        const int OneBits = 1 << QBits;
        /// <summary>
        /// 0.5を表すビット
        /// </summary>
        const int HalfBits = 1 << (QBits - 1);
        #endregion 定数

        #region フィールド
        /// <summary>
        /// 全体のビット
        /// </summary>
        [FieldOffset(0)]
        int bits;
        /// <summary>
        /// 整数部
        /// </summary>
        [FieldOffset(2)]
        short integer;
        /// <summary>
        /// 小数部
        /// </summary>
        [FieldOffset(0)]
        ushort fractional;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public int Bits { get { return bits; } }
        /// <summary>
        /// 整数部
        /// 注意点：負数のとき、-0.1は-1。-1.1は-2になる。Floor関数を使用して整数にした値と同じ。
        /// </summary>
        public short Integer { get { return integer; } }
        /// <summary>
        /// 小数部
        /// </summary>
        public ushort Fractional { get { return fractional; } }
        #endregion プロパティ

        /// <summary>
        /// 指定した整数で初期化
        /// </summary>
        /// <param name="integer">整数部</param>
        public FixedPointNumber32Q16(short integer)
        {
            bits = 0;
            this.integer = integer;
            fractional = 0;
        }

        /// <summary>
        /// 指定した整数と小数で初期化
        /// </summary>
        /// <param name="integer">整数部</param>
        /// <param name="fractional">小数部</param>
        public FixedPointNumber32Q16(short integer, ushort fractional)
        {
            bits = 0;
            this.integer = integer;
            this.fractional = fractional;
        }

        /// <summary>
        /// 全体のビットを設定
        /// </summary>
        /// <param name="bits">設定するビット</param>
        public void SetBits(int bits)
        {
            this.bits = bits;
        }

        #region ユーティリティ
        /// <summary>
        /// 浮動小数点数値を設定する。
        /// キャストしたほうが高速なので非公開
        /// </summary>
        /// <param name="value">浮動小数点数値</param>
        /// <returns>非数・無限大の場合false それ以外はtrue</returns>
        private bool SetDouble(double value)
        {
            var exponentBits = value.GetExponentBits();
            var fractionBits = value.GetFractionBits();
            if (exponentBits == 0)
            {
                // ゼロか非正規化数
                bits = 0;
            }
            else if (exponentBits == 0x7ff)
            {
                if (fractionBits != 0)
                {
                    // 非数
                    bits = 0;
                }
                else if (value.GetSignBits() == 0)
                {
                    // 正の無限大
                    bits = int.MaxValue;
                }
                else
                {
                    // 負の無限大
                    bits = int.MinValue;
                }
                return false;
            }
            else
            {
                fractionBits |= 0x10_0000_0000_0000;
                var shift = -52 + (exponentBits - 1023);
                shift += QBits;

                if (shift < 0)
                {
                    shift = -shift;
                    bits = (int)(fractionBits >> shift);
                }
                else if (shift > 0)
                {
                    bits = (int)(fractionBits << shift);
                }
                else
                {
                    bits = (int)(fractionBits);
                }
                // 負数に変換
                if (value.GetSignBits() == 1)
                {
                    bits = ~bits + 1;
                }
            }
            return true;
        }

        #endregion ユーティリティ

        #region 数学系関数
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public bool IsEven()
        {
            return (integer & 1) == 0 && fractional == 0;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public bool IsOdd()
        {
            return (integer & 1) == 1 && fractional == 0;
        }
        /// <summary>
        /// 絶対値を取得します。
        /// </summary>
        /// <param name="value">元の値</param>
        /// <returns>絶対値</returns>
        public static FixedPointNumber32Q16 Abs(FixedPointNumber32Q16 value)
        {
            return new FixedPointNumber32Q16() { bits = System.Math.Abs(value.bits) };
        }
        /// <summary>
        /// 指定した値以上の、最小の整数値を返します。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>value 以上の最小の整数値</returns>
        public static FixedPointNumber32Q16 Ceiling(FixedPointNumber32Q16 value)
        {
            if (value.fractional != 0)
            {
                value.integer++;
                value.fractional = 0;
                return value;
            }
            // すでに整数ならそのまま
            return value;
        }
        /// <summary>
        /// 指定した値以下の、最大の整数値を返します。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>value 以下の最大の整数値</returns>
        public static FixedPointNumber32Q16 Floor(FixedPointNumber32Q16 value)
        {
            value.fractional = 0;
            return value;
        }
        /// <summary>
        /// 指定した値を最も近い整数に丸めます。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>値に最も近い整数。2 つの整数の中間にある場合は偶数が返されます。</returns>
        /// <exception cref="System.OverflowException">結果が範囲外</exception>
        public static FixedPointNumber32Q16 Round(FixedPointNumber32Q16 value)
        {
            // 整数ではないときに処理
            if (value.fractional != 0)
            {
                // ちょうど中間のとき
                if (value.fractional == HalfBits)
                {
                    // 奇数のとき
                    if ((value.integer & 1) == 1)
                    {
                        value.bits += HalfBits;
                    }
                    else
                    {
                        value.bits -= HalfBits;
                    }
                    value.fractional = 0;
                }
                else
                {
                    value.bits += HalfBits;
                }
                value.fractional = 0;
            }
            return value;
        }
        /// <summary>
        /// 指定した値の整数の桁を返します。小数の桁は破棄されます。
        /// </summary>
        /// <param name="value">切り捨てる値</param>
        /// <returns>value を 0 方向の近似整数に丸めた結果</returns>
        public static FixedPointNumber32Q16 Truncate(FixedPointNumber32Q16 value)
        {
            // 負数のときは、integerが単純な整数ではないので補正
            if (value.integer < 0)
            {
                if (value.fractional != 0)
                {
                    value.integer++;
                }
            }
            value.fractional = 0;
            return value;
        }
        #endregion 数学系関数

        #region 単項演算子
        public static FixedPointNumber32Q16 operator +(FixedPointNumber32Q16 value)
        {
            return value;
        }
        public static FixedPointNumber32Q16 operator -(FixedPointNumber32Q16 value)
        {
            value.bits = -value.bits;
            return value;
        }
        public static FixedPointNumber32Q16 operator ~(FixedPointNumber32Q16 value)
        {
            value.bits = ~value.bits;
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        public static FixedPointNumber32Q16 operator +(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            var temp = new FixedPointNumber32Q16();
            temp.bits = valueL.bits + valueR.bits;
            return temp;
        }
        public static FixedPointNumber32Q16 operator -(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            var temp = new FixedPointNumber32Q16();
            temp.bits = valueL.bits - valueR.bits;
            return temp;
        }
        public static FixedPointNumber32Q16 operator *(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            long temp = valueL.bits;
            temp *= valueR.bits;
            temp >>= QBits;
            return new FixedPointNumber32Q16() { bits = (int)temp };
        }
        public static FixedPointNumber32Q16 operator /(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            long temp = valueL.bits;
            temp <<= QBits;
            temp /= valueR.bits;
            return new FixedPointNumber32Q16() { bits = (int)temp };
        }
        public static FixedPointNumber32Q16 operator %(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits % valueR.bits };
        }
        public static FixedPointNumber32Q16 operator &(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits & valueR.bits };
        }
        public static FixedPointNumber32Q16 operator &(FixedPointNumber32Q16 valueL, int valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits & valueR };
        }
        public static FixedPointNumber32Q16 operator |(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits | valueR.bits };
        }
        public static FixedPointNumber32Q16 operator |(FixedPointNumber32Q16 valueL, int valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits | valueR };
        }
        public static FixedPointNumber32Q16 operator ^(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits ^ valueR.bits };
        }
        public static FixedPointNumber32Q16 operator ^(FixedPointNumber32Q16 valueL, int valueR)
        {
            return new FixedPointNumber32Q16() { bits = valueL.bits ^ valueR };
        }
        public static FixedPointNumber32Q16 operator <<(FixedPointNumber32Q16 value, int shift)
        {
            return new FixedPointNumber32Q16() { bits = value.bits << shift };
        }
        public static FixedPointNumber32Q16 operator >>(FixedPointNumber32Q16 value, int shift)
        {
            return new FixedPointNumber32Q16() { bits = value.bits >> shift };
        }
        #endregion 2項演算子

        #region 比較演算子
        public static bool operator ==(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits == valueR.bits;
        }
        public static bool operator !=(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits != valueR.bits;
        }
        public static bool operator >(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits > valueR.bits;
        }
        public static bool operator <(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits < valueR.bits;
        }
        public static bool operator >=(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits >= valueR.bits;
        }
        public static bool operator <=(FixedPointNumber32Q16 valueL, FixedPointNumber32Q16 valueR)
        {
            return valueL.bits <= valueR.bits;
        }
        #endregion 比較演算子

        #region 型変換
        #region 他の型→固定小数点数型
        public static implicit operator FixedPointNumber32Q16(byte value)
        {
            return new FixedPointNumber32Q16() { integer = value };
        }
        public static implicit operator FixedPointNumber32Q16(sbyte value)
        {
            return new FixedPointNumber32Q16() { integer = value };
        }
        public static implicit operator FixedPointNumber32Q16(short value)
        {
            return new FixedPointNumber32Q16() { integer = value };
        }
        public static explicit operator FixedPointNumber32Q16(ushort value)
        {
            return new FixedPointNumber32Q16() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber32Q16(int value)
        {
            return new FixedPointNumber32Q16() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber32Q16(uint value)
        {
            return new FixedPointNumber32Q16() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber32Q16(long value)
        {
            return new FixedPointNumber32Q16() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber32Q16(ulong value)
        {
            return new FixedPointNumber32Q16() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber32Q16(float value)
        {
            return (FixedPointNumber32Q16)((double)value);
        }
        public static explicit operator FixedPointNumber32Q16(double value)
        {
            value *= OneBits;
            return new FixedPointNumber32Q16() { bits = (int)value };
        }
        public static explicit operator FixedPointNumber32Q16(decimal value)
        {
            value *= OneBits;
            return new FixedPointNumber32Q16() { bits = (int)value };
        }
        #endregion 他の型→固定小数点数型
        #region 固定小数点数型→他の型
        public static explicit operator byte(FixedPointNumber32Q16 value)
        {
            return (byte)value.integer;
        }
        public static explicit operator sbyte(FixedPointNumber32Q16 value)
        {
            return (sbyte)value.integer;
        }
        public static explicit operator short(FixedPointNumber32Q16 value)
        {
            return value.integer;
        }
        public static explicit operator ushort(FixedPointNumber32Q16 value)
        {
            return (ushort)value.integer;
        }
        public static explicit operator int(FixedPointNumber32Q16 value)
        {
            return value.integer;
        }
        public static explicit operator uint(FixedPointNumber32Q16 value)
        {
            return (uint)value.integer;
        }
        public static explicit operator long(FixedPointNumber32Q16 value)
        {
            return value.integer;
        }
        public static explicit operator ulong(FixedPointNumber32Q16 value)
        {
            return (ulong)value.integer;
        }
        public static explicit operator float(FixedPointNumber32Q16 value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return (float)temp;
        }
        public static implicit operator double(FixedPointNumber32Q16 value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        public static implicit operator decimal(FixedPointNumber32Q16 value)
        {
            decimal temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        #endregion 固定小数点数型→他の型
        #endregion 型変換

        #region IComparable
        public int CompareTo(object obj)
        {
            return CompareTo((FixedPointNumber32Q16)obj);
        }

        public int CompareTo(FixedPointNumber32Q16 other)
        {
            return this.bits.CompareTo(other.bits);
        }
        #endregion IComparable

        #region IEquatable
        public bool Equals(FixedPointNumber32Q16 other)
        {
            return bits == other.bits;
        }
        #endregion IEquatable

        #region object
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is FixedPointNumber32Q16)
            {
                return Equals((FixedPointNumber32Q16)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return bits.GetHashCode();
        }
        public override string ToString()
        {
            decimal temp = bits;
            temp /= OneBits;
            return temp.ToString();
        }
        #endregion object
    }
}
