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
using Original = System;

namespace Ksnm.ExtensionMethods.System
{
    /// <summary>
    /// Arrayの拡張メソッド
    /// </summary>
    public static class Array_
    {
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
    }
}