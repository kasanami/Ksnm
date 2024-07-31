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

namespace Ksnm.Units
{
    /// <summary>
    /// 加速度
    /// </summary>
    public class Acceleration<T> : Quantity<T> where T : INumber<T>
    {
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Acceleration() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Acceleration(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Acceleration(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Acceleration(decimal value) : base(value) { }
        /// <summary>
        /// 速度と時間から加速度を計算する
        /// </summary>
        public Acceleration(Velocity<T> velocity, Time<T> time)
        {
            Value = velocity.Value / time.Value;
            Symbol = velocity.Symbol + "/" + time.Symbol;
        }
        #endregion コンストラクタ
    }
}