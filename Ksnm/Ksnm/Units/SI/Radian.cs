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
    /// ラジアン
    /// <para>記号:rad</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :平面角</para>
    /// <para>組立:m/m</para>
    /// </summary>
    public class Radian<T> : PlaneAngle<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "radian";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "rad";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Radian()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Radian(T value) : base(value) { }
        /// <summary>
        /// 半径と円弧から角度を計算する
        /// </summary>
        /// <param name="radius">半径</param>
        /// <param name="arc">円弧</param>
        public Radian(Metre<T> radius, Metre<T> arc)
        {
            Value = arc.Value / radius.Value;
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 半径と角度から円弧を計算する
        /// </summary>
        public static Metre<T> operator *(Metre<T> radius, Radian<T> radian)
        {
            return new Metre<T>(radius.Value * radian.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Radian<T> operator *(T value, Radian<T> quantity)
        {
            return new Radian<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Radian<T> operator *(Radian<T> quantity, T value)
        {
            return new Radian<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}