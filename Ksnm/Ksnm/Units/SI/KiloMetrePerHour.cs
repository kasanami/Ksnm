/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using Ksnm.Numerics;
using System.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// キロメートル毎時
    /// <para>記号:km/h</para>
    /// <para>系  :SI併用単位</para>
    /// <para>量  :速度</para>
    /// <para>定義:1時間に1 km 進む速さ</para>
    /// </summary>
    public class KiloMetrePerHour<T> : Velocity<T> where T : INumber<T>
    {
        #region 定数
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilometre per hour";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "km/h";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public KiloMetrePerHour() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KiloMetrePerHour(T value) : base(value) { }
        public KiloMetrePerHour(KiloMetre<T> length, Hour<T> time) : base(length, time) { }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 速度と時間から加速度を計算する
        /// </summary>
        public static KiloMetrePerHourPerSecond<T> operator /(KiloMetrePerHour<T> velocity, Second<T> time) => new KiloMetrePerHourPerSecond<T>(velocity, time);
        public static KiloMetrePerHour<T> operator *(T scale, KiloMetrePerHour<T> velocity) => new KiloMetrePerHour<T>(scale * velocity.Value);
        public static KiloMetrePerHour<T> operator *(KiloMetrePerHour<T> velocity, T scale) => new KiloMetrePerHour<T>(velocity.Value * scale);
        #endregion 演算子

        #region 型変換
        public static explicit operator KiloMetrePerHour<T>(MetrePerSecond<T> velocity) => new KiloMetrePerHour<T>(velocity.Value * _3600 / _1000);
        public static explicit operator KiloMetrePerHour<T>(NonSI.Knot<T> velocity) => new KiloMetrePerHour<T>(velocity.Value * _1852 / _1000);
        /// <summary>
        /// SI単位に変換する
        /// </summary>
        public override MetrePerSecond<T> SI
        {
            get => (MetrePerSecond<T>)(Value * _1000 / _3600);
            set => Value = (value.Value * _3600 / _1000);
        }
        #endregion 型変換
    }
}