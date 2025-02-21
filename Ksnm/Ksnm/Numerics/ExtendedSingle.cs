using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
    using UInt = System.UInt32;
    /// <summary>
    /// 32bit浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedSingle
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
        public const UInt SignBitMask = 1;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const UInt SignShiftedBitMask = SignBitMask << SignBitShift;

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
        public const UInt ExponentBitMask = ((UInt)1 << ExponentLength) - 1;
        /// <summary>
        /// 指数部のビットマスク(実際のビット位置)
        /// </summary>
        public const UInt ExponentShiftedBitMask = ExponentBitMask << ExponentBitShift;
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
        public const UInt MantissaBitMask = ((UInt)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド
        [FieldOffset(0)]
        public Single Value;
        [FieldOffset(0)]
        public UInt Bits;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public byte SignBit
        {
            get => (byte)(Bits >> SignBitShift);
            set => Bits = (UInt)((Bits & ~SignShiftedBitMask) | ((value & 1ul) << SignBitShift));
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
            set => Bits = (UInt)((Bits & ~ExponentShiftedBitMask) | ((value & ExponentBitMask) << ExponentBitShift));
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
        public UInt MantissaBits
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
        public UInt Mantissa
        {
            get
            {
                // 符号ビット以外が0なら0を返す
                if ((Bits & ~SignShiftedBitMask) == 0)
                {
                    return 0;
                }
                // ((UInt)1 << MantissaLength)は"1."を意味する
                return MantissaBits | ((UInt)1 << MantissaLength);
            }
            set => Bits = value;
        }
        #endregion プロパティ

        #region コンストラクタ
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
        /// </summary>
        public static implicit operator Single(ExtendedSingle value)
        {
            return value.Value;
        }
        #endregion 型変更
    }
}