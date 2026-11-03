using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics;

/// <summary>
/// 128ビット固定小数点数
/// 111～111.1111～111
/// <--16-->.<--112-->
/// 
/// ・現在は主にデバッグ用
/// </summary>
readonly struct Fixed128Q112
{
    /// <summary>
    /// 小数部のビット数(128 - 16)
    /// </summary>
    public const int FractionLength = 112;
    public static readonly Fixed128Q112 One = new(1, 0);
    public static readonly Fixed128Q112 Zero = new();
    public UInt128 Bits { get; }

    #region コンストラクタ
    /// <summary>
    /// 0で初期化
    /// </summary>
    public Fixed128Q112()
    {
        Bits = UInt128.Zero;
    }
    /// <summary>
    /// ビットを指定
    /// </summary>
    /// <param name="bits"></param>
    public Fixed128Q112(UInt128 bits)
    {
        Bits = bits;
    }
    /// <summary>
    /// 整数部と小数部を個別に指定
    /// </summary>
    /// <param name="integer">整数部</param>
    /// <param name="fraction">小数部</param>
    public Fixed128Q112(UInt128 integer, UInt128 fraction)
    {
        Bits = integer << FractionLength;
        Bits |= fraction;
    }
    #endregion コンストラクタ

    #region 型変換
    public static implicit operator Fixed128Q112(UInt16 value)
    {
        return new Fixed128Q112(value, UInt128.Zero);
    }
    public static explicit operator Fixed128Q112(UInt128 value)
    {
        return new Fixed128Q112(value, UInt128.Zero);
    }
    public static explicit operator double(Fixed128Q112 value)
    {
        return (double)value.Bits / (double)One.Bits;
    }
    #endregion 型変換

    #region
    public override string ToString()
    {
        return ((double)this).ToString();
    }
    #endregion
}
