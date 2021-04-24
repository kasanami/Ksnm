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

namespace Ksnm.Units
{
    /// <summary>
    /// 長さ
    /// </summary>
    public class Length<T> : Quantity<T> where T : IMath<T>
    {
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Length() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Length(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Length(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Length(decimal value) : base(value) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 2つの長さから面積を計算する
        /// </summary>
        public static Area<T> operator *(Length<T> valueL, Length<T> valueR)
        {
            return new Area<T>(valueL, valueR);
        }
        /// <summary>
        /// 距離と時間から速度を計算する
        /// </summary>
        public static Velocity<T> operator /(Length<T> length, Time<T> time)
        {
            return new Velocity<T>(length, time);
        }
        #endregion 演算子
    }
}