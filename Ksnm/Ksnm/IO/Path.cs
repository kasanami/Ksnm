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
using System.Collections.Generic;

namespace Ksnm.IO
{
    /// <summary>
    /// System.IO.Pathに無い処理を定義
    /// </summary>
    public class Path
    {
        /// <summary>
        /// ファイル名に使用できない文字と、その全角文字。
        /// </summary>
        static Dictionary<char, char> InvalidFileNameWideChars = new Dictionary<char, char>()
        {
            {'\\','＼' },
            {'/' ,'／' },
            {':' ,'：' },
            {'*' ,'＊' },
            {'?' ,'？' },
            {'\"','”' },
            {'<' ,'＜' },
            {'>' ,'＞' },
            {'|' ,'｜' },
        };
        /// <summary>
        /// ファイル名に使えない文字を使える文字に変換
        /// </summary>
        public static string ToSafeFileName(string fileName)
        {
            foreach (var item in InvalidFileNameWideChars)
            {
                fileName = fileName.Replace(item.Key, item.Value);
            }
            return fileName;
        }
    }
}
