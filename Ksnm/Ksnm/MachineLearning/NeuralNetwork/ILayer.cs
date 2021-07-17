using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    public interface ILayer
    {
        IReadOnlyList<INeuron> Neurons { get; }
        ILayer Clone();
    }
}