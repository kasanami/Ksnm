using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
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
        public const int SignBitShift = 63;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignBitMask = 1ul;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType SignShiftedBitMask = SignBitMask << SignBitShift;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 11;
        /// <summary>
        /// 指数部のビットシフト数
        /// </summary>
        public const int ExponentBitShift = 52;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const BitsType ExponentBitMask = ((BitsType)1 << ExponentLength) - 1;
        /// <summary>
        /// 指数部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType ExponentShiftedBitMask = ExponentBitMask << ExponentBitShift;
        /// <summary>
        /// 指数部バイアス
        /// </summary>
        public const int ExponentBias = 1023;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const int InfinityExponent = 1024;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 52;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const BitsType MantissaBitMask = ((BitsType)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド
        [FieldOffset(0)]
        public Double Value;
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
        public BitsType Bits { get => bits; set => bits = value; }

        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public BitsType SignBit
        {
            get => (BitsType)(Bits >> SignBitShift);
            set => Bits = (Bits & ~SignShiftedBitMask) | ((value & 1ul) << SignBitShift);
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
            set => Bits = (Bits & ~ExponentShiftedBitMask) | ((value & ExponentBitMask) << ExponentBitShift);
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
                return (int)(ExponentBits - ExponentBias - MantissaLength);
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
            get => Bits & MantissaBitMask;
            set => Bits = (Bits & ~MantissaBitMask) | (value & MantissaBitMask);
        }
        /// <summary>
        /// 仮数を取得/設定
        /// 0～1.0の値
        /// ※1.0=0b100_0000000000_0000000000_0000000000_0000000000_0000000000
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
                return MantissaBits | ((BitsType)1 << MantissaLength);
            }
            set => Bits = value;
        }
        #endregion プロパティ

        #region コンストラクタ
        public ExtendedDouble()
        {
        }
        public ExtendedDouble(double value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator ExtendedDouble(double value)
        {
            return new ExtendedDouble(value);
        }
        /// </summary>
        public static implicit operator double(ExtendedDouble value)
        {
            return value.Value;
        }
        #endregion 型変更
    }
}