using System.Numerics;

namespace Ksnm.Numerics
{
    public interface IFloatingPointNumberBase
    {
        static double ToDouble<T, TBits>(T from)
            where T : IFloatingPointProperties<TBits>
            where TBits : INumber<TBits>
        {
            ExtendedDouble extendedDouble = new();
            extendedDouble.SignBit = ulong.CreateTruncating(from.SignBit);
            extendedDouble.Exponent = from.Exponent;
            extendedDouble.Mantissa = ulong.CreateTruncating(from.Mantissa);
            return extendedDouble;
        }
        static float ToSingle<T, TBits>(T from)
            where T : IFloatingPointProperties<TBits>
            where TBits : INumber<TBits>
        {
            ExtendedSingle extendedSingle = new();
            extendedSingle.SignBit = uint.CreateTruncating(from.SignBit);
            extendedSingle.Exponent = from.Exponent;
            extendedSingle.Mantissa = uint.CreateTruncating(from.Mantissa);
            return extendedSingle;
        }
        static T FromDouble<T, TBits>(ExtendedDouble from)
            where T : IFloatingPointProperties<TBits>, new()
            where TBits : INumber<TBits>
        {
            T fp = new();
            fp.SignBit = TBits.CreateTruncating(from.SignBit);
            fp.Exponent = from.Exponent;
            fp.Mantissa = TBits.CreateTruncating(from.Mantissa);
            return fp;
        }
        static T FromSingle<T, TBits>(ExtendedSingle from)
            where T : IFloatingPointProperties<TBits>, new()
            where TBits : INumber<TBits>
        {
            T fp = new();
            fp.SignBit = TBits.CreateTruncating(from.SignBit);
            fp.Exponent = from.Exponent;
            fp.Mantissa = TBits.CreateTruncating(from.Mantissa);
            return fp;
        }
    }
}