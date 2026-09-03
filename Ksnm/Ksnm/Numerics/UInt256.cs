using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics;

public readonly struct UInt256
{
    public readonly UInt128 Low;
    public readonly UInt128 High;

    public UInt256(UInt128 low, UInt128 high)
    {
        Low = low;
        High = high;
    }

    public static UInt256 operator +(UInt256 a, UInt256 b)
    {
        UInt128 low = a.Low + b.Low;

        UInt128 carry = low < a.Low ? UInt128.One : UInt128.Zero;

        UInt128 high = a.High + b.High + carry;

        return new UInt256(low, high);
    }

    public static implicit operator UInt256(UInt128 value)
    {
        return new UInt256(value, UInt128.Zero);
    }

    private static UInt128 Multiply64(ulong a, ulong b)
    {
        return (UInt128)a * b;
    }

    public static UInt256 Multiply(UInt128 a, UInt128 b)
    {
        ulong a0 = (ulong)a;
        ulong a1 = (ulong)(a >> 64);

        ulong b0 = (ulong)b;
        ulong b1 = (ulong)(b >> 64);

        UInt128 p00 = (UInt128)a0 * b0;
        UInt128 p01 = (UInt128)a0 * b1;
        UInt128 p10 = (UInt128)a1 * b0;
        UInt128 p11 = (UInt128)a1 * b1;

        UInt128 low = p00;

        UInt128 middle = (p01 << 64) + (p10 << 64);

        UInt128 high = p11 + (p01 >> 64) + (p10 >> 64);

        UInt128 newLow = low + middle;

        if (newLow < low)
            high++;

        return new UInt256(newLow, high);
    }

    private static void Multiply128(UInt128 a, UInt128 b,
        out ulong r0, out ulong r1, out ulong r2, out ulong r3)
    {
        ulong a0 = (ulong)a;
        ulong a1 = (ulong)(a >> 64);

        ulong b0 = (ulong)b;
        ulong b1 = (ulong)(b >> 64);

        UInt128 p00 = (UInt128)a0 * b0;
        UInt128 p01 = (UInt128)a0 * b1;
        UInt128 p10 = (UInt128)a1 * b0;
        UInt128 p11 = (UInt128)a1 * b1;

        r0 = (ulong)p00;

        UInt128 t = (p00 >> 64) + (ulong)p01 + (ulong)p10;
        r1 = (ulong)t;

        UInt128 t2 = (p01 >> 64) + (p10 >> 64) + (ulong)p11 + (t >> 64);
        r2 = (ulong)t2;

        UInt128 t3 = (p11 >> 64) + (t2 >> 64);
        r3 = (ulong)t3;
    }
}