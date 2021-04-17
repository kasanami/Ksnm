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

namespace Ksnm.Units.SI
{
    /// <summary>
    /// キログラム
    /// </summary>
    public class Kilogram<T> : Mass<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilogram";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "kg";
        #endregion プロパティ
        #region コンストラクタ
        public Kilogram()
        {
        }
        public Kilogram(T value)
        {
            Value = value;
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算し力を計算する
        /// </summary>
        public static Newton<T> operator *(Kilogram<T> mass, Acceleration<T> acceleration)
        {
            return new Newton<T>(mass, acceleration);
        }
        #endregion 演算子
    }
}