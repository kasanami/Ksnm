using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
    using BaseType = System.Double;
    using BitsType = System.UInt64;
    /// <summary>
    /// 64bit浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedDouble : IFloatingPointProperties<BitsType>
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
        public const int SignShift = 63;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignMask = 1ul;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType SignShiftedMask = SignMask << SignShift;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 11;
        /// <summary>
        /// 指数部のビットシフト数
        /// </summary>
        public const int ExponentShift = 52;
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
        public const int ExponentBias = 1023;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const BitsType InfinityExponentBits = 0b111_1111_1111;
        public const int InfinityExponent = 1024;

        public const int MinExponent = 1 - ExponentBias;
        public const int MaxExponent = ExponentBias;

        public const BitsType MinExponentBits = 0x00;
        public const BitsType MaxExponentBits = 0x3FF;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 52;
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
        public UInt32 UpperBits;
        [FieldOffset(4)]
        public UInt32 LowerBits;
#else
        [FieldOffset(0)]
        public UInt32 LowerBits;
        [FieldOffset(4)]
        public UInt32 UpperBits;
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
            set => Bits = (BitsType)((Bits & ~SignShiftedMask) | ((value & 1ul) << SignShift));
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
            get => Bits & MantissaMask;
            set => Bits = (Bits & ~MantissaMask) | (value & MantissaMask);
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
                return MantissaBits | ((BitsType)1 << MantissaLength);
            }
            set => MantissaBits = value;
        }
        int IFloatingPointProperties<BitsType>.MantissaLength => MantissaLength;
        public double Coefficient => Mantissa / (double)(1ul << MantissaLength);
        bool IFloatingPointProperties<BitsType>.IsPositive => BaseType.IsPositive(Value);
        bool IFloatingPointProperties<BitsType>.IsNegative => BaseType.IsNegative(Value);
        bool IFloatingPointProperties<BitsType>.IsZero => Value == 0;
        public bool IsZero => Value == 0;
        bool IFloatingPointProperties<BitsType>.IsInfinity => BaseType.IsInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsPositiveInfinity => BaseType.IsPositiveInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsNegativeInfinity => BaseType.IsNegativeInfinity(Value);
        bool IFloatingPointProperties<BitsType>.IsNaN => BaseType.IsNaN(Value);
        #endregion プロパティ

        #region コンストラクタ
        public ExtendedDouble()
        {
        }
        public ExtendedDouble(BaseType value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator ExtendedDouble(BaseType value)
        {
            return new ExtendedDouble(value);
        }
        public static implicit operator BaseType(ExtendedDouble value)
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