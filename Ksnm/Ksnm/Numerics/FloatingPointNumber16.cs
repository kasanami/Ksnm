/*
The zlib License

Copyright (c) 2019-2021 Takahiro Kasanami

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
using Ksnm.ExtensionMethods.System.Double;
using System;
using BitsType = System.UInt16;// 半精度浮動小数点数 全体のビットを表す型

namespace Ksnm.Numerics
{
    /// <summary>
    /// 半精度浮動小数点数型
    /// </summary>
    public struct FloatingPointNumber16 : IEquatable<FloatingPointNumber16>
    {
        #region 定数
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 15;
        /// <summary>
        /// 指数の最小値
        /// </summary>
        public const int ExponentMinValue = -14;
        /// <summary>
        /// 指数の最大値
        /// </summary>
        public const int ExponentMaxValue = 15;
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public const BitsType ZeroBits = 0;
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public const BitsType OneBits = 0x3C00;
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public const BitsType MinusOneBits = 0xBC00;
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public const BitsType MinValueBits = 0xFBFF;
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public const BitsType MaxValueBits = 0x7BFF;
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public const BitsType EpsilonBits = 0x0001;
        /// <summary>
        /// 負の無限大を表します。
        /// </summary>
        public const BitsType NegativeInfinityBits = 0xFC00;
        /// <summary>
        /// 正の無限大を表します。
        /// </summary>
        public const BitsType PositiveInfinityBits = 0x7C00;
        /// <summary>
        /// 非数 (NaN) の値を表します。
        /// </summary>
        public const BitsType NaNBits = 0b1_11111_10_0000_0000;

        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 Zero = new FloatingPointNumber16 { Bits = ZeroBits };
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 One = new FloatingPointNumber16 { Bits = OneBits };
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MinusOne = new FloatingPointNumber16 { Bits = MinusOneBits };
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MinValue = new FloatingPointNumber16 { Bits = MinValueBits };
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 MaxValue = new FloatingPointNumber16 { Bits = MaxValueBits };
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 Epsilon = new FloatingPointNumber16 { Bits = EpsilonBits };
        /// <summary>
        /// 負の無限大を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 NegativeInfinity = new FloatingPointNumber16 { Bits = NegativeInfinityBits };
        /// <summary>
        /// 正の無限大を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 PositiveInfinity = new FloatingPointNumber16 { Bits = PositiveInfinityBits };
        /// <summary>
        /// 非数 (NaN) の値を表します。
        /// </summary>
        public readonly static FloatingPointNumber16 NaN = new FloatingPointNumber16 { Bits = NaNBits };
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public BitsType Bits { get; private set; }
        /// <summary>
        /// 符号部ビット
        /// 負数ならば 1 になる。
        /// </summary>
        public BitsType SignBits
        {
            get => (BitsType)((Bits & 0x8000) >> 15);
            set
            {
                value &= 1;
                if (value == 1)
                {
                    Bits |= 0x8000;
                }
                else
                {
                    Bits &= 0x7FFF;
                }
            }
        }
        /// <summary>
        /// 指数
        /// </summary>
        public int Exponent
        {
            get => ExponentBits - ExponentBias;
            set => ExponentBits = (ushort)(value + ExponentBias);
        }
        /// <summary>
        /// 指数部ビット
        /// </summary>
        public BitsType ExponentBits
        {
            get => (BitsType)((Bits & 0b01111100_00000000) >> 10);
            set => Bits ^= (BitsType)((value & 0b11111) << 10);
        }
        /// <summary>
        /// 仮数部ビット
        /// </summary>
        public BitsType MantissaBits
        {
            get => (BitsType)(Bits & 0b00000011_11111111);
            set => Bits ^= (BitsType)(value & 0b00000011_11111111);
        }
        #endregion プロパティ

        #region 型変換
        /// <summary>
        /// 倍精度浮動小数点数に変換する
        /// </summary>
        public double ToDouble()
        {
            ulong doubleBits = 0;
            // 符号部
            var sign = SignBits != 0;
            // 指数部
            int exponentBits = ExponentBits;
            // 仮数部
            ulong mantissaBits = MantissaBits;
            if (exponentBits == 0x1F)
            {
                if (mantissaBits == 0)
                {
                    if (sign)
                    {
                        return double.NegativeInfinity;
                    }
                    else
                    {
                        return double.PositiveInfinity;
                    }
                }
                else
                {
                    return double.NaN;
                }
            }
            else if (exponentBits == 0)
            {
                doubleBits = 0;
            }
            else
            {
                exponentBits -= ExponentBias;
                exponentBits += 1023;
                doubleBits |= (ulong)exponentBits << (52);
            }
            // 符号部
            if (sign)
            {
                doubleBits |= 0x8000_0000_0000_0000;
            }
            // 仮数部
            {
                doubleBits |= mantissaBits << (52 - 10);
            }
            return BitConverter.Int64BitsToDouble((long)doubleBits);
        }

        /// <summary>
        /// 倍精度浮動小数点数から半精度浮動小数点数型に変換する
        /// </summary>
        public static FloatingPointNumber16 FromDouble(double value)
        {
            FloatingPointNumber16 temp = Zero;
            // 符号部
            var sign = value.GetSignBits() != 0;
            // 指数部
            int exponentBits = value.GetExponentBits();
            // 仮数部
            var mantissaBits = value.GetMantissaBits();

            if (exponentBits == 0x7FF)
            {
                temp.ExponentBits = 0x1F;
            }
            else if (exponentBits == 0)
            {
                temp.ExponentBits = 0;
            }
            else
            {
                exponentBits -= 1023;
                if (exponentBits < ExponentMinValue)
                {
                    // 最小値未満
                    temp.Bits = EpsilonBits;
                }
                else if (exponentBits > ExponentMaxValue)
                {
                    // 最大値超過
                    temp.Bits = PositiveInfinityBits;
                }
                else
                {
                    exponentBits += ExponentBias;
                    temp.ExponentBits = (BitsType)exponentBits;
                }
            }
            // 符号部
            if (sign)
            {
                temp.SignBits = 1;
            }
            // 仮数部
            {
                temp.MantissaBits |= (BitsType)(mantissaBits >> (52 - 10));
            }
            return temp;
        }

        public static explicit operator FloatingPointNumber16(double value)
        {
            return FromDouble(value);
        }
        public static implicit operator float(FloatingPointNumber16 value)
        {
            return (float)value.ToDouble();
        }
        public static implicit operator double(FloatingPointNumber16 value)
        {
            return value.ToDouble();
        }
        #endregion 型変換

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した FloatingPointNumber16 値に等しいかどうかを示す値を返します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FloatingPointNumber16 other)
        {
            return Bits.Equals(other.Bits);
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
            if (obj is FloatingPointNumber16)
            {
                return Equals((FloatingPointNumber16)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Bits.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return ToDouble().ToString();
        }
        #endregion object
    }
}