/*
The zlib License

Copyright (c) 2014-2019 Takahiro Kasanami

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

#if Ksnm_Using_UniLinq
using UniLinq;
#else
using System.Linq;
#endif

namespace Ksnm.ExtensionMethods.System.String
{
    /// <summary>
    /// Stringの拡張メソッド
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 半角を全角に変換する辞書
        /// ※IReadOnlyDictionary ではないので、publicにしてはだめ。
        /// 　IReadOnlyDictionary にしないのは、for文で初期化したいからだけど、自動生成にしたほうがいいかな。
        /// </summary>
        static readonly Dictionary<char, char> ToWideDictionary = new Dictionary<char, char>();
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static StringExtensions()
        {
            // 半角を全角に変換する辞書
            #region ToWideDictionary
            ToWideDictionary[' '] = '　';
            ToWideDictionary['!'] = '！';
            ToWideDictionary['"'] = '”';
            ToWideDictionary['#'] = '＃';
            ToWideDictionary['$'] = '＄';
            ToWideDictionary['%'] = '％';
            ToWideDictionary['&'] = '＆';
            ToWideDictionary['\''] = '’';
            ToWideDictionary['('] = '（';
            ToWideDictionary[')'] = '）';
            ToWideDictionary['*'] = '＊';
            ToWideDictionary['+'] = '＋';
            ToWideDictionary[','] = '，';
            ToWideDictionary['-'] = '－';
            ToWideDictionary['.'] = '．';
            ToWideDictionary['/'] = '／';
            for (int i = '0'; i <= '9'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('０' + (i - '0'));
            }
            ToWideDictionary[':'] = '：';
            ToWideDictionary[';'] = '；';
            ToWideDictionary['<'] = '＜';
            ToWideDictionary['='] = '＝';
            ToWideDictionary['>'] = '＞';
            ToWideDictionary['?'] = '？';
            ToWideDictionary['@'] = '＠';
            for (int i = 'A'; i <= 'Z'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('Ａ' + (i - 'A'));
            }
            ToWideDictionary['['] = '［';
            ToWideDictionary['\\'] = '￥';
            ToWideDictionary[']'] = '］';
            ToWideDictionary['^'] = '＾';
            ToWideDictionary['_'] = '＿';
            ToWideDictionary['`'] = '｀';
            for (int i = 'a'; i <= 'z'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('ａ' + (i - 'a'));
            }
            ToWideDictionary['{'] = '｛';
            ToWideDictionary['|'] = '｜';
            ToWideDictionary['}'] = '｝';
            ToWideDictionary['~'] = '\uFF5E';// 全角チルダ
            #endregion ToWideDictionary
        }
        /// <summary>
        /// 最後の文字を取得
        /// </summary>
        public static char GetLast(this string self)
        {
            return self[self.Length - 1];
        }
        /// <summary>
        /// ひらがなをカタカナに変換します。
        /// </summary>
        public static string HiraganaToKatakana(this string self)
        {
            return new string(self.Select(c => (c >= 'ぁ' && c <= 'ゖ') ? (char)(c + 'ァ' - 'ぁ') : c).ToArray());
        }
        /// <summary>
        /// カタカナをひらがなに変換します。
        /// </summary>
        public static string KatakanaToHiragana(this string self)
        {
            return new string(self.Select(c => (c >= 'ァ' && c <= 'ヶ') ? (char)(c + 'ぁ' - 'ァ') : c).ToArray());
        }
        /// <summary>
        /// 半角を全角に変換
        /// </summary>
        public static string ToWide(this string self)
        {
            return new string(self.Select(c => ToWideDictionary.ContainsKey(c) ? ToWideDictionary[c] : c).ToArray());
        }
        /// <summary>
        /// 数値の文字列形式を、それと等価な 32 ビット符号付き整数に変換します。
        /// </summary>
        public static int ToInt32(this string self)
        {
            return int.Parse(self);
        }
        /// <summary>
        /// 数値の文字列形式を、それと等価な 64 ビット符号付き整数に変換します。
        /// </summary>
        public static long ToInt64(this string self)
        {
            return long.Parse(self);
        }
        /// <summary>
        /// 数値の文字列形式を、それと等しい単精度浮動小数点数に変換します。
        /// </summary>
        public static float ToSingle(this string self)
        {
            return float.Parse(self);
        }
        /// <summary>
        /// 数値の文字列形式を、等価の倍精度浮動小数点数に変換します。
        /// </summary>
        public static double ToDouble(this string self)
        {
            return double.Parse(self);
        }
        /// <summary>
        /// 数値の文字列形式を、それと等価の System.Decimal に変換します。
        /// </summary>
        public static decimal ToDecimal(this string self)
        {
            return decimal.Parse(self);
        }
        /// <summary>
        /// 制御文字を削除する
        /// </summary>
        /// <returns>削除</returns>
        public static string RemoveControlChar(this string self)
        {
            return new string(self.Where(c => !char.IsControl(c)).ToArray());
        }
        /// <summary>
        /// 部分文字列を取得します。
        /// この部分文字列は、startString～endStringの文字列です。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="startString">開始文字列 nullを指定すると先頭から選択されます。</param>
        /// <param name="endString">終了文字列 nullを指定すると最後まで選択されます。</param>
        /// <param name="includeStartString">trueなら、開始文字列を含める</param>
        /// <param name="includeEndString">trueなら、終了文字列を含める</param>
        public static string Substring(this string self, string startString, string endString, bool includeStartString = false, bool includeEndString = false)
        {
            return self.AsSpan(startString, endString, includeStartString, includeEndString).ToString();
        }
        /// <summary>
        /// 部分文字列を取得します。
        /// この部分文字列は、startString～endStringの文字列です。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="startString">開始文字列 nullを指定すると先頭から選択されます。</param>
        /// <param name="endString">終了文字列 nullを指定すると最後まで選択されます。</param>
        /// <param name="includeStartString">trueなら、開始文字列を含める</param>
        /// <param name="includeEndString">trueなら、終了文字列を含める</param>
        public static ReadOnlySpan<char> AsSpan(this string self, string startString, string endString, bool includeStartString = false, bool includeEndString = false)
        {
            var startIndex = 0;
            if (startString != null)
            {
                startIndex = self.IndexOf(startString);
            }
            if (startIndex < 0)
            {
                throw new global::System.IndexOutOfRangeException("startString が見つかりませんでした。");
            }
            var endIndex = self.Length;
            if (endString != null)
            {
                if (startString != null)
                {
                    endIndex = self.IndexOf(endString, startIndex + startString.Length);
                }
                else
                {
                    endIndex = self.IndexOf(endString, startIndex);
                }
            }
            if (endIndex < 0)
            {
                throw new global::System.IndexOutOfRangeException("endString が見つかりませんでした。");
            }
            if (includeStartString == false && startString != null)
            {
                startIndex += startString.Length;
            }
            if (includeEndString && endString != null)
            {
                endIndex += endString.Length;
            }
            return self.AsSpan(startIndex, endIndex - startIndex);
        }
        /// <summary>
        /// 配列内の文字数に基づいて文字列を部分文字列に分割します。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="lengths">分割する文字数のリスト</param>
        /// <returns>部分文字列のコレクション</returns>
        public static IEnumerable<string> Split(this string self, IEnumerable<int> lengths)
        {
            int offset = 0;
            foreach (var length in lengths)
            {
                yield return new string(self.Skip(offset).Take(length).ToArray());
                offset += length;
            }
        }
        /// <summary>
        /// 配列内の文字数に基づいて文字列を部分文字列に分割します。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="lengths">分割する文字数のリスト</param>
        /// <returns>部分文字列のコレクション</returns>
        public static IEnumerable<string> Split(this string self, params int[] lengths)
        {
            return self.Split((IEnumerable<int>)lengths);
        }
        /// <summary>
        /// 文字列が null または System.String.Empty 文字列であるかどうかを示します。
        /// </summary>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }
        /// <summary>
        /// 文字列が null または空であるか、空白文字だけで構成されているかどうかを示します。
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }
        /// <summary>
        /// このインスタンスに出現する指定文字をすべて、指定した別の文字に置換します。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="oldChars">置換する文字。</param>
        /// <param name="newChar">置換後の文字。</param>
        /// <returns>変更後の新しいインスタンス</returns>
        public static string Replace(this string self, IEnumerable<char> oldChars, char newChar)
        {
            var temp = new global::System.Text.StringBuilder(self);
            foreach (var oldChar in oldChars)
            {
                temp.Replace(oldChar, newChar);
            }
            return temp.ToString();
#if false
            // この方法は遅かった
            return new string(self.Select(c => oldChars.Contains(c) ? newChar : c).ToArray());
#endif
        }
        /// <summary>
        /// このインスタンスに出現する指定文字列をすべて、指定した別の文字列に置換します。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="oldValues">置換する文字列。</param>
        /// <param name="newValue">置換後の文字列。</param>
        /// <returns>変更後の新しいインスタンス</returns>
        public static string Replace(this string self, IEnumerable<string> oldValues, string newValue)
        {
            var temp = new global::System.Text.StringBuilder(self);
            foreach (var oldValue in oldValues)
            {
                temp.Replace(oldValue, newValue);
            }
            return temp.ToString();
        }
        /// <summary>
        /// 指定文字列が連続して出現する場合、一つの文字列に置換します。
        /// </summary>
        /// <param name="self">インスタンス</param>
        /// <param name="value">置換する文字列。</param>
        /// <returns>変更後の新しいインスタンス</returns>
        public static string Unify(this string self, string value)
        {
            string oldValue = value + value;
            var temp = new global::System.Text.StringBuilder(self);
            int afterLength = temp.Length;
            int beforeLength;
            do
            {
                beforeLength = afterLength;
                temp.Replace(oldValue, value);
                afterLength = temp.Length;
            }
            while (beforeLength != afterLength);
            return temp.ToString();
        }
    }
}
