using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.AI
{
    public interface INeuron
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 計算結果値
        /// </summary>
        double Value { get; set; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IReadOnlyList<INeuron> InputNeurons { get; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IList<double> InputWeights { get; }
        /// <summary>
        /// 
        /// </summary>
        void Randomization(double expectedValue, double learningRate);
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        void Backpropagation(double expectedValue, double learningRate);
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        void Backpropagation(double expectedValue, double learningRate, double nextDelta, double nextWeight);
    }
}