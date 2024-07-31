/*
The zlib License

Copyright (c) 2018 Takahiro Kasanami

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
using System.Text;
using Original = System.IO;

namespace Ksnm.IO
{
    /// <summary>
    /// System.IO.Fileに無い処理を定義
    /// </summary>
    public static class File
    {
        /// <summary>
        /// 新しいファイルを作成し、指定したバイト配列をそのファイルに書き込んだ後、ファイルを閉じます。
        /// 既存のターゲット ファイルは上書きされます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="bytes">ファイルに書き込むバイト。</param>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllBytes(path, bytes);
        }
        /// <summary>
        /// 新しいファイルを作成し、指定した文字列配列をそのファイルに書き込んだ後、ファイルを閉じます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む文字列配列。</param>
        public static void WriteAllLines(string path, string[] contents)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllLines(path, contents);
        }
        /// <summary>
        /// 新しいファイルを作成し、指定されたエンコーディングを使用することにより、指定された文字列配列をそのファイルに書き込んでから、そのファイルを閉じます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む文字列配列。</param>
        /// <param name="encoding">文字列配列に適用された文字エンコーディングを表す System.Text.Encoding オブジェクト。</param>
        public static void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllLines(path, contents, encoding);
        }
        /// <summary>
        /// 新しいファイルを作成し、文字列のコレクションをそのファイルに書き込んでから、そのファイルを閉じます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む行。</param>
        public static void WriteAllLines(string path, IEnumerable<string> contents)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllLines(path, contents);
        }
        /// <summary>
        /// 指定されたエンコーディングを使用して新しいファイルを作成し、
        /// 文字列のコレクションをそのファイルに書き込んでから、そのファイルを閉じます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む行。</param>
        /// <param name="encoding">使用する文字エンコーディング。</param>
        public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllLines(path, contents, encoding);
        }
        /// <summary>
        /// 新しいファイルを作成し、指定した文字列をそのファイルに書き込んだ後、
        /// ファイルを閉じます。既存のターゲット ファイルは上書きされます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む文字列。</param>
        public static void WriteAllText(string path, string contents)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllText(path, contents);
        }
        /// <summary>
        /// 新しいファイルを作成し、指定したエンコーディングで指定の文字列をそのファイルに書き込んだ後、
        /// ファイルを閉じます。既存のターゲット ファイルは上書きされます。
        /// フォルダの作成も行います。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="contents">ファイルに書き込む文字列。</param>
        /// <param name="encoding">文字列に適用するエンコーディング。</param>
        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            Directory.CreateParentDirectory(path);
            Original.File.WriteAllText(path, contents, encoding);
        }
    }
}
