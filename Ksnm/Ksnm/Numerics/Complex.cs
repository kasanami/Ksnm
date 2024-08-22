using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 複素数型
    /// </summary>
    public struct Complex<T> :
        INumber<Complex<T>>
        where T : INumber<T>,
        IRootFunctions<T>,
        IFloatingPointIeee754<T>
    {
        #region プロパティ
        /// <summary>
        /// 実数部
        /// </summary>
        public T Real;
        /// <summary>
        /// 虚数部
        /// </summary>
        public T Imaginary;
        /// <summary>
        /// 絶対値
        /// </summary>
        public T Magnitude=> T.Sqrt(Real * Real + Imaginary * Imaginary);
        /// <summary>
        /// 偏角[ラジアン]
        /// </summary>
        public T Phase=> T.Atan2(Imaginary, Real);

        public static Complex<T> One => new Complex<T>(T.One, T.Zero);

        public static Complex<T> ImaginaryOne = new Complex<T>(T.Zero, T.One);

        public static int Radix => T.Radix;

        public static Complex<T> Zero => new Complex<T>(T.Zero, T.Zero);

        public static Complex<T> AdditiveIdentity => Zero;

        public static Complex<T> MultiplicativeIdentity => One;
        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="real">実部</param>
        /// <param name="imaginary">虚部</param>
        public Complex(T real, T imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        #region Operators 演算子
        public static bool operator >(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator %(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator +(Complex<T> left, Complex<T> right)
        {
            return new Complex<T>(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }

        public static Complex<T> operator --(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator /(Complex<T> left, Complex<T> right)
        {
            var a = left.Real;
            var b = left.Imaginary;
            var c = right.Real;
            var d = right.Imaginary;
            var divisor = (c * c + d * d);
            return new Complex<T>(
                (a * c + b * d) / divisor,
                (b * c - a * d) / divisor);
        }

        public static bool operator ==(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator ++(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator *(Complex<T> left, Complex<T> right)
        {
            return new Complex<T>(
                left.Real * right.Real - left.Imaginary * right.Imaginary,
                right.Real * left.Imaginary + left.Real * right.Imaginary);
        }

        public static Complex<T> operator -(Complex<T> left, Complex<T> right)
        {
            return new Complex<T>(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }

        public static Complex<T> operator -(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> operator +(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Complex<T>, Complex<T>, bool>.operator >(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Complex<T>, Complex<T>, bool>.operator >=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Complex<T>, Complex<T>, bool>.operator <(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Complex<T>, Complex<T>, bool>.operator <=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IModulusOperators<Complex<T>, Complex<T>, Complex<T>>.operator %(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IAdditionOperators<Complex<T>, Complex<T>, Complex<T>>.operator +(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IDecrementOperators<Complex<T>>.operator --(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IDivisionOperators<Complex<T>, Complex<T>, Complex<T>>.operator /(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<Complex<T>, Complex<T>, bool>.operator ==(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<Complex<T>, Complex<T>, bool>.operator !=(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IIncrementOperators<Complex<T>>.operator ++(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IMultiplyOperators<Complex<T>, Complex<T>, Complex<T>>.operator *(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> ISubtractionOperators<Complex<T>, Complex<T>, Complex<T>>.operator -(Complex<T> left, Complex<T> right)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IUnaryNegationOperators<Complex<T>, Complex<T>>.operator -(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IUnaryPlusOperators<Complex<T>, Complex<T>>.operator +(Complex<T> value)
        {
            throw new NotImplementedException();
        }
        #endregion Operators 演算子

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
        public bool Equals(Complex<T> other)
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


        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Complex<T> other)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> Abs(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegativeInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositiveInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> MaxMagnitude(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> MaxMagnitudeNumber(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> MinMagnitude(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> MinMagnitudeNumber(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        public static Complex<T> Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        int IComparable.CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        int IComparable<Complex<T>>.CompareTo(Complex<T> other)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.Abs(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsCanonical(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsComplexNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsEvenInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsFinite(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsImaginaryNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsNaN(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsNegative(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsNegativeInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsNormal(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsOddInteger(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsPositive(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsPositiveInfinity(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsRealNumber(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsSubnormal(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.IsZero(Complex<T> value)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.MaxMagnitude(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.MaxMagnitudeNumber(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.MinMagnitude(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.MinMagnitudeNumber(Complex<T> x, Complex<T> y)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static Complex<T> INumberBase<Complex<T>>.Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertFromChecked<TOther>(TOther value, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertFromSaturating<TOther>(TOther value, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertFromTruncating<TOther>(TOther value, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertToChecked<TOther>(Complex<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertToSaturating<TOther>(Complex<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryConvertToTruncating<TOther>(Complex<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Complex<T>>.TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<Complex<T>>.Equals(Complex<T> other)
        {
            throw new NotImplementedException();
        }

        bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        static Complex<T> ISpanParsable<Complex<T>>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool ISpanParsable<Complex<T>>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Complex<T> result)
        {
            throw new NotImplementedException();
        }

        static Complex<T> IParsable<Complex<T>>.Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool IParsable<Complex<T>>.TryParse(string? s, IFormatProvider? provider, out Complex<T> result)
        {
            throw new NotImplementedException();
        }
    }
}