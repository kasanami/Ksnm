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
namespace Ksnm.Numerics
{
    /// <summary>
    /// 数学系関数のインターフェイス
    /// </summary>
    public interface IMath<T>
    {
        #region 定数
        /// <summary>
        /// 数値 0 を表します。
        /// </summary>
        T Zero { get; }
        /// <summary>
        /// 数値 1 を表します。
        /// </summary>
        T One { get; }
        /// <summary>
        /// 負の 1 (-1) を表します。
        /// </summary>
        T MinusOne { get; }
        #endregion 定数
        #region プロパティ
        /// <summary>
        /// 符号 (負、正、または 0) を示す数値を取得します。
        /// -1 : 値は負です。
        /// 0 : 値は 0 (ゼロ) です。
        /// 1 : 値は正です。
        /// </summary>
        int Sign { get; }
        /// <summary>
        /// Zero かどうかを示します。
        /// Zero の場合は true。それ以外の場合は false。
        /// </summary>
        bool IsZero { get; }
        /// <summary>
        /// One かどうかを示します。
        /// One の場合は true。それ以外の場合は false。
        /// </summary>
        bool IsOne { get; }
        #endregion プロパティ
        #region メソッド
        /// <summary>
        /// 絶対値を取得します。
        /// </summary>
        /// <returns>絶対値。</returns>
        T Abs();
        /// <summary>
        /// 加算し、その結果を返します。
        /// </summary>
        /// <param name="addend">加数。</param>
        /// <returns>合計。</returns>
        T Add(T addend);
        /// <summary>
        /// 指定した数以上の数のうち、最小の整数値を返します。
        /// </summary>
        /// <returns>以上の最小の整数値。 </returns>
        T Ceiling();
        /// <summary>
        /// 2 つの値を比較し、小さいか、同じか、または大きいかを示す整数を返します。
        /// </summary>
        /// <returns>0 より小さい : other より小さい。
        //  0 : other と等しい。
        //  0 より大きい :  other より大きい。</returns>
        int Compare(T other);
        /// <summary>
        /// 除算し、その結果を返します。
        /// </summary>
        /// <param name="divisor">除数。</param>
        /// <returns>除算の商。</returns>
        /// <exception cref="System.DivideByZeroException">divisor が 0 (ゼロ) です。</exception>
        T Divide(T divisor);
        /// <summary>
        /// 除算し、その結果を返します。剰余は出力パラメーターとして返されます。
        /// </summary>
        /// <param name="divisor">除数。</param>
        /// <param name="remainder">このメソッドから制御が戻るときに、除算の剰余を表す System.Numerics.BigInteger 値が格納されます。 このパラメーターは初期化せずに渡されます。</param>
        /// <returns>除算の商。</returns>
        /// <exception cref="System.DivideByZeroException">divisor が 0 (ゼロ) です。</exception>
        T DivRem(T divisor, out T remainder);
        /// <summary>
        /// 指定した数以下の数のうち、最大の整数値を返します。
        /// </summary>
        /// <returns>以下の最大の整数値。</returns>
        T Floor();
        /// <summary>
        /// 指定した数値の指定した底での対数を返します。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue">対数の底。</param>
        /// <returns>baseValue を底とする value の対数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">対数が、System.Double データ型の範囲外です。</exception>
        double Log(double baseValue);
        /// <summary>
        /// 指定した数の自然 (底 e) 対数を返します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns>自然対数 (e を底とする対数)。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">自然対数が、System.Double データ型の範囲外です。</exception>
        double Log();
        /// <summary>
        /// 底 10 の対数を返します。
        /// </summary>
        /// <returns>10 を底とする value の対数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">対数が、System.Double データ型の範囲外です。</exception>
        double Log10();
        /// <summary>
        /// 2 つの値のうち 大きい方の値を返します。
        /// </summary>
        /// <param name="other">比較する 2 番目の値です。</param>
        /// <returns>いずれか大きい方。</returns>
        T Max(T other);
        /// <summary>
        /// 2 つの値のうち小さい方の値を返します。
        /// </summary>
        /// <param name="other">比較する 2 番目の値です。</param>
        /// <returns>いずれか小さい方。</returns>
        T Min(T other);
#if false// 一旦作らない
        /// <summary>
        /// ある数値を別の数値で累乗し、それをさらに別の数値で割った結果生じた剰余を求めます。
        /// </summary>
        /// <param name="exponent">指数。</param>
        /// <param name="modulus">累乗した exponent の除算に使用する除数。</param>
        /// <returns>指数 を modulus で割った結果生じた剰余。</returns>
        /// <exception cref="System.DivideByZeroException">modulus が 0 です。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">exponent が負の値です。</exception>
        T ModPow(T exponent, T modulus);
#endif
        /// <summary>
        /// 積を返します。
        /// </summary>
        /// <param name="multiplier">乗算対象の 2 番目の数。</param>
        /// <returns>積。</returns>
        T Multiply(T multiplier);
        /// <summary>
        /// 否定 (負数化) します。
        /// </summary>
        /// <returns>-1 を乗算した結果。</returns>
        T Negate();
        /// <summary>
        /// 指定された値を指数として累乗します。
        /// </summary>
        /// <param name="exponent">指数。</param>
        /// <returns>累乗した結果。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">exponent が負の値です。</exception>
        T Pow(int exponent);
        /// <summary>
        /// 整数除算を実行し、その剰余を返します。
        /// </summary>
        /// <param name="divisor">除数。</param>
        /// <returns>divisor で除算した結果生じた剰余。</returns>
        /// <exception cref="System.DivideByZeroException">divisor が 0 (ゼロ) です。</exception>
        T Remainder(T divisor);
        /// <summary>
        /// 指定された数値の平方根を返します。
        /// </summary>
        /// <returns>戻り値 0 または正 d の正の平方根。</returns>
        T Sqrt();
        /// <summary>
        /// 減算し、その結果を返します。
        /// </summary>
        /// <param name="substrahend">減算する値 (減数)。</param>
        /// <returns>減算した結果。</returns>
        T Subtract(T substrahend);
        /// <summary>
        /// 整数部を計算します。
        /// </summary>
        /// <returns>整数部。つまり、小数部の桁を破棄した後に残る数値。</returns>
        T Truncate();
        #endregion メソッド
    }
}