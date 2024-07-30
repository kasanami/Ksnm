using System;
using System.Collections.Generic;

namespace Ksnm.ExtensionMethods.System.Collections.Generic.IReadOnlyList
{
    /// <summary>
    /// Listの拡張メソッド
    /// </summary>
    public static class IReadOnlyListExtensions
    {
        #region IndexOf
        /// <summary>
        /// 最大の要素のインデックスを返す。
        /// 要素数が0なら-1を返す。
        /// </summary>
        public static int IndexOf<T>(this IReadOnlyList<T> list, T item) where T : IEquatable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion IndexOf
    }
}
