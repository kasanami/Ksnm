using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.AI.Tests
{
    [TestClass()]
    public class NeuronTests
    {
        [TestMethod()]
        public void NeuronTest()
        {
            var inputNeuron = new Neuron();
            var neuron = new Neuron(new[] { inputNeuron, inputNeuron });

            Assert.AreEqual(2, neuron.InputNeurons.Count());
            Assert.AreEqual(2, neuron.InputWeights.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var inputNeuron = new Neuron();
            var neuron = new Neuron(new[] { inputNeuron });
            neuron.Update();
            Assert.AreEqual(0, neuron.Value);

            inputNeuron.Value = 123;
            neuron.Update();
            Assert.AreEqual(123, neuron.Value);

            neuron.InputWeights[0] = 0;
            neuron.Update();
            Assert.AreEqual(0, neuron.Value);

            neuron.Bias = 1;
            neuron.Update();
            Assert.AreEqual(1, neuron.Value);
        }
    }
}