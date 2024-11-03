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
    /// 体積
    /// </summary>
    public class Volume<T> : Quantity<T> where T : INumber<T>
    {
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Volume() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Volume(T value) : base(value) { }
        /// <summary>
        /// 3つの長さから体積を計算する
        /// </summary>
        public Volume(Length<T> length1, Length<T> length2, Length<T> length3)
        {
            Value = length1.Value * length2.Value * length3.Value;
            if (length1.Symbol == length2.Symbol && length1.Symbol == length3.Symbol)
            {
                Symbol = length1.Symbol + "^3";
            }
            else
            {
                Symbol = length1.Symbol + "*" + length2.Symbol + "*" + length3.Symbol;
            }
        }
        /// <summary>
        /// 面積と長さから体積を計算する
        /// </summary>
        public Volume(Area<T> area, Length<T> length)
        {
            Value = area.Value * length.Value;
            Symbol = area.Symbol + "*" + length.Symbol;
        }
        #endregion コンストラクタ
    }
}