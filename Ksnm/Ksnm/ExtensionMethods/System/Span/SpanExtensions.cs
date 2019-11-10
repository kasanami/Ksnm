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
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Span
{
    /// <summary>
    /// Spanの拡張メソッド
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this ReadOnlySpan<T> self, ReadOnlySpan<T> value, int startIndex) where T : IEquatable<T>
        {
            var index = self.Slice(startIndex).IndexOf(value);
            if (index < 0)
            {
                return index;
            }
            return startIndex + index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="values"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOfAny<T>(this ReadOnlySpan<T> self, ReadOnlySpan<T> values, int startIndex) where T : IEquatable<T>
        {
            var index = self.Slice(startIndex).IndexOfAny(values);
            if (index < 0)
            {
                return index;
            }
            return startIndex + index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this Span<T> self, ReadOnlySpan<T> value, int startIndex) where T : IEquatable<T>
        {
            var index = self.Slice(startIndex).IndexOf(value);
            if (index < 0)
            {
                return index;
            }
            return startIndex + index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="includeStart"></param>
        /// <param name="includeEnd"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> Slice(this ReadOnlySpan<char> self, string start, string end, bool includeStart = false, bool includeEnd = false)
        {
            return self.Slice(start.AsSpan(), end.AsSpan(), includeStart, includeEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="includeStart"></param>
        /// <param name="includeEnd"></param>
        /// <returns></returns>
        public static ReadOnlySpan<T> Slice<T>(this ReadOnlySpan<T> self, T[] start, T[] end, bool includeStart = false, bool includeEnd = false) where T : IEquatable<T>
        {
            return self.Slice(start.AsSpan(), end.AsSpan(), includeStart, includeEnd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="includeStart"></param>
        /// <param name="includeEnd"></param>
        /// <returns></returns>
        public static ReadOnlySpan<T> Slice<T>(this ReadOnlySpan<T> self, ReadOnlySpan<T> start, ReadOnlySpan<T> end, bool includeStart = false, bool includeEnd = false) where T : IEquatable<T>
        {
            var startIndex = 0;
            if (start != null)
            {
                startIndex = self.IndexOf(start);
            }
            if (startIndex < 0)
            {
                throw new global::System.IndexOutOfRangeException("start が見つかりませんでした。");
            }
            var endIndex = self.Length;
            if (end != null)
            {
                if (start != null)
                {
                    endIndex = self.IndexOf(end, startIndex + start.Length);
                }
                else
                {
                    endIndex = self.IndexOf(end, startIndex);
                }
            }
            if (endIndex < 0)
            {
                throw new global::System.IndexOutOfRangeException("end が見つかりませんでした。");
            }
            if (includeStart == false && start != null)
            {
                startIndex += start.Length;
            }
            if (includeEnd && end != null)
            {
                endIndex += end.Length;
            }
            return self.Slice(startIndex, endIndex - startIndex);
        }
        /// <summary>
        /// 配列内の文字に基づいて文字列を部分文字列に分割します。
        /// </summary>
        /// <param name="self">文字列のSpan</param>
        /// <param name="separator">区切り文字</param>
        /// <returns>区切り文字で区切った部分文字列を格納したリスト</returns>
        public static List<string> Split(this ReadOnlySpan<char> self, params char[] separator)
        {
            var list = new List<string>();
            int length;
            int offset = 0;
            int index = self.IndexOfAny(separator);
            while (index >= 0)
            {
                length = index - offset;
                list.Add(self.Slice(offset, length).ToString());
                offset = index + 1;
                index = self.IndexOfAny(separator, offset);
            }
            length = self.Length - offset;
            list.Add(self.Slice(offset, length).ToString());
            return list;
        }
    }
}
