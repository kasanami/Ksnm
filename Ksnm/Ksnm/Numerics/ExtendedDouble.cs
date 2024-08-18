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
        /// 指数部のビット数
        /// </summary>
        public const int ExponentLength = 11;
        /// <summary>
        /// 指数部のビットマスク
        /// </summary>
        public const UInt ExponentBitMask = ((UInt)1 << ExponentLength) - 1;
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
        /// 符号ビットを取得
        /// </summary>
        public byte SignBit => (byte)(Bits >> (ExponentLength + MantissaLength));
        /// <summary>
        /// 符号を取得
        /// </summary>
        public int Sign => SignBit == 1 ? -1 : +1;

        /// <summary>
        /// 指数部を取得
        /// </summary>
        public ushort ExponentBits => (ushort)((Bits >> MantissaLength) & ExponentBitMask);
        /// <summary>
        /// 指数を取得
        /// </summary>
        public int Exponent => ExponentBits - ExponentBias;

        /// <summary>
        /// 仮数部を取得
        /// </summary>
        public UInt MantissaBits => Bits & MantissaBitMask;
        /// <summary>
        /// 仮数を取得
        /// </summary>
        public UInt Mantissa => MantissaBits | ((UInt)1 << MantissaLength);// ((UInt)1 << MantissaLength)は"1."を意味する
        #endregion プロパティ
    }
}