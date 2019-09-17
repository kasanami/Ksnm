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
using System.Linq;
using System.Text;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.IO;

namespace Ksnm.Utilities
{
    /// <summary>
    /// CSV形式でログを作成するロガー
    /// </summary>
    public class CsvLogger
    {
        /// <summary>
        /// CSVファイルのパス
        /// </summary>
        string filePath = null;
        /// <summary>
        /// CSVファイルのエンコーディング
        /// </summary>
        Encoding encoding = Encoding.UTF8;
        /// <summary>
        /// CSVファイルのパスを指定して初期化
        /// </summary>
        /// <param name="filePath">CSVファイルのパス</param>
        public CsvLogger(string filePath) : this(filePath, Encoding.UTF8)
        {
        }
        /// <summary>
        /// CSVファイルのパスと、エンコーディングを指定して初期化
        /// </summary>
        /// <param name="filePath">CSVファイルのパス</param>
        /// <param name="encoding">CSVファイルのエンコーディング</param>
        public CsvLogger(string filePath, Encoding encoding)
        {
            this.filePath = filePath;
            this.encoding = encoding;
            Directory.CreateParentDirectory(filePath);
        }
        /// <summary>
        /// ファイルにCSV形式の一行を追加します。
        /// </summary>
        /// <param name="values">追加する値</param>
        public void AppendLine(params string[] values)
        {
            AppendLine(filePath, encoding, values);
        }
        /// <summary>
        /// ファイルにCSV形式の一行を追加します。
        /// </summary>
        /// <param name="filePath">CSVファイルのパス</param>
        /// <param name="encoding">CSVファイルのエンコーディング</param>
        /// <param name="values">追加する値</param>
        public static void AppendLine(string filePath, Encoding encoding, params string[] values)
        {
            var escapeChars = new char[] { '\"', '\n', ',' };
            var text = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                if (i != 0)
                {
                    text.Append(",");
                }
                var arg = values[i];
                if (arg.Contains(escapeChars))
                {
                    arg = arg.Replace("\n", "\r\n");// 改行はCRLFに統一
                    // 特定の文字列が含まれている場合、ダブルクォーテーションで挟む
                    text.Append("\"" + arg + "\"");
                }
                else
                {
                    text.Append(arg);
                }
            }
            text.AppendLine();
            System.IO.File.AppendAllText(filePath, text.ToString(), encoding);
        }
    }
}
