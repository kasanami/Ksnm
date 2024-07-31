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
using Ksnm.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// 立法メートル
    /// </summary>
    public class CubicMetre<T> : Volume<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "cubic metre";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "m^3";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public CubicMetre() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public CubicMetre(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public CubicMetre(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public CubicMetre(decimal value) : base(value) { }
        /// <summary>
        /// 3つの長さから体積を計算する
        /// </summary>
        public CubicMetre(Metre<T> length1, Metre<T> length2, Metre<T> length3) : base(length1, length2, length3) { }
        /// <summary>
        /// 面積と長さから体積を計算する
        /// </summary>
        public CubicMetre(SquareMetre<T> area, Metre<T> length) : base(area, length) { }
        /// <summary>
        /// 別の体積から初期化
        /// </summary>
        public CubicMetre(Litre<T> litre) : base(litre.Value.Multiply(0.001m)) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static CubicMetre<T> operator *(int value, CubicMetre<T> quantity)
        {
            return new CubicMetre<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static CubicMetre<T> operator *(decimal value, CubicMetre<T> quantity)
        {
            return new CubicMetre<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator CubicMetre<T>(Litre<T> litre)
        {
            return new CubicMetre<T>(litre);
        }
        #endregion 型変換
    }
}