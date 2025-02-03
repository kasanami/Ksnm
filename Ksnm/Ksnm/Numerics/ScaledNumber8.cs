/*
The zlib License

Copyright (c) 2024 Takahiro Kasanami

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
using System.Numerics;
using System.Runtime.InteropServices;
using Ksnm.ExtensionMethods.System.Double;
using BitsType = System.Byte;// 固定小数点数 全体のビットを表す型
using System.Globalization;
using System.Diagnostics.CodeAnalysis;// 固定小数点数 小数部分のビットを表す型

namespace Ksnm.Numerics
{
    // コードを再利用するためのエイリアスを定義
    using Self = ScaledNumber8;
    /// <summary>
    /// 8ビットにスケーリングされた値型
    /// ・固定小数点数
    /// ・0x0 = 0.0 , 0xFF = 1.0
    /// ・他の型名候補、非正規化された値（denormalized value）・・・長い
    /// </summary>
    public struct ScaledNumber8 :
        INumber<Self>,
        IMinMaxValue<Self>
    {
        #region 定数,static
        /// <summary>
        /// 1を表すビット
        /// </summary>
        const BitsType OneBits = 0xFF;
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public readonly static Self Zero = new (0);
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public readonly static Self One = new () { bits = OneBits };
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public readonly static Self MinValue = new () { bits = BitsType.MinValue };
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public readonly static Self MaxValue = new () { bits = BitsType.MaxValue };
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public readonly static Self Epsilon = new () { bits = 1 };
        #endregion 定数

        #region INumberBase
        static Self INumberBase<Self>.One => One;

        public static int Radix => 2;

        static Self INumberBase<Self>.Zero => Zero;

        public static Self AdditiveIdentity => Zero;

        public static Self MultiplicativeIdentity => One;
        #endregion INumberBase

        #region IMinMaxValue
        static Self IMinMaxValue<Self>.MaxValue => MaxValue;

        static Self IMinMaxValue<Self>.MinValue => MinValue;
        #endregion IMinMaxValue

        #region フィールド
        /// <summary>
        /// 全体のビット
        /// </summary>
        BitsType bits;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public BitsType Bits => bits;
        #endregion プロパティ

        #region コンストラクタ
        public ScaledNumber8()
        {
            bits = 0;
        }
        /// <summary>
        /// 指定した浮動小数点数から初期化
        /// </summary>
        /// <param name="value">0.0～1.0の値</param>
        public ScaledNumber8(double value)
        {
            bits = BitsType.CreateSaturating(value * BitsType.MaxValue);
        }
        #endregion コンストラクタ

        #region ユーティリティ
        #endregion ユーティリティ

        #region 単項演算子
        /// <summary>
        /// 符号維持
        /// </summary>
        public static Self operator +(Self value)
        {
            return value;
        }
        /// <summary>
        /// 符号反転
        /// </summary>
        public static Self operator -(Self value)
        {
            value.bits = (BitsType)(-value.bits);
            return value;
        }
        /// <summary>
        /// valueの補数を返す
        /// </summary>
        public static Self operator ~(Self value)
        {
            value.bits = (BitsType)(~value.bits);
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Self operator +(Self left, Self right)
        {
            var temp = left;
            temp.bits += right.bits;
            return temp;
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Self operator -(Self left, Self right)
        {
            var temp = left;
            temp.bits -= right.bits;
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Self operator *(Self left, Self right)
        {
            double temp = left.bits * right.bits;
            temp /= BitsType.MaxValue * BitsType.MaxValue;
            return new Self(temp);
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Self operator /(Self left, Self right)
        {
            double temp = left.bits;
            temp *= BitsType.MaxValue;
            temp /= right.bits;
            return new Self(temp);
        }
        /// <summary>
        /// 剰余演算
        /// </summary>
        public static Self operator %(Self left, Self right)
        {
            return new Self() { bits = (BitsType)(left.bits % right.bits) };
        }
        /// <summary>
        /// 論理積
        /// </summary>
        public static Self operator &(Self left, Self right)
        {
            return new Self() { bits = (BitsType)(left.bits & right.bits) };
        }
        /// <summary>
        /// 論理積
        /// </summary>
        public static Self operator &(Self left, BitsType right)
        {
            return new Self() { bits = (BitsType)(left.bits & right) };
        }
        /// <summary>
        /// 論理和
        /// </summary>
        public static Self operator |(Self left, Self right)
        {
            return new Self() { bits = (BitsType)(left.bits | right.bits) };
        }
        /// <summary>
        /// 論理和
        /// </summary>
        public static Self operator |(Self left, BitsType right)
        {
            return new Self() { bits = (BitsType)(left.bits | right) };
        }
        /// <summary>
        /// 排他的論理和
        /// </summary>
        public static Self operator ^(Self left, Self right)
        {
            return new Self() { bits = (BitsType)(left.bits ^ right.bits) };
        }
        /// <summary>
        /// 排他的論理和
        /// </summary>
        public static Self operator ^(Self left, BitsType right)
        {
            return new Self() { bits = (BitsType)(left.bits ^ right) };
        }
        /// <summary>
        /// 左シフト
        /// </summary>
        public static Self operator <<(Self value, int shift)
        {
            return new Self() { bits = (BitsType)(value.bits << shift) };
        }
        /// <summary>
        /// 右シフト
        /// </summary>
        public static Self operator >>(Self value, int shift)
        {
            return new Self() { bits = (BitsType)(value.bits >> shift) };
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(Self left, Self right)
        {
            return left.bits == right.bits;
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(Self left, Self right)
        {
            return left.bits != right.bits;
        }
        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(Self left, Self right)
        {
            return left.bits > right.bits;
        }
        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(Self left, Self right)
        {
            return left.bits < right.bits;
        }
        /// <summary>
        /// 以上演算子
        /// </summary>
        public static bool operator >=(Self left, Self right)
        {
            return left.bits >= right.bits;
        }
        /// <summary>
        /// 以下演算子
        /// </summary>
        public static bool operator <=(Self left, Self right)
        {
            return left.bits <= right.bits;
        }
        #endregion 比較演算子

        #region 型変換
        #region 整数→固定小数点数型　切り捨て
        public static explicit operator Self(byte value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(sbyte value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(short value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(ushort value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(int value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(uint value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(long value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(ulong value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(Int128 value)
            => new Self() { bits = byte.CreateTruncating(value) };
        public static explicit operator Self(UInt128 value)
            => new Self() { bits = byte.CreateTruncating(value) };
        #endregion
        #region 浮動小数点数→固定小数点数型　クリップ
        public static explicit operator Self(Half value) => (Self)(double)value;
        public static explicit operator Self(float value) => (Self)(double)value;
        public static explicit operator Self(double value)
        {
            if (value > 1)
            {
                return MaxValue;
            }
            value *= OneBits;
            return new Self() { bits = (byte)value };
        }
        public static explicit operator Self(decimal value)
        {
            if (value > 1)
            {
                return MaxValue;
            }
            value *= MaxValue;
            return new Self() { bits = (byte)value };
        }
        #endregion
        #region 固定小数点数型→他の型
        public static explicit operator byte(Self value)
        {
            if (value.bits == byte.MaxValue)
            {
                return 1;
            }
            return 0;
        }
        public static explicit operator sbyte(Self value)
        {
            if (value.bits == byte.MaxValue)
            {
                return 1;
            }
            return 0;
        }
        public static explicit operator short(Self value) => (byte)value;
        public static explicit operator ushort(Self value) => (byte)value;
        public static explicit operator int(Self value) => (byte)value;
        public static explicit operator uint(Self value) => (byte)value;
        public static explicit operator long(Self value) => (byte)value;
        public static explicit operator ulong(Self value) => (byte)value;
        public static explicit operator Half(Self value) => (Half)(double)value;
        public static explicit operator float(Self value) => (float)(double)value;
        public static implicit operator double(Self value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        public static implicit operator decimal(Self value)
        {
            decimal temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        #endregion 固定小数点数型→他の型
        #endregion 型変換

        public static Self operator --(Self value)
        {
            value.bits = (BitsType)(value.bits - OneBits);
            return value;
        }

        public static Self operator ++(Self value)
        {
            value.bits = (BitsType)(value.bits + OneBits);
            return value;
        }
        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(object obj)
        {
            return CompareTo((Self)obj);
        }
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="other">比較対象の固定小数点数</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(Self other)
        {
            return bits.CompareTo(other.bits);
        }
        #endregion IComparable

        #region IEquatable
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        public bool Equals(Self other)
        {
            return bits == other.bits;
        }
        #endregion IEquatable

        #region object
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Self)
            {
                return Equals((Self)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return bits.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            decimal temp = bits;
            temp /= OneBits;
            return temp.ToString();
        }
        #endregion object

        #region INumberBase

        public static bool IsCanonical(Self value) => true;

        public static bool IsComplexNumber(Self value) => false;

        public static bool IsEvenInteger(Self value)
            => value.bits == 0;

        public static bool IsFinite(Self value) => false;

        public static bool IsImaginaryNumber(Self value) => false;

        public static bool IsInfinity(Self value) => false;

        public static bool IsInteger(Self value)
            => value.bits == 0 || value.bits == OneBits;

        public static bool IsNaN(Self value) => false;

        public static bool IsNegative(Self value) => false;

        public static bool IsNegativeInfinity(Self value) => false;

        public static bool IsNormal(Self value) => value.bits != 0;

        public static bool IsOddInteger(Self value)=> value.bits == OneBits;

        public static bool IsPositive(Self value) => true;

        public static bool IsPositiveInfinity(Self value) => false;

        public static bool IsRealNumber(Self value) => true;

        public static bool IsSubnormal(Self value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Self value)
        {
            return value.bits == 0;
        }

        public static Self MaxMagnitude(Self x, Self y)
        {
            throw new NotImplementedException();
        }

        public static Self MaxMagnitudeNumber(Self x, Self y)
        {
            throw new NotImplementedException();
        }

        public static Self MinMagnitude(Self x, Self y)
        {
            throw new NotImplementedException();
        }

        public static Self MinMagnitudeNumber(Self x, Self y)
        {
            throw new NotImplementedException();
        }

        public static Self Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            return (Self)double.Parse(s, style, provider);
        }

        public static Self Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return (Self)double.Parse(s, style, provider);
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Self result)
        {
            double doubleResult;
            if (double.TryParse(s, style, provider, out doubleResult))
            {
                result = (Self)doubleResult;
                return true;
            }
            result = Zero;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Self result)
        {
            double doubleResult;
            if (double.TryParse(s, style, provider, out doubleResult))
            {
                result = (Self)doubleResult;
                return true;
            }
            result = Zero;
            return false;
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            double temp = this;
            return temp.TryFormat(destination, out charsWritten, format, provider);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            double temp = this;
            return temp.ToString(format, formatProvider);
        }

        public static Self Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            return (Self)double.Parse(s, provider);
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Self result)
        {
            double doubleResult;
            if (double.TryParse(s, provider, out doubleResult))
            {
                result = (Self)doubleResult;
                return true;
            }
            result = Zero;
            return false;
        }

        public static Self Parse(string s, IFormatProvider? provider)
        {
            return (Self)double.Parse(s, provider);
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Self result)
        {
            double doubleResult;
            if (double.TryParse(s, provider, out doubleResult))
            {
                result = (Self)doubleResult;
                return true;
            }
            result = Zero;
            return false;
        }

        static bool INumberBase<Self>.TryConvertFromChecked<TOther>(TOther value, out Self result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Self>.TryConvertFromSaturating<TOther>(TOther value, out Self result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Self>.TryConvertFromTruncating<TOther>(TOther value, out Self result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Self>.TryConvertToChecked<TOther>(Self value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Self>.TryConvertToSaturating<TOther>(Self value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Self>.TryConvertToTruncating<TOther>(Self value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static Self INumberBase<Self>.Abs(Self value)
        {
            throw new NotImplementedException();
        }
        #endregion INumberBase
    }
}
