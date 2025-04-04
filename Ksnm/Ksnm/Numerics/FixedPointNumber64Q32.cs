/*
The zlib License

Copyright (c) 2019-2025 Takahiro Kasanami

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
using BigBitsType = System.Int128;// BitsTypeより大きい型
using BitsType = System.Int64;// 固定小数点数 全体のビットを表す型
using IntegerType = System.Int32;// 固定小数点数 整数部分のビットを表す型
using FractionalType = System.UInt32;
using System.Numerics;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Ksnm.Numerics
{
    // 精度の異なる固定小数点数型でコードを再利用するためのエイリアスを定義
    using FixedPointNumber = FixedPointNumber64Q32;
    /// <summary>
    /// 固定小数点数(全体のビット数64、小数部分のビット数32)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct FixedPointNumber64Q32 :
        IComparable, IComparable<FixedPointNumber>, IEquatable<FixedPointNumber>,
        IFloatingPointIeee754<FixedPointNumber>,
        IMinMaxValue<FixedPointNumber>
    {
        #region 定数
        /// <summary>
        /// 小数部分のビット数
        /// </summary>
        public const int QBits = 32;
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public readonly static FixedPointNumber Zero = new(0, 0);
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public readonly static FixedPointNumber One = new(1, 0);
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public readonly static FixedPointNumber MinValue = new(BitsType.MinValue);
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public readonly static FixedPointNumber MaxValue = new(BitsType.MaxValue);
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public readonly static FixedPointNumber Epsilon = new(1);
        /// <summary>
        /// 1を表すビット
        /// </summary>
        const BitsType OneBits = 1L << QBits;
        /// <summary>
        /// 0.5を表すビット
        /// </summary>
        const BitsType HalfBits = 1L << QBits - 1;

        static FixedPointNumber IFloatingPointIeee754<FixedPointNumber>.Epsilon => Epsilon;

        public static FixedPointNumber NaN => throw new NotImplementedException();

        public static FixedPointNumber NegativeInfinity => throw new NotImplementedException();

        public static FixedPointNumber NegativeZero => throw new NotImplementedException();

        public static FixedPointNumber PositiveInfinity => throw new NotImplementedException();

        public static FixedPointNumber NegativeOne => new(-1, 0);

        public static FixedPointNumber E => (FixedPointNumber)double.E;

        public static FixedPointNumber Pi => (FixedPointNumber)double.Pi;

        public static FixedPointNumber Tau => (FixedPointNumber)double.Tau;

        static FixedPointNumber INumberBase<FixedPointNumber>.One => One;

        public static int Radix => 2;

        static FixedPointNumber INumberBase<FixedPointNumber>.Zero => Zero;

        public static FixedPointNumber AdditiveIdentity => Zero;

        public static FixedPointNumber MultiplicativeIdentity => One;

        static FixedPointNumber IMinMaxValue<FixedPointNumber>.MaxValue => MaxValue;

        static FixedPointNumber IMinMaxValue<FixedPointNumber>.MinValue => MinValue;
        #endregion 定数

        #region フィールド
        /// <summary>
        /// 全体のビット
        /// </summary>
        [FieldOffset(0)]
        BitsType bits;
        /// <summary>
        /// 整数部
        /// </summary>
        [FieldOffset(4)]
        IntegerType integer;
        /// <summary>
        /// 小数部
        /// </summary>
        [FieldOffset(0)]
        FractionalType fractional;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public BitsType Bits { get { return bits; } }
        /// <summary>
        /// 整数部
        /// 注意点：負数のとき、-0.1は-1。-1.1は-2になる。Floor関数を使用して整数にした値と同じ。
        /// </summary>
        public IntegerType Integer { get { return integer; } }
        /// <summary>
        /// 小数部
        /// </summary>
        public FractionalType Fractional { get { return fractional; } }
        #endregion プロパティ

        /// <summary>
        /// 指定した整数で初期化
        /// </summary>
        /// <param name="bits">全体のビット</param>
        public FixedPointNumber64Q32(BitsType bits)
        {
            this.bits = bits;
        }

        /// <summary>
        /// 指定した整数と小数で初期化
        /// </summary>
        /// <param name="integer">整数部</param>
        /// <param name="fractional">小数部</param>
        public FixedPointNumber64Q32(IntegerType integer, FractionalType fractional)
        {
            bits = 0;
            this.integer = integer;
            this.fractional = fractional;
        }

        /// <summary>
        /// 全体のビットを設定
        /// </summary>
        /// <param name="bits">設定するビット</param>
        public void SetBits(BitsType bits)
        {
            this.bits = bits;
        }

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
        public static FixedPointNumber Abs(FixedPointNumber value)
        {
            return new FixedPointNumber() { bits = System.Math.Abs(value.bits) };
        }
        /// <summary>
        /// 指定した値以上の、最小の整数値を返します。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>value 以上の最小の整数値</returns>
        public static FixedPointNumber Ceiling(FixedPointNumber value)
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
        public static FixedPointNumber Floor(FixedPointNumber value)
        {
            value.fractional = 0;
            return value;
        }
        /// <summary>
        /// 指定した値を最も近い整数に丸めます。
        /// </summary>
        /// <param name="value">丸める値</param>
        /// <returns>値に最も近い整数。2 つの整数の中間にある場合は偶数が返されます。</returns>
        /// <exception cref="OverflowException">結果が範囲外</exception>
        public static FixedPointNumber Round(FixedPointNumber value)
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
        public static FixedPointNumber Truncate(FixedPointNumber value)
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
        /// <summary>
        /// 符号維持
        /// </summary>
        public static FixedPointNumber operator +(FixedPointNumber value)
        {
            return value;
        }
        /// <summary>
        /// 符号反転
        /// </summary>
        public static FixedPointNumber operator -(FixedPointNumber value)
        {
            value.bits = -value.bits;
            return value;
        }
        /// <summary>
        /// valueの補数を返す
        /// </summary>
        public static FixedPointNumber operator ~(FixedPointNumber value)
        {
            value.bits = ~value.bits;
            return value;
        }
        public static FixedPointNumber operator --(FixedPointNumber value)
        {
            value.integer--;
            return value;
        }
        public static FixedPointNumber operator ++(FixedPointNumber value)
        {
            value.integer++;
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static FixedPointNumber operator +(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            var temp = new FixedPointNumber();
            temp.bits = valueL.bits + valueR.bits;
            return temp;
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static FixedPointNumber operator -(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            var temp = new FixedPointNumber();
            temp.bits = valueL.bits - valueR.bits;
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static FixedPointNumber operator *(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            BigBitsType temp = valueL.bits;
            temp *= valueR.bits;
            temp >>= QBits;
            return new FixedPointNumber() { bits = (BitsType)temp };
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static FixedPointNumber operator /(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            BigBitsType temp = valueL.bits;
            temp <<= QBits;
            temp /= valueR.bits;
            return new FixedPointNumber() { bits = (BitsType)temp };
        }
        /// <summary>
        /// 剰余演算
        /// </summary>
        public static FixedPointNumber operator %(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits % valueR.bits };
        }
        /// <summary>
        /// 論理積
        /// </summary>
        public static FixedPointNumber operator &(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits & valueR.bits };
        }
        /// <summary>
        /// 論理積
        /// </summary>
        public static FixedPointNumber operator &(FixedPointNumber valueL, BitsType valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits & valueR };
        }
        /// <summary>
        /// 論理和
        /// </summary>
        public static FixedPointNumber operator |(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits | valueR.bits };
        }
        /// <summary>
        /// 論理和
        /// </summary>
        public static FixedPointNumber operator |(FixedPointNumber valueL, BitsType valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits | valueR };
        }
        /// <summary>
        /// 排他的論理和
        /// </summary>
        public static FixedPointNumber operator ^(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits ^ valueR.bits };
        }
        /// <summary>
        /// 排他的論理和
        /// </summary>
        public static FixedPointNumber operator ^(FixedPointNumber valueL, BitsType valueR)
        {
            return new FixedPointNumber() { bits = valueL.bits ^ valueR };
        }
        /// <summary>
        /// 左シフト
        /// </summary>
        public static FixedPointNumber operator <<(FixedPointNumber value, int shift)
        {
            return new FixedPointNumber() { bits = value.bits << shift };
        }
        /// <summary>
        /// 右シフト
        /// </summary>
        public static FixedPointNumber operator >>(FixedPointNumber value, int shift)
        {
            return new FixedPointNumber() { bits = value.bits >> shift };
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits == valueR.bits;
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits != valueR.bits;
        }
        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits > valueR.bits;
        }
        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits < valueR.bits;
        }
        /// <summary>
        /// 以上演算子
        /// </summary>
        public static bool operator >=(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits >= valueR.bits;
        }
        /// <summary>
        /// 以下演算子
        /// </summary>
        public static bool operator <=(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            return valueL.bits <= valueR.bits;
        }
        #endregion 比較演算子

        #region 型変換
        #region 他の型→固定小数点数型
        public static explicit operator FixedPointNumber(byte value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(sbyte value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(short value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(ushort value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(int value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(uint value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(long value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(ulong value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(Int128 value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(UInt128 value) => new((IntegerType)value, 0);
        public static explicit operator FixedPointNumber(Half value) => (FixedPointNumber)(double)value;
        public static explicit operator FixedPointNumber(float value) => (FixedPointNumber)(double)value;
        public static explicit operator FixedPointNumber(double value)
        {
            value *= OneBits;
            return new FixedPointNumber() { bits = (BitsType)value };
        }
        public static explicit operator FixedPointNumber(decimal value)
        {
            value *= OneBits;
            return new FixedPointNumber() { bits = (BitsType)value };
        }
        #endregion 他の型→固定小数点数型
        #region 固定小数点数型→他の型
        public static explicit operator byte(FixedPointNumber value) => (byte)Truncate(value).integer;
        public static explicit operator sbyte(FixedPointNumber value) => (sbyte)Truncate(value).integer;
        public static explicit operator short(FixedPointNumber value) => (short)Truncate(value).integer;
        public static explicit operator ushort(FixedPointNumber value) => (ushort)Truncate(value).integer;
        public static explicit operator int(FixedPointNumber value) => (int)Truncate(value).integer;
        public static explicit operator uint(FixedPointNumber value) => (uint)Truncate(value).integer;
        public static explicit operator long(FixedPointNumber value) => (long)Truncate(value).integer;
        public static explicit operator ulong(FixedPointNumber value) => (ulong)Truncate(value).integer;
        public static explicit operator Int128(FixedPointNumber value) => (Int128)Truncate(value).integer;
        public static explicit operator UInt128(FixedPointNumber value) => (UInt128)Truncate(value).integer;
        public static explicit operator Half(FixedPointNumber value) => (Half)(double)value;
        public static explicit operator float(FixedPointNumber value) => (float)(double)value;
        public static explicit operator double(FixedPointNumber value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        public static explicit operator decimal(FixedPointNumber value)
        {
            decimal temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        #endregion 固定小数点数型→他の型
        #endregion 型変換

        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(object obj)
        {
            return CompareTo((FixedPointNumber)obj);
        }
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="other">比較対象の固定小数点数</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(FixedPointNumber other)
        {
            return bits.CompareTo(other.bits);
        }
        #endregion IComparable

        #region IEquatable
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        public bool Equals(FixedPointNumber other)
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
            if (obj is FixedPointNumber)
            {
                return Equals((FixedPointNumber)obj);
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

        #region

        public static FixedPointNumber Atan2(FixedPointNumber y, FixedPointNumber x)
            => Math.Atan2(y, x);

        public static FixedPointNumber Atan2Pi(FixedPointNumber y, FixedPointNumber x)
            => Math.Atan2Pi(y, x);

        public static FixedPointNumber BitDecrement(FixedPointNumber x)
        {
            FixedPointNumber fixedPointNumber = new();
            fixedPointNumber.bits--;
            return fixedPointNumber;
        }

        public static FixedPointNumber BitIncrement(FixedPointNumber x)
        {
            FixedPointNumber fixedPointNumber = new();
            fixedPointNumber.bits++;
            return fixedPointNumber;
        }

        public static FixedPointNumber FusedMultiplyAdd(FixedPointNumber left, FixedPointNumber right, FixedPointNumber addend)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Ieee754Remainder(FixedPointNumber left, FixedPointNumber right)
        {
            throw new NotImplementedException();
        }

        public static int ILogB(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber ScaleB(FixedPointNumber x, int n)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Exp(FixedPointNumber x)
            => Math.Exp(x);

        public static FixedPointNumber Exp10(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Exp2(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public int GetExponentByteCount()
        {
            throw new NotImplementedException();
        }

        public int GetExponentShortestBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandByteCount()
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Round(FixedPointNumber x, int digits, MidpointRounding mode)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Acosh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Asinh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Atanh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Cosh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Sinh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Tanh(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Log(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Log(FixedPointNumber x, FixedPointNumber newBase)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Log10(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Log2(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Pow(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Cbrt(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Hypot(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber RootN(FixedPointNumber x, int n)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Sqrt(FixedPointNumber x)
            => Math.Sqrt(x);

        public static FixedPointNumber Acos(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber AcosPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Asin(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber AsinPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Atan(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber AtanPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Cos(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber CosPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Sin(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static (FixedPointNumber Sin, FixedPointNumber Cos) SinCos(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static (FixedPointNumber SinPi, FixedPointNumber CosPi) SinCosPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber SinPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Tan(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber TanPi(FixedPointNumber x)
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(FixedPointNumber value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(FixedPointNumber value) => false;

        public static bool IsEvenInteger(FixedPointNumber value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(FixedPointNumber value) => true;

        public static bool IsImaginaryNumber(FixedPointNumber value) => false;

        public static bool IsInfinity(FixedPointNumber value) => false;

        public static bool IsInteger(FixedPointNumber value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(FixedPointNumber value) => false;

        public static bool IsNegative(FixedPointNumber value) => value.bits < 0;

        public static bool IsNegativeInfinity(FixedPointNumber value) => false;

        public static bool IsNormal(FixedPointNumber value) => false;

        public static bool IsOddInteger(FixedPointNumber value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(FixedPointNumber value) => value.bits >= 0;

        public static bool IsPositiveInfinity(FixedPointNumber value) => false;

        public static bool IsRealNumber(FixedPointNumber value) => true;

        public static bool IsSubnormal(FixedPointNumber value) => false;

        public static bool IsZero(FixedPointNumber value) => value.bits == 0;

        public static FixedPointNumber MaxMagnitude(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber MaxMagnitudeNumber(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber MinMagnitude(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber MinMagnitudeNumber(FixedPointNumber x, FixedPointNumber y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointNumber result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(char))
            {
                char actualValue = (char)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(byte))
            {
                byte actualValue = (byte)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                sbyte actualValue = (sbyte)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                short actualValue = (short)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                ushort actualValue = (ushort)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                int actualValue = (int)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                uint actualValue = (uint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                long actualValue = (long)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                ulong actualValue = (ulong)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                Int128 actualValue = (Int128)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                UInt128 actualValue = (UInt128)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                nint actualValue = (nint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                nuint actualValue = (nuint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualValue = (Half)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualValue = (float)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualValue = (double)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                decimal actualValue = (decimal)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointNumber result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointNumber result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(FixedPointNumber value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(FixedPointNumber value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(FixedPointNumber value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointNumber result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointNumber result)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ((double)this).TryFormat(destination, out charsWritten, format, provider);

        public string ToString(string? format, IFormatProvider? formatProvider)
            => ((double)this).ToString(format, formatProvider);

        public static FixedPointNumber Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointNumber result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointNumber Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointNumber result)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}