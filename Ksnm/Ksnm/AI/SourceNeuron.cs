using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.AI
{
    /// <summary>
    /// 情報元のニューロン　Valueは設定後は変化しない。
    /// </summary>
    public class SourceNeuron : INeuron
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 現在の値
        /// Update()で更新されない。
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 入力無し
        /// </summary>
        public IReadOnlyList<INeuron> InputNeurons { get; private set; } = new SourceNeuron[0];
        /// <summary>
        /// 入力無し
        /// </summary>
        public IList<double> InputWeights { get; private set; } = new double[0];

        /// <summary>
        /// デフォルト値で初期化
        /// </summary>
        public SourceNeuron()
        {
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public SourceNeuron(SourceNeuron source)
        {
            Name = source.Name;
            Value = source.Value;
        }

        public void Randomization(double expectedValue, double learningRate)
        {
            // 何もしない
        }

        public void Backpropagation(double expectedValue, double learningRate)
        {
            // 何もしない
        }

        public void Backpropagation(double expectedValue, double learningRate, double nextDelta, double nextWeight)
        {
            // 何もしない
        }
    }
}