/*
The zlib License

Copyright (c) 2017-2019 Takahiro Kasanami

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

namespace Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary
{
    /// <summary>
    /// Dictionaryの拡張メソッド
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 指定したキーが存在しなければ、指定したキーと値を追加
        /// </summary>
        /// <param name="dictionary">インスタンス</param>
        /// <param name="key">検索するキー</param>
        /// <param name="value">追加する値</param>
        public static void AddIfKeyNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key) == false)
            {
                dictionary.Add(key, value);
            }
        }
        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <param name="dictionary">インスタンス</param>
        /// <param name="key">取得する値のキー。</param>
        /// <param name="defaultValue">キーが見つからない場合の value パラメーターの型に対する既定の値。</param>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return defaultValue;
        }
        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <param name="dictionary">インスタンス</param>
        /// <param name="key">取得する値のキー。</param>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.GetValueOrDefault(key, default(TValue));
        }
        /// <summary>
        /// キーに従って昇順のシーケンスの要素を並べ替えます。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="self">インスタンス</param>
        /// <returns>並べ替え後の新しいインスタンス</returns>
        public static Dictionary<TKey, TValue> OrderByKey<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self.OrderBy(item => item.Key)
                .ToDictionary(item => item.Key, item => item.Value);
        }
        /// <summary>
        /// 値に従って昇順のシーケンスの要素を並べ替えます。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="self">インスタンス</param>
        /// <returns>並べ替え後の新しいインスタンス</returns>
        public static Dictionary<TKey, TValue> OrderByValue<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self.OrderBy(item => item.Value)
                .ToDictionary(item => item.Key, item => item.Value);
        }
        /// <summary>
        /// キーに従って昇順のシーケンスの要素を並べ替えます。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="self">インスタンス</param>
        /// <returns>並べ替え後の新しいインスタンス</returns>
        public static Dictionary<TKey, TValue> OrderByDescendingKey<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self.OrderByDescending(item => item.Key)
                .ToDictionary(item => item.Key, item => item.Value);
        }
        /// <summary>
        /// 値に従って降順のシーケンスの要素を並べ替えます。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="self">インスタンス</param>
        /// <returns>並べ替え後の新しいインスタンス</returns>
        public static Dictionary<TKey, TValue> OrderByDescendingValue<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self.OrderByDescending(item => item.Value)
                .ToDictionary(item => item.Key, item => item.Value);
        }
        /// <summary>
        /// 型に対して既定の等値比較子を使用して要素を比較することで、2 つの Dictionary が等しいかどうかを決定します。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="self">インスタンス</param>
        /// <param name="other">インスタンス</param>
        /// <returns>等しい場合は true それ以外の場合 false です。</returns>
        public static bool SequenceEqual<TKey, TValue>(this IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> other)
        {
            var selfEnumerator = self.GetEnumerator();
            var otherEnumerator = other.GetEnumerator();
            while (true)
            {
                var movedA = selfEnumerator.MoveNext();
                var movedB = otherEnumerator.MoveNext();
                // 片方が終わったら終了
                if (movedA == false || movedB == false)
                {
                    return movedA == movedB;// 両方同時に終了ならtrue;
                }
                var selfCurrent = selfEnumerator.Current;
                var otherCurrent = otherEnumerator.Current;
                if (selfCurrent.Key.Equals(otherCurrent.Key) == false)
                {
                    return false;
                }
                if (selfCurrent.Value.Equals(otherCurrent.Value) == false)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// デバッグ用文字列に変換します。
        /// </summary>
        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> self, string keyFormat, string valueFormat, bool isMultiLine = false) where TKey : IFormattable where TValue : IFormattable
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
                str.Append("[");
                str.Append(self.ElementAt(i).Key.ToString(keyFormat, null));
                str.Append("]=");
                str.Append(self.ElementAt(i).Value.ToString(valueFormat, null));
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