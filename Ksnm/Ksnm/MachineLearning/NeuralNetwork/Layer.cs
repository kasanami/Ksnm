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
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="neurons">複製するニューロン</param>
        /// <param name="inputNeurons">入力ニューロン</param>
        public Layer(IReadOnlyList<T> neurons, IReadOnlyList<INeuron> inputNeurons)
        {
            this.neurons = new List<T>();
            foreach (var item in neurons)
            {
                this.neurons.Add(item.Clone(inputNeurons) as T);
            }
        }
        public Layer(Layer<T> layer, IReadOnlyList<INeuron> inputNeurons) : this(layer.neurons, inputNeurons)
        {
        }
        #endregion コンストラクタ
        public ILayer Clone(IReadOnlyList<INeuron> inputNeurons)
        {
            return new Layer<T>(this, inputNeurons);
        }
        #region 型変換
        #endregion 型変換
    }
}