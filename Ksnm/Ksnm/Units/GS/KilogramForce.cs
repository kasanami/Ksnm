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

namespace Ksnm.Units.GS
{
    /// <summary>
    /// 重量キログラム
    /// <para>記号:kgf, kgw, kp</para>
    /// <para>系  :MKS重力単位系</para>
    /// <para>量  :力</para>
    /// <para>SI  :9.80665 N</para>
    /// <para>組立:g·kg</para>
    /// <para>定義:1 kg の質量が標準重力加速度下で受ける重力</para>
    /// </summary>
    public class KilogramForce<T> : Force<T> where T : INumber<T>
    {
        #region 定数
        /// <summary>
        /// 標準重力
        /// </summary>
        public static readonly T StandardGravity = T.CreateChecked(9.80665m);
        #endregion 定数
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilogram-force";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "kgf";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public KilogramForce() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public KilogramForce(T value) : base(value) { }
        /// <summary>
        /// ニュートンから変換
        /// </summary>
        public KilogramForce(SI.Newton<T> newton)
        {
            Value = newton.Value * StandardGravity;
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static KilogramForce<T> operator *(T value, KilogramForce<T> quantity)
        {
            return new KilogramForce<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static KilogramForce<T> operator *(KilogramForce<T> quantity, T value)
        {
            return new KilogramForce<T>(quantity.Value * value);
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator KilogramForce<T>(SI.Newton<T> force)
        {
            return new KilogramForce<T>(force);
        }
        #endregion 型変換
    }
}