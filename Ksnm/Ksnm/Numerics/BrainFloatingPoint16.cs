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
        public static BrainFloatingPoint16 E => throw new NotImplementedException();

        public static BrainFloatingPoint16 Pi => throw new NotImplementedException();

        public static BrainFloatingPoint16 Tau => throw new NotImplementedException();

        public static BrainFloatingPoint16 One => new(0b0000_0000_0000_0000);

        public static BrainFloatingPoint16 Zero => new (0);

        public static BrainFloatingPoint16 AdditiveIdentity => throw new NotImplementedException();

        public static BrainFloatingPoint16 MultiplicativeIdentity => throw new NotImplementedException();

        static int INumberBase<BrainFloatingPoint16>.Radix => throw new NotImplementedException();
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
                // 符号ビット以外が0なら0を返す
                if ((Bits & ~SignShiftedBitMask) == 0)
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
                // 符号ビット以外が0なら0を返す
                if ((Bits & ~SignShiftedBitMask) == 0)
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
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegativeInfinity(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositiveInfinity(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 MaxMagnitude(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 MaxMagnitudeNumber(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 MinMagnitude(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 MinMagnitudeNumber(BrainFloatingPoint16 x, BrainFloatingPoint16 y)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BrainFloatingPoint16 result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(BrainFloatingPoint16 other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator +(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator +(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator -(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator -(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator ++(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator --(BrainFloatingPoint16 value)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator *(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }

        public static BrainFloatingPoint16 operator /(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(BrainFloatingPoint16 left, BrainFloatingPoint16 right)
        {
            throw new NotImplementedException();
        }
        #endregion メソッド

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
            return Bits.ToString();
        }
        #endregion override Object
    }
}