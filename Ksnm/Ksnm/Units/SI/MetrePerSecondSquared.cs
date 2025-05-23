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
using System.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// メートル毎秒毎秒
    /// </summary>
    public class MetrePerSecondSquared<T> : Acceleration<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "metre per second squared";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "m/s^2";
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public MetrePerSecondSquared() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public MetrePerSecondSquared(T value) : base(value) { }
        /// <summary>
        /// 速度と時間から加速度を計算する
        /// </summary>
        public MetrePerSecondSquared(MetrePerSecond<T> velocity, Second<T> time) : base(velocity, time) { }
        /// <summary>
        /// 他の加速度から初期化
        /// </summary>
        public MetrePerSecondSquared(GS.StandardGravity<T> acceleration) :
            this(acceleration.Value * T.CreateChecked(9.80665m)) { }
        #endregion コンストラクタ

        #region 演算子
        /// <summary>
        /// 加速度と時間から速度を計算する
        /// </summary>
        public static MetrePerSecond<T> operator *(MetrePerSecondSquared<T> velocity, Second<T> time)
        {
            return new MetrePerSecond<T>(velocity.Value * time.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static MetrePerSecondSquared<T> operator *(T value, MetrePerSecondSquared<T> quantity)
        {
            return new MetrePerSecondSquared<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static MetrePerSecondSquared<T> operator *(MetrePerSecondSquared<T> quantity, T value)
        {
            return new MetrePerSecondSquared<T>(quantity.Value * value);
        }
        #endregion 演算子

        #region 型変換
        public static explicit operator MetrePerSecondSquared<T>(T acceleration) => new MetrePerSecondSquared<T>(acceleration);
        public static explicit operator MetrePerSecondSquared<T>(GS.StandardGravity<T> acceleration)=> new MetrePerSecondSquared<T>(acceleration);
        #endregion 型変換
    }
}