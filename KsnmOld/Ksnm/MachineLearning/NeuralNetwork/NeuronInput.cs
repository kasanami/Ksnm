
namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 入力情報
    /// </summary>
    public class NeuronInput
    {
        /// <summary>
        /// 結合先のニューロン
        /// </summary>
        public INeuron Neuron;
        /// <summary>
        /// 結合強度
        /// </summary>
        public double Weight;


        public double Value
        {
            get
            {
                return Neuron.Value * Weight;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public NeuronInput(INeuron neuron, double weight)
        {
            Neuron = neuron;
            Weight = weight;
        }
    }
}
