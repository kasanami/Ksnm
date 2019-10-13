﻿/*
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

namespace Ksnm.Numerics
{
    // コードを再利用するためのエイリアスを定義
    using Fraction = AutoReduceFraction32;
    /// <summary>
    /// 自動で約分する分数
    /// </summary>
    public struct AutoReduceFraction32 : IComparable, IComparable<Fraction>, IEquatable<Fraction>
    {
        #region プロパティ
        /// <summary>
        /// 分子
        /// </summary>
        public int Numerator { get; private set; }
        /// <summary>
        /// 分母
        /// </summary>
        public int Denominator { get; private set; }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 分子と分母を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        public AutoReduceFraction32(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            Reduce();
        }
        /// <summary>
        /// 分子を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        public AutoReduceFraction32(int numerator) : this(numerator, 1)
        {
        }
        /// <summary>
        /// 実数を分数に変換して初期化
        /// </summary>
        /// <param name="value">実数</param>
        public AutoReduceFraction32(double value) : this((decimal)value)
        {
#if false
            if (value >= 0)
            {
                Numerator = (int)value;
                Denominator = 1;
            }
            else
            {
                Numerator = (int)(value.GetMantissa() >> (52 - 31));
                Denominator = 1 << -value.GetExponent();
            }
#endif
        }
        /// <summary>
        /// 実数を分数に変換して初期化
        /// </summary>
        /// <param name="value">実数</param>
        public AutoReduceFraction32(decimal value)
        {
            var mantissa = value.GetMantissa();
            var exponent = value.GetExponentBits();
            var denominator = Math.Pow(10u, exponent);
            Numerator = (int)mantissa;
            Denominator = (int)denominator;
            if (value < 0)
            {
                Numerator = -Numerator;
            }
            Reduce();
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
            value.Numerator = -value.Numerator;
            return value;
        }
        /// <summary>
        /// valueの補数を返す
        /// </summary>
        public static Fraction operator ~(Fraction value)
        {
            value.Numerator = ~value.Numerator;
            value.Denominator = ~value.Denominator;
            return value;
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Fraction operator +(in Fraction valueL, in Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = valueL.Numerator * valueR.Denominator + valueR.Numerator * valueL.Denominator;
            temp.Denominator = valueL.Denominator * valueR.Denominator;
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Fraction operator -(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = valueL.Numerator * valueR.Denominator - valueR.Numerator * valueL.Denominator;
            temp.Denominator = valueL.Denominator * valueR.Denominator;
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Fraction operator *(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = valueL.Numerator * valueR.Numerator;
            temp.Denominator = valueL.Denominator * valueR.Denominator;
            temp.Reduce();
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Fraction operator /(Fraction valueL, Fraction valueR)
        {
            var temp = new Fraction();
            temp.Numerator = valueL.Numerator * valueR.Denominator;
            temp.Denominator = valueL.Denominator * valueR.Numerator;
            temp.Reduce();
            return temp;
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator == valueR.Numerator * valueL.Denominator;
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(Fraction valueL, Fraction valueR)
        {
            return valueL.Numerator * valueR.Denominator != valueR.Numerator * valueL.Denominator;
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
            return new Fraction(value);
        }
        /// <summary>
        /// int から 分数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// uint から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(uint value)
        {
            return new Fraction((int)value);
        }
        /// <summary>
        /// long から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(long value)
        {
            return new Fraction((int)value);
        }
        /// <summary>
        /// ulong から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(ulong value)
        {
            return new Fraction((int)value);
        }
        /// <summary>
        /// float から 分数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Fraction(float value)
        {
            return (Fraction)(double)value;
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
        /// 分数型 から double への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator double(Fraction value)
        {
            double temp = value.Numerator;
            temp /= value.Denominator;
            return temp;
        }
        /// <summary>
        /// 分数型 から double への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator decimal(Fraction value)
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
            other.Numerator *= Denominator;
            return (Numerator * other.Denominator).CompareTo(other.Numerator);
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
        #endregion object
    }
}
