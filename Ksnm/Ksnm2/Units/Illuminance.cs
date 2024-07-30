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

namespace Ksnm.Units
{
    /// <summary>
    /// 照度
    /// </summary>
    public class Illuminance<T> : Quantity<T> where T : IMath<T>
    {
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Illuminance() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Illuminance(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Illuminance(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Illuminance(decimal value) : base(value) { }
        /// <summary>
        /// 光束と面積から照度を計算する
        /// </summary>
        /// <param name="luminousFlux">光束</param>
        /// <param name="area">面積</param>
        public Illuminance(LuminousFlux<T> luminousFlux, Area<T> area)
        {
            Value = luminousFlux.Value.Divide(area.Value);
            Symbol = luminousFlux.Symbol + "/" + area.Symbol;
        }
        #endregion コンストラクタ
    }
}