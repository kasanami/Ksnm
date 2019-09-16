using System.Linq;
using System.Text;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.IO;

namespace Ksnm.Utilities
{
    /// <summary>
    /// CSV形式でログを作成するロガー
    /// </summary>
    public class CSVLogger
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
        public CSVLogger(string filePath) : this(filePath, Encoding.UTF8)
        {
        }
        /// <summary>
        /// CSVファイルのパスと、エンコーディングを指定して初期化
        /// </summary>
        /// <param name="filePath">CSVファイルのパス</param>
        /// <param name="encoding">エンコーディング</param>
        public CSVLogger(string filePath, Encoding encoding)
        {
            this.filePath = filePath;
            this.encoding = encoding;
            Directory.CreateParentDirectory(filePath);
        }
        /// <summary>
        /// ファイルにCSV形式の一行を追加します。
        /// </summary>
        /// <param name="args"></param>
        public void AppendLine(params string[] args)
        {
            AppendLine(filePath, encoding, args);
        }
        /// <summary>
        /// ファイルにCSV形式の一行を追加します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="args"></param>
        public static void AppendLine(string filePath, Encoding encoding, params string[] args)
        {
            var escapeChars = new char[] { '\"', '\n', ',' };
            var text = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                if (i != 0)
                {
                    text.Append(",");
                }
                var arg = args[i];
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
