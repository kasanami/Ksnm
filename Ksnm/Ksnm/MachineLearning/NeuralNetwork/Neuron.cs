﻿/*
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
using static Ksnm.MachineLearning.NeuralNetwork.Utility;

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
        /// Update()で更新される。
        /// 入力ニューロンの場合は、これに入力値を設定する。
        /// </summary>
        public double Value { get; set; }
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
        public Utility.ActivationFunction Activation { get; set; } = Keep;
        /// <summary>
        /// 活性化関数の微分
        /// </summary>
        public Utility.ActivationFunction DifferentiatedActivation { get; set; } = DifferentiatedKeep;

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
            DifferentiatedActivation = source.DifferentiatedActivation;
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
            DifferentiatedActivation = source.DifferentiatedActivation;
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

        /// <summary>
        /// 複製を作成
        /// </summary>
        public INeuron Clone()
        {
            return new Neuron(this);
        }

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
            Value = Activation(sum + Bias);
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
        /// <param name="expectedValue">期待値</param>
        /// <param name="learningRate">学習係数</param>
        public void Randomization(Random random, double expectedValue, double learningRate)
        {
            // 誤差
            // 期待値＞出力値なら+値　期待値＜出力値なら-値が得られる
            var delta = (expectedValue - Value);

            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                InputWeights[i] += random.NextDouble() * delta * learningRate;
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
        /// バックプロパゲーション
        /// </summary>
        /// <param name="expectedValue">期待値</param>
        /// <param name="learningRate">学習係数</param>
        public void Backpropagation(double expectedValue, double learningRate)
        {
            var error = (expectedValue - Value);
            error *= error;
            error /= 2;

            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                // 修正量 = () * 学習係数
                // 期待値＞出力値なら+値　期待値＜出力値なら-値が得られる
                InputWeights[i] += (expectedValue - Value) * error * learningRate;
            }
            // バイアスを修正
            Bias += (expectedValue - Value) * error * learningRate;
#if false
            // δ = (期待値 - 出力値) × 出力の微分
            var delta = (expectedValue - Value) * Value * (1.0 - Value);
            // 現在の重みを保存
            var oldInputWeights = new double[InputWeights.Count];
            InputWeights.CopyTo(oldInputWeights, 0);
            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                // 修正量 = δ × 中間層の値 × 学習係数
                InputWeights[i] += delta * InputNeurons[i].Value * learningRate;
            }
            Bias += delta * Bias * learningRate;

            // 前の層へ
            var count = InputNeurons.Count;
            for (int i = 0; i < count; i++)
            {
                var neuron = InputNeurons[i];
                neuron.Backpropagation(neuron.Value, learningRate, delta, oldInputWeights[i]);
            }
#endif
        }
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        /// <param name="expectedValue">期待値</param>
        /// <param name="learningRate">学習係数</param>
        /// <param name="nextWeight">次の層の重み（出力層から中間層のこの関数を使用するときに、出力層が持っていた重みを渡す）</param>
        public void Backpropagation(double expectedValue, double learningRate, double nextDelta, double nextWeight)
        {
#if false
            //// δ = (期待値 - 出力値) ×  × 出力の微分
            //var delta = (expectedValue - Value) * Value * (1.0 - Value);
            // δ = 前のδ × 前の重み × 出力の微分
            var delta = nextDelta * nextWeight * Value * (1.0 - Value);
            // 現在の重みを保存
            var oldInputWeights = new double[InputWeights.Count];
            InputWeights.CopyTo(oldInputWeights, 0);
            // 重みを修正
            for (int i = 0; i < InputWeights.Count; i++)
            {
                // 修正量 = δ × 中間層の値 × 学習係数
                InputWeights[i] += delta * InputNeurons[i].Value * learningRate;
            }
            Bias += delta * Bias * learningRate;

            // 前の層へ
            var count = InputNeurons.Count;
            for (int i = 0; i < count; i++)
            {
                var neuron = InputNeurons[i];
                neuron.Backpropagation(neuron.Value, learningRate, delta, oldInputWeights[i]);
            }
#endif
        }
        #region Object
        public override string ToString()
        {
            return $"{Name}:{Value}";
        }
        #endregion Object
    }
}