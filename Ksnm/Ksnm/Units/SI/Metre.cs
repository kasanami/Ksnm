using Ksnm.Numerics;

namespace Ksnm.Units.SI
{
    /// <summary>
    /// メートル
    /// </summary>
    public class Metre<T> : Length<T> where T : IMath<T>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public new string Name => "metre";
        /// <summary>
        /// 記号
        /// </summary>
        public new string Symbol => "m";
        #endregion プロパティ
        #region コンストラクタ
        public Metre()
        {
        }
        public Metre(T value)
        {
            Value = value;
        }
        #endregion コンストラクタ
    }
}