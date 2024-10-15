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
    /// メートル毎秒
    /// <para>記号:m/s</para>
    /// <para>系  :国際単位系（組立単位）</para>
    /// <para>量  :速さ</para>
    /// <para>定義:1秒間に 1m 進む速さ</para>
    /// </summary>
    public class MetrePerSecond<T> : Velocity<T> where T : INumber<T>
    {
        #region 定数
        protected static readonly T _299792458 = T.CreateChecked(299792458);
        /// <summary>
        /// 光速
        /// </summary>
        public static readonly MetrePerSecond<T> SpeedOfLight = (MetrePerSecond<T>)_299792458;
        #endregion 定数

        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "metre per second";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "m/s";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public MetrePerSecond() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public MetrePerSecond(T value) : base(value) { }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public MetrePerSecond(Metre<T> length, Second<T> time) : base(length, time) { }
        /// <summary>
        /// 別の速度から初期化
        /// </summary>
        public MetrePerSecond(KiloMetrePerHour<T> velocity) : this(velocity.Value / T.CreateChecked(3.6m)) { }
        /// <summary>
        /// 別の速度から初期化
        /// </summary>
        public MetrePerSecond(NonSI.Knot<T> velocity) :
            this(velocity.Value * T.CreateChecked(1852) / T.CreateChecked(3600))
        {
        }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 速度と時間から加速度を計算する
        /// </summary>
        public static MetrePerSecondSquared<T> operator /(MetrePerSecond<T> velocity, Second<T> time)
        {
            return new MetrePerSecondSquared<T>(velocity, time);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static MetrePerSecond<T> operator *(T value, MetrePerSecond<T> quantity)
        {
            return new MetrePerSecond<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static MetrePerSecond<T> operator *(MetrePerSecond<T> quantity, T value)
        {
            return new MetrePerSecond<T>(quantity.Value * value);
        }
        #endregion 演算子

        #region 型変換
        public static explicit operator MetrePerSecond<T>(T velocity) => new MetrePerSecond<T>(velocity);
        public static explicit operator MetrePerSecond<T>(KiloMetrePerHour<T> velocity) => new MetrePerSecond<T>(velocity);
        public static explicit operator MetrePerSecond<T>(NonSI.Knot<T> velocity) => new MetrePerSecond<T>(velocity);
        /// <summary>
        /// SI単位に変換する
        /// </summary>
        public override MetrePerSecond<T> SI
        {
            get => this;
            set => Value = value.Value;
        }
        #endregion 型変換
    }
}