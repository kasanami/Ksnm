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
namespace Ksnm.Numerics
{
    /// <summary>
    /// 整数のインターフェイス
    /// </summary>
    public interface IInteger<T>
    {
        #region プロパティ
        /// <summary>
        /// 2 の累乗かどうかを示します。
        /// 2 の累乗の場合は true。それ以外の場合は false。
        /// </summary>
        bool IsPowerOfTwo { get; }
        /// <summary>
        /// 偶数かどうかを示します。
        /// 偶数の場合は true。それ以外の場合は false。
        /// </summary>
        bool IsEven { get; }
        /// <summary>
        /// 奇数かどうかを示します。
        /// 奇数の場合は true。それ以外の場合は false。
        /// </summary>
        bool IsOdd { get; }
        /// <summary>
        /// 素数ならtrueを返す。
        /// </summary>
        bool IsPrime { get; }
        #endregion プロパティ

        #region メソッド
        /// <summary>
        /// 最大公約数を求めます。
        /// </summary>
        /// <returns>this と right の最大公約数。</returns>
        T GreatestCommonDivisor(T right);
        #endregion メソッド
    }
}
