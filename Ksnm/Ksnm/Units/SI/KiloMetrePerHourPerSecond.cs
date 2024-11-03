using System.Numerics;
using System.Security.Cryptography;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// キロメートル毎時毎秒
    /// </summary>
    public class KiloMetrePerHourPerSecond<T> : Acceleration<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilometre per hour per second";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "km/h/s";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public KiloMetrePerHourPerSecond() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KiloMetrePerHourPerSecond(T value) : base(value) { }
        /// <summary>
        /// 速度と時間から加速度を計算する
        /// </summary>
        public KiloMetrePerHourPerSecond(KiloMetrePerHour<T> velocity, Second<T> time) : this(velocity.Value / time.Value) { }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 加速度と時間から速度を計算する
        /// </summary>
        public static KiloMetrePerHour<T> operator *(KiloMetrePerHourPerSecond<T> velocity, Second<T> time)
        {
            return new KiloMetrePerHour<T>(velocity.Value * time.Value);
        }
        public static KiloMetrePerHourPerSecond<T> operator *(T value, KiloMetrePerHourPerSecond<T> quantity) => new KiloMetrePerHourPerSecond<T>(value * quantity.Value);
        public static KiloMetrePerHourPerSecond<T> operator *(KiloMetrePerHourPerSecond<T> quantity, T value) => new KiloMetrePerHourPerSecond<T>(quantity.Value * value);
        #endregion 演算子

        #region 型変換
        public static explicit operator KiloMetrePerHourPerSecond<T>(T value) => new KiloMetrePerHourPerSecond<T>(value);
        public static explicit operator KiloMetrePerHourPerSecond<T>(MetrePerSecondSquared<T> acceleration) => (KiloMetrePerHourPerSecond<T>)(acceleration.Value * _5 / _18);
        /// <summary>
        /// SI単位に変換する
        /// </summary>
        public override MetrePerSecondSquared<T> SI
        {
            get => (MetrePerSecondSquared<T>)(Value * _1000 / _3600);
            set => Value = value.Value * _3600 / _1000;
        }
        #endregion 型変換
    }
}