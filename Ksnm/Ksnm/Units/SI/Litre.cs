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
using System.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// リットル
    /// <para>記号:L, l</para>
    /// <para>度量衡:メートル法</para>
    /// <para>系  :DKS単位系</para>
    /// <para>種類:組立単位・SI併用単位</para>
    /// <para>量  :体積</para>
    /// <para>SI  :10^−3 m^3</para>
    /// <para>定義:1 dm^3</para>
    /// </summary>
    public class Litre<T> : Volume<T> where T : INumber<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "litre";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "L";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Litre() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Litre(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Litre(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Litre(decimal value) : base(value) { }
        /// <summary>
        /// 3つの長さから体積を計算する
        /// </summary>
        public Litre(Metre<T> length1, Metre<T> length2, Metre<T> length3)
        {
            Value = length1.Value * length2.Value * length3.Value * T.CreateChecked(1000);
        }
        /// <summary>
        /// 面積と長さから体積を計算する
        /// </summary>
        public Litre(SquareMetre<T> area, Metre<T> length)
        {
            Value = area.Value * length.Value * T.CreateChecked(1000);
        }
        /// <summary>
        /// 別の体積から初期化
        /// </summary>
        public Litre(CubicMetre<T> cubicMetre) : base(cubicMetre.Value * T.CreateChecked(1000)) { }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Litre<T> operator *(T value, Litre<T> quantity)
        {
            return new Litre<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Litre<T> operator *(Litre<T> quantity, T value)
        {
            return new Litre<T>(quantity.Value * value);
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator Litre<T>(CubicMetre<T> cubicMetre)
        {
            return new Litre<T>(cubicMetre);
        }
        #endregion 型変換
    }
}