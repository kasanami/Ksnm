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

        const int DefaultTerms = 1000;

        /// <summary>
        /// 黄金数
        /// </summary>
        public const double GoldenNumber = 1.61803398874989484820;

        /// <summary>
        /// 白銀数
        /// </summary>
        public const double SilverNumber = 2.41421356237309504880;
        /// <summary>
        /// 円周率
        /// </summary>
        public const decimal PI_Decimal = 3.141592653589793238462643383279m;
        /// <summary>
        /// ネイピア数
        /// </summary>
        public const decimal E_Decimal = 2.718281828459045235360287471352m;
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
        /// 単位ステップ関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0か1の値</returns>
        public static double UnitStep(double x)
        {
            if (x < 0.0)
            {
                return 0.0;
            }
            return 1.0;
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
        /// <summary>
        /// 単位ステップ関数の導関数
        /// </summary>
        /// <param name="x">入力</param>
        /// <returns>0の値</returns>
        public static double DerUnitStep(double x)
        {
            return 0.0;
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return T.One / (T.One + Exp<T>(-gain * x));
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
            return 1.0 / (1.0 + SMath.Exp(-gain * x));
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return T.One / (T.One + Exp(-x));
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return Tanh(x, T.Epsilon);
        }
        /// <summary>
        /// 双曲線正接関数
        /// </summary>
        public static double Tanh(double x)
        {
            var ePlus = SMath.Exp(x);
            var eMinus = SMath.Exp(-x);
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
        /// 正規化線形関数
        /// </summary>
        public static double ReLU(double x)
        {
            return double.Max(0.0, x);
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
        /// <summary>
        /// 正規化線形関数の導関数
        /// </summary>
        public static double DerReLU(double x)
        {
            if (x > 0.0)
            {
                return 1.0;
            }
            return 0.0;
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return Softplus(x, T.Epsilon);
        }
        /// <summary>
        /// ソフトプラス関数
        /// </summary>
        public static double Softplus(double x)
        {
            return SMath.Log(1.0 + SMath.Exp(x));
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return DerSoftplus(x, T.Epsilon);
        }
        /// <summary>
        /// ソフトプラス関数の導関数
        /// </summary>
        public static double DerSoftplus(double x)
        {
            return 1 / (1 + SMath.Exp(-x));
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

        #region 階乗 Factorial
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
        /// 階乗
        /// ※ long で表現できる整数の最大値は 9223372036854775807
        /// よって、20!	までなら正確に計算できる。
        /// </summary>
        /// <param name="value">階乗する値</param>
        /// <returns>階乗した値</returns>
        public static long Factorial(long value)
        {
            long temp = 1;
            for (long i = value; i > 0; i--)
            {
                temp *= i;
            }
            return temp;
        }
        /// <summary>
        /// 階乗
        /// ※ double で表現できる整数の最大値は 9007199254740992
        /// よって、18!	までなら正確に計算できる。
        /// </summary>
        /// <param name="value">階乗する値</param>
        /// <returns>階乗した値</returns>
        public static double Factorial(double value)
        {
            double temp = 1;
            for (double i = value; i > 0; i--)
            {
                temp *= i;
            }
            return temp;
        }
        /// <summary>
        /// 階乗
        /// NOTE:21!以上は BigInteger でないと表現できない
        /// </summary>
        /// <param name="value">階乗する整数</param>
        /// <returns>階乗した値</returns>
        public static BigInteger Factorial(BigInteger value)
        {
            BigInteger temp = 1;
            for (BigInteger i = value; i > 0; i--)
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
        #endregion Factorial

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
                var log = Log(-baseValue, tolerance);
                // 負の基数の場合、指数が整数かどうかを確認
                if (exponent % _2 == T.Zero)
                {
                    return Exp(exponent * log, tolerance);
                }
                else
                {
                    return -Exp(exponent * log, tolerance);
                }
            }
            else
            {
                // 正の基数の場合、通常の計算
                var log = Log(baseValue, tolerance);
                return Exp(exponent * log, tolerance);
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
            where T : INumber<T>, IFloatingPointIeee754<T>
        {
            return Pow<T>(baseValue, exponent, T.Epsilon);
        }
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
        /// <summary>
        /// 指定の decimal を指定した値で累乗した値を返します。
        /// </summary>
        /// <param name="baseValue">累乗対象の底</param>
        /// <param name="exponent">冪指数</param>
        /// <returns>累乗した値</returns>
        public static decimal Pow(decimal baseValue, int exponent)
        {
            if (exponent == 0)
            {
                return 1;
            }
            if (baseValue == 0)
            {
                return 0;
            }
            if (baseValue == 1)
            {
                return 1;
            }
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
            if (exponent == 0)
            {
                return 1;
            }
            if (baseValue == 0)
            {
                return 0;
            }
            if (baseValue == 1)
            {
                return 1;
            }
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
        #endregion Exp

        #region Log 対数
        /// <summary>
        /// 指定した数の自然対数を返します。 (底=e) 
        /// </summary>
        /// <param name="value">対数を求める対象の数値</param>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T Log<T>(T value, T tolerance, int terms = DefaultTerms) where T : INumber<T>
        {
            if (value <= T.Zero)
            {
                throw new ArgumentOutOfRangeException("x", "1以上の値でなければならない。");
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
        public static T Log<T>(T x) where T : IFloatingPointIeee754<T>
        {
            return Log(x, T.Epsilon);
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
        #endregion 三角関数

        #region 最大公約数 GreatestCommonDivisor
        /// <summary>
        /// 最大公約数
        /// </summary>
        public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
        {
            if (a < b)
            {
                // 引数を入替えて自分を呼び出す
                return GreatestCommonDivisor(b, a);
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
        /// <param name="a">整数</param>
        /// <param name="b">整数</param>
        /// <param name="c">整数</param>
        /// <returns>最大公約数</returns>
        public static T GreatestCommonDivisor<T>(T a, T b, T c) where T : INumber<T>
        {
            return GreatestCommonDivisor(GreatestCommonDivisor(a, b), c);
        }

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

        /// <summary>
        /// 最大公約数を計算する。
        /// <para>負数は正数にされる。</para>
        /// </summary>
        /// <param name="a">整数</param>
        /// <param name="b">整数</param>
        /// <param name="c">整数</param>
        /// <returns>最大公約数</returns>
        public static int GreatestCommonDivisor(int a, int b, int c)
        {
            return GreatestCommonDivisor(GreatestCommonDivisor(a, b), c);
        }

        /// <summary>
        /// 最大公約数を計算する。
        /// <para>負数は正数にされる。</para>
        /// </summary>
        /// <param name="a">符号なし整数</param>
        /// <param name="b">符号なし整数</param>
        /// <param name="c">符号なし整数</param>
        /// <returns>最大公約数</returns>
        public static uint GreatestCommonDivisor(uint a, uint b, uint c)
        {
            return GreatestCommonDivisor(GreatestCommonDivisor(a, b), c);
        }

        #endregion 最大公約数 GreatestCommonDivisor

        #region 最小公倍数
        /// <summary>
        /// 最小公倍数を計算する。
        /// </summary>
        /// <param name="a">整数</param>
        /// <param name="b">整数</param>
        /// <returns>最小公倍数</returns>
        public static int LeastCommonMultiple(int a, int b)
        {
            var gcd = GreatestCommonDivisor(a, b);
            return (a * b) / gcd;
        }
        /// <summary>
        /// 最小公倍数を計算する。
        /// </summary>
        /// <param name="a">符号なし整数</param>
        /// <param name="b">符号なし整数</param>
        /// <returns>最小公倍数</returns>
        public static uint LeastCommonMultiple(uint a, uint b)
        {
            var gcd = GreatestCommonDivisor(a, b);
            return (a * b) / gcd;
        }
        #endregion 最小公倍数

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

        #region ネイピア数
        /// <summary>
        /// ネイピア数を計算します
        /// </summary>
        /// <param name="tolerance">許容値</param>
        /// <param name="terms">単項式数</param>
        /// <returns>ネイピア数</returns>
        public static T NapiersConstant<T>(T tolerance, int terms = DefaultTerms)
            where T : INumber<T>
        {
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
        }
        #endregion ネイピア数
    }
}