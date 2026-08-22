using System;
using System.Numerics;

namespace Ksnm.Cryptography.Ecc;

public sealed class EllipticCurve
{
    /// <summary>
    /// y² = x³ + ax + b (mod p)
    /// </summary>
    public BigInteger A { get; }

    public BigInteger B { get; }

    /// <summary>
    /// 有限体の法 p
    /// </summary>
    public BigInteger P { get; }

    public EcPoint Infinity => EcPoint.Infinity;

    public EllipticCurve(
        BigInteger a,
        BigInteger b,
        BigInteger p)
    {
        if (p <= 2)
            throw new ArgumentOutOfRangeException(nameof(p));

        P = p;
        A = Mod(a);
        B = Mod(b);

        // 4a³ + 27b² != 0 (mod p)
        BigInteger discriminant =
            Mod(4 * A * A * A + 27 * B * B);

        if (discriminant == 0)
            throw new ArgumentException(
                "特異な楕円曲線です。");
    }

    /// <summary>
    /// 値を 0 ～ p-1 に正規化する。
    /// </summary>
    public BigInteger Mod(BigInteger value)
    {
        value %= P;

        if (value < 0)
            value += P;

        return value;
    }

    /// <summary>
    /// 点が楕円曲線上に存在するか。
    /// </summary>
    public bool IsOnCurve(EcPoint point)
    {
        if (point.IsInfinity)
            return true;

        BigInteger left =
            Mod(point.Y * point.Y);

        BigInteger right =
            Mod(
                point.X * point.X * point.X
                + A * point.X
                + B);

        return left == right;
    }

    private static BigInteger ModInverse(
    BigInteger value,
    BigInteger modulus)
    {
        value %= modulus;

        if (value < 0)
            value += modulus;

        BigInteger oldR = value;
        BigInteger r = modulus;

        BigInteger oldS = 1;
        BigInteger s = 0;

        while (r != 0)
        {
            BigInteger q = oldR / r;

            (oldR, r) =
                (r, oldR - q * r);

            (oldS, s) =
                (s, oldS - q * s);
        }

        if (oldR != 1)
            throw new ArithmeticException(
                "逆元が存在しません。");

        return (oldS % modulus + modulus) % modulus;
    }

    public EcPoint Add(EcPoint p1, EcPoint p2)
    {
        if (!IsOnCurve(p1))
            throw new ArgumentException(
                "p1 が楕円曲線上にありません。",
                nameof(p1));

        if (!IsOnCurve(p2))
            throw new ArgumentException(
                "p2 が楕円曲線上にありません。",
                nameof(p2));

        // O + P = P
        if (p1.IsInfinity)
            return p2;

        // P + O = P
        if (p2.IsInfinity)
            return p1;

        // x が同じで y が逆符号
        //
        // P + (-P) = O
        if (p1.X == p2.X &&
            Mod(p1.Y + p2.Y) == 0)
        {
            return Infinity;
        }

        BigInteger lambda;

        if (p1 == p2)
        {
            // 接線の傾き
            //
            // λ = (3x1² + a) / 2y1
            BigInteger numerator =
                Mod(3 * p1.X * p1.X + A);

            BigInteger denominator =
                Mod(2 * p1.Y);

            lambda =
                Mod(
                    numerator *
                    ModInverse(denominator, P));
        }
        else
        {
            // λ = (y2 - y1) / (x2 - x1)
            BigInteger numerator =
                Mod(p2.Y - p1.Y);

            BigInteger denominator =
                Mod(p2.X - p1.X);

            lambda =
                Mod(
                    numerator *
                    ModInverse(denominator, P));
        }

        BigInteger x3 =
            Mod(lambda * lambda - p1.X - p2.X);

        BigInteger y3 =
            Mod(lambda * (p1.X - x3) - p1.Y);

        return new EcPoint(x3, y3);
    }

    public EcPoint Multiply(
    EcPoint point,
    BigInteger scalar)
    {
        if (!IsOnCurve(point))
            throw new ArgumentException(
                "点が楕円曲線上にありません。",
                nameof(point));

        if (scalar < 0)
            throw new ArgumentOutOfRangeException(
                nameof(scalar));

        EcPoint result = Infinity;
        EcPoint current = point;

        while (scalar > 0)
        {
            if (!scalar.IsEven)
            {
                result = Add(result, current);
            }

            current = Add(current, current);

            scalar >>= 1;
        }

        return result;
    }
}