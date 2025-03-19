using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    using BitsType = Int16;
    /// <summary>
    /// 10進数の固定小数点数型
    /// ・全体のビット数:16
    /// ・小数点以下10進数で3桁
    /// </summary>
    public struct FixedPointDecimal16Q3 :
        INumberBase<FixedPointDecimal16Q3>,
        IMinMaxValue<FixedPointDecimal16Q3>,
        ISignedNumber<FixedPointDecimal16Q3>, IEquatable<FixedPointDecimal16Q3>
    {
        BitsType _bits;

        public static FixedPointDecimal16Q3 One => new(1000);

        public static int Radix => 2;

        public static FixedPointDecimal16Q3 Zero => new(0);

        public static FixedPointDecimal16Q3 AdditiveIdentity => Zero;

        public static FixedPointDecimal16Q3 MultiplicativeIdentity => One;

        public static FixedPointDecimal16Q3 MaxValue => new(BitsType.MaxValue);

        public static FixedPointDecimal16Q3 MinValue => new(BitsType.MinValue);

        public static FixedPointDecimal16Q3 NegativeOne => new(-1000);

        public FixedPointDecimal16Q3() { }

        public FixedPointDecimal16Q3(BitsType bits)
        {
            _bits = bits;
        }

        public FixedPointDecimal16Q3(decimal value)
        {
            if (value < -65.536m)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            else if (value > 65.535m)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            _bits = (BitsType)(value * 1000);
        }

        public double ToDouble()
        {
            return _bits / 1000.0;
        }
        public decimal ToDecimal()
        {
            return _bits / 1000m;
        }

        public override string ToString()
        {
            return $"{ToDecimal()}";
        }

        public static FixedPointDecimal16Q3 Abs(FixedPointDecimal16Q3 value)
            => new(BitsType.Abs(value._bits));

        public static bool IsCanonical(FixedPointDecimal16Q3 value) => false;

        public static bool IsComplexNumber(FixedPointDecimal16Q3 value) => false;

        public static bool IsEvenInteger(FixedPointDecimal16Q3 value)
            => BitsType.IsEvenInteger(value._bits);

        public static bool IsFinite(FixedPointDecimal16Q3 value) => false;

        public static bool IsImaginaryNumber(FixedPointDecimal16Q3 value) => false;

        public static bool IsInfinity(FixedPointDecimal16Q3 value) => false;

        public static bool IsInteger(FixedPointDecimal16Q3 value)
        {
            return (value._bits % 1000 == 0);
        }

        public static bool IsNaN(FixedPointDecimal16Q3 value) => false;

        public static bool IsNegative(FixedPointDecimal16Q3 value)
            => BitsType.IsNegative(value._bits);

        public static bool IsNegativeInfinity(FixedPointDecimal16Q3 value) => false;

        public static bool IsNormal(FixedPointDecimal16Q3 value) => false;

        public static bool IsOddInteger(FixedPointDecimal16Q3 value)
            => BitsType.IsOddInteger(value._bits);

        public static bool IsPositive(FixedPointDecimal16Q3 value)
            => BitsType.IsPositive(value._bits);

        public static bool IsPositiveInfinity(FixedPointDecimal16Q3 value) => false;

        public static bool IsRealNumber(FixedPointDecimal16Q3 value) => true;
        public static bool IsSubnormal(FixedPointDecimal16Q3 value) => false;

        public static bool IsZero(FixedPointDecimal16Q3 value) => value._bits == 0;

        public static FixedPointDecimal16Q3 MaxMagnitude(FixedPointDecimal16Q3 x, FixedPointDecimal16Q3 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 MaxMagnitudeNumber(FixedPointDecimal16Q3 x, FixedPointDecimal16Q3 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 MinMagnitude(FixedPointDecimal16Q3 x, FixedPointDecimal16Q3 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 MinMagnitudeNumber(FixedPointDecimal16Q3 x, FixedPointDecimal16Q3 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(FixedPointDecimal16Q3 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(FixedPointDecimal16Q3 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(FixedPointDecimal16Q3 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(FixedPointDecimal16Q3 other) => _bits == other._bits;

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ToDecimal().TryFormat(destination, out charsWritten, format, provider);
        public string ToString(string? format, IFormatProvider? formatProvider)
            => ToDecimal().ToString(format, formatProvider);

        public static FixedPointDecimal16Q3 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal16Q3 result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal16Q3 operator +(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
            => new((BitsType)(left._bits + right._bits));

        public static FixedPointDecimal16Q3 operator --(FixedPointDecimal16Q3 value)
            => value - One;

        public static FixedPointDecimal16Q3 operator /(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
        {
            int temp = (left._bits * 1000) / (right._bits);
            return new((BitsType)temp);
        }

        public static bool operator ==(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
            => left.Equals(right);

        public static bool operator !=(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
            => !left.Equals(right);

        public static FixedPointDecimal16Q3 operator ++(FixedPointDecimal16Q3 value)
            => value + One;

        public static FixedPointDecimal16Q3 operator *(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
        {
            int temp = left._bits * right._bits;
            temp /= 1000;
            return new((BitsType)temp);
        }

        public static FixedPointDecimal16Q3 operator -(FixedPointDecimal16Q3 left, FixedPointDecimal16Q3 right)
            => new((BitsType)(left._bits - right._bits));

        public static FixedPointDecimal16Q3 operator -(FixedPointDecimal16Q3 value)
            => new((BitsType)(-value._bits));

        public static FixedPointDecimal16Q3 operator +(FixedPointDecimal16Q3 value)
            => value;

        public override bool Equals(object? obj)
        {
            return obj is FixedPointDecimal16Q3 && Equals((FixedPointDecimal16Q3)obj);
        }
    }
}