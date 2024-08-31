using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 分数型
    /// </summary>
    public struct Fraction<T> :
        IFraction<Fraction<T>, T, T>,
        INumber<Fraction<T>>
        where T : INumber<T>
    {
        #region 定数・静的変数
        public static Fraction<T> One => 1;

        public static int Radix => 2;

        public static Fraction<T> Zero => 0;

        public static Fraction<T> AdditiveIdentity => 0;

        public static Fraction<T> MultiplicativeIdentity => 1;
        #endregion 定数・静的変数

        #region プロパティ
        /// <summary>
        /// 分子
        /// </summary>
        public T Numerator { get; set; }
        /// <summary>
        /// 分母
        /// </summary>
        public T Denominator { get; set; }
        #endregion プロパティ

        #region コンストラクタ
        public Fraction(T numerator, T denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public Fraction(T numerator)
        {
            Numerator = numerator;
            Denominator = T.One;
        }
        #endregion コンストラクタ

        public static Fraction<T> Abs(Fraction<T> value)
        {
            return new Fraction<T>(T.Abs(value.Numerator), T.Abs(value.Denominator));
        }

        public static bool IsCanonical(Fraction<T> value)
        {
            return true;
        }

        public static bool IsComplexNumber(Fraction<T> value)
        {
            return false;
        }

        public static bool IsEvenInteger(Fraction<T> value)
        {
            if (IsInteger(value))
            {
                return T.IsEvenInteger(value.Numerator);
            }
            return false;
        }
        /// <summary>
        /// 有限値ならtrueを返す。
        /// </summary>
        public static bool IsFinite(Fraction<T> value)
        {
            return true;
        }

        public static bool IsImaginaryNumber(Fraction<T> value)
        {
            return false;
        }

        public static bool IsInfinity(Fraction<T> value)
        {
            return false;
        }

        public static bool IsInteger(Fraction<T> value)
        {
            return value.Denominator == T.One;
        }

        public static bool IsNaN(Fraction<T> value)
        {
            return value.Denominator == T.Zero;
        }

        public static bool IsNegative(Fraction<T> value)
        {
            return T.IsNegative(value.Numerator);
        }

        public static bool IsNegativeInfinity(Fraction<T> value)
        {
            return false;
        }

        public static bool IsNormal(Fraction<T> value)
        {
            return true;
        }

        public static bool IsOddInteger(Fraction<T> value)
        {
            if (IsInteger(value))
            {
                return T.IsOddInteger(value.Numerator);
            }
            return false;
        }

        public static bool IsPositive(Fraction<T> value)
        {
            return T.IsPositive(value.Numerator);
        }

        public static bool IsPositiveInfinity(Fraction<T> value)
        {
            return false;
        }

        public static bool IsRealNumber(Fraction<T> value)
        {
            return true;
        }

        public static bool IsSubnormal(Fraction<T> value)
        {
            return true;
        }

        public static bool IsZero(Fraction<T> value)
        {
            return value.Numerator == T.Zero;
        }

        public static Fraction<T> MaxMagnitude(Fraction<T> x, Fraction<T> y)
        {
            T xNumerator = x.Numerator * y.Denominator;
            T yNumerator = y.Numerator * x.Denominator;
            if (xNumerator > yNumerator)
            {
                return x;
            }
            return y;
        }

        public static Fraction<T> MaxMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            return MaxMagnitude(x, y);
        }

        public static Fraction<T> MinMagnitude(Fraction<T> x, Fraction<T> y)
        {
            T xNumerator = x.Numerator * y.Denominator;
            T yNumerator = y.Numerator * x.Denominator;
            if (xNumerator < yNumerator)
            {
                return x;
            }
            return y;
        }

        public static Fraction<T> MinMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            return MinMagnitude(x, y);
        }

        public static Fraction<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(T.Parse(s, style, provider));
            }
            else
            {
                return new(
                    T.Parse(s[..slashIndex], style, provider),
                    T.Parse(s[(slashIndex + 1)..], style, provider));
            }
        }

        public static Fraction<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), style, provider);
        }

        public static Fraction<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(T.Parse(s, provider));
            }
            else
            {
                return new(
                    T.Parse(s[..slashIndex], provider),
                    T.Parse(s[(slashIndex + 1)..], provider));
            }
        }

        public static Fraction<T> Parse(string s, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), provider);
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (T.TryParse(s, style, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                T.TryParse(s[..slashIndex], style, provider, out var numerator) &&
                T.TryParse(s[(slashIndex + 1)..], style, provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            return TryParse(s.AsSpan(), style, provider, out result);
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (T.TryParse(s, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                T.TryParse(s[..slashIndex], provider, out var numerator) &&
                T.TryParse(s[(slashIndex + 1)..], provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction<T> result)
        {
            return TryParse(s.AsSpan(), provider, out result);
        }

        static bool TryConvertFromChecked<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            if (TOther.TryConvertToChecked<T>(value, out var _result))
            {
                result = new(_result);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryConvertFromSaturating<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        static bool TryConvertFromTruncating<TOther>(TOther value, out Fraction<T> result)
            where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        static bool TryConvertToChecked<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        static bool TryConvertToSaturating<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        static bool TryConvertToTruncating<TOther>(Fraction<T> value, out TOther result)
            where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Fraction<T>? other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Fraction<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Fraction<T>? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Fraction<T> other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator +(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator +(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator -(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator -(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator ++(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator --(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator *(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator /(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static Fraction<T> operator %(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Fraction<T>? left, Fraction<T>? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Fraction<T>? left, Fraction<T>? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        int IComparable.CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        int IComparable<Fraction<T>>.CompareTo(Fraction<T> other)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.Abs(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsCanonical(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsComplexNumber(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsEvenInteger(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsFinite(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsImaginaryNumber(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsInfinity(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsInteger(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsNaN(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsNegative(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsNegativeInfinity(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsNormal(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsOddInteger(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsPositive(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsPositiveInfinity(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsRealNumber(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsSubnormal(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.IsZero(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.MaxMagnitude(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.MaxMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.MinMagnitude(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.MinMagnitudeNumber(Fraction<T> x, Fraction<T> y)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> INumberBase<Fraction<T>>.Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromChecked<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromSaturating<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertFromTruncating<TOther>(TOther value, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToChecked<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToSaturating<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<Fraction<T>>.TryConvertToTruncating<TOther>(Fraction<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<Fraction<T>>.Equals(Fraction<T> other)
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

        static Fraction<T> ISpanParsable<Fraction<T>>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool ISpanParsable<Fraction<T>>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IParsable<Fraction<T>>.Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool IParsable<Fraction<T>>.TryParse(string? s, IFormatProvider? provider, out Fraction<T> result)
        {
            throw new NotImplementedException();
        }

        #region 型変換
        /// <summary>
        /// 暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Fraction<T>(T value)
        {
            return new Fraction<T>(value);
        }
        public static implicit operator Fraction<T>(int value) => T.CreateChecked(value);
        #endregion 型変換

        #region オペレーター

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator >(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator >=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator <(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IComparisonOperators<Fraction<T>, Fraction<T>, bool>.operator <=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IModulusOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator %(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IAdditionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator +(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IDecrementOperators<Fraction<T>>.operator --(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IDivisionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator /(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<Fraction<T>, Fraction<T>, bool>.operator ==(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<Fraction<T>, Fraction<T>, bool>.operator !=(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IIncrementOperators<Fraction<T>>.operator ++(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IMultiplyOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator *(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> ISubtractionOperators<Fraction<T>, Fraction<T>, Fraction<T>>.operator -(Fraction<T> left, Fraction<T> right)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IUnaryNegationOperators<Fraction<T>, Fraction<T>>.operator -(Fraction<T> value)
        {
            throw new NotImplementedException();
        }

        static Fraction<T> IUnaryPlusOperators<Fraction<T>, Fraction<T>>.operator +(Fraction<T> value)
        {
            throw new NotImplementedException();
        }
        #endregion オペレーター
    }
}
