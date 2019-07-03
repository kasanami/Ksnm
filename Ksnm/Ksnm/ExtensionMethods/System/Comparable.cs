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
using System;

namespace Ksnm.ExtensionMethods.System
{
    /// <summary>
    /// IComparableの拡張メソッド
    /// </summary>
    public static class Comparable
    {
        /// <summary>
        /// 指定した値を最小値と最大値の範囲に制限します。
        /// </summary>
        /// <typeparam name="T">IComparableを継承した型</typeparam>
        /// <param name="value">制限させる値</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>制限された値</returns>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            else if (value.CompareTo(max) > 0) return max;
            else return value;
        }
    }
}
