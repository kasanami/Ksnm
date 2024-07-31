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
using System;
using System.Runtime.CompilerServices;

namespace Ksnm
{
    /// <summary>
    /// デバッグ情報
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// この関数を呼び出し元の、ファイルパスと行番号とメンバー名を、Consoleに出力する。
        /// </summary>
        public static void WriteLineCallerInfo([CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            Console.WriteLine($"{memberName} {filePath} {lineNumber}");
        }
        /// <summary>
        /// この関数を呼び出し元の、ファイルパスと行番号を文字列形式で取得する。
        /// </summary>
        public static string GetFilePathAndLineNumber([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            return $"{filePath} {lineNumber}";
        }
        /// <summary>
        /// この関数を呼び出し元の、メソッドなどのメンバー名を文字列形式で取得する。
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static string GetMemberName([CallerMemberName] string memberName = "")
        {
            return memberName;
        }
    }
}
