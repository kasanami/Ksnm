using System.Numerics;

namespace Ksnm.MachineLearning.Clustering
{
    public interface IData<T> where T : INumber<T>
    {
        public Numerics.Vector<T> Values { get; }

        public int DimensionCount => Values.Count;
    }
}