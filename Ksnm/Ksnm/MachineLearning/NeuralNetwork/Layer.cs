using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    public class Layer<T> : ILayer where T : class, INeuron
    {
        public List<T> neurons { get; private set; } = new List<T>();
        public IReadOnlyList<INeuron> Neurons
        {
            get => neurons.Select(x => (INeuron)x).ToList();
        }
        #region コンストラクタ
        public Layer()
        {
        }
        public Layer(IReadOnlyList<T> neurons)
        {
            this.neurons = new List<T>();
            foreach (var item in neurons)
            {
                this.neurons.Add(item.Clone() as T);
            }
        }
        public Layer(Layer<T> layer) : this(layer.neurons)
        {
        }
        #endregion コンストラクタ
        public ILayer Clone()
        {
            return new Layer<T>(this);
        }
        #region 型変換
        public static implicit operator Layer<INeuron>(Layer<T> layer)
        {
            return new Layer<INeuron>(layer.Neurons);
        }
        #endregion 型変換
    }
}