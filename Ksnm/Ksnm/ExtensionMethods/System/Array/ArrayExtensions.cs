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
using System.Numerics;
using _Debug = System.Diagnostics.Debug;
using Original = System;

namespace Ksnm.ExtensionMethods.System.Array
{
    /// <summary>
    /// Arrayの拡張メソッド
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 指定したオブジェクトを検索し、1 次元配列でそのオブジェクトが最初に見つかった位置のインデックス番号を返します。
        /// </summary>
        /// <typeparam name="T">配列要素の型。</typeparam>
        /// <param name="array">検索する 1 次元の配列。インデックス番号が 0 から始まる必要があります。</param>
        /// <param name="value">array 内で検索するオブジェクト。</param>
        /// <returns>array 全体を対象とする value の検索で最初に見つかった位置のインデックス (0 から始まる)。それ以外の場合は -1。</returns>
        /// <exception cref="global::System.ArgumentNullException">array は nullです。</exception>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Original.Array.IndexOf(array, value);
        }
        /// <summary>
        /// 指定されたオブジェクトを 1 次元配列の要素範囲内で検索し、最初に見つかったオブジェクトのインデックスを返します。 要素範囲は、指定されたインデックスから、配列の最後までの範囲です。
        /// </summary>
        /// <typeparam name="T">配列要素の型。</typeparam>
        /// <param name="array">検索する 1 次元配列。</param>
        /// <param name="value">array 内で検索するオブジェクト。</param>
        /// <param name="startIndex">検索の開始インデックス。 空の配列の場合 0 (ゼロ) は有効です。</param>
        /// <returns>array の startIndex から最後の要素までの範囲内で value が見つかった場合は、最初に見つかった位置のインデックス。それ以外の場合は、-1。</returns>
        /// /// <exception cref="global::System.ArgumentNullException">array は nullです。</exception>
        /// /// <exception cref="global::System.ArgumentOutOfRangeException">startIndex は array の有効なインデックスの範囲外です。</exception>
        /// /// <exception cref="global::System.RankException">array が多次元です。</exception>
        public static int IndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Original.Array.IndexOf(array, value, startIndex);
        }
        /// <summary>
        /// 最小の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// </summary>
        public static int IndexOfMin<T>(this T[] array)
            where T : INumber<T>, IMinMaxValue<T>
        {
            T min = T.MaxValue;
            var index = 0;
            var minIndex = -1;
            foreach (var item in array)
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
        /// <para>２つの配列の要素の大きさを比較します。</para>
        /// <para>長さが違う場合、短い方で比較され、それでも同じであれば、長い方が大きいと判定されます。</para>
        /// <para>配列の要素が同じか判定するだけであれば、System.LinqのSequenceEqual関数を使用する方法もあります。</para>
        /// </summary>
        /// <returns>
        /// <para>other より小さい場合、0未満の値を返す。</para>
        /// <para>other と等しい場合、0を返す。</para>
        /// <para>other より大きい場合、0 を超える値を返す。</para>
        /// </returns>
        public static int Compare<T>(this T[] array, T[] other) where T : Original.IComparable<T>
        {
            // 短い方の長さ
            int length = Original.Math.Min(array.Length, other.Length);
            for (int i = 0; i < length; i++)
            {
                var valueA = array[i];
                var valueB = other[i];
                var valueCompare = valueA.CompareTo(valueB);
                if (valueCompare != 0)
                    return valueCompare;
            }
            // 長い方が大きいとする
            if (array.Length > other.Length)
                return +1;
            else if (array.Length < other.Length)
                return -1;
            // 等しい
            return 0;
        }
        /// <summary>
        /// 最後の要素を取得
        /// </summary>
        public static T GetLast<T>(this T[] self)
        {
            return self[self.Length - 1];
        }

        /// <summary>
        /// 各次元のLengthを1/2した位置の要素を取得
        /// </summary>
        public static object GetCenterValue(this Original.Array array)
        {
            var indices = new int[array.Rank];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = array.GetLength(i) / 2;
            }
            return array.GetValue(indices);
        }
        /// <summary>
        /// 各次元のLengthを1/2した位置の要素を設定
        /// </summary>
        public static void SetCenterValue(this Original.Array array, object value)
        {
            var indices = new int[array.Rank];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = array.GetLength(i) / 2;
            }
            array.SetValue(value, indices);
        }
        #region CopyFrom
        /// <summary>
        /// コレクションの要素を配列へコピーする。
        /// </summary>
        public static void CopyFrom<T>(this T[] self, IReadOnlyList<T> values)
        {
            Original.Diagnostics.Debug.Assert(self.Length == values.Count);
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = values[i];
            }
        }
        /// <summary>
        /// 2次元配列の要素を1次元配列へコピーする。
        /// </summary>
        public static void CopyFrom<T>(this T[] self, in T[,] values)
        {
            int index = 0;
            var length0 = values.GetLength(0);
            var length1 = values.GetLength(1);
            _Debug.Assert(self.Length == (length0 * length1));
            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    self[index] = values[i, j];
                    index++;
                }
            }
        }
        /// <summary>
        /// 3次元配列の要素を1次元配列へコピーする。
        /// </summary>
        public static void CopyFrom<T>(this T[] self, in T[,,] values)
        {
            int index = 0;
            var length0 = values.GetLength(0);
            var length1 = values.GetLength(1);
            var length2 = values.GetLength(2);
            _Debug.Assert(self.Length == (length0 * length1 * length2)); ;
            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    for (int k = 0; k < length2; k++)
                    {
                        self[index] = values[i, j, k];
                        index++;
                    }
                }
            }
        }
        #endregion CopyFrom
#if false// EnumerableExtensionsに移動
        /// <summary>
        /// 配列をデバッグ用文字列に変換します。
        /// </summary>
        /// <param name="array">変換する配列</param>
        /// <param name="isMultiLine">trueなら１要素を１行に出力</param>
        /// <returns></returns>
        public static string ToDebugString(this Original.Array array, bool isMultiLine = false)
        {
            var str = new Original.Text.StringBuilder();
            str.Append("[" + array.Length + "]={");
            if (isMultiLine)
            {
                str.AppendLine();
                for (int i = 0; i < array.Length; ++i)
                {
                    str.Append("[" + i + "]=");
                    str.AppendLine(array.GetValue(i).ToString());
                }
            }
            else
            {
                for (int i = 0; i < array.Length; ++i)
                {
                    if (i != 0)
                    {
                        str.Append(",");
                    }
                    str.Append(array.GetValue(i).ToString());
                }
            }
            str.Append("}");
            return str.ToString();
        }
#endif
    }
}
