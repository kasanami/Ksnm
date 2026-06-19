using BenchmarkDotNet.Attributes;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.MachineLearning.NeuralNetwork;
using Ksnm.Numerics;
using System.Numerics;
using System.Text;

namespace ConsoleApp
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class MathMesurement
    {
#if true// IsPrime
        [Benchmark]
        public void IsPrimeInt()
        {
            var length = 1000;
            for (int i = 0; i < length; i++)
            {
                Math.IsPrime(i);
            }
        }
        [Benchmark]
        public void IsPrimeIntT()
        {
            var length = 1000;
            for (int i = 0; i < length; i++)
            {
                Math.IsPrime<int>(i);
            }
        }
        [Benchmark]
        public void IsPrimeFloat()
        {
            var length = 1000;
            for (int i = 0; i < length; i++)
            {
                Math.IsPrime((double)i);
            }
        }
        [Benchmark]
        public void IsPrimeFloatT()
        {
            var length = 1000;
            for (int i = 0; i < length; i++)
            {
                Math.IsPrime<double>(i);
            }
        }
#endif
#if false// IsEven
        [Benchmark]
        public void Test_IsEven()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEven(i);
            }
        }
        [Benchmark]
        public void Test_IsEven2()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEven(i);
            }
        }
        [Benchmark]
        public void Test_IsEvenT()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEven<int>(i);
            }
        }
        [Benchmark]
        public void Test_IsEvenT2()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEven<int>(i);
            }
        }
        [Benchmark]
        public void Test_IsEvenBitwise()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEvenBitwise(i);
            }
        }
        [Benchmark]
        public void Test_IsEvenBitwise2()
        {
            var length = 100000000;
            for (int i = 0; i < length; i++)
            {
                Math.IsEvenBitwise(i);
            }
        }
#endif
    }
}