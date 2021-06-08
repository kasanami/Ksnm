/*
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
using System;
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
        #region Contains
        /// <summary>
        /// シーケンスが既定の等値比較子を使用して、指定した要素を含んでいるか判定します。
        /// </summary>
        /// <returns>指定した要素を含んでいる場合はtrueを返す。</returns>
        public static bool Contains<T>(this IEnumerable<T> source, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (source.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 複数のシーケンスを連結します。
        /// </summary>
        /// <param name="self">連結するシーケンス。</param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> self)
        {
            return self.SelectMany(item => item);
        }
        #endregion Contains

        #region SequenceEqual
        /// <summary>
        /// System.Linq.Enumerable.SequenceEqual を可変長引数にしたもの。
        /// </summary>
        public static bool SequenceEqual<T>(this IEnumerable<T> self, params T[] values)
        {
            return global::System.Linq.Enumerable.SequenceEqual(self, values);
        }
        #endregion SequenceEqual

        #region Product
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
        #endregion Product

        #region ToString
        /// <summary>
        /// コレクションのメンバーを連結します。各メンバーの間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T">values のメンバーの型。</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション。</param>
        /// <param name="separator">区切り文字として使用する文字列。戻される文字列に separator が含まれるのは、values に複数の要素がある場合のみです。</param>
        /// <returns>values のメンバーからなる、separator 文字列で区切られた文字列。 values にメンバーがない場合、メソッドは System.String.Emptyを返します。</returns>
        public static string ToJoinedString<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join<T>(separator, values);
        }
        /// <summary>
        /// コレクションのメンバーを連結します。各メンバーの間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T">values のメンバーの型。</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション。</param>
        /// <param name="separator">区切り文字として使用する文字列。戻される文字列に separator が含まれるのは、values に複数の要素がある場合のみです。</param>
        /// <param name="format">使用する書式。</param>
        /// <param name="formatProvider">値を書式設定するために使用するプロバイダー。</param>
        /// <returns>values のメンバーからなる、separator 文字列で区切られた文字列。 values にメンバーがない場合、メソッドは System.String.Emptyを返します。</returns>
        public static string ToJoinedString<T>(this IEnumerable<T> values, string separator, string format, IFormatProvider formatProvider = null) where T : IFormattable
        {
            return values.Select(item => item.ToString(format, formatProvider)).ToJoinedString(separator);
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
        #endregion ToString
    }
}