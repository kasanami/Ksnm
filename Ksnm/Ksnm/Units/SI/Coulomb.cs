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
    /// クーロン
    /// <para>記号:C</para>
    /// <para>系  :国際単位系 (SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :電荷</para>
    /// <para>定義:e / 1.602176634 * 10^−19</para>
    /// </summary>
    public class Coulomb<T> : ElectricCharge<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "coulomb";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "C";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Coulomb() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Coulomb(T value) : base(value) { }
        /// <summary>
        /// 電流と時間から電荷を計算する
        /// </summary>
        public Coulomb(Ampere<T> current, Second<T> time) : base(current, time)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Coulomb<T> operator *(T value, Coulomb<T> quantity)
        {
            return new Coulomb<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Coulomb<T> operator *(Coulomb<T> quantity, T value)
        {
            return new Coulomb<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}