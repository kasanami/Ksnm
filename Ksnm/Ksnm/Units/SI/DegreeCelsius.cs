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
    /// セルシウス度
    /// <para>記号:℃</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:固有の名称と記号を持つ SI 単位</para>
    /// <para>量  :温度</para>
    /// </summary>
    public class DegreeCelsius<T> : Temperature<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "degree Celsius";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "℃";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public DegreeCelsius()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public DegreeCelsius(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public DegreeCelsius(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public DegreeCelsius(decimal value) : base(value) { }
        /// <summary>
        /// ケルビンをセルシウス度に設定する。
        /// </summary>
        public DegreeCelsius(Kelvin<T> kelvin)
        {
            Value = kelvin.Value - T.CreateChecked(273.15m);
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static DegreeCelsius<T> operator *(T value, DegreeCelsius<T> quantity)
        {
            return new DegreeCelsius<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static DegreeCelsius<T> operator *(DegreeCelsius<T> quantity, T value)
        {
            return new DegreeCelsius<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}