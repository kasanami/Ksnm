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
    /// キログラム
    /// <para>記号:kg</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:基本単位</para>
    /// <para>量  :質量</para>
    /// </summary>
    public class Kilogram<T> : Mass<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "kilogram";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "kg";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Kilogram()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Kilogram(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Kilogram(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Kilogram(decimal value) : base(value) { }
        /// <summary>
        /// エネルギーと光速から質量を計算する
        /// </summary>
        public Kilogram(Joule<T> energy) : base(energy, MetrePerSecond<T>.SpeedOfLight)
        {
        }
        /// <summary>
        /// グラムから変換
        /// </summary>
        public Kilogram(Gram<T> gram)
        {
            Value = gram.Value.Divide(1000m);
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 質量と加速度から力を計算する
        /// </summary>
        public static Newton<T> operator *(Kilogram<T> mass, MetrePerSecondSquared<T> acceleration)
        {
            return new Newton<T>(mass, acceleration);
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Kilogram<T> operator *(int value, Kilogram<T> quantity)
        {
            return new Kilogram<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Kilogram<T> operator *(decimal value, Kilogram<T> quantity)
        {
            return new Kilogram<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
        #region 型変換
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Kilogram<T>(Joule<T> mass)
        {
            return new Kilogram<T>(mass);
        }
        /// <summary>
        /// 他の型から、この型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator Kilogram<T>(Gram<T> gram)
        {
            return new Kilogram<T>(gram);
        }
        #endregion 型変換
    }
}