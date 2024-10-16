using Ksnm.Numerics;
using System.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// キロメートル
    /// <para>記号:km</para>
    /// <para>系  :国際単位系</para>
    /// <para>種類:倍量単位</para>
    /// <para>量  :長さ</para>
    /// <para>定義:1000 m</para>
    /// </summary>
    public class KiloMetre<T> : Length<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilometre";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "km";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public KiloMetre()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KiloMetre(T value) : base(value) { }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 2つの長さから面積を計算する
        /// </summary>
        public static SquareMetre<T> operator *(KiloMetre<T> length1, KiloMetre<T> length2) => new SquareMetre<T>(length1, length2);
        public static KiloMetre<T> operator *(T scale, KiloMetre<T> length) => new KiloMetre<T>(scale * length.Value);
        public static KiloMetre<T> operator *(KiloMetre<T> length, T scale) => new KiloMetre<T>(length.Value * scale);
        public static KiloMetrePerHour<T> operator /(KiloMetre<T> length, Hour<T> time) => new KiloMetrePerHour<T>(length, time);
        #endregion 演算子

        #region 型変換
        public static explicit operator KiloMetre<T>(T value) => new KiloMetre<T>(value);
        public static explicit operator KiloMetre<T>(Metre<T> length) => new KiloMetre<T>(length.Value / _1000);
        /// <summary>
        /// SI単位に変換する
        /// </summary>
        public virtual Metre<T> SI
        {
            get => (Metre<T>)this;
            set => Value = value.Value / _1000;
        }
        #endregion 型変換
    }
}