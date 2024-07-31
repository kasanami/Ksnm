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
using Ksnm.ExtensionMethods.System.Double;
using BitsType = System.Int32;// 固定小数点数 全体のビットを表す型
using IntegerType = System.Int16;// 固定小数点数 整数部分のビットを表す型
using FractionalType = System.UInt16;// 固定小数点数 小数部分のビットを表す型

namespace Ksnm.Numerics
{
    // 精度の異なる固定小数点数型でコードを再利用するためのエイリアスを定義
    using FixedPointNumber = FixedPointNumber32Q16;
    /// <summary>
    /// 固定小数点数(全体のビット数32、小数部分のビット数16)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct FixedPointNumber32Q16 : IComparable, IComparable<FixedPointNumber>, IEquatable<FixedPointNumber>
    {
        #region 定数
        /// <summary>
        /// 小数部分のビット数
        /// </summary>
        public const int QBits = 16;
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public readonly static FixedPointNumber Zero = new FixedPointNumber() { integer = 0 };
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public readonly static FixedPointNumber One = new FixedPointNumber() { integer = 1 };
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public readonly static FixedPointNumber MinusOne = new FixedPointNumber() { integer = -1 };
        /// <summary>
        /// 最小有効値を表します。
        /// </summary>
        public readonly static FixedPointNumber MinValue = new FixedPointNumber() { bits = BitsType.MinValue };
        /// <summary>
        /// 最大有効値を表します。
        /// </summary>
        public readonly static FixedPointNumber MaxValue = new FixedPointNumber() { bits = BitsType.MaxValue };
        /// <summary>
        /// ゼロより大きい最小の値を表します。
        /// </summary>
        public readonly static FixedPointNumber Epsilon = new FixedPointNumber() { bits = 1 };
        /// <summary>
        /// 1を表すビット
        /// </summary>
        const BitsType OneBits = 1 << QBits;
        /// <summary>
        /// 0.5を表すビット
        /// </summary>
        const BitsType HalfBits = 1 << QBits - 1;
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
        [FieldOffset(2)]
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
        /// <param name="integer">整数部</param>
        public FixedPointNumber32Q16(IntegerType integer)
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
        public FixedPointNumber32Q16(IntegerType integer, FractionalType fractional)
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

        #region ユーティリティ
        /// <summary>
        /// 浮動小数点数値を設定する。
        /// キャストしたほうが高速なので非公開
        /// </summary>
        /// <param name="value">浮動小数点数値</param>
        /// <returns>非数・無限大の場合false それ以外はtrue</returns>
        private bool SetDouble(double value)
        {
            var exponentBits = value.GetExponentBits();
            var mantissaBits = value.GetMantissaBits();
            if (exponentBits == 0)
            {
                // ゼロか非正規化数
                bits = 0;
            }
            else if (exponentBits == 0x7ff)
            {
                if (mantissaBits != 0)
                {
                    // 非数
                    bits = 0;
                }
                else if (value.GetSignBits() == 0)
                {
                    // 正の無限大
                    bits = BitsType.MaxValue;
                }
                else
                {
                    // 負の無限大
                    bits = BitsType.MinValue;
                }
                return false;
            }
            else
            {
                mantissaBits |= 0x10_0000_0000_0000;
                var shift = -52 + (exponentBits - 1023);
                shift += QBits;

                if (shift < 0)
                {
                    shift = -shift;
                    bits = (BitsType)(mantissaBits >> shift);
                }
                else if (shift > 0)
                {
                    bits = (BitsType)(mantissaBits << shift);
                }
                else
                {
                    bits = (BitsType)mantissaBits;
                }
                // 負数に変換
                if (value.GetSignBits() == 1)
                {
                    bits = ~bits + 1;
                }
            }
            return true;
        }

        #endregion ユーティリティ

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
            long temp = valueL.bits;
            temp *= valueR.bits;
            temp >>= QBits;
            return new FixedPointNumber() { bits = (BitsType)temp };
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static FixedPointNumber operator /(FixedPointNumber valueL, FixedPointNumber valueR)
        {
            long temp = valueL.bits;
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
        /// <summary>
        /// byte から 固定小数点数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator FixedPointNumber(byte value)
        {
            return new FixedPointNumber() { integer = value };
        }
        /// <summary>
        /// sbyte から 固定小数点数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator FixedPointNumber(sbyte value)
        {
            return new FixedPointNumber() { integer = value };
        }
        /// <summary>
        /// short から 固定小数点数型 への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator FixedPointNumber(short value)
        {
            return new FixedPointNumber() { integer = value };
        }
        /// <summary>
        /// ushort から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(ushort value)
        {
            return new FixedPointNumber() { integer = (IntegerType)value };
        }
        /// <summary>
        /// int から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(int value)
        {
            return new FixedPointNumber() { integer = (IntegerType)value };
        }
        /// <summary>
        /// uint から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(uint value)
        {
            return new FixedPointNumber() { integer = (IntegerType)value };
        }
        /// <summary>
        /// long から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(long value)
        {
            return new FixedPointNumber() { integer = (IntegerType)value };
        }
        /// <summary>
        /// ulong から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(ulong value)
        {
            return new FixedPointNumber() { integer = (IntegerType)value };
        }
        /// <summary>
        /// float から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(float value)
        {
            return (FixedPointNumber)(double)value;
        }
        /// <summary>
        /// double から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(double value)
        {
            value *= OneBits;
            return new FixedPointNumber() { bits = (BitsType)value };
        }
        /// <summary>
        /// decimal から 固定小数点数型 への明示的な変換を定義します。
        /// </summary>
        public static explicit operator FixedPointNumber(decimal value)
        {
            value *= OneBits;
            return new FixedPointNumber() { bits = (BitsType)value };
        }
        #endregion 他の型→固定小数点数型
        #region 固定小数点数型→他の型
        /// <summary>
        /// 固定小数点数型 から byte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator byte(FixedPointNumber value)
        {
            return (byte)Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から sbyte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator sbyte(FixedPointNumber value)
        {
            return (sbyte)Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から short への明示的な変換を定義します。
        /// </summary>
        public static explicit operator short(FixedPointNumber value)
        {
            return Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から ushort への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ushort(FixedPointNumber value)
        {
            return (ushort)Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から int への明示的な変換を定義します。
        /// </summary>
        public static explicit operator int(FixedPointNumber value)
        {
            return Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から uint への明示的な変換を定義します。
        /// </summary>
        public static explicit operator uint(FixedPointNumber value)
        {
            return (uint)Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から long への明示的な変換を定義します。
        /// </summary>
        public static explicit operator long(FixedPointNumber value)
        {
            return Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から ulong への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ulong(FixedPointNumber value)
        {
            return (ulong)Truncate(value).integer;
        }
        /// <summary>
        /// 固定小数点数型 から float への明示的な変換を定義します。
        /// </summary>
        public static explicit operator float(FixedPointNumber value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return (float)temp;
        }
        /// <summary>
        /// 固定小数点数型 から double への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator double(FixedPointNumber value)
        {
            double temp = value.bits;
            temp /= OneBits;
            return temp;
        }
        /// <summary>
        /// 固定小数点数型 から double への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator decimal(FixedPointNumber value)
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
    }
}
