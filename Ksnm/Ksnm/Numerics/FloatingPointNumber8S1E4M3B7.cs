﻿using Ksnm.ExtensionMethods.System.Double;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ksnm.Numerics
{
    using FixedPointNumber = FloatingPointNumber8S1E4M3B7;
    using BitsType = System.Byte;
    /// <summary>
    /// 8ビット浮動小数点数型
    /// </summary>
    public struct FloatingPointNumber8S1E4M3B7 :
        IEquatable<FloatingPointNumber8S1E4M3B7>,
        IFloatingPointNumberBase,
        IFloatingPointProperties<BitsType>,
        INumber<FloatingPointNumber8S1E4M3B7>
    {
        #region 符号部
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignMask = 0b1000_0000;
        /// <summary>
        /// 符号部のビット数
        /// </summary>
        public const int SignLength = 1;
        /// <summary>
        /// 符号部を右シフトする際のシフト数
        /// </summary>
        public const int SignShift = MantissaLength + ExponentLength;
        #endregion 符号部

        #region 指数部
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const BitsType ExponentMask = 0b0111_1000;
        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 4;
        /// <summary>
        /// 指数部を右シフトする際のシフト数
        /// </summary>
        public const int ExponentShift = MantissaLength;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 7;
        /// <summary>
        /// 無限大を表す指数部のビット
        /// </summary>
        public const BitsType InfinityExponentBits = 0b1111;

        public const int MinExponent = 1 - ExponentBias;
        public const int MaxExponent = ExponentBias;

        public const BitsType MinExponentBits = 0b0000;
        public const BitsType MaxExponentBits = 0b1111;
        #endregion 指数部

        #region 仮数部
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const BitsType MantissaMask = 0b0000_0111;
        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 3;
        #endregion 仮数部

        #region 定数
        public const int Radix = 2;

        private const BitsType EpsilonBits = 0b0000_0001;
        public static FloatingPointNumber8S1E4M3B7 Epsilon => new(EpsilonBits);

        private const BitsType PositiveQNaNBits = 0b0_1111_100;
        private const BitsType NegativeQNaNBits = 0b1_1111_100;
        public static FloatingPointNumber8S1E4M3B7 NaN => new(NegativeQNaNBits);

        private const BitsType PositiveOneBits = 0b0_0111_000;
        private const BitsType NegativeOneBits = 0b1_0111_000;
        public static FloatingPointNumber8S1E4M3B7 One => new(PositiveOneBits);
        public static FloatingPointNumber8S1E4M3B7 NegativeOne => new(NegativeOneBits);

        static int INumberBase<FloatingPointNumber8S1E4M3B7>.Radix => Radix;

        public static FloatingPointNumber8S1E4M3B7 Zero => new(0);

        public static FloatingPointNumber8S1E4M3B7 AdditiveIdentity => Zero;

        public static FloatingPointNumber8S1E4M3B7 MultiplicativeIdentity => One;
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 全体のビット
        /// </summary>
        public BitsType Bits { get; set; }
        /// <summary>
        /// 符号部ビット
        /// 負数ならば 1 になる。
        /// </summary>
        public BitsType SignBit
        {
            get => (BitsType)(Bits >> SignShift);
            set => Bits = (BitsType)((Bits & ~SignMask) | ((value & 1) << SignShift));
        }
        /// <summary>
        /// 符号を取得/設定
        /// </summary>
        public int Sign
        {
            get => SignBit == 1 ? -1 : +1;
            set => SignBit = value < 0 ? (BitsType)1 : (BitsType)0;
        }
        /// <summary>
        /// 指数部ビット
        /// </summary>
        public BitsType ExponentBits
        {
            get => (BitsType)((Bits & ExponentMask) >> ExponentShift);
            set => Bits = (BitsType)(((value << ExponentShift) & ExponentMask) | (Bits & ~ExponentMask));
        }
        /// <summary>
        /// 指数
        /// </summary>
        public int Exponent
        {
            get
            {
                // 0なら0を返す
                if (IsZero)
                {
                    return 0;
                }
                // ExponentBitsが0なら最小値を返す
                if (ExponentBits == MinExponentBits)
                {
                    return MinExponent;
                }
                return (int)ExponentBits - ExponentBias;
            }
            set
            {
                if (value > MaxExponent)
                {
                    ExponentBits = MaxExponentBits;
                }
                else if (value < MinExponent)
                {
                    ExponentBits = MinExponentBits;
                }
                ExponentBits = (BitsType)(value + ExponentBias);
            }
        }
        /// <summary>
        /// 仮数部ビット
        /// </summary>
        public BitsType MantissaBits
        {
            get => (BitsType)(Bits & MantissaMask);
            set => Bits ^= (BitsType)(value & MantissaMask);
        }
        /// <summary>
        /// 仮数を取得/設定
        /// 0～1.0の値
        /// ※1.0=0b1000
        /// ※0が出力されるのは、符号ビット以外が0のとき
        /// </summary>
        public BitsType Mantissa
        {
            get
            {
                // 非正規化数なら最上位に1を加えずに返す
                if (ExponentBits == 0)
                {
                    return MantissaBits;
                }
                // (1 << MantissaLength)は"1."を意味する
                return (BitsType)(MantissaBits | (1 << MantissaLength));
            }
            set => Bits = value;
        }
        int IFloatingPointProperties<BitsType>.MantissaLength => MantissaLength;
        /// <summary>
        /// 倍率
        /// Mantissaと乗算すると元の値になる係数
        /// </summary>
        public double Scale
        {
            get
            {
                if (ExponentBits == InfinityExponentBits)
                {
                    return double.PositiveInfinity;
                }
                return System.Math.Pow(Radix, Exponent);
            }
        }

        public double Coefficient => Mantissa / (double)(1 << MantissaLength);

        bool IsZero => (Bits & ~SignMask) == 0;
        bool IFloatingPointProperties<BitsType>.IsPositive => IsPositive(this);
        bool IFloatingPointProperties<BitsType>.IsNegative => IsNegative(this);
        bool IFloatingPointProperties<BitsType>.IsInfinity
            => (ExponentBits == InfinityExponentBits) && (MantissaBits == 0);
        bool IFloatingPointProperties<BitsType>.IsPositiveInfinity
            => IsPositive(this) && IsInfinity(this);
        bool IFloatingPointProperties<BitsType>.IsNegativeInfinity
            => IsNegative(this) && IsInfinity(this);
        bool IFloatingPointProperties<BitsType>.IsNaN
            => (ExponentBits == InfinityExponentBits) && (MantissaBits != 0);
        bool IFloatingPointProperties<BitsType>.IsZero => IsZero;
        #endregion プロパティ

        #region コンストラクタ
        public FloatingPointNumber8S1E4M3B7()
        {
        }
        public FloatingPointNumber8S1E4M3B7(BitsType bits)
        {
            Bits = bits;
        }
        public FloatingPointNumber8S1E4M3B7(ExtendedDouble value)
        {
            Bits = CreateBits(
                (BitsType)value.SignBit,
                (BitsType)(value.ExponentBits >> (ExtendedDouble.ExponentLength - ExponentLength)),
                (BitsType)(value.MantissaBits >> (ExtendedDouble.MantissaLength - MantissaLength)));
        }
        #endregion コンストラクタ

        #region 
        static BitsType CreateBits(BitsType signBit, BitsType exponentBits, BitsType mantissaBits)
        {
            return BitsOr(signBit << SignShift, exponentBits << ExponentShift, mantissaBits);
        }
        static BitsType BitsOr(int bits1, int bits2, int bits3)
        {
            return (BitsType)(bits1 | bits2 | bits3);
        }
        #endregion 

        #region 型変換
        public double ToDouble()
        {
            return IFloatingPointNumberBase.ToDouble<FloatingPointNumber8S1E4M3B7, BitsType>(this);
        }
        public float ToSingle()
        {
            return IFloatingPointNumberBase.ToSingle<FloatingPointNumber8S1E4M3B7, BitsType>(this);
        }
        public static FloatingPointNumber8S1E4M3B7 FromDouble(double value)
        {
            return IFloatingPointNumberBase.FromDouble<FloatingPointNumber8S1E4M3B7, BitsType>(value);
        }
        public static FloatingPointNumber8S1E4M3B7 FromSingle(float value)
        {
            return IFloatingPointNumberBase.FromSingle<FloatingPointNumber8S1E4M3B7, BitsType>(value);
        }

        public static explicit operator FloatingPointNumber8S1E4M3B7(char value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(byte value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(short value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(ushort value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(int value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(uint value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(long value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(ulong value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(Half value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(float value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(double value)=> FromDouble(value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(decimal value) => FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(Int128 value)=> FromDouble((double)value);
        public static explicit operator FloatingPointNumber8S1E4M3B7(UInt128 value) => FromDouble((double)value);
        public static implicit operator float(FloatingPointNumber8S1E4M3B7 value)=> value.ToSingle();
        public static implicit operator double(FloatingPointNumber8S1E4M3B7 value)=> value.ToDouble();
        #endregion 型変換

        public static bool operator >(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() > right.ToDouble();

        public static bool operator >=(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() >= right.ToDouble();

        public static bool operator <(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() < right.ToDouble();

        public static bool operator <=(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() <= right.ToDouble();

        public static FloatingPointNumber8S1E4M3B7 operator %(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => new(left.ToDouble() % right.ToDouble());

        public static FloatingPointNumber8S1E4M3B7 operator +(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => new(left.ToDouble() + right.ToDouble());

        public static FloatingPointNumber8S1E4M3B7 operator --(FloatingPointNumber8S1E4M3B7 value)
            => throw new NotImplementedException();

        public static FloatingPointNumber8S1E4M3B7 operator /(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => new(left.ToDouble() / right.ToDouble());

        public static bool operator ==(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() == right.ToDouble();

        public static bool operator !=(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => left.ToDouble() != right.ToDouble();

        public static FloatingPointNumber8S1E4M3B7 operator ++(FloatingPointNumber8S1E4M3B7 value)
            => throw new NotImplementedException();

        public static FloatingPointNumber8S1E4M3B7 operator *(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => new(left.ToDouble() * right.ToDouble());

        public static FloatingPointNumber8S1E4M3B7 operator -(FloatingPointNumber8S1E4M3B7 left, FloatingPointNumber8S1E4M3B7 right)
            => new(left.ToDouble() - right.ToDouble());

        public static FloatingPointNumber8S1E4M3B7 operator -(FloatingPointNumber8S1E4M3B7 value)
        {
            value.SignBit ^= SignMask;
            return value;
        }

        public static FloatingPointNumber8S1E4M3B7 operator +(FloatingPointNumber8S1E4M3B7 value)
            => value;

        #region IEquatable
        /// <summary>
        /// このインスタンスが指定した FloatingPointNumber16 値に等しいかどうかを示す値を返します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FloatingPointNumber8S1E4M3B7 other)
        {
            return Bits.Equals(other.Bits);
        }
        #endregion IEquatable

        #region object
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is FloatingPointNumber16)
            {
                return Equals((FloatingPointNumber16)obj);
            }
            return false;
        }
        /// <summary>
        /// このインスタンスのハッシュ コードを返します。
        /// </summary>
        public override int GetHashCode()
        {
            return Bits;
        }
        /// <summary>
        /// このインスタンスの数値を、それと等価な文字列形式に変換します。
        /// </summary>
        public override string ToString()
        {
            return ToDouble().ToString();
        }
        #endregion object

        #region INumberBase
        public static FloatingPointNumber8S1E4M3B7 Abs(FloatingPointNumber8S1E4M3B7 value)
            => new(value.Bits & ~SignMask);

        public static bool IsCanonical(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits != 0 && value.MantissaBits != 0;

        public static bool IsComplexNumber(FloatingPointNumber8S1E4M3B7 value)
            => false;

        public static bool IsEvenInteger(FloatingPointNumber8S1E4M3B7 value)
            => throw new NotImplementedException();

        public static bool IsFinite(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits != InfinityExponentBits && value.MantissaBits != 0;

        public static bool IsImaginaryNumber(FloatingPointNumber8S1E4M3B7 value)
            => false;

        public static bool IsInfinity(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits == InfinityExponentBits && value.MantissaBits == 0;

        public static bool IsInteger(FloatingPointNumber8S1E4M3B7 value)
            => value.ToDouble().IsInteger();

        public static bool IsNaN(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits == InfinityExponentBits && value.MantissaBits != 0;

        public static bool IsNegative(FloatingPointNumber8S1E4M3B7 value)
            => value.SignBit == 1;

        public static bool IsNegativeInfinity(FloatingPointNumber8S1E4M3B7 value)
            => IsNegative(value) && IsInfinity(value);

        public static bool IsNormal(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits != 0 && value.ExponentBits != InfinityExponentBits && value.MantissaBits != 0;

        public static bool IsOddInteger(FloatingPointNumber8S1E4M3B7 value)
            => throw new NotImplementedException();

        public static bool IsPositive(FloatingPointNumber8S1E4M3B7 value)
            => value.SignBit == 0;

        public static bool IsPositiveInfinity(FloatingPointNumber8S1E4M3B7 value)
            => IsPositive(value) && IsInfinity(value);

        public static bool IsRealNumber(FloatingPointNumber8S1E4M3B7 value) => true;

        public static bool IsSubnormal(FloatingPointNumber8S1E4M3B7 value)
            => value.ExponentBits == 0 && value.MantissaBits != 0;

        public static FloatingPointNumber8S1E4M3B7 MaxMagnitude(FloatingPointNumber8S1E4M3B7 x, FloatingPointNumber8S1E4M3B7 y)
            => x > y ? x : y;

        public static FloatingPointNumber8S1E4M3B7 MaxMagnitudeNumber(FloatingPointNumber8S1E4M3B7 x, FloatingPointNumber8S1E4M3B7 y)
            => x > y ? x : y;

        public static FloatingPointNumber8S1E4M3B7 MinMagnitude(FloatingPointNumber8S1E4M3B7 x, FloatingPointNumber8S1E4M3B7 y)
            => x < y ? x : y;

        public static FloatingPointNumber8S1E4M3B7 MinMagnitudeNumber(FloatingPointNumber8S1E4M3B7 x, FloatingPointNumber8S1E4M3B7 y)
            => x < y ? x : y;

        public static FloatingPointNumber8S1E4M3B7 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
            => (FloatingPointNumber8S1E4M3B7)double.Parse(s, style, provider);

        public static FloatingPointNumber8S1E4M3B7 Parse(string s, NumberStyles style, IFormatProvider? provider)
            => (FloatingPointNumber8S1E4M3B7)double.Parse(s, style, provider);

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(char))
            {
                char actualValue = (char)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(byte))
            {
                byte actualValue = (byte)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                sbyte actualValue = (sbyte)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                short actualValue = (short)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                ushort actualValue = (ushort)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                int actualValue = (int)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                uint actualValue = (uint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                long actualValue = (long)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                ulong actualValue = (ulong)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                Int128 actualValue = (Int128)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                UInt128 actualValue = (UInt128)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                nint actualValue = (nint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                nuint actualValue = (nuint)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualValue = (Half)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualValue = (float)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualValue = (double)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                decimal actualValue = (decimal)(object)value;
                result = checked((FixedPointNumber)actualValue);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(FloatingPointNumber8S1E4M3B7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(FloatingPointNumber8S1E4M3B7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(FloatingPointNumber8S1E4M3B7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public static FloatingPointNumber8S1E4M3B7 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result)
        {
            throw new NotImplementedException();
        }

        public static FloatingPointNumber8S1E4M3B7 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber8S1E4M3B7 result)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            if (obj is FloatingPointNumber8S1E4M3B7 other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException($"Argument must be of type {nameof(FloatingPointNumber8S1E4M3B7)}", nameof(obj));
        }

        public int CompareTo(FloatingPointNumber8S1E4M3B7 other)
        {
            if (this == other)
            {
                return 0;
            }
            if (this < other)
            {
                return -1;
            }
            return 1;
        }

        static bool INumberBase<FloatingPointNumber8S1E4M3B7>.IsZero(FloatingPointNumber8S1E4M3B7 value)
            => value.IsZero;
        #endregion INumberBase
    }
}