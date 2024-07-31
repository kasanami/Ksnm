using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 複素数型
    /// </summary>
    public struct Complex : IEquatable<Complex>
    {
        #region プロパティ
        /// <summary>
        /// 実数部
        /// </summary>
        public double Real;
        /// <summary>
        /// 虚数部
        /// </summary>
        public double Imaginary;
        /// <summary>
        /// 絶対値
        /// </summary>
        public double Magnitude
        {
            get { return System.Math.Sqrt(Real * Real + Imaginary * Imaginary); }
        }
        /// <summary>
        /// 偏角[ラジアン]
        /// </summary>
        public double Phase
        {
            get { return System.Math.Atan2(Imaginary, Real); }
        }
        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        #region 二項演算子
        /// <summary>
        /// 加算
        /// </summary>
        public static Complex operator +(in Complex valueL, in Complex valueR)
        {
            return new Complex(valueL.Real + valueR.Real, valueL.Imaginary + valueR.Imaginary);
        }
        /// <summary>
        /// 減算
        /// </summary>
        public static Complex operator -(in Complex valueL, in Complex valueR)
        {
            return new Complex(valueL.Real - valueR.Real, valueL.Imaginary - valueR.Imaginary);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Complex operator *(in Complex valueL, in Complex valueR)
        {
            return new Complex(
                valueL.Real * valueR.Real - valueL.Imaginary * valueR.Imaginary,
                valueR.Real * valueL.Imaginary + valueL.Real * valueR.Imaginary);
        }
        /// <summary>
        /// 除算
        /// </summary>
        public static Complex operator /(in Complex valueL, in Complex valueR)
        {
            var a = valueL.Real;
            var b = valueL.Imaginary;
            var c = valueR.Real;
            var d = valueR.Imaginary;
            var divisor = (c * c + d * d);
            return new Complex(
                (a * c + b * d) / divisor,
                (b * c - a * d) / divisor);
        }
        #endregion 2項演算子

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
            if (obj is Complex)
            {
                return Equals((Complex)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return $"({Real}, {Imaginary})";
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// 現在のインスタンスの値と指定した複素数の値が等しいかどうかを示す値を返します。
        /// </summary>
        /// <param name="other">比較対象の複素数。</param>
        /// <returns></returns>
        public bool Equals(Complex other)
        {
            if (Real != other.Real)
            {
                return false;
            }
            if (Imaginary != other.Imaginary)
            {
                return false;
            }
            return true;
        }
        #endregion IEquatable
    }
}