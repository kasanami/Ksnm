using Ksnm.Numerics;

namespace Ksnm.Units
{
    /// <summary>
    /// 面積
    /// </summary>
    public class Area<T> : Quantity<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public override string Name => "area";
        /// <summary>
        /// 記号
        /// </summary>
        public override string Symbol { get; protected set; }
        #endregion プロパティ
        #region コンストラクタ
        /// <summary>
        /// ２つの長さから面積を計算する
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Area(Length<T> a, Length<T> b)
        {
            Value = a.Value.Multiply(b.Value);
            if (a.Symbol == b.Symbol)
            {
                Symbol = a.Symbol + "^2";
            }
            else
            {
                Symbol = a.Symbol + "*" + b.Symbol;
            }
        }
        #endregion コンストラクタ
    }
}