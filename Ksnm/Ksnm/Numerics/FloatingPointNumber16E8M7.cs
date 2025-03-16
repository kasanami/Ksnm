using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ksnm.Numerics
{
    using BitsType = System.UInt16;// 全体のビットを表す型
    /// <summary>
    /// 16ビットの浮動小数点数
    /// ・Google Brainによって2018年に開発された、より機械学習での利用に適したフォーマット。
    /// ・符号ビット : 1ビット
    /// ・指数部 : 8ビット
    /// ・仮数部 : 7ビット(暗黙的に8ビットの表現幅を持つ。)
    /// </summary>
    public struct FloatingPointNumber16E8M7 :
        IFloatingPointProperties<BitsType>,
        IFloatingPointConstants<FloatingPointNumber16E8M7>,
        IFloatingPointIeee754<FloatingPointNumber16E8M7>,
        IMinMaxValue<FloatingPointNumber16E8M7>
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
        public static FloatingPointNumber16E8M7 E => (FloatingPointNumber16E8M7)float.E;

        public static FloatingPointNumber16E8M7 Pi => (FloatingPointNumber16E8M7)float.Pi;

        public static FloatingPointNumber16E8M7 Tau => (FloatingPointNumber16E8M7)float.Tau;

        public static FloatingPointNumber16E8M7 One => new(PositiveOneBits);

        public static FloatingPointNumber16E8M7 Zero => new(PositiveZeroBits);

        public static FloatingPointNumber16E8M7 AdditiveIdentity => Zero;

        public static FloatingPointNumber16E8M7 MultiplicativeIdentity => One;

        static int INumberBase<FloatingPointNumber16E8M7>.Radix => 2;
        #endregion IFloatingPointConstants
        #region IMinMaxValue
        /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue" />
        public static FloatingPointNumber16E8M7 MaxValue => new(MaxValueBits);
        /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue" />
        public static FloatingPointNumber16E8M7 MinValue => new(MinValueBits);
        #endregion IMinMaxValue
        #region IFloatingPointIeee754
        public static FloatingPointNumber16E8M7 Epsilon => new(EpsilonBits);
        public static FloatingPointNumber16E8M7 NaN => new(NegativeQNaNBits);
        public static FloatingPointNumber16E8M7 PositiveInfinity => new(PositiveInfinityBits);
        public static FloatingPointNumber16E8M7 NegativeInfinity => new(NegativeInfinityBits);
        /// <inheritdoc cref="IFloatingPointIeee754{TSelf}.NegativeZero" />
        public static FloatingPointNumber16E8M7 NegativeZero => new(NegativeZeroBits);
        /// <inheritdoc cref="IFloatingPointIeee754{TSelf}.NegativeOne" />
        public static FloatingPointNumber16E8M7 NegativeOne => new(NegativeOneBits);
        #endregion IFloatingPointIeee754
        #endregion 定数

        #region フィールド
        private BitsType bits;
        #endregion フィールド

        #region プロパティ
        public BitsType Bits { get => bits; set => bits = value; }

        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public BitsType SignBit
        {
            get => (BitsType)(Bits >> SignBitShift);
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
        public BitsType ExponentBits
        {
            get => (BitsType)((Bits >> ExponentBitShift) & ExponentBitMask);
            set => Bits = (BitsType)((Bits & ~ExponentShiftedBitMask) | ((value & ExponentBitMask) << ExponentBitShift));
        }
        /// <summary>
        /// 指数を取得/設定
        /// 2のべき乗の指数（2^Exponent）
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
                return ExponentBits - ExponentBias;
            }
            set => ExponentBits = (ushort)(value + ExponentBias);
        }
        /// <summary>
        /// 倍率
        /// Mantissaと乗算すると元の値になる係数
        /// </summary>
        public double Scale => System.Math.Pow(Radix, Exponent - MantissaLength);

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
                // 非正規化数なら最上位に1を加えずに返す
                if (ExponentBits == 0)
                {
                    return MantissaBits;
                }
                // ((UInt)1 << MantissaLength)は"1."を意味する
                return (BitsType)(MantissaBits | ((BitsType)1 << MantissaLength));
            }
            set => Bits = value;
        }
        int IFloatingPointProperties<BitsType>.MantissaLength => MantissaLength;

        public double Coefficient => Mantissa / (double)(1 << MantissaLength);
        bool IFloatingPointProperties<BitsType>.IsPositive => IsPositive(this);
        bool IFloatingPointProperties<BitsType>.IsNegative => IsNegative(this);
        bool IFloatingPointProperties<BitsType>.IsZero => IsZero(this);
        bool IFloatingPointProperties<BitsType>.IsInfinity => IsInfinity(this);
        bool IFloatingPointProperties<BitsType>.IsPositiveInfinity => IsPositiveInfinity(this);
        bool IFloatingPointProperties<BitsType>.IsNegativeInfinity => IsNegativeInfinity(this);
        bool IFloatingPointProperties<BitsType>.IsNaN => IsNaN(this);
        #endregion プロパティ

        #region コンストラクタ
        public FloatingPointNumber16E8M7()
        {
        }
        public FloatingPointNumber16E8M7(BitsType bits)
        {
            Bits = bits;
        }
        #endregion コンストラクタ

        #region メソッド
        public static FloatingPointNumber16E8M7 Abs(FloatingPointNumber16E8M7 value)
            => (FloatingPointNumber16E8M7)float.Abs(value);

        public static bool IsCanonical(FloatingPointNumber16E8M7 value)
            => throw new NotImplementedException();

        public static bool IsComplexNumber(FloatingPointNumber16E8M7 value)
            => false;

        public static bool IsEvenInteger(FloatingPointNumber16E8M7 value)
            => float.IsEvenInteger(value);

        public static bool IsFinite(FloatingPointNumber16E8M7 value)
            => float.IsFinite(value);

        public static bool IsImaginaryNumber(FloatingPointNumber16E8M7 value)
            => false;

        public static bool IsInfinity(FloatingPointNumber16E8M7 value)
            => float.IsInfinity(value);

        public static bool IsInteger(FloatingPointNumber16E8M7 value)
            => float.IsInteger(value);

        public static bool IsNaN(FloatingPointNumber16E8M7 value)
            => float.IsNaN(value);

        public static bool IsNegative(FloatingPointNumber16E8M7 value)
            => float.IsNegative(value);

        public static bool IsNegativeInfinity(FloatingPointNumber16E8M7 value)
            => float.IsNegativeInfinity(value);

        public static bool IsNormal(FloatingPointNumber16E8M7 value)
            => float.IsNormal(value);

        public static bool IsOddInteger(FloatingPointNumber16E8M7 value)
            => float.IsOddInteger(value);

        public static bool IsPositive(FloatingPointNumber16E8M7 value)
            => float.IsPositive(value);

        public static bool IsPositiveInfinity(FloatingPointNumber16E8M7 value)
            => float.IsPositiveInfinity(value);

        public static bool IsRealNumber(FloatingPointNumber16E8M7 value)
            => float.IsRealNumber(value);

        public static bool IsSubnormal(FloatingPointNumber16E8M7 value)
            => float.IsSubnormal(value);

        public static bool IsZero(FloatingPointNumber16E8M7 value)
        {
            // 符号ビット以外が0なら0を返す
            return (value.Bits & ~SignShiftedBitMask) == 0;
        }

        public static FloatingPointNumber16E8M7 MaxMagnitude(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y)
            => (FloatingPointNumber16E8M7)float.MaxMagnitude(x, y);

        public static FloatingPointNumber16E8M7 MaxMagnitudeNumber(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y)
            => (FloatingPointNumber16E8M7)float.MaxMagnitudeNumber(x, y);

        public static FloatingPointNumber16E8M7 MinMagnitude(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y)
            => (FloatingPointNumber16E8M7)float.MinMagnitude(x, y);

        public static FloatingPointNumber16E8M7 MinMagnitudeNumber(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y)
            => (FloatingPointNumber16E8M7)float.MinMagnitudeNumber(x, y);

        public static FloatingPointNumber16E8M7 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
            => (FloatingPointNumber16E8M7)float.Parse(s, style, provider);

        public static FloatingPointNumber16E8M7 Parse(string s, NumberStyles style, IFormatProvider? provider)
            => (FloatingPointNumber16E8M7)float.Parse(s, style, provider);

        public static FloatingPointNumber16E8M7 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
            => (FloatingPointNumber16E8M7)float.Parse(s, provider);

        public static FloatingPointNumber16E8M7 Parse(string s, IFormatProvider? provider)
            => (FloatingPointNumber16E8M7)float.Parse(s, provider);

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (FloatingPointNumber16E8M7)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateChecked(value);
                result = (FloatingPointNumber16E8M7)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (FloatingPointNumber16E8M7)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateSaturating(value);
                result = (FloatingPointNumber16E8M7)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result) where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(float))
            {
                result = (FloatingPointNumber16E8M7)(object)value;
                return true;
            }
            try
            {
                float temp = float.CreateTruncating(value);
                result = (FloatingPointNumber16E8M7)temp;
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryConvertToChecked<TOther>(FloatingPointNumber16E8M7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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

        public static bool TryConvertToSaturating<TOther>(FloatingPointNumber16E8M7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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

        public static bool TryConvertToTruncating<TOther>(FloatingPointNumber16E8M7 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result)
        {
            float temp;
            var success = float.TryParse(s, style, provider, out temp);
            result = (FloatingPointNumber16E8M7)temp;
            return success;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result)
        {
            float temp;
            var success = float.TryParse(s, style, provider, out temp);
            result = (FloatingPointNumber16E8M7)temp;
            return success;
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result)
        {
            float temp;
            var success = float.TryParse(s, provider, out temp);
            result = (FloatingPointNumber16E8M7)temp;
            return success;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FloatingPointNumber16E8M7 result)
        {
            float temp;
            var success = float.TryParse(s, provider, out temp);
            result = (FloatingPointNumber16E8M7)temp;
            return success;
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
            => ((float)this).ToString(format, formatProvider);

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
            => ((float)this).TryFormat(destination, out charsWritten, format, provider);
        #endregion メソッド

        #region operators
        public static FloatingPointNumber16E8M7 operator +(FloatingPointNumber16E8M7 value)
            => value;

        public static FloatingPointNumber16E8M7 operator +(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)((float)left + (float)right);

        public static FloatingPointNumber16E8M7 operator -(FloatingPointNumber16E8M7 value)
        {
            value.Bits ^= SignShiftedBitMask;// 符号ビットを反転
            return value;
        }

        public static FloatingPointNumber16E8M7 operator -(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)((float)left - (float)right);

        public static FloatingPointNumber16E8M7 operator ++(FloatingPointNumber16E8M7 value)
            => (FloatingPointNumber16E8M7)((float)value++);

        public static FloatingPointNumber16E8M7 operator --(FloatingPointNumber16E8M7 value)
            => (FloatingPointNumber16E8M7)((float)value--);

        public static FloatingPointNumber16E8M7 operator *(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)((float)left * (float)right);

        public static FloatingPointNumber16E8M7 operator /(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)((float)left / (float)right);

        public static bool operator ==(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
        {
            return !(left == right);
        }
        public static bool operator >(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (float)left > (float)right;

        public static bool operator >=(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (float)left >= (float)right;

        public static bool operator <(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (float)left < (float)right;

        public static bool operator <=(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (float)left <= (float)right;

        public static FloatingPointNumber16E8M7 operator %(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)((float)left % (float)right);
        #endregion operators

        #region 型変換
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FloatingPointNumber16E8M7 FromSingle(float value)
        {
            ExtendedSingle extendedSingle = new(value);
            return new(extendedSingle.UpperBits);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToSingle(FloatingPointNumber16E8M7 value)
        {
            ExtendedSingle extendedSingle = new();
            extendedSingle.UpperBits = value.Bits;
            return extendedSingle.Value;
        }
        #region 他の型→BrainFloatingPoint16
        public static explicit operator FloatingPointNumber16E8M7(byte value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(sbyte value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(short value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(ushort value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(int value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(uint value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(long value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(ulong value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(Int128 value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(UInt128 value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(Half value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(float value) => FromSingle(value);
        public static explicit operator FloatingPointNumber16E8M7(double value) => (FloatingPointNumber16E8M7)(float)value;
        public static explicit operator FloatingPointNumber16E8M7(decimal value) => (FloatingPointNumber16E8M7)(float)value;
        #endregion 他の型→BrainFloatingPoint16
        #region BrainFloatingPoint16→他の型
        public static explicit operator byte(FloatingPointNumber16E8M7 value) => (byte)(float)value;
        public static explicit operator sbyte(FloatingPointNumber16E8M7 value) => (sbyte)(float)value;
        public static explicit operator short(FloatingPointNumber16E8M7 value) => (short)(float)value;
        public static explicit operator ushort(FloatingPointNumber16E8M7 value) => (ushort)(float)value;
        public static explicit operator int(FloatingPointNumber16E8M7 value) => (int)(float)value;
        public static explicit operator uint(FloatingPointNumber16E8M7 value) => (uint)(float)value;
        public static explicit operator long(FloatingPointNumber16E8M7 value) => (long)(float)value;
        public static explicit operator ulong(FloatingPointNumber16E8M7 value) => (ulong)(float)value;
        public static explicit operator Int128(FloatingPointNumber16E8M7 value) => (Int128)(float)value;
        public static explicit operator UInt128(FloatingPointNumber16E8M7 value) => (UInt128)(float)value;
        public static explicit operator Half(FloatingPointNumber16E8M7 value) => (Half)(float)value;
        public static implicit operator float(FloatingPointNumber16E8M7 value) => ToSingle(value);
        public static implicit operator double(FloatingPointNumber16E8M7 value) => (double)(float)value;
        public static explicit operator decimal(FloatingPointNumber16E8M7 value) => (decimal)(float)value;
        #endregion BrainFloatingPoint16→他の型
        #endregion 型変換

        #region IEquatable
        public bool Equals(FloatingPointNumber16E8M7 other)
        {
            return Bits.Equals(other.Bits);
        }
        #endregion IEquatable

        #region override Object
        public override bool Equals(object obj)
        {
            return obj is FloatingPointNumber16E8M7 && Equals((FloatingPointNumber16E8M7)obj);
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
        public static FloatingPointNumber16E8M7 Atan2(FloatingPointNumber16E8M7 y, FloatingPointNumber16E8M7 x)
            => (FloatingPointNumber16E8M7)float.Atan2(y, x);
        public static FloatingPointNumber16E8M7 Atan2Pi(FloatingPointNumber16E8M7 y, FloatingPointNumber16E8M7 x)
            => (FloatingPointNumber16E8M7)float.Atan2Pi(y, x);

        public static FloatingPointNumber16E8M7 BitDecrement(FloatingPointNumber16E8M7 x)
        {
            throw new NotImplementedException();
        }

        public static FloatingPointNumber16E8M7 BitIncrement(FloatingPointNumber16E8M7 x)
        {
            throw new NotImplementedException();
        }

        public static FloatingPointNumber16E8M7 FusedMultiplyAdd(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right, FloatingPointNumber16E8M7 addend)
            => (FloatingPointNumber16E8M7)float.FusedMultiplyAdd(left, right, addend);
        public static FloatingPointNumber16E8M7 Ieee754Remainder(FloatingPointNumber16E8M7 left, FloatingPointNumber16E8M7 right)
            => (FloatingPointNumber16E8M7)float.Ieee754Remainder(left, right);
        public static int ILogB(FloatingPointNumber16E8M7 x) => float.ILogB(x);
        public static FloatingPointNumber16E8M7 ScaleB(FloatingPointNumber16E8M7 x, int n) => (FloatingPointNumber16E8M7)float.ScaleB(x, n);
        public static FloatingPointNumber16E8M7 Exp(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Exp(x);
        public static FloatingPointNumber16E8M7 Exp10(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Exp10(x);
        public static FloatingPointNumber16E8M7 Exp2(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Exp2(x);

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

        public static FloatingPointNumber16E8M7 Round(FloatingPointNumber16E8M7 x, int digits, MidpointRounding mode) => (FloatingPointNumber16E8M7)float.Round(x, digits, mode);

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
            if (obj is FloatingPointNumber16E8M7)
            {
                return CompareTo((FloatingPointNumber16E8M7)obj);
            }
            return -1;
        }
        public int CompareTo(FloatingPointNumber16E8M7 other)
        {
            return ((float)this).CompareTo((float)other);
        }
        public static FloatingPointNumber16E8M7 Acosh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Acosh(x);
        public static FloatingPointNumber16E8M7 Asinh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Asinh(x);
        public static FloatingPointNumber16E8M7 Atanh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Atanh(x);
        public static FloatingPointNumber16E8M7 Cosh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Cosh(x);
        public static FloatingPointNumber16E8M7 Sinh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Sinh(x);
        public static FloatingPointNumber16E8M7 Tanh(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Tanh(x);
        public static FloatingPointNumber16E8M7 Log(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Log(x);
        public static FloatingPointNumber16E8M7 Log(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 newBase) => (FloatingPointNumber16E8M7)float.Log(x, newBase);
        public static FloatingPointNumber16E8M7 Log10(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Log10(x);
        public static FloatingPointNumber16E8M7 Log2(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Log2(x);
        public static FloatingPointNumber16E8M7 Pow(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y) => (FloatingPointNumber16E8M7)float.Pow(x, y);
        public static FloatingPointNumber16E8M7 Cbrt(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Cbrt(x);
        public static FloatingPointNumber16E8M7 Hypot(FloatingPointNumber16E8M7 x, FloatingPointNumber16E8M7 y) => (FloatingPointNumber16E8M7)float.Hypot(x, y);
        public static FloatingPointNumber16E8M7 RootN(FloatingPointNumber16E8M7 x, int n) => (FloatingPointNumber16E8M7)float.RootN(x, n);
        public static FloatingPointNumber16E8M7 Sqrt(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Sqrt(x);
        public static FloatingPointNumber16E8M7 Acos(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Acos(x);
        public static FloatingPointNumber16E8M7 AcosPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.AcosPi(x);
        public static FloatingPointNumber16E8M7 Asin(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Asin(x);
        public static FloatingPointNumber16E8M7 AsinPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.AsinPi(x);
        public static FloatingPointNumber16E8M7 Atan(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Atan(x);
        public static FloatingPointNumber16E8M7 AtanPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.AtanPi(x);
        public static FloatingPointNumber16E8M7 Cos(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Cos(x);
        public static FloatingPointNumber16E8M7 CosPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.CosPi(x);
        public static FloatingPointNumber16E8M7 Sin(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Sin(x);
        public static (FloatingPointNumber16E8M7 Sin, FloatingPointNumber16E8M7 Cos) SinCos(FloatingPointNumber16E8M7 x)
        {
            (float sin, float cos) = float.SinCos(x);
            return ((FloatingPointNumber16E8M7)sin, (FloatingPointNumber16E8M7)cos);
        }
        public static (FloatingPointNumber16E8M7 SinPi, FloatingPointNumber16E8M7 CosPi) SinCosPi(FloatingPointNumber16E8M7 x)
        {
            (float sinPi, float cosPi) = float.SinCosPi(x);
            return ((FloatingPointNumber16E8M7)sinPi, (FloatingPointNumber16E8M7)cosPi);
        }
        public static FloatingPointNumber16E8M7 SinPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.SinPi(x);
        public static FloatingPointNumber16E8M7 Tan(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.Tan(x);
        public static FloatingPointNumber16E8M7 TanPi(FloatingPointNumber16E8M7 x) => (FloatingPointNumber16E8M7)float.TanPi(x);
        #endregion IFloatingPointIeee754
    }
}