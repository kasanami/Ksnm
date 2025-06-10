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
    /// カロリー
    /// <para>記号:cal</para>
    /// <para>系  :CGS単位系</para>
    /// <para>量  :熱量</para>
    /// <para>SI  :4.184 J</para>
    /// <para>由来:1gの水の温度を標準大気圧下で1℃上げる熱量</para>
    /// </summary>
    public class Calorie<T> : Energy<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "calorie";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "cal";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Calorie() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Calorie(T value) : base(value) { }
        /// <summary>
        /// 別のエネルギーから初期化
        /// </summary>
        public Calorie(SI.Joule<T> energy) : this(energy.Value / _4_184) { }
        #endregion コンストラクタ
        #region 演算子
        public static Calorie<T> operator *(T value, Calorie<T> quantity) => new(value * quantity.Value);
        public static Calorie<T> operator *(Calorie<T> quantity, T value) => new(quantity.Value * value);
        #endregion 演算子
        #region 型変換
        public static explicit operator Calorie<T>(SI.Joule<T> energy) => new(energy);
        #endregion 型変換
    }
}