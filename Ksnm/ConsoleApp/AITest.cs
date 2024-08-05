using Ksnm.MachineLearning.NeuralNetwork;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using System.Numerics;
using System;

#pragma warning disable CS0162 // 到達できないコードが検出されました
namespace ConsoleApp
{
    internal class AITest
    {
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            TestLogicGate<double>();
            //TestLogicGate<float>();
            //TestLogicGate<Half>();
        }
        static void TestLogicGate<T>()
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
                Console.WriteLine("NOTゲート");
                var nn = new MultilayerPerceptron<T>(1, 1, activation);
                // 目標値
                var NotSample = new List<Sample<T>>
                {
                    new ([_1],[_0]),
                    new ([_0],[_1]),
                };
                _LogicGate(nn, NotSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.ResultNeurons[0].Inputs[0].Weight = -_10;
                    nn.ResultNeurons[0].Bias = _5;
                    _LogicGate(nn, NotSample, _1, false);
                }
                #endregion NOTゲート
            }
            if (true)
            {
                #region ORゲート
                Console.WriteLine("ORゲート");
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                // 目標値
                var OrSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_1]),
                };
                _LogicGate(nn, OrSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.ResultNeurons[0].Inputs[0].Weight = _10;
                    nn.ResultNeurons[0].Inputs[1].Weight = _10;
                    nn.ResultNeurons[0].Bias = -_5;
                    _LogicGate(nn, OrSample, _1, false);
                }
                #endregion ORゲート
            }
            if (true)
            {
                #region NORゲート
                Console.WriteLine("NORゲート");
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                // 目標値
                var OrSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_1]),
                    new Sample<T>([_0, _1],[_0]),
                    new Sample<T>([_1, _0],[_0]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, OrSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.ResultNeurons[0].Inputs[0].Weight = -_8;
                    nn.ResultNeurons[0].Inputs[1].Weight = -_8;
                    nn.ResultNeurons[0].Bias = _5;
                    _LogicGate(nn, OrSample, _1, false);
                }
                #endregion NORゲート
            }
            if (true)
            {
                #region ANDゲート
                Console.WriteLine("ANDゲート");
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                // 目標値
                var AndSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_0]),
                    new Sample<T>([_1, _0],[_0]),
                    new Sample<T>([_1, _1],[_1]),
                };
                _LogicGate(nn, AndSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.ResultNeurons[0].Inputs[0].Weight = _10;
                    nn.ResultNeurons[0].Inputs[1].Weight = _10;
                    nn.ResultNeurons[0].Bias = -_15;
                    _LogicGate(nn, AndSample, _1, false);
                }
                #endregion ANDゲート
            }
            if (true)
            {
                #region NANDゲート
                Console.WriteLine("NANDゲート");
                var nn = new MultilayerPerceptron<T>(2, 1, activation);
                // 目標値
                var AndSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_1]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, AndSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.ResultNeurons[0].Inputs[0].Weight = -_8;
                    nn.ResultNeurons[0].Inputs[1].Weight = -_8;
                    nn.ResultNeurons[0].Bias = _12;
                    _LogicGate(nn, AndSample, _1, false);
                }
                #endregion NANDゲート
            }
            if (true)
            {
                #region XORゲート
                Console.WriteLine("XORゲート");
                var nn = new MultilayerPerceptron<T>(2, 2, 1, activation, activation);
                // 目標値
                var XorSample = new List<Sample<T>>
                {
                    new Sample<T>([_0, _0],[_0]),
                    new Sample<T>([_0, _1],[_1]),
                    new Sample<T>([_1, _0],[_1]),
                    new Sample<T>([_1, _1],[_0]),
                };
                _LogicGate(nn, XorSample, _1, true);
                // はじめからほぼ正解を設定
                if (true)
                {
                    Console.WriteLine("ほぼ正解を手動設定");
                    nn.HiddenNeurons[0].Inputs[0].Weight = +_5;
                    nn.HiddenNeurons[0].Inputs[1].Weight = -_5;
                    nn.HiddenNeurons[0].Bias = _5;
                    nn.HiddenNeurons[1].Inputs[0].Weight = -_5;
                    nn.HiddenNeurons[1].Inputs[1].Weight = +_5;
                    nn.HiddenNeurons[1].Bias = _5;
                    nn.ResultNeurons[0].Inputs[0].Weight = -_10;
                    nn.ResultNeurons[0].Inputs[1].Weight = -_10;
                    nn.ResultNeurons[0].Bias = _15;
                    _LogicGate(nn, XorSample, _1, false);
                }
                #endregion XORゲート
            }
        }
        /// <summary>
        /// 数字認識
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static void TestNumericRecognition<T>()
            where T : INumber<T>, IFloatingPointIeee754<T>, IMinMaxValue<T>
        {
            Activation<T> activation = Activation<T>.Sigmoid;
            var _0 = T.Zero;
            var _1 = T.One;
            var _2 = T.CreateChecked(2);
            var _10 = T.CreateChecked(10);

            if (true)
            {
                #region 数字認識
                var random = new Random();
                var samples = new List<Sample<T>>
                {
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _1,
                                        _1, _0, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [ _1, _0, _0, _0, _0, _0, _0, _0, _0, _0 ] },
                    new Sample<T>() {
                        SourceValues = [_0, _1, _0,
                                        _0, _1, _0,
                                        _0, _1, _0,
                                        _0, _1, _0,
                                        _0, _1, _0,],
                        ResultValues = [ _0, _1, _0, _0, _0, _0, _0, _0, _0, _0 ] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _0, _0, _1,
                                        _1, _1, _1,
                                        _1, _0, _0,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _1, _0, _0, _0, _0, _0, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _0, _0, _1,
                                        _1, _1, _1,
                                        _0, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _0, _1, _0, _0, _0, _0, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _0, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,
                                        _0, _0, _1,
                                        _0, _0, _1,],
                        ResultValues = [_0, _0, _0, _0, _1, _0, _0, _0, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _0,
                                        _1, _1, _1,
                                        _0, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _0, _0, _0, _1, _0, _0, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _0,
                                        _1, _1, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _0, _0, _0, _0, _1, _0, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _1,
                                        _0, _0, _1,
                                        _0, _0, _1,
                                        _0, _0, _1,],
                        ResultValues = [_0, _0, _0, _0, _0, _0, _0, _1, _0, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _0, _0, _0, _0, _0, _0, _1, _0] },
                    new Sample<T>() {
                        SourceValues = [_1, _1, _1,
                                        _1, _0, _1,
                                        _1, _1, _1,
                                        _0, _0, _1,
                                        _1, _1, _1,],
                        ResultValues = [_0, _0, _0, _0, _0, _0, _0, _0, _0, _1] },
                };
                // サンプル数を増やす
                if (false)
                {
                    // 元々の数
                    var count = samples.Count;
                    for (int i = 0; i < count; i++)
                    {
                        // 10パターン追加
                        for (int j = 0; j < 10; j++)
                        {
                            var sample = new Sample<T>(samples[i]);
                            sample.Randomization(random, _1 / _10);
                            samples.Add(sample);
                        }
                    }
                }
                //

                var numbersNN = new MultilayerPerceptron<T>(15, 10, 10, activation, activation);
                Console.WriteLine($"{nameof(numbersNN)}={numbersNN}");
                numbersNN.ResetWeightsWithRandom(_2);

                // 学習率
                var learningRate = _1;
                Console.WriteLine($"{nameof(learningRate)}={learningRate}");

                for (int i = 0; i < 10000; i++)
                {
                    var doWriteLine = i % 1000 == 0;
                    var errorSum = _0;
                    foreach (Sample<T> sample in samples)
                    {
                        var targets = sample.ResultValues.ToList();
                        numbersNN.SetSourceValues(sample.SourceValues);
                        // 順伝播
                        numbersNN.ForwardPropagation();
                        var results = numbersNN.ResultValues;
                        // 誤差の計算(二乗誤差)
                        var error = numbersNN.Error(sample.ResultValues);
                        if (doWriteLine)
                        {
                            Console.WriteLine($"{i} {targets.ToJoinedString(",")} : {nameof(results)}={results.ToJoinedString(",", "0.000")} {nameof(error)}={error}");
                        }
                        if (error > _0)
                        {
                            numbersNN.BackPropagation(targets, learningRate);
                        }
                        errorSum += error;
                    }
                    if (doWriteLine)
                    {
                        Console.WriteLine($"{i} {nameof(errorSum)}={errorSum}");
                    }
                    if (errorSum == _0)
                    {
                        break;
                    }
                }
                Console.WriteLine("--------------------------------");
                Console.WriteLine("出力層の活性化関数を変更して出力を0と1に");
                {
                    numbersNN.SetResultActivation(Activation<T>.HeavisideStep);
                    foreach (var sample in samples)
                    {
                        numbersNN.SetSourceValues(sample.SourceValues);
                        numbersNN.ForwardPropagation();
                        //var error = nn.Error(sample.ResultValues);
                        Console.WriteLine($"{sample.SourceValues.ToJoinedString(",")}→{numbersNN.ResultValues.ToJoinedString(",")}");
                    }
                    numbersNN.SetResultActivation(Activation<T>.Sigmoid);// 戻す
                }
                Console.WriteLine("--------------------------------");
                #endregion 数字認識
            }

        }

        static void _LogicGate<T>(MultilayerPerceptron<T> nn, IReadOnlyList<Sample<T>> samples, T learningRate, bool isResetWeights)
            where T : INumber<T>, IFloatingPointIeee754<T>, IMinMaxValue<T>
        {
            var _2 = T.CreateChecked(2);
            Console.WriteLine("================================");
            // 最初の重みはランダム
            if (isResetWeights)
            {
                nn.ResetWeightsWithRandom(_2);
            }

            for (int i = 0; i < 10000; i++)
            {
                var doWriteLine = i % 1000 == 0;
                if (doWriteLine) { Console.WriteLine($"{i}"); }
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
                    if (doWriteLine)
                    {
                        Console.WriteLine($"{sample.SourceValues.ToJoinedString(",")}→{nn.ResultValues[0]} {nameof(error)}={error}");
                    }
                    if (error > T.Zero)
                    {
                        nn.BackPropagation(targets, learningRate);
                    }
                    errorSum += error;
                }
                if (doWriteLine) { Console.WriteLine($"{nameof(errorSum)}={errorSum}"); }
                if (errorSum == T.Zero)
                {
                    break;
                }
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("出力層の活性化関数を変更して出力を0と1に");
            {
                nn.SetResultActivation(Activation<T>.HeavisideStep);
                foreach (var sample in samples)
                {
                    nn.SetSourceValues(sample.SourceValues);
                    nn.ForwardPropagation();
                    //var error = nn.Error(sample.ResultValues);
                    Console.WriteLine($"{sample.SourceValues.ToJoinedString(",")}→{nn.ResultValues[0]}");
                }
                nn.SetResultActivation(Activation<T>.Sigmoid);// 戻す
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("nn=");
            Console.WriteLine(nn.ToString());
            Console.WriteLine("================================");
        }
    }
}
#pragma warning restore CS0162 // 到達できないコードが検出されました