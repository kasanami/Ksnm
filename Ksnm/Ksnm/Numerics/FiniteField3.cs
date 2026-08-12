using System.Diagnostics;

namespace Ksnm.Numerics;

/// <summary>
/// F₃ = GF(3) の元を表します。
/// 要素は 0, 1, 2 です。
/// </summary>
public readonly struct FiniteField3 :
    IEquatable<FiniteField3>
{
    private readonly byte _value;

    public FiniteField3(byte value)
    {
        _value = (byte)(value % 3);
    }

    public int Value => _value;

    public static readonly FiniteField3 Zero = new(0);
    public static readonly FiniteField3 One = new(1);
    public static readonly FiniteField3 Two = new(2);

    public static FiniteField3 operator +(FiniteField3 a, FiniteField3 b)
        => new((byte)((a._value + b._value) % 3));

    public static FiniteField3 operator -(FiniteField3 a, FiniteField3 b)
        => new((byte)((a._value + 3 - b._value) % 3));

    public static FiniteField3 operator *(FiniteField3 a, FiniteField3 b)
        => new((byte)((a._value * b._value) % 3));

    public static FiniteField3 operator /(FiniteField3 a, FiniteField3 b)
    {
        if (b._value == 0)
            throw new DivideByZeroException();

        return new((byte)((a._value * MultiplicativeInverse(b._value)) % 3));
    }

    public static FiniteField3 operator -(FiniteField3 value)
        => value._value switch
        {
            0 => Zero,
            1 => Two,
            2 => One,
            _ => throw new UnreachableException()
        };

    private static byte MultiplicativeInverse(byte value)
        => value switch
        {
            1 => 1,
            2 => 2,
            _ => throw new DivideByZeroException()
        };

    public bool Equals(FiniteField3 other)
        => _value == other._value;

    public override bool Equals(object? obj)
        => obj is FiniteField3 other && Equals(other);

    public override int GetHashCode()
        => _value;

    public override string ToString()
        => _value.ToString();

    public static bool operator ==(FiniteField3 left, FiniteField3 right)
        => left._value == right._value;

    public static bool operator !=(FiniteField3 left, FiniteField3 right)
        => left._value != right._value;

    public static implicit operator FiniteField3(byte value)
        => new(value);

    public static implicit operator byte(FiniteField3 value)
        => value._value;
}