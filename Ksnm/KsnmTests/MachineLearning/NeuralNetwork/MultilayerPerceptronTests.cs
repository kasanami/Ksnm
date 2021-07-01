using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            nn.HiddenNeurons[0].Activation = Neuron.Keep;
            nn.HiddenNeurons[1].Activation = Neuron.Keep;
            nn.HiddenNeurons[0].Bias = 0;
            nn.HiddenNeurons[1].Bias = 1;
            nn.ResultNeurons[0].Activation = Neuron.Keep;
            nn.ResultNeurons[0].Bias = 0;
            nn.Update();
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
    }
}