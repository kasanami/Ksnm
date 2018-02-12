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
using Original = System.IO;

namespace Ksnm.IO
{
    /// <summary>
    /// System.IO.Directoryに無い処理を定義
    /// </summary>
    public static class Directory
    {
        /// <summary>
        /// 既に存在している場合以外は、指定したパスにすべてのディレクトリとサブディレクトリを作成します。
        /// </summary>
        /// <param name="path">作成するディレクトリの子のパス。</param>
        /// <returns>指定したパスに存在するディレクトリを表すオブジェクト。
        /// 指定したパスにおいてディレクトリが既に存在するかどうかにかかわりなく、
        /// このオブジェクトが返されます。</returns>
        public static Original.DirectoryInfo CreateParentDirectory(string path)
        {
            var directory = Original.Path.GetDirectoryName(path);
            return Original.Directory.CreateDirectory(directory);
        }
    }
}
