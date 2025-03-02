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
        static double ToSingle<T, TBits>(T from)
            where T : IFloatingPointProperties<TBits>
            where TBits : INumber<TBits>
        {
            ExtendedSingle extendedSingle = new();
            extendedSingle.SignBit = uint.CreateTruncating(from.SignBit);
            extendedSingle.Exponent = from.Exponent;
            extendedSingle.Mantissa = uint.CreateTruncating(from.Mantissa);
            return extendedSingle;
        }
    }
}