﻿/*
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
    /// カンデラ
    /// <para>記号:cd</para>
    /// <para>系  :国際単位系 (SI)</para>
    /// <para>種類:基本単位</para>
    /// <para>量  :光度</para>
    /// <para>定義:放射強度683分の1ワット毎ステラジアンで540テラヘルツの単色光を放射する光源のその放射の方向における光度</para>
    /// </summary>
    public class Candela<T> : LuminousIntensity<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "candela";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "cd";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Candela() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Candela(T value) : base(value) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Candela<T> operator *(T value, Candela<T> quantity)
        {
            return new Candela<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Candela<T> operator *(Candela<T> quantity, T value)
        {
            return new Candela<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}