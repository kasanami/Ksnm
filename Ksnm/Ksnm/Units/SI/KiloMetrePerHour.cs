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

namespace Ksnm.Units.SI
{
    /// <summary>
    /// キロメートル毎時
    /// <para>記号:km/h</para>
    /// <para>系  :SI併用単位</para>
    /// <para>量  :速度</para>
    /// <para>定義:1時間に1 km 進むの速さ</para>
    /// </summary>
    public class KiloMetrePerHour<T> : Velocity<T> where T : IMath<T>
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
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KiloMetrePerHour(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KiloMetrePerHour(decimal value) : base(value) { }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public KiloMetrePerHour(Metre<T> length, Second<T> time) : base(length, time) { }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public KiloMetrePerHour(Metre<T> length, Hour<T> time) : this(length, (Second<T>)time) { }
        /// <summary>
        /// 別の速度から初期化
        /// </summary>
        public KiloMetrePerHour(MetrePerSecond<T> velocity) : this(velocity.Value.Multiply(3.6m)) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static KiloMetrePerHour<T> operator *(int value, KiloMetrePerHour<T> quantity)
        {
            return new KiloMetrePerHour<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static KiloMetrePerHour<T> operator *(decimal value, KiloMetrePerHour<T> quantity)
        {
            return new KiloMetrePerHour<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator KiloMetrePerHour<T>(MetrePerSecond<T> velocity)
        {
            return new KiloMetrePerHour<T>(velocity);
        }
        #endregion 型変換
    }
}