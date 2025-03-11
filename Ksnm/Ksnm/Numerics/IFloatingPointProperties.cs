using System.Numerics;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 浮動小数点数型のプロパティ
    /// </summary>
    /// <typeparam name="TBits">浮動小数点数型と同じサイズの整数型</typeparam>
    public interface IFloatingPointProperties<TBits> where TBits : INumber<TBits>
    {
        TBits Bits { get; set; }

        #region 符号部
        /// <summary>
        /// 符号ビットを取得/設定
        /// </summary>
        TBits SignBit { get; set; }
        /// <summary>
        /// 符号を取得/設定
        /// </summary>
        int Sign { get; set; }
        #endregion 符号

        #region 指数部
        /// <summary>
        /// 指数部を取得/設定
        /// </summary>
        TBits ExponentBits { get; set; }
        /// <summary>
        /// 指数を取得/設定
        /// 2のべき乗の指数（2^Exponent）
        /// </summary>
        int Exponent { get; set; }
        /// <summary>
        /// 倍率
        /// Coefficientと乗算すると元の値になる係数
        /// </summary>
        double Scale { get; }
        #endregion 指数部

        #region 仮数部
        /// <summary>
        /// 仮数部を取得/設定
        /// </summary>
        TBits MantissaBits { get; set; }
        /// <summary>
        /// 仮数を取得/設定
        /// 0～1.0の値
        /// ※1.0=0b1000_0000
        /// ※0が出力されるのは、符号ビット以外が0のとき
        /// </summary>
        TBits Mantissa { get; set; }
        /// <summary>
        /// 仮数部のビット数を取得
        /// </summary>
        int MantissaLength { get; }
        /// <summary>
        /// 係数
        /// Mantissaを実数に変換した値
        /// Scaleと乗算すると元の値になる係数
        /// </summary>
        double Coefficient { get; }
        #endregion 仮数部
    }
}