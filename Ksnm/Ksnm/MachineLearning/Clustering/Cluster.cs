using System.Numerics;

namespace Ksnm.MachineLearning.Clustering
{
    /// <summary>
    /// クラスター
    /// </summary>
    public class Cluster<T> where T : INumber<T>
    {
        #region フィールド、プロパティ
        public List<IData<T>> DataList = new();
        public IEnumerable<Numerics.Vector<T>> DataValues => DataList.Select(item => item.Values);
        /// <summary>
        /// 空間内の位置
        /// </summary>
        public Numerics.Vector<T> Location = new();
        #endregion フィールド、プロパティ

        public Cluster()
        {
        }
        public Cluster(int dimensionCount)
        {
            Location = new(dimensionCount);
        }
        /// <summary>
        /// 位置更新
        /// </summary>
        /// <returns>変化量の2乗</returns>
        public T UpdateLocation()
        {
            var newLocation = Numerics.Vector<T>.Sum(DataValues);
            newLocation /= T.CreateChecked(DataList.Count);
            var delta = Location - newLocation;
            Location = newLocation;
            return delta.MagnitudePow2;
        }
    }
}