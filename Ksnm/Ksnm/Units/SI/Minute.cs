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
    /// 分
    /// <para>記号:min</para>
    /// <para>種類:SI併用単位</para>
    /// <para>量  :時間</para>
    /// </summary>
    public class Minute<T> : Time<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "minute";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "min";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Minute()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Minute(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// 秒→分
        /// </summary>
        public Minute(Second<T> time)
        {
            Value = time.Value / T.CreateChecked(60);
        }
        /// <summary>
        /// 指定した値で初期化
        /// 時→分
        /// </summary>
        public Minute(Hour<T> time)
        {
            Value = time.Value * T.CreateChecked(60);
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Minute<T> operator *(T value, Minute<T> quantity)
        {
            return new Minute<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Minute<T> operator *(Minute<T> quantity, T value)
        {
            return new Minute<T>(quantity.Value * value);
        }
        #endregion 演算子
        #region 型変換
        public static explicit operator Minute<T>(Second<T> time) => new Minute<T>(time);
        public static explicit operator Minute<T>(Hour<T> time) => new Minute<T>(time);
        #endregion 型変換
    }
}