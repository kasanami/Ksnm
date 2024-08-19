using System.Runtime.InteropServices;

namespace Ksnm.Numerics
{
    using UInt = System.UInt64;
    /// <summary>
    /// 64bit浮動小数点数型
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ExtendedDouble
    {
        #region 定数
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
        public const UInt SignBitMask = 1ul;
        /// <summary>
        /// 符号部のビットマスク(実際のビット位置)
        /// </summary>
        public const UInt SignShiftedBitMask = SignBitMask << SignBitShift;

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
        public const UInt ExponentBitMask = ((UInt)1 << ExponentLength) - 1;
        /// <summary>
        /// 指数部のビットマスク(実際のビット位置)
        /// </summary>
        public const UInt ExponentShiftedBitMask = ExponentBitMask << ExponentBitShift;
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
        public const UInt MantissaBitMask = ((UInt)1 << MantissaLength) - 1;
        #endregion 定数

        #region フィールド
        [FieldOffset(0)]
        public Double Value;
        [FieldOffset(0)]
        public UInt Bits;
        #endregion フィールド

        #region プロパティ
        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        public byte SignBit
        {
            get => (byte)(Bits >> (ExponentLength + MantissaLength));
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
        public ushort ExponentBits
        {
            get => (ushort)((Bits >> ExponentBitShift) & ExponentBitMask);
            set => Bits = (Bits & ~ExponentShiftedBitMask) | ((value & ExponentBitMask) << ExponentBitShift);
        }
        /// <summary>
        /// 指数を取得/設定
        /// </summary>
        public int Exponent
        {
            get => ExponentBits - ExponentBias;
            set => ExponentBits = (ushort)(value + ExponentBias);
        }

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
        public UInt Mantissa
        {
            get => MantissaBits | ((UInt)1 << MantissaLength);// ((UInt)1 << MantissaLength)は"1."を意味する
            set => Bits = value;
        }
        #endregion プロパティ
    }
}