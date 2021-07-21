using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    public interface ILayer
    {
        IReadOnlyList<INeuron> Neurons { get; }
        ILayer Clone(IReadOnlyList<INeuron> inputNeurons);
        #region Set
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(IReadOnlyList<double> values);
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(in double[,] values);
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(in double[,,] values);
        #endregion Set
    }
}