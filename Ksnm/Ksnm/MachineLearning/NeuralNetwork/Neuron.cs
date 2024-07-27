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
using System.Linq;
using Ksnm.ExtensionMethods.System.Random;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// ニューロン
    /// </summary>
    public class Neuron : INeuron
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 現在の値
        /// ForwardPropagation()で更新される。
        /// 入力ニューロンの場合は、これに入力値を設定する。
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 現在の勾配
        /// BackPropagation()で更新される。
        /// </summary>
        public double Delta { get; set; }
        /// <summary>
        /// バイアス
        /// </summary>
        public double Bias { get; set; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        public IReadOnlyList<INeuron> InputNeurons { get; private set; } = new SourceNeuron[0];
        /// <summary>
        /// 重み
        /// </summary>
        public IList<double> InputWeights { get; private set; } = new double[0];
        /// <summary>
        /// 活性化関数
        /// </summary>
        public Activation Activation { get; set; } = Activation.Identity;

        #region コンストラクタ
        /// <summary>
        /// 入力ニューロン無しで初期化
        /// </summary>
        public Neuron()
        {
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public Neuron(Neuron source)
        {
            Name = source.Name;
            Value = source.Value;
            Activation = source.Activation;
        }
        /// <summary>
        /// コピーコンストラクタ
        /// ※inputNeuronsは、インスタンスを明確にするため指定する必要がある。source.inputNeuronsは使用されない。
        /// </summary>
        public Neuron(Neuron source, IReadOnlyList<INeuron> inputNeurons) : this(inputNeurons)
        {
            Name = source.Name;
            Value = source.Value;
            InputWeights = new List<double>(source.InputWeights);
            Activation = source.Activation;
        }
        /// <summary>
        /// 入力ニューロンを指定して初期化
        /// </summary>
        public Neuron(IReadOnlyList<INeuron> inputNeurons)
        {
            InputNeurons = inputNeurons;
            InputWeights = new double[inputNeurons.Count()];
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] = 1;
            }
        }
        #endregion コンストラクタ

        #region その他
        /// <summary>
        /// 指定したニューロンを入力に持っていれば、そのインデックスを返す。
        /// 持っていなければ-1を返す。
        /// </summary>
        public int InputIndexOf(INeuron neuron)
        {
            for (int i = 0; i < InputNeurons.Count; i++)
            {
                if (ReferenceEquals(InputNeurons[i], neuron))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion その他

        /// <summary>
        /// 複製を作成
        /// </summary>
        public INeuron Clone(IReadOnlyList<INeuron> inputNeurons)
        {
            return new Neuron(this, inputNeurons);
        }

        #region 学習
        /// <summary>
        /// Valueを更新
        /// </summary>
        public void ForwardPropagation()
        {
            double sum = 0.0;
            System.Diagnostics.Debug.Assert(InputNeurons.Count() == InputWeights.Count());
            var count = InputNeurons.Count();
            for (int i = 0; i < count; i++)
            {
                sum += InputNeurons[i].Value * InputWeights[i];
            }
            Value = Activation.Function(sum + Bias);
        }
        /// <summary>
        /// 重みを指定した値に設定
        /// </summary>
        public void ResetWeights(double weight)
        {
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] = weight;
            }
            Bias = weight;
        }
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        public void ResetWeights(Random random, double weightRange)
        {
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] = random.Range(-weightRange, +weightRange);
            }
            Bias = random.Range(-weightRange, +weightRange);
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        public void Randomization(Random random, double weightRange)
        {
            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] += random.Range(-weightRange, +weightRange);
            }
            // バイアスを修正
            Bias += random.Range(-weightRange, +weightRange);
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        /// <param name="random"></param>
        /// <param name="targetValue">目標値</param>
        /// <param name="learningRate">学習係数</param>
        public void Randomization(Random random, double targetValue, double learningRate)
        {
            // 誤差
            // 期待値＞出力値なら+値　期待値＜出力値なら-値が得られる
            var delta = (targetValue - Value);

            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] += random.NextDouble() * InputNeurons[i].Value * delta * learningRate;
            }

            // バイアスを修正
            Bias += random.NextDouble() * delta * learningRate;

            // 前の層へ
            var count = InputNeurons.Count;
            for (int i = 0; i < count; i++)
            {
                var neuron = InputNeurons[i];
                var weight = InputWeights[i];
                neuron.Randomization(random, neuron.Value + delta, learningRate * weight);
            }
        }

        /// <summary>
        /// バックプロパゲーション(出力層)
        /// </summary>
        /// <param name="targetValue">目標値</param>
        public void BackPropagationDelta(double targetValue)
        {
            // 出力層の誤差
            Delta = (Value - targetValue) * Activation.DerivativeFunction(Value);
        }
        /// <summary>
        /// バックプロパゲーション(中間層)
        /// </summary>
        /// <param name="beforeLayer">前の層（中間層のときは、出力層を渡す）</param>
        public void BackPropagationDelta(ILayer beforeLayer)
        {
            Delta = 1;
            foreach (var beforeNeuron in beforeLayer.Neurons)
            {
                var index = beforeNeuron.InputIndexOf(this);
                if (index >= 0)
                {
                    Delta *= beforeNeuron.Delta * beforeNeuron.InputWeights[index];
                }
            }
            Delta *= Activation.DerivativeFunction(Value);
        }
        public void BackPropagationWeight(double learningRate)
        {
            var inputNeurons = InputNeurons;
            for (var j = 0; j < inputNeurons.Count; j++)
            {
                InputWeights[j] -= learningRate * (Delta * inputNeurons[j].Value);
            }
            Bias -= learningRate * Delta;
        }
        #endregion 学習

        #region Object
        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            return $"{Name}:{Value}";
        }
        #endregion Object
    }
}