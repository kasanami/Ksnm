using Ksnm.Numerics;
using Ksnm.Units.SI;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KsnmTests.Numerics
{
    using BitsType = System.UInt16;// 全体のビットを表す型
    /// <summary>
    /// 16ビットの浮動小数点数
    /// ・Google Brainによって2018年に開発された、より機械学習での利用に適したフォーマット。
    /// ・符号ビット : 1ビット
    /// ・指数部 : 8ビット
    /// ・仮数部 : 7ビット(暗黙的に8ビットの表現幅を持つ。)
    /// </summary>
    public struct BrainFloatingPoint16 :
        IFloatingPointConstants<BrainFloatingPoint16>,
        IFloatingPointIeee754<BrainFloatingPoint16>,
        IMinMaxValue<BrainFloatingPoint16>
    {
        #region 定数
        public const int Radix = 2;

        /// <summary>
        /// 符号部のビット数
        /// </summary>
        public const int SignLength = 1;
        /// <summary>
        /// 符号部のビットシフト数
        /// </summary>
        public const int SignBitShift = 15;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignBitMask = 1;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType SignShiftedBitMask = 0b1000_0000_0000_0000;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 8;
        /// <summary>
        /// 指数部のビットシフト数
        /// </summary>
        public const int ExponentBitShift = 7;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const BitsType ExponentBitMask = 0b0000_0000_1111_1111;
        /// <summary>
        /// 指数部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType ExponentShiftedBitMask = 0b0111_1111_1000_0000;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 127;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const int InfinityExponent = 128;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 7;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const BitsType MantissaBitMask = 0b0000_0000_0111_1111;

        private const BitsType EpsilonBits = 0x0001;
        private const BitsType PositiveQNaNBits = 0b0111_1111_1100_0000;
        private const BitsType NegativeQNaNBits = 0b1111_1111_1100_0000;
        private const BitsType PositiveInfinityBits = 0b0111_1111_1000_0000;
        private const BitsType NegativeInfinityBits = 0b1111_1111_1000_0000;
        private const BitsType PositiveZeroBits = 0x0000;
        private const BitsType NegativeZeroBits = 0x8000;
        private const BitsType PositiveOneBits = 0b0011_1111_1000_0000;
        private const BitsType NegativeOneBits = 0b1011_1111_1000_0000;
        private const BitsType MinValueBits = 0b1111_1111_0111_1111;
        private const BitsType MaxValueBits = 0b0111_1111_0111_1111;

        #region IFloatingPointConstants
        public static BrainFloatingPoint16 E => (BrainFloatingPoint16)float.E;

        public static BrainFloatingPoint16 Pi => (BrainFloatingPoint16)float.Pi;

        public static BrainFloatingPoint16 Tau => (BrainFloatingPoint16)float.Tau;

        public static BrainFloatingPoint16 One => new(PositiveOneBits);

        public static BrainFloatingPoint16 Zero => new(PositiveZeroBits);

        public static BrainFloatingPoint16 AdditiveIdentity => Zero;

        public static BrainFloatingPoint16 MultiplicativeIdentity => One;

        static int INumberBase<BrainFloatingPoint16>.Radix => 2;
        #endregion IFloatingPointConstants
        #region IMinMaxValue
        /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue" />
        public static BrainFloatingPoint16 MaxValue => new(MaxValueBits);
        /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue" />
        public static BrainFloatingPoint16 MinValue => new(MinValueBits);
        #endregion IMinMaxValue
        #region IFloatingPointIeee754
        public static BrainFloatingPoint16 Epsilon => new(EpsilonBits);
        public static BrainFloatingPoint16 NaN => new(NegativeQNaNBits);
        public static BrainFloatingPoint16 PositiveInfinity => new(PositiveInfinityBits);
        public static BrainFloatingPoint16 NegativeInfinity => new(NegativeInfinityBits);
        /// <inheritdoc cref="IFloatingPointIeee754{TSelf}.NegativeZero" />
        public static BrainFloatingPoint16 NegativeZero => new(NegativeZeroBits);
        /// <inheritdoc cref="IFloatingPointIeee754{TSelf}.NegativeOne" />
        public static BrainFloatingPoint16 NegativeOne => new(NegativeOneBits);
        #endregion IFloatingPointIeee754
        #endregion 定数

        #region フィールド
        public BitsType Bits;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public byte SignBit
        {
            get => (byte)(Bits >> SignBitShift);
            set => Bits = (BitsType)((Bits & ~SignShiftedBitMask) | (BitsType)((value & 1) << SignBitShift));
        }
        /// <summary>
        /// 符号を取得/設定
        /// </summary>
        public int Sign
        {
            get => SignBit == 1 ? -1 : +1;
            set => SignBit = value < 0 ? (byte)1 : (byte)0;
        }

        /// <summary>
        /// 指数部を取得/設定
        /// </summary>
        public ushort ExponentBits
        {
            get => (ushort)((Bits >> ExponentBitShift) & ExponentBitMask);
            set => Bits = (BitsType)((Bits & ~ExponentShiftedBitMask) | ((value & ExponentBitMask) << ExponentBitShift));
        }
        /// <summary>
        /// 指数を取得/設定
        /// 2のべき乗の指数（2^Exponent）
        /// Mantissaが左詰めのため、そのビット数は引いたあとの値
        /// </summary>
        public int Exponent
        {
            get
            {
                // 0なら0を返す
                if (IsZero(this))
                {
                    return 0;
                }
                return ExponentBits - ExponentBias - MantissaLength;
            }
            set => ExponentBits = (ushort)(value + ExponentBias + MantissaLength);
        }
        /// <summary>
        /// 倍率
        /// Mantissaと乗算すると元の値になる係数
        /// </summary>
        public double Scale => System.Math.Pow(Radix, Exponent);

        /// <summary>
        /// 仮数部を取得/設定
        /// </summary>
        public BitsType MantissaBits
        {
            get => (BitsType)(Bits & MantissaBitMask);
            set => Bits = (BitsType)((Bits & ~MantissaBitMask) | (value & MantissaBitMask));
        }
        /// <summary>
        /// 仮数を取得/設定
        /// 0～1.0の値
        /// ※1.0=0b1000_0000
        /// ※0が出力されるのは、符号ビット以外が0のとき
        /// </summary>
        public BitsType Mantissa
        {
            get
            {
                // 0なら0を返す
                if (IsZero(this))
                {
                    return 0;
                }
                // ((UInt)1 << MantissaLength)は"1."を意味する
                return (BitsType)(MantissaBits | ((BitsType)1 << MantissaLength));
            }
            set => Bits = value;
        }
        #endregion プロパティ

        #region コンストラクタ
        public BrainFloatingPoint16()
        {
        }
        public BrainFloatingPoint16(BitsType bits)
        {
            Bits = bits;
        }
        #endregion コンストラクタ

        #region メソッド
        public static BrainFloatingPoint16 Abs(BrainFloatingPoint16 value)
            => (BrainFloatingPoint16)float.Abs(value);

        public static bool IsCanonical(BrainFloatingPoint16 value)
            => throw new NotImplementedException();

        public static bool IsComplexNumber(BrainFloatingPoint16 value)
            => false;

        public static bool IsEvenInteger(BrainFloatingPoint16 value)
            => float.IsEvenInteger(value);

        public static bool IsFinite(BrainFloatingPoint16 value)
            => float.IsFinite(value);

        public static bool IsImaginaryNumber(BrainFloatingPoint16 value)
            => false;

        public static bool IsInfinity(BrainFloatingPoint16 value)
            => float.IsInfinity(value);

        public static bool IsInteger(BrainFloatingPoint16 value)
            => float.IsInteger(value);

        public static bool IsNaN(BrainFloatingPoint16 value)
            => float.IsNaN(value);

        public static bool IsNegative(BrainFloatingPoint16 value)
            => float.IsNegative(value);

        public static bool IsNegativeInfinity(BrainFloatingPoint16 value)
            => float.IsNegativeInfinity(value);

        public static bool IsNormal(BrainFloatingPoint16 value)
            => float.IsNormal(value);

        public static bool IsOddInteger(BrainFloatingPoint16 value)
            => float.IsOddInteger(value);

        public static bool IsPositive(BrainFloatingPoint16 value)
            => float.IsPositive(value);

        public static bool IsPositiveInfinity(BrainFloatingPoint16 value)
            => float.IsPositiveInfinity(value);

        public static bool IsRealNumber(BrainFloatingPoint16 value)
            => float.IsRealNumber(value);

        public static bool IsSubnormal(BrainFloatingPoint16 value)
            => float.IsSubnormal(value);

        public static bool IsZero(BrainFloatingPoint16 value)
        {
            // 符号ビット以外が0なら0を返す
            return (value.Bits & ~SignShiftedBitMask) == 0;
        }

        public static BrainFloatingPoint16 MaxMagnitude(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
            => (BrainFloatingPoint16)float.MaxMagnitude(x, y);

        public static BrainFloatingPoint16 MaxMagnitudeNumber(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
            => (BrainFloatingPoint16)float.MaxMagnitudeNumber(x, y);

        public static BrainFloatingPoint16 MinMagnitude(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
            => (BrainFloatingPoint16)float.MinMagnitude(x, y);

        public static BrainFloatingPoint16 MinMagnitudeNumber(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
            => (BrainFloatingPoint16)float.MinMagnitudeNumber(x, y);

        public static BrainFloatingPoint16 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
            => (BrainFloatingPoint16)float.Parse(s, style, provider);

        public static BrainFloatingPoint16 Parse(string s, NumberStyles style, IFormatProvider? provider)
            => (BrainFloatingPoint16)float.Parse(s, style, provider);

        public static BrainFloatingPoint16 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
            => (BrainFloatingPoint16)float.Parse(s, provider);

        public static BrainFloatingPoint16 Parse(string s, IFormatProvider? provider)
            => (BrainFloatingPoint16)float.Parse(s, provider);

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out BrainFloatingPoint16 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (BrainFloatingPoint16)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateChecked(value);
                result = (BrainFloatingPoint16)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out BrainFloatingPoint16 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (BrainFloatingPoint16)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateSaturating(value);
                result = (BrainFloatingPoint16)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out BrainFloatingPoint16 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (BrainFloatingPoint16)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateTruncating(value);
                result = (BrainFloatingPoint16)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertToChecked<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (TOther)(object)value;
                return true;
            }
            try
            {
                float temp = (float)(value);
                result = TOther.CreateChecked(temp);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertToSaturating<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (TOther)(object)value;
                return true;
            }
            try
            {
                float temp = (float)(value);
                result = TOther.CreateSaturating(temp);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertToTruncating<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (TOther)(object)value;
                return true;
            }
            try
            {
                float temp = (float)(value);
                result = TOther.CreateTruncating(temp);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            float temp;
            var success = float.TryParse(s, style, provider, out temp);
            result = (BrainFloatingPoint16)temp;
            return success;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            float temp;
            var success = float.TryParse(s, style, provider, out temp);
            result = (BrainFloatingPoint16)temp;
            return success;
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            float temp;
            var success = float.TryParse(s, provider, out temp);
            result = (BrainFloatingPoint16)temp;
            return success;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            float temp;
            var success = float.TryParse(s, provider, out temp);
            result = (BrainFloatingPoint16)temp;
            return success;
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
            => ((float)this).ToString(format, formatProvider);

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ((float)this).TryFormat(destination, out charsWritten, format, provider);
        #endregion メソッド

        #region operators
        public static BrainFloatingPoint16 operator +(BrainFloatingPoint16 value)
            => value;

        public static BrainFloatingPoint16 operator +(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)((float)left + (float)right);

        public static BrainFloatingPoint16 operator -(BrainFloatingPoint16 value)
        {
            value.Bits ^= SignShiftedBitMask;// 符号ビットを反転
            return value;
        }

        public static BrainFloatingPoint16 operator -(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)((float)left - (float)right);

        public static BrainFloatingPoint16 operator ++(BrainFloatingPoint16 value)
            => (BrainFloatingPoint16)((float)value++);

        public static BrainFloatingPoint16 operator --(BrainFloatingPoint16 value)
            => (BrainFloatingPoint16)((float)value--);

        public static BrainFloatingPoint16 operator *(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)((float)left * (float)right);

        public static BrainFloatingPoint16 operator /(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)((float)left / (float)right);

        public static bool operator ==(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            return !(left == right);
        }
        public static bool operator >(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (float)left > (float)right;

        public static bool operator >=(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (float)left >= (float)right;

        public static bool operator <(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (float)left < (float)right;

        public static bool operator <=(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (float)left <= (float)right;

        public static BrainFloatingPoint16 operator %(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)((float)left % (float)right);
        #endregion operators

        #region 型変換
        public static BrainFloatingPoint16 FromSingle(float value)
        {
            ExtendedSingle extendedSingle = new ExtendedSingle(value);
            return new((BitsType)(extendedSingle.Bits >> 16));
        }
        public static float ToSingle(BrainFloatingPoint16 value)
        {
            ExtendedSingle extendedSingle = new ExtendedSingle();
            extendedSingle.Bits = (UInt32)value.Bits << 16;
            return extendedSingle;
        }
        #region 他の型→BrainFloatingPoint16
        public static explicit operator BrainFloatingPoint16(byte value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(sbyte value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(short value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(ushort value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(int value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(uint value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(long value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(ulong value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(Int128 value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(UInt128 value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(Half value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(float value) => FromSingle(value);
        public static explicit operator BrainFloatingPoint16(double value) => (BrainFloatingPoint16)(float)value;
        public static explicit operator BrainFloatingPoint16(decimal value) => (BrainFloatingPoint16)(float)value;
        #endregion 他の型→BrainFloatingPoint16
        #region BrainFloatingPoint16→他の型
        public static explicit operator byte(BrainFloatingPoint16 value) => (byte)(float)value;
        public static explicit operator sbyte(BrainFloatingPoint16 value) => (sbyte)(float)value;
        public static explicit operator short(BrainFloatingPoint16 value) => (short)(float)value;
        public static explicit operator ushort(BrainFloatingPoint16 value) => (ushort)(float)value;
        public static explicit operator int(BrainFloatingPoint16 value) => (int)(float)value;
        public static explicit operator uint(BrainFloatingPoint16 value) => (uint)(float)value;
        public static explicit operator long(BrainFloatingPoint16 value) => (long)(float)value;
        public static explicit operator ulong(BrainFloatingPoint16 value) => (ulong)(float)value;
        public static explicit operator Int128(BrainFloatingPoint16 value) => (Int128)(float)value;
        public static explicit operator UInt128(BrainFloatingPoint16 value) => (UInt128)(float)value;
        public static explicit operator Half(BrainFloatingPoint16 value) => (Half)(float)value;
        public static implicit operator float(BrainFloatingPoint16 value) => ToSingle(value);
        public static implicit operator double(BrainFloatingPoint16 value) => (double)(float)value;
        public static explicit operator decimal(BrainFloatingPoint16 value) => (decimal)(float)value;
        #endregion BrainFloatingPoint16→他の型
        #endregion 型変換

        #region IEquatable
        public bool Equals(BrainFloatingPoint16 other)
        {
            return Bits.Equals(other.Bits);
        }
        #endregion IEquatable

        #region override Object
        public override bool Equals(object obj)
        {
            return obj is BrainFloatingPoint16 && Equals((BrainFloatingPoint16)obj);
        }
        public override int GetHashCode()
        {
            return Bits.GetHashCode();
        }
        public override string ToString()
        {
            return ((float)this).ToString();
        }
        #endregion override Object

        #region IFloatingPointIeee754
        public static BrainFloatingPoint16 Atan2(BrainFloatingPoint16 y, BrainFloatingPoint16 x)
            => (BrainFloatingPoint16)float.Atan2(y, x);
        public static BrainFloatingPoint16 Atan2Pi(BrainFloatingPoint16 y, BrainFloatingPoint16 x)
            => (BrainFloatingPoint16)float.Atan2Pi(y, x);

        public static BrainFloatingPoint16 BitDecrement(BrainFloatingPoint16 x)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 BitIncrement(BrainFloatingPoint16 x)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 FusedMultiplyAdd(BrainFloatingPoint16 left, BrainFloatingPoint16 right, BrainFloatingPoint16 addend)
            => (BrainFloatingPoint16)float.FusedMultiplyAdd(left, right, addend);
        public static BrainFloatingPoint16 Ieee754Remainder(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
            => (BrainFloatingPoint16)float.Ieee754Remainder(left, right);
        public static int ILogB(BrainFloatingPoint16 x) => float.ILogB(x);
        public static BrainFloatingPoint16 ScaleB(BrainFloatingPoint16 x, int n) => (BrainFloatingPoint16)float.ScaleB(x, n);
        public static BrainFloatingPoint16 Exp(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Exp(x);
        public static BrainFloatingPoint16 Exp10(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Exp10(x);
        public static BrainFloatingPoint16 Exp2(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Exp2(x);

        public int GetExponentByteCount()
        {
            throw new NotImplementedException();
        }

        public int GetExponentShortestBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandBitLength()
        {
            throw new NotImplementedException();
        }

        public int GetSignificandByteCount()
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 Round(BrainFloatingPoint16 x, int digits, MidpointRounding mode) => (BrainFloatingPoint16)float.Round(x, digits, mode);

        public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return -1;
            }
            if (obj is BrainFloatingPoint16)
            {
                return CompareTo((BrainFloatingPoint16)obj);
            }
            return -1;
        }
        public int CompareTo(BrainFloatingPoint16 other)
        {
            return ((float)this).CompareTo((float)other);
        }
        public static BrainFloatingPoint16 Acosh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Acosh(x);
        public static BrainFloatingPoint16 Asinh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Asinh(x);
        public static BrainFloatingPoint16 Atanh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Atanh(x);
        public static BrainFloatingPoint16 Cosh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Cosh(x);
        public static BrainFloatingPoint16 Sinh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Sinh(x);
        public static BrainFloatingPoint16 Tanh(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Tanh(x);
        public static BrainFloatingPoint16 Log(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Log(x);
        public static BrainFloatingPoint16 Log(BrainFloatingPoint16 x, BrainFloatingPoint16 newBase) => (BrainFloatingPoint16)float.Log(x, newBase);
        public static BrainFloatingPoint16 Log10(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Log10(x);
        public static BrainFloatingPoint16 Log2(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Log2(x);
        public static BrainFloatingPoint16 Pow(BrainFloatingPoint16 x, BrainFloatingPoint16 y) => (BrainFloatingPoint16)float.Pow(x, y);
        public static BrainFloatingPoint16 Cbrt(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Cbrt(x);
        public static BrainFloatingPoint16 Hypot(BrainFloatingPoint16 x, BrainFloatingPoint16 y) => (BrainFloatingPoint16)float.Hypot(x, y);
        public static BrainFloatingPoint16 RootN(BrainFloatingPoint16 x, int n) => (BrainFloatingPoint16)float.RootN(x, n);
        public static BrainFloatingPoint16 Sqrt(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Sqrt(x);
        public static BrainFloatingPoint16 Acos(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Acos(x);
        public static BrainFloatingPoint16 AcosPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.AcosPi(x);
        public static BrainFloatingPoint16 Asin(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Asin(x);
        public static BrainFloatingPoint16 AsinPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.AsinPi(x);
        public static BrainFloatingPoint16 Atan(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Atan(x);
        public static BrainFloatingPoint16 AtanPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.AtanPi(x);
        public static BrainFloatingPoint16 Cos(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Cos(x);
        public static BrainFloatingPoint16 CosPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.CosPi(x);
        public static BrainFloatingPoint16 Sin(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Sin(x);
        public static (BrainFloatingPoint16 Sin, BrainFloatingPoint16 Cos) SinCos(BrainFloatingPoint16 x)
        {
            (float sin, float cos) = float.SinCos(x);
            return ((BrainFloatingPoint16)sin, (BrainFloatingPoint16)cos);
        }
        public static (BrainFloatingPoint16 SinPi, BrainFloatingPoint16 CosPi) SinCosPi(BrainFloatingPoint16 x)
        {
            (float sinPi, float cosPi) = float.SinCosPi(x);
            return ((BrainFloatingPoint16)sinPi, (BrainFloatingPoint16)cosPi);
        }
        public static BrainFloatingPoint16 SinPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.SinPi(x);
        public static BrainFloatingPoint16 Tan(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.Tan(x);
        public static BrainFloatingPoint16 TanPi(BrainFloatingPoint16 x) => (BrainFloatingPoint16)float.TanPi(x);
        #endregion IFloatingPointIeee754
    }
}