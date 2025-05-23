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
    /// ワット
    /// <para>記号:W</para>
    /// <para>系  :国際単位系 (SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :仕事率・工率・電力・放射束</para>
    /// <para>定義:1秒間に1ジュールの仕事率</para>
    /// </summary>
    public class Watt<T> : Power<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "watt";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "W";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Watt() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Watt(T value) : base(value) { }
        /// <summary>
        /// エネルギーと時間から速度を計算する
        /// </summary>
        public Watt(Joule<T> energy, Second<T> time) : base(energy, time)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        public static Watt<T> operator *(T value, Watt<T> quantity) => new(value * quantity.Value);
        public static Watt<T> operator *(Watt<T> quantity, T value) => new(quantity.Value * value);
        public static Ampere<T> operator /(Watt<T> power, Volt<T> volt) => new(power.Value / volt.Value);
        public static Volt<T> operator /(Watt<T> power, Ampere<T> ampere) => new(power.Value / ampere.Value);
        public static Joule<T> operator *(Watt<T> power, Second<T> second) => new(power.Value * second.Value);
        #endregion 演算子
    }
}