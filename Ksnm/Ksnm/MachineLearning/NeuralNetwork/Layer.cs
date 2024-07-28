using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Layer<T> : ILayer where T : class, INeuron
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public List<T> neurons { get; private set; } = new List<T>();
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<INeuron> Neurons
        {
            get => neurons.Select(x => (INeuron)x).ToList();
        }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// 名前は空白になる
        /// </summary>
        public Layer()
        {
        }
        /// <summary>
        /// 名前を指定するコンストラクタ
        /// </summary>
        public Layer(string name)
        {
            Name = name;
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
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="inputNeurons"></param>
        public Layer(Layer<T> layer, IReadOnlyList<INeuron> inputNeurons) : this(layer.neurons, inputNeurons)
        {
            Name = layer.Name;
        }
        #endregion コンストラクタ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputNeurons"></param>
        /// <returns></returns>
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

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine(" {");
            str.AppendLine($"  {nameof(Name)}:{Name},");
            str.AppendLine($"  {nameof(neurons)}[{neurons.Count}]=[");
            foreach (var neuron in neurons)
            {
                str.AppendLine($"{neuron},");
            }
            str.AppendLine("  ]");
            str.AppendLine(" }");
            return str.ToString();
        }
        #endregion
    }
}