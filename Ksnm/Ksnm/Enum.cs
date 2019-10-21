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
namespace Ksnm
{
    /// <summary>
    /// System.Enum に無い機能を定義したクラス
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// 指定した列挙体に含まれている定数の値の配列を取得します。
        /// </summary>
        /// <typeparam name="T">列挙型。</typeparam>
        /// <returns>T に含まれている定数の値を格納した配列。</returns>
        public static T[] GetValues<T>() where T : System.Enum
        {
            return (T[])System.Enum.GetValues(typeof(T));
        }
    }
}
