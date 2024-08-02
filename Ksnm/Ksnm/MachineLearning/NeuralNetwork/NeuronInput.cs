
using System.Numerics;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 入力情報
    /// </summary>
    public class NeuronInput<TValue>
        where TValue : INumber<TValue>, IFloatingPointIeee754<TValue>
    {
        /// <summary>
        /// 結合先のニューロン
        /// </summary>
        public INeuron<TValue> Neuron;
        /// <summary>
        /// 結合強度
        /// </summary>
        public TValue Weight;


        public TValue Value
        {
            get
            {
                return Neuron.Value * Weight;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NeuronInput(INeuron<TValue> neuron, TValue weight)
        {
            Neuron = neuron;
            Weight = weight;
        }
    }
}
