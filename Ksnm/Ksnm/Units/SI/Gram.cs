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
    /// グラム
    /// <para>記号:g</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:基本単位の分量単位</para>
    /// <para>量  :質量</para>
    /// <para>定義:10^−3 kg</para>
    /// </summary>
    public class Gram<T> : Mass<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "gram";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "g";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Gram()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Gram(T value)
        {
            Value = value;
        }
        /// <summary>
        /// キログラムから変換
        /// </summary>
        public Gram(Kilogram<T> kilogram)
        {
            Value = kilogram.Value.Multiply(1000m);
        }
        #endregion コンストラクタ
        #region 演算子
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Gram<T>(Kilogram<T> kilogram)
        {
            return new Gram<T>(kilogram);
        }
        #endregion 型変換
    }
}