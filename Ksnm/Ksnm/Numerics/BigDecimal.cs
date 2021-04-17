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
using Ksnm.ExtensionMethods.System.Numerics.BigInteger;
using static System.Diagnostics.Debug;
using static System.Math;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 任意の大きさを持つ10 進数の浮動小数点数を表します。
    /// 
    /// BigDecimal=Mantissa*10^Exponent
    /// </summary>
    public struct BigDecimal : IEquatable<BigDecimal>, IComparable, IComparable<BigDecimal>, IMath<BigDecimal>
    {
        #region 定数
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        public static readonly BigDecimal Zero = 0;
        /// <summary>
        /// 数値 0.5 を表します。
        /// </summary>
        public static readonly BigDecimal ZeroPointFive = 0.5m;
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        public static readonly BigDecimal One = 1;
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        public static readonly BigDecimal MinusOne = -1;
        /// <summary>
        /// 十進数の底（てい）
        /// </summary>
        public const int Base = 10;
        /// <summary>
        /// MinExponentの初期値
        /// </summary>
        public const int DefaultMinExponent = DecimalMinExponent;
        /// <summary>
        /// System.Decimal の指数の最小値
        /// ※System.Decimal 内では正数で保持しているが、この値は指数のため負の値とする。
        /// </summary>
        private const int DecimalMinExponent = -28;
        /// <summary>
        /// 任意の計算結果が Decimal で同様の計算をした結果と一致しないので、より高精度にするための追加指数。
        /// </summary>
        private const int AddExponent = -3;
        /// <summary>
        /// デフォルトの丸め処理方法
        /// </summary>
        public const MidpointRounding DefaultMidpointRounding = MidpointRounding.ToEven;
        /// <summary>
        /// 円周率を小数点以下100桁まで表します。
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148
        /// </summary>
        public static readonly BigDecimal PI_100 = BigDecimal.Parse("3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170680");
        /// <summary>
        /// 自然対数の底を小数点以下100桁まで表します。
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:2.718281828459045235360287471352662497757247093699959574966967627724076630353547594571382178525166427427466
        /// </summary>
        public static readonly BigDecimal E_100 = BigDecimal.Parse("2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274");
        /// <summary>
        /// 2 の平方根を小数点以下100桁まで表します。
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:1.414213562373095048801688724209698078569671875376948073176679737990732478462107038850387534327641572735014
        /// </summary>
        public static readonly BigDecimal SquareRootOfTwo_100 = BigDecimal.Parse("1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727");
        /// <summary>
        /// 3 の平方根を小数点以下100桁まで表します。
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:1.732050807568877293527446341505872366942805253810380628055806979451933016908800037081146186757248575675626
        /// </summary>
        public static readonly BigDecimal SquareRootOfThree_100 = BigDecimal.Parse("1.7320508075688772935274463415058723669428052538103806280558069794519330169088000370811461867572485757");
        /// <summary>
        /// 5 の平方根を小数点以下100桁まで表します。
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:2.236067977499789696409173668731276235440618359611525724270897245410520925637804899414414408378782274969508
        /// </summary>
        public static readonly BigDecimal SquareRootOfFive_100 = BigDecimal.Parse("2.2360679774997896964091736687312762354406183596115257242708972454105209256378048994144144083787822750");
        /// <summary>
        /// 黄金数
        /// * 最下位の桁は丸め済みの値
        /// * 105桁の場合:1.618033988749894848204586834365638117720309179805762862135448622705260462818902449707207204189391137484754
        /// </summary>
        public static readonly BigDecimal GoldenNumber_100 = BigDecimal.Parse("1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911375");
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
        /// <summary>
        /// 2 つの値を比較します。
        /// </summary>
        /// <param name="d1">比較する最初の値です。</param>
        /// <param name="d2">比較する 2 番目の値です。</param>
        /// <returns>
        /// -1：d1 は d2 より小さい。
        /// 0 ：d1 と d2 が等しい。 
        /// +1：d1 が d2 より大きい。</returns>
        public static int Compare(BigDecimal d1, BigDecimal d2)
        {
            _ConvertSameExponent(ref d1, ref d2);
            if (d1.Mantissa > d2.Mantissa)
            {
                return 1;
            }
            else if (d1.Mantissa < d2.Mantissa)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 指定された 2 つのインスタンスが同じ値を表しているかどうかを示す値を返します。
        /// </summary>
        /// <param name="d1">比較する最初の値です。</param>
        /// <param name="d2">比較する 2 番目の値です。</param>
        /// <returns>d1 と d2 が等しい場合は true。それ以外の場合は false。</returns>
        public static bool Equals(BigDecimal d1, BigDecimal d2)
        {
            // Exponent を一致させてから比較
            if (d1.Exponent > d2.Exponent)
            {
                var diff = d1.Exponent - d2.Exponent;
                d1.Mantissa *= Pow10(diff);
            }
            else if (d1.Exponent < d2.Exponent)
            {
                var diff = d2.Exponent - d1.Exponent;
                d2.Mantissa *= Pow10(diff);
            }
            return d1.Mantissa == d2.Mantissa;
        }
        /// <summary>
        /// 数値の文字列形式を、それと等価の BigDecimal に変換します。
        /// </summary>
        /// <param name="value">変換する数値の文字列形式。</param>
        /// <returns>指定されている数値と等価の値。</returns>
        public static BigDecimal Parse(string value)
        {
            var temp = new BigDecimal();
            var pointIindex = value.IndexOf('.');
            if (pointIindex < 0)
            {
                temp.Mantissa = BigInteger.Parse(value);
            }
            else
            {
                value = value.Remove(pointIindex, 1);
                temp.Mantissa = BigInteger.Parse(value);
                temp.Exponent = -(value.Length - pointIindex);
            }
            return temp;
        }
        #endregion 独自メソッド

        #region Is*
        /// <summary>
        /// 偶数なら true を返す。
        /// </summary>
        public bool IsEven()
        {
            return (this % 2) == 0;
        }
        /// <summary>
        /// 奇数なら true を返す。
        /// </summary>
        public bool IsOdd()
        {
            return (this % 2) != 0;
        }
        #endregion Is*

        #region Get*
        /// <summary>
        /// 小数部を取得
        /// </summary>
        public BigDecimal GetFractional()
        {
            return this % 1;
        }
        #endregion Get*

        #region 数学関数
        /// <summary>
        /// 除算し、その結果を返します。剰余は出力パラメーターとして返されます。
        /// </summary>
        /// <param name="dividend">被除数。</param>
        /// <param name="divisor">除数。</param>
        /// <param name="remainder">このメソッドから制御が戻るときに、除算の剰余を表す System.Numerics.BigInteger 値が格納されます。 このパラメーターは初期化せずに渡されます。</param>
        /// <returns>除算の商。</returns>
        /// <exception cref="System.DivideByZeroException">divisor が 0 (ゼロ) です。</exception>
        public static BigDecimal DivRem(BigDecimal dividend, BigDecimal divisor, out BigDecimal remainder)
        {
            // 指数が小さい方に合わせる
            if (dividend.Exponent > divisor.Exponent)
            {
                var diff = dividend.Exponent - divisor.Exponent;
                dividend.Mantissa *= Pow10(diff);
                dividend.Exponent -= diff;
            }
            else if (divisor.Exponent > dividend.Exponent)
            {
                var diff = divisor.Exponent - dividend.Exponent;
                divisor.Mantissa *= Pow10(diff);
                divisor.Exponent -= diff;
            }
            Assert(dividend.Exponent == divisor.Exponent);
            BigInteger remainderInt;
            var quotient = BigInteger.DivRem(dividend.Mantissa, divisor.Mantissa, out remainderInt);
            remainder = new BigDecimal(remainderInt, dividend.Exponent);
            return new BigDecimal(quotient);
        }
        /// <summary>
        /// 指定された値を指数として System.Numerics.BigInteger 値を累乗します。
        /// </summary>
        /// <param name="value">累乗する数値</param>
        /// <param name="exponent">指数</param>
        /// <returns>value を exponent で累乗した結果。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">exponent が負の値です。</exception>
        public static BigDecimal Pow(BigDecimal value, int exponent)
        {
            if (exponent < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(exponent)} が負の値です。");
            }
            if (exponent == 0)
            {
                return 1;
            }
            if (value == 0)
            {
                return 0;
            }
            var temp = value;
            temp.Mantissa = BigInteger.Pow(temp.Mantissa, exponent);
            return temp;
        }
        /// <summary>
        /// 指定された値を指数として 10 を累乗します。
        /// </summary>
        /// <param name="exponent">指数</param>
        /// <returns>10 を exponent で累乗した結果。</returns>
        public static BigInteger Pow10(int exponent)
        {
            return Math.BigIntegerPow10(exponent);
        }
        /// <summary>
        /// 10 進値を最も近い整数に丸めます。
        /// </summary>
        public static BigDecimal Round(BigDecimal value, MidpointRounding mode)
        {
            var fractional = value.GetFractional();
            if (mode == MidpointRounding.AwayFromZero)
            {
                if (fractional >= ZeroPointFive)
                {
                    // 切り上げ
                    return (value - fractional) + 1;
                }
                else if (fractional <= -ZeroPointFive)
                {
                    // マイナス方向へ切り上げ
                    return (value - fractional) - 1;
                }
                else if (fractional < ZeroPointFive && fractional > -ZeroPointFive)
                {
                    // 切り捨て
                    return value - fractional;
                }
            }
            else if (mode == MidpointRounding.ToEven)
            {
                if (fractional > ZeroPointFive)
                {
                    // 切り上げ
                    return (value - fractional) + 1;
                }
                else if (fractional < -ZeroPointFive)
                {
                    // マイナス方向へ切り上げ
                    return (value - fractional) - 1;
                }
                else if (fractional < ZeroPointFive && fractional > -ZeroPointFive)
                {
                    // 切り捨て
                    return value - fractional;
                }
                // 中間の場合は奇数なら加算
                if (value.ToBigInteger().IsOdd())
                {
                    if (fractional > 0)
                    {
                        // 切り上げ
                        return (value - fractional) + 1;
                    }
                    else
                    {
                        // マイナス方向へ切り上げ
                        return (value - fractional) - 1;
                    }
                }
                // 偶数なら切り捨て
                return value - fractional;
            }
            throw new ArgumentException($"{nameof(mode)}={mode} 値が不正");
        }
        /// <summary>
        /// 10 進値を指定した精度に丸めます。 パラメーターは、値が他の 2 つの数値の中間にある場合にその値を丸める方法を指定します。
        /// </summary>
        /// <param name="value">丸め対象の 10 進数。</param>
        /// <param name="decimals">戻り値の小数点以下の有効桁数 (精度)。</param>
        /// <param name="mode">d が他の 2 つの数値の中間にある場合に丸める方法を指定する値。</param>
        public static BigDecimal Round(BigDecimal value, int decimals, MidpointRounding mode)
        {
            if (decimals < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(decimals)}={decimals} 範囲を超えています。");
            }
            var scale = Pow10(decimals);
            value *= scale;
            return Round(value, mode) / scale;
        }
        /// <summary>
        /// 今の値が MinExponent 以下の値の場合、MinExponent に収まるように丸めます。
        /// 丸められた桁は 0 になります。
        /// * 丸めの種類は DefaultMidpointRounding
        /// </summary>
        public void RoundByMinExponent()
        {
            if (Exponent < MinExponent)
            {
                var digits = -(Exponent - MinExponent);
                RoundMantissa(digits);
            }
        }
        /// <summary>
        /// 仮数部を指定した桁の最も近い10の累乗に丸めます。
        /// * 丸めの種類は DefaultMidpointRounding
        /// * Round と違い小数部分だけでなく仮数部自体に対する丸め処理
        /// </summary>
        /// <param name="digits">丸める桁数</param>
        public void RoundMantissa(int digits)
        {
            if (digits > 0)
            {
                Mantissa = Mantissa.Round(digits, DefaultMidpointRounding);
                Exponent += digits;
            }
        }
        /// <summary>
        /// 整数の桁を返します。小数の桁は破棄されます。
        /// </summary>
        /// <param name="value">切り捨てる 10 進数。</param>
        /// <returns>0 方向の近似整数に丸めた結果。</returns>
        public static BigDecimal Truncate(BigDecimal value)
        {
            if (value.Exponent < 0)
            {
                value.Mantissa /= Pow10(-value.Exponent);
                value.Exponent = 0;
            }
            return value;
        }
        /// <summary>
        /// 正の無限大方向の近似整数に丸めます。
        /// 小数部がない場合は、未変更のまま返されます。
        /// </summary>
        public static BigDecimal Ceiling(BigDecimal value)
        {
            if (value.Exponent < 0)
            {
                var scale = Pow10(-value.Exponent);
                var remainder = value.Mantissa % scale;
                value.Mantissa /= scale;
                value.Exponent = 0;
                if (remainder > 0)
                {
                    value.Mantissa++;
                }
            }
            return value;
        }
        /// <summary>
        /// 負の無限大方向の近似整数に丸めます。
        /// 小数部がない場合は、未変更のまま返されます。
        /// </summary>
        public static BigDecimal Floor(BigDecimal value)
        {
            if (value.Exponent < 0)
            {
                var scale = Pow10(-value.Exponent);
                var remainder = value.Mantissa % scale;
                value.Mantissa /= scale;
                value.Exponent = 0;
                if (remainder < 0)
                {
                    value.Mantissa--;
                }
            }
            return value;
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// * 精度は現在の value.MinExponent の値となる
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static BigDecimal Sqrt(BigDecimal value)
        {
            int decimals = 0;
            if (value.MinExponent < 0)
            {
                decimals = -value.MinExponent;
            }
            return Sqrt(value, decimals);
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <param name="decimals">小数点以下の有効桁数 (精度)。</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static BigDecimal Sqrt(BigDecimal value, int decimals)
        {
            // 計算回数は仮
            return Sqrt(value, decimals, decimals + 10);
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <param name="decimals">小数点以下の有効桁数 (精度)。</param>
        /// <param name="count">計算回数</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static BigDecimal Sqrt(BigDecimal value, int decimals, int count)
        {
            if (value == 0)
            {
                return 0;
            }
            var temp = value;
            var prev = value;
            for (int i = 0; i < count; i++)
            {
                temp = (temp * temp + value) / (2 * temp);
                // 精度を制限
                temp = Round(temp, decimals, DefaultMidpointRounding);
                // 前回から値が変わっていないなら終了
                if (prev == temp)
                {
                    return temp;
                }
                prev = temp;
            }
            return temp;
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
            return new BigDecimal(
                valueL.Mantissa * valueR.Mantissa,
                valueL.Exponent + valueR.Exponent);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static BigDecimal operator *(BigDecimal valueL, int valueR)
        {
            return new BigDecimal(
                valueL.Mantissa * valueR,
                valueL.Exponent);
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static BigDecimal operator /(BigDecimal valueL, BigDecimal valueR)
        {
            var temp = new BigDecimal(valueL);
            // 指数が小さい方に合わせる
            temp.MinExponent = System.Math.Min(valueL.MinExponent, valueR.MinExponent);
            // 割られる数の Exponent を最小にする。
            // さらに、丸め処理のため桁増やす
            var addExponent = System.Math.Min(valueR.Exponent + AddExponent, 0);
            temp._MinimizeExponent(temp.MinExponent + addExponent);
            // 除算
            temp.Mantissa /= valueR.Mantissa;
            temp.Exponent -= valueR.Exponent;
            // 丸め処理(桁増やした分はここで減る)
            temp.RoundByMinExponent();
            Assert(temp.Exponent >= temp.MinExponent);
            // 最適化
            temp.MinimizeMantissa();
            return temp;
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static BigDecimal operator /(BigDecimal valueL, int valueR)
        {
            var temp = new BigDecimal(valueL);
            // 割られる数の Exponent を最小にする。
            // さらに、丸め処理のため桁増やす
            temp._MinimizeExponent(temp.MinExponent + AddExponent);
            // 除算
            temp.Mantissa /= valueR;
            // 丸め処理(桁増やした分はここで減る)
            temp.RoundByMinExponent();
            Assert(temp.Exponent >= temp.MinExponent);
            // 最適化
            temp.MinimizeMantissa();
            return temp;
        }
        /// <summary>
        /// 剰余
        /// </summary>
        /// <returns></returns>
        public static BigDecimal operator %(BigDecimal valueL, BigDecimal valueR)
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
            return new BigDecimal(valueL.Mantissa % valueR.Mantissa, valueL.Exponent);
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
                    mantissa = mantissa.Round(diff, MidpointRounding.ToEven);
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
            return Equals(this, other);
        }
        #endregion IEquatable

        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        public int CompareTo(object obj)
        {
            return CompareTo((BigDecimal)obj);
        }
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="other">比較対象</param>
        public int CompareTo(BigDecimal other)
        {
            return Compare(this, other);
        }
        #endregion IComparable

        #region IMath

        BigDecimal IMath<BigDecimal>.Zero => Zero;

        BigDecimal IMath<BigDecimal>.One => One;

        BigDecimal IMath<BigDecimal>.MinusOne => MinusOne;

        public int Sign
        {
            get
            {
                if (this > 0) { return +1; }
                if (this < 0) { return -1; }
                return 0;
            }
        }

        public bool IsZero => this == Zero;

        public bool IsOne => this == One;

        public BigDecimal Abs()
        {
            if (this < 0)
            {
                return Negate();
            }
            return this;
        }

        public BigDecimal Add(BigDecimal addend)
        {
            return this + addend;
        }
        /// <summary>
        /// 指定した数以上の数のうち、最小の整数値を返します。
        /// </summary>
        /// <returns>以上の最小の整数値。 </returns>
        public BigDecimal Ceiling()
        {
            return Ceiling(this);
        }

        public int Compare(BigDecimal other)
        {
            return Compare(this, other);
        }

        public BigDecimal Divide(BigDecimal divisor)
        {
            return this / divisor;
        }

        public BigDecimal DivRem(BigDecimal divisor, out BigDecimal remainder)
        {
            return DivRem(this, divisor, out remainder);
        }
        /// <summary>
        /// 指定した数以下の数のうち、最大の整数値を返します。
        /// </summary>
        /// <returns>以下の最大の整数値。</returns>
        public BigDecimal Floor()
        {
            return Floor(this);
        }
        /// <summary>
        /// 指定した値を T 型の値に変換します。
        /// </summary>
        /// <returns>変換した値。</returns>
        public BigDecimal From(int value)
        {
            return value;
        }
        /// <summary>
        /// 指定した値を T 型の値に変換します。
        /// </summary>
        /// <returns>変換した値。</returns>
        public BigDecimal From(double value)
        {
            throw new NotImplementedException();
        }

        public double Log(double baseValue)
        {
            throw new NotImplementedException();
        }

        public double Log()
        {
            throw new NotImplementedException();
        }

        public double Log10()
        {
            throw new NotImplementedException();
        }

        public BigDecimal Max(BigDecimal other)
        {
            if (this > other)
            {
                return this;
            }
            return other;
        }

        public BigDecimal Min(BigDecimal other)
        {
            if (this < other)
            {
                return this;
            }
            return other;
        }

        public BigDecimal Multiply(BigDecimal multiplier)
        {
            return this * multiplier;
        }

        public BigDecimal Negate()
        {
            return -this;
        }

        public BigDecimal Pow(int exponent)
        {
            return Pow(this, exponent);
        }

        public BigDecimal Remainder(BigDecimal divisor)
        {
            return this % divisor;
        }

        public BigDecimal Subtract(BigDecimal substrahend)
        {
            return this - substrahend;
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// * 精度は現在の MinExponent の値となる
        /// </summary>
        /// <returns></returns>
        public BigDecimal Sqrt()
        {
            return Sqrt(this);
        }
        /// <summary>
        /// 整数部を計算します。
        /// </summary>
        /// <returns>整数部。つまり、小数部の桁を破棄した後に残る数値。</returns>
        public BigDecimal Truncate()
        {
            return Truncate(this);
        }
        #endregion IMath
    }
}