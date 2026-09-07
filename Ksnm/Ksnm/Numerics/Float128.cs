using System;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics
{
    /// <summary>
    /// IEEE 754 binary128（4倍精度）の浮動小数点数。
    ///
    /// Layout:
    ///   bit 127       : sign
    ///   bits 126..112 : exponent (15 bits)
    ///   bits 111..0   : fraction (112 bits)
    ///
    /// Bias = 16383
    /// Precision = 113 bits including hidden bit.
    /// </summary>
    public readonly struct Float128 :
        IComparable<Float128>,
        IEquatable<Float128>
    {
        public const int FractionBits = 112;
        public const int ExponentBits = 15;
        public const int Precision = 113;

        public const int ExponentBias = 16383;

        public const int MinBiasedExponent = 0;
        public const int MaxBiasedExponent = 0x7FFF;

        public const int MinNormalExponent = -16382;
        public const int MaxNormalExponent = 16383;

        public const int MinSubnormalExponent = -16494;

        private const ulong SignMask = 0x8000000000000000UL;
        private const ulong ExponentMask = 0x7FFF000000000000UL;
        private const ulong FractionHighMask = 0x0000FFFFFFFFFFFFUL;

        private readonly ulong _hi;
        private readonly ulong _lo;

        public Float128(ulong hi, ulong lo)
        {
            _hi = hi;
            _lo = lo;
        }

        #region Constants
        public static readonly Float128 Zero = new Float128(0, 0);

        public static readonly Float128 NegativeZero = new Float128(SignMask, 0);

        public static readonly Float128 PositiveInfinity = new Float128(0x7FFF000000000000UL, 0);

        public static readonly Float128 NegativeInfinity = new Float128(0xFFFF000000000000UL, 0);

        public static readonly Float128 NaN = new Float128(0x7FFF800000000000UL, 0);

        public static readonly Float128 One = FromBits(0x3FFF000000000000UL, 0);

        public static readonly Float128 NegativeOne = FromBits(0xBFFF000000000000UL, 0);

        public static readonly Float128 Two = FromBits(0x4000000000000000UL, 0);

        public static readonly Float128 Half = FromBits(0x3FFE000000000000UL, 0);

        public static readonly Float128 MaxValue = FromBits(0x7FFEFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);

        public static readonly Float128 MinValue = FromBits(0xFFFEFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);

        public static readonly Float128 Epsilon = FromBits(0, 1);

        public static readonly Float128 MinNormal = FromBits(0x0001000000000000UL, 0);
        #endregion Constants

        #region Properties
        public ulong HighBits => _hi;
        public ulong LowBits => _lo;

        public bool IsNegative =>
            (_hi & SignMask) != 0;

        public int Sign =>
            IsNegative ? -1 : 1;

        public int BiasedExponent =>
            (int)((_hi >> 48) & 0x7FFF);

        public int Exponent =>
            BiasedExponent == 0
                ? MinNormalExponent
                : BiasedExponent - ExponentBias;

        public UInt128 Fraction =>
            ((UInt128)(_hi & FractionHighMask) << 64) | _lo;

        public bool IsZero =>
            (_hi & 0x7FFFFFFFFFFFFFFFUL) == 0 &&
            _lo == 0;

        public bool IsInfinity =>
            BiasedExponent == MaxBiasedExponent &&
            Fraction == 0;

        public bool IsNaN =>
            BiasedExponent == MaxBiasedExponent &&
            Fraction != 0;

        public bool IsFinite =>
            BiasedExponent != MaxBiasedExponent;

        public bool IsSubnormal =>
            BiasedExponent == 0 && Fraction != 0;

        public bool IsNormal =>
            BiasedExponent != 0 &&
            BiasedExponent != MaxBiasedExponent;

        #endregion

        #region Bit conversion
        public static Float128 FromBits(ulong high, ulong low)
        {
            return new Float128(high, low);
        }

        public static Float128 FromBits(UInt128 bits)
        {
            ulong hi = (ulong)(bits >> 64);
            ulong lo = (ulong)bits;
            return new Float128(hi, lo);
        }

        public UInt128 ToUInt128Bits()
        {
            return ((UInt128)_hi << 64) | _lo;
        }

        #endregion Bit conversion

        #region 他の型からの変換
        public static Float128 FromInt32(int value)
        {
            return FromInt64(value);
        }

        public static Float128 FromInt64(long value)
        {
            if (value == 0)
                return Zero;

            bool negative = value < 0;

            ulong magnitude;

            if (negative)
                magnitude = (ulong)(-(value + 1)) + 1;
            else
                magnitude = (ulong)value;

            return FromUInt64(magnitude, negative);
        }
        /// <summary>
        /// UInt64 から Float128 に変換します。
        /// </summary>
        public static Float128 FromUInt64(ulong value)
        {
            return FromUInt64(value, false);
        }
        /// <summary>
        /// UInt64 から Float128 に変換します。
        /// 符号を指定できます。
        /// </summary>
        private static Float128 FromUInt64(ulong value, bool negative)
        {
            if (value == 0)
                return negative ? NegativeZero : Zero;

            int msb = 63 - BitOperations.LeadingZeroCount(value);

            int exponent = msb;

            UInt128 significand = (UInt128)value << (112 - msb);

            ulong fractionHigh = (ulong)(significand >> 64) & FractionHighMask;

            ulong fractionLow = (ulong)significand;

            int biased = exponent + ExponentBias;

            ulong hi = (negative ? SignMask : 0) |
                ((ulong)biased << 48) |
                fractionHigh;

            return new Float128(hi, fractionLow);
        }
        /// <summary>
        /// UInt128 から Float128 に変換します。
        /// </summary>
        public static Float128 FromUInt128(UInt128 value)
        {
            if (value == 0)
                return Zero;

            bool negative = false;

            int msb = 127 - (int)UInt128.LeadingZeroCount(value);

            int exponent = msb;

            UInt128 significand;

            if (msb <= 112)
            {
                significand = value << (112 - msb);
            }
            else
            {
                int shift = msb - 112;

                significand = value >> shift;

                UInt128 remainder = value & ((UInt128.One << shift) - 1);

                bool round = remainder >
                    (UInt128.One << (shift - 1));

                bool tie = remainder == (UInt128.One << (shift - 1));

                if (round ||
                    (tie && (significand & 1) != 0))
                {
                    significand++;

                    if (significand == (UInt128.One << 113))
                    {
                        significand >>= 1;
                        exponent++;
                    }
                }
            }

            return Pack(negative, exponent, significand);
        }
        /// <summary>
        /// double から Float128 に変換します。
        /// </summary>
        public static Float128 FromDouble(double value)
        {
            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);

            bool negative = (bits & 0x8000000000000000UL) != 0;

            int exponent = (int)((bits >> 52) & 0x7FF);

            ulong fraction = bits & 0x000FFFFFFFFFFFFFUL;

            if (exponent == 0x7FF)
            {
                if (fraction == 0)
                    return negative ? NegativeInfinity : PositiveInfinity;

                return NaN;
            }

            if (exponent == 0)
            {
                if (fraction == 0)
                    return negative ? NegativeZero : Zero;

                // Convert double subnormal to exact binary128.
                int shift = BitOperations.LeadingZeroCount(fraction) - 11;

                ulong normalized = fraction << shift;

                int e = -1022 - shift;

                UInt128 significand = (UInt128)normalized << 60;

                return Pack(
                    negative,
                    e,
                    significand);
            }

            int unbiased = exponent - 1023;

            UInt128 sig = ((UInt128)(fraction | (1UL << 52))) << 60;

            return Pack(negative, unbiased, sig);
        }
        /// <summary>
        /// Float128 を double に変換します。
        /// </summary>
        public double ToDouble()
        {
            if (IsNaN)
                return double.NaN;

            if (IsInfinity)
                return IsNegative ? double.NegativeInfinity : double.PositiveInfinity;

            if (IsZero)
                return IsNegative ? -0.0 : 0.0;

            BigInteger value = GetSignificandBigInteger();

            int exponent = Exponent;

            double result = (double)value * Math.Pow(2.0, exponent - 112);

            return IsNegative
                ? -result
                : result;
        }

        public static explicit operator double(Float128 value)
            => value.ToDouble();

        public static implicit operator Float128(double value)
            => FromDouble(value);

        public static implicit operator Float128(int value)
            => FromInt32(value);

        public static implicit operator Float128(long value)
            => FromInt64(value);

        public static implicit operator Float128(ulong value)
            => FromUInt64(value);

        public static explicit operator Float128(UInt128 value)
            => FromUInt128(value);

        #endregion 他の型からの変換

        #region Internal representation
        private UInt128 GetRawSignificand()
        {
            UInt128 fraction = Fraction;

            if (BiasedExponent == 0)
                return fraction;

            return (UInt128.One << 112) | fraction;
        }

        private BigInteger GetSignificandBigInteger()
        {
            UInt128 value = GetRawSignificand();

            return
                ((BigInteger)(ulong)(value >> 64) << 64) |
                (ulong)value;
        }

        private static Float128 Pack(
            bool negative,
            int exponent,
            UInt128 significand)
        {
            if (significand == 0)
                return negative
                    ? NegativeZero
                    : Zero;

            while (significand >= (UInt128.One << 113))
            {
                significand >>= 1;
                exponent++;
            }

            while (significand < (UInt128.One << 112) &&
                   exponent > MinNormalExponent)
            {
                significand <<= 1;
                exponent--;
            }

            if (exponent > MaxNormalExponent)
            {
                return negative
                    ? NegativeInfinity
                    : PositiveInfinity;
            }

            // Subnormal
            if (exponent < MinNormalExponent)
            {
                int shift = MinNormalExponent - exponent;

                if (shift >= 114)
                    return negative
                        ? NegativeZero
                        : Zero;

                significand = RoundRightShift(
                        significand,
                        shift);

                exponent = MinNormalExponent;

                if (significand == 0)
                    return negative
                        ? NegativeZero
                        : Zero;

                ulong hiFraction = (ulong)(significand >> 64);

                ulong loFraction = (ulong)significand;

                return new Float128(
                    (negative ? SignMask : 0) |
                    hiFraction,
                    loFraction);
            }

            int biased = exponent + ExponentBias;

            UInt128 fraction = significand & ((UInt128.One << 112) - 1);

            ulong hi = (negative ? SignMask : 0) |
                ((ulong)biased << 48) |
                (ulong)(fraction >> 64);

            ulong lo = (ulong)fraction;

            return new Float128(hi, lo);
        }
        #endregion Internal representation

        #region Rounding
        private static UInt128 RoundRightShift(UInt128 value, int shift)
        {
            if (shift <= 0)
                return value << (-shift);

            if (shift >= 128)
                return 0;

            UInt128 result = value >> shift;

            UInt128 mask = (UInt128.One << shift) - 1;

            UInt128 remainder = value & mask;

            UInt128 half = UInt128.One << (shift - 1);

            if (remainder > half || (remainder == half && (result & 1) != 0))
            {
                result++;
            }

            return result;
        }
        #endregion Rounding

        public static Float128 operator +(Float128 a, Float128 b)
        {
            if (a.IsNaN || b.IsNaN)
                return NaN;

            if (a.IsInfinity)
            {
                if (b.IsInfinity && a.IsNegative != b.IsNegative)
                    return NaN;

                return a;
            }

            if (b.IsInfinity)
                return b;

            if (a.IsZero)
                return b;

            if (b.IsZero)
                return a;

            bool signA = a.IsNegative;
            bool signB = b.IsNegative;

            int expA = a.Exponent;
            int expB = b.Exponent;

            UInt128 sigA = a.GetRawSignificand();
            UInt128 sigB = b.GetRawSignificand();

            // Add 3 extra bits:
            // guard, round, sticky.
            sigA <<= 3;
            sigB <<= 3;

            if (expA < expB)
            {
                Swap(ref expA, ref expB);
                Swap(ref sigA, ref sigB);
                Swap(ref signA, ref signB);
            }

            int diff = expA - expB;

            sigB = ShiftRightSticky(sigB, diff);

            if (signA == signB)
            {
                UInt128 result = sigA + sigB;

                return NormalizeAndRound(signA, expA, result);
            }
            else
            {
                if (sigA == sigB)
                    return Zero;

                if (sigA < sigB)
                {
                    Swap(ref sigA, ref sigB);
                    signA = signB;
                }

                UInt128 result = sigA - sigB;

                return NormalizeAndRound(signA, expA, result);
            }
        }

        public static Float128 operator -(Float128 a, Float128 b)
        {
            return a + b.Negate();
        }

        private Float128 Negate()
        {
            return new Float128(_hi ^ SignMask, _lo);
        }

        public static Float128 operator *(Float128 a, Float128 b)
        {
            if (a.IsNaN || b.IsNaN)
                return NaN;

            if (a.IsInfinity || b.IsInfinity)
            {
                if (a.IsZero || b.IsZero)
                    return NaN;

                bool negative = a.IsNegative ^ b.IsNegative;

                return negative ? NegativeInfinity : PositiveInfinity;
            }

            if (a.IsZero || b.IsZero)
            {
                return
                    a.IsNegative ^ b.IsNegative ? NegativeZero : Zero;
            }

            bool sign = a.IsNegative ^ b.IsNegative;

            int exponent = a.Exponent + b.Exponent;

            UInt128 sigA = a.GetRawSignificand();

            UInt128 sigB = b.GetRawSignificand();

            UInt256 product = UInt256.Multiply(sigA, sigB);

            // Product is approximately:
            //
            //   (1.x * 2^112) * (1.y * 2^112)
            //
            // => 226-bit integer.
            //
            // We need 113 significant bits plus G/R/S.

            int top = product.BitLength - 1;

            int shift = top - 115;

            UInt128 rounded;

            if (shift > 0)
            {
                UInt256 shifted = product >> shift;

                rounded = shifted.Low128;

                if (product.HasAnyLowBits(shift - 1))
                {
                    // Sticky information is incorporated below.
                }

                bool guard = product.GetBit(shift - 1);

                bool sticky = product.HasAnyLowBits(shift - 1);

                if (guard &&
                    (sticky || (rounded & 1) != 0))
                {
                    rounded++;
                }
            }
            else
            {
                rounded = product.Low128 << (-shift);
            }

            // Remove 3 extra bits.
            rounded = RoundRightShift(rounded, 3);

            if (rounded >= (UInt128.One << 113))
            {
                rounded >>= 1;
                exponent++;
            }

            return Pack(sign, exponent, rounded);
        }

        public static Float128 operator /(Float128 a, Float128 b)
        {
            if (a.IsNaN || b.IsNaN)
                return NaN;

            bool sign = a.IsNegative ^ b.IsNegative;

            if (a.IsInfinity && b.IsInfinity)
                return NaN;

            if (a.IsZero && b.IsZero)
                return NaN;

            if (b.IsZero)
            {
                return sign ? NegativeInfinity : PositiveInfinity;
            }

            if (a.IsInfinity)
            {
                return sign ? NegativeInfinity : PositiveInfinity;
            }

            if (a.IsZero)
            {
                return sign ? NegativeZero : Zero;
            }

            if (b.IsInfinity)
            {
                return sign ? NegativeZero : Zero;
            }

            int exponent = a.Exponent - b.Exponent;

            UInt128 numerator = a.GetRawSignificand();

            UInt128 denominator = b.GetRawSignificand();

            // Generate 116 bits:
            // 113 significant bits + guard/round/sticky.
            //
            // numerator / denominator is in approximately [0.5, 2).
            UInt256 dividend = UInt256.FromUInt128(numerator) << 116;

            UInt128 quotient = UInt256.Divide(dividend, denominator, out bool remainder);

            if (quotient < (UInt128.One << 115))
            {
                quotient <<= 1;
                exponent--;
            }

            bool guard = (quotient & 1) != 0;

            quotient >>= 1;

            bool round = (quotient & 1) != 0;

            quotient >>= 1;

            bool sticky = remainder;

            if (guard && (round || sticky || (quotient & 1) != 0))
            {
                quotient++;

                if (quotient >= (UInt128.One << 113))
                {
                    quotient >>= 1;
                    exponent++;
                }
            }

            return Pack(sign, exponent, quotient);
        }

        #region Normalize / round
        private static Float128 NormalizeAndRound(bool negative, int exponent, UInt128 value)
        {
            if (value == 0)
                return negative ? NegativeZero : Zero;

            // value contains 3 extra bits.
            UInt128 target = UInt128.One << 115;

            while (value >= (target << 1))
            {
                bool sticky = (value & 1) != 0;

                value >>= 1;

                if (sticky)
                    value |= 1;

                exponent++;
            }

            while (value < target &&
                   exponent > MinNormalExponent)
            {
                value <<= 1;
                exponent--;
            }

            UInt128 rounded = RoundRightShift(value, 3);

            if (rounded >= (UInt128.One << 113))
            {
                rounded >>= 1;
                exponent++;
            }

            return Pack(negative, exponent, rounded);
        }
        #endregion Normalize / round

        #region Shift with sticky bit
        private static UInt128 ShiftRightSticky(UInt128 value, int shift)
        {
            if (shift <= 0)
                return value << (-shift);

            if (shift >= 128)
                return value == UInt128.Zero ? UInt128.Zero : UInt128.One;

            UInt128 result = value >> shift;

            UInt128 mask = (UInt128.One << shift) - 1;

            if ((value & mask) != 0)
                result |= 1;

            return result;
        }
        #endregion Shift with sticky bit

        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        #region Comparison
        public int CompareTo(Float128 other)
        {
            if (IsNaN)
                return other.IsNaN ? 0 : 1;

            if (other.IsNaN)
                return -1;

            if (IsZero && other.IsZero)
                return 0;

            if (IsNegative != other.IsNegative)
                return IsNegative ? -1 : 1;

            int result = CompareMagnitude(other);

            return IsNegative ? -result : result;
        }

        private int CompareMagnitude(Float128 other)
        {
            int e1 = Exponent;
            int e2 = other.Exponent;

            if (e1 != e2)
                return e1.CompareTo(e2);

            UInt128 s1 = GetRawSignificand();

            UInt128 s2 = other.GetRawSignificand();

            return s1.CompareTo(s2);
        }

        public bool Equals(Float128 other)
        {
            if (IsNaN || other.IsNaN)
                return false;

            if (IsZero && other.IsZero)
                return true;

            return _hi == other._hi && _lo == other._lo;
        }

        public override bool Equals(object? obj)
        {
            return obj is Float128 other &&
                   Equals(other);
        }

        public override int GetHashCode()
        {
            if (IsZero)
                return 0;

            return HashCode.Combine(_hi, _lo);
        }

        public static bool operator ==(Float128 a, Float128 b)
            => a.Equals(b);

        public static bool operator !=(Float128 a, Float128 b)
            => !a.Equals(b);

        public static bool operator <(Float128 a, Float128 b)
            => a.CompareTo(b) < 0;

        public static bool operator >(Float128 a, Float128 b)
            => a.CompareTo(b) > 0;

        public static bool operator <=(Float128 a, Float128 b)
            => a.CompareTo(b) <= 0;

        public static bool operator >=(Float128 a, Float128 b)
            => a.CompareTo(b) >= 0;
        #endregion Comparison

        #region Unary operators
        public static Float128 operator +(Float128 value)
            => value;

        public static Float128 operator -(Float128 value)
            => value.Negate();
        #endregion Unary operators

        /// <summary>
        /// 文字列に変換します。
        /// 仮実装。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsNaN)
                return "NaN";

            if (IsInfinity)
                return IsNegative ? "-Infinity" : "Infinity";

            if (IsZero)
                return IsNegative ? "-0" : "0";

            return ToDouble().ToString("R", CultureInfo.InvariantCulture);
        }
    }
}