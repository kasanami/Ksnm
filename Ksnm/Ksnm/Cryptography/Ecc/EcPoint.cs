
using System;
using System.Numerics;

namespace Ksnm.Cryptography.Ecc;

/// <summary>
/// 素数有限体上の楕円曲線の点
///
/// y² = x³ + ax + b (mod p)
/// </summary>
public readonly struct EcPoint : IEquatable<EcPoint>
{
    public BigInteger X { get; }
    public BigInteger Y { get; }

    /// <summary>
    /// 無限遠点を表す。
    /// </summary>
    public bool IsInfinity { get; }

    public EcPoint(BigInteger x, BigInteger y)
    {
        X = x;
        Y = y;
        IsInfinity = false;
    }

    private EcPoint(bool infinity)
    {
        X = 0;
        Y = 0;
        IsInfinity = infinity;
    }

    /// <summary>
    /// 無限遠点 O
    /// </summary>
    public static EcPoint Infinity => new(true);

    public bool Equals(EcPoint other)
    {
        if (IsInfinity || other.IsInfinity)
            return IsInfinity == other.IsInfinity;

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is EcPoint other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, IsInfinity);
    }

    public static bool operator ==(EcPoint left, EcPoint right)
        => left.Equals(right);

    public static bool operator !=(EcPoint left, EcPoint right)
        => !left.Equals(right);

    public override string ToString()
    {
        return IsInfinity
            ? "O"
            : $"({X}, {Y})";
    }
}