/*
The zlib License

Copyright (c) 2014 Takahiro Kasanami

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

namespace Ksnm.ExtensionMethods.System.Collections.Generic
{
    /// <summary>
    /// Listの拡張メソッド
    /// </summary>
    public static class List_
    {
        /// <summary>
        /// 指定位置から最後までを削除
        /// </summary>
        public static void RemoveRange<T>(this Original.List<T> list, int index)
        {
            list.RemoveRange(index, list.Count - index);
        }
    }
}
