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
        IFloatingPointConstants<BrainFloatingPoint16>
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

        #region IFloatingPointConstants
        public static BrainFloatingPoint16 E => (BrainFloatingPoint16)float.E;

        public static BrainFloatingPoint16 Pi => (BrainFloatingPoint16)float.Pi;

        public static BrainFloatingPoint16 Tau => (BrainFloatingPoint16)float.Tau;

        public static BrainFloatingPoint16 One => (BrainFloatingPoint16)1.0f;

        public static BrainFloatingPoint16 Zero => new(0);

        public static BrainFloatingPoint16 AdditiveIdentity => Zero;

        public static BrainFloatingPoint16 MultiplicativeIdentity => One;

        static int INumberBase<BrainFloatingPoint16>.Radix => 2;
        #endregion IFloatingPointConstants
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
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out BrainFloatingPoint16 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out BrainFloatingPoint16 result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(BrainFloatingPoint16 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
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
        /// <summary>
        /// BrainFloatingPoint16 から byte への明示的な変換を定義します。
        /// </summary>
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
    }
}