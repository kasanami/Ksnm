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
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.ExtensionMethods.System.Collections.Generic.List;
using Ksnm.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 多層パーセプトロン
    /// </summary>
    public class MultilayerPerceptron
    {
        #region Set
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
            Layers[0].SetValues(values);
        }
        /// <summary>
        /// 入力値設定
        /// </summary>
        public void SetSourceValues(in double[,] values)
        {
            Layers[0].SetValues(values);
        }
        /// <summary>
        /// 入力値設定
        /// </summary>
        public void SetSourceValues(in double[,,] values)
        {
            Layers[0].SetValues(values);
        }
        #endregion Set
        #region プロパティ

        /// <summary>
        /// 擬似乱数ジェネレーター
        /// </summary>
        public Random Random = new Random();

        /// <summary>
        /// 入力値取得※取得のみ
        /// </summary>
        public IEnumerable<double> SourceValues { get => SourceNeurons.Select(x => x.Value); }
        /// <summary>
        /// 出力値取得
        /// </summary>
        public IEnumerable<double> ResultValues { get => ResultNeurons.Select(x => x.Value); }

        /// <summary>
        /// 入力レイヤーのニューロン
        /// </summary>
        public IReadOnlyList<SourceNeuron> SourceNeurons { get => Layers[0].Neurons.Select(x => x as SourceNeuron).ToList(); }

        /// <summary>
        /// 中間レイヤー（1番目）のニューロン
        /// </summary>
        public IReadOnlyList<Neuron> HiddenNeurons
        {
            get
            {
                if (Layers.Count < 3) { return null; }
                return Layers[1].Neurons.Select(x => x as Neuron).ToList();
            }
        }

        /// <summary>
        /// 中間レイヤー（2番目）のニューロン
        /// </summary>
        public IReadOnlyList<Neuron> Hidden2Neurons
        {
            get
            {
                if (Layers.Count < 4) { return null; }
                return Layers[2].Neurons.Select(x => x as Neuron).ToList();
            }
        }

        /// <summary>
        /// 中間レイヤー
        /// </summary>
        public IEnumerable<ILayer> HiddenLayers
        {
            get
            {
                for (int i = 1; i < Layers.Count-1; i++)
                {
                    yield return Layers[i];
                }
            }
        }
        /// <summary>
        /// 出力レイヤーのニューロン
        /// </summary>
        public IReadOnlyList<Neuron> ResultNeurons { get => Layers[Layers.Count - 1].Neurons.Select(x => x as Neuron).ToList(); }

        /// <summary>
        /// 入力レイヤー
        /// </summary>
        public ILayer SourceLayer => Layers[0];
        /// <summary>
        /// 出力レイヤー
        /// </summary>
        public ILayer ResultLayer => Layers[Layers.Count - 1];

        /// <summary>
        /// レイヤー一覧
        /// </summary>
        public IReadOnlyList<ILayer> Layers { get => layers; }
        /// <summary>
        /// レイヤー一覧
        /// </summary>
        protected List<ILayer> layers = new List<ILayer>();

        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// ニューロン数ゼロで初期化
        /// </summary>
        public MultilayerPerceptron()
        {
        }
        /// <summary>
        /// 2層のニューラルネットワーク
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int resultCount, Activation resultActivation)
        {
            ILayer beforeLayer = null;
            // 1層目
            {
                var layer = new Layer<SourceNeuron>("Source");
                for (int i = 0; i < sourceCount; i++)
                {
                    SourceNeuron neuron = new SourceNeuron();
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 2層目
            {
                var layer = new Layer<Neuron>("Result");
                layer.Activation = resultActivation;
                for (int i = 0; i < resultCount; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = resultActivation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
            }
        }
        /// <summary>
        /// 各レイヤーを指定したニューロン数で初期化
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int hiddenCount, int resultCount, Activation hiddenActivation, Activation resultActivation)
        {
            ILayer beforeLayer = null;
            // 1層目
            {
                var layer = new Layer<SourceNeuron>("Source");
                for (int i = 0; i < sourceCount; i++)
                {
                    SourceNeuron neuron = new SourceNeuron();
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 2層目
            {
                var layer = new Layer<Neuron>("Hidden");
                layer.Activation = hiddenActivation;
                for (int i = 0; i < hiddenCount; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = hiddenActivation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 3層目
            {
                var layer = new Layer<Neuron>("Result");
                layer.Activation = resultActivation;
                for (int i = 0; i < resultCount; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = resultActivation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
            }
        }
        /// <summary>
        /// 各レイヤーを指定したニューロン数で初期化
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int hidden1Count, int hidden2Count, int resultCount, Activation hidden1Activation, Activation hidden2Activation, Activation resultActivation)
        {
            ILayer beforeLayer = null;
            // 1層目
            {
                var layer = new Layer<SourceNeuron>("Source");
                for (int i = 0; i < sourceCount; i++)
                {
                    SourceNeuron neuron = new SourceNeuron();
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 2層目
            {
                var layer = new Layer<Neuron>("Hidden");
                layer.Activation = hidden1Activation;
                for (int i = 0; i < hidden1Count; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = hidden1Activation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 3層目
            {
                var layer = new Layer<Neuron>("Hidden2");
                layer.Activation = hidden2Activation;
                for (int i = 0; i < hidden2Count; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = hidden2Activation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
                beforeLayer = layer;
            }
            // 4層目
            {
                var layer = new Layer<Neuron>("Result");
                layer.Activation = resultActivation;
                for (int i = 0; i < resultCount; i++)
                {
                    Neuron neuron = new Neuron(beforeLayer.Neurons);
                    neuron.Activation = resultActivation;
                    layer.neurons.Add(neuron);
                }
                layers.Add(layer);
            }
        }
        /// <summary>
        /// 2層のニューラルネットワーク
        /// 2層目の活性化関数はシグモイド関数
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int resultCount) :
            this(sourceCount, resultCount, Activation.Sigmoid)
        {
        }
        /// <summary>
        /// 3層のニューラルネットワーク
        /// 2層目～3層目の活性化関数はシグモイド関数
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int hiddenCount, int resultCount) :
            this(sourceCount, hiddenCount, resultCount, Activation.Sigmoid, Activation.Sigmoid)
        {
        }
        /// <summary>
        /// 4層のニューラルネットワーク
        /// 2層目～4層目の活性化関数はシグモイド関数
        /// </summary>
        public MultilayerPerceptron(int sourceCount, int hidden1Count, int hidden2Count, int resultCount) :
            this(sourceCount, hidden1Count, hidden2Count, resultCount, Activation.Sigmoid, Activation.Sigmoid, Activation.Sigmoid)
        {
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public MultilayerPerceptron(MultilayerPerceptron source)
        {
            IReadOnlyList<INeuron> inputNeurons = null;
            Random = source.Random;
            foreach (var layer in source.layers)
            {
                var newLayer = layer.Clone(inputNeurons);
                layers.Add(newLayer);
                inputNeurons = newLayer.Neurons;
            }
        }
        #endregion コンストラクタ

        #region 学習
        #region ForwardPropagation
        /// <summary>
        /// フォワードプロパゲーション
        /// </summary>
        public void ForwardPropagation()
        {
            foreach (var layer in layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.ForwardPropagation();
                }
            }
        }
        /// <summary>
        /// フォワードプロパゲーション
        /// </summary>
        public IEnumerable<double> ForwardPropagation(IReadOnlyList<double> inputValues)
        {
            SetSourceValues(inputValues);
            ForwardPropagation();
            return ResultValues;
        }
        #endregion ForwardPropagation

        #region BackPropagation
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        /// <param name="targetValues">目標値</param>
        /// <param name="learningRate">学習係数</param>
        public void BackPropagation(IReadOnlyList<double> targetValues, double learningRate)
        {
            // D・・・「∂」は偏微分を示す記号であり、多変数関数の一つの変数に関する微分を表します。
            // Delta・・・「δ」（デルタ）は、ニューラルネットワークや誤差逆伝播（バックプロパゲーション）において、一般的に「誤差項」を表します。

            // 隠れ層の各ノードに逆伝播される誤差更新
            // 最後のレイヤー（入力層）は更新しない
            ILayer beforeLayer = null;
            for (var li = Layers.Count - 1; li >= 1; li--)
            {
                var layer = Layers[li];
                var neurons = layer.Neurons;
                for (var i = 0; i < neurons.Count; i++)
                {
                    var neuron = neurons[i];
                    if (beforeLayer != null)
                    {
                        neuron.BackPropagationDelta(beforeLayer);
                    }
                    else
                    {
                        neuron.BackPropagationDelta(targetValues[i]);
                    }
                }
                beforeLayer = layer;
            }

            // 重みの更新
            foreach (var layer in Layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.BackPropagationWeight(learningRate);
                }
            }
        }
        #endregion BackPropagation

        /// <summary>
        /// 複製を作成
        /// </summary>
        public IEnumerable<MultilayerPerceptron> Clones(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var nn = new MultilayerPerceptron(this);
                yield return nn;
            }
        }

        #region 設定
        /// <summary>
        /// 重みを指定した値に設定
        /// </summary>
        public void ResetWeights(double weight)
        {
            foreach (var layer in layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.ResetWeights(weight);
                }
            }
        }
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        public void ResetWeightsWithRandom(double weightRange)
        {
            foreach (var layer in layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.ResetWeights(Random, weightRange);
                }
            }
        }
        /// <summary>
        /// 重みをランダムに調整
        /// </summary>
        public void Randomization(double weightRange)
        {
            foreach (var layer in layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.Randomization(Random, weightRange);
                }
            }
        }
        /// <summary>
        /// 乱数による調整
        /// </summary>
        /// <param name="expectedValues">期待値</param>
        /// <param name="learningRate">学習係数</param>
        public void Randomization(IReadOnlyList<double> expectedValues, double learningRate)
        {
            var count = ResultNeurons.Count;
            System.Diagnostics.Debug.Assert(count == expectedValues.Count());
            for (int i = 0; i < count; i++)
            {
                var neuron = ResultNeurons[i];
                neuron.Randomization(Random, expectedValues[i], learningRate);
            }
        }
        /// <summary>
        /// 出力レイヤーの活性化関数を設定
        /// </summary>
        /// <param name="activation"></param>
        public void SetResultActivation(Activation activation)
        {
            foreach (var neuron in ResultNeurons)
            {
                neuron.Activation = activation;
            }
        }
        #endregion 設定

        #region Error
        /// <summary>
        /// 現在値と期待値との誤差を計算
        /// 各値の差を2乗→合計→2で割る。
        /// </summary>
        /// <param name="targetValues">目標値</param>
        /// <returns>誤差</returns>
        public double Error(IReadOnlyList<double> targetValues)
        {
            var count = ResultNeurons.Count;
            System.Diagnostics.Debug.Assert(count == targetValues.Count());
            var errors = 0.0;
            for (int i = 0; i < count; i++)
            {
                var error = ResultNeurons[i].Value - targetValues[i];
                errors += error * error;
            }
            return errors / 2;
        }
        /// <summary>
        /// 再計算し期待値との誤差を計算
        /// </summary>
        /// <param name="sample">サンプル</param>
        /// <returns>誤差</returns>
        public double ErrorRecalculate(Sample sample)
        {
            SetSourceValues(sample.SourceValues);
            ForwardPropagation();
            return Error(sample.ResultValues);
        }
        /// <summary>
        /// 現在値と期待値との誤差を計算
        /// </summary>
        /// <param name="samples">期待値を持っているSample</param>
        /// <returns>誤差の合計</returns>
        public double ErrorRecalculate(IReadOnlyList<Sample> samples)
        {
            double error = 0;
            foreach (var sample in samples)
            {
                error += ErrorRecalculate(sample);
            }
            return error;
        }
        #endregion Error

        #region Learn
        /// <summary>
        /// LearnByGeneticAlgorithmのパラメータ
        /// </summary>
        public class GeneticAlgorithmParam
        {
            /// <summary>
            /// 学習係数
            /// </summary>
            public double learningRate = 1;
            /// <summary>
            /// 学習サンプル
            /// </summary>
            public IReadOnlyList<Sample> samples;
            /// <summary>
            /// 学習回数
            /// </summary>
            public int tryCount = 100;
            /// <summary>
            /// 作成するクローンの数
            /// </summary>
            public int cloneCount = 100;
            /// <summary>
            /// クローンのWeightを変更する範囲の初期値
            /// </summary>
            public double cloneWeightRangeStart = 0;
            /// <summary>
            /// クローンのWeightを変更する範囲の増加量
            /// </summary>
            public double cloneWeightRangeDelta = 0.1;
            /// <summary>
            /// 
            /// </summary>
            public GeneticAlgorithmParam()
            {
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var str = new StringBuilder(512);
                str.AppendLine("{");
                str.AppendLine($"{nameof(learningRate)}={learningRate}");
                str.AppendLine($"{nameof(samples)}     ={samples.Count}");
                str.AppendLine($"{nameof(tryCount)}    ={tryCount}");
                str.AppendLine($"{nameof(cloneCount)}  ={cloneCount}");
                str.AppendLine($"{nameof(cloneWeightRangeStart)}={cloneWeightRangeStart}");
                str.AppendLine($"{nameof(cloneWeightRangeDelta)}={cloneWeightRangeDelta}");
                str.AppendLine("}");
                return str.ToString();
            }
        }
        /// <summary>
        /// 学習
        /// </summary>
        public void Learn(Sample sample, double learningRate)
        {
            // SourceNeuronsの値更新
            SetSourceValues(sample.SourceValues);
            // 更新
            ForwardPropagation();
            // バックプロパゲーション
            BackPropagation(sample.ResultValues, learningRate);
            //Randomization(sample.ResultValues, learningRate);
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
        public void Learn(IReadOnlyList<Sample> samples, double learningRate, int tryCount)
        {
            for (int i = 0; i < tryCount; i++)
            {
                Learn(samples, learningRate);
            }
        }
        /// <summary>
        /// 学習
        /// </summary>
        public static MultilayerPerceptron LearnByGeneticAlgorithm(MultilayerPerceptron neuralNetwork, Sample sample, double learningRate)
        {
            var learnParam = new GeneticAlgorithmParam();
            learnParam.samples = new[] { sample };
            learnParam.learningRate = learningRate;
            return LearnByGeneticAlgorithm(neuralNetwork, learnParam);
        }
        /// <summary>
        /// 学習
        /// </summary>
        public static MultilayerPerceptron LearnByGeneticAlgorithm(MultilayerPerceptron neuralNetwork, GeneticAlgorithmParam learnParam)
        {
            // 複製
            var children = neuralNetwork.Clones(learnParam.cloneCount).ToList();
            // 設定変更
            double weightRange = learnParam.cloneWeightRangeStart;
            foreach (var child in children)
            {
                child.Randomization(weightRange);
                weightRange += learnParam.cloneWeightRangeDelta;
            }
            // 誤差
            var errors = new double[children.Count];
#if true// 並列処理
            Parallel.For(0, children.Count, i =>
#else
            for (int i = 0; i < children.Count; i++)
#endif
            {
                children[i].Learn(learnParam.samples, learnParam.learningRate, learnParam.tryCount);
                errors[i] = children[i].ErrorRecalculate(learnParam.samples);
            }
#if true// 並列処理
            );
#endif
            // 成績が最も良いCloneを返す
            var minErrorIndex = errors.IndexOfMin();
            return children[minErrorIndex];
        }
        #endregion Learn
        #endregion 学習

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = new StringBuilder(256);
            str.AppendLine("{");
            for (int i = 0; i < Layers.Count; i++)
            {
                str.AppendLine($"{nameof(Layers)}[{i}]={Layers[i]}");
            }
            str.AppendLine("}");
            return str.ToString();
        }
    }
}