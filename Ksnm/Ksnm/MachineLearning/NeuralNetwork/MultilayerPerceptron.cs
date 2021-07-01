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

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 多層パーセプトロン
    /// </summary>
    public class MultilayerPerceptron
    {
        /// <summary>
        /// 入力値設定
        /// </summary>
        public void SetSourceValue(int index, double value)
        {
            if (index >= SourceNeurons.Count())
            {
                throw new ArgumentOutOfRangeException($"index={index}");
            }
            SourceNeurons[index].Value = value;
        }
        /// <summary>
        /// 入力値設定
        /// </summary>
        public void SetSourceValues(IReadOnlyList<double> values)
        {
            System.Diagnostics.Debug.Assert(SourceNeurons.Count() == values.Count());
            for (int i = 0; i < SourceNeurons.Count(); i++)
            {
                SourceNeurons[i].Value = values[i];
            }
        }
        #region プロパティ
        /// <summary>
        /// 出力値取得
        /// </summary>
        public IEnumerable<double> ResultValues { get => resultNeurons.Select(x => x.Value); }

        /// <summary>
        /// 入力レイヤー
        /// </summary>
        public IReadOnlyList<SourceNeuron> SourceNeurons { get => sourceNeurons; }
        private List<SourceNeuron> sourceNeurons = new List<SourceNeuron>();

        /// <summary>
        /// 中間レイヤー
        /// </summary>
        public IReadOnlyList<Neuron> HiddenNeurons { get => hiddenNeurons; }
        private List<Neuron> hiddenNeurons = new List<Neuron>();

        /// <summary>
        /// 出力レイヤー
        /// </summary>
        public IReadOnlyList<Neuron> ResultNeurons { get => resultNeurons; }
        private List<Neuron> resultNeurons = new List<Neuron>();
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// ニューロン数ゼロで初期化
        /// </summary>
        public MultilayerPerceptron()
        {
        }
        /// <summary>
        /// 各レイヤーを指定したニューロン数で初期化
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int hiddenCount, int resultCount)
        {
            for (int i = 0; i < sourceCount; i++)
            {
                SourceNeuron neuron = new SourceNeuron();
                sourceNeurons.Add(neuron);
            }
            for (int i = 0; i < hiddenCount; i++)
            {
                Neuron neuron = new Neuron(SourceNeurons);
                neuron.Activation = Neuron.Sigmoid;
                hiddenNeurons.Add(neuron);
            }
            for (int i = 0; i < resultCount; i++)
            {
                Neuron neuron = new Neuron(HiddenNeurons);
                neuron.Activation = Neuron.Sigmoid;
                resultNeurons.Add(neuron);
            }
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public MultilayerPerceptron(MultilayerPerceptron source)
        {
            sourceNeurons = new List<SourceNeuron>();
            hiddenNeurons = new List<Neuron>();
            resultNeurons = new List<Neuron>();
            foreach (var item in source.sourceNeurons)
            {
                sourceNeurons.Add(new SourceNeuron(item));
            }
            foreach (var item in source.hiddenNeurons)
            {
                hiddenNeurons.Add(new Neuron(item, sourceNeurons));
            }
            foreach (var item in source.resultNeurons)
            {
                resultNeurons.Add(new Neuron(item, hiddenNeurons));
            }
        }
        #endregion コンストラクタ
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            // ニューロンの値更新
            foreach (var item in hiddenNeurons)
            {
                item.Update();
            }
            foreach (var item in resultNeurons)
            {
                item.Update();
            }
        }

        /// <summary>
        /// 複製を作成
        /// </summary>
        public IEnumerable<MultilayerPerceptron> Clones(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var nn = new MultilayerPerceptron(this);
                nn.Randomization(10);
                yield return nn;
            }
        }

        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        public void ResetWeights(Random random, double weightRange)
        {
            // sourceNeuronsは変更しない

            foreach (var item in hiddenNeurons)
            {
                item.ResetWeights(random, weightRange);
            }
            foreach (var item in resultNeurons)
            {
                item.ResetWeights(random, weightRange);
            }
        }
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        public void ResetWeights(double weightRange)
        {
            Random random = new Random();
            ResetWeights(random, weightRange);
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        public void Randomization(double weightRange)
        {
            foreach (var neuron in hiddenNeurons)
            {
                neuron.Randomization(weightRange);
            }
            foreach (var neuron in resultNeurons)
            {
                neuron.Randomization(weightRange);
            }
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        /// <param name="expectedValues">期待値</param>
        /// <param name="learningRate">学習係数</param>
        public void Randomization(IReadOnlyList<double> expectedValues, double learningRate)
        {
            var count = resultNeurons.Count;
            System.Diagnostics.Debug.Assert(count == expectedValues.Count());
            for (int i = 0; i < count; i++)
            {
                var neuron = resultNeurons[i];
                neuron.Randomization(expectedValues[i], learningRate);
            }
        }
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        /// <param name="expectedValues">期待値</param>
        /// <param name="learningRate">学習係数</param>
        public void Backpropagation(IReadOnlyList<double> expectedValues, double learningRate)
        {
            // 誤差
            var error = Error(expectedValues);
            // 出力層調整
            var count = resultNeurons.Count;
            System.Diagnostics.Debug.Assert(count == expectedValues.Count());
            for (int i = 0; i < count; i++)
            {
                var neuron = resultNeurons[i];
                neuron.Backpropagation(expectedValues[i], learningRate);
            }
        }
        #region Error
        /// <summary>
        /// 現在値と期待値との誤差を計算
        /// </summary>
        /// <param name="expectedValues">期待値</param>
        /// <returns>誤差</returns>
        public double Error(IReadOnlyList<double> expectedValues)
        {
            var count = resultNeurons.Count;
            System.Diagnostics.Debug.Assert(count == expectedValues.Count());
            var errors = 0.0;
            for (int i = 0; i < count; i++)
            {
                var error = expectedValues[i] - resultNeurons[i].Value;
                errors += error * error;
            }
            // 各値の差を2乗→合計→2で割る。
            return errors / 2;
        }
        /// <summary>
        /// 再計算し期待値との誤差を計算
        /// </summary>
        /// <param name="expectedValues">期待値</param>
        /// <returns>誤差</returns>
        public double Error(Sample sample)
        {
            SetSourceValues(sample.SourceValues);
            Update();
            return Error(sample.ResultValues);
        }
        /// <summary>
        /// 現在値と期待値との誤差を計算
        /// </summary>
        /// <param name="samples">期待値を持っているSample</param>
        /// <returns>誤差の合計</returns>
        public double Error(IReadOnlyList<Sample> samples)
        {
            double error = 0;
            foreach (var sample in samples)
            {
                error += Error(sample);
            }
            return error;
        }
        #endregion Error

        #region Learn
        /// <summary>
        /// 学習
        /// </summary>
        public void Learn(Sample sample, double learningRate)
        {
            // SourceNeuronsの値更新
            SetSourceValues(sample.SourceValues);
            // 更新
            Update();
            // バックプロパゲーション
            //Backpropagation(sample.ResultValues, learningRate);
            Randomization(sample.ResultValues, learningRate);
        }
        /// <summary>
        /// 学習
        /// </summary>
        public void Learn(IReadOnlyList<Sample> samples, double learningRate, int tryCount)
        {
            for (int i = 0; i < tryCount; i++)
            {
                foreach (var sample in samples)
                {
                    Learn(sample, learningRate);
                }
            }
        }
        /// <summary>
        /// 学習
        /// </summary>
        public void Learn(IReadOnlyList<Sample> samples, double learningRate)
        {
            foreach (var sample in samples)
            {
                Learn(sample, learningRate);
            }
        }
        /// <summary>
        /// 学習
        /// </summary>
        public static MultilayerPerceptron Learn(MultilayerPerceptron neuralNetwork, IReadOnlyList<Sample> samples, double learningRate)
        {
            int childrenCount = 100;
            // 複製
            var children = neuralNetwork.Clones(childrenCount).ToList();
            // 誤差
            var minErrorIndex = -1;
            double minError = double.MaxValue;
            for (int i = 0; i < childrenCount; i++)
            {
                children[i].Learn(samples, learningRate);
                var error = children[i].Error(samples);
                if (minError > error)
                {
                    minError = error;
                    minErrorIndex = i;
                }
            }
            // 成績が最も良いClone
            return children[minErrorIndex];
        }
        #endregion Learn
    }
}