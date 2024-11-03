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
    /// ケルビン
    /// <para>記号:K</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:基本単位</para>
    /// <para>量  :熱力学温度</para>
    /// </summary>
    public class Kelvin<T> : Temperature<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kelvin";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "K";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Kelvin()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Kelvin(T value) : base(value) { }
        /// <summary>
        /// セルシウス度をケルビンに設定する。
        /// </summary>
        public Kelvin(DegreeCelsius<T> degreeCelsius)
        {
            Value = degreeCelsius.Value + T.CreateChecked(273.15m);
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Kelvin<T> operator *(T value, Kelvin<T> quantity)
        {
            return new Kelvin<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Kelvin<T> operator *(Kelvin<T> quantity, T value)
        {
            return new Kelvin<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}