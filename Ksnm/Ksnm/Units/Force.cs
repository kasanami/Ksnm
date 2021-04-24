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
    /// 力
    /// </summary>
    public class Force<T> : Quantity<T> where T : IMath<T>
    {
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Force() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Force(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Force(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Force(decimal value) : base(value) { }
        /// <summary>
        /// 質量と加速度から力を計算する
        /// </summary>
        public Force(Mass<T> mass, Acceleration<T> acceleration)
        {
            Value = mass.Value.Multiply(acceleration.Value);
            if (mass.Symbol == acceleration.Symbol)
            {
                Symbol = mass.Symbol + "^2";
            }
            else
            {
                Symbol = mass.Symbol + "*" + acceleration.Symbol;
            }
        }
        #endregion コンストラクタ
    }
}