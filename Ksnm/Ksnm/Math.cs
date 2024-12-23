/*
The zlib License

Copyright (c) 2014-2024 Takahiro Kasanami

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
using Ksnm.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SMath = System.Math;

namespace Ksnm
{
    /// <summary>
    /// System.Math に無い機能を定義したクラス
    /// </summary>
    public static partial class Math
    {
        #region 定数

        const int DefaultTerms = 10000;

        /// <summary>
        /// 黄金数
        /// </summary>
        public const double GoldenNumber = 1.61803398874989484820;

        /// <summary>
        /// 白銀数
        /// </summary>
        public const double SilverNumber = 2.41421356237309504880;
        /// <summary>
        /// decimal の円周率(四捨五入している)
        /// 3.1415926535897932384626433832 79 
        /// </summary>
        public const decimal DecimalPi = 3.1415926535897932384626433833m;
        /// <summary>
        /// decimal のネイピア数(四捨五入している)
        /// 2.7182818284590452353602874713 52
        /// </summary>
        public const decimal DecimalE = 2.7182818284590452353602874714m;
        /// <summary>
        /// decimal の0以上の最小値
        /// </summary>
        public const decimal DecimalEpsilon = 0.00000_00000_00000_00000_00000_001m;
        #endregion 定数

        #region ネイピア数
        /// <summary>
        /// ネイピア数を計算する
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数 ※decimalの場合28以上でオーバーフローする</param>
        /// <returns>ネイピア数 2.7182...</returns>
        public static T CalculateE<T>(T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
#if true
            T sum = T.One;
            T term = T.One;
            for (int n = 1; n <= terms; n++)
            {
                term /= T.CreateChecked(n);
                sum += term;
                if (term < tolerance)
                {
                    break;
                }
            }
            return sum;
#else
            T e = T.One;
            T factorial = T.One;
            T n = T.One;
            for (int i = 1; i <= terms; i++, n++)
            {
                factorial *= n;
                var add = T.One / factorial;
                e += add;
                if (add < tolerance)
                {
                    break;
                }
            }
            return e;
#endif
        }
        /// <summary>
        /// ネイピア数を計算する
        /// </summary>
        /// <returns>ネイピア数 2.7182...</returns>
        public static T CalculateE<T>() where T : IFloatingPointIeee754<T>
        {
            return CalculateE(T.Epsilon);
        }
        /// <summary>
        /// ネイピア数を計算する
        /// </summary>
        /// <returns>ネイピア数 2.7182...</returns>
        public static decimal CalculateDecimalE()
        {
            return CalculateE(DecimalEpsilon, 27);
        }
        #endregion ネイピア数

        #region 円周率
        /// <summary>
        /// 円周率を計算する
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>円周率 3.1415...</returns>
        public static T CalculatePi<T>(T tolerance, int terms = DefaultTerms)
            where T : INumber<T>
        {
            Func<T, T> Sqrt = (T value) =>
            {
                return Math.Sqrt(value, tolerance, terms);
            };
            return Ksnm.Science.Mathematics.Algorithm.GaussLegendre<T>(tolerance, terms, Sqrt);
        }
        /// <summary>
        /// 円周率を計算する
        /// </summary>
        /// <param name="terms">単項式数</param>
        /// <returns>円周率 3.1415...</returns>
        public static T CalculatePi<T>(int terms = DefaultTerms) where T : IFloatingPointIeee754<T>
        {
            return Ksnm.Science.Mathematics.Algorithm.GaussLegendre<T>(terms);
        }
        /// <summary>
        /// 円周率を計算する
        /// </summary>
        /// <returns>円周率 3.14159265358979323846264338 17</returns>
        public static decimal CalculateDecimalPi()
        {
            return CalculatePi(DecimalEpsilon, 8);
        }
        #endregion 円周率

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

        #region 素数
        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        public static bool IsPrime<T>(T value) where T : INumber<T>
        {
            // 1以下の数は素数ではない
            T _2 = T.CreateChecked(2);
            if (value < _2)
            {
                return false;
            }
            // 2と3は素数
            T _3 = T.CreateChecked(3);
            if (value == _2 || value == _3)
            {
                return true;
            }
            // 2や3で割り切れる場合は素数ではない
            T _0 = T.Zero;
            if (value % _2 == _0 || value % _3 == _0)
            {
                return false;
            }
            // 6の倍数±1で割り切れるかを確認
            // ※素数でない数は必ず √n 以下の因数を持つ、i * i <= value は i <= √n と同じ
            T _5 = T.CreateChecked(5);
            T _6 = T.CreateChecked(6);
            for (T i = _5; i * i <= value; i += _6)
            {
                if (value % i == _0 || value % (i + _2) == _0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 指定した数以下の非素数を計算します。
        /// ただし、順不同。
        /// </summary>
        static public IEnumerable<int> CalculateNotPrimes(int max)
        {
            // 各値が非素数ならtrue
            // isNotPrimes[0],[1]は使用しない
            var isNotPrimes = new bool[max + 1];
            for (int i = 2; i * i <= max; i++)
            {
                if (isNotPrimes[i]) { continue; }
                // iの倍数には非素数を設定
                for (int j = i * i; j <= max; j += i)
                {
                    if (isNotPrimes[j]) { continue; }
                    // 非素数を設定
                    isNotPrimes[j] = true;
                    yield return j;
                }
            }
        }
        /// <summary>
        /// 指定した数以下の素数を計算します
        /// </summary>
        static public IEnumerable<int> CalculatePrimes(int max)
        {
            // 各値が非素数ならtrue
            // isNotPrimes[0],[1]は使用しない
            var isNotPrimes = new bool[max + 1];
            for (int i = 2; i * i <= max; i++)
            {
                if (isNotPrimes[i]) { continue; }
                // iの倍数には非素数を設定
                for (int j = i * i; j <= max; j += i)
                {
                    if (isNotPrimes[j]) { continue; }
                    // 非素数を設定
                    isNotPrimes[j] = true;
                }
            }
            // 非素数以外＝素数　を返す
            for (int i = 2; i <= max; i++)
            {
                if (isNotPrimes[i] == false)
                {
                    yield return i;
                }
            }
        }
        /// <summary>
        /// n番目の素数を計算します。
        /// なお、0番目は2となります。
        /// </summary>
        static public int CalculatePrime(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException($"引数 {nameof(n)} は0以上である必要があります。");
            }
            var primes = new List<int>();
            int number = 2;// 素数の候補
            while (primes.Count <= n)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
                number++;
            }
            return primes[n];
        }
        #endregion 素数

        #region レピュニット数
        /// <summary>
        /// n番目のレピュニット数を計算する
        /// </summary>
        public static int Repunit(int n, int radix)
        {
            if (radix <= 1)
            {
                throw new ArgumentException($"{nameof(radix)}は2以上の必要があります。");
            }
            if (n < 0)
            {
                throw new ArgumentException($"{nameof(n)}は1以上の必要があります。");
            }
            if (radix == 2)
            {
                return (1 << n) - 1;
            }
            return (Pow(radix, n) - 1) / (radix - 1);
        }
        #endregion レピュニット数

        #region Max Min
        public static T Max<T>(T value1, T value2) where T : INumber<T>
        {
            if (value1 < value2)
            {
                return value2;
            }
            return value1;
        }
        public static T Max<T>(T value1, T value2, T value3) where T : INumber<T>
        {
            return Max(Max(value1, value2), value3);
        }
        public static T Min<T>(T value1, T value2) where T : INumber<T>
        {
            if (value1 > value2)
            {
                return value2;
            }
            return value1;
        }
        public static T Min<T>(T value1, T value2, T value3) where T : INumber<T>
        {
            return Min(Min(value1, value2), value3);
        }
        #endregion Max Min

        #region Abs
        /// <summary>
        /// 数値の絶対値を返します。
        /// </summary>
        public static BigDecimal Abs(BigDecimal value)
        {
            if (value < 0)
            {
                return -value;
            }
            return value;
        }
        #endregion Abs

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

        #region Ramp ランプ関数
        /// <summary>
        /// ランプ関数
        /// </summary>
        public static T Ramp<T>(T x) where T : INumber<T>
        {
            if (x < T.Zero)
            {
                return T.Zero;
            }
            return x;
        }
        #endregion Ramp

        #region UnitStep 単位ステップ関数
        /// <summary>
        /// 単位ステップ関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0か1の値</returns>
        public static T UnitStep<T>(T x) where T : INumber<T>
        {
            if (x < T.Zero)
            {
                return T.Zero;
            }
            return T.One;
        }
        /// <summary>
        /// 単位ステップ関数の導関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0の値</returns>
        public static T DerUnitStep<T>(T x) where T : INumber<T>
        {
            return T.Zero;
        }
        #endregion UnitStep

        #region HeavisideStep ヘヴィサイドの階段関数
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static T HeavisideStep<T>(T x, T c) where T : INumber<T>
        {
            if (x < T.Zero)
            {
                return T.Zero;
            }
            else if (x > T.Zero)
            {
                return T.One;
            }
            return c;
        }
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static double HeavisideStep(double x, double c)
        {
            if (x < 0.0)
            {
                return 0.0;
            }
            else if (x > 0.0)
            {
                return 1.0;
            }
            return c;
        }
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static T HeavisideStep<T>(T x) where T : INumber<T> => HeavisideStep(x, T.CreateChecked(0.5));
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <param name="c">x=0の時の返り値</param>
        /// <returns>0か1かcの値</returns>
        public static double HeavisideStep(double x) => HeavisideStep(x, 0.5);
        /// <summary>
        /// ヘヴィサイドの階段関数の導関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0の値</returns>
        public static T DerHeavisideStep<T>(T x) where T : INumber<T>
        {
            return T.Zero;
        }
        /// <summary>
        /// ヘヴィサイドの階段関数の導関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0の値</returns>
        public static double DerHeavisideStep(double x)
        {
            return 0.0;
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
        public static T Sigmoid<T>(T x, T gain, T tolerance) where T : INumber<T>
        {
            return T.One / (T.One + Exp<T>(-gain * x, tolerance));
        }
        /// <summary>
        /// シグモイド関数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="gain">ゲイン
        /// 1.0(標準)の場合、xに6.0を与えると約1.0になる。
        /// 5.0の場合、xに1.0を与えると約1.0になる。</param>
        /// <returns></returns>
        public static T Sigmoid<T>(T x, T gain)
            where T : INumber<T>, IExponentialFunctions<T>
        {
            return T.One / (T.One + T.Exp(-gain * x));
        }
        /// <summary>
        /// 標準シグモイド関数(ゲイン=1.0)
        /// </summary>
        public static T StandardSigmoid<T>(T x, T tolerance) where T : INumber<T>
        {
            return T.One / (T.One + Exp(-x, tolerance));
        }
        /// <summary>
        /// 標準シグモイド関数(ゲイン=1.0)
        /// </summary>
        public static T StandardSigmoid<T>(T x)
            where T : INumber<T>, IExponentialFunctions<T>
        {
            return T.One / (T.One + T.Exp(-x));
        }
        /// <summary>
        /// 標準シグモイド関数(ゲイン=1.0)
        /// </summary>
        public static double StandardSigmoid(double x)
        {
            return 1.0 / (1.0 + SMath.Exp(-x));
        }
        /// <summary>
        /// シグモイド関数の導関数
        /// </summary>
        public static T DerStandardSigmoid<T>(T x) where T : INumber<T>
        {
            return x * (T.One - x);
        }
        /// <summary>
        /// シグモイド関数の導関数
        /// </summary>
        public static double DerStandardSigmoid(double x)
        {
            return x * (1.0 - x);
        }
        #endregion Sigmoid

        #region Identity 恒等関数
        /// <summary>
        /// 恒等関数
        /// </summary>
        public static T Identity<T>(T x) where T : INumber<T>
        {
            return x;
        }
        /// <summary>
        /// 恒等関数
        /// </summary>
        public static double Identity(double x)
        {
            return x;
        }
        /// <summary>
        /// 恒等関数の導関数
        /// </summary>
        public static T DerIdentity<T>(T x) where T : INumber<T>
        {
            return T.One;
        }
        /// <summary>
        /// 恒等関数の導関数
        /// </summary>
        public static double DerIdentity(double x)
        {
            return 1.0;
        }
        #endregion 恒等関数 Identity

        #region Tanh 双曲線正接関数
        /// <summary>
        /// 双曲線正接関数
        /// </summary>
        public static T Tanh<T>(T x, T tolerance) where T : INumber<T>
        {
            var ePlus = Exp(x, tolerance);
            var eMinus = Exp(-x, tolerance);
            return (ePlus - eMinus) / (ePlus + eMinus);
        }
        /// <summary>
        /// 双曲線正接関数
        /// </summary>
        public static T Tanh<T>(T x)
            where T : INumber<T>, IExponentialFunctions<T>
        {
            var ePlus = T.Exp(x);
            var eMinus = T.Exp(-x);
            return (ePlus - eMinus) / (ePlus + eMinus);
        }
        /// <summary>
        /// 双曲線正接関数の導関数
        /// </summary>
        public static T DerTanh<T>(T x) where T : INumber<T>
        {
            return (T.One - x * x);
        }
        /// <summary>
        /// 双曲線正接関数の導関数
        /// </summary>
        public static double DerTanh(double x)
        {
            return (1.0 - x * x);
        }
        #endregion Tanh

        #region ReLU 正規化線形関数
        /// <summary>
        /// 正規化線形関数
        /// </summary>
        public static T ReLU<T>(T x) where T : INumber<T>
        {
            return T.Max(T.Zero, x);
        }
        /// <summary>
        /// 正規化線形関数の導関数
        /// </summary>
        public static T DerReLU<T>(T x) where T : INumber<T>
        {
            if (x > T.Zero)
            {
                return T.One;
            }
            return T.Zero;
        }
        #endregion ReLU

        #region LeakyReLU
        /// <summary>
        /// 漏れている正規化線形関数
        /// a=0.01
        /// </summary>
        public static T LeakyReLU<T>(T x) where T : INumber<T>
        {
            return LeakyReLU(x, T.CreateChecked(0.01));
        }
        /// <summary>
        /// 漏れている正規化線形関数
        /// a=0.01
        /// </summary>
        public static double LeakyReLU(double x)
        {
            return LeakyReLU(x, 0.01);
        }
        /// <summary>
        /// 漏れている正規化線形関数
        /// </summary>
        public static T LeakyReLU<T>(T x, T a) where T : INumber<T>
        {
            if (x >= T.Zero)
            {
                return x;
            }
            return x * a;
        }
        /// <summary>
        /// 漏れている正規化線形関数
        /// </summary>
        public static double LeakyReLU(double x, double a)
        {
            if (x >= 0.0)
            {
                return x;
            }
            return x * a;
        }
        /// <summary>
        /// 漏れている正規化線形関数の導関数
        /// </summary>
        public static T DerLeakyReLU<T>(T x) where T : INumber<T>
        {
            return DerLeakyReLU(x, T.CreateChecked(0.01));
        }
        /// <summary>
        /// 漏れている正規化線形関数の導関数
        /// </summary>
        public static double DerLeakyReLU(double x)
        {
            return DerLeakyReLU(x, 0.01);
        }
        /// <summary>
        /// 漏れている正規化線形関数の導関数
        /// </summary>
        public static T DerLeakyReLU<T>(T x, T a) where T : INumber<T>
        {
            if (x >= T.Zero)
            {
                return T.One;
            }
            return a;
        }
        /// <summary>
        /// 漏れている正規化線形関数の導関数
        /// </summary>
        public static double DerLeakyReLU(double x, double a)
        {
            if (x >= 0.0)
            {
                return 1.0;
            }
            return a;
        }
        #endregion LeakyReLU

        #region Softplus
        /// <summary>
        /// ソフトプラス関数
        /// </summary>
        public static T Softplus<T>(T x, T tolerance) where T : INumber<T>
        {
            return Log(T.One + Exp(x, tolerance), tolerance);
        }
        /// <summary>
        /// ソフトプラス関数
        /// </summary>
        public static T Softplus<T>(T x)
            where T : INumber<T>, ILogarithmicFunctions<T>, IExponentialFunctions<T>
        {
            return T.Log(T.One + T.Exp(x));
        }
        /// <summary>
        /// ソフトプラス関数の導関数
        /// </summary>
        public static T DerSoftplus<T>(T x, T tolerance) where T : INumber<T>
        {
            return T.One / (T.One + Exp(-x, tolerance));
        }
        /// <summary>
        /// ソフトプラス関数の導関数
        /// </summary>
        public static T DerSoftplus<T>(T x)
            where T : INumber<T>, IExponentialFunctions<T>
        {
            return T.One / (T.One + T.Exp(-x));
        }
        #endregion Softplus

        #region Lerp 線形補間
        /// <summary>
        /// 線形補間
        /// </summary>
        /// <param name="from">tが0のときの値</param>
        /// <param name="to">tが1のときの値</param>
        /// <param name="t">補間係数</param>
        public static T Lerp<T>(T from, T to, T t) where T : INumber<T>
        {
            return from + ((to - from) * t);
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
        public static T InverseLerp<T>(T from, T to, T value) where T : INumber<T>
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

        #region Integral 積分
        /// <summary>
        /// 台形公式を使った数値積分
        /// </summary>
        /// <param name="min">積分区間の下限</param>
        /// <param name="max">積分区間の上限</param>
        /// <param name="divisions">分割数</param>
        /// <param name="func">積分する関数</param>
        public static T TrapezoidalIntegral<T>(T min, T max, T divisions, Func<T, T> func) where T : INumber<T>
        {
            T _2 = T.CreateChecked(2);
            T h = (max - min) / divisions; // 各区間の幅
            T sum = (func(min) + func(max)) / _2;// 端点の評価
            // 中間点の評価
            for (T i = T.One; i < divisions; i++)
            {
                T x = min + i * h;
                sum += func(x);
            }
            // 面積を計算
            return sum * h;
        }
        #endregion Integral 積分

        #region Factorial 階乗
        /// <summary>
        /// 階乗
        /// </summary>
        /// <param name="value">階乗する値</param>
        /// <returns>階乗した値</returns>
        public static T Factorial<T>(T value) where T : INumber<T>
        {
            T temp = T.One;
            for (T i = T.One; i <= value; i++)
            {
                temp *= i;
            }
            return temp;
        }
        /// <summary>
        /// 範囲を指定した階乗
        /// </summary>
        public static T RangeFactorial<T>(T from, T to) where T : INumber<T>
        {
            if (from < T.One)
            {
                from = T.One;
            }
            if (from > to)
            {
                return RangeFactorial(to, from);
            }
            T result = T.One;
            for (T i = from; i <= to; i++)
            {
                result *= i;
            }
            return result;
        }
        /// <summary>
        /// 二重階乗
        /// </summary>
        public static T DoubleFactorial<T>(T value) where T : INumber<T>
        {
            T _2 = T.CreateChecked(2);
            T temp = T.One;
            for (T i = value; i > T.One; i -= _2)
            {
                temp *= i;
            }
            return temp;
        }
        #endregion Factorial 階乗

        #region Product 総乗
        public static T Product<T>(params IEnumerable<T> values) where T : INumber<T>
        {
            return values.Aggregate((result, current) => result * current);
        }
        #endregion Product 総乗

        #region Gamma ガンマ関数
#if false
        public static T Gamma<T>(T z)
            where T : INumber<T>
        {
#if true
            if (z < T.One)
            {
                return T.One;
            }
            // 乗法公式
            var product = T.One;
            var n = z - T.One;
            for (; z > T.Zero; z -= T.One)
            {
                for (T i = T.One; i < n; i++)
                {
                    product *= z - i;
                }
            }
            return product;
#elif false
            var e = NapiersConstant(tolerance, terms);
            return Science.Mathematics.Formula.StirlingsFormula(x, pi,e, tolerance, terms);
#else
            if (x <= T.Zero)
            {
                throw new ArgumentOutOfRangeException("x", "x must be greater than 0.");
            }
            T sqrtTwoPi = Sqrt<T>(pi + pi, tolerance, terms);
            T t = x - T.CreateChecked(0.5);
            T _12 = T.CreateChecked(12);
            return sqrtTwoPi
                * Pow<T>(t, t, tolerance, terms)
                * Exp<T>(-t, tolerance, terms)
                * (T.One + T.One / (_12 * t));
#endif
        }
        public static T Gamma<T>(T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
            return Gamma<T>(x, T.Pi, tolerance, terms);
        }
        public static T Gamma<T>(T x, int terms = DefaultTerms)
            where T : IFloatingPointIeee754<T>
        {
            return Gamma<T>(x, T.Pi, T.Epsilon, terms);
        }
#endif
        #endregion Gamma ガンマ関数

        #region Pow
        /// <summary>
        /// べき乗
        /// </summary>
        /// <param name="baseValue">底</param>
        /// <param name="exponent">指数</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns></returns>
        public static T Pow<T>(T baseValue, T exponent, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>
        {
            return _Pow(baseValue, exponent, tolerance, terms, Log, Exp);
        }
        public static T _Pow<T>(T baseValue, T exponent, T tolerance, int terms, Func<T, T, int, T> Log, Func<T, T, int, T> Exp)
            where T : INumber<T>
        {
            // 0乗は1
            if (exponent == T.Zero)
            {
                return T.One;
            }
            // 1に何をかけても1
            if (baseValue == T.One)
            {
                return T.One;
            }
#if true
            if (baseValue < T.Zero)
            {
                if (T.IsInteger(exponent))
                {
                    return IntegerPow(baseValue, exponent);
                }
                throw new ArgumentException("Negative base with non-integer exponent is not real.");
            }

            if (baseValue == T.Zero)
            {
                if (exponent < T.Zero)
                {
                    throw new ArgumentException("Zero base with negative exponent is not defined.");
                }
                return T.Zero;
            }
            T _2 = T.CreateChecked(2);

            if (baseValue < T.Zero)
            {
                var log = Log(-baseValue, tolerance, terms);
                // 負の基数の場合、指数が整数かどうかを確認
                if (exponent % _2 == T.Zero)
                {
                    return Exp(exponent * log, tolerance, terms);
                }
                else
                {
                    return -Exp(exponent * log, tolerance, terms);
                }
            }
            else
            {
                // 正の基数の場合、通常の計算
                var log = Log(baseValue, tolerance, terms);
                return Exp(exponent * log, tolerance, terms);
            }
#elif true// 繰り返し二乗法
            if (baseValue < T.Zero && T.IsInteger(exponent) == false)
            {
                throw new ArgumentException("指数が整数でない負の底は実数ではない。");
            }

            T result = T.One;
            T currentMultiplier = baseValue;
            T currentExponent = exponent;
            T _2 = T.CreateChecked(2);

            while (T.Abs(currentExponent) > tolerance)
            {
                if (currentExponent % _2 != T.Zero)
                {
                    result *= currentMultiplier;
                }
                currentMultiplier *= currentMultiplier;
                currentExponent /= _2;
            }
            return result;
#elif false
            int n = 55;// 大きな整数 n を設定
            T root = Root(baseValue, n, tolerance);
            return Pow(root, int.CreateTruncating(T.CreateChecked(n) * exponent));
#elif false// 誤差が大きいのでOFF
            // LogとExpを使う方法
            if (baseValue < T.Zero)
            {
                T lnBase = Log(-baseValue, tolerance);
                T result = Exp(exponent * lnBase, tolerance);
                return -result;
            }
            else
            {
                T lnBase = Log(baseValue, tolerance);
                T result = Exp(exponent * lnBase, tolerance);
                return result;
            }
#else
            // 冪級数展開
            if (baseValue < T.Zero)
            {
                // 負数の場合 一旦正数にする
                return -Pow<T>(-baseValue, exponent, tolerance, terms);
            }
            // baseValue が2より大きい場合は、1 に近くなるように変換
            var _2 = T.CreateChecked(2);
            if (baseValue > _2)
            {
                T factor = T.Zero;
                while (baseValue > _2)
                {
                    baseValue /= _2;
                    factor++;
                }
#if DEBUG
                var test = System.Math.Pow(double.CreateChecked(baseValue), double.CreateChecked(exponent));
#endif
                T partialResult = Pow(baseValue, exponent, tolerance, terms);
                return Pow(_2, factor * exponent, tolerance, terms) * partialResult;
            }
            // baseValue を 1 + x に変換
            T x = baseValue - T.One;
            T sum = T.One;
            T term = T.One;
            for (int n = 1; n <= terms; n++)
            {
                term *= (exponent - T.CreateChecked(n - 1)) / T.CreateChecked(n) * x;
                sum += term;
                if (T.Abs(term) < tolerance)
                {
                    break;
                }
            }
            return sum;
#endif
        }
        public static T Pow<T>(T baseValue, T exponent)
            where T : INumber<T>, IExponentialFunctions<T>, ILogarithmicFunctions<T>
        {
            // 0乗は1
            if (exponent == T.Zero)
            {
                return T.One;
            }
            // 1に何をかけても1
            if (baseValue == T.One)
            {
                return T.One;
            }
            if (baseValue < T.Zero)
            {
                if (T.IsInteger(exponent))
                {
                    return IntegerPow(baseValue, exponent);
                }
                throw new ArgumentException("Negative base with non-integer exponent is not real.");
            }
            if (baseValue == T.Zero)
            {
                if (exponent < T.Zero)
                {
                    throw new ArgumentException("Zero base with negative exponent is not defined.");
                }
                return T.Zero;
            }
            T _2 = T.CreateChecked(2);

            if (baseValue < T.Zero)
            {
                var log = T.Log(-baseValue);
                // 負の基数の場合、指数が整数かどうかを確認
                if (exponent % _2 == T.Zero)
                {
                    return T.Exp(exponent * log);
                }
                else
                {
                    return -T.Exp(exponent * log);
                }
            }
            else
            {
                // 正の基数の場合、通常の計算
                var log = T.Log(baseValue);
                return T.Exp(exponent * log);
            }
        }
        /// <summary>
        /// exponentが整数のPow関数。
        /// baseValueが0,exponentが負数のときは0除算エラーが発生します。
        /// </summary>
        /// <typeparam name="T">実数型</typeparam>
        /// <typeparam name="TExponent">整数型</typeparam>
        /// <param name="baseValue">累乗対象</param>
        /// <param name="exponent">指数</param>
        public static T IntegerPow<T, TExponent>(T baseValue, TExponent exponent)
            where T : INumber<T>
            where TExponent : INumber<TExponent>
        {
            if (exponent == TExponent.Zero)
            {
                return T.One;
            }
            if (exponent < TExponent.Zero)
            {
                return T.One / IntegerPow(baseValue, -exponent);
            }
            T result = T.One;
#if true
            for (TExponent i = TExponent.Zero; i < exponent; i++)
            {
                result *= baseValue;
            }
#else
            if (baseValue < T.Zero)
            {
                if (TExponent.IsEvenInteger(exponent))
                {
                    return IntegerPow(-baseValue, exponent);
                }
                if (TExponent.IsOddInteger(exponent))
                {
                    return -IntegerPow(-baseValue, exponent);
                }
            }
            var _2 = TExponent.CreateChecked(2);
            while (exponent > TExponent.Zero)
            {
                if ((exponent % _2) == TExponent.One)
                {
                    result *= baseValue;
                }
                baseValue *= baseValue;
                exponent /= _2;
            }
#endif
            return result;
        }
        /// <summary>
        /// 指定の整数を指定した値で累乗した値を返します。
        /// <para>整数限定ですが、System.Math.Powより高速</para>
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        public static T Pow<T>(T baseValue, int exponent) where T : INumber<T>
        {
            return IntegerPow(baseValue, exponent);
        }
        /// <summary>
        /// 指定の符号なし整数を指定した値で累乗した値を返します。
        /// <para>符号なし整数限定ですが、System.Math.Powより高速</para>
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値</returns>
        public static T Pow<T>(T baseValue, uint exponent) where T : INumber<T>
        {
            T value = T.One;
            for (uint i = 0; i < exponent; i++)
            {
                value *= baseValue;
            }
            return value;
        }
        #region CachedPow10
        /// <summary>
        /// Pow10の計算結果をキャッシュします。
        /// 過去に計算されていたらキャッシュから返す。
        /// </summary>
        /// <param name="exponent">指数</param>
        /// <returns>baseValue を exponent で累乗した結果。</returns>
        public static BigInteger CachedPow10(int exponent)
        {
            if (Pow10Caches.ContainsKey(exponent))
            {
                return Pow10Caches[exponent];
            }
            var result = BigInteger.Pow(10, exponent);
            Pow10Caches[exponent] = result;
            return result;
        }
        /// <summary>
        /// BigIntegerPow10() の結果を保存しておき、2回目以降はこちらを使用する。
        /// </summary>
        private static readonly Dictionary<int, BigInteger> Pow10Caches = new Dictionary<int, BigInteger>();
        #endregion CachedPow10
        #endregion Pow

        #region Exp
        /// <summary>
        /// ネイピア数を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>ネイピア数を exponent で冪乗した値。</returns>
        public static T Exp<T>(T exponent, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            T sum = T.One;
            T term = T.One;
            T n = T.One;
            for (int i = 0; i < terms; i++)
            {
                term *= exponent / n;
                if (T.IsInfinity(term))
                {
                    break;
                }
                sum += term;
                if (T.Abs(term) < tolerance)
                {
                    break;
                }
                n++;
            }
            return sum;
        }
        /// <summary>
        /// ネイピア数を指定した値で冪乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <returns>ネイピア数を exponent で冪乗した値。</returns>
        public static T Exp<T>(T x) where T : IFloatingPointIeee754<T>
        {
            return Exp(x, T.Epsilon);
        }
        /// <summary>
        /// 10を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>10を exponent で冪乗した値。</returns>
        public static T Exp10<T>(T exponent, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            var _10 = T.CreateChecked(10);
            return Pow(_10, exponent, tolerance, terms);
        }
        /// <summary>
        /// 10を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <returns>10を exponent で冪乗した値。</returns>
        public static T Exp10<T>(T exponent) where T : IFloatingPointIeee754<T>
        {
            var _10 = T.CreateChecked(10);
            return Pow(_10, exponent);
        }
        /// <summary>
        /// 10を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>10を exponent で冪乗した値。</returns>
        public static T Exp2<T>(T exponent, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            var _2 = T.CreateChecked(2);
            return Pow(_2, exponent, tolerance, terms);
        }
        /// <summary>
        /// 2を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="exponent">冪指数</param>
        /// <returns>2を exponent で冪乗した値。</returns>
        public static T Exp2<T>(T exponent) where T : IFloatingPointIeee754<T>
        {
            var _2 = T.CreateChecked(2);
            return Pow(_2, exponent);
        }
        #endregion Exp

        #region Log 対数
        /// <summary>
        /// 指定した数の自然対数を返します。 (底=e) 
        /// </summary>
        /// <param name="value">対数を求める対象の数値</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T Log<T>(T value, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            if (value <= T.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "1以上の値でなければならない。");
            }
            if (value == T.One)
            {
                return T.Zero;
            }

            T sum = T.Zero;
            T numerator = (value - T.One) / (value + T.One);
            T termSquared = numerator * numerator;
            T term;
            T k = T.One;
            T _2 = T.CreateChecked(2);

            for (int i = 0; i < terms; i++)
            {
                term = numerator / k;
                sum += term;
                if (T.Abs(term) < tolerance)
                {
                    break;
                }
                numerator *= termSquared;
                k += _2;
            }
            return _2 * sum;
        }
        /// <summary>
        /// 自然対数
        /// </summary>
        public static T Log<T>(T x) where T : IFloatingPointIeee754<T>
        {
            return Log(x, T.Epsilon);
        }
        /// <summary>
        /// 対数
        /// </summary>
        /// <param name="x">対数を求める値</param>
        /// <param name="baseValue">対象の低</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        public static T LogB<T>(T x, T baseValue, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            return Log(x, tolerance, terms) / Log(baseValue, tolerance, terms);
        }
        /// <summary>
        /// 対数
        /// </summary>
        /// <param name="x">対数を求める値</param>
        /// <param name="baseValue">対象の低</param>
        public static T LogB<T>(T x, T baseValue) where T : IFloatingPointIeee754<T>
        {
            var logX = Log(x);
            var logBase = Log(baseValue);
            return logX / logBase;
        }
        /// <summary>
        /// 常用対数
        /// </summary>
        public static T Log10<T>(T x, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            var _10 = T.CreateChecked(10);
            return LogB(x, _10, tolerance, terms);
        }
        /// <summary>
        /// 常用対数
        /// </summary>
        public static T Log10<T>(T x) where T : IFloatingPointIeee754<T>
        {
            var _10 = T.CreateChecked(10);
            return LogB(x, _10);
        }
        /// <summary>
        /// 二進対数
        /// </summary>
        public static T Log2<T>(T x, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            var _2 = T.CreateChecked(2);
            return LogB(x, _2, tolerance, terms);
        }
        /// <summary>
        /// 二進対数
        /// </summary>
        public static T Log2<T>(T x) where T : IFloatingPointIeee754<T>
        {
            var _2 = T.CreateChecked(2);
            return LogB(x, _2);
        }

        const int ILogB_NaN = 0x7FFFFFFF;
        const int ILogB_Zero = (-1 - 0x7FFFFFFF);
        public static int ILogB(double x)
        {
            // Implementation based on https://git.musl-libc.org/cgit/musl/tree/src/math/ilogb.c

            if (double.IsNaN(x))
            {
                return ILogB_NaN;
            }

            ulong i = BitConverter.DoubleToUInt64Bits(x);
            int e = (int)((i >> 52) & 0x7FF);

            if (e == 0)
            {
                i <<= 12;
                if (i == 0)
                {
                    return ILogB_Zero;
                }
                for (e = -0x3FF; (i >> 63) == 0; e--, i <<= 1) ;
                return e;
            }

            if (e == 0x7FF)
            {
                return (i << 12) != 0 ? ILogB_Zero : int.MaxValue;
            }

            return e - 0x3FF;
        }
        #endregion Log

        #region Root 根
        #region Sqrt
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>0 または value の平方根。</returns>
        public static T Sqrt<T>(T value, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            if (T.IsZero(value))
            {
                return T.Zero;
            }
            var temp = value;
            var before = temp;
            for (int i = 0; i < terms; i++)
            {
                temp = (temp * temp + value) / (temp + temp);
                if (T.Abs(temp - before) < tolerance)
                {
                    break;
                }
                before = temp;
            }
            return temp;
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <param name="terms">単項式数</param>
        /// <returns>0 または value の平方根。</returns>
        public static T Sqrt<T>(T value, int terms = DefaultTerms) where T : INumber<T>
        {
            if (T.IsZero(value))
            {
                return T.Zero;
            }
            var temp = value;
            for (int i = 0; i < terms; i++)
            {
                temp = (temp * temp + value) / (temp + temp);
            }
            return temp;
        }
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// ※最下位桁は丸められます。
        /// 丸めないと Sqrt(16)=4.0000000000000000000000000001 となるため
        /// </summary>
        /// <param name="value">平方根を求める対象の数値。</param>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        public static decimal Sqrt(decimal value)
        {
            decimal tolerance = 0.0000000000_0000000000_0000000000_0000000000_0000000000_0000000000_1m;
            var result = Sqrt(value, tolerance);
            return result.RoundBottom();// 丸める
        }
        public static T Sqrt<T>(T value) where T : IFloatingPointIeee754<T>
        {
            if (value < T.Zero)
            {
                return T.NaN;
            }
            return Sqrt(value, T.Epsilon);
        }
        #endregion Sqrt
        #region InverseSqrt 逆平方根
        /// <summary>
        /// 平方根の逆数を求める
        /// </summary>
        public static float InverseSqrt(float value)
        {
            return 1 / float.Sqrt(value);
        }
        /// <summary>
        /// 平方根の逆数を求める
        /// </summary>
        public static double InverseSqrt(double value)
        {
            return 1 / double.Sqrt(value);
        }
        /// <summary>
        /// 高速逆平方根
        /// </summary>
        public static float FastInverseSqrt(float value)
        {
            const uint MagicNumber = 0x5F3759DF;
            var i = BitConverter.SingleToUInt32Bits(value);
            i = MagicNumber - (i >> 1);
            var f = BitConverter.UInt32BitsToSingle(i);
            return f * (1.5f - (value * 0.5f * f * f));
        }
        /// <summary>
        /// 高速逆平方根
        /// </summary>
        public static double FastInverseSqrt(double value)
        {
            const ulong MagicNumber = 0x5FE6EC85E7DE30DA;
            var i = BitConverter.DoubleToUInt64Bits(value);
            i = MagicNumber - (i >> 1);
            var f = BitConverter.UInt64BitsToDouble(i);
            return f * (1.5 - (value * 0.5 * f * f));
        }
        #endregion InverseSqrt 逆平方根
        /// <summary>
        /// n乗根をバイナリサーチで求める
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Root<T>(T x, int n, T epsilon) where T : INumber<T>
        {
            bool isNegative = x < T.Zero;
            // 負数の偶数乗根を求めようとするとエラー
            if (isNegative && int.IsEvenInteger(n))
            {
                throw new ArgumentException("負の数の偶数根は実在しない。");
            }
            x = T.Abs(x);
            var root = _Root(x, n, epsilon);
            if (isNegative)
            {
                return -root;
            }
            return root;
        }
        public static T Root<T>(T x, int n) where T : IFloatingPointIeee754<T>
        {
            bool isNegative = x < T.Zero;
            // 負数の偶数乗根を求めようとすると非数を返す
            if (isNegative && int.IsEvenInteger(n))
            {
                return T.NaN;
            }
            x = T.Abs(x);
            var root = _Root(x, n, T.Epsilon);
            if (isNegative)
            {
                return -root;
            }
            return root;
        }
        static T _Root<T>(T x, int n, T epsilon) where T : INumber<T>
        {
#if true
            if (x == T.Zero)
            {
                return T.Zero;
            }
            // ニュートン法による反復計算
            T _n = T.CreateChecked(n);
            // 初期推定値
            T guess = x / _n;
            T before = T.Zero;
            for (int i = 0; i < 1000; i++)
            {
                before = guess;
                guess = ((_n - T.One) * guess + x / Pow(guess, n - 1)) / _n;
                if (T.Abs(before - guess) <= epsilon)
                {
                    break;
                }
            }
            return guess;
#else
            T low = T.Zero;
            T high = x;
            T mid = T.Zero;
            T _2 = T.CreateChecked(2);

            while (high - low > epsilon)
            {
                mid = (low + high) / _2;
                if (Pow<T>(mid, n) < x)
                {
                    low = mid;
                }
                else
                {
                    high = mid;
                }
            }
            return mid;
#endif
        }
        #endregion Root 根

        #region Radical 根基
        /// <summary>
        /// 根基
        /// 底に現れる指数を全部１にしたもの
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Radical<T>(T value) where T : INumber<T>
        {
            if (value == T.Zero)
            {
                throw new ArgumentException($"{nameof(value)}が0です。");
            }
            if (value == T.One)
            {
                return T.One;
            }
            var primes = PrimeFactorization(value);// 素因数分解
            primes = primes.Distinct();// 重複を削除＝指数を1とする
            return Product(primes);// すべてかけあわせる
        }
        #endregion Radical 根基

        #region 三角関数
        /// <summary>
        /// 指定された角度のサインを返します。
        /// </summary>
        /// <param name="x">ラジアンで表した角度。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        public static T Sin<T>(T angle, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>, IFloatingPoint<T>
        {
            // -2π～2πにする
            if (angle < T.Tau || angle > T.Tau)
            {
                angle -= T.Floor(angle / (T.Tau)) * T.Tau;
            }
            T sum = angle;
            T add = angle;
            for (int n = 1; n <= terms; n++)
            {
                var d = (n + n + 1) * (n + n);
                add *= -(angle * angle) / T.CreateChecked(d);
                sum += add;
                if (T.Abs(add) < tolerance)
                {
                    break;
                }
            }
            return sum;
        }
        public static T Sin<T>(T angle)
            where T : IFloatingPointIeee754<T>
        {
            return Sin(angle, T.Epsilon);
        }
        /// <summary>
        /// 指定された角度のコサインを返します。
        /// </summary>
        /// <param name="angle">ラジアンで表した角度。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        public static T Cos<T>(T angle, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>, IFloatingPoint<T>
        {
            return Sin(T.Pi / T.CreateChecked(2) - angle, tolerance, terms);
        }
        public static T Cos<T>(T angle)
            where T : IFloatingPointIeee754<T>
        {
            return Cos(angle, T.Epsilon);
        }
        /// <summary>
        /// 指定された角度のタンジェントを返します。
        /// </summary>
        /// <param name="angle">ラジアンで表した角度。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        public static T Tan<T>(T angle, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>, IFloatingPoint<T>
        {
            return Sin(angle, tolerance, terms) / Cos(angle, tolerance, terms);
        }
        public static T Tan<T>(T angle)
            where T : IFloatingPointIeee754<T>
        {
            return Tan(angle, T.Epsilon);
        }
        #region Asin
        /// <summary>
        /// サインが指定数となる角度を返します。
        /// </summary>
        /// <param name="x">サインを表す数で、-1 以上 1 以下である必要があります。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。</returns>
        public static T Asin<T>(T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
            if (x < -T.One || x > T.One)
            {
                throw new ArgumentOutOfRangeException($"{nameof(x)}={x} が範囲外の値です。");
            }
            var _2 = T.CreateChecked(2);
            if (x == T.One)
            {
                return T.Pi / _2;
            }
            if (x == -T.One)
            {
                return -T.Pi / _2;
            }
            terms *= 2;// 2ずつカウントしていくので2倍する
            T sum = x;// 1回目のループは省略
            var x2 = x * x;// 前回のに2乗ずつかけていく。
            var x2Product = x;
            var numerator = T.One;
            var denominator = T.One;
            for (int odd = 1; odd < terms; odd += 2)
            {
                var even = odd + 1;
                numerator *= T.CreateChecked(odd);
                denominator *= T.CreateChecked(even);
                if (T.IsInfinity(numerator) || T.IsInfinity(denominator))
                {
                    break;
                }
                x2Product *= x2;
                var odd2 = T.CreateChecked(odd + 2);// 次の奇数
                var add = (numerator / denominator) * (x2Product / odd2);
                sum += add;
                if (T.Abs(add) < tolerance)
                {
                    break;
                }
            }
            return sum;
        }
        public static T Asin<T>(T x)
            where T : IFloatingPointIeee754<T>
        {
            return Asin(x, T.Epsilon);
        }
        #endregion Asin
        /// <summary>
        /// コサインが指定数となる角度を返します。
        /// </summary>
        /// <param name="x">コサインを表す数で、-1 以上 1 以下である必要があります。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>0 ≤θ≤π の、ラジアンで表した角度 θ。</returns>
        public static T Acos<T>(T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
            return T.Pi / T.CreateChecked(2) - Asin(x, tolerance, terms);
        }
        public static T Acos<T>(T x)
            where T : IFloatingPointIeee754<T>
        {
            return Acos(x, T.Epsilon);
        }
        /// <summary>
        /// タンジェントが指定数となる角度を返します。
        /// </summary>
        /// <param name="x">タンジェントを表す数。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>-π/2 ≤θ≤π/2 の、ラジアンで表した角度 θ。</returns>
        public static T Atan<T>(T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
#if false
            // TODO:x の値によっては、count=100では足りない。
            //if (x < -1 || x > 1)
            //{
            //    throw new ArgumentOutOfRangeException($"{nameof(x)}={x} が範囲外の値です。");
            //}
            decimal sum = x;// １周目は省略
            try
            {
                for (int n = 1; n < count; n++)
                {
                    var n2 = (2 * n + 1);
                    if (IsEven(n))
                    {
                        sum += Pow(x, n2) / n2;
                    }
                    else
                    {
                        sum -= Pow(x, n2) / n2;
                    }
                }
            }
            catch(OverflowException)
            {
                // オーバーフローが発生したらそこまでの計算結果を返す。
            }
            return sum;
#else
            var _1 = T.One;
            var _2 = T.CreateChecked(2);
            // 過去の積を再利用する
            T product = _1;
            int k = 1;
            /// ｘ の2乗
            var x2 = (x * x);
            T sum = _1;// n=0のときは1なのでループ１回目は省略
            for (int n = 1; n < terms; n++)
            {
#if false
                decimal product = 1;
                for (int k = 1; k <= n; k++)
                {
                    decimal multiplier = (2m * k * x2) / ((2m * k + 1) * (1 + x2));
                    product *= multiplier;
                }
#else
                if (k <= n)
                {
                    var _k = T.CreateChecked(k);
                    T multiplier = (_2 * _k * x2) / ((_2 * _k + _1) * (_1 + x2));
                    product *= multiplier;
                    k++;
                }
#endif
                sum += product;
                if (T.Abs(product) < tolerance)
                {
                    break;
                }
            }
            var temp = x / (_1 + x * x);
            return temp * sum;
#endif
        }
        public static T Atan<T>(T x)
            where T : IFloatingPointIeee754<T>
        {
            return Atan(x, T.Epsilon);
        }
        /// <summary>
        /// タンジェントが 2 つの指定された数の商である角度を返します。
        /// </summary>
        /// <param name="y">点の y 座標。</param>
        /// <param name="x">点の x 座標。</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>-π≤θ≤π および tan(θ) = y / x の、ラジアンで示した角度 θ。(x, y) は、デカルト座標の点を示します。
        /// 次の点に注意してください。
        /// クワドラント 1 の (x, y) の場合は、0 < θ < π/2。
        /// クワドラント 2 の (x, y) の場合は、π/2 < θ≤π。
        /// クワドラント 3 の (x, y) の場合は、-π < θ < -π/2。
        /// クワドラント 4 の (x, y) の場合は、-π/2 < θ < 0。
        /// クワドラント間の境界上にある点の場合は、次の戻り値になります。
        /// y が 0 で x が負数でない場合は、θ = 0。
        /// y が 0 で x が負の場合は、θ = π。
        /// y が正で x が 0 の場合は、θ = π/2。
        /// y が負数で x が 0 の場合は、θ = -π/2。
        /// y が 0 かつ x が 0 の場合は、θ = 0。</returns>
        public static T Atan2<T>(T y, T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
            if (x > T.Zero)
            {
                return Atan(y / x, tolerance, terms);
            }
            else if (x < T.Zero)
            {
                if (y > T.Zero)
                {
                    return Atan(y / x, tolerance, terms) + T.Pi;
                }
                else
                {
                    return Atan(y / x, tolerance, terms) - T.Pi;
                }
            }
            else// x==0
            {
                var _2 = T.CreateChecked(2);
                if (y > T.Zero)
                {
                    return +(T.Pi / _2);
                }
                else if (y < T.Zero)
                {
                    return -(T.Pi / _2);
                }
                // y==0
                return T.Zero;
            }
        }
        public static T Atan2<T>(T y, T x)
            where T : IFloatingPointIeee754<T>
        {
            return Atan2(y, x, T.Epsilon);
        }
        public static T Atan2Pi<T>(T y, T x, T tolerance, int terms = DefaultTerms)
            where T : INumber<T>, IFloatingPointConstants<T>
        {
            return Atan2(y, x, tolerance, terms) / T.Pi;
        }
        public static T Atan2Pi<T>(T y, T x)
            where T : IFloatingPointIeee754<T>
        {
            return Atan2(y, x) / T.Pi;
        }
        #endregion 三角関数

        #region 約数
        /// <summary>
        /// 約数の個数を数える
        /// </summary>
        /// <returns>約数の個数</returns>
        public static int CountDivisors<T>(T n) where T : INumber<T>
        {
            if (n == T.Zero)
            {
                return 1;// 0の約数は0 
            }
            if (n == T.One)
            {
                return 1;// 1の約数は1
            }
            if (n < T.Zero)
            {
                return CountDivisors(-n);
            }
            var factors = PrimeFactorization(n);
            var factorsExponent = FactorsExponent(factors);
            return factorsExponent.Values// 個数
                .Select(count => count + 1)// 個数に1を足す
                .Aggregate((total, next) => total * next);// すべて乗算
        }
        #endregion 約数

        #region 最大公約数 GreatestCommonDivisor
        /// <summary>
        /// 最大公約数
        /// </summary>
        static T _GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
        {
            if (a < b)
            {
                // 引数を入替えて自分を呼び出す
                return _GreatestCommonDivisor(b, a);
            }
            while (T.IsZero(b) == false)
            {
                var remainder = a % b;
                a = b;
                b = remainder;
            }
            return T.Abs(a);
        }
        /// <summary>
        /// 最大公約数を計算する。
        /// <para>負数は正数にされる。</para>
        /// </summary>
        /// <returns>最大公約数</returns>
        public static T GreatestCommonDivisor<T>(T value, params IEnumerable<T> values) where T : INumber<T>
        {
            if (values.Count() <= 0)
            {
                return value;
            }
            foreach (var value2 in values)
            {
                value = _GreatestCommonDivisor(value, value2);
            }
            return value;
        }
        #endregion 最大公約数 GreatestCommonDivisor

        #region 最小公倍数
        /// <summary>
        /// 最小公倍数を計算する。
        /// </summary>
        /// <param name="a">整数</param>
        /// <param name="b">整数</param>
        /// <returns>最小公倍数</returns>
        public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        {
            var gcd = GreatestCommonDivisor(a, b);
            return (a * b) / gcd;
        }
        #endregion 最小公倍数

        #region Coprime 互いに素
        /// <summary>
        /// 指定した値が、互いに素か判定する。
        /// = 指定した値を共に割り切る正の整数が 1 のみ。
        /// = 指定した値の最大公約数が 1 である。
        /// </summary>
        public static bool Coprime<T>(T value, params IEnumerable<T> values) where T : INumber<T>
        {
            return GreatestCommonDivisor(value, values) == T.One;
        }
        #endregion Coprime 互いに素

        #region 素因数分解
        /// <summary>
        /// 素因数分解
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>一連の素数</returns>
        public static IEnumerable<T> PrimeFactorization<T>(T value) where T : INumber<T>
        {
            // 1未満は無し
            if (value < T.One)
            {
                yield break;
            }
#if false

            var temp = value;
            var _2 = T.CreateChecked(2);

            for (T i = _2; i * i <= value;)
            {
                if (temp % i == T.Zero)
                {
                    temp /= i;
                    yield return i;
                }
                else
                {
                    i++;
                }
            }
            if (temp > T.One)
            {
                yield return temp;
            }
#else
            // 2で割れるだけ割る
            var _2 = T.CreateChecked(2);
            while (value % _2 == T.Zero)
            {
                yield return _2;
                value /= _2;
            }

            // 奇数で割る
            var _3 = T.CreateChecked(3);
            for (T i = _3; i * i <= value; i += _2)
            {
                while (value % i == T.Zero)
                {
                    yield return i;
                    value /= i;
                }
            }

            // value が 2 より大きい場合はそれが素数である
            if (value > _2)
            {
                yield return value;
            }
#endif
        }
        #endregion 素因数分解

        #region 因数
        /// <summary>
        /// 因数の集合から、べき指数の集合に変換します。
        /// </summary>
        /// <param name="factors">因数の集合</param>
        /// <returns>[因数]=因数の個数</returns>
        public static Dictionary<T, int> FactorsExponent<T>(IEnumerable<T> factors) where T : INumber<T>
        {
            return factors
                .GroupBy(item => item)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        #endregion 因数

        #region 合成数
        /// <summary>
        /// 高度合成数を判定する。
        /// 1と2は合成数ではないが、高度合成数に含める。
        /// </summary>
        public static bool IsHighlyComposite<T>(T n) where T : INumber<T>
        {
            var divisorsFromN = CountDivisors(n);
            for (T i = n - T.One; i > T.One; i--)
            {
                var divisorsFromI = CountDivisors(i);
                if (divisorsFromN <= divisorsFromI)
                {
                    // 一つでも約数が多いならfalse
                    return false;
                }
            }
            return true;
        }
        #endregion 合成数

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
        public static T ContinuedFraction<T>(T a, IReadOnlyList<Tuple<T, T>> args) where T : INumber<T>
        {
            T temp = T.Zero;
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
        /// 正則連分数を計算する。
        /// </summary>
        /// <param name="a">[a; args[0], args[1], args[2], ...]</param>
        /// <param name="args">[a; args[0], args[1], args[2], ...]</param>
        /// <returns>計算結果</returns>
        public static T RegularContinuedFraction<T>(T a, params T[] args) where T : INumber<T>
        {
            T temp = T.Zero;
            var reversed = args.Reverse();
            foreach (var item in reversed)
            {
                temp = T.One / (item + temp);
            }
            return a + temp;
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
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>貴金属数</returns>
        public static T MetallicNumber<T>(T n, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            var _2 = T.CreateChecked(2);
            var _4 = T.CreateChecked(4);
            return (n + Sqrt(n * n + _4, tolerance, terms)) / _2;
        }
        #endregion 貴金属数

        #region 順列・組合せ
        /// <summary>
        /// 順列の総数
        /// </summary>
        /// <param name="n">選ぶことのできる元の種類</param>
        /// <param name="r">nから選ぶ数</param>
        /// <returns>n個のものからr個とった順列の総数</returns>
        public static BigInteger Permutation(int n, int r)
        {
            return Factorial<BigInteger>(n) / Factorial<BigInteger>(n - r);
        }
        /// <summary>
        /// 組合せの総数
        /// </summary>
        /// <param name="n">選ぶことのできる元の種類</param>
        /// <param name="r">nから選ぶ数</param>
        /// <returns>n個のものからr個とった組合せの総数</returns>
        public static BigInteger Combination(int n, int r)
        {
            return Factorial<BigInteger>(n) / (Factorial<BigInteger>(r) * Factorial<BigInteger>(n - r));
        }
        #endregion 順列・組合せ
    }
}