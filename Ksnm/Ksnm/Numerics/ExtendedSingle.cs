using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
    using BitsType = System.UInt32;
    /// <summary>
    /// 32bit浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedSingle : IFloatingPointProperties<BitsType>
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
        public const int SignBitShift = 31;
        /// <summary>
        /// 符号部のビットマスク
        /// </summary>
        public const BitsType SignBitMask = 1;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const BitsType SignShiftedBitMask = SignBitMask << SignBitShift;

        /// <summary>
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 8;
        /// <summary>
        /// 指数部のビットシフト数
        /// </summary>
        public const int ExponentBitShift = 23;
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
        public const int ExponentBias = 127;
        /// <summary>
        /// 無限大を表す指数部
        /// </summary>
        public const int InfinityExponent = 128;

        /// <summary>
        /// 仮数部のビット数
        /// </summary>
        public const int MantissaLength = 23;
        /// <summary>
        /// 仮数部のビットマスク
        /// </summary>
        public const BitsType MantissaBitMask = ((BitsType)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド
        [FieldOffset(0)]
        public Single Value;
        [FieldOffset(0)]
        private BitsType bits = 0;
#if BIGENDIAN
        [FieldOffset(0)]
        public UInt16 UpperBits;
        [FieldOffset(2)]
        public UInt16 LowerBits;
#else
        [FieldOffset(0)]
        public UInt16 LowerBits;
        [FieldOffset(2)]
        public UInt16 UpperBits;
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
            get => (BitsType)(Bits >> SignBitShift);
            set => Bits = (BitsType)((Bits & ~SignShiftedBitMask) | ((value & 1ul) << SignBitShift));
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
            get => (ushort)((Bits >> ExponentBitShift) & ExponentBitMask);
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
                // 符号ビット以外が0なら0を返す
                if ((Bits & ~SignShiftedBitMask) == 0)
                {
                    return 0;
                }
                return (int)ExponentBits - ExponentBias;
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
            get => Bits & MantissaBitMask;
            set => Bits = (Bits & ~MantissaBitMask) | (value & MantissaBitMask);
        }
        /// <summary>
        /// 仮数を取得/設定
        /// 0～1.0の値
        /// ※1.0=0b1000_0000000000_0000000000
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
        public ExtendedSingle()
        {
        }
        public ExtendedSingle(Single value)
        {
            Value = value;
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator ExtendedSingle(Single value)
        {
            return new ExtendedSingle(value);
        }
        public static implicit operator Single(ExtendedSingle value)
        {
            return value.Value;
        }
        #endregion 型変更
    }
}