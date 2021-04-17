using Ksnm.Numerics;

namespace Ksnm.Units
{
    /// <summary>
    /// 面積
    /// </summary>
    public class Area<T> : Quantity<T> where T : IMath<T>
    {
        #region コンストラクタ
        /// <summary>
        /// ２つの長さから面積を計算する
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Area(Length<T> a, Length<T> b)
        {
            Value = a.Value.Multiply(b.Value);
        }
        #endregion コンストラクタ
    }
}