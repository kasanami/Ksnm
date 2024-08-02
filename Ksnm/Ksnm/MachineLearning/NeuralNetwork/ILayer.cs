using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// レイヤー（層）のインターフェイス
    /// </summary>
    public interface ILayer<TValue>
        where TValue : INumber<TValue>, IFloatingPointIeee754<TValue>
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; }
        /// <summary>
        /// ニューロン一覧
        /// </summary>
        IReadOnlyList<INeuron<TValue>> Neurons { get; }
        /// <summary>
        /// 複製を生成する。
        /// </summary>
        /// <param name="inputNeurons"></param>
        /// <returns></returns>
        ILayer<TValue> Clone(IReadOnlyList<INeuron<TValue>> inputNeurons);
        #region Set
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(IReadOnlyList<TValue> values);
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(in TValue[,] values);
        /// <summary>
        /// 値設定
        /// </summary>
        void SetValues(in TValue[,,] values);
        #endregion Set
    }
}