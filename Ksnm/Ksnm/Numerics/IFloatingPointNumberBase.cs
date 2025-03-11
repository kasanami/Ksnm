using System.Numerics;

namespace Ksnm.Numerics
{
    public interface IFloatingPointNumberBase
    {
        static double ToDouble<TFrom, TFromBits>(TFrom from)
            where TFrom : IFloatingPointProperties<TFromBits>
            where TFromBits : INumber<TFromBits>, IShiftOperators<TFromBits, int, TFromBits>
        {
            ExtendedDouble result = new();
            result.SignBit = ulong.CreateTruncating(from.SignBit);
            if (from.IsZero)
            {
                return result;
            }
            else if(from.IsPositiveInfinity)
            {
                return Double.PositiveInfinity;
            }
            else if (from.IsNegativeInfinity)
            {
                return Double.NegativeInfinity;
            }
            else if (from.IsNaN)
            {
                return Double.NaN;
            }
            else
            {
                result.Exponent = from.Exponent;
            }
            result.MantissaBits = ShiftMantissa<TFromBits, UInt64>(from.MantissaBits, from.MantissaLength, ExtendedDouble.MantissaLength);
            return result;
        }
        static float ToSingle<TFrom, TFromBits>(TFrom from)
            where TFrom : IFloatingPointProperties<TFromBits>
            where TFromBits : INumber<TFromBits>, IShiftOperators<TFromBits, int, TFromBits>
        {
            ExtendedSingle result = new();
            result.SignBit = uint.CreateTruncating(from.SignBit);
            if (from.IsZero)
            {
                return result;
            }
            else if (from.IsPositiveInfinity)
            {
                return Single.PositiveInfinity;
            }
            else if (from.IsNegativeInfinity)
            {
                return Single.NegativeInfinity;
            }
            else if (from.IsNaN)
            {
                return Single.NaN;
            }
            else
            {
                result.Exponent = from.Exponent;
            }
            result.MantissaBits = ShiftMantissa<TFromBits, UInt32>(from.MantissaBits, from.MantissaLength, ExtendedSingle.MantissaLength);
            return result;
        }
        static TTo FromDouble<TTo, TToBits>(ExtendedDouble from)
            where TTo : IFloatingPointProperties<TToBits>, new()
            where TToBits : INumber<TToBits>, IShiftOperators<TToBits, int, TToBits>
        {
            TTo fp = new();
            fp.SignBit = TToBits.CreateTruncating(from.SignBit);
            if (IsZero<ExtendedDouble, UInt64>(from))
            {
                return fp;
            }
            else
            {
                fp.Exponent = from.Exponent;
            }
            fp.MantissaBits = ShiftMantissa<UInt64, TToBits>(from.MantissaBits, ExtendedDouble.MantissaLength, fp.MantissaLength);
            return fp;
        }
        static TTo FromSingle<TTo, TToBits>(ExtendedSingle from)
            where TTo : IFloatingPointProperties<TToBits>, new()
            where TToBits : INumber<TToBits>, IShiftOperators<TToBits, int, TToBits>
        {
            TTo fp = new();
            fp.SignBit = TToBits.CreateTruncating(from.SignBit);
            if (IsZero<ExtendedSingle, UInt32>(from))
            {
                return fp;
            }
            else
            {
                fp.Exponent = from.Exponent;
            }
            fp.MantissaBits = ShiftMantissa<UInt32, TToBits>(from.MantissaBits, ExtendedSingle.MantissaLength, fp.MantissaLength);
            return fp;
        }
        /// <summary>
        /// 仮数部を他のビット数の違う仮数部へ変換します
        /// </summary>
        static TTo ShiftMantissa<TFrom, TTo>(TFrom mantissa, int fromLength, int toLength)
            where TFrom : INumber<TFrom>, IShiftOperators<TFrom, int, TFrom>
            where TTo : INumber<TTo>, IShiftOperators<TTo, int, TTo>
        {
            if (fromLength > toLength)
            {
                var diff = fromLength - toLength;
                return TTo.CreateTruncating(mantissa >> diff);
            }
            else if (fromLength < toLength)
            {
                var diff = toLength - fromLength;
                return TTo.CreateTruncating(mantissa) << diff;
            }
            return TTo.CreateTruncating(mantissa);
        }
        static bool IsZero<T, TBits>(T value)
            where T : IFloatingPointProperties<TBits>
            where TBits : INumber<TBits>, IShiftOperators<TBits, int, TBits>
        {
            return value.ExponentBits == TBits.Zero && value.MantissaBits == TBits.Zero;
        }
    }
}