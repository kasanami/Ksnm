using System;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 分数
    /// </summary>
    public struct Fraction32 : IComparable, IComparable<Fraction32>, IEquatable<Fraction32>
    {
        /// <summary>
        /// 分子
        /// </summary>
        public int Numerator { get; private set; }
        /// <summary>
        /// 分母
        /// </summary>
        public int Denominator { get; private set; }
        /// <summary>
        /// 分子と分母を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        public Fraction32(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        /// <summary>
        /// 分子を指定して初期化
        /// </summary>
        /// <param name="numerator">分子</param>
        public Fraction32(int numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }

        #region IComparable
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="obj">比較するオブジェクト</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(object obj)
        {
            return CompareTo((Fraction32)obj);
        }
        /// <summary>
        /// 比較し、これらの相対値を示す値を返します。
        /// </summary>
        /// <param name="other">比較対象</param>
        /// <returns>0の場合等価です。0 より大きい値の場合 obj よりも大きいです。0 より小さい値の場合 obj よりも小さいです。</returns>
        public int CompareTo(Fraction32 other)
        {
            other.Numerator *= Denominator;
            return (Numerator * other.Denominator).CompareTo(other.Numerator);
        }
        #endregion IComparable

        #region IEquatable
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。</returns>
        public bool Equals(Fraction32 other)
        {
            return CompareTo(other) == 0;
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
            if (obj is Fraction32)
            {
                return Equals((Fraction32)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
        #endregion object
    }
}
