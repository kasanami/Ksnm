/*
The zlib License

Copyright (c) 2019 Takahiro Kasanami

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
using static System.Math;

namespace Ksnm
{
    public static partial class Math
    {
        /// <summary>
        /// アルゴリズム
        /// </summary>
        public static class Algorithm
        {
            /// <summary>
            /// ガウス＝ルジャンドルのアルゴリズム
            /// </summary>
            /// <param name="count">計算回数</param>
            /// <returns>円周率</returns>
            public static double GaussLegendre(int count)
            {
                double a = 1.0;
                double b = 1.0 / Sqrt(2.0);
                double t = 1.0 / 4;
                double p = 1.0;
                for (var i = 0; i < count; i++)
                {
                    var beforeA = a;
                    a = (a + b) / 2;
                    b = Sqrt(beforeA * b);
                    t -= (p * (a - beforeA) * (a - beforeA));
                    p = 2 * p;
                }
                return (a + b) * (a + b) / (4 * t);
            }
        }
    }
}
