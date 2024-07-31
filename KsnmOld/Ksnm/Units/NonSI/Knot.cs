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

namespace Ksnm.Units.NonSI
{
    /// <summary>
    /// ノット
    /// <para>記号:kt</para>
    /// <para>系  :SIに属さない単位</para>
    /// <para>量  :速さ</para>
    /// <para>SI  :約 0.514 444 m/s</para>
    /// <para>定義:(1852/3600)m/s = 1時間に1海里進む速さ</para>
    /// </summary>
    public class Knot<T> : Velocity<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "knot";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "kt";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Knot() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Knot(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Knot(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Knot(decimal value) : base(value) { }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public Knot(NauticalMile<T> length, SI.Second<T> time) : base(length, (SI.Hour<T>)time) { }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public Knot(NauticalMile<T> length, SI.Hour<T> time) : base(length, time) { }
        /// <summary>
        /// 別の速度から初期化
        /// </summary>
        public Knot(SI.MetrePerSecond<T> velocity) : this((SI.KiloMetrePerHour<T>)velocity) { }
        /// <summary>
        /// 別の速度から初期化
        /// </summary>
        public Knot(SI.KiloMetrePerHour<T> velocity) : this(velocity.Value.Divide(1.852m)) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Knot<T> operator *(int value, Knot<T> quantity)
        {
            return new Knot<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Knot<T> operator *(decimal value, Knot<T> quantity)
        {
            return new Knot<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator Knot<T>(SI.MetrePerSecond<T> velocity)
        {
            return new Knot<T>(velocity);
        }
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator Knot<T>(SI.KiloMetrePerHour<T> velocity)
        {
            return new Knot<T>(velocity);
        }
        #endregion 型変換
    }
}