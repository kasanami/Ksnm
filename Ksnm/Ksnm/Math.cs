/*
The zlib License

Copyright (c) 2014-2021 Takahiro Kasanami

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
using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Ksnm
{
    /// <summary>
    /// System.Math に無い機能を定義したクラス
    /// </summary>
    public static partial class Math
    {
        #region 定数

        /// <summary>
        /// 黄金数
        /// </summary>
        public const double GoldenNumber = 1.61803398874989484820;

        /// <summary>
        /// 白銀数
        /// </summary>
        public const double SilverNumber = 2.41421356237309504880;

        #endregion 定数

        #region IsEven

        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(uint value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(long value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(ulong value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(float value)
        {
            return (value % 2) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(double value)
        {
            return (value % 2) == 0;
        }
        /// <summary>
        /// 偶数ならtrueを返す。
        /// </summary>
        public static bool IsEven(decimal value)
        {
            return (value % 2) == 0;
        }

        #endregion IsEven

        #region IsOdd

        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(int value)
        {
            return (value & 1) != 0;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(uint value)
        {
            return (value & 1) != 0;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(long value)
        {
            return (value & 1) != 0;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(ulong value)
        {
            return (value & 1) != 0;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(float value)
        {
            return System.Math.Abs(value % 2) == 1;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(double value)
        {
            return System.Math.Abs(value % 2) == 1;
        }
        /// <summary>
        /// 奇数ならtrueを返す。
        /// </summary>
        public static bool IsOdd(decimal value)
        {
            return System.Math.Abs(value % 2) == 1;
        }

        #endregion IsOdd

        #region IsPrime

        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime(int value)
        {
            if (value < 2)
            {
                return false;
            }
            else if (value == 2)
            {
                return true;
            }

            if (value % 2 == 0)
            {
                return false;
            }

            for (int i = 3; i <= value / i; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime(uint value)
        {
            if (value < 2)
            {
                return false;
            }
            else if (value == 2)
            {
                return true;
            }

            if (value % 2 == 0)
            {
                return false;
            }

            for (uint i = 3; i <= value / i; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime(long value)
        {
            if (value < 2)
            {
                return false;
            }
            else if (value == 2)
            {
                return true;
            }

            if (value % 2 == 0)
            {
                return false;
            }

            for (long i = 3; i <= value / i; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime(ulong value)
        {
            if (value < 2)
            {
                return false;
            }
            else if (value == 2)
            {
                return true;
            }

            if (value % 2 == 0)
            {
                return false;
            }

            for (ulong i = 3; i <= value / i; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime(decimal value)
        {
            if (value < 2)
            {
                return false;
            }
            else if (value == 2)
            {
                return true;
            }

            if (value % 2 == 0)
            {
                return false;
            }

            for (decimal i = 3; i <= value / i; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion IsPrime

        #region Sign

        /// <summary>
        /// 符号関数
        /// </summary>
        public static int Sign(int x)
        {
            if (x < 0)
            {
                return -1;
            }
            else if (x > 0)
            {
                return +1;
            }
            return 0;
        }
        /// <summary>
        /// 符号関数
        /// </summary>
        public static int Sign(long x)
        {
            if (x < 0)
            {
                return -1;
            }
            else if (x > 0)
            {
                return +1;
            }
            return 0;
        }
        /// <summary>
        /// 符号関数
        /// </summary>
        public static int Sign(float x)
        {
            if (x < 0)
            {
                return -1;
            }
            else if (x > 0)
            {
                return +1;
            }
            return 0;
        }
        /// <summary>
        /// 符号関数
        /// </summary>
        public static int Sign(double x)
        {
            if (x < 0)
            {
                return -1;
            }
            else if (x > 0)
            {
                return +1;
            }
            return 0;
        }

        #endregion Sign

        #region Ramp

        /// <summary>
        /// ランプ関数
        /// </summary>
        public static int Ramp(int x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x;
        }
        /// <summary>
        /// ランプ関数
        /// </summary>
        public static float Ramp(float x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x;
        }
        /// <summary>
        /// ランプ関数
        /// </summary>
        public static double Ramp(double x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x;
        }
        /// <summary>
        /// ランプ関数
        /// </summary>
        public static decimal Ramp(decimal x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x;
        }

        #endregion Ramp

        #region HeavisideStep

        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static float HeavisideStep(float x, float c = 0.5f)
        {
            if (x < 0)
            {
                return 0;
            }
            else if (x > 0)
            {
                return 1;
            }
            return c;
        }
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static double HeavisideStep(double x, double c = 0.5)
        {
            if (x < 0)
            {
                return 0;
            }
            else if (x > 0)
            {
                return 1;
            }
            return c;
        }

        #endregion HeavisideStep

        #region Sigmoid

        /// <summary>
        /// シグモイド関数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="gain">ゲイン
        /// 1.0(標準)の場合、xに6.0を与えると約1.0になる。
        /// 5.0の場合、xに1.0を与えると約1.0になる。</param>
        /// <returns></returns>
        public static float Sigmoid(float x, float gain)
        {
            return 1.0f / (1.0f + (float)System.Math.Exp(-gain * x));
        }
        /// <summary>
        /// シグモイド関数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="gain">ゲイン
        /// 1.0(標準)の場合、xに6.0を与えると約1.0になる。
        /// 5.0の場合、xに1.0を与えると約1.0になる。</param>
        /// <returns></returns>
        public static double Sigmoid(double x, double gain)
        {
            return 1.0 / (1.0 + System.Math.Exp(-gain * x));
        }

        #endregion Sigmoid

        #region Lerp

        /// <summary>
        /// 線形補間
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static float Lerp(float from, float to, float t)
        {
            return from + ((to - from) * t);
        }
        /// <summary>
        /// 線形補間
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static double Lerp(double from, double to, double t)
        {
            return from + ((to - from) * t);
        }
        /// <summary>
        /// 線形補間
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static decimal Lerp(decimal from, decimal to, decimal t)
        {
            return from + ((to - from) * t);
        }

        /// <summary>
        /// 線形補間(整数出力版)
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static int LerpInteger(int from, int to, float t)
        {
            return (int)Lerp(from, to, (double)t);
        }

        /// <summary>
        /// 線形補間(整数出力版)
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static int LerpInteger(int from, int to, double t)
        {
            return (int)Lerp(from, to, t);
        }

        #endregion Lerp

        #region InverseLerp

        /// <summary>
        /// 補間係数
        /// </summary>
        /// <param name="from">valueがこの値とき、0を返す。</param>
        /// <param name="to">valueがこの値とき、1を返す。</param>
        /// <param name="value">任意の値</param>
        public static float InverseLerp(float from, float to, float value)
        {
            return (value - from) / (to - from);
        }
        /// <summary>
        /// 補間係数
        /// </summary>
        /// <param name="from">valueがこの値とき、0を返す。</param>
        /// <param name="to">valueがこの値とき、1を返す。</param>
        /// <param name="value">任意の値</param>
        public static double InverseLerp(double from, double to, double value)
        {
            return (value - from) / (to - from);
        }
        /// <summary>
        /// 補間係数
        /// </summary>
        /// <param name="from">valueがこの値とき、0を返す。</param>
        /// <param name="to">valueがこの値とき、1を返す。</param>
        /// <param name="value">任意の値</param>
        public static decimal InverseLerp(decimal from, decimal to, decimal value)
        {
            return (value - from) / (to - from);
        }

        #endregion InverseLerp

        #region Average

        /// <summary>
        /// 平均値
        /// </summary>
        public static double Average(params int[] values)
        {
            return values.Average();
        }
        /// <summary>
        /// 平均値
        /// </summary>
        public static double Average(params long[] values)
        {
            return values.Average();
        }
        /// <summary>
        /// 平均値
        /// </summary>
        public static float Average(params float[] values)
        {
            return values.Average();
        }
        /// <summary>
        /// 平均値
        /// </summary>
        public static double Average(params double[] values)
        {
            return values.Average();
        }
        /// <summary>
        /// 平均値
        /// </summary>
        public static decimal Average(params decimal[] values)
        {
            return values.Average();
        }

        #endregion Average

        #region Median

        /// <summary>
        /// 中央値
        /// </summary>
        public static double Median(params int[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }
            System.Array.Sort(values);
            if (IsEven(values.Length))
            {
                return Average(values[values.Length / 2], values[values.Length / 2 - 1]);
            }
            return values[values.Length / 2];
        }
        /// <summary>
        /// 中央値
        /// </summary>
        public static float Median(params float[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }
            System.Array.Sort(values);
            if (IsEven(values.Length))
            {
                return Average(values[values.Length / 2], values[values.Length / 2 - 1]);
            }
            return values[values.Length / 2];
        }
        /// <summary>
        /// 中央値
        /// </summary>
        public static double Median(params double[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }
            System.Array.Sort(values);
            if (IsEven(values.Length))
            {
                return Average(values[values.Length / 2], values[values.Length / 2 - 1]);
            }
            return values[values.Length / 2];
        }
        /// <summary>
        /// 中央値
        /// </summary>
        public static decimal Median(params decimal[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }
            System.Array.Sort(values);
            if (IsEven(values.Length))
            {
                return Average(values[values.Length / 2], values[values.Length / 2 - 1]);
            }
            return values[values.Length / 2];
        }

        #endregion Median

        #region Pow
        /// <summary>
        /// 指定の整数を指定した値で累乗した値を返します。
        /// <para>整数限定ですが、System.Math.Powより高速</para>
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値
        /// <para>baseValue==0,exponent&lt;0 のときは int.MinValue(無限大の代わり)</para>
        /// <para>baseValue&gt;+1,exponent&lt;0 のときは 0</para>
        /// <para>baseValue&lt;-1,exponent&lt;0 のときは 0</para>
        /// </returns>
        public static int Pow(int baseValue, int exponent)
        {
            // exponent がマイナスのときは、1未満になるので0にする。
            if (exponent < 0)
            {
                // baseValueが0なら無限大の代わりに int.MinValue を返す。
                if (baseValue == 0)
                {
                    return int.MinValue;
                }
                // baseValue が -1 か 1 のときは、exponent を正にして継続
                if (baseValue == -1 || baseValue == 1)
                {
                    exponent = -exponent;
                }
                else
                {
                    return 0;
                }
            }
            int value = 1;
            for (int i = 0; i < exponent; i++)
            {
                value *= baseValue;
            }
            return value;
        }
        /// <summary>
        /// 指定の符号なし整数を指定した値で累乗した値を返します。
        /// <para>符号なし整数限定ですが、System.Math.Powより高速</para>
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値</returns>
        public static uint Pow(uint baseValue, uint exponent)
        {
            uint value = 1;
            for (uint i = 0; i < exponent; i++)
            {
                value *= baseValue;
            }
            return value;
        }
        /// <summary>
        /// 指定の decimal を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値</returns>
        public static decimal Pow(decimal baseValue, int exponent)
        {
            decimal value = 1;
            if (exponent > 0)
            {
                for (int i = 0; i < exponent; i++)
                {
                    value *= baseValue;
                }
            }
            else if (exponent < 0)
            {
                exponent = -exponent;
                for (int i = 0; i < exponent; i++)
                {
                    value /= baseValue;
                }
            }
            return value;
        }
        /// <summary>
        /// 指定の decimal を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値</returns>
        public static decimal Pow(decimal baseValue, uint exponent)
        {
            decimal value = 1;
            for (uint i = 0; i < exponent; i++)
            {
                value *= baseValue;
            }
            return value;
        }
        #region BigInteger
        /// <summary>
        /// 指定された値を指数として 10 を累乗します。
        /// </summary>
        /// <param name="exponent">指数</param>
        /// <returns>baseValue を exponent で累乗した結果。</returns>
        public static BigInteger BigIntegerPow10(int exponent)
        {
            if (BigIntegerPow10Results.ContainsKey(exponent))
            {
                return BigIntegerPow10Results[exponent];
            }
            var result = BigInteger.Pow(10, exponent);
            BigIntegerPow10Results[exponent] = result;
            return result;
        }
        /// <summary>
        /// BigIntegerPow10() の結果を保存しておき、2回目以降はこちらを使用する。
        /// </summary>
        private static readonly Dictionary<int, BigInteger> BigIntegerPow10Results = new Dictionary<int, BigInteger>();
        #endregion BigInteger
        #endregion Pow

        #region Sqrt
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// ※最下位桁は丸められます。
        /// 丸めないと Sqrt(16)=4.0000000000000000000000000001 となるため
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static decimal Sqrt(decimal value)
        {
            // 0～100000で実験した結果、最低18必要
            int count = 18;
            return Sqrt(value, count);
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// ※最下位桁は丸められます。
        /// 丸めないと Sqrt(16)=4.0000000000000000000000000001 となるため
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <param name="count">計算回数</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static decimal Sqrt(decimal value, int count)
        {
            if (value == 0)
            {
                return 0;
            }
            var temp = value;
            var prev = value;
            var prev2 = value;
            var prev3 = value;
            var prev4 = value;
            var prev5 = value;
            var prev6 = value;
            for (int i = 0; i < count; i++)
            {
                temp = (temp * temp + value) / (2 * temp);
                // 前から値が変わっていないなら終了
                // (最下位桁が循環的に変わるパターンがあるので6個前まで比較する)
                if (prev == temp || prev2 == temp || prev3 == temp || prev4 == temp || prev5 == temp || prev6 == temp)
                {
                    // 丸める
                    return temp.RoundBottom();
                }
                prev6 = prev5;
                prev5 = prev4;
                prev4 = prev3;
                prev3 = prev2;
                prev2 = prev;
                prev = temp;
            }
            return temp;
        }
        #endregion Sqrt

        #region 最大公約数 GreatestCommonDivisor

        /// <summary>
        /// 最大公約数を計算する。
        /// <para>負数は正数にされる。</para>
        /// </summary>
        /// <param name="a">整数</param>
        /// <param name="b">整数</param>
        /// <returns>最大公約数</returns>
        public static int GreatestCommonDivisor(int a, int b)
        {
            if (a < b)
            {
                // 引数を入替えて自分を呼び出す
                return GreatestCommonDivisor(b, a);
            }
#if true
            while (b != 0)
            {
                var remainder = a % b;
                a = b;
                b = remainder;
            }
            return System.Math.Abs(a);
#else
            // 再帰呼び出しでは、わずかに遅い
            var remainder = a % b;
            if (remainder == 0)
            {
                return b;
            }
            return GreatestCommonDivisor(b, remainder);
#endif
        }
        /// <summary>
        /// 最大公約数を計算する。
        /// </summary>
        /// <param name="a">符号なし整数</param>
        /// <param name="b">符号なし整数</param>
        /// <returns>最大公約数</returns>
        public static uint GreatestCommonDivisor(uint a, uint b)
        {
            if (a < b)
            {
                // 引数を入替えて自分を呼び出す
                return GreatestCommonDivisor(b, a);
            }
            while (b != 0)
            {
                var remainder = a % b;
                a = b;
                b = remainder;
            }
            return a;
        }

        #endregion 最大公約数 GreatestCommonDivisor

        #region 素因数分解

        /// <summary>
        /// 素因数分解
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>一連の素数</returns>
        public static IEnumerable<int> PrimeFactorization(int value)
        {
            if (value < 1)
            {
                yield break;
            }

            var temp = value;
            var sqrt = System.Math.Sqrt(value);

            for (int i = 2; i <= sqrt;)
            {
                if (temp % i == 0)
                {
                    temp /= i;
                    yield return i;
                }
                else
                {
                    i++;
                }
            }
            if (temp > 1)
            {
                yield return temp;
            }
        }

        /// <summary>
        /// 素因数分解
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>一連の素数</returns>
        public static IEnumerable<long> PrimeFactorization(long value)
        {
            if (value < 1)
            {
                yield break;
            }

            var temp = value;
            var sqrt = System.Math.Sqrt(value);

            for (long i = 2; i <= sqrt;)
            {
                if (temp % i == 0)
                {
                    temp /= i;
                    yield return i;
                }
                else
                {
                    i++;
                }
            }
            if (temp > 1)
            {
                yield return temp;
            }
        }

        #endregion 素因数分解

        #region 数列

        /// <summary>
        /// 三角数
        /// </summary>
        /// <param name="n">番号</param>
        /// <returns>n番目の点の数</returns>
        public static int TriangularNumber(int n)
        {
            return (n * n + n) / 2;
        }
        /// <summary>
        /// 多角数
        /// </summary>
        /// <param name="vectors">頂点数</param>
        /// <param name="n">番号</param>
        /// <returns>n番目の点の数</returns>
        public static int PolygonalNumber(int vectors, int n)
        {
            var n2 = n * n;
            if (IsEven(vectors))
            {
                return (vectors / 2 - 1) * n2 - (vectors / 2 - 2) * n;
            }
            else
            {
                return ((vectors - 2) * n2 - (vectors - 4) * n) / 2;
            }
        }

        /// <summary>
        /// フィボナッチ数
        /// </summary>
        /// <param name="n">番号</param>
        /// <returns>n番目のフィボナッチ数</returns>
        public static int FibonacciNumber(int n)
        {
            int current = 0;
            int previous = 1;
            for (int i = 0; i < n; i++)
            {
                var next = current + previous;
                previous = current;
                current = next;
            }
            return current;
#if false
            var phi = GoldenNumber;
            var phiN = System.Math.Pow(phi, n);
            var _phi_N = System.Math.Pow(-phi, -n);
            var root5 = System.Math.Sqrt(5);
            return (int)((phiN - _phi_N) / root5);
#endif
        }

        /// <summary>
        /// フィボナッチ数列を計算
        /// </summary>
        /// <param name="count">計算する個数</param>
        /// <returns>フィボナッチ数列の列挙子</returns>
        public static IEnumerable<int> FibonacciNumbers(int count)
        {
            int current = 0;
            int previous = 1;
            for (int i = 0; i < count; i++)
            {
                yield return current;
                var next = current + previous;
                previous = current;
                current = next;
            }
        }

        /// <summary>
        /// モーザー数列
        /// 円周上に n 個の頂点を置き、その全ての2頂点間を線分で結で出来た領域の数。
        /// </summary>
        /// <param name="n">円周上の頂点の数</param>
        /// <returns>領域の数</returns>
        public static int MosersCircleRegions(int n)
        {
            var n2 = n * n;
            var n3 = n2 * n;
            var n4 = n3 * n;
            return (n4 - 6 * n3 + 23 * n2 - 18 * n + 24) / 24;
        }

        #endregion 数列

        #region 連分数
        /// <summary>
        /// 連分数を計算する。
        /// numerators と denominators のどちらか短い方の長さに合わせて要素を参照します。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="args">args[i].Item1=分子。args[i].Item2=分母</param>
        /// <returns>計算結果</returns>
        public static double ContinuedFraction(double a, IReadOnlyList<Tuple<double, double>> args)
        {
            double temp = 0;
            var reversed = args.Reverse();
            foreach (var item in reversed)
            {
                var numerator = item.Item1;
                var denominator = item.Item2;
                temp = numerator / (denominator + temp);
            }
            return a + temp;
        }
        /// <summary>
        /// 連分数を計算する。
        /// numerators と denominators のどちらか短い方の長さに合わせて要素を参照します。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="args">args[i].Item1=分子。args[i].Item2=分母</param>
        /// <returns>計算結果</returns>
        public static T ContinuedFraction<T>(T a, IReadOnlyList<Tuple<T, T>> args) where T : IMath<T>
        {
            T temp = a.Zero;
            var reversed = args.Reverse();
            foreach (var item in reversed)
            {
                var numerator = item.Item1;
                var denominator = item.Item2;
                temp = numerator.Divide(denominator.Add(temp));
            }
            return a.Add(temp);
        }
        /// <summary>
        /// 正則連分数を計算する。
        /// </summary>
        /// <param name="a">[a; args[0], args[1], args[2], ...]</param>
        /// <param name="args">[a; args[0], args[1], args[2], ...]</param>
        /// <returns>計算結果</returns>
        public static double RegularContinuedFraction(double a, params int[] args)
        {
            double temp = 0;
            var r = args.Reverse();
            foreach (var item in r)
            {
                temp = 1.0 / (item + temp);
            }
            return a + temp;
        }
        /// <summary>
        /// 正則連分数を計算する。
        /// </summary>
        /// <param name="a">[a; args[0], args[1], args[2], ...]</param>
        /// <param name="args">[a; args[0], args[1], args[2], ...]</param>
        /// <returns>計算結果</returns>
        public static T RegularContinuedFraction<T>(T a, params T[] args) where T : IMath<T>
        {
            T temp = a.Zero;
            var reversed = args.Reverse();
            foreach (var item in reversed)
            {
                temp = a.One.Divide(item.Add(temp));
            }
            return a.Add(temp);
        }
        #endregion 連分数

        #region 貴金属数
        /// <summary>
        /// 貴金属数を計算する。
        /// </summary>
        /// <param name="n">自然数</param>
        /// <returns>貴金属数</returns>
        public static double MetallicNumber(int n)
        {
            return (n + System.Math.Sqrt(n * n + 4)) / 2;
        }
        /// <summary>
        /// 貴金属数を計算する。
        /// </summary>
        /// <param name="n">自然数</param>
        /// <returns>貴金属数</returns>
        public static T MetallicNumber<T>(T n) where T : IMath<T>
        {
            var two = n.From(2);
            var four = n.From(4);
            return n.Add(n.Multiply(n).Add(four).Sqrt()).Divide(two);
        }
        #endregion 貴金属数
    }
}
