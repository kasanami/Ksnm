﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksnm.MachineLearning.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork.Tests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void KeepTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(i, Utility.Keep(i));
            }
        }

        [TestMethod()]
        public void DifferentiatedKeepTest()
        {
            for (double i = -2; i <= +2; i++)
            {
                Assert.AreEqual(1, Utility.DifferentiatedKeep(i));
            }
        }

        [TestMethod()]
        public void SigmoidTest()
        {
            Assert.AreEqual(0.0, Utility.Sigmoid(-10), 0.0001);
            Assert.AreEqual(0.5, Utility.Sigmoid(0), 0.0001);
            Assert.AreEqual(1.0, Utility.Sigmoid(+10), 0.0001);
        }

        [TestMethod()]
        public void DifferentiatedSigmoidTest()
        {
            Assert.AreEqual(0.00, Utility.DifferentiatedSigmoid(-10), 0.0001);
            Assert.AreEqual(0.25, Utility.DifferentiatedSigmoid(0), 0.0001);
            Assert.AreEqual(0.00, Utility.DifferentiatedSigmoid(+10), 0.0001);
        }

        [TestMethod()]
        public void ReLUTest()
        {
            Assert.AreEqual(0, Utility.ReLU(-2));
            Assert.AreEqual(0, Utility.ReLU(-1));
            Assert.AreEqual(0, Utility.ReLU(0));
            Assert.AreEqual(1, Utility.ReLU(+1));
            Assert.AreEqual(2, Utility.ReLU(+2));
        }

        [TestMethod()]
        public void DifferentiatedReLUTest()
        {
            Assert.AreEqual(0, Utility.DifferentiatedReLU(-2));
            Assert.AreEqual(0, Utility.DifferentiatedReLU(-1));
            Assert.AreEqual(0, Utility.DifferentiatedReLU(0));
            Assert.AreEqual(1, Utility.DifferentiatedReLU(+1));
            Assert.AreEqual(1, Utility.DifferentiatedReLU(+2));
        }
    }
}