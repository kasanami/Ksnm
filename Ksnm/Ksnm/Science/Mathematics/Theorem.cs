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
using System.Numerics;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 数学の定理
    /// </summary>
    public class Theorem
    {
        /// <summary>
        /// ウィルソンの定理
        /// p が大きくなるにつれて計算量が膨大になるため、素数を判定するために用いるには実用的ではない。
        /// ※ 1 は例外で true となる。
        /// </summary>
        /// <returns>p が素数ならば true</returns>
        public static bool Wilsons(int p)
        {
            return (Math.Factorial((BigInteger)p - 1) + 1) % p == 0;
        }
    }
}