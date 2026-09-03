using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics;
/// <summary>
/// 符号：1 bit
/// 指数：15 bit
/// 仮数：112 bit（隠れた1を含めると113bit精度）
/// 合計：128 bit
/// 約34桁の10進精度
/// 指数範囲：約 ±4932
/// </summary>
public readonly struct Float128
{
    #region Constants
    public static Float128 PositiveZero => new Float128(UInt128.Zero);

    public static Float128 NegativeZero => new Float128(UInt128.One << 127);
    public static Float128 PositiveInfinity
    {
        get
        {
            UInt128 bits =(UInt128)MaxExponent << FractionBits;

            return new Float128(bits);
        }
    }

    public static Float128 NegativeInfinity
    {
        get
        {
            UInt128 bits =
                (UInt128.One << 127) |
                ((UInt128)MaxExponent << FractionBits);

            return new Float128(bits);
        }
    }

    public static Float128 NaN
    {
        get
        {
            UInt128 bits =
                ((UInt128)MaxExponent << FractionBits) |
                UInt128.One;

            return new Float128(bits);
        }
    }

    private const int ExponentBits = 15;
    private const int FractionBits = 112;

    private const int ExponentBias = 16383;

    private const ushort MaxExponent = 0x7FFF;

    private static readonly UInt128 FractionMask = (UInt128.One << FractionBits) - 1;
    #endregion Constants

    #region Fields
    private readonly UInt128 _bits;
    #endregion Fields

    public Float128(UInt128 bits)
    {
        _bits = bits;
    }

    public Float128(UInt64 upper, UInt64 lower)
    {
        _bits = new UInt128(upper, lower);
    }

    #region Properties

    public UInt128 Bits => _bits;
    /// <summary>
    /// 負数の場合はtrueを返す
    /// </summary>
    public bool IsNegative
    {
        get
        {
            return (_bits & (UInt128.One << 127)) != 0;
        }
    }
    /// <summary>
    /// 負数の場合は-1、正数の場合は1を返す
    /// </summary>
    public int Sign
    {
        get
        {
            return IsNegative ? -1 : 1;
        }
    }
    /// <summary>
    /// 指数部の値を返す（バイアスあり）
    /// </summary>
    public ushort BiasedExponent
    {
        get
        {
            return (ushort)((_bits >> FractionBits) & MaxExponent);
        }
    }
    /// <summary>
    /// 仮数部の値を返す（隠れた1は含まない）
    /// </summary>
    public UInt128 Fraction
    {
        get
        {
            return _bits & FractionMask;
        }
    }
    /// <summary>
    /// 値がゼロの場合はtrueを返す
    /// </summary>
    public bool IsZero
    {
        get
        {
            return BiasedExponent == 0 && Fraction == 0;
        }
    }
    /// <summary>
    /// 値が無限大の場合はtrueを返す
    /// </summary>
    public bool IsInfinity
    {
        get
        {
            return BiasedExponent == MaxExponent && Fraction == 0;
        }
    }
    /// <summary>
    /// 値がNaNの場合はtrueを返す
    /// </summary>
    public bool IsNaN
    {
        get
        {
            return BiasedExponent == MaxExponent && Fraction != 0;
        }
    }
    /// <summary>
    /// 値が非正規化数の場合はtrueを返す
    /// </summary>
    public bool IsSubnormal
    {
        get
        {
            return BiasedExponent == 0 && Fraction != 0;
        }
    }
    /// <summary>
    /// 値が正規化数の場合はtrueを返す
    /// </summary>
    public bool IsNormal
    {
        get
        {
            return BiasedExponent != 0 && BiasedExponent != MaxExponent;
        }
    }
    #endregion Properties

    public static Float128 FromInt64(long value)
    {
        if (value == 0)
            return PositiveZero;

        bool negative = value < 0;

        ulong magnitude;

        if (negative)
        {
            magnitude = (ulong)(-(value + 1));
            magnitude += 1;
        }
        else
        {
            magnitude = (ulong)value;
        }

        // 最上位の1bit
        int exponent = BitOperations.Log2(magnitude);

        // 先頭の1を除去
        ulong significand = magnitude ^ (1UL << exponent);

        // Fraction(112bit)の上位側に配置
        UInt128 fraction =(UInt128)significand << (FractionBits - exponent);

        // Exponent
        UInt128 bits =
            ((UInt128)(exponent + ExponentBias) << FractionBits)
            | fraction;

        // Sign
        if (negative)
            bits |= UInt128.One << 127;

        return new Float128(bits);
    }

    public long ToInt64()
    {
        if (IsNaN)
            throw new OverflowException("NaN cannot be converted to Int64.");

        if (IsInfinity)
            throw new OverflowException("Infinity cannot be converted to Int64.");

        if (IsZero)
            return 0;

        int exponent = BiasedExponent - ExponentBias;

        // 0 < |value| < 1
        if (exponent < 0)
            return 0;

        // |value| >= 2^64
        if (exponent >= 64)
            throw new OverflowException("Value is outside Int64 range.");

        UInt128 significand =
            (UInt128.One << FractionBits) | Fraction;

        int shift = exponent - FractionBits;

        UInt128 magnitude;

        if (shift >= 0)
        {
            magnitude = significand << shift;
        }
        else
        {
            magnitude = significand >> -shift;
        }

        if (IsNegative)
        {
            // -2^63 までは long に入る
            if (magnitude > (UInt128)1 << 63)
                throw new OverflowException("Value is outside Int64 range.");

            if (magnitude == ((UInt128)1 << 63))
                return long.MinValue;

            return -(long)magnitude;
        }
        else
        {
            if (magnitude > long.MaxValue)
                throw new OverflowException("Value is outside Int64 range.");

            return (long)magnitude;
        }
    }
}