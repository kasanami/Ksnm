using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ksnm.AI.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void NeuralNetworkTest()
        {
            var nn = new NeuralNetwork(3, 2, 1);
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
            var nn = new NeuralNetwork(3, 2, 1);
            nn.SourceNeurons.ElementAt(0).Bias = 0;
            nn.SourceNeurons.ElementAt(1).Bias = 1;
            nn.SourceNeurons.ElementAt(2).Bias = 2;
            nn.HiddenNeurons.ElementAt(0).Bias = 0;
            nn.HiddenNeurons.ElementAt(1).Bias = 1;
            nn.ResultNeurons.ElementAt(0).Bias = 0;
            nn.Update();
            Assert.AreEqual(7, nn.ResultNeurons.ElementAt(0).Value);
        }
    }
}