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
        public readonly static FixedPointNumber64Q32 Zero = new FixedPointNumber64Q32() { Integer = 0 };
        public readonly static FixedPointNumber64Q32 One = new FixedPointNumber64Q32() { Integer = 1 };
        public readonly static FixedPointNumber64Q32 MinValue = new FixedPointNumber64Q32() { Bits = long.MinValue };
        public readonly static FixedPointNumber64Q32 MaxValue = new FixedPointNumber64Q32() { Bits = long.MaxValue };
        public readonly static FixedPointNumber64Q32 Epsilon = new FixedPointNumber64Q32() { Fractional = 1 };
        const long OneBits = 1L << QBits;
        #endregion 定数

        #region フィールド
        /// <summary>
        /// 全体
        /// </summary>
        [FieldOffset(0)]
        public long Bits;
        /// <summary>
        /// 整数部
        /// </summary>
        [FieldOffset(4)]
        public int Integer;
        /// <summary>
        /// 小数部
        /// </summary>
        [FieldOffset(0)]
        public uint Fractional;
        #endregion フィールド

        #region 単項演算子
        public static FixedPointNumber64Q32 operator +(FixedPointNumber64Q32 value)
        {
            return value;
        }
        public static FixedPointNumber64Q32 operator -(FixedPointNumber64Q32 value)
        {
            value.Bits = -value.Bits;
            return value;
        }
        public static FixedPointNumber64Q32 operator ~(FixedPointNumber64Q32 value)
        {
            value.Bits = ~value.Bits;
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        public static FixedPointNumber64Q32 operator +(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            var temp = new FixedPointNumber64Q32();
            temp.Bits = valueL.Bits + valueR.Bits;
            return temp;
        }
        public static FixedPointNumber64Q32 operator -(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            var temp = new FixedPointNumber64Q32();
            temp.Bits = valueL.Bits - valueR.Bits;
            return temp;
        }
        public static FixedPointNumber64Q32 operator *(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            decimal temp = valueL.Bits;
            temp *= valueR.Bits;
            temp /= OneBits;
            return new FixedPointNumber64Q32() { Bits = (long)temp };
        }
        public static FixedPointNumber64Q32 operator /(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            decimal temp = valueL.Bits;
            temp *= OneBits;
            temp /= valueR.Bits;
            return new FixedPointNumber64Q32() { Bits = (long)temp };
        }
        public static FixedPointNumber64Q32 operator %(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits % valueR.Bits };
        }
        public static FixedPointNumber64Q32 operator &(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits & valueR.Bits };
        }
        public static FixedPointNumber64Q32 operator &(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits & valueR };
        }
        public static FixedPointNumber64Q32 operator |(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits | valueR.Bits };
        }
        public static FixedPointNumber64Q32 operator |(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits | valueR };
        }
        public static FixedPointNumber64Q32 operator ^(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits ^ valueR.Bits };
        }
        public static FixedPointNumber64Q32 operator ^(FixedPointNumber64Q32 valueL, long valueR)
        {
            return new FixedPointNumber64Q32() { Bits = valueL.Bits ^ valueR };
        }
        public static FixedPointNumber64Q32 operator <<(FixedPointNumber64Q32 value, int shift)
        {
            return new FixedPointNumber64Q32() { Bits = value.Bits << shift };
        }
        public static FixedPointNumber64Q32 operator >>(FixedPointNumber64Q32 value, int shift)
        {
            return new FixedPointNumber64Q32() { Bits = value.Bits >> shift };
        }
        #endregion 2項演算子

        #region 比較演算子
        public static bool operator ==(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits == valueR.Bits;
        }
        public static bool operator !=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits != valueR.Bits;
        }
        public static bool operator >(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits > valueR.Bits;
        }
        public static bool operator <(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits < valueR.Bits;
        }
        public static bool operator >=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits >= valueR.Bits;
        }
        public static bool operator <=(FixedPointNumber64Q32 valueL, FixedPointNumber64Q32 valueR)
        {
            return valueL.Bits <= valueR.Bits;
        }
        #endregion 比較演算子

        #region 型変換
        public static implicit operator FixedPointNumber64Q32(byte value)
        {
            return new FixedPointNumber64Q32() { Integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(sbyte value)
        {
            return new FixedPointNumber64Q32() { Integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(short value)
        {
            return new FixedPointNumber64Q32() { Integer = value };
        }
        public static implicit operator FixedPointNumber64Q32(ushort value)
        {
            return new FixedPointNumber64Q32() { Integer = (short)value };
        }
        public static implicit operator FixedPointNumber64Q32(int value)
        {
            return new FixedPointNumber64Q32() { Integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(uint value)
        {
            return new FixedPointNumber64Q32() { Integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(long value)
        {
            return new FixedPointNumber64Q32() { Integer = (short)value };
        }
        public static explicit operator FixedPointNumber64Q32(ulong value)
        {
            return new FixedPointNumber64Q32() { Integer = (short)value };
        }
        public static implicit operator FixedPointNumber64Q32(float value)
        {
            return (FixedPointNumber64Q32)((double)value);
        }
        public static explicit operator FixedPointNumber64Q32(double value)
        {
            return new FixedPointNumber64Q32()
            {
                Integer = (int)value,
                Fractional = (uint)((value % 1) * OneBits)
            };
        }
        public static explicit operator FixedPointNumber64Q32(decimal value)
        {
            value *= OneBits;
            return new FixedPointNumber64Q32() { Bits = (long)value };
        }
        #endregion 型変換


        #region IComparable
        public int CompareTo(object obj)
        {
            return CompareTo((FixedPointNumber64Q32)obj);
        }

        public int CompareTo(FixedPointNumber64Q32 other)
        {
            return this.Bits.CompareTo(other.Bits);
        }
        #endregion IComparable

        #region IEquatable
        public bool Equals(FixedPointNumber64Q32 other)
        {
            return Bits == other.Bits;
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
            return Bits.GetHashCode();
        }
        public override string ToString()
        {
            decimal temp = Bits;
            temp /= OneBits;
            return temp.ToString();
        }
        #endregion object
    }
}
