﻿/*
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
using System.Linq;
using System.Numerics;
using Ksnm.ExtensionMethods.System.Decimal;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 任意の大きさを持つ10 進数の浮動小数点数を表します。
    /// 
    /// BigDecimal=Mantissa*10^Exponent
    /// </summary>
    public struct BigDecimal : IEquatable<BigDecimal>
    {
        #region 定数
        /// <summary>
        /// 十進数の底（てい）
        /// </summary>
        public const int Base = 10;
        /// <summary>
        /// MinExponentの初期値
        /// </summary>
        public const int DefaultMinExponent = -10;
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 指数部
        /// </summary>
        public int Exponent { get; private set; }
        /// <summary>
        /// 仮数部
        /// </summary>
        public BigInteger Mantissa { get; private set; }
        /// <summary>
        /// 指数部の最小値
        /// 無理数の場合にこの桁数で丸める
        /// 精度とも言える
        /// </summary>
        public int MinExponent { get; private set; }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        /// <param name="other">コピー元</param>
        public BigDecimal(BigDecimal other)
        {
            Exponent = other.Exponent;
            Mantissa = other.Mantissa;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        /// <param name="mantissa">仮数部</param>
        /// <param name="exponent">指数部</param>
        public BigDecimal(BigInteger mantissa, int exponent)
        {
            Exponent = exponent;
            Mantissa = mantissa;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(int value)
        {
            Exponent = 0;
            Mantissa = value;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(decimal value)
        {
            Exponent = value.GetExponent();
            Mantissa = (BigInteger)value.GetMantissa();
            Mantissa *= value.GetSign();
            MinExponent = DefaultMinExponent;
        }
        #endregion コンストラクタ

        #region 独自メソッド
        /// <summary>
        /// Exponent が最小になるように変換します。
        /// Mantissa は大きくなります。
        /// </summary>
        public void MinimizeExponent()
        {
            if (Exponent > MinExponent)
            {
                var diff = Exponent - MinExponent;
                Exponent = MinExponent;
                Mantissa *= BigInteger.Pow(Base, diff);
            }
        }
        /// <summary>
        /// Mantissa が最小になるように変換します。
        /// Exponent が大きくなります。
        /// </summary>
        public void MinimizeMantissa()
        {
            // 10^iで割り切れる値の最大
            BigInteger maxDivisor = 0;
            int maxExponent = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                var divisor = BigInteger.Pow(Base, i);
                // divisor のほうが大きいなら終了
                if (Mantissa < divisor)
                {
                    break;
                }
                // 10^iで割り切れるか
                if (Mantissa % divisor == 0)
                {
                    // 更新して次へ
                    maxDivisor = divisor;
                    maxExponent = i;
                }
                else
                {
                    // 終了
                    break;
                }
            }
            //
            if (maxDivisor > 0)
            {
                Exponent += maxExponent;
                Mantissa /= maxDivisor;
            }
        }
        #endregion 独自メソッド

        #region 単項演算子
        /// <summary>
        /// 符号維持
        /// </summary>
        public static BigDecimal operator +(BigDecimal value)
        {
            return value;
        }
        /// <summary>
        /// 符号反転
        /// <para>変更されるのは Mantissa</para>
        /// </summary>
        public static BigDecimal operator -(BigDecimal value)
        {
            return new BigDecimal(-value.Mantissa, value.Exponent);
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static BigDecimal operator +(BigDecimal valueL, BigDecimal valueR)
        {
            if (valueL.Exponent > valueR.Exponent)
            {
                var temp = new BigDecimal(valueL);
                var scale = temp.Exponent - valueR.Exponent;
                temp.Mantissa *= BigInteger.Pow(Base, scale);
                temp.Mantissa += valueR.Mantissa;
                temp.Exponent = valueR.Exponent;
                return temp;
            }
            else if (valueL.Exponent < valueR.Exponent)
            {
                var temp = new BigDecimal(valueR);
                var scale = temp.Exponent - valueL.Exponent;
                temp.Mantissa *= BigInteger.Pow(Base, scale);
                temp.Mantissa += valueL.Mantissa;
                temp.Exponent = valueL.Exponent;
                return temp;
            }
            return new BigDecimal(valueL.Mantissa + valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static BigDecimal operator -(BigDecimal valueL, BigDecimal valueR)
        {
            if (valueL.Exponent > valueR.Exponent)
            {
                var temp = new BigDecimal(valueL);
                var scale = temp.Exponent - valueR.Exponent;
                temp.Mantissa *= BigInteger.Pow(Base, scale);
                temp.Mantissa -= valueR.Mantissa;
                temp.Exponent = valueR.Exponent;
                return temp;
            }
            else if (valueL.Exponent < valueR.Exponent)
            {
                var temp = new BigDecimal(valueR);
                var scale = temp.Exponent - valueL.Exponent;
                temp.Mantissa *= BigInteger.Pow(Base, scale);
                temp.Mantissa -= valueL.Mantissa;
                temp.Exponent = valueL.Exponent;
                return temp;
            }
            return new BigDecimal(valueL.Mantissa - valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(BigDecimal valueL, BigDecimal valueR)
        {
            var temp = new BigDecimal(valueL);
            temp.Mantissa *= valueR.Mantissa;
            temp.Exponent += valueR.Exponent;
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(BigDecimal valueL, int valueR)
        {
            var temp = new BigDecimal(valueL);
            temp.Mantissa *= valueR;
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static BigDecimal operator /(BigDecimal valueL, BigDecimal valueR)
        {
            var temp = new BigDecimal(valueL);
            // 割られる数の Exponent を最小にする。
            temp.MinimizeExponent();
            //
            temp.Mantissa /= valueR.Mantissa;
            temp.Exponent -= valueR.Exponent;
            return temp;
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(BigDecimal valueL, BigDecimal valueR)
        {
            return valueL.Mantissa == valueR.Mantissa && valueL.Exponent == valueR.Exponent;
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(BigDecimal valueL, BigDecimal valueR)
        {
            return valueL.Mantissa != valueR.Mantissa || valueL.Exponent != valueR.Exponent;
        }
        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(BigDecimal valueL, BigDecimal valueR)
        {
            _ConvertSameExponent(ref valueL, ref valueR);
            return valueL.Mantissa > valueR.Mantissa;
        }
        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(BigDecimal valueL, BigDecimal valueR)
        {
            _ConvertSameExponent(ref valueL, ref valueR);
            return valueL.Mantissa < valueR.Mantissa;
        }
        /// <summary>
        /// 以上演算子
        /// </summary>
        public static bool operator >=(BigDecimal valueL, BigDecimal valueR)
        {
            _ConvertSameExponent(ref valueL, ref valueR);
            return valueL.Mantissa >= valueR.Mantissa;
        }
        /// <summary>
        /// 以下演算子
        /// </summary>
        public static bool operator <=(BigDecimal valueL, BigDecimal valueR)
        {
            _ConvertSameExponent(ref valueL, ref valueR);
            return valueL.Mantissa <= valueR.Mantissa;
        }
        /// <summary>
        /// 指定した双方の値を、同じ Exponent になるように変換する
        /// ※Mantissa は調整するが、Exponent は調整しないので、元の値から大きさが変わる。
        /// </summary>
        private static void _ConvertSameExponent(ref BigDecimal valueL, ref BigDecimal valueR)
        {
            if (valueL.Exponent > valueR.Exponent)
            {
                var exponentDiff = valueL.Exponent - valueR.Exponent;
                valueL.Mantissa *= BigInteger.Pow(Base, exponentDiff);
            }
            else if (valueR.Exponent > valueL.Exponent)
            {
                var exponentDiff = valueR.Exponent - valueL.Exponent;
                valueR.Mantissa *= BigInteger.Pow(Base, exponentDiff);
            }
        }
        #endregion 比較演算子

        #region To*
        /// <summary>
        /// decimal へ変換します。
        /// </summary>
        public decimal ToDecimal()
        {
            // mantissa は正の数にする
            var mantissa = BigInteger.Abs(Mantissa);
            // decimal は正の Exponent に対応していないので、mantissa を変換
            if (Exponent > 0)
            {
                mantissa *= BigInteger.Pow(Base, Exponent);
            }
            // [0]=最上位
            var bytes = mantissa.ToByteArray().ToList();
            if (bytes.Count > (4 * 3))
            {
                throw new InvalidCastException($"{nameof(Mantissa)}が decimal の最大値より大きい");
            }
            while (bytes.Count < (4 * 3))
            {
                bytes.Add(0);
            }
            var bytes2 = bytes.ToArray();
            int lo = BitConverter.ToInt32(bytes2, 0);
            int mid = BitConverter.ToInt32(bytes2, 4);
            int hi = BitConverter.ToInt32(bytes2, 8);
            bool isNegative = Mantissa < 0;
            byte scale = 0;
            if (Exponent < 0)
            {
                scale = (byte)(-Exponent);
            }
            return new decimal(lo, mid, hi, isNegative, scale);
        }
        #endregion To*

        #region object
        /// <summary>
        /// 現在のインスタンスの値と指定されたオブジェクトの値が等しいかどうかを示す値を返します。
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is BigDecimal)
            {
                return Equals((BigDecimal)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Mantissa.GetHashCode() ^ Exponent.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return $"({Mantissa}*10^{Exponent})";
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した値と等しいかどうかを示す値を返します。
        /// </summary>
        public bool Equals(BigDecimal other)
        {
            if (Exponent != other.Exponent)
            {
                return false;
            }
            if (Mantissa != other.Mantissa)
            {
                return false;
            }
            return true;
        }
        #endregion IEquatable
    }
}