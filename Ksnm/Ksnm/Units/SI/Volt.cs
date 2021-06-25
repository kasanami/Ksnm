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
    /// ボルト
    /// <para>記号:V</para>
    /// <para>系  :国際単位系 (SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :電圧・起電力</para>
    /// <para>定義:1Aの電流が流れる導体の2点間において消費される電力が1Wであるときの、その2点間の電圧</para>
    /// </summary>
    public class Volt<T> : ElectricPotential<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "volt";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "V";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Volt()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Volt(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Volt(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Volt(decimal value) : base(value) { }
        /// <summary>
        /// エネルギーと電荷から電位差を計算する
        /// </summary>
        public Volt(Joule<T> energy, Coulomb<T> charge) : base(energy, charge)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Volt<T> operator *(int value, Volt<T> quantity)
        {
            return new Volt<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Volt<T> operator *(decimal value, Volt<T> quantity)
        {
            return new Volt<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
    }
}