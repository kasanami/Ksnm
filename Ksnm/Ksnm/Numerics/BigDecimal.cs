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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ksnm.ExtensionMethods.System.Decimal;
using static System.Diagnostics.Debug;
using static System.Math;

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
        public const int DefaultMinExponent = DecimalMinExponent - 1;
        /// <summary>
        /// System.Decimal の指数の最小値
        /// ※System.Decimal 内では正数で保持しているが、この値は指数のため負の値とする。
        /// </summary>
        private const int DecimalMinExponent = -28;
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
        /// 無限小数の場合にこの桁数で丸める
        /// 精度とも言える
        /// </summary>
        public int MinExponent { get; private set; }
        /// <summary>
        /// Pow10の結果を保存しておき、2回目以降はこちらを使用する。
        /// </summary>
        private static readonly Dictionary<int, BigInteger> Pow10Results = new Dictionary<int, BigInteger>();
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
            MinExponent = other.MinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        /// <param name="integer">整数</param>
        public BigDecimal(BigInteger integer)
        {
            Exponent = 0;
            Mantissa = integer;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// * exponent と DefaultMinExponent どちらか小さい方を、MinExponent に設定します。
        /// </summary>
        /// <param name="mantissa">仮数部</param>
        /// <param name="exponent">指数部</param>
        public BigDecimal(BigInteger mantissa, int exponent)
        {
            Exponent = exponent;
            Mantissa = mantissa;
            MinExponent = System.Math.Min(exponent, DefaultMinExponent);
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        /// <param name="mantissa">仮数部</param>
        /// <param name="exponent">指数部</param>
        /// <param name="minExponent">指数部の最小値</param>
        public BigDecimal(BigInteger mantissa, int exponent, int minExponent)
        {
            Assert(exponent >= minExponent);
            Exponent = exponent;
            Mantissa = mantissa;
            MinExponent = minExponent;
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
        public BigDecimal(uint value)
        {
            Exponent = 0;
            Mantissa = value;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(long value)
        {
            Exponent = 0;
            Mantissa = value;
            MinExponent = DefaultMinExponent;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public BigDecimal(ulong value)
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
            _MinimizeExponent(MinExponent);
        }
        /// <summary>
        /// 指定した Exponent を設定する。
        /// * minExponent が現在の値より大きい場合は何もしません
        /// * この関数では MinExponent より小さい値を設定してもエラーにしない
        /// </summary>
        /// <param name="newExponent">設定する Exponent</param>
        private void _MinimizeExponent(int newExponent)
        {
            if (Exponent > newExponent)
            {
                var diff = Exponent - newExponent;
                Exponent = newExponent;
                Mantissa *= Pow10(diff);
            }
        }
        /// <summary>
        /// Mantissa が最小になるように変換します。
        /// * Exponent が大きくなります。
        /// * 0 の場合は、Exponent は 0 になります。
        /// </summary>
        public void MinimizeMantissa()
        {
            if (Mantissa == 0)
            {
                Exponent = 0;
                return;
            }
            // 10^eで割り切れる値の最大
            int maxExponent = MaxExponent(Mantissa);
            // プロパティを更新
            if (maxExponent > 0)
            {
                Exponent += maxExponent;
                Mantissa /= Pow10(maxExponent);
                Assert(Exponent > MinExponent);
            }
        }
        /// <summary>
        /// 指定された値を指数として 10 を累乗します。
        /// </summary>
        /// <param name="exponent">指数</param>
        /// <returns>10 を exponent で累乗した結果。</returns>
        public static BigInteger Pow10(int exponent)
        {
            if (Pow10Results.ContainsKey(exponent))
            {
                return Pow10Results[exponent];
            }
            var value = BigInteger.Pow(Base, exponent);
            Pow10Results[exponent] = value;
            return value;
        }
        /// <summary>
        /// 底を 10 とする value の割り切れる最大の指数
        /// * 100 の場合は 2 を返す。
        /// * 120 の場合は 1 を返す。
        /// * 123 の場合は 0 を返す。
        /// * 0 の場合は 0 を返す。
        /// </summary>
        /// <param name="value">調査する値</param>
        /// <returns>底を 10 とする指数</returns>
        public static int MaxExponent(BigInteger value)
        {
            value = BigInteger.Abs(value);
            // 10^eで割り切れる値の最大
            int maxExponent = 0;
            for (int e = 1; e < int.MaxValue; e++)
            {
                var divisor = Pow10(e);
                // divisor のほうが大きいなら終了
                if (value < divisor)
                {
                    break;
                }
                // 10^eで割り切れるか
                if (value % divisor == 0)
                {
                    // 更新して次へ
                    maxExponent = e;
                }
                else
                {
                    // 終了
                    break;
                }
            }
            return maxExponent;
        }
        #endregion 独自メソッド

        #region 数学関数
        /// <summary>
        /// 最下位の桁を最も近い10の累乗に丸めます。
        /// 最下位の桁は削除なります。
        /// * 中間にある場合は偶数が返されます。
        /// </summary>
        public void RoundBottom()
        {
            RoundBottom(1);
        }
        public void RoundBottom(int ex)
        {
            if (ex > 0)
            {
                Mantissa = RoundBottom(Mantissa, ex);
                Exponent += ex;
            }
        }
        public static BigInteger RoundBottom(BigInteger mantissa, int ex)
        {
            if (ex <= 0)
            {
                return mantissa;
            }
            var divisor = Pow10(ex);
            var half = divisor / 2;
            var remainder = mantissa % divisor;
            // 中間を超えている時
            if (remainder > half)
            {
                mantissa += divisor;
            }
            else if (remainder < -half)
            {
                mantissa -= divisor;
            }
            // Mantissa を桁減らし、Exponent を増やす
            mantissa /= divisor;
            // 中間の時
            if (remainder == half || remainder == -half)
            {
#if false
                // 偶数に変更する。
                remainder = (int)(Mantissa % Base);// 新1桁目
                // 奇数なら変更する
                if (remainder.IsEven == false)
                {
                    if (remainder > 0)
                    {
                        Mantissa++;
                    }
                    else
                    {
                        Mantissa--;
                    }
                }
#else
                // 普通の四捨五入
                if (remainder > 0)
                {
                    mantissa++;
                }
                else
                {
                    mantissa--;
                }
#endif
            }
            return mantissa;
        }
        #endregion 数学関数

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
            // 指数が小さい方に合わせる
            if (valueL.Exponent > valueR.Exponent)
            {
                var diff = valueL.Exponent - valueR.Exponent;
                valueL.Mantissa *= Pow10(diff);
                valueL.Exponent -= diff;
            }
            else if (valueR.Exponent > valueL.Exponent)
            {
                var diff = valueR.Exponent - valueL.Exponent;
                valueR.Mantissa *= Pow10(diff);
                valueR.Exponent -= diff;
            }
            Assert(valueR.Exponent == valueL.Exponent);
            return new BigDecimal(valueL.Mantissa + valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static BigDecimal operator -(BigDecimal valueL, BigDecimal valueR)
        {
            // 指数が小さい方に合わせる
            if (valueL.Exponent > valueR.Exponent)
            {
                var diff = valueL.Exponent - valueR.Exponent;
                valueL.Mantissa *= Pow10(diff);
                valueL.Exponent -= diff;
            }
            else if (valueR.Exponent > valueL.Exponent)
            {
                var diff = valueR.Exponent - valueL.Exponent;
                valueR.Mantissa *= Pow10(diff);
                valueR.Exponent -= diff;
            }
            Assert(valueR.Exponent == valueL.Exponent);
            return new BigDecimal(valueL.Mantissa - valueR.Mantissa, valueL.Exponent);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(BigDecimal valueL, BigDecimal valueR)
        {
            return new BigDecimal(valueL.Mantissa * valueR.Mantissa, valueL.Exponent + valueR.Exponent);
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
            // 指数が小さい方に合わせる
            temp.MinExponent = Min(valueL.MinExponent, valueR.MinExponent);
            // 割られる数の Exponent を最小にする。
            // 丸め処理のため桁増やす
            var addExponent = Min(valueR.Exponent, 0);
            temp._MinimizeExponent(temp.MinExponent + addExponent);
            // 除算
            temp.Mantissa /= valueR.Mantissa;
            temp.Exponent -= valueR.Exponent;
            // 丸め処理(桁増やした分はここで減る)
            temp.RoundBottom();
            Assert(temp.Exponent >= temp.MinExponent);
            // 最適化
            temp.MinimizeMantissa();
            return temp;
        }
        #endregion 2項演算子

        #region 比較演算子
        /// <summary>
        /// 等しい場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator ==(BigDecimal valueL, BigDecimal valueR)
        {
            return valueL.Equals(valueR);
        }
        /// <summary>
        /// 等しくない場合は true。それ以外の場合は false。
        /// </summary>
        public static bool operator !=(BigDecimal valueL, BigDecimal valueR)
        {
            return !valueL.Equals(valueR);
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
                valueL.Mantissa *= Pow10(exponentDiff);
            }
            else if (valueR.Exponent > valueL.Exponent)
            {
                var exponentDiff = valueR.Exponent - valueL.Exponent;
                valueR.Mantissa *= Pow10(exponentDiff);
            }
        }
        #endregion 比較演算子

        #region 型変換
        #region 他の型→BigDecimal
        /// <summary>
        /// byte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(byte value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// sbyte から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(sbyte value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// short から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(short value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// ushort から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(ushort value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// int から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(int value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// uint から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(uint value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// long から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(long value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// ulong から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(ulong value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// float から BigDecimal への明示的な変換を定義します。
        /// </summary>
        public static explicit operator BigDecimal(float value)
        {
            return new BigDecimal((decimal)value);
        }
        /// <summary>
        /// double から BigDecimal への明示的な変換を定義します。
        /// </summary>
        public static explicit operator BigDecimal(double value)
        {
            return new BigDecimal((decimal)value);
        }
        /// <summary>
        /// decimal から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(decimal value)
        {
            return new BigDecimal(value);
        }
        /// <summary>
        /// BigInteger から BigDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator BigDecimal(BigInteger value)
        {
            return new BigDecimal(value);
        }
        #endregion 他の型→BigDecimal
        #region BigDecimal→他の型
        /// <summary>
        /// BigDecimal から byte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator byte(BigDecimal value)
        {
            return (byte)value.ToBigInteger();
        }
        /// <summary>
        /// BigDecimal から sbyte への明示的な変換を定義します。
        /// </summary>
        public static explicit operator sbyte(BigDecimal value)
        {
            return (sbyte)value.ToBigInteger();
        }
        /// <summary>
        /// BigDecimal から short への明示的な変換を定義します。
        /// </summary>
        public static explicit operator short(BigDecimal value)
        {
            return (short)value.ToBigInteger();
        }
        /// <summary>
        /// BigDecimal から ushort への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ushort(BigDecimal value)
        {
            return (ushort)value.ToBigInteger();
        }
        /// <summary>
        /// BigDecimal から int への明示的な変換を定義します。
        /// </summary>
        public static explicit operator int(BigDecimal value)
        {
            return value.ToInt32();
        }
        /// <summary>
        /// BigDecimal から uint への明示的な変換を定義します。
        /// </summary>
        public static explicit operator uint(BigDecimal value)
        {
            return value.ToUInt32();
        }
        /// <summary>
        /// BigDecimal から long への明示的な変換を定義します。
        /// </summary>
        public static explicit operator long(BigDecimal value)
        {
            return value.ToInt64();
        }
        /// <summary>
        /// BigDecimal から ulong への明示的な変換を定義します。
        /// </summary>
        public static explicit operator ulong(BigDecimal value)
        {
            return value.ToUInt64();
        }
        /// <summary>
        /// BigDecimal から float への明示的な変換を定義します。
        /// </summary>
        public static explicit operator float(BigDecimal value)
        {
            return (float)value.ToDecimal();
        }
        /// <summary>
        /// BigDecimal から double への明示的な変換を定義します。
        /// </summary>
        public static explicit operator double(BigDecimal value)
        {
            return (double)value.ToDecimal();
        }
        /// <summary>
        /// BigDecimal から decimal への明示的な変換を定義します。
        /// </summary>
        public static explicit operator decimal(BigDecimal value)
        {
            return value.ToDecimal();
        }
        /// <summary>
        /// BigDecimal から BigInteger への明示的な変換を定義します。
        /// </summary>
        public static explicit operator BigInteger(BigDecimal value)
        {
            return value.ToBigInteger();
        }
        #endregion BigDecimal→他の型
        #endregion 型変換

        #region To*
        /// <summary>
        /// int へ変換します。
        /// * 小数点以下は切り捨て
        /// </summary>
        public int ToInt32()
        {
            return (int)ToBigInteger();
        }
        /// <summary>
        /// uint へ変換します。
        /// * 小数点以下は切り捨て
        /// </summary>
        public uint ToUInt32()
        {
            return (uint)ToBigInteger();
        }
        /// <summary>
        /// long へ変換します。
        /// * 小数点以下は切り捨て
        /// </summary>
        public long ToInt64()
        {
            return (long)ToBigInteger();
        }
        /// <summary>
        /// ulong へ変換します。
        /// * 小数点以下は切り捨て
        /// </summary>
        public ulong ToUInt64()
        {
            return (ulong)ToBigInteger();
        }
        /// <summary>
        /// decimal へ変換します。
        /// </summary>
        public decimal ToDecimal()
        {
            // mantissa は正の数にする
            var mantissa = BigInteger.Abs(Mantissa);
            byte scale = 0;
            // decimal は正の Exponent に対応していないので、mantissa を変換
            if (Exponent > 0)
            {
                mantissa *= Pow10(Exponent);
            }
            else if (Exponent < 0)
            {
                var exponent = Exponent;
                // Decimal より精度が高い場合、丸める
                if (exponent < DecimalMinExponent)
                {
                    var diff = DecimalMinExponent - exponent;
                    mantissa = RoundBottom(mantissa, diff);
                    exponent = DecimalMinExponent;
                }
                scale = (byte)(-exponent);
            }
            // [0]=最上位
            var bytes = mantissa.ToByteArray().ToList();
            if (bytes.Count > (4 * 3))
            {
                //return decimal.MaxValue;
                throw new OverflowException($"{nameof(Mantissa)}={Mantissa}");
                //throw new InvalidCastException($"{nameof(Mantissa)}({Mantissa})が decimal の最大値より大きい");
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
            return new decimal(lo, mid, hi, isNegative, scale);
        }
        /// <summary>
        /// BigInteger へ変換します。
        /// * 小数点以下は切り捨て
        /// </summary>
        public BigInteger ToBigInteger()
        {
            var mantissa = Mantissa;
            if (Exponent > 0)
            {
                mantissa *= Pow10(Exponent);
            }
            else if (Exponent < 0)
            {
                mantissa /= Pow10(-Exponent);
            }
            return mantissa;
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
#if true
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Mantissa.ToString());
            if (Exponent > 0)
            {
                stringBuilder.Append(new string('0', Exponent));
            }
            else if (Exponent < 0)
            {
                var offset = (Mantissa < 0) ? 1 : 0;
                var e = -Exponent;
                var length = stringBuilder.Length - offset;
                if (e < length)
                {
                    stringBuilder.Insert(stringBuilder.Length - e, '.');
                }
                else
                {
                    var zeroCount = e - length + 1;
                    stringBuilder.Insert(offset, new string('0', zeroCount));
                    stringBuilder.Insert(offset + 1, '.');
                }
            }
            return stringBuilder.ToString();
#else
            return $"({Mantissa}*10^{Exponent})";
#endif
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した値と等しいかどうかを示す値を返します。
        /// </summary>
        public bool Equals(BigDecimal other)
        {
            // Exponent を一致させてから比較
            if (Exponent > other.Exponent)
            {
                var diff = Exponent - other.Exponent;
                return Mantissa * Pow10(diff) == other.Mantissa;
            }
            else if (Exponent < other.Exponent)
            {
                var diff = other.Exponent - Exponent;
                return Mantissa == other.Mantissa * Pow10(diff);
            }
            return Mantissa == other.Mantissa;
        }
        #endregion IEquatable
    }
}