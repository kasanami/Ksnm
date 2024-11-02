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
    /// 平方メートル
    /// </summary>
    public class SquareMetre<T> : Area<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "square metre";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "m^2";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public SquareMetre()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public SquareMetre(T value) : base(value) { }
        /// <summary>
        /// 2つの長さから面積を計算する
        /// </summary>
        public SquareMetre(Metre<T> length1, Metre<T> length2) : base(length1, length2) { }
        public SquareMetre(KiloMetre<T> length1, KiloMetre<T> length2) : base((Metre<T>)length1, (Metre<T>)length2) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 面積と長さから体積を計算する
        /// </summary>
        public static CubicMetre<T> operator *(SquareMetre<T> area, Metre<T> length)
        {
            return new CubicMetre<T>(area, length);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static SquareMetre<T> operator *(T value, SquareMetre<T> quantity)
        {
            return new SquareMetre<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static SquareMetre<T> operator *(SquareMetre<T> quantity, T value)
        {
            return new SquareMetre<T>(quantity.Value * value);
        }
        #endregion 演算子
    }
}