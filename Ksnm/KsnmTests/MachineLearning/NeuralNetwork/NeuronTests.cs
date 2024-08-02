using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.MachineLearning.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork.Tests
{
    [TestClass()]
    public class NeuronTests
    {
        [TestMethod()]
        public void NeuronTest()
        {
            var inputNeuron = new Neuron<double>();
            var neuron = new Neuron<double>(new[] { inputNeuron, inputNeuron });

            Assert.AreEqual(2, neuron.InputNeurons.Count());
            Assert.AreEqual(2, neuron.InputWeights.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var inputNeuron = new Neuron<double>();
            var neuron = new Neuron<double>(new[] { inputNeuron });
            neuron.ForwardPropagation();
            Assert.AreEqual(0, neuron.Value);

            inputNeuron.Value = 123;
            neuron.ForwardPropagation();
            Assert.AreEqual(123, neuron.Value);

            neuron.Inputs[0].Weight = 0;
            neuron.ForwardPropagation();
            Assert.AreEqual(0, neuron.Value);

            neuron.Bias = 1;
            neuron.ForwardPropagation();
            Assert.AreEqual(1, neuron.Value);
        }

        [TestMethod()]
        public void CloneTest()
        {
            var neuron1 = new Neuron<double>();
            var neuron2 = new Neuron<double>(new[] { neuron1 });

            neuron1.Name = "neuron1";
            neuron2.Name = "neuron2";
            neuron2.Inputs[0].Weight = 123;

            var neuron1Clone = new Neuron<double>(neuron1);
            var neuron2Clone = new Neuron<double>(neuron2, new[] { neuron1Clone });

            Assert.AreEqual(neuron1.Name, neuron1Clone.Name);
            Assert.AreEqual(neuron2.Name, neuron2Clone.Name);
            Assert.AreEqual(neuron2.Inputs[0].Weight, neuron2Clone.Inputs[0].Weight);

            // 元を変更しても、クローンには影響ない
            neuron1.Name = "NEURON1";
            neuron2.Name = "NEURON2";
            neuron2.Inputs[0].Weight = 456;
            Assert.AreEqual("neuron1", neuron1Clone.Name);
            Assert.AreEqual("neuron2", neuron2Clone.Name);
            Assert.AreEqual(123, neuron2Clone.Inputs[0].Weight);
        }
    }
}