/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

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
namespace Ksnm.ExtensionMethods.System.Enum
{
    /// <summary>
    /// Enumの拡張メソッド
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 指定したフラグを全て持っていたらtrueを返す。
        /// </summary>
        public static bool All<T>(this T self, params T[] flags) where T : global::System.Enum
        {
            foreach (var flag in flags)
            {
                if (self.HasFlag(flag) == false)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 指定したフラグを1つ持っていたらtrueを返す。
        /// </summary>
        public static bool Any<T>(this T self, params T[] flags) where T : global::System.Enum
        {
            foreach (var flag in flags)
            {
                if (self.HasFlag(flag))
                {
                    return true;
                }
            }
            return false;
        }
    }
}