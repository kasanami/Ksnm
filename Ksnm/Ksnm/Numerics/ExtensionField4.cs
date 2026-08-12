using System.Diagnostics;

namespace Ksnm.Numerics;

/// <summary>
/// GF(4) = GF(2²) の元を表します。
///
/// 既約多項式:
///     x² + x + 1
///
/// 要素:
///     0    = 00
///     1    = 01
///     x    = 10
///     x+1  = 11
/// </summary>
public readonly struct ExtensionField4 : IEquatable<ExtensionField4>
{
    private readonly byte _value;

    private const byte Mask = 0b11;

    public ExtensionField4(byte value)
    {
        _value = (byte)(value & Mask);
    }

    public int Value => _value;

    public static readonly ExtensionField4 Zero = new(0b00);
    public static readonly ExtensionField4 One = new(0b01);
    public static readonly ExtensionField4 X = new(0b10);

    /// <summary>
    /// 加算。
    ///
    /// GF(2) 上の多項式の係数なので XOR で計算できます。
    /// </summary>
    public static ExtensionField4 operator +(ExtensionField4 a, ExtensionField4 b)
        => new((byte)(a._value ^ b._value));

    /// <summary>
    /// ExtensionField4 では -a = a です。
    /// </summary>
    public static ExtensionField4 operator -(ExtensionField4 value)
        => value;

    /// <summary>
    /// 減算も XOR と同じです。
    /// </summary>
    public static ExtensionField4 operator -(ExtensionField4 a, ExtensionField4 b)
        => new((byte)(a._value ^ b._value));

    /// <summary>
    /// 乗算。
    /// 2ビットの多項式を掛け算したあと、
    /// x² + x + 1 = 0
    /// で剰余を取ります。
    /// </summary>
    public static ExtensionField4 operator *(ExtensionField4 a, ExtensionField4 b)
    {
        byte a0 = (byte)(a._value & 1);
        byte a1 = (byte)((a._value >> 1) & 1);

        byte b0 = (byte)(b._value & 1);
        byte b1 = (byte)((b._value >> 1) & 1);

        // (a1 x + a0)(b1 x + b0)
        //
        // = a1b1 x²
        // + (a1b0 + a0b1)x
        // + a0b0
        byte c0 = (byte)(a0 & b0);
        byte c1 = (byte)((a1 & b0) ^ (a0 & b1));
        byte c2 = (byte)(a1 & b1);

        // x² = x + 1
        //
        // c2 x²
        // = c2 x + c2
        c0 ^= c2;
        c1 ^= c2;

        return new((byte)(c0 | (c1 << 1)));
    }

    /// <summary>
    /// 乗法逆元。
    /// </summary>
    public ExtensionField4 Inverse()
    {
        return _value switch
        {
            0b00 => throw new DivideByZeroException(),

            // 1 × 1 = 1
            0b01 => new(0b01),

            // x × (x+1)
            // = x² + x
            // = (x+1) + x
            // = 1
            0b10 => new(0b11),

            // (x+1) × x = 1
            0b11 => new(0b10),

            _ => throw new UnreachableException()
        };
    }

    public static ExtensionField4 operator /(ExtensionField4 a, ExtensionField4 b)
        => a * b.Inverse();

    public bool Equals(ExtensionField4 other)
        => _value == other._value;

    public override bool Equals(object? obj)
        => obj is ExtensionField4 other && Equals(other);

    public override int GetHashCode()
        => _value;

    public override string ToString()
        => _value switch
        {
            0b00 => "0",
            0b01 => "1",
            0b10 => "x",
            0b11 => "x + 1",
            _ => throw new UnreachableException()
        };

    public static bool operator ==(ExtensionField4 left, ExtensionField4 right)
        => left._value == right._value;

    public static bool operator !=(ExtensionField4 left, ExtensionField4 right)
        => left._value != right._value;

    public static implicit operator ExtensionField4(byte value)
        => new(value);

    public static implicit operator byte(ExtensionField4 value)
        => value._value;
}