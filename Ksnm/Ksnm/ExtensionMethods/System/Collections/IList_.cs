/*
The zlib License

Copyright (c) 2014-2018 Takahiro Kasanami

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
using Original = System.Collections.Generic;
using Ksnm.ExtensionMethods.System;
using Random = global::System.Random;

namespace Ksnm.ExtensionMethods.System.Collections.Generic
{
    /// <summary>
    /// IListの拡張メソッド
    /// </summary>
    public static class IList_
    {
        /// <summary>
        /// 最後の要素を取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static T GetLast<T>(this Original.IList<T> list)
        {
            return list[list.Count - 1];
        }
        /// <summary>
        /// 最後の要素を削除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void RemoveLast<T>(this Original.IList<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
        /// <summary>
        /// 指定した要素が最後の要素と同じか判定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static bool EqualsLast<T>(this Original.IList<T> list, T item)
        {
            return list.GetLast().Equals(item);
        }
        /// <summary>
        /// リストの全要素を設定します。
        /// </summary>
        public static void SetAll<T>(this Original.IList<T> list, T value)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                list[i] = value;
            }
        }
        /// <summary>
        /// リストの指定要素を交換します
        /// </summary>
        public static void Swap<T>(this Original.IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        /// <summary>
        /// リストの要素をランダムに並び替える
        /// </summary>
        public static void Shuffle<T>(this Original.IList<T> list, Random random = null)
        {
            for (int indexA = 0; indexA < list.Count; ++indexA)
            {
                int indexB = list.RandomIndex(random);
                list.Swap(indexA, indexB);
            }
        }
        /// <summary>
        /// 指定したインデックス位置の項目を削除し、削除された項目を返します。
        /// </summary>
        public static T Pop<T>(this Original.IList<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }
        /// <summary>
        /// ランダムな位置の項目を削除し、削除された項目を返します。
        /// </summary>
        public static T PopRandom<T>(this Original.IList<T> list, Random random = null)
        {
            var index = list.RandomIndex(random);
            return list.Pop(index);
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int RandomIndex<T>(this Original.IList<T> list, Random random)
        {
            return random.Next(list.Count);
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int RandomIndex<T>(this Original.IList<T> list)
        {
            return list.RandomIndex(new Random());
        }
    }
}