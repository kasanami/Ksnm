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
    /// ルーメン
    /// </summary>
    public class Lumen<T> : LuminousFlux<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "lumen";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "lm";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Lumen()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Lumen(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Lumen(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Lumen(decimal value) : base(value) { }
        /// <summary>
        /// 光度と角度から光束を計算する
        /// </summary>
        /// <param name="luminousIntensity">光度</param>
        /// <param name="solidAngle">立体角</param>
        public Lumen(LuminousIntensity<T> luminousIntensity, Steradian<T> solidAngle) : base(luminousIntensity, solidAngle)
        {
        }
        #endregion コンストラクタ
        #region 演算子
        /// <summary>
        /// 乗算
        /// </summary>
        public static Lumen<T> operator *(int value, Lumen<T> quantity)
        {
            return new Lumen<T>(quantity.Value.Multiply(value));
        }
        /// <summary>
        /// 乗算
        /// </summary>
        public static Lumen<T> operator *(decimal value, Lumen<T> quantity)
        {
            return new Lumen<T>(quantity.Value.Multiply(value));
        }
        #endregion 演算子
    }
}