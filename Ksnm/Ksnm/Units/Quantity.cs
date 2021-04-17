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
        /// 名前
        /// </summary>
        public virtual T Value { get; protected set; }
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