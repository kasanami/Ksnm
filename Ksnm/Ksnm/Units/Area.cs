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
    /// 面積
    /// </summary>
    public class Area<T> : Quantity<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "area";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol { get; protected set; }
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Area()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Area(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Area(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Area(decimal value) : base(value) { }
        /// <summary>
        /// ２つの長さから面積を計算する
        /// </summary>
        public Area(Length<T> length1, Length<T> length2)
        {
            Value = length1.Value.Multiply(length2.Value);
            if (length1.Symbol == length2.Symbol)
            {
                Symbol = length1.Symbol + "^2";
            }
            else
            {
                Symbol = length1.Symbol + "*" + length2.Symbol;
            }
        }
        #endregion コンストラクタ
    }
}