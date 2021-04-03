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
using System.Linq;

namespace Ksnm.Science.Electromagnetism
{
    /// <summary>
    /// 電気抵抗
    /// </summary>
    public class ElectricalResistance
    {
        /// <summary>
        /// 直列接続の合成抵抗を計算する。
        /// </summary>
        public static double SeriesConnection(params double[] resistances)
        {
            return resistances.Sum();
        }
        /// <summary>
        /// 並列接続の合成抵抗を計算する。
        /// </summary>
        public static double ParallelConnection(params double[] resistances)
        {
            if (resistances.Length == 1)
            {
                return resistances[0];
            }
            else if (resistances.Length == 2)
            {
                // 2個の場合は和分の積
                return (resistances[0] * resistances[1]) / (resistances[0] + resistances[1]);
            }
            return 1 / resistances.Select(r => 1 / r).Sum();
        }
    }
}