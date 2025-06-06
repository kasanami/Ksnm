﻿/*
The zlib License

Copyright (c) 2024 Takahiro Kasanami

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
    /// オーム
    /// <para>記号:Ω</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :電気抵抗</para>
    /// </summary>
    public class Ohm<T> : ElectricalResistance<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "ohm";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "Ω";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Ohm() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Ohm(T value) : base(value) { }
        #endregion コンストラクタ
        #region 演算子
        public static Ohm<T> operator *(T value, Ohm<T> quantity) => new(value * quantity.Value);
        public static Ohm<T> operator *(Ohm<T> quantity, T value) => new(quantity.Value * value);
        public static Volt<T> operator *(Ohm<T> ohm, Ampere<T> ampere) => new(ohm.Value * ampere.Value);
        #endregion 演算子
    }
}