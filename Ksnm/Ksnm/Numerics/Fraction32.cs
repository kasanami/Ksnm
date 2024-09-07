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
using Ksnm.ExtensionMethods.System.Decimal;
using Ksnm.ExtensionMethods.System.Double;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics
{
    // コードを再利用するためのエイリアスを定義
    using Int = Int16;
    using Int2 = Int32;// 計算途中に使用する
    using Fraction = Fraction32;
    /// <summary>
    /// 符号付き32ビット分数(16ビット×2)
    /// </summary>
    public struct Fraction32 :
        IComparable, IComparable<Fraction>, IEquatable<Fraction>,
        IFloatingPointIeee754<Fraction>
    {
        #region 定数
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public static Fraction Zero => new Fraction(0);
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public static Fraction One => new Fraction(1);
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public static Fraction MinusOne => new Fraction(-1);
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public static Fraction MinValue => new Fraction(Int.MinValue);
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public static Fraction MaxValue => new Fraction(Int.MaxValue);
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public static Fraction Epsilon => new Fraction(1, Int.MaxValue);

        public static Fraction NaN => new Fraction(0, 0);

        public static Fraction NegativeInfinity => new Fraction(-1, 0);

        public static Fraction NegativeZero => new Fraction(-0, 0);

        public static Fraction PositiveInfinity => new Fraction(1, 0);

        public static Fraction NegativeOne => new Fraction(-1);

        public static Fraction E => throw new NotImplementedException();

        public static Fraction Pi => throw new NotImplementedException();

        public static Fraction Tau => throw new NotImplementedException();

        static Fraction INumberBase<Fraction>.One => One;

        public static int Radix => 2;

        static Fraction INumberBase<Fraction>.Zero => Zero;

        public static Fraction AdditiveIdentity => Zero;

        public static Fraction MultiplicativeIdentity => One;

        static Fraction IFloatingPointIeee754<Fraction>.NaN => NaN;

        static Fraction IFloatingPointIeee754<Fraction>.NegativeInfinity => throw new NotImplementedException();

        static Fraction IFloatingPointIeee754<Fraction>.NegativeZero => throw new NotImplementedException();

        static Fraction IFloatingPointIeee754<Fraction>.PositiveInfinity => throw new NotImplementedException();

        static Fraction ISignedNumber<Fraction>.NegativeOne => throw new NotImplementedException();

        static Fraction IFloatingPointConstants<Fraction>.E => throw new NotImplementedException();

        static Fraction IFloatingPointConstants<Fraction>.Pi => throw new NotImplementedException();

        static Fraction IFloatingPointConstants<Fraction>.Tau => throw new NotImplementedException();

        static int INumberBase<Fraction>.Radix => 2;

        static Fraction IAdditiveIdentity<Fraction, Fraction>.AdditiveIdentity => AdditiveIdentity;

        static Fraction IMultiplicativeIdentity<Fraction, Fraction>.MultiplicativeIdentity => MultiplicativeIdentity;
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 分子
        /// </summary>
        public Int Numerator { get; set; }
        /// <summary>
        /// 分母
        /// </summary>
        public Int Denominator { get; set; }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 分子と分母を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        public Fraction32(Int numerator, Int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            Reduce();
        }
        /// <summary>
        /// 分子を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        public Fraction32(Int numerator) : this(numerator, 1)
        {
        }
        public Fraction32(ExtendedDouble value)
        {
            var mantissa = value.Mantissa;
            var exponent = value.Exponent;
            var denominator = 1ul;
            // 指数が
            if (exponent > 0)
            {
                var scale = Math.Pow(2ul, exponent);
                mantissa *= scale;
            }
            else if (exponent < 0)
            {
                var scale = Math.Pow(2ul, -exponent);
                denominator = scale;
            }
            // 分子
            Numerator = Int.CreateChecked(mantissa);
            // 分母
            Denominator = Int.CreateChecked(denominator);
            // 符号
            if (value.SignBit == 1)
            {
                Numerator = (Int)(-Numerator);
            }
        }
        public Fraction32(ExtendedDecimal value)
        {
            var mantissa = value.Mantissa;
            var exponent = value.Exponent;
            var denominator = 1ul;
            // 指数が
            if (exponent > 0)
            {
                var scale = Math.Pow(10ul, exponent);
                mantissa *= scale;
            }
            else if (exponent < 0)
            {
                var scale = Math.Pow(10ul, -exponent);
                denominator = scale;
            }
            // 分子
            Numerator = Int.CreateChecked(mantissa);
            // 分母
            Denominator = Int.CreateChecked(denominator);
            // 符号
            if (value.SignBit == 1)
            {
                Numerator = (Int)(-Numerator);
            }
        }
        #endregion コンストラクタ

        /// <summary>
        /// 約分する。
        /// <para>可約でない場合は何もしません。</para>
        /// </summary>
        public void Reduce()
        {
            var gcd = Math.GreatestCommonDivisor(Numerator, Denominator);
            if (gcd > 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }
        /// <summary>
        /// 可約ならtrueを返す。
        /// <para>判定後にReduce()を呼び出すより、Reduce()単体で使用したほうが効率的です。</para>
        /// </summary>
        public bool IsReducible()
        {
            var gcd = Math.GreatestCommonDivisor(Numerator, Denominator);
            return gcd > 1;
        }
        /// <summary>
        /// 逆数を返す。
        /// </summary>
        public Fraction GetReciprocal()
        {
            return new Fraction(Denominator, Numerator);
        }

        #region 単項演算子
        /// <summary>
        /// 符号維持
        /// </summary>
        public static Fraction operator +(Fraction value)
        {
            return value;
        }
        /// <summary>
        /// 符号反転
        /// <para>変更されるのは Numerator</para>
        /// </summary>
        public static Fraction operator -(Fraction value)
        {
            return new Fraction((Int)(-value.Numerator), value.Denominator);
        }
        /// <summary>
        /// valueの補数を返す
        /// </summary>
        public static Fraction operator ~(Fraction value)
        {
            return new Fraction((Int)(~value.Numerator), (Int)(~value.Denominator));
        }
        public static Fraction operator --(Fraction value)
        {
            return value - 1;
        }
        public static Fraction operator ++(Fraction value)
        {
            return value + 1;
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Fraction operator +(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            if (valueR.Denominator == valueL.Denominator)
            {
                temp.Numerator = Int.CreateChecked(valueL.Numerator + valueR.Numerator);
                temp.Denominator = valueL.Denominator;
            }
            else
            {
                temp.Numerator = Int.CreateChecked(valueL.Numerator * valueR.Denominator + valueR.Numerator * valueL.Denominator);
                temp.Denominator = (Int)(valueL.Denominator * valueR.Denominator);
            }
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Fraction operator -(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            if (valueR.Denominator == valueL.Denominator)
            {
                temp.Numerator = Int.CreateChecked(valueL.Numerator - valueR.Numerator);
                temp.Denominator = valueL.Denominator;
            }
            else
            {
                temp.Numerator = Int.CreateChecked(valueL.Numerator * valueR.Denominator - valueR.Numerator * valueL.Denominator);
                temp.Denominator = Int.CreateChecked(valueL.Denominator * valueR.Denominator);
            }
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Fraction operator *(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = Int.CreateChecked(valueL.Numerator * valueR.Numerator);
            temp.Denominator = Int.CreateChecked(valueL.Denominator * valueR.Denominator);
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Fraction operator /(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = Int.CreateChecked(valueL.Numerator * valueR.Denominator);
            temp.Denominator = Int.CreateChecked(valueL.Denominator * valueR.Numerator);
            temp.Reduce();
            return temp;
        }

        public static Fraction operator %(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator == valueR.Numerator && valueL.Denominator == valueR.Denominator;
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator != valueR.Numerator || valueL.Denominator != valueR.Denominator;
        }
        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator > valueR.Numerator * valueL.Denominator;
        }
        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator < valueR.Numerator * valueL.Denominator;
        }
        /// <summary>
        /// 以上演算子
        /// </summary>
        public static bool operator >=(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator >= valueR.Numerator * valueL.Denominator;
        }
        /// <summary>
        /// 以下演算子
        /// </summary>
        public static bool operator <=(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator <= valueR.Numerator * valueL.Denominator;
        }
        #endregion 比較演算子

        #region 型変換
        #region 他の型→分数型
        /// <summary>
        /// byte から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(byte value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// sbyte から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(sbyte value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// short から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(short value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// ushort から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(ushort value)
        {
            return new Fraction((Int)value);
        }
        /// <summary>
        /// int から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(int value)
        {
            return new Fraction((Int)value);
        }
        /// <summary>
        /// uint から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(uint value)
        {
            return new Fraction((Int)value);
        }
        /// <summary>
        /// long から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(long value)
        {
            return new Fraction((Int)value);
        }
        /// <summary>
        /// ulong から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(ulong value)
        {
            return new Fraction((Int)value);
        }
        /// <summary>
        /// float から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(float value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// double から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(double value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// decimal から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(decimal value)
        {
            return new Fraction(value);
        }
        #endregion 他の型→分数型
        #region 分数型→他の型
        /// <summary>
        /// 分数型 から byte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator byte(Fraction value)
        {
            return (byte)(int)value;
        }
        /// <summary>
        /// 分数型 から sbyte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator sbyte(Fraction value)
        {
            return (sbyte)(int)value;
        }
        /// <summary>
        /// 分数型 から short への明示的な変換を定義します。
        /// </summary>
        public static explicit operator short(Fraction value)
        {
            return (short)(int)value;
        }
        /// <summary>
        /// 分数型 から ushort への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ushort(Fraction value)
        {
            return (ushort)(int)value;
        }
        /// <summary>
        /// 分数型 から int への明示的な変換を定義します。
        /// </summary>
        public static explicit operator int(Fraction value)
        {
            return value.Numerator / value.Denominator;
        }
        /// <summary>
        /// 分数型 から uint への明示的な変換を定義します。
        /// </summary>
        public static explicit operator uint(Fraction value)
        {
            return (uint)(int)value;
        }
        /// <summary>
        /// 分数型 から long への明示的な変換を定義します。
        /// </summary>
        public static explicit operator long(Fraction value)
        {
            return (int)value;
        }
        /// <summary>
        /// 分数型 から ulong への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ulong(Fraction value)
        {
            return (ulong)(int)value;
        }
        /// <summary>
        /// 分数型 から float への明示的な変換を定義します。
        /// </summary>
        public static explicit operator float(Fraction value)
        {
            return (float)(double)value;
        }
        /// <summary>
        /// 分数型 から double への明示的な変換を定義します。
        /// </summary>
        public static explicit operator double(Fraction value)
        {
            double temp = value.Numerator;
            temp /= value.Denominator;
            return temp;
        }
        /// <summary>
        /// 分数型 から decimal への明示的な変換を定義します。
        /// </summary>
        public static explicit operator decimal(Fraction value)
        {
            decimal temp = value.Numerator;
            temp /= value.Denominator;
            return temp;
        }
        #endregion 分数型→他の型
        #endregion 型変換

        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(object obj)
        {
            return CompareTo((Fraction)obj);
        }
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="other">比較対象</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(Fraction other)
        {
            return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);
        }
        #endregion IComparable

        #region IEquatable
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。</returns>
        public bool Equals(Fraction other)
        {
            return CompareTo(other) == 0;
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
            if (obj is Fraction)
            {
                return Equals((Fraction)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public static Fraction Atan2(Fraction y, Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Atan2Pi(Fraction y, Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction BitDecrement(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction BitIncrement(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction FusedMultiplyAdd(Fraction left, Fraction right, Fraction addend)
        {
            throw new NotImplementedException();
        }

        public static Fraction Ieee754Remainder(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static int ILogB(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction ScaleB(Fraction x, int n)
        {
            throw new NotImplementedException();
        }

        public static Fraction Exp(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Exp10(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Exp2(Fraction x)
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

        public static Fraction Round(Fraction x, int digits, MidpointRounding mode)
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

        public static Fraction Acosh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Asinh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Atanh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Cosh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Sinh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Tanh(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Log(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Log(Fraction x, Fraction newBase)
        {
            throw new NotImplementedException();
        }

        public static Fraction Log10(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Log2(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Pow(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction Cbrt(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Hypot(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction RootN(Fraction x, int n)
        {
            throw new NotImplementedException();
        }

        public static Fraction Sqrt(Fraction x)
        {
            return Math.Sqrt(x, Epsilon, 1);
        }

        public static Fraction Acos(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction AcosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Asin(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction AsinPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Atan(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction AtanPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Cos(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction CosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Sin(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static (Fraction Sin, Fraction Cos) SinCos(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static (Fraction SinPi, Fraction CosPi) SinCosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction SinPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Tan(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction TanPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        public static Fraction Abs(Fraction value)
        {
            if (IsNegative(value))
            {
                return -value;
            }
            return value;
        }

        public static bool IsCanonical(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(Fraction value) => value < 0;

        public static bool IsNegativeInfinity(Fraction value)
        {
            return value.Numerator < 0 && value.Denominator == 0;
        }

        public static bool IsNormal(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositiveInfinity(Fraction value)
        {
            return value.Numerator > 0 && value.Denominator == 0;
        }

        public static bool IsRealNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(Fraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Fraction value)
        {
            return value == 0;
        }

        public static Fraction MaxMagnitude(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction MaxMagnitudeNumber(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction MinMagnitude(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction MinMagnitudeNumber(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        public static Fraction Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Fraction Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public static Fraction Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
        {
            throw new NotImplementedException();
        }

        public static Fraction Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.Atan2(Fraction y, Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.Atan2Pi(Fraction y, Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.BitDecrement(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.BitIncrement(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.FusedMultiplyAdd(Fraction left, Fraction right, Fraction addend)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.Ieee754Remainder(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        static int IFloatingPointIeee754<Fraction>.ILogB(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPointIeee754<Fraction>.ScaleB(Fraction x, int n)
        {
            throw new NotImplementedException();
        }

        static Fraction IExponentialFunctions<Fraction>.Exp(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IExponentialFunctions<Fraction>.Exp10(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IExponentialFunctions<Fraction>.Exp2(Fraction x)
        {
            throw new NotImplementedException();
        }

        int IFloatingPoint<Fraction>.GetExponentByteCount()
        {
            throw new NotImplementedException();
        }

        int IFloatingPoint<Fraction>.GetExponentShortestBitLength()
        {
            throw new NotImplementedException();
        }

        int IFloatingPoint<Fraction>.GetSignificandBitLength()
        {
            throw new NotImplementedException();
        }

        int IFloatingPoint<Fraction>.GetSignificandByteCount()
        {
            throw new NotImplementedException();
        }

        static Fraction IFloatingPoint<Fraction>.Round(Fraction x, int digits, MidpointRounding mode)
        {
            throw new NotImplementedException();
        }

        bool IFloatingPoint<Fraction>.TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        bool IFloatingPoint<Fraction>.TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        bool IFloatingPoint<Fraction>.TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        bool IFloatingPoint<Fraction>.TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        int IComparable.CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        int IComparable<Fraction>.CompareTo(Fraction other)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Acosh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Asinh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Atanh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Cosh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Sinh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IHyperbolicFunctions<Fraction>.Tanh(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ILogarithmicFunctions<Fraction>.Log(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ILogarithmicFunctions<Fraction>.Log(Fraction x, Fraction newBase)
        {
            throw new NotImplementedException();
        }

        static Fraction ILogarithmicFunctions<Fraction>.Log10(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ILogarithmicFunctions<Fraction>.Log2(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IPowerFunctions<Fraction>.Pow(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction IRootFunctions<Fraction>.Cbrt(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction IRootFunctions<Fraction>.Hypot(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction IRootFunctions<Fraction>.RootN(Fraction x, int n)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Acos(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.AcosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Asin(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.AsinPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Atan(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.AtanPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Cos(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.CosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Sin(Fraction x)
        {
            throw new NotImplementedException();
        }

        static (Fraction Sin, Fraction Cos) ITrigonometricFunctions<Fraction>.SinCos(Fraction x)
        {
            throw new NotImplementedException();
        }

        static (Fraction SinPi, Fraction CosPi) ITrigonometricFunctions<Fraction>.SinCosPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.SinPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.Tan(Fraction x)
        {
            throw new NotImplementedException();
        }

        static Fraction ITrigonometricFunctions<Fraction>.TanPi(Fraction x)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsCanonical(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsComplexNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsEvenInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsFinite(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsImaginaryNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsInfinity(Fraction value)
        {
            return value.Numerator != 0 && value.Denominator == 0;
        }

        static bool INumberBase<Fraction>.IsInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsNaN(Fraction value)
        {
            return value.Numerator == 0 && value.Denominator == 0;
        }

        static bool INumberBase<Fraction>.IsNegative(Fraction value)
        {
            return value.Numerator == 0 && value.Denominator == 0;
        }

        static bool INumberBase<Fraction>.IsNegativeInfinity(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsNormal(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsOddInteger(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsPositive(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsPositiveInfinity(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsRealNumber(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsSubnormal(Fraction value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.IsZero(Fraction value)
        {
            return value.Numerator == 0;
        }

        static Fraction INumberBase<Fraction>.MaxMagnitude(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction INumberBase<Fraction>.MaxMagnitudeNumber(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction INumberBase<Fraction>.MinMagnitude(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction INumberBase<Fraction>.MinMagnitudeNumber(Fraction x, Fraction y)
        {
            throw new NotImplementedException();
        }

        static Fraction INumberBase<Fraction>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static Fraction INumberBase<Fraction>.Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryConvertFromChecked<TOther>(TOther value, out Fraction result)
        {
            if (typeof(TOther) == typeof(byte))
            {
                var actualValue = (byte)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                var actualValue = (sbyte)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                var actualValue = (char)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                var actualValue = (short)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                var actualValue = (ushort)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                var actualValue = (int)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                var actualValue = (uint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                var actualValue = (long)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                var actualValue = (ulong)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                var actualValue = (Int128)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                var actualValue = (UInt128)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                var actualValue = (nint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                var actualValue = (nuint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                var actualValue = (Half)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                var actualValue = (float)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                var actualValue = (double)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                var actualValue = (decimal)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(BigInteger))
            {
                var actualValue = (BigInteger)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool INumberBase<Fraction>.TryConvertFromSaturating<TOther>(TOther value, out Fraction result)
        {
            if (typeof(TOther) == typeof(byte))
            {
                var actualValue = (byte)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                var actualValue = (sbyte)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                var actualValue = (char)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                var actualValue = (short)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                var actualValue = (ushort)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                var actualValue = (int)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                var actualValue = (uint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                var actualValue = (long)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                var actualValue = (ulong)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                var actualValue = (Int128)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                var actualValue = (UInt128)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                var actualValue = (nint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                var actualValue = (nuint)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                var actualValue = (Half)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                var actualValue = (float)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                var actualValue = (double)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                var actualValue = (decimal)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(BigInteger))
            {
                var actualValue = (BigInteger)(object)value;
                result = checked((Int)actualValue);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool INumberBase<Fraction>.TryConvertFromTruncating<TOther>(TOther value, out Fraction result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryConvertToChecked<TOther>(Fraction value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryConvertToSaturating<TOther>(Fraction value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryConvertToTruncating<TOther>(Fraction value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Fraction result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction>.TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Fraction result)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<Fraction>.Equals(Fraction other)
        {
            throw new NotImplementedException();
        }

        bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => TryFormat(destination, out charsWritten, format, provider);

        string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        static Fraction ISpanParsable<Fraction>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool ISpanParsable<Fraction>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Fraction result)
        {
            throw new NotImplementedException();
        }

        static Fraction IParsable<Fraction>.Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool IParsable<Fraction>.TryParse(string? s, IFormatProvider? provider, out Fraction result)
        {
            throw new NotImplementedException();
        }
        #endregion object
    }
}