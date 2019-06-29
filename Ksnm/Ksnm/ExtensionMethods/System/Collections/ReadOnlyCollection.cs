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
using System.Collections.ObjectModel;

namespace Ksnm.ExtensionMethods.System.Collections
{
    /// <summary>
    /// ReadOnlyCollectionの拡張メソッド
    /// </summary>
    public static class ReadOnlyCollection
    {
        /// <summary>
        /// ランダムな位置にある要素を返します。
        /// </summary>
        public static T ElementAtRandom<T>(this ReadOnlyCollection<T> self, global::System.Random random)
        {
            var index = self.IndexOfRandom(random);
            return self[index];
        }
        /// <summary>
        /// ランダムな位置にある要素を返します。
        /// </summary>
        public static T ElementAtRandom<T>(this ReadOnlyCollection<T> self)
        {
            var index = self.IndexOfRandom();
            return self[index];
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int IndexOfRandom<T>(this ReadOnlyCollection<T> self, global::System.Random random)
        {
            return random.Next(self.Count);
        }
        /// <summary>
        /// ランダムなindexを返します。
        /// </summary>
        public static int IndexOfRandom<T>(this ReadOnlyCollection<T> self)
        {
            return self.IndexOfRandom(new global::System.Random());
        }
    }
}