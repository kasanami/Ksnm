﻿namespace Ksnm.Units
{
    /// <summary>
    /// 何らかの量
    /// </summary>
    /// <typeparam name="T">値型</typeparam>
    public class Quantity<T> : IQuantity<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name => "";
        /// <summary>
        /// 記号
        /// </summary>
        public string Symbol => "";
        /// <summary>
        /// 名前
        /// </summary>
        public T Value { get; protected set; }
        #endregion プロパティ
    }
}