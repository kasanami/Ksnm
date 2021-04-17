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
    /// ワット
    /// </summary>
    public class Watt<T> : Power<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "watt";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "W";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Watt()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Watt(T value)
        {
            Value = value;
        }
        /// <summary>
        /// エネルギーと時間から速度を計算する
        /// </summary>
        public Watt(Joule<T> energy, Second<T> time) : base(energy, time)
        {
        }
        #endregion コンストラクタ
    }
}