using System.Numerics;

namespace Ksnm.Numerics;

/// <summary>
/// 256ビットの符号なし整数
/// </summary>
public readonly struct UInt256
{
    /// <summary>
    /// 最下位64ビット
    /// </summary>
    public readonly UInt64 U0;
    /// <summary>
    /// 2番目の64ビット
    /// </summary>
    public readonly UInt64 U1;
    /// <summary>
    /// 3番目の64ビット
    /// </summary>
    public readonly UInt64 U2;
    /// <summary>
    /// 最上位64ビット
    /// </summary>
    public readonly UInt64 U3;
    /// <summary>
    /// 256ビットの符号なし整数を生成する。
    /// </summary>
    /// <param name="u3">最上位64ビット</param>
    /// <param name="u2">3番目の64ビット</param>
    /// <param name="u1">2番目の64ビット</param>
    /// <param name="u0">最下位64ビット</param>
    UInt256(UInt64 u3, UInt64 u2, UInt64 u1, UInt64 u0)
    {
        U0 = u0;
        U1 = u1;
        U2 = u2;
        U3 = u3;
    }

    public static UInt256 FromUInt64(UInt64 value)
    {
        return new UInt256(0, 0, 0, value);
    }

    public static UInt256 FromUInt128(UInt128 value)
    {
        return new UInt256(0, 0, (UInt64)(value >> 64), (UInt64)value);
    }

    override public string ToString()
    {
        if (U3 != 0)
            return $"0x{U3:X16}{U2:X16}{U1:X16}{U0:X16}";
        if (U2 != 0)
            return $"0x{U2:X16}{U1:X16}{U0:X16}";
        if (U1 != 0)
            return $"0x{U1:X16}{U0:X16}";
        return $"0x{U0:X16}";
    }

    public UInt128 Low128 =>
        ((UInt128)U1 << 64) | U0;

    public int BitLength
    {
        get
        {
            if (U3 != 0)
                return 192 +
                    (64 - BitOperations.LeadingZeroCount(U3));

            if (U2 != 0)
                return 128 +
                    (64 - BitOperations.LeadingZeroCount(U2));

            if (U1 != 0)
                return 64 +
                    (64 - BitOperations.LeadingZeroCount(U1));

            if (U0 != 0)
                return 64 -
                    BitOperations.LeadingZeroCount(U0);

            return 0;
        }
    }

    public bool GetBit(int index)
    {
        if (index < 0 || index >= 256)
            return false;

        int word = index >> 6;
        int bit = index & 63;

        ulong value = word switch
        {
            0 => U0,
            1 => U1,
            2 => U2,
            3 => U3,
            _ => 0
        };

        return ((value >> bit) & 1) != 0;
    }

    public bool HasAnyLowBits(int count)
    {
        if (count <= 0)
            return false;

        if (count >= 256)
            return U0 != 0 ||
                   U1 != 0 ||
                   U2 != 0 ||
                   U3 != 0;

        int words = count / 64;
        int bits = count % 64;

        if (words >= 1 && U0 != 0)
            return true;

        if (words >= 2 && U1 != 0)
            return true;

        if (words >= 3 && U2 != 0)
            return true;

        if (words >= 4 && U3 != 0)
            return true;

        if (bits == 0)
            return false;

        ulong mask = (1UL << bits) - 1;

        ulong selected = words switch
        {
            0 => U0,
            1 => U1,
            2 => U2,
            _ => U3
        };

        return (selected & mask) != 0;
    }

    public static UInt256 operator <<(UInt256 value, int shift)
    {
        if (shift <= 0)
            return value;

        if (shift >= 256)
            return default;

        int wordShift = shift / 64;
        int bitShift = shift % 64;

        ulong[] src = { value.U0, value.U1, value.U2, value.U3 };

        ulong[] dst = { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int d = i + wordShift;

            if (d >= 4)
                continue;

            dst[d] |= src[i] << bitShift;

            if (bitShift != 0 &&
                d + 1 < 4)
            {
                dst[d + 1] |= src[i] >> (64 - bitShift);
            }
        }

        return new UInt256(dst[0], dst[1], dst[2], dst[3]);
    }

    public static UInt256 operator >>(UInt256 value, int shift)
    {
        if (shift <= 0)
            return value;

        if (shift >= 256)
            return default;

        int wordShift = shift / 64;
        int bitShift = shift % 64;

        ulong[] src = { value.U0, value.U1, value.U2, value.U3 };

        ulong[] dst = { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int d = i - wordShift;

            if (d < 0)
                continue;

            dst[d] |= src[i] >> bitShift;

            if (bitShift != 0 &&
                d - 1 >= 0)
            {
                dst[d - 1] |= src[i] << (64 - bitShift);
            }
        }

        return new UInt256(dst[0], dst[1], dst[2], dst[3]);
    }

    public static UInt256 Multiply(UInt128 a, UInt128 b)
    {
        ulong a0 = (ulong)a;
        ulong a1 = (ulong)(a >> 64);

        ulong b0 = (ulong)b;
        ulong b1 = (ulong)(b >> 64);

        Mul64(
            a0,
            b0,
            out ulong p00Lo,
            out ulong p00Hi);

        Mul64(
            a0,
            b1,
            out ulong p01Lo,
            out ulong p01Hi);

        Mul64(
            a1,
            b0,
            out ulong p10Lo,
            out ulong p10Hi);

        Mul64(
            a1,
            b1,
            out ulong p11Lo,
            out ulong p11Hi);

        ulong u0 = p00Lo;

        ulong u1 = p00Hi;

        ulong old = u1;
        u1 += p01Lo;
        ulong carry = u1 < old ? 1UL : 0UL;

        old = u1;
        u1 += p10Lo;
        carry += u1 < old ? 1UL : 0UL;

        ulong u2 = p01Hi +
            p10Hi +
            carry;

        old = u2;
        u2 += p11Lo;
        if (u2 < old)
        {
            // Carry into u3.
            p11Hi++;
        }

        ulong u3 = p11Hi +
            (u2 < old ? 1UL : 0UL);

        return new UInt256(u0, u1, u2, u3);
    }

    private static void Mul64(ulong x, ulong y, out ulong lo, out ulong hi)
    {
        ulong x0 = x & 0xFFFFFFFF;
        ulong x1 = x >> 32;

        ulong y0 = y & 0xFFFFFFFF;
        ulong y1 = y >> 32;

        ulong t = x0 * y0;

        ulong w3 = t >> 32;
        ulong w0 = t & 0xFFFFFFFF;

        t = x1 * y0 + w3;

        ulong w1 = t & 0xFFFFFFFF;
        ulong w2 = t >> 32;

        w1 += x0 * y1;

        hi = x1 * y1 +
            w2 +
            (w1 >> 32);

        lo = (w1 << 32) |
            w0;
    }

    public static UInt128 Divide(UInt256 numerator, UInt128 denominator, out bool remainder)
    {
        if (denominator == 0)
            throw new DivideByZeroException();

        UInt256 d = FromUInt128(denominator);

        UInt256 rem = default;

        UInt128 quotient = 0;

        for (int i = 255; i >= 0; i--)
        {
            rem <<= 1;

            if (numerator.GetBit(i))
                rem = new UInt256(rem.U0 | 1, rem.U1, rem.U2, rem.U3);

            if (Compare(rem, d) >= 0)
            {
                rem = Subtract(rem, d);

                if (i < 128)
                    quotient |= UInt128.One << i;
            }
        }

        remainder =
            rem.U0 != 0 ||
            rem.U1 != 0 ||
            rem.U2 != 0 ||
            rem.U3 != 0;

        return quotient;
    }

    private static int Compare(UInt256 a, UInt256 b)
    {
        int c = a.U3.CompareTo(b.U3);

        if (c != 0)
            return c;

        c = a.U2.CompareTo(b.U2);

        if (c != 0)
            return c;

        c = a.U1.CompareTo(b.U1);

        if (c != 0)
            return c;

        return a.U0.CompareTo(b.U0);
    }

    private static UInt256 Subtract(UInt256 a, UInt256 b)
    {
        ulong u0 = a.U0 - b.U0;

        ulong borrow = a.U0 < b.U0 ? 1UL : 0UL;

        ulong u1 = a.U1 - b.U1 - borrow;

        ulong borrow2 = a.U1 < b.U1 + borrow ? 1UL : 0UL;

        ulong u2 = a.U2 - b.U2 - borrow2;

        ulong borrow3 = a.U2 < b.U2 + borrow2 ? 1UL : 0UL;

        ulong u3 = a.U3 - b.U3 - borrow3;

        return new UInt256(u0, u1, u2, u3);
    }
}