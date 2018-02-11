/*
The zlib License

Copyright (c) 2017 Takahiro Kasanami

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

namespace Ksnm.ExtensionMethods.System.Collections
{
    /// <summary>
    /// Dictionaryの拡張メソッド
    /// </summary>
    public static class Dictionary_
    {
        /// <summary>
        /// 指定したキーが存在しなければ、指定したキーと値を追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddIfKeyNotExists<TKey, TValue>(this Original.Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key) == false)
            {
                dictionary.Add(key, value);
            }
        }
        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <param name="key">取得する値のキー。</param>
        /// <param name="defaultValue">キーが見つからない場合の value パラメーターの型に対する既定の値。</param>
        public static TValue GetValueOrDefault<TKey, TValue>(this Original.Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
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
        /// <param name="key">取得する値のキー。</param>
        public static TValue GetValueOrDefault<TKey, TValue>(this Original.Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.GetValueOrDefault(key, default(TValue));
        }
    }
}
