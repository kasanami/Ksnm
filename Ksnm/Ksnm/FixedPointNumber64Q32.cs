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

namespace Ksnm
{
    /// <summary>
    /// 固定小数点数(全体のビット数64、小数部分のビット数32)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct FixedPointNumber64Q32 : IComparable, IComparable<FixedPointNumber64Q32>, IEquatable<FixedPointNumber64Q32>
    {
        #region 定数
        public const int QBits = 32;
        public readonly static FixedPointNumber64Q32 Zero = new FixedPointNumber64Q32() { integer = 0 };
        public readonly static FixedPointNumber64Q32 One = new FixedPointNumber64Q32() { integer = 1 };
        public readonly static FixedPointNumber64Q32 MinValue = new FixedPointNumber64Q32() { bits = long.MinValue };
        public readonly static FixedPointNumber64Q32 MaxValue = new FixedPointNumber64Q32() { bits = long.MaxValue };
        public readonly static FixedPointNumber64Q32 Epsilon = new FixedPointNumber64Q32() { fractional = 1 };
        /// <summary>
        /// 1を表すビット
        /// </summary>
        const long OneBits = 1L << QBits;
        /// <summary>
        /// 0.5を表すビット
        /// </summary>
        const long HalfBits = 1L << (QBits - 1);
        #endregion 定数

        #region フィールド
        /// <summary>
        /// 全体のビット
        /// </summary>
        [FieldOffset(0)]
        long bits;
        /// <summary>
        /// 整数部
        /// </summary>
        [FieldOffset(4)]
        int integer;
        /// <summary>
        /// 小数部
        /// </summary>
        [FieldOffset(0)]
        uint fractional;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public long Bits { get { return bits; } }
        /// <summary>
        /// 整数部
        /// 注意点：負数のとき、-0.1は-1。-1.1は-2になる。Floor関数を使用して整数にした値と同じ。
        /// </summary>
        public int Integer { get { return integer; } }
        /// <summary>
        /// 小数部
        /// </summary>
        public uint Fractional { get { return fractional; } }
        #endregion プロパティ

        /// <summary>
        /// 指定した整数で初期化
        /// </summary>
        /// <param name="integer">整数部</param>
        public FixedPointNumber64Q32(int integer)
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
        public FixedPointNumber64Q32(int integer, uint fractional)
        {
            bits = 0;
            this.integer = integer;
            this.fractional = fractional;
        }

        /// <summary>
        /// 全体のビットを設定
        /// </summary>
        /// <param name="bits">設定するビット</param>
        public void SetBits(long bits)
        {
            this.bits = bits;
        }

        #region 数学系関数
        /// <summary>
        /// 絶対値を取得します。
        /// </summary>
        /// <param name="value">元の値</param>
        /// <returns>絶対値</returns>
        public static FixedPointNumber64Q32 Abs(FixedPointNumber64Q32 value)
        {
            return new FixedPointNumber64Q32() { bits = System.Math.Abs(value.bits) };
        }
        /// <summary>
        /// 指定した値以上の、最小の整数値を返します。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>value 以上の最小の整数値</returns>
        public static FixedPointNumber64Q32 Ceiling(FixedPointNumber64Q32 value)
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
        public static FixedPointNumber64Q32 Floor(FixedPointNumber64Q32 value)
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
        public static FixedPointNumber64Q32 Round(FixedPointNumber64Q32 value)
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
        public static FixedPointNumber64Q32 Truncate(FixedPointNumber64Q32 value)
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
        public static FixedPointNumber64Q32 operator +(FixedPointNumber64Q32 value)
        {
            return value;
        }
        public static FixedPointNumber64Q32 operator -(FixedPointNumber64Q32 value)
        {
            value.bits = -value.bits;
            return value;
        }
        public static FixedPointNumber64Q32 operator ~(FixedPointNumber64Q32 value)
        {
            value.bits = ~value.bits;
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        public static FixedPointNumber64Q32 operator +(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            var temp = new FixedPointNumber64Q32();
            temp.bits = valueL.bits + valueR.bits;
            return temp;
        }
        public static FixedPointNumber64Q32 operator -(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            var temp = new FixedPointNumber64Q32();
            temp.bits = valueL.bits - valueR.bits;
            return temp;
        }
        public static FixedPointNumber64Q32 operator *(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            decimal temp = valueL.bits;
            temp *= valueR.bits;
            temp /= OneBits;
            return new FixedPointNumber64Q32() { bits = (long)temp };
        }
        public static FixedPointNumber64Q32 operator /(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            decimal temp = valueL.bits;
            temp *= OneBits;
            temp /= valueR.bits;
            return new FixedPointNumber64Q32() { bits = (long)temp };
        }
        public static FixedPointNumber64Q32 operator %(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits % valueR.bits };
        }
        public static FixedPointNumber64Q32 operator &(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits & valueR.bits };
        }
        public static FixedPointNumber64Q32 operator &(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits & valueR };
        }
        public static FixedPointNumber64Q32 operator |(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits | valueR.bits };
        }
        public static FixedPointNumber64Q32 operator |(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits | valueR };
        }
        public static FixedPointNumber64Q32 operator ^(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits ^ valueR.bits };
        }
        public static FixedPointNumber64Q32 operator ^(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { bits = valueL.bits ^ valueR };
        }
        public static FixedPointNumber64Q32 operator <<(FixedPointNumber64Q32 value, int shift)
        {
            return new FixedPointNumber64Q32() { bits = value.bits << shift };
        }
        public static FixedPointNumber64Q32 operator >>(FixedPointNumber64Q32 value, int shift)
        {
            return new FixedPointNumber64Q32() { bits = value.bits >> shift };
        }
        #endregion 2項演算子

        #region 比較演算子
        public static bool operator ==(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits == valueR.bits;
        }
        public static bool operator !=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits != valueR.bits;
        }
        public static bool operator >(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits > valueR.bits;
        }
        public static bool operator <(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits < valueR.bits;
        }
        public static bool operator >=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits >= valueR.bits;
        }
        public static bool operator <=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.bits <= valueR.bits;
        }
        #endregion 比較演算子

        #region 型変換
        #region 他の型→固定小数点数型
        public static implicit operator FixedPointNumber64Q32(byte value)
        {
            return new FixedPointNumber64Q32() { integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(sbyte value)
        {
            return new FixedPointNumber64Q32() { integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(short value)
        {
            return new FixedPointNumber64Q32() { integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(ushort value)
        {
            return new FixedPointNumber64Q32() { integer = (short)value };
        }
        public static implicit operator FixedPointNumber64Q32(int value)
        {
            return new FixedPointNumber64Q32() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(uint value)
        {
            return new FixedPointNumber64Q32() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(long value)
        {
            return new FixedPointNumber64Q32() { integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(ulong value)
        {
            return new FixedPointNumber64Q32() { integer = (short)value };
        }
        public static implicit operator FixedPointNumber64Q32(float value)
        {
            return (FixedPointNumber64Q32)((double)value);
        }
        public static explicit operator FixedPointNumber64Q32(double value)
        {
            value *= OneBits;
            return new FixedPointNumber64Q32() { bits = (long)value };
        }
        public static explicit operator FixedPointNumber64Q32(decimal value)
        {
            value *= OneBits;
            return new FixedPointNumber64Q32() { bits = (long)value };
        }
        #endregion 他の型→固定小数点数型
        #region 固定小数点数型→他の型
        public static explicit operator byte(FixedPointNumber64Q32 value)
        {
            return (byte)value.integer;
        }
        public static explicit operator sbyte(FixedPointNumber64Q32 value)
        {
            return (sbyte)value.integer;
        }
        public static explicit operator short(FixedPointNumber64Q32 value)
        {
            return (short)value.integer;
        }
        public static explicit operator ushort(FixedPointNumber64Q32 value)
        {
            return (ushort)value.integer;
        }
        public static explicit operator int(FixedPointNumber64Q32 value)
        {
            return value.integer;
        }
        public static explicit operator uint(FixedPointNumber64Q32 value)
        {
            return (uint)value.integer;
        }
        public static explicit operator long(FixedPointNumber64Q32 value)
        {
            return value.integer;
        }
        public static explicit operator ulong(FixedPointNumber64Q32 value)
        {
            return (ulong)value.integer;
        }
        public static explicit operator float(FixedPointNumber64Q32 value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return (float)temp;
        }
        public static explicit operator double(FixedPointNumber64Q32 value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        public static implicit operator decimal(FixedPointNumber64Q32 value)
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
            return CompareTo((FixedPointNumber64Q32)obj);
        }

        public int CompareTo(FixedPointNumber64Q32 other)
        {
            return this.bits.CompareTo(other.bits);
        }
        #endregion IComparable

        #region IEquatable
        public bool Equals(FixedPointNumber64Q32 other)
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
            if (obj is FixedPointNumber64Q32)
            {
                return Equals((FixedPointNumber64Q32)obj);
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
