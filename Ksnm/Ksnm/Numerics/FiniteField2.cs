namespace Ksnm.Numerics;

/// <summary>
/// F₂ = GF(2) の元を表します。
/// 要素は 0 または 1 です。
/// </summary>
public readonly struct FiniteField2 :
    IEquatable<FiniteField2>
{
    private readonly byte _value;

    public FiniteField2(byte value)
    {
        _value = (byte)(value & 1);
    }

    public int Value => _value;

    public static readonly FiniteField2 Zero = new(0);
    public static readonly FiniteField2 One = new(1);

    public static FiniteField2 operator +(FiniteField2 a, FiniteField2 b)
        => new((byte)(a._value ^ b._value));

    public static FiniteField2 operator -(FiniteField2 a, FiniteField2 b)
        => a + b;

    public static FiniteField2 operator *(FiniteField2 a, FiniteField2 b)
        => new((byte)(a._value & b._value));

    public static FiniteField2 operator /(FiniteField2 a, FiniteField2 b)
    {
        if (b._value == 0)
            throw new DivideByZeroException();

        return a;
    }

    public static FiniteField2 operator -(FiniteField2 value)
        => value;

    public bool Equals(FiniteField2 other)
        => _value == other._value;

    public override bool Equals(object? obj)
        => obj is FiniteField2 other && Equals(other);

    public override int GetHashCode()
        => _value;

    public override string ToString()
        => _value.ToString();

    public static bool operator ==(FiniteField2 left, FiniteField2 right)
        => left._value == right._value;

    public static bool operator !=(FiniteField2 left, FiniteField2 right)
        => left._value != right._value;

    public static implicit operator FiniteField2(byte value)
        => new(value);

    public static implicit operator byte(FiniteField2 value)
        => value._value;
}