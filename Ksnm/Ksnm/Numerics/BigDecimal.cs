/*
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
    public class BigDecimal : IEquatable<BigDecimal>
    {
        #region 定数
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
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(int value)
        {
            Exponent = 0;
            Mantissa = value;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(decimal value)
        {
            Exponent = value.GetExponent();
            Mantissa = (BigInteger)value.GetMantissa();
            Mantissa *= value.GetSign();
        }
        #endregion コンストラクタ

        #region 単項演算子
        /// <summary>
        /// 符号維持
        /// </summary>
        public static BigDecimal operator +(in BigDecimal value)
        {
            return value;
        }
        /// <summary>
        /// 符号反転
        /// <para>変更されるのは Numerator</para>
        /// </summary>
        public static BigDecimal operator -(in BigDecimal value)
        {
            return new BigDecimal(-value.Mantissa, value.Exponent);
        }
        #endregion 単項演算子

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static BigDecimal operator +(in BigDecimal valueL, in BigDecimal valueR)
        {
            if (valueL.Exponent > valueR.Exponent)
            {
                var temp = new BigDecimal(valueL);
                var scale = temp.Exponent - valueR.Exponent;
                temp.Mantissa *= Math.Pow(10, scale);
                temp.Mantissa += valueR.Mantissa;
                temp.Exponent = valueR.Exponent;
                return temp;
            }
            else if (valueL.Exponent < valueR.Exponent)
            {
                var temp = new BigDecimal(valueR);
                var scale = temp.Exponent - valueL.Exponent;
                temp.Mantissa *= Math.Pow(10, scale);
                temp.Mantissa += valueL.Mantissa;
                temp.Exponent = valueL.Exponent;
                return temp;
            }
            return new BigDecimal(valueL.Mantissa + valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static BigDecimal operator -(in BigDecimal valueL, in BigDecimal valueR)
        {
            if (valueL.Exponent > valueR.Exponent)
            {
                var temp = new BigDecimal(valueL);
                var scale = temp.Exponent - valueR.Exponent;
                temp.Mantissa *= Math.Pow(10, scale);
                temp.Mantissa -= valueR.Mantissa;
                temp.Exponent = valueR.Exponent;
                return temp;
            }
            else if (valueL.Exponent < valueR.Exponent)
            {
                var temp = new BigDecimal(valueR);
                var scale = temp.Exponent - valueL.Exponent;
                temp.Mantissa *= Math.Pow(10, scale);
                temp.Mantissa -= valueL.Mantissa;
                temp.Exponent = valueL.Exponent;
                return temp;
            }
            return new BigDecimal(valueL.Mantissa - valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(in BigDecimal valueL, in BigDecimal valueR)
        {
            var temp = new BigDecimal(valueL);
            temp.Mantissa *= valueR.Mantissa;
            temp.Exponent += valueR.Exponent;
            return temp;
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(in BigDecimal valueL, in int valueR)
        {
            var temp = new BigDecimal(valueL);
            temp.Mantissa *= valueR;
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static BigDecimal operator /(in BigDecimal valueL, in BigDecimal valueR)
        {
            var temp = new BigDecimal(valueL);
            temp.Mantissa /= valueR.Mantissa;
            temp.Exponent -= valueR.Exponent;
            return temp;
        }
        #endregion 2項演算子

        #region To*
        /// <summary>
        /// decimal へ変換します。
        /// </summary>
        public decimal ToDecimal()
        {
            // [0]=最上位
            var mantissa = BigInteger.Abs(Mantissa);
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

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した FloatingPointNumber16 値に等しいかどうかを示す値を返します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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