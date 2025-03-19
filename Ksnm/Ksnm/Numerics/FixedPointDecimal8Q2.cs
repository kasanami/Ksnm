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
    using BitsType = SByte;
    /// <summary>
    /// 10進数の固定小数点数型
    /// ・全体のビット数:8
    /// ・小数点以下10進数で2桁
    /// </summary>
    public struct FixedPointDecimal8Q2 :
        INumberBase<FixedPointDecimal8Q2>,
        IMinMaxValue<FixedPointDecimal8Q2>,
        ISignedNumber<FixedPointDecimal8Q2>, IEquatable<FixedPointDecimal8Q2>
    {
        BitsType _bits;

        public static FixedPointDecimal8Q2 One => new(100);

        public static int Radix => 2;

        public static FixedPointDecimal8Q2 Zero => new(0);

        public static FixedPointDecimal8Q2 AdditiveIdentity => Zero;

        public static FixedPointDecimal8Q2 MultiplicativeIdentity => One;

        public static FixedPointDecimal8Q2 MaxValue => new(BitsType.MaxValue);

        public static FixedPointDecimal8Q2 MinValue => new(BitsType.MinValue);

        public static FixedPointDecimal8Q2 NegativeOne => new(-100);

        public FixedPointDecimal8Q2() { }

        public FixedPointDecimal8Q2(BitsType bits)
        {
            _bits = bits;
        }

        public FixedPointDecimal8Q2(decimal value)
        {
            if (value < -1.28m)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            else if (value > 1.27m)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            _bits = (BitsType)(value * 100);
        }

        public double ToDouble()
        {
            return _bits / 100.0;
        }
        public decimal ToDecimal()
        {
            return _bits / 100m;
        }

        public override string ToString()
        {
            return $"{ToDecimal()}";
        }

        public static FixedPointDecimal8Q2 Abs(FixedPointDecimal8Q2 value)
            => new(BitsType.Abs(value._bits));

        public static bool IsCanonical(FixedPointDecimal8Q2 value) => false;

        public static bool IsComplexNumber(FixedPointDecimal8Q2 value) => false;

        public static bool IsEvenInteger(FixedPointDecimal8Q2 value)
            => BitsType.IsEvenInteger(value._bits);

        public static bool IsFinite(FixedPointDecimal8Q2 value) => false;

        public static bool IsImaginaryNumber(FixedPointDecimal8Q2 value) => false;

        public static bool IsInfinity(FixedPointDecimal8Q2 value) => false;

        public static bool IsInteger(FixedPointDecimal8Q2 value)
        {
            return (value._bits % 100 == 0);
        }

        public static bool IsNaN(FixedPointDecimal8Q2 value) => false;

        public static bool IsNegative(FixedPointDecimal8Q2 value)
            => BitsType.IsNegative(value._bits);

        public static bool IsNegativeInfinity(FixedPointDecimal8Q2 value) => false;

        public static bool IsNormal(FixedPointDecimal8Q2 value) => false;

        public static bool IsOddInteger(FixedPointDecimal8Q2 value)
            => BitsType.IsOddInteger(value._bits);

        public static bool IsPositive(FixedPointDecimal8Q2 value)
            => BitsType.IsPositive(value._bits);

        public static bool IsPositiveInfinity(FixedPointDecimal8Q2 value) => false;

        public static bool IsRealNumber(FixedPointDecimal8Q2 value) => true;
        public static bool IsSubnormal(FixedPointDecimal8Q2 value) => false;

        public static bool IsZero(FixedPointDecimal8Q2 value) => value._bits == 0;

        public static FixedPointDecimal8Q2 MaxMagnitude(FixedPointDecimal8Q2 x, FixedPointDecimal8Q2 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 MaxMagnitudeNumber(FixedPointDecimal8Q2 x, FixedPointDecimal8Q2 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 MinMagnitude(FixedPointDecimal8Q2 x, FixedPointDecimal8Q2 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 MinMagnitudeNumber(FixedPointDecimal8Q2 x, FixedPointDecimal8Q2 y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(FixedPointDecimal8Q2 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(FixedPointDecimal8Q2 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(FixedPointDecimal8Q2 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(FixedPointDecimal8Q2 other) => _bits == other._bits;

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ToDecimal().TryFormat(destination, out charsWritten, format, provider);
        public string ToString(string? format, IFormatProvider? formatProvider)
            => ToDecimal().ToString(format, formatProvider);

        public static FixedPointDecimal8Q2 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal8Q2 result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal8Q2 operator +(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
            => new((BitsType)(left._bits + right._bits));

        public static FixedPointDecimal8Q2 operator --(FixedPointDecimal8Q2 value)
            => value - One;

        public static FixedPointDecimal8Q2 operator /(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
        {
            int temp = (left._bits * 100) / (right._bits);
            return new((BitsType)temp);
        }

        public static bool operator ==(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
            => left.Equals(right);

        public static bool operator !=(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
            => !left.Equals(right);

        public static FixedPointDecimal8Q2 operator ++(FixedPointDecimal8Q2 value)
            => value + One;

        public static FixedPointDecimal8Q2 operator *(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
        {
            int temp = left._bits * right._bits;
            temp /= 100;
            return new((BitsType)temp);
        }

        public static FixedPointDecimal8Q2 operator -(FixedPointDecimal8Q2 left, FixedPointDecimal8Q2 right)
            => new((BitsType)(left._bits - right._bits));

        public static FixedPointDecimal8Q2 operator -(FixedPointDecimal8Q2 value)
            => new((BitsType)(-value._bits));

        public static FixedPointDecimal8Q2 operator +(FixedPointDecimal8Q2 value)
            => value;

        public override bool Equals(object? obj)
        {
            return obj is FixedPointDecimal8Q2 && Equals((FixedPointDecimal8Q2)obj);
        }
    }
}