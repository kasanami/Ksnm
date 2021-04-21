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
    /// 電荷
    /// </summary>
    public class ElectricCharge<T> : Quantity<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "electric charge";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol { get; protected set; }
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public ElectricCharge()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public ElectricCharge(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public ElectricCharge(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public ElectricCharge(decimal value) : base(value) { }
        /// <summary>
        /// 電流と時間から電荷を計算する
        /// </summary>
        public ElectricCharge(ElectricCurrent<T> current, Time<T> time)
        {
            Value = current.Value.Multiply(time.Value);
            Symbol = current.Symbol + "*" + time.Symbol;
        }
        #endregion コンストラクタ
    }
}