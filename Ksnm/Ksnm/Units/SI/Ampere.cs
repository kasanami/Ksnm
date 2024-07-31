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
    /// アンペア
    /// <para>記号:A</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:基本単位</para>
    /// <para>量  :電流</para>
    /// </summary>
    public class Ampere<T> : ElectricCurrent<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "ampere";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "A";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Ampere() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Ampere(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Ampere(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Ampere(decimal value) : base(value) { }
        /// <summary>
        /// 電荷と時間から電流を計算する
        /// </summary>
        public Ampere(Coulomb<T> charge, Second<T> time) : base(charge, time)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Ampere<T> operator *(T value, Ampere<T> quantity)
        {
            return new Ampere<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Ampere<T> operator *(Ampere<T> quantity, T value)
        {
            return new Ampere<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}