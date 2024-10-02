using Ksnm.ExtensionMethods.System.Decimal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    using Decimal = System.Decimal;
    using UInt = System.UInt128;
    /// <summary>
    /// 10進浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedDecimal :
        IFloatingPointIeee754<ExtendedDecimal>,
        IFloatingPointConstants<ExtendedDecimal>,
        IExponentialFunctions<ExtendedDecimal>,
        IHyperbolicFunctions<ExtendedDecimal>,
        ILogarithmicFunctions<ExtendedDecimal>,
        IPowerFunctions<ExtendedDecimal>,
        IRootFunctions<ExtendedDecimal>,
        ITrigonometricFunctions<ExtendedDecimal>
    {
        #region IFloatingPointConstants
        /// <summary>
        /// ネイピア数(四捨五入している)
        /// 2.7182818284590452353602874713 52
        /// </summary>
        public static ExtendedDecimal E => 2.7182818284590452353602874714m;
        /// <summary>
        /// 円周率(四捨五入している)
        /// 3.1415926535897932384626433832 79 
        /// </summary>
        public static ExtendedDecimal Pi => 3.1415926535897932384626433833m;
        /// <summary>
        /// 円周率*2(四捨五入している)
        /// 6.2831853071795864769252867665 59
        /// </summary>
        public static ExtendedDecimal Tau => 6.2831853071795864769252867666m;
        #endregion IFloatingPointConstants

        #region 定数
        /// <summary>
        /// 符号部のビット数
        /// </summary>
        public const int SignLength = 1;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public static readonly UInt SignBitMask = 1;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 7;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public static readonly UInt ExponentBitMask = ((UInt)1 << ExponentLength) - 1;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 32 * 3;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public static readonly UInt MantissaBitMask = ((UInt)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド オフセットの位置がイメージと違う
        [FieldOffset(0)] public Decimal Value;
        [FieldOffset(4 * 0)] public UInt32 Flags;
        [FieldOffset(4 * 1)] public UInt32 HiBits;
        [FieldOffset(4 * 3)] public UInt32 MidBits;
        [FieldOffset(4 * 2)] public UInt32 LoBits;

        [FieldOffset(0)] private UInt _bits;
        [FieldOffset(4 * 0)] private UInt32 _bits0;
        [FieldOffset(4 * 1)] private UInt32 _bits1;
        [FieldOffset(4 * 3)] private UInt32 _bits2;
        [FieldOffset(4 * 2)] private UInt32 _bits3;

        [FieldOffset(2)] private byte _exponentBits;
        [FieldOffset(3)] private byte _signBits;
        #endregion フィールド

        #region プロパティ
        public UInt Bits
        {
            get
            {
                return
                    ((UInt)_bits0 << 96) |
                    ((UInt)_bits1 << 64) |
                    ((UInt)_bits2 << 32) |
                    ((UInt)_bits3);
            }
            set
            {
                _bits0 = (UInt32)((value >> 96) & UInt.MaxValue);
                _bits1 = (UInt32)((value >> 64) & UInt.MaxValue); ;
                _bits2 = (UInt32)((value >> 32) & UInt.MaxValue);
                _bits3 = (UInt32)((value) & UInt.MaxValue);
            }
        }
        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public byte SignBit
        {
            get => (byte)(_signBits >> 7);
            set => _signBits = (byte)(value << 7);
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
        public byte ExponentBits
        {
            get => _exponentBits;
            set => _exponentBits = (byte)(value & ExponentBitMask);
        }
        /// <summary>
        /// 指数を取得/設定
        /// 0.1の場合は-1。
        /// </summary>
        public int Exponent => -ExponentBits;
        /// <summary>
        /// 倍率
        /// Mantissaと乗算すると元の値になる係数
        /// </summary>
        public double Scale => System.Math.Pow(Radix, Exponent);

        /// <summary>
        /// 仮数部を取得/設定
        /// </summary>
        public UInt MantissaBits
        {
            get => Bits & MantissaBitMask;
            set => Bits = (Bits & ~MantissaBitMask) | (value & MantissaBitMask);
        }
        /// <summary>
        /// 仮数を取得/設定
        /// </summary>
        public UInt Mantissa => MantissaBits;
        #endregion プロパティ


        #region 型変換
        /// <summary>
        /// Decimal から ExtendedDecimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator ExtendedDecimal(Decimal value)
        {
            return new ExtendedDecimal() { Value = value };
        }
        /// <summary>
        /// ExtendedDecimal から Decimal への暗黙的な変換を定義します。
        /// </summary>
        public static implicit operator Decimal(ExtendedDecimal value)
        {
            return value.Value;
        }
        #endregion 型変換

        #region object
        public override string ToString()
        {
            return Value.ToString();
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        #endregion object

        #region IFloatingPointIeee754
        public static ExtendedDecimal One => Decimal.One;

        public static int Radix => 10;

        public static ExtendedDecimal Zero => Decimal.Zero;

        public static ExtendedDecimal AdditiveIdentity => Decimal.Zero;

        public static ExtendedDecimal MultiplicativeIdentity => Decimal.One;

        public static ExtendedDecimal Epsilon => new Decimal(1, 0, 0, false, 28);

        public static ExtendedDecimal NaN => throw new NotImplementedException();

        public static ExtendedDecimal NegativeInfinity => throw new NotImplementedException();

        public static ExtendedDecimal NegativeZero => -0;

        public static ExtendedDecimal PositiveInfinity => throw new NotImplementedException();

        public static ExtendedDecimal NegativeOne => -1;
        #endregion IFloatingPointIeee754

        public static ExtendedDecimal Abs(ExtendedDecimal value) => Decimal.Abs(value);

        #region IExponentialFunctions
        public static ExtendedDecimal Exp(ExtendedDecimal x) => Math.Exp(x, 0, 28);
        public static ExtendedDecimal Exp10(ExtendedDecimal x) => Math.Exp10(x, 0, 28);
        public static ExtendedDecimal Exp2(ExtendedDecimal x) => Math.Exp2(x, 0, 28);
        #endregion IExponentialFunctions

        #region IHyperbolicFunctions
        public static ExtendedDecimal Acosh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Asinh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Atanh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Cosh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Sinh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Tanh(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }
        #endregion IHyperbolicFunctions

        #region ILogarithmicFunctions
        public static ExtendedDecimal Log(ExtendedDecimal x) => Math.Log(x, Epsilon, 10000);
        public static ExtendedDecimal Log(ExtendedDecimal x, ExtendedDecimal newBase) => Math.LogB(x, newBase, Epsilon, 10000);
        public static ExtendedDecimal Log10(ExtendedDecimal x) => Math.Log10(x, Epsilon, 10000);
        public static ExtendedDecimal Log2(ExtendedDecimal x) => Math.Log2(x, Epsilon, 10000);
        #endregion ILogarithmicFunctions

        #region IPowerFunctions
        public static ExtendedDecimal Pow(ExtendedDecimal x, ExtendedDecimal y) => Math.Pow(x, y, 0, 28);
        #endregion IPowerFunctions

        #region IRootFunctions
        public static ExtendedDecimal Cbrt(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Hypot(ExtendedDecimal x, ExtendedDecimal y)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal RootN(ExtendedDecimal x, int n)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Sqrt(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }
        #endregion IRootFunctions

        #region ITrigonometricFunctions
        public static ExtendedDecimal Acos(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal AcosPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Asin(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal AsinPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Atan(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal AtanPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Cos(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal CosPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }
        public static ExtendedDecimal Sin(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static (ExtendedDecimal Sin, ExtendedDecimal Cos) SinCos(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static (ExtendedDecimal SinPi, ExtendedDecimal CosPi) SinCosPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal SinPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Tan(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal TanPi(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        #endregion ITrigonometricFunctions

        public static bool IsCanonical(ExtendedDecimal value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(ExtendedDecimal value) => false;
        public static bool IsEvenInteger(ExtendedDecimal value) => Decimal.IsEvenInteger(value);

        public static bool IsFinite(ExtendedDecimal value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(ExtendedDecimal value) => false;

        public static bool IsInfinity(ExtendedDecimal value) => false;

        public static bool IsInteger(ExtendedDecimal value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(ExtendedDecimal value) => false;

        public static bool IsNegative(ExtendedDecimal value) => Decimal.IsNegative(value);

        public static bool IsNegativeInfinity(ExtendedDecimal value) => false;

        public static bool IsNormal(ExtendedDecimal value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(ExtendedDecimal value) => Decimal.IsOddInteger(value);
        public static bool IsPositive(ExtendedDecimal value) => Decimal.IsPositive(value);
        public static bool IsPositiveInfinity(ExtendedDecimal value) => false;

        public static bool IsRealNumber(ExtendedDecimal value) => true;

        public static bool IsSubnormal(ExtendedDecimal value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(ExtendedDecimal value) => value == Zero;

        public static ExtendedDecimal MaxMagnitude(ExtendedDecimal x, ExtendedDecimal y)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal MaxMagnitudeNumber(ExtendedDecimal x, ExtendedDecimal y)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal MinMagnitude(ExtendedDecimal x, ExtendedDecimal y)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal MinMagnitudeNumber(ExtendedDecimal x, ExtendedDecimal y)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ExtendedDecimal other) => Value.Equals(other.Value);

        public string ToString(string? format, IFormatProvider? formatProvider)
            => Value.ToString(format, formatProvider);

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertFromChecked<TOther>(TOther value, out ExtendedDecimal result)
        {
            try
            {
                result = Decimal.CreateChecked(value);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertFromSaturating<TOther>(TOther value, out ExtendedDecimal result)
        {
            try
            {
                result = Decimal.CreateSaturating(value);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertFromTruncating<TOther>(TOther value, out ExtendedDecimal result)
        {
            try
            {
                result = Decimal.CreateTruncating(value);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertToChecked<TOther>(ExtendedDecimal value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertToSaturating<TOther>(ExtendedDecimal value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<ExtendedDecimal>.TryConvertToTruncating<TOther>(ExtendedDecimal value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<ExtendedDecimal>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<ExtendedDecimal>.TryParse(string? s, NumberStyles style, IFormatProvider? provider, out ExtendedDecimal result)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<ExtendedDecimal>.Equals(ExtendedDecimal other) => Value.Equals(other.Value);

        bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => Value.TryFormat(destination, out charsWritten, format, provider);

        string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
            => Value.ToString(format, formatProvider);

        static ExtendedDecimal ISpanParsable<ExtendedDecimal>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
            => Decimal.Parse(s, provider);

        static bool ISpanParsable<ExtendedDecimal>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out ExtendedDecimal result)
        {
            Decimal result2;
            var success = Decimal.TryParse(s, provider, out result2);
            result = result2;
            return success;
        }

        static ExtendedDecimal IParsable<ExtendedDecimal>.Parse(string s, IFormatProvider? provider)
            => Decimal.Parse(s, provider);

        static bool IParsable<ExtendedDecimal>.TryParse(string? s, IFormatProvider? provider, out ExtendedDecimal result)
        {
            Decimal result2;
            var success = Decimal.TryParse(s, provider, out result2);
            result = result2;
            return success;
        }

        public static ExtendedDecimal Atan2(ExtendedDecimal y, ExtendedDecimal x)
            => Math.Atan2(y, x);

        public static ExtendedDecimal Atan2Pi(ExtendedDecimal y, ExtendedDecimal x)
            => Atan2(y, x) / Pi;

        public static ExtendedDecimal BitDecrement(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal BitIncrement(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal FusedMultiplyAdd(ExtendedDecimal left, ExtendedDecimal right, ExtendedDecimal addend)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal Ieee754Remainder(ExtendedDecimal left, ExtendedDecimal right)
        {
            throw new NotImplementedException();
        }

        public static int ILogB(ExtendedDecimal x)
        {
            throw new NotImplementedException();
        }

        public static ExtendedDecimal ScaleB(ExtendedDecimal x, int n)
        {
            throw new NotImplementedException();
        }

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

        #region IFloatingPoint
        public static ExtendedDecimal Round(ExtendedDecimal x, int digits, MidpointRounding mode)
            => Decimal.Round(x, digits, mode);

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
        #endregion IFloatingPoint

        public int CompareTo(object? obj) => Value.CompareTo(obj);
        public int CompareTo(ExtendedDecimal other) => Value.CompareTo(other.Value);
        public static ExtendedDecimal operator +(ExtendedDecimal value) => +value.Value;
        public static ExtendedDecimal operator +(ExtendedDecimal left, ExtendedDecimal right) => left.Value + right.Value;
        public static ExtendedDecimal operator -(ExtendedDecimal value) => -value.Value;
        public static ExtendedDecimal operator -(ExtendedDecimal left, ExtendedDecimal right) => left.Value - right.Value;
        public static ExtendedDecimal operator ++(ExtendedDecimal value) => value.Value++;
        public static ExtendedDecimal operator --(ExtendedDecimal value) => value.Value--;
        public static ExtendedDecimal operator *(ExtendedDecimal left, ExtendedDecimal right) => left.Value * right.Value;
        public static ExtendedDecimal operator /(ExtendedDecimal left, ExtendedDecimal right) => left.Value / right.Value;
        public static bool operator ==(ExtendedDecimal left, ExtendedDecimal right) => left.Value == right.Value;
        public static bool operator !=(ExtendedDecimal left, ExtendedDecimal right) => left.Value != right.Value;
        public static bool operator >(ExtendedDecimal left, ExtendedDecimal right) => left.Value > right.Value;
        public static bool operator >=(ExtendedDecimal left, ExtendedDecimal right) => left.Value >= right.Value;
        public static bool operator <(ExtendedDecimal left, ExtendedDecimal right) => left.Value < right.Value;
        public static bool operator <=(ExtendedDecimal left, ExtendedDecimal right) => left.Value <= right.Value;
        public static ExtendedDecimal operator %(ExtendedDecimal left, ExtendedDecimal right) => left.Value % right.Value;
    }
}