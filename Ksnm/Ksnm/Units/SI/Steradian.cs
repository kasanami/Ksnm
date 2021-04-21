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
    /// ステラジアン
    /// <para>記号:sr</para>
    /// <para>系  :国際単位系(SI)</para>
    /// <para>種類:組立単位</para>
    /// <para>量  :立体角</para>
    /// <para>組立:m^2/m^2</para>
    /// </summary>
    public class Steradian<T> : SolidAngle<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "steradian";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol => "sr";
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Steradian()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Steradian(T value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Steradian(int value) : base(value) { }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Steradian(decimal value) : base(value) { }
        /// <summary>
        /// 半径と表面積から角度を計算する
        /// </summary>
        /// <param name="radius">半径</param>
        /// <param name="area">表面積</param>
        public Steradian(Metre<T> radius, SquareMetre<T> area)
        {
            Value = area.Value.Divide(radius.Value.Multiply(radius.Value));
        }
        #endregion コンストラクタ
    }
}