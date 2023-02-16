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
using System;

namespace Ksnm.Units
{
    /// <summary>
    /// 何らかの量
    /// </summary>
    /// <typeparam name="T">値型</typeparam>
    public abstract class Quantity<T> : IQuantity<T>, IEquatable<Quantity<T>> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public virtual string Name { get; protected set; } = "";
        /// <summary>
        /// 記号
        /// </summary>
        public virtual string Symbol { get; protected set; } = "";
        /// <summary>
        /// 値
        /// </summary>
        public virtual T Value { get; set; }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// 0 で初期化
        /// </summary>
        public Quantity()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Quantity(T value)
        {
            Value = value;
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Quantity(int value)
        {
            Value = Value.From(value);
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Quantity(double value)
        {
            Value = Value.From(value);
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Quantity(decimal value)
        {
            Value = Value.From(value);
        }
        #endregion コンストラクタ

        #region object
        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="obj">現在のオブジェクトと比較するオブジェクト。</param>
        /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object obj)
        {
            if (obj is Quantity<T> quantity)
            {
                return Equals(quantity);
            }
            return false;
        }
        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <returns>現在のオブジェクトを表す文字列。</returns>
        public override string ToString()
        {
            return Value.ToString() + Symbol;
        }
        #endregion object

        #region IEquatable
        /// <summary>
        /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを示します。
        /// </summary>
        /// <param name="other">このオブジェクトと比較するオブジェクト。</param>
        /// <returns>現在のオブジェクトが other パラメーターと等しい場合は true、それ以外の場合は false です。</returns>
        public bool Equals(Quantity<T> other)
        {
            return Value.Equals(other.Value);
        }
        #endregion IEquatable

        #region 型変換
        /// <summary>
        /// T型への明示的な変換を定義します。
        /// </summary>
        public static explicit operator T(Quantity<T> quantity)
        {
            return quantity.Value;
        }
        #endregion 型変換
    }
}