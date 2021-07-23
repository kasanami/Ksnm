/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using System;
using System.Collections.Generic;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 情報元のニューロン　Valueは設定後は変化しない。
    /// </summary>
    public class SourceNeuron : INeuron
    {
        #region プロパティ
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
        /// バイアス
        /// </summary>
        public double Bias { get; set; }
        /// <summary>
        /// 入力無し
        /// </summary>
        public IReadOnlyList<INeuron> InputNeurons { get; private set; } = new SourceNeuron[0];
        /// <summary>
        /// 入力無し
        /// </summary>
        public IList<double> InputWeights { get; private set; } = new double[0];
        /// <summary>
        /// 活性化関数
        /// </summary>
        public Activations.ActivationFunction Activation { get; set; } = null;
        /// <summary>
        /// 活性化関数の微分
        /// </summary>
        public Activations.ActivationFunction DifferentiatedActivation { get; set; } = null;
        #endregion プロパティ

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

        /// <summary>
        /// 複製を作成
        /// </summary>
        public INeuron Clone(IReadOnlyList<INeuron> inputNeurons)
        {
            return new SourceNeuron(this);
        }

        public void ResetWeights(Random random, double weightRange)
        {
            // 何もしない
        }

        public void Randomization(Random random, double weightRange)
        {
            // 何もしない
        }

        public void Randomization(Random random, double expectedValue, double learningRate)
        {
            // 何もしない
        }

        public void Backpropagation(double expectedValue, double learningRate)
        {
            // 何もしない
        }

        public void Backpropagation(double nextDelta, double nextWeight, double learningRate)
        {
            // 何もしない
        }

        public void ForwardPropagation()
        {
            // 何もしない
        }
        #region Object
        public override string ToString()
        {
            return $"{Name}:{Value}";
        }
        #endregion Object
    }
}