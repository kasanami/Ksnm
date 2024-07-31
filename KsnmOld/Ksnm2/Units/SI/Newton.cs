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
    /// ニュートン
    /// <para>記号:N</para>
    /// <para>系  :国際単位系 (SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :力</para>
    /// <para>定義:1kgの質量を持つ物体に1m/s^2の加速度を生じさせる力</para>
    /// </summary>
    public class Newton<T> : Force<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "newton";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "N";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Newton()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Newton(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Newton(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Newton(decimal value) : base(value) { }
        /// <summary>
        /// 質量と加速度から力を計算する
        /// </summary>
        public Newton(Kilogram<T> mass, MetrePerSecondSquared<T> acceleration) : base(mass, acceleration)
        {
        }
        /// <summary>
        /// 重量キログラムから変換
        /// </summary>
        public Newton(GS.KilogramForce<T> kgf)
        {
            Value = kgf.Value.Divide(GS.KilogramForce<T>.StandardGravity);
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 力と面積から圧力を計算する
        /// </summary>
        public static Pascal<T> operator /(Newton<T> force, SquareMetre<T> area)
        {
            return new Pascal<T>(force, area);
        }
        /// <summary>
        /// 力と距離からエネルギーを計算する
        /// </summary>
        public static Joule<T> operator *(Newton<T> force, Metre<T> length)
        {
            return new Joule<T>(force, length);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Newton<T> operator *(int value, Newton<T> quantity)
        {
            return new Newton<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Newton<T> operator *(decimal value, Newton<T> quantity)
        {
            return new Newton<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Newton<T>(GS.KilogramForce<T> force)
        {
            return new Newton<T>(force);
        }
        #endregion 型変換
    }
}