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
        #region Set
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetValues(IReadOnlyList<double> values)
        {
            System.Diagnostics.Debug.Assert(neurons.Count() == values.Count());
            for (int i = 0; i < neurons.Count(); i++)
            {
                neurons[i].Value = values[i];
            }
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetValues(in double[,] values)
        {
            int index = 0;
            var length0 = values.GetLength(0);
            var length1 = values.GetLength(1);
            System.Diagnostics.Debug.Assert(neurons.Count() == (length0 * length1));
            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    neurons[index].Value = values[i, j];
                    index++;
                }
            }
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetValues(in double[,,] values)
        {
            int index = 0;
            var length0 = values.GetLength(0);
            var length1 = values.GetLength(1);
            var length2 = values.GetLength(2);
            System.Diagnostics.Debug.Assert(neurons.Count() == (length0 * length1 * length2));
            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    for (int k = 0; k < length2; k++)
                    {
                        neurons[index].Value = values[i, j, k];
                        index++;
                    }
                }
            }
        }
        #endregion Set
    }
}