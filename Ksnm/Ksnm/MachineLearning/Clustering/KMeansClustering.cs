using System.Data;
using System.Diagnostics.Metrics;
using System.Numerics;
using Ksnm.Numerics;

namespace Ksnm.MachineLearning.Clustering
{
    /// <summary>
    /// k平均法
    /// </summary>
    public class KMeansClustering<T>
        where T : INumber<T>, IRootFunctions<T>, IMinMaxValue<T>
    {
        #region フィールド、プロパティ
        List<IData<T>> dataList = new();

        Cluster<T>[] clusters = [];
        public IReadOnlyCollection<Cluster<T>> Clusters => clusters;

        public int ClustersCount => clusters.Length;
        /// <summary>
        /// 更新不要となる許容値
        /// </summary>
        public T Tolerance = T.One;
        #endregion フィールド、プロパティ

        #region コンストラクタ
        public KMeansClustering()
        {
        }
        public KMeansClustering(int clustersCount)
        {
            clusters = new Cluster<T>[clustersCount];
        }
        #endregion コンストラクタ

        #region Data操作
        public void AddData(IData<T> data)
        {
            dataList.Add(data);
        }
        public void AddData(IEnumerable<IData<T>> data)
        {
            dataList.AddRange(data);
        }
        public void ClearData()
        {
            dataList.Clear();
        }
        #endregion Data操作

        #region クラスタリング
        public void Initialize()
        {
            var dimensionCount = dataList[0].Values.Count;
            Numerics.Vector<T> minValues = new(dimensionCount);
            Numerics.Vector<T> maxValues = new(dimensionCount);
            for (int i = 0; i < dimensionCount; i++)
            {
                minValues[i] = T.MaxValue;
                maxValues[i] = T.MinValue;
            }
            foreach (var data in dataList)
            {
                for (int i = 0; i < dimensionCount; i++)
                {
                    if (minValues[i] > data.Values[i])
                    {
                        minValues[i] = data.Values[i];
                    }
                    if (maxValues[i] < data.Values[i])
                    {
                        maxValues[i] = data.Values[i];
                    }
                }
            }
            for (int i = 0; i < ClustersCount; i++)
            {
                clusters[i] = new();
                T t = T.CreateChecked(i) / T.CreateChecked(ClustersCount - 1);
                clusters[i].Location = Numerics.Vector<T>.Lerp(minValues, maxValues, t);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>trueなら更新不要</returns>
        public bool Update()
        {
            // クリア
            foreach (var cluster in clusters)
            {
                cluster.DataList.Clear();
            }
            // 最も距離の近いクラスターに登録
            foreach (var data in dataList)
            {
                var cluster = GetNearCluster(data);
                cluster?.DataList.Add(data);
            }
            // クラスターの位置更新
            T delta = T.Zero;
            foreach (var cluster in clusters)
            {
                delta += cluster.UpdateLocation();
            }
            return delta < Tolerance;
        }

        /// <summary>
        /// 最も距離の近いクラスターを返す
        /// </summary>
        Cluster<T>? GetNearCluster(IData<T> data)
        {
            Cluster<T>? cluster = null;
            T minDistance = T.MaxValue;
            for (int i = 0; i < ClustersCount; i++)
            {
                var distance = Numerics.Vector<T>.DistancePow2(clusters[i].Location, data.Values);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    cluster = clusters[i];
                }
            }
            return cluster;
        }
        #endregion クラスタリング
    }
}