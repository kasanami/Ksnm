using Ksnm.Units.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Units.NonSI
{
    /// <summary>
    /// 度
    /// <para>記号:°</para>
    /// <para>系  :非SI単位</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :平面角</para>
    /// <para>定義:円周を360等分した弧の中心に対する角度</para>
    /// </summary>
    public class Degree<T> : PlaneAngle<T> where T : INumber<T>, IFloatingPointConstants<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "degree";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "°";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Degree() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Degree(T value) : base(value) { }
        #endregion コンストラクタ
        #region 演算子
        public static Degree<T> operator *(T value, Degree<T> quantity) => new(value * quantity.Value);
        public static Degree<T> operator *(Degree<T> quantity, T value) => new(quantity.Value * value);
        #endregion 演算子
        #region 型変換
        public static explicit operator Degree<T>(SI.Radian<T> radian) => new(radian.Value * _180 / T.Pi);
        public static explicit operator SI.Radian<T>(Degree<T> degree) => new(degree.Value * T.Pi / _180);
        #endregion 型変換
    }
}