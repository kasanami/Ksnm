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
        public readonly static FixedPointNumber32Q16 Epsilon = new FixedPointNumber32Q16() { fractional = 1 };
        const int OneBits = 1 << QBits;
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
