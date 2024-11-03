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
    /// 標準重力加速度
    /// <para>記号:G</para>
    /// <para>量  :加速度</para>
    /// <para>定義:9.80665 m/s^2</para>
    /// </summary>
    public class StandardGravity<T> : Acceleration<T> where T : INumber<T>
    {
        #region 定数
        #endregion 定数
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "Standard gravity";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "G";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public StandardGravity() { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public StandardGravity(T value) : base(value) { }
        /// <summary>
        /// 他の加速度から初期化
        /// </summary>
        public StandardGravity(SI.MetrePerSecondSquared<T> acceleration) :
            this(acceleration.Value / T.CreateChecked(9.80665m))
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static StandardGravity<T> operator *(T value, StandardGravity<T> quantity)
        {
            return new StandardGravity<T>(value * quantity.Value);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static StandardGravity<T> operator *(StandardGravity<T> quantity, T value)
        {
            return new StandardGravity<T>(quantity.Value * value);
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 明示的な変換を定義します。
        /// </summary>
        public static explicit operator StandardGravity<T>(SI.MetrePerSecondSquared<T> acceleration)
        {
            return new StandardGravity<T>(acceleration);
        }
        #endregion 型変換
    }
}