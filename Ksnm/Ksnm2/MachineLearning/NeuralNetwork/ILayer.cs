using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// レイヤー（層）のインターフェイス
    /// </summary>
    public interface ILayer
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; }
        /// <summary>
        /// ニューロン一覧
        /// </summary>
        IReadOnlyList<INeuron> Neurons { get; }
        /// <summary>
        /// 複製を生成する。
        /// </summary>
        /// <param name="inputNeurons"></param>
        /// <returns></returns>
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