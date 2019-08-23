﻿/*
The zlib License

Copyright (c) 2018-2019 Takahiro Kasanami

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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable
{
    /// <summary>
    /// Enumerableの拡張メソッド
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 総乗
        /// </summary>
        public static int Product(this IEnumerable<int> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static uint Product(this IEnumerable<uint> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static long Product(this IEnumerable<long> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static ulong Product(this IEnumerable<ulong> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static float Product(this IEnumerable<float> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static double Product(this IEnumerable<double> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// 総乗
        /// </summary>
        public static decimal Product(this IEnumerable<decimal> source)
        {
            return source.Aggregate((product, item) => product * item);
        }
        /// <summary>
        /// デバッグ用文字列に変換します。
        /// </summary>
        public static string ToDebugString<T>(this IEnumerable<T> self, bool isMultiLine = false)
        {
            var str = new StringBuilder();
            str.Append("[" + self.Count() + "]={");
            for (int i = 0; i < self.Count(); ++i)
            {
                if (i != 0)
                {
                    str.Append(",");
                }
                if (isMultiLine)
                {
                    str.AppendLine();
                }
                str.Append(self.ElementAt(i).ToString());
            }
            if (isMultiLine)
            {
                str.AppendLine();
            }
            str.Append("}");
            return str.ToString();
        }
        /// <summary>
        /// デバッグ用文字列に変換します。
        /// </summary>
        public static string ToDebugString<T>(this IEnumerable<T> self, string format, global::System.IFormatProvider formatProvider, bool isMultiLine = false) where T : global::System.IFormattable
        {
            var str = new StringBuilder();
            str.Append("[" + self.Count() + "]={");
            for (int i = 0; i < self.Count(); ++i)
            {
                if (i != 0)
                {
                    str.Append(",");
                }
                if (isMultiLine)
                {
                    str.AppendLine();
                }
                str.Append(self.ElementAt(i).ToString(format, formatProvider));
            }
            if (isMultiLine)
            {
                str.AppendLine();
            }
            str.Append("}");
            return str.ToString();
        }
    }
}
