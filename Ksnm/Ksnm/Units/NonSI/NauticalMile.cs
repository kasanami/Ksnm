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

namespace Ksnm.Units.NonSI
{
    /// <summary>
    /// 海里
    /// <para>記号:M, nm</para>
    /// <para>系  :非SI単位</para>
    /// <para>量  :長さ</para>
    /// <para>定義:正確に1852 m（国際海里）</para>
    /// <para>由来:地球の大円上における弧1分の長さ</para>
    /// </summary>
    public class NauticalMile<T> : Length<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "nautical mile";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "nm";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public NauticalMile() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public NauticalMile(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public NauticalMile(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public NauticalMile(decimal value) : base(value) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static NauticalMile<T> operator *(T value, NauticalMile<T> quantity)
        {
            return new NauticalMile<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static NauticalMile<T> operator *(NauticalMile<T> quantity, T value)
        {
            return new NauticalMile<T>(quantity.Value * value);
        }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public static Knot<T> operator /(NauticalMile<T> length, SI.Second<T> time)
        {
            return new Knot<T>(length, time);
        }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public static Knot<T> operator /(NauticalMile<T> length, SI.Hour<T> time)
        {
            return new Knot<T>(length, time);
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator SI.Metre<T>(NauticalMile<T> nauticalMile)
        {
            var _1852 = T.CreateChecked(1852);
            return new SI.Metre<T>(nauticalMile.Value * _1852);
        }
        #endregion 型変換
    }
}