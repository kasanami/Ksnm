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
    public class ActivationsTests
    {
        [TestMethod()]
        public void IdentityTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(i, Activations.Identity(i));
            }
        }

        [TestMethod()]
        public void DerIdentityTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(1, Activations.DerIdentity(i));
            }
        }

        [TestMethod()]
        public void SigmoidTest()
        {
            Assert.AreEqual(0.0, Activations.Sigmoid(-10), 0.0001);
            Assert.AreEqual(0.5, Activations.Sigmoid(0), 0.0001);
            Assert.AreEqual(1.0, Activations.Sigmoid(+10), 0.0001);
        }

        [TestMethod()]
        public void DerSigmoidTest()
        {
            Assert.AreEqual(0.00, Activations.DerSigmoid(Activations.Sigmoid(-10)), 0.0001);
            Assert.AreEqual(0.25, Activations.DerSigmoid(Activations.Sigmoid(0)), 0.0001);
            Assert.AreEqual(0.00, Activations.DerSigmoid(Activations.Sigmoid(+10)), 0.0001);
        }

        [TestMethod()]
        public void ReLUTest()
        {
            Assert.AreEqual(0, Activations.ReLU(-2));
            Assert.AreEqual(0, Activations.ReLU(-1));
            Assert.AreEqual(0, Activations.ReLU(0));
            Assert.AreEqual(1, Activations.ReLU(+1));
            Assert.AreEqual(2, Activations.ReLU(+2));
        }

        [TestMethod()]
        public void DerReLUTest()
        {
            Assert.AreEqual(0, Activations.DerReLU(-2));
            Assert.AreEqual(0, Activations.DerReLU(-1));
            Assert.AreEqual(0, Activations.DerReLU(0));
            Assert.AreEqual(1, Activations.DerReLU(+1));
            Assert.AreEqual(1, Activations.DerReLU(+2));
        }

        [TestMethod()]
        public void TanhTest()
        {
            Assert.AreEqual(-0.9950547, Activations.Tanh(-3), 0.0000001);
            Assert.AreEqual(-0.7615942, Activations.Tanh(-1), 0.0000001);
            Assert.AreEqual(+0.0000000, Activations.Tanh(+0), 0.0000001);
            Assert.AreEqual(+0.7615942, Activations.Tanh(+1), 0.0000001);
            Assert.AreEqual(+0.9950547, Activations.Tanh(+3), 0.0000001);
        }

        [TestMethod()]
        public void DerTanhTest()
        {
            Assert.AreEqual(0.0098, Activations.DerTanh(-0.9950547), 0.0001);
            Assert.AreEqual(0.4199, Activations.DerTanh(-0.7615942), 0.0001);
            Assert.AreEqual(1.0000, Activations.DerTanh(+0.0000000), 0.0001);
            Assert.AreEqual(0.4199, Activations.DerTanh(+0.7615942), 0.0001);
            Assert.AreEqual(0.0098, Activations.DerTanh(+0.9950547), 0.0001);
        }
    }
}