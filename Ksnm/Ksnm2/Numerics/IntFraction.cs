using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm2.Numerics
{
    /// <summary>
    /// 整数の分数
    /// </summary>
    public struct IntFraction : IFraction<IntFraction, int, int>, INumber<IntFraction>
    {
        public static IntFraction One => 1;

        public static int Radix => 2;

        public static IntFraction Zero => 0;

        public static IntFraction AdditiveIdentity => 0;

        public static IntFraction MultiplicativeIdentity => 1;
        /// <summary>
        /// 分子
        /// </summary>
        public int Numerator { get ; set ; }
        /// <summary>
        /// 分母
        /// </summary>
        public int Denominator { get; set; }

        public IntFraction(int numerator,int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public IntFraction(int numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }

        public static IntFraction Abs(IntFraction value)
        {
            return new IntFraction(int.Abs(value.Numerator), int.Abs(value.Denominator));
        }

        public static bool IsCanonical(IntFraction value)
        {
            return true;
        }

        public static bool IsComplexNumber(IntFraction value)
        {
            return false;
        }

        public static bool IsEvenInteger(IntFraction value)
        {
            if (IsInteger(value))
            {
                return int.IsEvenInteger(value.Numerator);
            }
            return false;
        }
        /// <summary>
        /// 有限値ならtrueを返す。
        /// </summary>
        public static bool IsFinite(IntFraction value)
        {
            return true;
        }

        public static bool IsImaginaryNumber(IntFraction value)
        {
            return false;
        }

        public static bool IsInfinity(IntFraction value)
        {
            return false;
        }

        public static bool IsInteger(IntFraction value)
        {
            return value.Denominator == 1;
        }

        public static bool IsNaN(IntFraction value)
        {
            return value.Denominator == 0;
        }

        public static bool IsNegative(IntFraction value)
        {
            return int.IsNegative(value.Numerator);
        }

        public static bool IsNegativeInfinity(IntFraction value)
        {
            return false;
        }

        public static bool IsNormal(IntFraction value)
        {
            return true;
        }

        public static bool IsOddInteger(IntFraction value)
        {
            if (IsInteger(value))
            {
                return int.IsOddInteger(value.Numerator);
            }
            return false;
        }

        public static bool IsPositive(IntFraction value)
        {
            return int.IsPositive(value.Numerator);
        }

        public static bool IsPositiveInfinity(IntFraction value)
        {
            return false;
        }

        public static bool IsRealNumber(IntFraction value)
        {
            return true;
        }

        public static bool IsSubnormal(IntFraction value)
        {
            return true;
        }

        public static bool IsZero(IntFraction value)
        {
            return value.Numerator == 0;
        }

        public static IntFraction MaxMagnitude(IntFraction x, IntFraction y)
        {
            long xNumerator = x.Numerator * y.Denominator;
            long yNumerator = y.Numerator * x.Denominator;
            if (xNumerator > yNumerator)
            {
                return x;
            }
            return y;
        }

        public static IntFraction MaxMagnitudeNumber(IntFraction x, IntFraction y)
        {
            return MaxMagnitude(x, y);
        }

        public static IntFraction MinMagnitude(IntFraction x, IntFraction y)
        {
            long xNumerator = x.Numerator * y.Denominator;
            long yNumerator = y.Numerator * x.Denominator;
            if (xNumerator < yNumerator)
            {
                return x;
            }
            return y;
        }

        public static IntFraction MinMagnitudeNumber(IntFraction x, IntFraction y)
        {
            return MinMagnitude(x, y);
        }

        public static IntFraction Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(int.Parse(s, style, provider));
            }
            else
            {
                return new(
                    int.Parse(s[..slashIndex], style, provider),
                    int.Parse(s[(slashIndex + 1)..], style, provider));
            }
        }

        public static IntFraction Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), style, provider);
        }

        public static IntFraction Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            var slashIndex = s.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(int.Parse(s, provider));
            }
            else
            {
                return new(
                    int.Parse(s[..slashIndex], provider),
                    int.Parse(s[(slashIndex + 1)..], provider));
            }
        }

        public static IntFraction Parse(string s, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), provider);
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (int.TryParse(s, style, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                int.TryParse(s[..slashIndex], style, provider, out var numerator) &&
                int.TryParse(s[(slashIndex + 1)..], style, provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            return TryParse(s.AsSpan(),style,provider,out result);
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            var slashIndex = s.IndexOf('/');

            if (slashIndex == -1)
            {
                if (int.TryParse(s, provider, out var numerator))
                {
                    result = new(numerator);
                    return true;
                }
            }
            else if (
                int.TryParse(s[..slashIndex], provider, out var numerator) &&
                int.TryParse(s[(slashIndex + 1)..], provider, out var denominator))
            {
                result = new(numerator, denominator);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            return TryParse(s.AsSpan(), provider, out result);
        }

        static bool INumberBase<IntFraction>.TryConvertFromChecked<TOther>(TOther value, out IntFraction result)
        {
            if (TOther.TryConvertToChecked<int>(value, out var _result))
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

        static bool INumberBase<IntFraction>.TryConvertFromSaturating<TOther>(TOther value, out IntFraction result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<IntFraction>.TryConvertFromTruncating<TOther>(TOther value, out IntFraction result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<IntFraction>.TryConvertToChecked<TOther>(IntFraction value, out TOther result)
        {
            int.
            if (int.TryConvertToSaturating(value.Numerator, out TOther numerator) &&
                int.TryConvertToSaturating(value.Denominator, out TOther denominator))
            {
                return TOther.TryConvertFromChecked(numer / denom, out result);
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool INumberBase<IntFraction>.TryConvertToSaturating<TOther>(IntFraction value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<IntFraction>.TryConvertToTruncating<TOther>(IntFraction value, out TOther result)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IntFraction? other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IntFraction other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IntFraction? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IntFraction other)
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

        public static IntFraction operator +(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator +(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator -(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator -(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator ++(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator --(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator *(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator /(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static IntFraction operator %(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(IntFraction? left, IntFraction? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(IntFraction? left, IntFraction? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(IntFraction left, IntFraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(IntFraction left, IntFraction right)
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

        #region 型変換
        /// <summary>
        /// 暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator IntFraction(int value)
        {
            return new IntFraction(value);
        }
        #endregion 型変換
    }
}
