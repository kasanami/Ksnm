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
        /// <summary>
        /// Fraction のビット数。
        /// </summary>
        public const int FractionBits = 112;
        /// <summary>
        /// Exponent のビット数。
        /// </summary>
        public const int ExponentBits = 15;
        /// <summary>
        /// Precision（有効桁数）。
        /// </summary>
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
        /// <summary>
        /// Float128 を構築します。
        /// </summary>
        /// <param name="negative">負の数かどうか</param>
        /// <param name="exponent">指数[-16383, 16383]</param>
        /// <param name="fraction">分数</param>
        public Float128(bool negative, int exponent, UInt128 fraction)
        {
            if (exponent < MinNormalExponent || exponent > MaxNormalExponent)
                throw new ArgumentOutOfRangeException(nameof(exponent), $"exponent must be in range [{MinNormalExponent}, {MaxNormalExponent}]");

            int biasedExponent = exponent + ExponentBias;
            ulong fractionHigh = (ulong)(fraction >> 64) & FractionHighMask;
            ulong fractionLow = (ulong)(fraction & 0xFFFFFFFFFFFFFFFFUL);
            _hi = (negative ? SignMask : 0) |
                  ((ulong)(biasedExponent) << 48) |
                  (fractionHigh & FractionHighMask);
            _lo = fractionLow;
        }
        /// <summary>
        /// Float128 を構築します。
        /// </summary>
        /// <param name="negative">負の数かどうか</param>
        /// <param name="exponent">指数[-16383, 16383]</param>
        /// <param name="fractionHigh">分数の上位48ビット(49ビット目より高位は無視されます)</param>
        /// <param name="fractionLow">分数の下位64ビット</param>
        public Float128(bool negative, int exponent, ulong fractionHigh, ulong fractionLow)
        {
            if (exponent < MinNormalExponent || exponent > MaxNormalExponent)
                throw new ArgumentOutOfRangeException(nameof(exponent), $"exponent must be in range [{MinNormalExponent}, {MaxNormalExponent}]");

            int biasedExponent = exponent + ExponentBias;
            _hi = (negative ? SignMask : 0) |
                  ((ulong)(biasedExponent) << 48) |
                  (fractionHigh & FractionHighMask);
            _lo = fractionLow;
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

            UInt128 significand = (UInt128)value << (FractionBits - msb);
            ulong fractionHigh = (ulong)(significand >> 64) & FractionHighMask;
            ulong fractionLow = (ulong)significand;
            return new Float128(negative, exponent, fractionHigh, fractionLow);
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

            if (msb <= FractionBits)
            {
                significand = value << (FractionBits - msb);
            }
            else
            {
                int shift = msb - FractionBits;

                significand = value >> shift;

                UInt128 remainder = value & ((UInt128.One << shift) - 1);

                bool round = remainder >
                    (UInt128.One << (shift - 1));

                bool tie = remainder == (UInt128.One << (shift - 1));

                if (round ||
                    (tie && (significand & 1) != 0))
                {
                    significand++;

                    if (significand == (UInt128.One << Precision))
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

                return Pack(negative, e, significand);
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

            double result = (double)value * Math.Pow(2.0, exponent - FractionBits);

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

            return (UInt128.One << FractionBits) | fraction;
        }

        private BigInteger GetSignificandBigInteger()
        {
            UInt128 value = GetRawSignificand();

            return
                ((BigInteger)(ulong)(value >> 64) << 64) |
                (ulong)value;
        }

        private static Float128 Pack(bool negative, int exponent, UInt128 significand)
        {
            if (significand == 0)
                return negative
                    ? NegativeZero
                    : Zero;

            while (significand >= (UInt128.One << Precision))
            {
                significand >>= 1;
                exponent++;
            }

            while (significand < (UInt128.One << FractionBits) &&
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

                significand = RoundRightShift(significand, shift);

                exponent = MinNormalExponent;

                if (significand == 0)
                    return negative
                        ? NegativeZero
                        : Zero;

                ulong hiFraction = (ulong)(significand >> 64);
                ulong loFraction = (ulong)significand;

                return new Float128((negative ? SignMask : 0) | hiFraction, loFraction);
            }
            UInt128 fraction = significand & ((UInt128.One << FractionBits) - 1);
            return new Float128(negative, exponent, fraction);
        }
        #endregion Internal representation

        #region Rounding
        /// <summary>
        /// value を shift ビット右に丸めてシフトします。
        /// * 境界値では、最後のbitが偶数になるように丸めます。
        /// </summary>
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

            if (rounded >= (UInt128.One << Precision))
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

                if (quotient >= (UInt128.One << Precision))
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

            if (rounded >= (UInt128.One << Precision))
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

        #region Parse
        public static Float128 Parse(string s)
        {
            return Parse(s, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public static Float128 Parse(string s, IFormatProvider? provider)
        {
            return Parse(s, NumberStyles.Float, provider);
        }

        public static Float128 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));

            if (TryParse(s, style, provider, out Float128 result))
            {
                return result;
            }

            throw new FormatException($"The input string '{s}' was not in a correct format.");
        }
        #endregion Parse

        #region TryParse
        public static bool TryParse(string? s, out Float128 result)
        {
            return TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        public static bool TryParse(string? s, IFormatProvider? provider, out Float128 result)
        {
            return TryParse(s, NumberStyles.Float, provider, out result);
        }

        public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Float128 result)
        {
            result = Zero;

            if (string.IsNullOrWhiteSpace(s))
                return false;

            string text = s.Trim();

            #region 特殊値の処理
            if (IsPositiveInfinityString(text))
            {
                result = PositiveInfinity;
                return true;
            }

            if (IsNegativeInfinityString(text))
            {
                result = NegativeInfinity;
                return true;
            }

            if (IsNaNString(text))
            {
                result = NaN;
                return true;
            }
            #endregion

            #region 符号の処理
            int index = 0;
            bool negative = false;

            if (text[index] == '+')
            {
                index++;
            }
            else if (text[index] == '-')
            {
                negative = true;
                index++;
            }

            if (index >= text.Length)
                return false;
            #endregion

            #region カルチャーの処理
            NumberFormatInfo nfi = NumberFormatInfo.GetInstance(provider ?? CultureInfo.InvariantCulture);

            string decimalSeparator = nfi.NumberDecimalSeparator;

            if (string.IsNullOrEmpty(decimalSeparator))
                decimalSeparator = ".";

            // NumberStyles.AllowDecimalPoint が無い場合、
            // 小数点を許可しない。
            bool allowDecimal = (style & NumberStyles.AllowDecimalPoint) != 0;

            bool allowExponent = (style & NumberStyles.AllowExponent) != 0;
            #endregion

            #region Digits
            BigInteger significand = BigInteger.Zero;

            int digitCount = 0;
            int fractionalDigits = 0;

            bool decimalSeen = false;

            while (index < text.Length)
            {
                char c = text[index];

                if (c >= '0' && c <= '9')
                {
                    significand =
                        significand * 10 +
                        (c - '0');

                    digitCount++;

                    if (decimalSeen)
                        fractionalDigits++;

                    index++;
                    continue;
                }

                if (!decimalSeen &&
                    allowDecimal &&
                    StartsWith(text, index, decimalSeparator))
                {
                    decimalSeen = true;

                    index += decimalSeparator.Length;
                    continue;
                }

                break;
            }

            if (digitCount == 0)
                return false;
            #endregion

            #region Exponent
            int decimalExponent = 0;

            if (index < text.Length &&
                (text[index] == 'e' ||
                 text[index] == 'E'))
            {
                if (!allowExponent)
                    return false;

                index++;

                if (index >= text.Length)
                    return false;

                bool exponentNegative = false;

                if (text[index] == '+')
                {
                    index++;
                }
                else if (text[index] == '-')
                {
                    exponentNegative = true;
                    index++;
                }

                if (index >= text.Length)
                    return false;

                int exponentValue = 0;
                int exponentDigits = 0;

                while (index < text.Length)
                {
                    char c = text[index];

                    if (c < '0' || c > '9')
                        return false;

                    exponentDigits++;

                    // overflow protection
                    if (exponentValue > 1_000_000)
                    {
                        exponentValue = 1_000_000;
                    }
                    else
                    {
                        exponentValue =
                            exponentValue * 10 +
                            (c - '0');

                        if (exponentValue > 1_000_000)
                            exponentValue = 1_000_000;
                    }

                    index++;
                }

                if (exponentDigits == 0)
                    return false;

                decimalExponent =
                    exponentNegative
                        ? -exponentValue
                        : exponentValue;
            }

            // 文字列の最後まで消費できていなければ不正。
            if (index != text.Length)
                return false;
            #endregion

            // --------------------------------------------------------
            // 末尾の小数点以下のゼロを削除します。
            // Example:
            //
            // 123.45000
            //
            // becomes
            //
            // significand = 12345
            // decimalExponent = -2
            // --------------------------------------------------------

            while (significand != 0 &&
                   significand % 10 == 0)
            {
                significand /= 10;

                fractionalDigits--;
            }

            if (significand == 0)
            {
                result =
                    negative
                        ? NegativeZero
                        : Zero;

                return true;
            }

            // Effective power of ten:
            //
            // significand × 10^decimalScale
            //
            int decimalScale = decimalExponent - fractionalDigits;

            result = FromDecimal(negative, significand, decimalScale);

            return true;
        }
        #endregion TryParse


        // ============================================================
        // Special value parsing
        // ============================================================

        private static bool IsPositiveInfinityString(string text)
        {
            return
                text.Equals(
                    "Infinity",
                    StringComparison.OrdinalIgnoreCase) ||
                text.Equals(
                    "+Infinity",
                    StringComparison.OrdinalIgnoreCase) ||
                text.Equals(
                    "∞",
                    StringComparison.Ordinal);
        }

        private static bool IsNegativeInfinityString(string text)
        {
            return
                text.Equals(
                    "-Infinity",
                    StringComparison.OrdinalIgnoreCase) ||
                text.Equals(
                    "-∞",
                    StringComparison.Ordinal);
        }

        private static bool IsNaNString(string text)
        {
            return text.Equals("NaN", StringComparison.OrdinalIgnoreCase);
        }

        private static bool StartsWith(string text, int index, string value)
        {
            if (index + value.Length > text.Length)
                return false;

            return string.Compare(
                text,
                index,
                value,
                0,
                value.Length,
                StringComparison.Ordinal) == 0;
        }

        // ============================================================
        // Decimal -> Float128
        // ============================================================

        private static Float128 FromDecimal(bool negative, BigInteger significand, int decimalScale)
        {
            if (significand.IsZero)
            {
                return negative
                    ? NegativeZero
                    : Zero;
            }

            // --------------------------------------------------------
            // value =
            //
            //     significand × 10^decimalScale
            //
            // 10^n = 2^n × 5^n
            //
            // Therefore:
            //
            // significand × 10^n
            //
            // can be represented exactly as
            //
            // significand × 5^n × 2^n
            // --------------------------------------------------------

            if (decimalScale >= 0)
            {
                BigInteger integerValue = significand * BigInteger.Pow(10, decimalScale);
                return FromBigInteger(negative, integerValue);
            }

            int scale = -decimalScale;

            // value = significand / 10^scale
            //
            //          significand
            //        = -----------
            //          2^scale × 5^scale
            //
            // Instead of creating an enormous decimal denominator,
            // calculate the binary exponent first and then perform
            // integer division with sufficient extra precision.

            BigInteger denominator = BigInteger.Pow(10, scale);

            return FromBigIntegerRatio(negative, significand, denominator);
        }

        // ============================================================
        // BigInteger -> Float128
        // ============================================================

        private static Float128 FromBigInteger(bool negative, BigInteger value)
        {
            if (value.IsZero)
            {
                return negative
                    ? NegativeZero
                    : Zero;
            }

            int bitLength = GetBitLength(value);
            int exponent = bitLength - 1;

            BigInteger significand;

            if (bitLength > Precision)
            {
                int shift = bitLength - Precision;
                BigInteger truncated = value >> shift;
                BigInteger remainder = value - (truncated << shift);
                BigInteger half = BigInteger.One << (shift - 1);
                bool roundUp = remainder > half || (remainder == half && !truncated.IsEven);
                significand = truncated + (roundUp ? BigInteger.One : BigInteger.Zero);
                if (significand == (BigInteger.One << Precision))
                {
                    significand >>= 1;
                    exponent++;
                }
            }
            else
            {
                significand = value << (Precision - bitLength);
            }

            return PackBigInteger(negative, exponent, significand);
        }

        /// <summary>
        /// 有理数をFloat128に変換
        /// value = numerator / denominator
        /// </summary>
        /// <param name="negative"></param>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Float128 FromBigIntegerRatio(bool negative, BigInteger numerator, BigInteger denominator)
        {
            if (numerator.IsZero)
            {
                return negative
                    ? NegativeZero
                    : Zero;
            }

            if (denominator.Sign <= 0)
                throw new ArgumentOutOfRangeException(nameof(denominator));

            // --------------------------------------------------------
            // 次の条件を満たす2進数の指数 e を求める。
            //
            //     2^e <= value < 2^(e+1)
            // --------------------------------------------------------
            int numeratorBits = GetBitLength(numerator);
            int denominatorBits = GetBitLength(denominator);
            int exponent = numeratorBits - denominatorBits;
            if (exponent >= 0)
            {
                if (numerator < (denominator << exponent))
                {
                    exponent--;
                }
            }
            else
            {
                if ((numerator << (-exponent)) < denominator)
                {
                    exponent--;
                }
            }

            // --------------------------------------------------------
            // Generate:
            //
            // 113 significant bits
            // + guard bit
            // + round bit
            // + enough remainder information
            //
            // q = floor(value × 2^N)
            // --------------------------------------------------------

            const int ExtraBits = 3;
            const int PrecisionWithExtra = Precision + ExtraBits;

            int shift = PrecisionWithExtra - 1 - exponent;

            BigInteger scaledNumerator;
            BigInteger scaledDenominator;

            if (shift >= 0)
            {
                scaledNumerator = numerator << shift;
                scaledDenominator = denominator;
            }
            else
            {
                scaledNumerator = numerator;
                scaledDenominator = denominator << (-shift);
            }

            BigInteger quotient = BigInteger.DivRem(scaledNumerator, scaledDenominator, out BigInteger remainder);
            var scaledNumeratorStr = scaledNumerator.ToString("X");
            var scaledDenominatorStr = scaledDenominator.ToString("X");
            var remainderStr = remainder.ToString("X");
            var quotientStr = quotient.ToString("X");
            // --------------------------------------------------------
            // quotient contains:
            //
            // [113 significant][G][R]
            //
            // remainder is the sticky information.
            // --------------------------------------------------------

            bool guard = ((quotient & 1) != 0);
            quotient >>= 1;
            bool round = ((quotient & 1) != 0);
            quotient >>= 1;
            bool sticky = remainder != 0;
            if (guard && (round || sticky || !quotient.IsEven))
            {
                quotient++;
            }

            // --------------------------------------------------------
            // Rounding overflow
            // --------------------------------------------------------
            quotientStr = quotient.ToString("X");
            if (quotient >= (BigInteger.One << Precision))
            {
                quotient >>= 1;
                exponent++;
            }
            quotientStr = quotient.ToString("X");
            return PackBigInteger(negative, exponent, quotient);
        }

        // ============================================================
        // Pack BigInteger significand
        // ============================================================

        /// <summary>
        /// BigInteger から Float128 を作成します。
        /// </summary>
        /// <param name="negative">負数かどうか</param>
        /// <param name="exponent">指数[-16382, 16383]</param>
        /// <param name="significand">仮数(1)</param>
        /// <returns></returns>
        public static Float128 PackBigInteger(bool negative, int exponent, BigInteger significand)
        {
            if (significand.IsZero)
            {
                return negative
                    ? NegativeZero
                    : Zero;
            }

            // Overflow
            if (exponent > MaxNormalExponent)
            {
                return negative
                    ? NegativeInfinity
                    : PositiveInfinity;
            }

            // Normal number
            if (exponent >= MinNormalExponent)
            {
                var significandStr = significand.ToString("X");
                BigInteger fraction = significand - (BigInteger.One << FractionBits);
                var fractionStr = fraction.ToString("X");
                ulong fractionHigh = (ulong)(fraction >> 64);
                ulong fractionLow = (ulong)(fraction & 0xFFFFFFFFFFFFFFFFUL);
                return new Float128(negative, exponent, fractionHigh, fractionLow);
            }
            else
            {
                // --------------------------------------------------------
                // Subnormal
                //
                // value = significand × 2^(exponent - 112)
                //
                // For the smallest normal exponent:
                //
                // 2^-16382
                //
                // the subnormal fraction uses:
                //
                // 2^-16494
                //
                // Therefore shift the significand so that the binary
                // point is at -16494.
                // --------------------------------------------------------

                int shift = exponent - MinSubnormalExponent - FractionBits;

                BigInteger fraction;

                if (shift >= 0)
                {
                    fraction = significand << shift;
                }
                else
                {
                    int rightShift = -shift;
                    BigInteger truncated = significand >> rightShift;
                    BigInteger remainder = significand - (truncated << rightShift);
                    BigInteger half = BigInteger.One << (rightShift - 1);
                    bool roundUp = remainder > half || (remainder == half && !truncated.IsEven);
                    fraction = truncated + (roundUp ? BigInteger.One : BigInteger.Zero);
                }

                // 丸め処理によって、最小の正規値が得られた可能性があります。
                if (fraction >= (BigInteger.One << FractionBits))
                {
                    ulong hi = (negative ? SignMask : 0) | 0x0001000000000000UL;
                    return new Float128(hi, 0);
                }

                ulong fractionHigh = (ulong)(fraction >> 64);
                ulong fractionLow = (ulong)(fraction & 0xFFFFFFFFFFFFFFFFUL);

                return new Float128((negative ? SignMask : 0) | fractionHigh, fractionLow);
            }
        }

        /// <summary>
        /// 何ビットの整数として表現されているかを調べる
        /// 例：13 = 0b1101　・・・　4ビット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetBitLength(BigInteger value)
        {
#if false
            if (value.IsZero)
                return 0;

            if (value.Sign < 0)
                value = BigInteger.Abs(value);

            byte[] bytes = value.ToByteArray(isUnsigned: true, isBigEndian: true);
            int leadingZeroCount = byte.LeadingZeroCount(bytes[0]);
            // 全体のビット数から先頭のゼロビット数を引く
            return bytes.Length * 8 - leadingZeroCount;
#else
            return (int)value.GetBitLength();
#endif
        }
    }
}