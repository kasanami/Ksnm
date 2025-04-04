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
    /// メートル
    /// <para>記号:m</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:基本単位</para>
    /// <para>量  :長さ</para>
    /// </summary>
    public class Metre<T> : Length<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "metre";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "m";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Metre()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Metre(T value) : base(value) { }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 2つの長さから面積を計算する
        /// </summary>
        public static SquareMetre<T> operator *(Metre<T> length1, Metre<T> length2) => new SquareMetre<T>(length1, length2);
        public static Metre<T> operator *(T scale, Metre<T> length) => new Metre<T>(scale * length.Value);
        public static Metre<T> operator *(Metre<T> length, T scale) => new Metre<T>(length.Value * scale);
        public static MetrePerSecond<T> operator /(Metre<T> length, Second<T> time) => new MetrePerSecond<T>(length, time);
        #endregion 演算子

        #region 型変換
        public static explicit operator Metre<T>(T value) => new Metre<T>(value);
        public static explicit operator Metre<T>(KiloMetre<T> length) => new Metre<T>(length.Value * _1000);
        /// <summary>
        /// SI単位に変換する
        /// </summary>
        public override Metre<T> SI
        {
            get => this;
            set => Value = value.Value;
        }
        #endregion 型変換
    }
}