/*
The zlib License

Copyright (c) 2019-2021 Takahiro Kasanami

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
using static Ksnm.Math;

namespace Ksnm.Science.Mathematics
{
    /// <summary>
    /// 公式
    /// </summary>
    public static class Formula
    {
        /// <summary>
        /// ライプニッツの公式
        /// </summary>
        /// <param name="count">計算回数</param>
        /// <returns>PI/4(円周率の4分の1)</returns>
        public static double Leibniz(int count)
        {
            double sum = 0.0;
            for (var i = 0; i < count; i++)
            {
                if (IsEven(i))
                {
                    sum += 1.0 / (2.0 * i + 1);
                }
                else
                {
                    sum -= 1.0 / (2.0 * i + 1);
                }
            }
            return sum;
        }
    }
}
