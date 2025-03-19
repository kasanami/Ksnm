using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics
{
    using BitsType = Int32;
    using TempType = Int64;
    using FixedPointDecimal = FixedPointDecimal32Q5;
    /// <summary>
    /// 10進数の固定小数点数型
    /// ・全体のビット数:32
    /// ・小数点以下10進数で5桁
    /// </summary>
    public struct FixedPointDecimal32Q5 :
        INumberBase<FixedPointDecimal>,
        IMinMaxValue<FixedPointDecimal>,
        ISignedNumber<FixedPointDecimal>,
        IEquatable<FixedPointDecimal>
    {
        BitsType _bits;

        const BitsType OneBits = 100000;

        public static FixedPointDecimal One => new(OneBits);

        public static int Radix => 2;

        public static FixedPointDecimal Zero => new(0);

        public static FixedPointDecimal AdditiveIdentity => Zero;

        public static FixedPointDecimal MultiplicativeIdentity => One;

        public static FixedPointDecimal MaxValue => new(BitsType.MaxValue);

        public static FixedPointDecimal MinValue => new(BitsType.MinValue);

        public static FixedPointDecimal NegativeOne => new(-OneBits);

        public FixedPointDecimal32Q5() { }

        public FixedPointDecimal32Q5(BitsType bits)
        {
            _bits = bits;
        }

        public FixedPointDecimal32Q5(decimal value)
        {
            decimal minValue = (decimal)MinValue;
            decimal maxValue = (decimal)MaxValue;
            if (value < minValue)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            else if (value > maxValue)
            {
                throw new OverflowException($"{value}は範囲外");
            }
            _bits = (BitsType)(value * OneBits);
        }

        #region 型変換
        public double ToDouble()
        {
            return _bits / (double)OneBits;
        }
        public decimal ToDecimal()
        {
            return _bits / (decimal)OneBits;
        }
        #region 他の型→BrainFloatingPoint16
        public static explicit operator FixedPointDecimal(byte value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(sbyte value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(short value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(ushort value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(int value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(uint value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(long value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(ulong value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(Int128 value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(UInt128 value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(Half value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(float value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(double value) => (FixedPointDecimal)(decimal)value;
        public static explicit operator FixedPointDecimal(decimal value) => new(value);
        #endregion 他の型→BrainFloatingPoint16
        #region BrainFloatingPoint16→他の型
        public static explicit operator byte(FixedPointDecimal value) => (byte)(decimal)value;
        public static explicit operator sbyte(FixedPointDecimal value) => (sbyte)(decimal)value;
        public static explicit operator short(FixedPointDecimal value) => (short)(decimal)value;
        public static explicit operator ushort(FixedPointDecimal value) => (ushort)(decimal)value;
        public static explicit operator int(FixedPointDecimal value) => (int)(decimal)value;
        public static explicit operator uint(FixedPointDecimal value) => (uint)(decimal)value;
        public static explicit operator long(FixedPointDecimal value) => (long)(decimal)value;
        public static explicit operator ulong(FixedPointDecimal value) => (ulong)(decimal)value;
        public static explicit operator Int128(FixedPointDecimal value) => (Int128)(decimal)value;
        public static explicit operator UInt128(FixedPointDecimal value) => (UInt128)(decimal)value;
        public static explicit operator Half(FixedPointDecimal value) => (Half)(decimal)value;
        public static explicit operator float(FixedPointDecimal value) => (float)(decimal)value;
        public static explicit operator double(FixedPointDecimal value) => (double)(decimal)value;
        public static implicit operator decimal(FixedPointDecimal value) => value.ToDecimal();
        #endregion BrainFloatingPoint16→他の型
        #endregion 型変換

        public override string ToString()
        {
            return $"{ToDecimal()}";
        }

        public static FixedPointDecimal Abs(FixedPointDecimal value)
            => new(BitsType.Abs(value._bits));

        public static bool IsCanonical(FixedPointDecimal value) => false;

        public static bool IsComplexNumber(FixedPointDecimal value) => false;

        public static bool IsEvenInteger(FixedPointDecimal value)
            => BitsType.IsEvenInteger(value._bits);

        public static bool IsFinite(FixedPointDecimal value) => false;

        public static bool IsImaginaryNumber(FixedPointDecimal value) => false;

        public static bool IsInfinity(FixedPointDecimal value) => false;

        public static bool IsInteger(FixedPointDecimal value)
        {
            return (value._bits % OneBits == 0);
        }

        public static bool IsNaN(FixedPointDecimal value) => false;

        public static bool IsNegative(FixedPointDecimal value)
            => BitsType.IsNegative(value._bits);

        public static bool IsNegativeInfinity(FixedPointDecimal value) => false;

        public static bool IsNormal(FixedPointDecimal value) => false;

        public static bool IsOddInteger(FixedPointDecimal value)
            => BitsType.IsOddInteger(value._bits);

        public static bool IsPositive(FixedPointDecimal value)
            => BitsType.IsPositive(value._bits);

        public static bool IsPositiveInfinity(FixedPointDecimal value) => false;

        public static bool IsRealNumber(FixedPointDecimal value) => true;
        public static bool IsSubnormal(FixedPointDecimal value) => false;

        public static bool IsZero(FixedPointDecimal value) => value._bits == 0;

        public static FixedPointDecimal MaxMagnitude(FixedPointDecimal x, FixedPointDecimal y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal MaxMagnitudeNumber(FixedPointDecimal x, FixedPointDecimal y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal MinMagnitude(FixedPointDecimal x, FixedPointDecimal y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal MinMagnitudeNumber(FixedPointDecimal x, FixedPointDecimal y)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FixedPointDecimal result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(FixedPointDecimal value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(FixedPointDecimal value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(FixedPointDecimal value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(FixedPointDecimal other) => _bits == other._bits;

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ToDecimal().TryFormat(destination, out charsWritten, format, provider);
        public string ToString(string? format, IFormatProvider? formatProvider)
            => ToDecimal().ToString(format, formatProvider);

        public static FixedPointDecimal Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FixedPointDecimal result)
        {
            throw new NotImplementedException();
        }

        public static FixedPointDecimal operator +(FixedPointDecimal left, FixedPointDecimal right)
            => new((BitsType)(left._bits + right._bits));

        public static FixedPointDecimal operator --(FixedPointDecimal value)
            => value - One;

        public static FixedPointDecimal operator /(FixedPointDecimal left, FixedPointDecimal right)
        {
            TempType temp = ((TempType)left._bits * OneBits) / (right._bits);
            return new((BitsType)temp);
        }

        public static bool operator ==(FixedPointDecimal left, FixedPointDecimal right)
            => left.Equals(right);

        public static bool operator !=(FixedPointDecimal left, FixedPointDecimal right)
            => !left.Equals(right);

        public static FixedPointDecimal operator ++(FixedPointDecimal value)
            => value + One;

        public static FixedPointDecimal operator *(FixedPointDecimal left, FixedPointDecimal right)
        {
            TempType temp = (TempType)left._bits * (TempType)right._bits;
            temp /= OneBits;
            return new((BitsType)temp);
        }

        public static FixedPointDecimal operator -(FixedPointDecimal left, FixedPointDecimal right)
            => new((BitsType)(left._bits - right._bits));

        public static FixedPointDecimal operator -(FixedPointDecimal value)
            => new((BitsType)(-value._bits));

        public static FixedPointDecimal operator +(FixedPointDecimal value)
            => value;

        public override bool Equals(object? obj)
        {
            return obj is FixedPointDecimal && Equals((FixedPointDecimal)obj);
        }
    }
}