﻿using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
    using BaseType = System.Half;
    using BitsType = System.UInt16;
    /// <summary>
    /// 16bit浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedHalf : IFloatingPointProperties<BitsType>
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
        public const int SignShift = 15;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignMask = 1;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType SignShiftedMask = SignMask << SignShift;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 5;
        /// <summary>
        /// 指数部のビットシフト数
        /// </summary>
        public const int ExponentShift = 10;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const BitsType ExponentMask = ((BitsType)1 << ExponentLength) - 1;
        /// <summary>
        /// 指数部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType ExponentShiftedBitMask = ExponentMask << ExponentShift;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 15;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const BitsType InfinityExponentBits = 0b1_1111;
        public const int InfinityExponent = 16;

        public const int MinExponent = 1 - ExponentBias;
        public const int MaxExponent = ExponentBias;

        public const BitsType MinExponentBits = 0x00;
        public const BitsType MaxExponentBits = 0x1F;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 10;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const BitsType MantissaMask = ((BitsType)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド
        [FieldOffset(0)]
        public BaseType Value;
        [FieldOffset(0)]
        private BitsType bits = 0;
#if BIGENDIAN
        [FieldOffset(0)]
        public byte UpperBits;
        [FieldOffset(2)]
        public byte LowerBits;
#else
        [FieldOffset(0)]
        public byte LowerBits;
        [FieldOffset(2)]
        public byte UpperBits;
#endif
        #endregion フィールド

        #region プロパティ
        public BitsType Bits
        {
            get => bits;
            set => bits = value;
        }

        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public BitsType SignBit
        {
            get => (BitsType)(Bits >> SignShift);
            set => Bits = (BitsType)((Bits & ~SignShiftedMask) | ((value & 1) << SignShift));
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
            get => (BitsType)((Bits >> ExponentShift) & ExponentMask);
            set => Bits = (BitsType)((Bits & ~ExponentShiftedBitMask) | ((value & ExponentMask) << ExponentShift));
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
        /// 倍率
        /// Coefficientと乗算すると元の値になる係数
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

        /// <summary>
        /// 仮数部を取得/設定
        /// </summary>
        public BitsType MantissaBits
        {
            get => (BitsType)(Bits & MantissaMask);
            set => Bits = (BitsType)((Bits & ~MantissaMask) | (value & MantissaMask));
        }
        /// <summary>
        /// 仮数を取得/設定
        /// ※実数で例えると0～2.0の値
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
            set => MantissaBits = value;
        }
        int IFloatingPointProperties<BitsType>.MantissaLength => MantissaLength;
        public double Coefficient => Mantissa / (double)(1u << MantissaLength);
        bool IFloatingPointProperties<BitsType>.IsPositive => BaseType.IsPositive(Value);
        bool IFloatingPointProperties<BitsType>.IsNegative => BaseType.IsNegative(Value);
        bool IFloatingPointProperties<BitsType>.IsZero => Value == BaseType.Zero;
        public bool IsZero => Value == BaseType.Zero;
        bool IFloatingPointProperties<BitsType>.IsInfinity => BaseType.IsInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsPositiveInfinity => BaseType.IsPositiveInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsNegativeInfinity => BaseType.IsNegativeInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsNaN => BaseType.IsNaN(Value);
        #endregion プロパティ

        #region コンストラクタ
        public ExtendedHalf()
        {
        }
        public ExtendedHalf(BaseType value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator ExtendedHalf(BaseType value)
        {
            return new ExtendedHalf(value);
        }
        public static implicit operator BaseType(ExtendedHalf value)
        {
            return value.Value;
        }
        #endregion 型変更

        #region override object
        public override string ToString()
        {
            return Value.ToString();
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        #endregion override object
    }
}