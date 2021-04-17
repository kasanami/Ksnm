namespace Ksnm.Units
{
    /// <summary>
    /// 何らかの量を表す。
    /// </summary>
    /// <typeparam name="T">量を表す値の型</typeparam>
    interface IQuantity<T>
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 記号
        /// </summary>
        string Symbol { get; }
        /// <summary>
        /// 値
        /// </summary>
        T Value { get; }
    }
}