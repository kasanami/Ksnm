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
namespace Ksnm.Units
{
    /// <summary>
    /// 何らかの量
    /// </summary>
    /// <typeparam name="T">値型</typeparam>
    public abstract class Quantity<T> : IQuantity<T>
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
        public Quantity()
        {
        }
        public Quantity(T value)
        {
            Value = value;
        }
        #endregion コンストラクタ
        #region object
        public override string ToString()
        {
            return Value.ToString() + Symbol;
        }
        #endregion object
    }
}