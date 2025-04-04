using BenchmarkDotNet.Attributes;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.MachineLearning.NeuralNetwork;
using System.Numerics;
using System.Text;

namespace ConsoleApp
{
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class AIMesurement
    {
        [Benchmark]
        public void TestLogicGate_Float64()
        {
            TestLogicGate<Float64>();
        }
        [Benchmark]
        public void TestLogicGate_Float32()
        {
            TestLogicGate<Float32>();
        }
        [Benchmark]
        public void TestLogicGate_Float16()
        {
            TestLogicGate<Float16>();
        }
        [Benchmark]
        public void TestLogicGate_BFloat16()
        {
            TestLogicGate<BFloat16>();
        }
        [Benchmark]
        public void TestLogicGate_Fixed64()
        {
           TestLogicGate<Fixed64>();
        }
        //[Benchmark]
        //public void TestLogicGate_Float8()
        //{
        //    TestLogicGate<Float8>();
        //}
        public static void TestLogicGate<T>()
            where T : INumber<T>, IFloatingPointIeee754<T>, IMinMaxValue<T>
        {
            Activation<T> activation = Activation<T>.Sigmoid;
            var _0 = T.Zero;
            var _1 = T.One;
            var _2 = T.CreateChecked(2);
            var _4 = T.CreateChecked(4);
            var _5 = T.CreateChecked(5);
            var _8 = T.CreateChecked(8);
            var _10 = T.CreateChecked(10);
            var _12 = T.CreateChecked(12);
            var _15 = T.CreateChecked(15);
            if (true)
            {
                #region NOTゲート
                var nn = new MultilayerPerceptron<T>(1, 1, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var NotSample = new List<Sample<T>>
                {
                    new ([_1],[_0]),
                    new ([_0],[_1]),
                };
                _LogicGate(nn, NotSample, _1, true);
                #endregion NOTゲート
            }
            if (true)
            {
                #region ORゲート
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var OrSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_1]),
                };
                _LogicGate(nn, OrSample, _1, true);
                #endregion ORゲート
            }
            if (true)
            {
                #region NORゲート
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var OrSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_1]),
                    new Sample<T>([_0, _1],[_0]),
                    new Sample<T>([_1, _0],[_0]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, OrSample, _1, true);
                #endregion NORゲート
            }
            if (true)
            {
                #region ANDゲート
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var AndSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_0]),
                    new Sample<T>([_1, _0],[_0]),
                    new Sample<T>([_1, _1],[_1]),
                };
                _LogicGate(nn, AndSample, _1, true);
                #endregion ANDゲート
            }
            if (true)
            {
                #region NANDゲート
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var AndSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_1]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, AndSample, _1, true);
                #endregion NANDゲート
            }
            if (true)
            {
                #region XORゲート
                var nn = new MultilayerPerceptron<T>(2, 2, 1, activation, activation);
                nn.Random = new Random(123456789);
                // 目標値
                var XorSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, XorSample, _1, true);
                #endregion XORゲート
            }
        }
        static void _LogicGate<T>(MultilayerPerceptron<T> nn, IReadOnlyList<Sample<T>> samples, T learningRate, bool isResetWeights)
            where T : INumber<T>, IFloatingPointIeee754<T>, IMinMaxValue<T>
        {
            var _2 = T.CreateChecked(2);
            // 最初の重みはランダム
            if (isResetWeights)
            {
                nn.ResetWeightsWithRandom(_2);
            }

            for (int i = 0; i < 10000; i++)
            {
                var errorSum = T.Zero;
                foreach (var sample in samples)
                {
                    var targets = sample.ResultValues;
                    nn.SetSourceValues(sample.SourceValues);
                    // 順伝播
                    nn.ForwardPropagation();
                    var result = nn.ResultNeurons[0].Value;
                    // 誤差の計算(二乗誤差)
                    var error = nn.Error(targets);
                    if (error > T.Zero)
                    {
                        nn.BackPropagation(targets, learningRate);
                    }
                    errorSum += error;
                }
                if (errorSum == T.Zero)
                {
                    break;
                }
            }
        }

    }
}
