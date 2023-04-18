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
        public static IntFraction One => throw new NotImplementedException();

        public static int Radix => throw new NotImplementedException();

        public static IntFraction Zero => throw new NotImplementedException();

        public static IntFraction AdditiveIdentity => throw new NotImplementedException();

        public static IntFraction MultiplicativeIdentity => throw new NotImplementedException();
        /// <summary>
        /// 分子
        /// </summary>
        public int Numerator { get ; set ; }
        /// <summary>
        /// 分母
        /// </summary>
        public int Denominator { get; set; }

        public static IntFraction Abs(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegativeInfinity(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositiveInfinity(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(IntFraction value)
        {
            throw new NotImplementedException();
        }

        public static IntFraction MaxMagnitude(IntFraction x, IntFraction y)
        {
            throw new NotImplementedException();
        }

        public static IntFraction MaxMagnitudeNumber(IntFraction x, IntFraction y)
        {
            throw new NotImplementedException();
        }

        public static IntFraction MinMagnitude(IntFraction x, IntFraction y)
        {
            throw new NotImplementedException();
        }

        public static IntFraction MinMagnitudeNumber(IntFraction x, IntFraction y)
        {
            throw new NotImplementedException();
        }

        public static IntFraction Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static IntFraction Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static IntFraction Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static IntFraction Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntFraction result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<IntFraction>.TryConvertFromChecked<TOther>(TOther value, out IntFraction result)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}
