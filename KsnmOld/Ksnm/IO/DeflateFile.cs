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
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Ksnm.IO
{
    /// <summary>
    /// デフレート アルゴリズムを使用して、ファイルの作成、圧縮、圧縮解除を実行するためのメソッドを提供します。
    /// </summary>
    public static class DeflateFile
    {
        /// <summary>
        /// バイナリ ファイルを開き、ファイルの内容をバイト配列に読み取った後、ファイルを閉じます。
        /// </summary>
        /// <param name="path">読み取り用に開かれるファイル。</param>
        /// <returns>ファイルの内容を格納しているバイト配列。</returns>
        public static byte[] ReadAllBytes(string path)
        {
            using (var bufferStream = new MemoryStream())
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true))
                    {
                        deflateStream.CopyTo(bufferStream);
                    }
                }
                return bufferStream.ToArray();
            }
        }
        /// <summary>
        /// ファイルを開き、指定したエンコーディングが適用されたファイルのすべての行を読み取った後、ファイルを閉じます。
        /// </summary>
        /// <param name="path">読み取り用に開かれるファイル。</param>
        /// <param name="encoding">ファイルの内容に適用されるエンコーディング。</param>
        /// <returns>ファイルのすべての行を格納している文字列。</returns>
        public static string ReadAllText(string path, Encoding encoding)
        {
            var bytes = ReadAllBytes(path);
            return encoding.GetString(bytes);
        }
        /// <summary>
        /// 新しいファイルを作成し、指定したバイト配列をそのファイルに書き込んだ後、ファイルを閉じます。既存のターゲット ファイルは上書きされます。
        /// </summary>
        /// <param name="path">書き込み先のファイル。</param>
        /// <param name="bytes">ファイルに書き込むバイト。</param>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            Directory.CreateParentDirectory(path);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var deflateStream = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);
                }
            }
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
            var bytes = encoding.GetBytes(contents);
            WriteAllBytes(path, bytes);
        }
    }
}
