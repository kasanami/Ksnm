/*
The zlib License

Copyright (c) 2026 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Ksnm.Numerics;

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
    IComparable,
    IComparable<Float128>,
    IEquatable<Float128>,
    IAdditionOperators<Float128, Float128, Float128>,
    IAdditiveIdentity<Float128, Float128>,
    ISubtractionOperators<Float128, Float128, Float128>,
    IMultiplyOperators<Float128, Float128, Float128>,
    IMultiplicativeIdentity<Float128, Float128>,
    IDivisionOperators<Float128, Float128, Float128>,
    IUnaryPlusOperators<Float128, Float128>,
    IUnaryNegationOperators<Float128, Float128>,
    IEqualityOperators<Float128, Float128, bool>,
    IComparisonOperators<Float128, Float128, bool>,
    INumberBase<Float128>,
    INumber<Float128>,
    IIncrementOperators<Float128>,
    IDecrementOperators<Float128>,
    IMinMaxValue<Float128>,
    IParsable<Float128>,
    IFormattable,
    ISpanFormattable
{
    #region 定数
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

    /// <summary>加法の単位元です。</summary>
    public static Float128 AdditiveIdentity => Zero;

    /// <summary>乗法の単位元です。</summary>
    public static Float128 MultiplicativeIdentity => One;

    public static int Radix => 2;

    static Float128 IMinMaxValue<Float128>.MaxValue => MaxValue;
    static Float128 IMinMaxValue<Float128>.MinValue => MinValue;
    #endregion 定数

    #region Fields
    private readonly ulong _upper;
    private readonly ulong _lower;
    #endregion Fields

    #region コンストラクタ
    public Float128(ulong upper, ulong lower)
    {
        _upper = upper;
        _lower = lower;
    }
    /// <summary>
    /// Float128 を構築します。
    /// </summary>
    /// <param name="negative">負の数かどうか</param>
    /// <param name="exponent">指数[-16383, 16383]</param>
    /// <param name="fraction">仮数</param>
    public Float128(bool negative, int exponent, UInt128 fraction)
    {
        if (exponent < MinNormalExponent || exponent > MaxNormalExponent)
            throw new ArgumentOutOfRangeException(nameof(exponent), $"exponent must be in range [{MinNormalExponent}, {MaxNormalExponent}]");

        int biasedExponent = exponent + ExponentBias;
        ulong fractionHigh = (ulong)(fraction >> 64) & FractionHighMask;
        ulong fractionLow = (ulong)(fraction & 0xFFFFFFFFFFFFFFFFUL);
        _upper = (negative ? SignMask : 0) |
              ((ulong)(biasedExponent) << 48) |
              (fractionHigh & FractionHighMask);
        _lower = fractionLow;
    }
    /// <summary>
    /// Float128 を構築します。
    /// </summary>
    /// <param name="negative">負の数かどうか</param>
    /// <param name="exponent">指数[-16383, 16383]</param>
    /// <param name="fractionHigh">仮数の上位48ビット(49ビット目より高位は無視されます)</param>
    /// <param name="fractionLow">仮数の下位64ビット</param>
    public Float128(bool negative, int exponent, ulong fractionHigh, ulong fractionLow)
    {
        if (exponent < MinNormalExponent || exponent > MaxNormalExponent)
            throw new ArgumentOutOfRangeException(nameof(exponent), $"exponent must be in range [{MinNormalExponent}, {MaxNormalExponent}]");

        int biasedExponent = exponent + ExponentBias;
        _upper = (negative ? SignMask : 0) |
              ((ulong)(biasedExponent) << 48) |
              (fractionHigh & FractionHighMask);
        _lower = fractionLow;
    }
    #endregion コンストラクタ

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
    /// <summary>
    /// Float128 の内部表現の上位64ビットを取得します。
    /// </summary>
    public ulong UpperBits => _upper;
    /// <summary>
    /// Float128 の内部表現の下位64ビットを取得します。
    /// </summary>
    public ulong LowerBits => _lower;

    public bool IsNegative =>
        (_upper & SignMask) != 0;

    public int Sign =>
        IsNegative ? -1 : 1;

    public int BiasedExponent =>
        (int)((_upper >> 48) & 0x7FFF);

    public int Exponent =>
        BiasedExponent == 0
            ? MinNormalExponent
            : BiasedExponent - ExponentBias;

    public UInt128 Fraction =>
        ((UInt128)(_upper & FractionHighMask) << 64) | _lower;

    public bool IsZero =>
        (_upper & 0x7FFFFFFFFFFFFFFFUL) == 0 &&
        _lower == 0;

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
    /// <summary>
    /// 正の無限大かどうかを判定します。
    /// </summary>
    public bool IsPositiveInfinity =>
        BiasedExponent == MaxBiasedExponent &&
        Fraction == 0 &&
        !IsNegative;
    /// <summary>
    /// 負の無限大かどうかを判定します。
    /// </summary>
    public bool IsNegativeInfinity =>
        BiasedExponent == MaxBiasedExponent &&
        Fraction == 0 &&
        IsNegative;

    static Float128 INumberBase<Float128>.One => One;

    static Float128 INumberBase<Float128>.Zero => Zero;
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
        return ((UInt128)_upper << 64) | _lower;
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

    #region Operators
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
    #endregion Operators

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
    /// <summary>
    /// Float128 を比較します。
    /// </summary>
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
    /// <summary>
    /// Float128 を比較します。
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is Float128 other)
            return CompareTo(other);

        throw new ArgumentException($"Object must be of type {nameof(Float128)}.",nameof(obj));
    }
    /// <summary>
    /// Float128 の大きさを比較します。
    /// ・大きさは符号を無視した値の比較です。
    /// </summary>
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

        return _upper == other._upper && _lower == other._lower;
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

        return HashCode.Combine(_upper, _lower);
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

    #region INumberBase
    public static Float128 Abs(Float128 value)
        => value.IsNegative ? value.Negate() : value;

    public static bool IsCanonical(Float128 value) => true;
    public static bool IsComplexNumber(Float128 value) => false;
    public static bool IsImaginaryNumber(Float128 value) => false;
    static bool INumberBase<Float128>.IsFinite(Float128 value) => value.IsFinite;
    static bool INumberBase<Float128>.IsInfinity(Float128 value) => value.IsInfinity;
    static bool INumberBase<Float128>.IsNaN(Float128 value) => value.IsNaN;
    static bool INumberBase<Float128>.IsNegative(Float128 value) => value.IsNegative;
    static bool INumberBase<Float128>.IsNegativeInfinity(Float128 value) => value.IsNegativeInfinity;
    static bool INumberBase<Float128>.IsNormal(Float128 value) => value.IsNormal;
    static bool INumberBase<Float128>.IsPositiveInfinity(Float128 value) => value.IsPositiveInfinity;
    static bool INumberBase<Float128>.IsSubnormal(Float128 value) => value.IsSubnormal;
    static bool INumberBase<Float128>.IsZero(Float128 value) => value.IsZero;
    static bool INumberBase<Float128>.IsPositive(Float128 value) => !value.IsNegative && !value.IsNaN;
    static bool INumberBase<Float128>.IsRealNumber(Float128 value) => !value.IsNaN;

    public static bool IsInteger(Float128 value)
    {
        if (!value.IsFinite)
            return false;
        if (value.IsZero)
            return true;

        int exponent = value.Exponent;
        if (exponent < 0)
            return false;
        if (exponent >= FractionBits)
            return true;

        int fractionalBitCount = FractionBits - exponent;
        UInt128 mask = (UInt128.One << fractionalBitCount) - 1;
        return (value.GetRawSignificand() & mask) == 0;
    }

    public static bool IsEvenInteger(Float128 value)
    {
        if (!IsInteger(value))
            return false;
        if (value.IsZero)
            return true;
        if (value.Exponent > FractionBits)
            return true;
        if (value.Exponent < FractionBits)
            return false;
        return (value.GetRawSignificand() & 1) == 0;
    }

    public static bool IsOddInteger(Float128 value)
    {
        if (!IsInteger(value))
            return false;
        if (value.Exponent != FractionBits)
            return false;
        return (value.GetRawSignificand() & 1) != 0;
    }

    public static Float128 MaxMagnitude(Float128 x, Float128 y)
    {
        if (x.IsNaN || y.IsNaN)
            return NaN;
        Float128 ax = Abs(x);
        Float128 ay = Abs(y);
        return ax >= ay ? x : y;
    }

    public static Float128 MinMagnitude(Float128 x, Float128 y)
    {
        if (x.IsNaN || y.IsNaN)
            return NaN;
        Float128 ax = Abs(x);
        Float128 ay = Abs(y);
        return ax <= ay ? x : y;
    }

    public static Float128 MaxMagnitudeNumber(Float128 x, Float128 y)
    {
        if (x.IsNaN) return y;
        if (y.IsNaN) return x;
        return MaxMagnitude(x, y);
    }

    public static Float128 MinMagnitudeNumber(Float128 x, Float128 y)
    {
        if (x.IsNaN) return y;
        if (y.IsNaN) return x;
        return MinMagnitude(x, y);
    }

    public static Float128 CreateChecked<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(Float128))
            return (Float128)(object)value;

        if (TOther.TryConvertToChecked(value, out Float128 result))
            return result;

        throw new NotSupportedException($"Conversion from {typeof(TOther)} to {typeof(Float128)} is not supported.");
    }

    public static Float128 CreateSaturating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(Float128))
            return (Float128)(object)value;

        if (TOther.TryConvertToSaturating(value, out Float128 result))
            return result;

        throw new NotSupportedException($"Conversion from {typeof(TOther)} to {typeof(Float128)} is not supported.");
    }

    public static Float128 CreateTruncating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(Float128))
            return (Float128)(object)value;

        if (TOther.TryConvertToTruncating(value, out Float128 result))
            return result;

        throw new NotSupportedException($"Conversion from {typeof(TOther)} to {typeof(Float128)} is not supported.");
    }

    public static bool TryConvertFromChecked<TOther>(TOther value, out Float128 result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = CreateChecked(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = Zero;
            return false;
        }
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Float128 result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = CreateSaturating(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = Zero;
            return false;
        }
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Float128 result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = CreateTruncating(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = Zero;
            return false;
        }
    }

    public static bool TryConvertToChecked<TOther>(Float128 value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = TOther.CreateChecked(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = default!;
            return false;
        }
    }

    public static bool TryConvertToSaturating<TOther>(Float128 value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = TOther.CreateSaturating(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = default!;
            return false;
        }
    }

    public static bool TryConvertToTruncating<TOther>(Float128 value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        try
        {
            result = TOther.CreateTruncating(value);
            return true;
        }
        catch (NotSupportedException)
        {
            result = default!;
            return false;
        }
    }

    public static Float128 operator %(Float128 a, Float128 b)
    {
        if (a.IsNaN || b.IsNaN || a.IsInfinity || b.IsZero)
            return NaN;
        if (a.IsZero || b.IsInfinity)
            return a;

        Float128 quotient = a / b;
        Float128 truncated = Truncate(quotient);
        return a - truncated * b;
    }

    private static Float128 Truncate(Float128 value)
    {
        if (!value.IsFinite || value.IsZero)
            return value;

        int exponent = value.Exponent;
        if (exponent < 0)
            return value.IsNegative ? NegativeZero : Zero;
        if (exponent >= FractionBits)
            return value;

        int clearBits = FractionBits - exponent;
        UInt128 raw = value.GetRawSignificand();
        UInt128 mask = (UInt128.One << clearBits) - 1;
        raw &= ~mask;
        return Pack(value.IsNegative, exponent, raw);
    }

    public static Float128 operator ++(Float128 value) => value + One;
    public static Float128 operator --(Float128 value) => value - One;

    public static Float128 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        => Parse(s.ToString(), style, provider);

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Float128 result)
        => TryParse(s.ToString(), style, provider, out result);

    public static Float128 Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider)
    {
        if (!TryParse(utf8Text, NumberStyles.Float, provider, out Float128 result))
            throw new FormatException("The input UTF-8 text was not in a correct format.");
        return result;
    }

    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Float128 result)
        => TryParse(utf8Text, NumberStyles.Float, provider, out result);

    public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out Float128 result)
    {
        string text = Encoding.UTF8.GetString(utf8Text);
        return TryParse(text, style, provider, out result);
    }

    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        string text = ToString(format.ToString(), provider);
        int required = Encoding.UTF8.GetByteCount(text);
        if (required > utf8Destination.Length)
        {
            bytesWritten = 0;
            return false;
        }
        bytesWritten = Encoding.UTF8.GetBytes(text, utf8Destination);
        return true;
    }
    #endregion INumberBase

    #region INumber
    public static Float128 Clamp(Float128 value, Float128 min, Float128 max)
    {
        if (min > max)
            throw new ArgumentException("min must be less than or equal to max.");

        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
    }

    public static Float128 CopySign(Float128 value, Float128 sign)
    {
        bool valueNegative = value.IsNegative;
        bool signNegative = sign.IsNegative;

        return valueNegative == signNegative
            ? value
            : value.Negate();
    }

    public static Float128 Max(Float128 x, Float128 y)
    {
        if (x.IsNaN || y.IsNaN)
            return NaN;

        if (x > y)
            return x;
        if (y > x)
            return y;

        // IEEE 754: +0 を -0 より優先
        if (x.IsZero && y.IsZero)
            return x.IsNegative ? y : x;

        return x;
    }

    public static Float128 MaxNumber(Float128 x, Float128 y)
    {
        if (x.IsNaN)
            return y;
        if (y.IsNaN)
            return x;

        return Max(x, y);
    }

    public static Float128 Min(Float128 x, Float128 y)
    {
        if (x.IsNaN || y.IsNaN)
            return NaN;

        if (x < y)
            return x;
        if (y < x)
            return y;

        // IEEE 754: -0 を +0 より優先
        if (x.IsZero && y.IsZero)
            return x.IsNegative ? x : y;

        return x;
    }

    public static Float128 MinNumber(Float128 x, Float128 y)
    {
        if (x.IsNaN)
            return y;
        if (y.IsNaN)
            return x;

        return Min(x, y);
    }

    // INumberBase<T>.Sign(T) は、既存のインスタンスプロパティ Sign と
    // 名前が衝突するため明示的インターフェース実装にする。
    static int INumber<Float128>.Sign(Float128 value)
    {
        if (value.IsNaN)
            return 0;

        if (value.IsZero)
            return 0;

        return value.IsNegative ? -1 : 1;
    }
    #endregion INumber

    #region Unary operators
    /// <summary>
    /// Float128 の符号を反転します。
    /// </summary>
    public Float128 Negate()
    {
        return new Float128(_upper ^ SignMask, _lower);
    }
    public static Float128 operator +(Float128 value)
        => value;

    public static Float128 operator -(Float128 value)
        => value.Negate();
    #endregion Unary operators

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

    #region Special value parsing
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
    #endregion Special value parsing

    #region 他の型からの変換
    /// <summary>
    /// Decimal から Float128 に変換します。
    /// </summary>
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
    /// <param name="negative">符号</param>
    /// <param name="numerator">割られる数。1.5の場合15</param>
    /// <param name="denominator">割る数。1.5の場合10</param>
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
            var shiftedDenominator = denominator << exponent;
            if (numerator < shiftedDenominator)
            {
                exponent--;
            }
        }
        else
        {
            var shiftedNumerator = numerator << (-exponent);
            if (shiftedNumerator < denominator)
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
        // --------------------------------------------------------
        // quotient contains:
        // 113bitに仮数を整形
        //
        // [113 significant][G][R]
        //
        // remainder is the sticky information.
        // --------------------------------------------------------
        bool sticky = remainder != 0;
        bool guard = ((quotient >> 2) & 1) != 0;
        bool round = ((quotient >> 1) & 1) != 0;
        sticky |= (quotient & 1) != 0;
        // 余分な3ビットを削除
        quotient >>= ExtraBits;

        if (guard && (round || sticky || !quotient.IsEven))
        {
            quotient++;
        }

        // --------------------------------------------------------
        // Rounding overflow
        // --------------------------------------------------------
        while (quotient > (BigInteger.One << Precision))
        {
            quotient >>= 1;
            exponent++;
        }
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
    /// <param name="significand">仮数(最上位の1を含めた数値、1.0＝1<<112)</param>
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
            BigInteger fraction = significand - (BigInteger.One << FractionBits);
            if (fraction < 0 || fraction >= (BigInteger.One << FractionBits))
            {
                throw new ArgumentOutOfRangeException(nameof(significand), "The significand is out of range for a normal number.");
            }
            ulong fractionHigh = unchecked((ulong)(fraction >> 64));
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
    #endregion 他の型からの変換

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

    #region ToString
#if true
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
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToDouble().ToString(format, formatProvider);
    }
#else
        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        public override string ToString()
        {
            return ToString(null, null);
        }
        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <param name="format">形式指定子</param>
        /// <param name="formatProvider">形式プロバイダー</param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            format ??= "G";

            ParseFormat(format.AsSpan(), out char formatChar, out int precision);

            switch (formatChar)
            {
                case 'G':
                case 'g':
                    return ToStringGeneral(formatProvider);

                case 'E':
                    return ToStringExponential(formatProvider, upperCase: true);

                case 'e':
                    return ToStringExponential(formatProvider, upperCase: false);

                case 'F':
                case 'f':
                    if (precision < 0)
                        precision = 2;

                    return ToStringFixed(precision, formatProvider);

            }

            throw new FormatException($"The format '{formatChar}' is not supported.");
        }
#endif
    /// <summary>
    /// 指数表記で文字列に変換します。
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="upperCase"></param>
    /// <returns></returns>
    public string ToStringExponential(IFormatProvider? provider, bool upperCase)
    {
        // E / e のデフォルト精度は6桁
        return ToStringExponential(6, provider, upperCase);
    }
    /// <summary>
    /// 指数表記で文字列に変換します。
    /// </summary>
    /// <param name="precision">精度 E3 の 3 は有効桁数3ではありません。</param>
    /// <param name="provider">形式プロバイダー</param>
    /// <param name="upperCase">大文字を使用するかどうか</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string ToStringExponential(int precision, IFormatProvider? provider, bool upperCase)
    {
        if (precision < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(precision));
        }

        if (IsNaN)
            return "NaN";

        if (IsInfinity)
        {
            return IsNegative
                ? "-Infinity"
                : "Infinity";
        }

        BigInteger significand =
            GetRawSignificand();

        /*
         * ±0
         */
        if (significand.IsZero)
        {
            string zero = precision == 0 ? "0" : "0." + new string('0', precision);
            string result = zero + FormatExponent(0, upperCase);
            return IsNegative ? "-" + result : result;
        }

        /*
         * Float128の値を
         *
         * significand × 2^binaryExponent
         *
         * として表す。
         */
        int binaryExponent = Exponent - FractionBits;

        BigInteger numerator;
        BigInteger denominator;

        if (binaryExponent >= 0)
        {
            numerator = significand << binaryExponent;
            denominator = BigInteger.One;
        }
        else
        {
            numerator = significand;
            denominator = BigInteger.One << -binaryExponent;
        }

        /*
         * 10進指数を求める。
         *
         * 例えば
         *
         * 123456.789
         *
         * → 5
         *
         * 0.001234
         *
         * → -3
         */
        int decimalExponent = EstimateDecimalExponent(numerator, denominator);

        /*
         * 必要な有効桁数。
         *
         * E3
         *     1 + 小数3桁
         *
         * なので4桁必要。
         */
        int significantDigits = precision + 1;

        /*
         * 最上位桁を1の位にする。
         *
         * value =
         *
         *     1.xxxxx × 10^decimalExponent
         *
         * となるようにスケールする。
         */
        int scaleExponent = significantDigits - 1 - decimalExponent;

        BigInteger rounded;

        if (scaleExponent >= 0)
        {
            BigInteger scale = BigInteger.Pow(10, scaleExponent);
            rounded = RoundToNearestEven(numerator * scale, denominator);
        }
        else
        {
            BigInteger scale = BigInteger.Pow(10, -scaleExponent);
            rounded = RoundToNearestEven(numerator, denominator * scale);
        }

        /*
         * 丸めによって
         *
         * 9.999...
         *
         * → 10.000...
         *
         * となった場合。
         */
        BigInteger upperLimit = BigInteger.Pow(10, significantDigits);

        if (rounded >= upperLimit)
        {
            rounded /= 10;
            decimalExponent++;
        }

        /*
         * 必要な桁数まで0で埋める。
         */
        string digits = rounded.ToString(CultureInfo.InvariantCulture);

        if (digits.Length < significantDigits)
        {
            digits = digits.PadLeft(significantDigits, '0');
        }

        /*
         * E形式では、最上位1桁と残りを
         * 小数点で分ける。
         */
        string mantissa;
        string separator = GetDecimalSeparator(provider);

        if (precision == 0)
        {
            mantissa = digits[0].ToString();
        }
        else
        {
            mantissa = digits[0] + separator + digits[1..];
        }

        string exponentText = FormatExponent(decimalExponent, upperCase);

        {
            string result = mantissa + exponentText;
            return IsNegative ? "-" + result : result;
        }
    }
    /// <summary>
    /// 固定小数点形式で文字列に変換します。
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public string ToStringFixed(IFormatProvider? provider)
    {
        // E / e のデフォルト精度は6桁
        return ToStringFixed(6, provider);
    }
    /// <summary>
    /// 固定小数点形式で文字列に変換します。
    /// </summary>
    /// <param name="precision">精度</param>
    /// <param name="provider">形式プロバイダー</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string ToStringFixed(int precision, IFormatProvider? provider)
    {
        string separator = GetDecimalSeparator(provider);

        if (precision < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(precision));
        }

        // NaN
        if (IsNaN)
            return "NaN";

        // +Infinity
        if (IsPositiveInfinity)
            return "Infinity";

        // -Infinity
        if (IsNegativeInfinity)
            return "-Infinity";

        // 符号を除いた仮数部
        BigInteger significand = GetRawSignificand();

        // ±0
        if (significand.IsZero)
        {
            string zero;

            if (precision == 0)
            {
                zero = "0";
            }
            else
            {
                zero = "0" + separator + new string('0', precision);
            }

            return IsNegative ? "-" + zero : zero;
        }

        /*
         * Float128 の値:
         *
         *   significand × 2^(Exponent - FractionBits)
         *
         * これを
         *
         *   整数部 + 小数部
         *
         * に変換する。
         */
        int binaryExponent = Exponent - FractionBits;

        BigInteger numerator;
        BigInteger denominator;

        if (binaryExponent >= 0)
        {
            numerator = significand << binaryExponent;
            denominator = BigInteger.One;
        }
        else
        {
            numerator = significand;
            denominator = BigInteger.One << -binaryExponent;
        }

        /*
         * 小数点以下 precision 桁まで残す。
         *
         * 例えば F2 なら
         *
         *   123.456
         *
         * を
         *
         *   12345.6
         *
         * 相当まで整数化してから丸める。
         */
        BigInteger scale = BigInteger.Pow(10, precision);
        BigInteger scaledNumerator = numerator * scale;

        /*
         * Round to nearest, ties to even
         */
        BigInteger rounded = RoundToNearestEven(scaledNumerator, denominator);

        /*
         * 例えば F2 で
         *
         *   12345
         *
         * なら
         *
         *   whole = 123
         *   fraction = 45
         *
         * となる。
         */
        BigInteger whole = rounded / scale;
        BigInteger fraction = rounded % scale;
        string wholeText = whole.ToString(CultureInfo.InvariantCulture);

        string result;

        if (precision == 0)
        {
            result = wholeText;
        }
        else
        {
            string fractionText = fraction.ToString(CultureInfo.InvariantCulture).PadLeft(precision, '0');
            result = wholeText + separator + fractionText;
        }

        if (IsNegative)
        {
            result = "-" + result;
        }

        return result;
    }
#if false
        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            string formatString = format.IsEmpty
                ? "G"
                : format.ToString();

            string result = ToString(formatString, provider);

            if (result.Length > destination.Length)
            {
                charsWritten = 0;
                return false;
            }

            result.AsSpan().CopyTo(destination);

            charsWritten = result.Length;
            return true;
        }
#else
    /// <summary>
    /// 指定された形式で文字列に変換します。
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="charsWritten"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        ParseFormat(format, out char formatChar, out int precision);

        switch (formatChar)
        {
            case 'G':
            case 'g':
                return TryFormatGeneral(destination, out charsWritten, precision, provider);

            case 'E':
            case 'e':
                return TryFormatExponential(destination, out charsWritten, precision, formatChar == 'E', provider);

            case 'F':
            case 'f':
                return TryFormatFixed(destination, out charsWritten, precision, provider);

            default:
                charsWritten = 0;
                throw new FormatException($"The format '{formatChar}' is not supported.");
        }
    }
#endif
    /// <summary>
    /// ReadOnlySpan<char> 用のフォーマット解析
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatChar"></param>
    /// <param name="precision"></param>
    /// <exception cref="FormatException"></exception>
    private static void ParseFormat(ReadOnlySpan<char> format, out char formatChar, out int precision)
    {
        if (format.IsEmpty)
        {
            formatChar = 'G';
            precision = -1;
            return;
        }

        formatChar = format[0];

        if (format.Length == 1)
        {
            precision = -1;
            return;
        }

        int value = 0;

        for (int i = 1; i < format.Length; i++)
        {
            char c = format[i];

            if (c < '0' || c > '9')
            {
                throw new FormatException($"The format '{format.ToString()}' is invalid.");
            }

            int digit = c - '0';

            if (value > (int.MaxValue - digit) / 10)
            {
                throw new FormatException("The format precision is too large.");
            }

            value = value * 10 + digit;
        }

        precision = value;
    }
    /// <summary>
    /// F / f 形式を直接 Span<char> に出力
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="charsWritten"></param>
    /// <param name="precision"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private bool TryFormatFixed(Span<char> destination, out int charsWritten, int precision, IFormatProvider? provider)
    {
        if (precision < 0)
            precision = 2;

        string text = ToStringFixed(precision, provider);

        if (text.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        text.AsSpan().CopyTo(destination);
        charsWritten = text.Length;
        return true;
    }
    /// <summary>
    /// numerator / denominator を四捨五入して最も近い偶数に丸めます。
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static BigInteger RoundToNearestEven(BigInteger numerator, BigInteger denominator)
    {
        if (numerator < 0)
            throw new ArgumentOutOfRangeException(nameof(numerator));

        if (denominator <= 0)
            throw new ArgumentOutOfRangeException(nameof(denominator));

        BigInteger quotient = BigInteger.DivRem(numerator, denominator, out BigInteger remainder);

        // 完全に割り切れる
        if (remainder.IsZero)
            return quotient;

        /*
         * remainder / denominator が
         *
         * 0.5 より小さい → 切り捨て
         * 0.5 より大きい → 切り上げ
         * ちょうど0.5     → 偶数へ
         *
         * とする。
         */

        int comparison = (remainder << 1).CompareTo(denominator);

        if (comparison < 0)
        {
            // 0.5未満
            return quotient;
        }

        if (comparison > 0)
        {
            // 0.5超
            return quotient + BigInteger.One;
        }

        // ちょうど0.5
        //
        // quotientが奇数なら+1して偶数にする。
        // quotientが偶数ならそのまま。
        return quotient.IsEven
            ? quotient
            : quotient + BigInteger.One;
    }
    /// <summary>
    /// 通常表記の文字列を生成します。
    /// </summary>
    /// <param name="digits"></param>
    /// <param name="decimalExponent"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static string FormatGeneralFixed(string digits, int decimalExponent, IFormatProvider? provider)
    {
        string separator = GetDecimalSeparator(provider);

        /*
         * decimalExponent は最上位桁の10進指数。
         *
         * 例えば
         *
         * digits = "123456"
         * decimalExponent = 5
         *
         * → 123456
         */

        int decimalPosition = decimalExponent + 1;

        if (decimalPosition >= digits.Length)
        {
            return digits + new string('0', decimalPosition - digits.Length);
        }

        if (decimalPosition <= 0)
        {
            return "0" + separator + new string('0', -decimalPosition) + digits;
        }

        return digits[..decimalPosition] + separator + digits[decimalPosition..];
    }
    /// <summary>
    /// 指数表記の文字列を生成します。
    /// </summary>
    /// <param name="digits"></param>
    /// <param name="decimalExponent"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static string FormatGeneralExponential(string digits, int decimalExponent, IFormatProvider? provider)
    {
        string separator = GetDecimalSeparator(provider);

        string mantissa;

        if (digits.Length == 1)
        {
            mantissa = digits;
        }
        else
        {
            mantissa = digits[0] + separator + digits[1..];
        }

        char exponentSign = decimalExponent >= 0 ? '+' : '-';
        int exponentValue = int.Abs(decimalExponent);

        string exponentDigits = exponentValue.ToString("D3", CultureInfo.InvariantCulture);

        return mantissa + "E" + exponentSign + exponentDigits;
    }
    /// <summary>
    /// G 形式を直接 Span<char> に出力
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="charsWritten"></param>
    /// <param name="precision"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private bool TryFormatGeneral(Span<char> destination, out int charsWritten, int precision, IFormatProvider? provider)
    {
        // 精度未指定なら既存の正確な ToString() を使用
        string text = precision < 0
            ? ToStringExactDecimal(provider)
            : ToStringGeneralWithPrecision(precision, provider);

        if (text.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        text.AsSpan().CopyTo(destination);
        charsWritten = text.Length;
        return true;
    }
    /// <summary>
    /// E / e 形式を直接 Span<char> に出力
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="charsWritten"></param>
    /// <param name="precision"></param>
    /// <param name="upperCase"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private bool TryFormatExponential(Span<char> destination, out int charsWritten, int precision, bool upperCase, IFormatProvider? provider)
    {
        if (precision < 0)
            precision = 6;

        string text = FormatExponential(precision, upperCase, provider);

        if (text.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        text.AsSpan().CopyTo(destination);
        charsWritten = text.Length;
        return true;
    }
    /// <summary>
    /// G 形式の文字列を生成します。
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private string ToStringExactDecimal(IFormatProvider? provider)
    {
        if (IsNaN)
            return "NaN";

        if (IsInfinity)
        {
            return IsNegative
                ? "-Infinity"
                : "Infinity";
        }

        BigInteger significand = GetRawSignificand();

        if (significand.IsZero)
        {
            return IsNegative ? "-0" : "0";
        }

        int exponent = Exponent - FractionBits;

        string result;

        if (exponent >= 0)
        {
            /*
             * value =
             *
             * significand × 2^exponent
             *
             * なので、単純に左シフトする。
             */
            BigInteger integerValue = significand << exponent;
            result = integerValue.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            /*
             * value =
             *
             * significand / 2^(-exponent)
             *
             * となる。
             *
             * 2^n の分母を10進数に変換するため、
             *
             * 1 / 2^n
             *
             * を
             *
             * 5^n / 10^n
             *
             * と変形する。
             *
             * したがって、
             *
             * significand / 2^n
             *
             * =
             *
             * significand × 5^n / 10^n
             */

            int shift = -exponent;
            BigInteger numerator = significand * BigInteger.Pow(5, shift);
            string digits = numerator.ToString(CultureInfo.InvariantCulture);

            /*
             * 分母は10^shiftなので、
             * 小数点は右からshift桁の位置。
             */

            if (digits.Length <= shift)
            {
                digits = digits.PadLeft(shift + 1, '0');
            }

            int decimalPosition = digits.Length - shift;
            string integerPart = digits[..decimalPosition];
            string fractionPart = digits[decimalPosition..];

            /*
             * 末尾の0は10進表現として不要。
             */
            fractionPart = fractionPart.TrimEnd('0');

            if (fractionPart.Length == 0)
            {
                result = integerPart;
            }
            else
            {
                string separator = GetDecimalSeparator(provider);
                result = integerPart + separator + fractionPart;
            }
        }

        if (IsNegative)
        {
            return "-" + result;
        }

        return result;
    }
    private string ToStringGeneral(IFormatProvider? provider)
    {
        return ToStringGeneralWithPrecision(34, provider);
    }
    /// <summary>
    /// G 形式の文字列を生成します。
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string ToStringGeneralWithPrecision(int precision, IFormatProvider? provider)
    {
        if (precision < 1 || precision > 34)
        {
            throw new ArgumentOutOfRangeException(nameof(precision), "Float128 general format precision must be between 1 and 34.");
        }

        if (IsNaN)
            return "NaN";

        if (IsPositiveInfinity)
            return "Infinity";

        if (IsNegativeInfinity)
            return "-Infinity";

        if (IsZero)
            return IsNegative ? "-0" : "0";

        bool negative = IsNegative;

        /*
         * 絶対値を
         *
         * numerator / denominator
         *
         * として取得する。
         */
        BigInteger significand = GetRawSignificand();

        int binaryExponent = Exponent - FractionBits;

        BigInteger numerator;
        BigInteger denominator;

        if (binaryExponent >= 0)
        {
            numerator = significand << binaryExponent;
            denominator = BigInteger.One;
        }
        else
        {
            numerator = significand;
            denominator = BigInteger.One << -binaryExponent;
        }

        /*
         * 10進指数を求める。
         *
         * 例えば
         *
         * 123456.789
         *
         * なら
         *
         * decimalExponent = 5
         *
         * となる。
         */
        int decimalExponent = EstimateDecimalExponent(numerator, denominator);

        /*
         * precision桁になるようにスケーリングする。
         *
         * 例えば precision=6,
         * decimalExponent=5 なら
         *
         * 123456.789
         *
         * → 1.23456... × 10^5
         *
         * なので、
         *
         * 1.23456
         *
         * の6桁を取得する。
         */
        int scaleExponent = precision - 1 - decimalExponent;

        BigInteger rounded;

        if (scaleExponent >= 0)
        {
            BigInteger scale = BigInteger.Pow(10, scaleExponent);
            rounded = RoundToNearestEven(numerator * scale, denominator);
        }
        else
        {
            BigInteger scale = BigInteger.Pow(10, -scaleExponent);
            rounded = RoundToNearestEven(numerator, denominator * scale);
        }

        /*
         * 丸めによって
         *
         * 9.99999...
         *
         * が
         *
         * 10.0000...
         *
         * になった場合。
         */
        BigInteger precisionLimit = BigInteger.Pow(10, precision);

        if (rounded >= precisionLimit)
        {
            rounded /= 10;
            decimalExponent++;
        }

        string digits = rounded.ToString(CultureInfo.InvariantCulture);

        /*
         * precision桁になるように左側を0で埋める。
         */
        if (digits.Length < precision)
        {
            digits = digits.PadLeft(precision, '0');
        }

        /*
         * General形式では末尾の0を削除する。
         *
         * 例:
         *
         * 1.23000
         * ↓
         * 1.23
         */
        int lastDigit = digits.Length - 1;

        while (lastDigit > 0 && digits[lastDigit] == '0')
        {
            lastDigit--;
        }

        digits = digits[..(lastDigit + 1)];

        /*
         * General形式で指数表記を使用するか決定する。
         *
         * .NETのG形式に近いルールとして、
         *
         * 10^-4 未満
         *
         * または
         *
         * 10^precision 以上
         *
         * の場合は指数形式にする。
         */
        bool useExponent = decimalExponent < -4 || decimalExponent >= precision;

        string result;

        if (useExponent)
        {
            result = FormatGeneralExponential(digits, decimalExponent, provider);
        }
        else
        {
            result = FormatGeneralFixed(digits, decimalExponent, provider);
        }

        if (IsNegative)
        {
            return "-" + result;
        }

        return result;
    }
    /// <summary>
    /// 指数表記の文字列を生成します。
    /// </summary>
    /// <param name="precision"> </param>
    /// <param name="upperCase"> </param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private string FormatExponential(int precision, bool upperCase, IFormatProvider? provider)
    {
        if (IsNaN)
            return "NaN";

        if (IsInfinity)
        {
            return IsNegative
                ? "-Infinity"
                : "Infinity";
        }

        var separator = GetDecimalSeparator(provider);

        if (IsZero)
        {
            string zero =
                precision == 0 ? "0" : "0" + separator + new string('0', precision);
            return zero + (upperCase ? "E+000" : "e+000");
        }

        /*
         * Float128:
         *
         * value =
         *
         * significand × 2^(exponent - 112)
         */

        BigInteger significand = GetRawSignificand();

        int binaryExponent = Exponent - FractionBits;

        BigInteger numerator;
        BigInteger denominator;

        if (binaryExponent >= 0)
        {
            numerator = significand << binaryExponent;
            denominator = BigInteger.One;
        }
        else
        {
            numerator = significand;
            denominator = BigInteger.One << (-binaryExponent);
        }

        /*
         * まず10進表現を作る。
         *
         * ここではprecision+1桁程度を取得して、
         * 最終的な丸めを行う。
         */

        int decimalExponent = EstimateDecimalExponent(numerator, denominator);

        /*
         * value / 10^decimalExponent
         *
         * を求める。
         *
         * 例えば
         *
         * 123456.789
         *
         * なら
         *
         * 1.23456789 × 10^5
         *
         * なので decimalExponent = 5。
         */
        int digitsAfterDecimal = precision;
        int significantDigits = precision + 1;
        BigInteger scale = BigInteger.Pow(10, significantDigits - 1);
        BigInteger scaled;
        if (decimalExponent >= 0)
        {
            BigInteger power = BigInteger.Pow(10, decimalExponent);
            scaled = RoundToNearestEven(numerator * scale, denominator * power);
        }
        else
        {
            BigInteger power = BigInteger.Pow(10, -decimalExponent);
            scaled = RoundToNearestEven(numerator * scale * power, denominator);
        }

        /*
         * 丸めによって
         *
         * 9.999... → 10.000...
         *
         * になる場合がある。
         */

        BigInteger limit = BigInteger.Pow(10, significantDigits);

        if (scaled >= limit)
        {
            scaled /= 10;
            decimalExponent++;
        }

        string digits = scaled.ToString(CultureInfo.InvariantCulture);

        // 桁数が足りない場合は0で埋める。
        if (digits.Length < significantDigits)
        {
            digits = digits.PadLeft(significantDigits, '0');
        }

        string mantissa;

        if (digitsAfterDecimal == 0)
        {
            mantissa = digits;
        }
        else
        {
            mantissa = digits[0].ToString() + separator + digits.Substring(1);
        }

        string exponentText = FormatExponent(decimalExponent, upperCase);

        string result = mantissa + exponentText;

        if (IsNegative)
        {
            return "-" + result;
        }

        return result;
    }
    /// <summary>
    /// 10進指数を求める
    /// </summary>
    /// <param name="numerator"> 分子 </param>
    /// <param name="denominator"> 分母 </param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static int EstimateDecimalExponent(BigInteger numerator, BigInteger denominator)
    {
        if (numerator <= 0)
            throw new ArgumentOutOfRangeException(nameof(numerator));

        if (denominator <= 0)
            throw new ArgumentOutOfRangeException(nameof(denominator));

        /*
         * まずbit lengthから
         *
         * log10(value)
         *
         * の近似値を求める。
         */

        int numeratorBits = GetBitLength(numerator);
        int denominatorBits = GetBitLength(denominator);
        int binaryExponent = numeratorBits - denominatorBits;
        double approximate = binaryExponent * 0.30102999566398119521;
        int exponent = (int)double.Floor(approximate);

        /*
         * 近似なので、最終的にはBigIntegerによる
         * 正確な比較で補正する。
         */

        while (CompareWithPowerOfTen(numerator, denominator, exponent) < 0)
        {
            exponent--;
        }

        while (CompareWithPowerOfTen(numerator, denominator, exponent + 1) >= 0)
        {
            exponent++;
        }

        return exponent;
    }
    /// <summary>
    /// 10のべき乗との比較
    /// </summary>
    /// <param name="numerator"> 分子 </param>
    /// <param name="denominator"> 分母 </param>
    /// <param name="exponent"> 指数 </param>
    /// <returns></returns>
    private static int CompareWithPowerOfTen(BigInteger numerator, BigInteger denominator, int exponent)
    {
        /*
         * numerator / denominator
         *
         * と
         *
         * 10^exponent
         *
         * を比較する。
         */

        if (exponent >= 0)
        {
            BigInteger power = BigInteger.Pow(10, exponent);
            return numerator.CompareTo(denominator * power);
        }
        else
        {
            BigInteger power = BigInteger.Pow(10, -exponent);

            /*
             * numerator / denominator
             *
             * と
             *
             * 1 / 10^(-exponent)
             *
             * の比較。
             *
             * numerator * 10^(-exponent)
             * と denominator を比較すればよい。
             */

            return (numerator * power).CompareTo(denominator);
        }
    }
    /// <summary>
    /// 指数部分の生成
    /// </summary>
    /// <param name="exponent">指数 </param>
    /// <param name="upperCase">大文字を使用するかどうか </param>
    /// <returns></returns>
    private static string FormatExponent(int exponent, bool upperCase)
    {
        string e = upperCase ? "E" : "e";
        string sign = exponent >= 0 ? "+" : "-";
        int magnitude = int.Abs(exponent);

        /*
         * .NETのE形式では指数を少なくとも3桁で
         * 表示するのが一般的。
         *
         * 例:
         *
         * E+005
         * E-010
         * E+100
         */

        string digits = magnitude.ToString("D3", CultureInfo.InvariantCulture);
        return e + sign + digits;
    }
    /// <summary>
    /// 小数点記号
    /// F と同様に IFormatProvider に対応させます。
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static string GetDecimalSeparator(IFormatProvider? provider)
    {
        NumberFormatInfo info = NumberFormatInfo.GetInstance(provider);
        return info.NumberDecimalSeparator;
    }
    #endregion ToString
}