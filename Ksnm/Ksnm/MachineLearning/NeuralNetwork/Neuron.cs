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
using System.Text;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
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
        /// 入力情報
        /// </summary>
        private List<NeuronInput> inputs = new List<NeuronInput>();
        /// <summary>
        /// 入力情報
        /// </summary>
        public IReadOnlyList<NeuronInput> Inputs => inputs;
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        public IEnumerable<INeuron> InputNeurons => inputs.Select(item => item.Neuron);
        /// <summary>
        /// 入力の重み
        /// </summary>
        public IEnumerable<double> InputWeights => inputs.Select(item => item.Weight);
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
        /// ※inputNeuronsは、インスタンスを明確にするため指定する必要がある。source.Inputsは使用されない。
        /// </summary>
        public Neuron(Neuron source, IReadOnlyList<INeuron> inputNeurons)
        {
            Name = source.Name;
            Value = source.Value;
            for (int i = 0; i < inputNeurons.Count; i++)
            {
                inputs.Add(new NeuronInput(inputNeurons[i], source.Inputs[i].Weight));
            }
            Activation = source.Activation;
        }
        /// <summary>
        /// 入力ニューロンを指定して初期化
        /// </summary>
        public Neuron(IReadOnlyList<INeuron> inputNeurons)
        {
            inputs = new List<NeuronInput>();
            foreach (var neuron in inputNeurons)
            {
                var input = new NeuronInput(neuron, 1.0);
                inputs.Add(input);
            }
        }
        #endregion コンストラクタ

        #region Input
        public NeuronInput FindInput(INeuron neuron)
        {
            var index = InputIndexOf(neuron);
            if (index < 0)
            {
                return null;
            }
            return inputs[index];
        }
        /// <summary>
        /// 指定したニューロンを入力に持っていれば、そのインデックスを返す。
        /// 持っていなければ-1を返す。
        /// </summary>
        public int InputIndexOf(INeuron neuron)
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (ReferenceEquals(Inputs[i].Neuron, neuron))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion Input

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
            double sum = Inputs.Sum(input => input.Value);
            Value = Activation.Function(sum + Bias);
        }
        /// <summary>
        /// 重みを指定した値に設定
        /// </summary>
        public void ResetWeights(double weight)
        {
            foreach (var input in Inputs)
            {
                input.Weight = weight;
            }
            Bias = weight;
        }
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        public void ResetWeights(Random random, double weightRange)
        {
            foreach (var input in Inputs)
            {
                input.Weight = random.Range(-weightRange, +weightRange);
            }
            Bias = random.Range(-weightRange, +weightRange);
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        public void Randomization(Random random, double weightRange)
        {
            // 重みを調整
            foreach (var input in Inputs)
            {
                input.Weight += random.Range(-weightRange, +weightRange);
            }
            // バイアスを調整
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
            foreach (var input in Inputs)
            {
                input.Weight += random.NextDouble() * input.Value * delta * learningRate;
            }

            // バイアスを修正
            Bias += random.NextDouble() * delta * learningRate;

            // 前の層へ
            foreach (var input in Inputs)
            {
                var neuron = input.Neuron;
                var weight = input.Weight;
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
                var input = beforeNeuron.FindInput(this);
                if (input != null)
                {
                    Delta *= beforeNeuron.Delta * input.Weight;
                }
            }
            Delta *= Activation.DerivativeFunction(Value);
        }
        public void BackPropagationWeight(double learningRate)
        {
            foreach (var input in Inputs)
            {
                input.Weight -= learningRate * (Delta * input.Neuron.Value);
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
            var str = new StringBuilder();
            str.AppendLine("  {");
            str.AppendLine($"   {nameof(Name)}:{Name},");
            str.AppendLine($"   {nameof(Value)}:{Value},");
            str.AppendLine($"   {nameof(Delta)}:{Delta},");
            str.AppendLine($"   {nameof(InputWeights)}:[{InputWeights.ToJoinedString(",")}],");
            str.AppendLine($"   {nameof(Bias)}:{Bias},");
            str.AppendLine($"   {nameof(Activation)}:{Activation.Name}");
            str.AppendLine("  }");
            return str.ToString();
        }
        #endregion Object
    }
}