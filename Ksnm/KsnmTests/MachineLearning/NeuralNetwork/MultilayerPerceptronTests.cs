using Ksnm.MachineLearning.NeuralNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ksnm.MachineLearning.NeuralNetwork.Tests
{
    [TestClass()]
    public class MultilayerPerceptronTests
    {
        [TestMethod()]
        public void NeuralNetworkTest()
        {
            var nn = new MultilayerPerceptron(3, 2, 1);
            Assert.AreEqual(3, nn.SourceNeurons.Count());
            Assert.AreEqual(2, nn.HiddenNeurons.Count());
            Assert.AreEqual(1, nn.ResultNeurons.Count());
            // 前のレイヤーの数をチェック
            foreach (var neuron in nn.SourceNeurons)
            {
                Assert.AreEqual(0, neuron.InputNeurons.Count());
                Assert.AreEqual(0, neuron.InputWeights.Count());
            }
            foreach (var neuron in nn.HiddenNeurons)
            {
                Assert.AreEqual(nn.SourceNeurons.Count(), neuron.InputNeurons.Count());
                Assert.AreEqual(nn.SourceNeurons.Count(), neuron.InputWeights.Count());
            }
            foreach (var neuron in nn.ResultNeurons)
            {
                Assert.AreEqual(nn.HiddenNeurons.Count(), neuron.InputNeurons.Count());
                Assert.AreEqual(nn.HiddenNeurons.Count(), neuron.InputWeights.Count());
            }
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var nn = new MultilayerPerceptron(3, 2, 1);
            nn.SourceNeurons[0].Value = 0;
            nn.SourceNeurons[1].Value = 1;
            nn.SourceNeurons[2].Value = 2;
            nn.HiddenNeurons[0].Activation = Activation.Identity;
            nn.HiddenNeurons[1].Activation = Activation.Identity;
            nn.HiddenNeurons[0].Bias = 0;
            nn.HiddenNeurons[1].Bias = 1;
            nn.ResultNeurons[0].Activation = Activation.Identity;
            nn.ResultNeurons[0].Bias = 0;
            nn.ForwardPropagation();
            Assert.AreEqual(7, nn.ResultNeurons.ElementAt(0).Value);
        }

        [TestMethod()]
        public void CloneTest()
        {
            var nn = new MultilayerPerceptron(1, 1, 1);

            nn.SourceNeurons[0].Name = "SourceNeuron";
            nn.HiddenNeurons[0].Name = "HiddenNeuron";
            nn.ResultNeurons[0].Name = "ResultNeuron";

            var nn2 = new MultilayerPerceptron(nn);

            Assert.AreEqual(nn.SourceNeurons[0].Name, nn2.SourceNeurons[0].Name);
            Assert.AreEqual(nn.HiddenNeurons[0].Name, nn2.HiddenNeurons[0].Name);
            Assert.AreEqual(nn.ResultNeurons[0].Name, nn2.ResultNeurons[0].Name);

            // 元を変更しても、クローンには影響ない
            nn.SourceNeurons[0].Name = "_";
            nn.HiddenNeurons[0].Name = "_";
            nn.ResultNeurons[0].Name = "_";

            Assert.AreEqual("SourceNeuron", nn2.SourceNeurons[0].Name);
            Assert.AreEqual("HiddenNeuron", nn2.HiddenNeurons[0].Name);
            Assert.AreEqual("ResultNeuron", nn2.ResultNeurons[0].Name);
        }

        [TestMethod()]
        public void SetSourceValuesTest()
        {
            var nn = new MultilayerPerceptron(12, 1, 1);

            var values = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            nn.SetSourceValues(values);
            Assert.IsTrue(values.SequenceEqual(nn.SourceValues));
            nn.SetSourceValues(new double[,] { { 0, 1, 2 }, { 3, 4, 5, }, { 6, 7, 8 }, { 9, 10, 11 } });
            Assert.IsTrue(values.SequenceEqual(nn.SourceValues));
            nn.SetSourceValues(new double[,,] { { { 0 }, { 1 }, { 2 } }, { { 3 }, { 4 }, { 5 }, }, { { 6 }, { 7 }, { 8 } }, { { 9 }, { 10 }, { 11 } } });
            Assert.IsTrue(values.SequenceEqual(nn.SourceValues));
        }

        [TestMethod()]
        public void BackPropagationTest()
        {
            var tolerance = 0.000000000000001;

            var multilayerPerceptron = new MultilayerPerceptron(2, 2, 1, Activation.Sigmoid, Activation.Sigmoid);

            multilayerPerceptron.SourceNeurons[0].Value = 0.5;
            multilayerPerceptron.SourceNeurons[1].Value = 0.3;

            multilayerPerceptron.HiddenNeurons[0].Inputs[0].Weight = 0.1;
            multilayerPerceptron.HiddenNeurons[0].Inputs[1].Weight = -0.2;
            multilayerPerceptron.HiddenNeurons[1].Inputs[0].Weight = 0.4;
            multilayerPerceptron.HiddenNeurons[1].Inputs[1].Weight = 0.2;

            multilayerPerceptron.ResultNeurons[0].Inputs[0].Weight = -0.3;
            multilayerPerceptron.ResultNeurons[0].Inputs[1].Weight = 0.5;

            // 目標値
            var target = 0.8;
            // 学習率
            var learningRate = 0.1;

            // 順伝播
            multilayerPerceptron.ForwardPropagation();
            var result = multilayerPerceptron.ResultNeurons[0].Value;
            Assert.AreEqual(0.533218033037244, result, tolerance);

            // 誤差の計算(二乗誤差)
            {
                var error = ((result - target) * (result - target)) / 2;
                Assert.AreEqual(0.0355863089482584, error, tolerance);
            }


            // 各値へのショートカット
            var result0Weight = multilayerPerceptron.ResultNeurons[0].Inputs[0].Weight;
            var result1Weight = multilayerPerceptron.ResultNeurons[0].Inputs[1].Weight;
            var resultDerFunc = multilayerPerceptron.ResultNeurons[0].Activation.DerivativeFunction;
            var hidden0Value = multilayerPerceptron.HiddenNeurons[0].Value;
            var hidden1Value = multilayerPerceptron.HiddenNeurons[1].Value;
            var hidden00Weight = multilayerPerceptron.HiddenNeurons[0].Inputs[0].Weight;
            var hidden01Weight = multilayerPerceptron.HiddenNeurons[0].Inputs[1].Weight;
            var hidden10Weight = multilayerPerceptron.HiddenNeurons[1].Inputs[0].Weight;
            var hidden11Weight = multilayerPerceptron.HiddenNeurons[1].Inputs[1].Weight;
            var hidden0DerFunc = multilayerPerceptron.HiddenNeurons[0].Activation.DerivativeFunction;
            var hidden1DerFunc = multilayerPerceptron.HiddenNeurons[1].Activation.DerivativeFunction;
            var source0Value = multilayerPerceptron.SourceNeurons[0].Value;
            var source1Value = multilayerPerceptron.SourceNeurons[1].Value;

            // 誤差の逆伝播
            {
                // D・・・「∂」は偏微分を示す記号であり、多変数関数の一つの変数に関する微分を表します。
                // Delta・・・「δ」（デルタ）は、ニューラルネットワークや誤差逆伝播（バックプロパゲーション）において、一般的に「誤差項」を表します。

                // 出力層の誤差：
                var errorD = result - target;
                var resultD = resultDerFunc(result);
                var resultDelta = errorD * resultD;
                Assert.AreEqual(-0.266781966962756, errorD, tolerance);
                Assert.AreEqual(+0.248896562281137, resultD, tolerance);
                Assert.AreEqual(-0.066401114455629, resultDelta, tolerance);

                // 出力層の重みの更新
                var result0WeightDelta = resultDelta * hidden0Value;
                var result1WeightDelta = resultDelta * hidden1Value;
                Assert.AreEqual(-0.0330345558250185, result0WeightDelta, tolerance);
                Assert.AreEqual(-0.0374924790378152, result1WeightDelta, tolerance);

                // 隠れ層の各ノードに逆伝播される誤差：
                var hidden0Delta = resultDelta * result0Weight * hidden0DerFunc(hidden0Value);
                var hidden1Delta = resultDelta * result1Weight * hidden1DerFunc(hidden1Value);
                Assert.AreEqual(+0.00497995908415762, hidden0Delta, tolerance);
                Assert.AreEqual(-0.00816143235170022, hidden1Delta, tolerance);

                // 隠れ層の重みに対する勾配
                var hidden00WeightDelta = hidden0Delta * source0Value;
                var hidden01WeightDelta = hidden0Delta * source1Value;
                var hidden10WeightDelta = hidden1Delta * source0Value;
                var hidden11WeightDelta = hidden1Delta * source1Value;
                Assert.AreEqual(+0.00248997954207881, hidden00WeightDelta, tolerance);
                Assert.AreEqual(+0.00149398772524729, hidden01WeightDelta, tolerance);
                Assert.AreEqual(-0.00408071617585011, hidden10WeightDelta, tolerance);
                Assert.AreEqual(-0.00244842970551007, hidden11WeightDelta, tolerance);

                // 重みの更新
                result0Weight = result0Weight - learningRate * result0WeightDelta;
                result1Weight = result1Weight - learningRate * result1WeightDelta;
                Assert.AreEqual(-0.296696544417498, result0Weight, tolerance);
                Assert.AreEqual(+0.503749247903782, result1Weight, tolerance);
                // 隠れ層の重みの更新
                hidden00Weight = hidden00Weight - learningRate * hidden00WeightDelta;
                hidden01Weight = hidden01Weight - learningRate * hidden01WeightDelta;
                hidden10Weight = hidden10Weight - learningRate * hidden10WeightDelta;
                hidden11Weight = hidden11Weight - learningRate * hidden11WeightDelta;
                Assert.AreEqual(+0.099751002045792, hidden00Weight, tolerance);
                Assert.AreEqual(-0.200149398772525, hidden01Weight, tolerance);
                Assert.AreEqual(+0.400408071617585, hidden10Weight, tolerance);
                Assert.AreEqual(+0.200244842970551, hidden11Weight, tolerance);

                // MultilayerPerceptronのバックプロパゲーションのチェック
                {
                    multilayerPerceptron.BackPropagation(new[] { target }, learningRate);

                    // 変わってないはずだが、入力値の確認
                    var actual_source0Value = multilayerPerceptron.SourceNeurons[0].Value;
                    var actual_source1Value = multilayerPerceptron.SourceNeurons[1].Value;
                    Assert.AreEqual(source0Value, actual_source0Value, tolerance);
                    Assert.AreEqual(source1Value, actual_source1Value, tolerance);

                    var actual_result = multilayerPerceptron.ResultNeurons[0].Value;
                    Assert.AreEqual(result, actual_result, tolerance);

                    var actual_resultDelta = multilayerPerceptron.ResultNeurons[0].Delta;
                    Assert.AreEqual(resultDelta, actual_resultDelta, tolerance);

                    var actual_result0Weight = multilayerPerceptron.ResultNeurons[0].Inputs[0].Weight;
                    var actual_result1Weight = multilayerPerceptron.ResultNeurons[0].Inputs[1].Weight;
                    Assert.AreEqual(result0Weight, actual_result0Weight, tolerance);
                    Assert.AreEqual(result1Weight, actual_result1Weight, tolerance);

                    var actual_hidden0Delta = multilayerPerceptron.HiddenNeurons[0].Delta;
                    var actual_hidden1Delta = multilayerPerceptron.HiddenNeurons[1].Delta;
                    Assert.AreEqual(hidden0Delta, actual_hidden0Delta, tolerance);
                    Assert.AreEqual(hidden1Delta, actual_hidden1Delta, tolerance);

                    var actual_hidden00Weight = multilayerPerceptron.HiddenNeurons[0].Inputs[0].Weight;
                    var actual_hidden01Weight = multilayerPerceptron.HiddenNeurons[0].Inputs[1].Weight;
                    var actual_hidden10Weight = multilayerPerceptron.HiddenNeurons[1].Inputs[0].Weight;
                    var actual_hidden11Weight = multilayerPerceptron.HiddenNeurons[1].Inputs[1].Weight;
                    Assert.AreEqual(hidden00Weight, actual_hidden00Weight, tolerance);
                    Assert.AreEqual(hidden01Weight, actual_hidden01Weight, tolerance);
                    Assert.AreEqual(hidden10Weight, actual_hidden10Weight, tolerance);
                    Assert.AreEqual(hidden11Weight, actual_hidden11Weight, tolerance);
                }
            }
            // 順伝播
            multilayerPerceptron.ForwardPropagation();
            result = multilayerPerceptron.ResultNeurons[0].Value;
            //Assert.AreEqual(0.533218033037244, result, tolerance);
            // 誤差の計算(二乗誤差)
            {
                var error = ((result - target) * (result - target)) / 2;
                //Assert.AreEqual(0.0355863089482584, error, tolerance);
            }
        }
    }
}