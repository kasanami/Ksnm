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

namespace Ksnm.Units.SI
{
    /// <summary>
    /// ジュール
    /// </summary>
    public class Joule<T> : Energy<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "joule";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "J";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Joule()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Joule(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Joule(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Joule(decimal value) : base(value) { }
        /// <summary>
        /// 力と距離からエネルギーを計算する
        /// </summary>
        public Joule(Newton<T> force, Metre<T> length) : base(force, length)
        {
        }
        /// <summary>
        /// 質量と光速からエネルギーを計算する
        /// </summary>
        public Joule(Kilogram<T> mass) : base(mass, MetrePerSecond<T>.SpeedOfLight)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// エネルギーと時間から仕事率を計算する
        /// </summary>
        public static Watt<T> operator /(Joule<T> energy, Second<T> time)
        {
            return new Watt<T>(energy, time);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Joule<T> operator *(int value, Joule<T> quantity)
        {
            return new Joule<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Joule<T> operator *(decimal value, Joule<T> quantity)
        {
            return new Joule<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Joule<T>(Kilogram<T> mass)
        {
            return new Joule<T>(mass);
        }
        #endregion 型変換
    }
}