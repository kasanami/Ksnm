/*
The zlib License

Copyright (c) 2022 Takahiro Kasanami

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

namespace Ksnm.Numerics
{
    /// <summary>
    /// 虚数
    /// </summary>
    public struct Imaginary : IEquatable<Imaginary>
    {
        #region プロパティ
        /// <summary>
        /// 値
        /// </summary>
        public double Value;
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 指定した値で初期化する
        /// </summary>
        public Imaginary(double value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Imaginary operator +(in Imaginary valueL, in Imaginary valueR)
        {
            return new Imaginary(valueL.Value + valueR.Value);
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Imaginary operator -(in Imaginary valueL, in Imaginary valueR)
        {
            return new Imaginary(valueL.Value - valueR.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static double operator *(in Imaginary valueL, in Imaginary valueR)
        {
            return -(valueL.Value * valueR.Value);
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static double operator /(in Imaginary valueL, in Imaginary valueR)
        {
            return (valueL.Value * valueR.Value) / (valueR.Value * valueR.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Imaginary operator *(in Imaginary valueL, in double valueR)
        {
            return new Imaginary(valueL.Value * valueR);
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Imaginary operator /(in Imaginary valueL, in double valueR)
        {
            return new Imaginary(valueL.Value / valueR);
        }
        #endregion 2項演算子


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
            if (obj is Imaginary)
            {
                return Equals((Imaginary)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return $"{Value}i";
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// 現在のインスタンスの値と指定した値が等しいかどうかを示す値を返します。
        /// </summary>
        public bool Equals(Imaginary other)
        {
            return Value == other.Value;
        }
        #endregion IEquatable
    }
}