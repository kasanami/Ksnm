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
using System.Collections.Generic;

#if Ksnm_Using_UniLinq
using UniLinq;
#else
using System.Linq;
#endif

namespace Ksnm.ExtensionMethods.System
{
    /// <summary>
    /// Stringの拡張メソッド
    /// </summary>
    public static class String_
    {
        /// <summary>
        /// 半角を全角に変換する辞書
        /// </summary>
        static Dictionary<char, char> ToWideDictionary = new Dictionary<char, char>();
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static String_()
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
        /// 部分文字列を取得します。
        /// この部分文字列は、startString～endStringの文字列です。
        /// </summary>
        /// <param name="startString">開始文字列 nullを指定すると先頭から選択されます。</param>
        /// <param name="endString">終了文字列 nullを指定すると最後まで選択されます。</param>
        /// <param name="includeStartString">trueなら、開始文字列を含める</param>
        /// <param name="includeEndString">trueなら、終了文字列を含める</param>
        public static string Substring(this string self, string startString, string endString, bool includeStartString = false, bool includeEndString = false)
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
            return self.Substring(startIndex, endIndex - startIndex);
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
    }
}