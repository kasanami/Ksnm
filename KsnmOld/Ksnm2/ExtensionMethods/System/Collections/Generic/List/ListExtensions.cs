/*
The zlib License

Copyright (c) 2014-2021 Takahiro Kasanami

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

namespace Ksnm.ExtensionMethods.System.Collections.Generic.List
{
    /// <summary>
    /// Listの拡張メソッド
    /// </summary>
    public static class ListExtensions
    {
        #region IndexOf
        /// <summary>
        /// 最大の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// </summary>
        public static int IndexOfMax(this IList<int> list)
        {
            long max = long.MinValue;
            var index = 0;
            var maxIndex = -1;
            foreach (var item in list)
            {
                if (max < item)
                {
                    max = item;
                    maxIndex = index;
                }
                index++;
            }
            return maxIndex;
        }
        /// <summary>
        /// 最小の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// </summary>
        public static int IndexOfMin(this IList<int> list)
        {
            long min = long.MaxValue;
            var index = 0;
            var minIndex = -1;
            foreach (var item in list)
            {
                if (min > item)
                {
                    min = item;
                    minIndex = index;
                }
                index++;
            }
            return minIndex;
        }
        /// <summary>
        /// 最大の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// 全要素が無限大なら-1を返す。
        /// </summary>
        public static int IndexOfMax(this IList<double> list)
        {
            double max = double.NegativeInfinity;
            var index = 0;
            var maxIndex = -1;
            foreach (var item in list)
            {
                if (max < item)
                {
                    max = item;
                    maxIndex = index;
                }
                index++;
            }
            return maxIndex;
        }
        /// <summary>
        /// 最小の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// 全要素が無限大なら-1を返す。
        /// </summary>
        public static int IndexOfMin(this IList<double> list)
        {
            double min = double.PositiveInfinity;
            var index = 0;
            var minIndex = -1;
            foreach (var item in list)
            {
                if (min > item)
                {
                    min = item;
                    minIndex = index;
                }
                index++;
            }
            return minIndex;
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int IndexOfRandom<T>(this IList<T> list, global::System.Random random)
        {
            return random.Next(list.Count);
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int IndexOfRandom<T>(this IList<T> list)
        {
            return list.IndexOfRandom(new global::System.Random());
        }
        #endregion IndexOf
        /// <summary>
        /// 指定位置から最後までを削除
        /// </summary>
        public static void RemoveRange<T>(this List<T> list, int index)
        {
            list.RemoveRange(index, list.Count - index);
        }
        /// <summary>
        /// 最後の要素を削除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void RemoveLast<T>(this IList<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
        /// <summary>
        /// リストの全要素を設定します。
        /// </summary>
        public static void SetAll<T>(this IList<T> list, T value)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                list[i] = value;
            }
        }
        /// <summary>
        /// リストの指定要素を交換します
        /// </summary>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        /// <summary>
        /// リストの要素をランダムに並び替える
        /// </summary>
        public static void Shuffle<T>(this IList<T> list, global::System.Random random)
        {
            for (int indexA = 0; indexA < list.Count; ++indexA)
            {
                int indexB = list.IndexOfRandom(random);
                list.Swap(indexA, indexB);
            }
        }
        /// <summary>
        /// 指定したインデックス位置の項目を削除し、削除された項目を返します。
        /// </summary>
        public static T Pop<T>(this IList<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }
        /// <summary>
        /// ランダムな位置の項目を削除し、削除された項目を返します。
        /// </summary>
        public static T PopRandom<T>(this IList<T> list, global::System.Random random)
        {
            var index = list.IndexOfRandom(random);
            return list.Pop(index);
        }
        /// <summary>
        /// ランダムな位置の項目を削除し、削除された項目を返します。
        /// </summary>
        public static T PopRandom<T>(this IList<T> list)
        {
            var index = list.IndexOfRandom();
            return list.Pop(index);
        }
    }
}