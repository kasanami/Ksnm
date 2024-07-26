using System;
using System.Diagnostics;
using System.Linq;
using Numeric = Ksnm.Numerics.Numeric;
using Ksnm.MachineLearning.NeuralNetwork;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary;
using Ksnm.ExtensionMethods.System.Single;
using Ksnm.ExtensionMethods.System.Double;
using System.Numerics;
using Ksnm.Numerics;
using static System.Math;
using System.Collections.Generic;
using Ksnm.ExtensionMethods.System.Random;

#pragma warning disable CS0162 // 到達できないコードが検出されました
namespace DemoApp
{
    public class Test
    {
        static Stopwatch stopwatch = new Stopwatch();

        public static void RunAll()
        {
            //SingleTest();
            //DoubleTest();
            //DecimalTest();
            //BigIntegerTest();
            //BigDecimalTest();
            //GreatestCommonDivisorWeightTest();
            //PowWeightTest();
            //PowTest();
            //ExpTest();
            //LogTest();
            //SqrtTest();
            //SinTest();
            //AsinTest();
            //AtanTest();
            FormulaTest();
            /*
            Numeric num = new Numeric(100m);
            num.Normalize();
            */
            //AbcConjecture();
            //FermatsLastTheorem();
            AITest();
            //AITest2();
        }
        public static void SingleTest()
        {
            Console.WriteLine($"SingleTest");
            var stopwatch = new Stopwatch();
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                float sample = 1.1f;
                for (int i = 0; i < 10000000; i++)
                {
                    sample.IsInteger();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }
        }
        public static void DoubleTest()
        {
            Console.WriteLine($"DoubleTest");
            var stopwatch = new Stopwatch();

            {
                for (int i = 0; i < 3; i++)
                {
                    var value = i * i;
                    Console.WriteLine($"{i} => {Math.Sqrt(value)}");
                }
            }

#if true
            {
                double baseValue = 0.00000001;
                for (int i = 0; i < 3; i++)
                {
                    var value = (float)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif

            Console.WriteLine($"IsPositive test");
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                double sample = 1.1;
                for (int i = 0; i < 100_000_000; i++)
                {
                    sample.IsPositive();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }

            Console.WriteLine($"IsInteger test");
            for (int j = 0; j < 5; j++)
            {
                stopwatch.Restart();
                double sample = 1.1;
                for (int i = 0; i < 10000000; i++)
                {
                    sample.IsInteger();
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ミリ秒");
            }
        }

        public static void DecimalTest()
        {
            Console.WriteLine("DecimalTest()");

            decimal pi = 3.14159_26535_89793_23846_26433_83279_50288m;
            pi = decimal.Parse("3.14159265358979323846264338327950288");


            {
                decimal baseValue = 0.001m;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#if true
            {
                decimal baseValue = 0.001m;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif
#if false
            {
                decimal baseValue = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    var value = (int)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {value}");
                }
            }
#endif

#if false
            {
                var baseValue = BigInteger.Parse("79228162514264337593543950335");
                for (int i = 0; i < 3; i++)
                {
                    decimal d = (decimal)(baseValue + i);
                    Console.WriteLine($"{baseValue + i} => {d}");
                }
            }
#endif

            {
                decimal d1 = 0.0000000000000000000000000001m;
                decimal d2 = 2m;

                decimal d3 = d1 / d2;
                Console.WriteLine($"{d1} / {d2} = {d3}");
            }

            for (decimal d1 = 0; d1 <= 20; d1++)
            {
                for (decimal d2 = 1; d2 <= 20; d2++)
                {
                    decimal d3 = d1 / d2;
                    Console.WriteLine($"{d1} / {d2} = {d3}");
                }
            }

            Console.WriteLine();
        }

        public static void BigIntegerTest()
        {
            Console.WriteLine("BigIntegerTest()");

            Console.WriteLine($"BigInteger.Pow(10, e);");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"e={i}");
                stopwatch.Restart();
                for (int j = 1; j < 5_000_000; j++)
                {
                    BigInteger.Pow(10, i);
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.Elapsed}");
            }
            Console.WriteLine();

            BigInteger sample = 0x0123_4567_89AB_CDEF;
            var bytes = sample.ToByteArray();
            Console.WriteLine(sample);
            Console.WriteLine(bytes.ToDebugString("X2", null, false));
        }

        public static void BigDecimalTest()
        {
            Console.WriteLine("BigDecimalTest()");

            Console.WriteLine($"BigDecimal.Sqrt();");
            for (int i = 0; i < 10; i++)
            {
                var value = BigDecimal.Sqrt(i, 105);
                Console.WriteLine($"√{i}={value}");
            }
            Console.WriteLine();

            Console.WriteLine($"BigDecimal.Pow10(e);");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"e={i}");
                stopwatch.Restart();
                for (int j = 1; j < 5_000_000; j++)
                {
                    BigDecimal.Pow10(i);
                }
                stopwatch.Stop();
                Console.WriteLine($"{stopwatch.Elapsed}");
            }
            Console.WriteLine();

            for (decimal i = -10; i < 10; i++)
            {
                var sample = new BigDecimal(i);
                Console.WriteLine(i);
                Console.WriteLine(sample.ToDecimal());
            }

            {
                decimal sourceDecimal = 0x0123_4567_89AB_CDEF;
                BigDecimal sample = new BigDecimal(sourceDecimal);
                var sampleDecimal = sample.ToDecimal();
                Console.WriteLine(sourceDecimal);
                Console.WriteLine(sampleDecimal);
                Console.WriteLine(sourceDecimal == sampleDecimal);
            }
        }
        public static void GreatestCommonDivisorWeightTest()
        {
            const int count = 2000;

            Console.WriteLine($"Math.GreatestCommonDivisor(int)");
            stopwatch.Restart();
            for (int m = 1; m <= count; m++)
            {
                for (int n = 1; n <= count; n++)
                {
                    Ksnm.Math.GreatestCommonDivisor(m, n);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            Console.WriteLine($"Math.GreatestCommonDivisor(uint)");
            stopwatch.Restart();
            for (uint m = 1; m <= count; m++)
            {
                for (uint n = 1; n <= count; n++)
                {
                    Ksnm.Math.GreatestCommonDivisor(m, n);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
        public static void PowWeightTest()
        {
            const int count = 10000;
            stopwatch.Restart();
            Console.WriteLine($"System.Math.Pow");
            for (int i = 0; i < count; i++)
            {
                for (int m = 0; m <= 10; m++)
                {
                    for (int n = 0; n <= 10; n++)
                    {
                        Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            stopwatch.Restart();
            Console.WriteLine($"Ksnm.Math.Pow(int)");
            for (int i = 0; i < count; i++)
            {
                for (int m = 0; m <= 10; m++)
                {
                    for (int n = 0; n <= 10; n++)
                    {
                        Ksnm.Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            stopwatch.Restart();
            Console.WriteLine($"Ksnm.Math.Pow(uint)");
            for (int i = 0; i < count; i++)
            {
                for (uint m = 0; m <= 10; m++)
                {
                    for (uint n = 0; n <= 10; n++)
                    {
                        Ksnm.Math.Pow(m, n);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
        public static void PowTest()
        {
            for (int e = -9; e <= 9; e++)
            {
                for (int n = -10; n < 10; n++)
                {
                    Console.WriteLine($"{n}^{e}={Math.Pow(n, e)}");
                }
                Console.WriteLine();
            }
            for (int n = -10; n < 10; n++)
            {
                for (int e = -9; e <= 9; e++)
                {
                    Console.WriteLine($"{n}^{e}={Math.Pow(n, e)}");
                }
                Console.WriteLine();
            }
        }
        public static void ExpTest()
        {
            Console.WriteLine($"ExpTest()");
            Console.WriteLine($"System.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"e^{n}={Exp(n)}");
            }
            Console.WriteLine($"Ksnm.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"e^{n}={Ksnm.Math.Exp(n)}");
            }
            // 計算回数を替えて試す
            for (int n = 0; n < 10; n++)
            {
                Console.WriteLine($"e^10 {n}={Ksnm.Math.Exp(10, n)}");
            }
        }
        public static void LogTest()
        {

        }
        public static void SqrtTest()
        {
            Console.WriteLine($"SqrtTest()");

            var countCount = new Dictionary<int, int>();

            {
                decimal sqrt;
                sqrt = Ksnm.Math.Sqrt(400m);
                sqrt = Ksnm.Math.Sqrt(16m);
                sqrt = Ksnm.Math.Sqrt(5m);
                sqrt = Ksnm.Math.Sqrt(31m, 30);
            }

            for (decimal i = 0; i < 100000; i++)
            {
                var sqrt = Ksnm.Math.Sqrt(i, 30);
                Console.WriteLine($"sqrt({i})={sqrt}");
            }

            countCount = countCount.OrderByKey();
        }
        public static void SinTest()
        {
            Console.WriteLine($"SinTest()");
            Console.WriteLine($"System.Sin");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"Sin {n}={Sin(n)}");
            }
            Console.WriteLine($"Ksnm.Math");
            for (int n = -10; n < 10; n++)
            {
                Console.WriteLine($"Sin {n}={Ksnm.Math.Sin(n)}");
            }
            // 計算回数を替えて試す
            Console.WriteLine();
            for (int n = -10; n < 10; n++)
            {
                for (int c = 0; c < 30; c++)
                {
                    Console.WriteLine($"Sin {n} {c}={Ksnm.Math.Sin(n, c)}");
                }
            }
        }
        public static void AsinTest()
        {
            Console.WriteLine($"AsinTest()");

            Console.WriteLine($"π/2={Ksnm.Math.PI_Decimal / 2}");
            Console.WriteLine($"π/4={Ksnm.Math.PI_Decimal / 4}");

            Console.WriteLine($"System.Math");
            for (double x = -1; x <= 1; x += 0.125)
            {
                Console.WriteLine($"Asin({x})={Math.Asin(x)}");
            }

            // 計算回数を替えて試す
            Console.WriteLine();
            for (decimal x = -1; x <= 1; x += 0.125m)
            {
                for (int c = 1; c <= 50; c++)
                {
                    try
                    {
                        Console.WriteLine($"Asin({x}, {c})={Ksnm.Math.Asin(x, c)}");
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        public static void AtanTest()
        {
            Console.WriteLine($"AtanTest()");

            Console.WriteLine($"π/2={Ksnm.Math.PI_Decimal / 2}");
            Console.WriteLine($"π/4={Ksnm.Math.PI_Decimal / 4}");

            Console.WriteLine($"System.Math");
            for (double x = -1; x <= 1; x += 0.125)
            {
                Console.WriteLine($"Atan({x})={Math.Atan(x)}");
            }

            {
                decimal x = 1;
                int c = 100000;
                Console.WriteLine($"Atan({x}, {c})={Ksnm.Math.Atan(x, c)}");
            }

            // 計算回数を替えて試す
            Console.WriteLine();
            for (decimal x = -1; x <= 1; x += 0.125m)
            {
                for (int c = 1; c <= 1; c++)
                {
                    try
                    {
                        Console.WriteLine($"Atan({x}, {c})={Ksnm.Math.Atan(x, c)}");
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        public static void FormulaTest()
        {
            Console.WriteLine("FormulaTest()");

            Console.WriteLine("PIByRamanujan");
            for (int n = 1; n <= 15; n++)
            {
                Console.WriteLine($"n={n}");
                var pi = 1 / Ksnm.Science.Mathematics.Formula.RamanujansPiFormula(n, 100);
                Console.WriteLine(pi.ToString());
            }

            Console.WriteLine("MachinsFormula");
            for (int n = 0; n <= 10; n++)
            {
                Console.WriteLine($"n={n}");
                var pi = Ksnm.Science.Mathematics.Formula.MachinsFormula(n) * 4;
                Console.WriteLine(pi.ToString());
            }
            Console.WriteLine("For BigDecimal");
            for (int n = 0; n <= 80; n++)
            {
                Console.WriteLine($"n={n}");
                var pi = Ksnm.Science.Mathematics.Formula.MachinsFormula(n, 100) * 4;
                Console.WriteLine(pi.ToString());
            }
        }
        /// <summary>
        /// ABC予想
        /// </summary>
        public static void AbcConjecture()
        {
            Console.WriteLine("ABC予想");
            var random = new Random();
            int count = 1000;// 試行回数
            checked
            {
                for (long a = 1; a < count; a++)
                {
                    for (long b = 1; b < count; b++)
                    {
                        long c = a + b;
                        long d = Rad(a * b * c);
                        var compare = c.CompareTo(d);
                        if (compare >= 0)
                        {
                            Console.WriteLine($"a={a}\tb={b}\tc={c}\t{compare.ToString()}");
                        }
                    }
                }
            }
        }
        static long Rad(long product)
        {
            var primeNumbers = Ksnm.Math.PrimeFactorization(product);
            primeNumbers = primeNumbers.Distinct();
            return primeNumbers.Product();
        }
        /// <summary>
        /// フェルマーの最終定理
        /// </summary>
        static void FermatsLastTheorem()
        {
            for (int n = 2; n <= 10; n++)
            {
                for (int x = 1; x < 100; x++)
                {
                    for (int y = 1; y < 100; y++)
                    {
                        double z2 = Math.Pow(x, n) + Math.Pow(y, n);
                        for (int z = 1; z < 100; z++)
                        {
                            var z3 = Math.Pow(z, n);
                            Console.WriteLine($"{x}^{n}+{y}^{n}={z2} == {z}^{n}={z3} → {(z2 == z3)}");
                            if (z2 <= z3)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static void AITest()
        {
            Console.WriteLine("AITest()");


            if (true)
            {
                #region NOTゲート
                // 目標値
                var NotSample = new List<Sample>
                {
                    new Sample(new [] {1.0},new [] {0.0}),
                    new Sample(new [] {0.0},new [] {1.0}),
                };

                var learningRate = 1.0;

                var nn = new MultilayerPerceptron(1, 1);
                nn.ResetWeightsWithRandom(1);
                for (int i = 0; i < 1000; i++)
                {
                    var errorSum = 0.0;
                    foreach (var sample in NotSample)
                    {
                        nn.SetSourceValues(sample.SourceValues);
                        // 更新
                        nn.ForwardPropagation();
                        // 誤差
                        var error = nn.Error(sample.ResultValues);
                        Console.WriteLine($"{i};{sample.SourceValues[0]}→{nn.ResultValues.ElementAt(0)} error ={error.ToDecimalString()}");
                        // バックプロパゲーション
                        if (error > 0)
                        {
                            AITest_BackPropagation2(nn, sample.ResultValues, learningRate);
                        }
                        errorSum += error;
                    }
                    Console.WriteLine($"{nameof(errorSum)}={errorSum}");
                    if (errorSum == 0)
                    {
                        break;
                    }
                }
                Console.WriteLine(nn.ToString());
                #endregion NOTゲート
            }
            if (true)
            {
                #region ORゲート
                // 目標値
                var OrSample = new List<Sample>
                {
                    new Sample(new [] {0.0, 0.0},new [] {0.0}),
                    new Sample(new [] {0.0, 1.0},new [] {1.0}),
                    new Sample(new [] {1.0, 0.0},new [] {1.0}),
                    new Sample(new [] {1.0, 1.0},new [] {1.0}),
                };

                var learningRate = 1.0;

                var nn = new MultilayerPerceptron(2, 1);
                nn.ResetWeightsWithRandom(1);
                for (int i = 0; i < 1000; i++)
                {
                    var errorSum = 0.0;
                    foreach (var sample in OrSample)
                    {
                        nn.SetSourceValues(sample.SourceValues);
                        // 更新
                        nn.ForwardPropagation();
                        // 誤差
                        var error = nn.Error(sample.ResultValues);
                        Console.WriteLine($"{i};{sample.SourceValues[0]}or{sample.SourceValues[1]}→{nn.ResultValues.ElementAt(0)} error ={error.ToDecimalString()}");
                        // バックプロパゲーション
                        if (error > 0)
                        {
                            AITest_BackPropagation2(nn, sample.ResultValues, learningRate);
                        }
                        errorSum += error;
                    }
                    Console.WriteLine($"{nameof(errorSum)}={errorSum}");
                    if (errorSum == 0)
                    {
                        break;
                    }
                }
                // 出力層の活性化関数を変更
                {
                    nn.SetResultActivation(Activation.HeavisideStep);
                    foreach (var sample in OrSample)
                    {
                        nn.SetSourceValues(sample.SourceValues);
                        nn.ForwardPropagation();
                        var error = nn.Error(sample.ResultValues);
                        Console.WriteLine($"{sample.SourceValues[0]}or{sample.SourceValues[1]}→{nn.ResultValues.ElementAt(0)} error ={error.ToDecimalString()}");
                    }
                }
                #endregion ORゲート
            }

            if (false)
            {
                #region 論理回路 GeneticAlgorithm
                var random = new Random();

                var numbersNN = new MultilayerPerceptron(2, 2, 1, Activation.Sigmoid, Activation.Sigmoid);
                Console.WriteLine($"{nameof(numbersNN)}={numbersNN}");

                var XorSample = new List<Sample>
                {
                    new Sample(new [] {0d, 0d},new [] {0d}),
                    new Sample(new [] {0d, 1d},new [] {1d}),
                    new Sample(new [] {1d, 0d},new [] {1d}),
                    new Sample(new [] {1d, 1d},new [] {0d}),
                };

                var learnParam = new MultilayerPerceptron.GeneticAlgorithmParam();
                learnParam.learningRate = 100;
                learnParam.tryCount = 100;
                learnParam.cloneCount = 1;
                learnParam.cloneWeightRangeStart = 0;
                learnParam.cloneWeightRangeDelta = 100.0 / learnParam.cloneCount;
                learnParam.samples = XorSample;

                Dictionary<int, double> seedErrorMap = new Dictionary<int, double>();

                for (int j = 0; j < 1000; j++)
                {
                    var randomSeed = 123;//0x2BDFAEA2;// random.Next();
                    learnParam.learningRate = j * 10;
                    Console.WriteLine($"No.{j} {nameof(randomSeed)}={randomSeed} {nameof(learnParam.learningRate)}={learnParam.learningRate}");
                    numbersNN.Random = new Random(randomSeed);
                    numbersNN.ResetWeights(1);

                    learnParam.learningRate = j / 1.0;
                    // 学習
                    for (int i = 0; i < 1; i++)
                    {
                        numbersNN = MultilayerPerceptron.LearnByGeneticAlgorithm(numbersNN, learnParam);
                    }
                    // 結果
                    Console.WriteLine("結果");
                    for (int i = 0; i < learnParam.samples.Count; i++)
                    {
                        numbersNN.ForwardPropagation(learnParam.samples[i].SourceValues);
                        // 数値調整
                        var resultValues = numbersNN.ResultValues.Select(x => x > 0.5 ? 1 : 0).ToArray();
                        Console.WriteLine($"NumbersSample[{i}]={resultValues.ToDebugString("0 ", null, false)}");
                    }
                    // 誤差
                    var error = numbersNN.ErrorRecalculate(learnParam.samples);
                    Console.WriteLine($"error ={error.ToDecimalString()}");
                    //seedErrorMap.Add(randomSeed, error);
                }
                seedErrorMap = seedErrorMap.OrderByValue();
                Console.WriteLine($"seedErrorMap={seedErrorMap.ToDebugString("X8", "0.0000000000", true)}");
                #endregion 論理回路
            }

            if (false)
            {
                #region 数字認識
                var random = new Random();
                var samples = new List<Sample>
                {
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 1,
                        1, 0, 1,
                        1, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
#if true
                    new Sample() { SourceValues = new double[] {
                        0, 1, 0,
                        0, 1, 0,
                        0, 1, 0,
                        0, 1, 0,
                        0, 1, 0,}, ResultValues = new double[] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        0, 0, 1,
                        1, 1, 1,
                        1, 0, 0,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        0, 0, 1,
                        1, 1, 1,
                        0, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 0, 1,
                        1, 0, 1,
                        1, 1, 1,
                        0, 0, 1,
                        0, 0, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 0,
                        1, 1, 1,
                        0, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 0,
                        1, 1, 1,
                        1, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 1,
                        0, 0, 1,
                        0, 0, 1,
                        0, 0, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 1,
                        1, 1, 1,
                        1, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 } },
                    new Sample() { SourceValues = new double[] {
                        1, 1, 1,
                        1, 0, 1,
                        1, 1, 1,
                        0, 0, 1,
                        1, 1, 1,}, ResultValues = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } },
#endif
                };
                // サンプル数を増やす
                if (true)
                {
                    // 元々の数
                    var count = samples.Count;
                    for (int i = 0; i < count; i++)
                    {
                        // 10パターン追加
                        for (int j = 0; j < 10; j++)
                        {
                            var sample = new Sample(samples[i]);
                            sample.Randomization(random, 0.3);
                            samples.Add(sample);
                        }
                    }
                }
                //

                var numbersNN = new MultilayerPerceptron(15, 10, 10, Activation.Sigmoid, Activation.Sigmoid);
                Console.WriteLine($"{nameof(numbersNN)}={numbersNN}");
                numbersNN.Random = new Random();
                numbersNN.ResetWeights(0.5);

                // 学習率
                var learningRate = 1.0;
                Console.WriteLine($"{nameof(learningRate)}={learningRate}");

                for (int i = 0; i < 1000; i++)
                {
                    var errorSum = 0.0;
                    foreach (Sample sample in samples)
                    {
                        var targets = sample.ResultValues.ToList();
                        numbersNN.SetSourceValues(sample.SourceValues);
                        // 順伝播
                        numbersNN.ForwardPropagation();
                        var results = numbersNN.ResultValues;
                        // 誤差の計算(二乗誤差)
                        var error = numbersNN.Error(sample.ResultValues);
                        if (i == 999)
                        {
                            Console.WriteLine($"{i} {targets.ToJoinedString(",")} : {nameof(results)}={results.ToJoinedString(",")} {nameof(error)}={error}");
                        }
                        if (error > 0)
                        {
                            AITest_BackPropagation(numbersNN, targets, learningRate);
                        }
                        errorSum += error;
                    }
                    Console.WriteLine($"{i} {nameof(errorSum)}={errorSum}");
                    if (errorSum == 0)
                    {
                        break;
                    }
                }
                #endregion 数字認識
            }

            if (true)
            {
                #region XORゲート
                var random = new Random();

                var nn = new MultilayerPerceptron(2, 3, 1, Activation.Sigmoid, Activation.Sigmoid);

                // 最初の重みはランダム
                nn.ResetWeightsWithRandom(2);

                // 目標値
                var XorSample = new List<Sample>
                {
                    new Sample(new [] {0d, 0d},new [] {0d}),
                    new Sample(new [] {0d, 1d},new [] {1d}),
                    new Sample(new [] {1d, 0d},new [] {1d}),
                    new Sample(new [] {1d, 1d},new [] {0d}),
                };
                // 学習率
                var learningRate = 1.0;
                Console.WriteLine($"{nameof(learningRate)}={learningRate}");

                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"{i}");
                    var errorSum = 0d;
                    foreach (Sample sample in XorSample)
                    {
                        var targets = sample.ResultValues;
                        nn.SetSourceValues(sample.SourceValues);
                        // 順伝播
                        nn.ForwardPropagation();
                        var result = nn.ResultNeurons[0].Value;
                        // 誤差の計算(二乗誤差)
                        var error = nn.Error(targets);
                        Console.WriteLine($"{sample.SourceValues[0]},{sample.SourceValues[1]}={targets[0]} : {nameof(result)}={result} {nameof(error)}={error}");
                        if (error > 0)
                        {
                            AITest_BackPropagation(nn, targets, learningRate);
                        }
                        errorSum += error;
                    }
                    Console.WriteLine($"{nameof(errorSum)}={errorSum}");
                    if (errorSum == 0)
                    {
                        break;
                    }
                }
                // 出力層の活性化関数を変更
                {
                    nn.SetResultActivation(Activation.HeavisideStep);
                    foreach (var sample in XorSample)
                    {
                        nn.SetSourceValues(sample.SourceValues);
                        nn.ForwardPropagation();
                        var error = nn.Error(sample.ResultValues);
                        Console.WriteLine($"{sample.SourceValues[0]}or{sample.SourceValues[1]}→{nn.ResultValues.ElementAt(0)} error ={error.ToDecimalString()}");
                    }
                }
                #endregion XORゲート
            }
        }
        public static void AITest_LogicGate(MultilayerPerceptron nn, IReadOnlyList<Sample> samples, double learningRate)
        {
            // 最初の重みはランダム
            nn.ResetWeightsWithRandom(2);

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"{i}");
                var errorSum = 0.0;
                foreach (var sample in samples)
                {
                    var targets = sample.ResultValues;
                    nn.SetSourceValues(sample.SourceValues);
                    // 順伝播
                    nn.ForwardPropagation();
                    var result = nn.ResultNeurons[0].Value;
                    // 誤差の計算(二乗誤差)
                    var error = nn.Error(targets);
                    Console.WriteLine($"{sample.SourceValues[0]},{sample.SourceValues[1]}={targets[0]} : {nameof(result)}={result} {nameof(error)}={error}");
                    if (error > 0)
                    {
                        AITest_BackPropagation(nn, targets, learningRate);
                    }
                    errorSum += error;
                }
                Console.WriteLine($"{nameof(errorSum)}={errorSum}");
                if (errorSum == 0)
                {
                    break;
                }
            }
            // 出力層の活性化関数を変更
            {
                nn.SetResultActivation(Activation.HeavisideStep);
                foreach (var sample in samples)
                {
                    nn.SetSourceValues(sample.SourceValues);
                    nn.ForwardPropagation();
                    var error = nn.Error(sample.ResultValues);
                    Console.WriteLine($"{sample.SourceValues[0]}or{sample.SourceValues[1]}→{nn.ResultValues.ElementAt(0)} error ={error.ToDecimalString()}");
                }
            }
        }
        public static void AITest_BackPropagation2(MultilayerPerceptron nn, IReadOnlyList<double> targetValues, double learningRate)
        {
            // 各値へのショートカット
            var results = nn.ResultValues.ToList();
            var sources = nn.SourceNeurons;

            // 出力層の誤差：
            var resultDeltas = new double[results.Count];
            for (var ri = 0; ri < results.Count; ri++)
            {
                var DerFunc = nn.ResultNeurons[ri].Activation.DerivativeFunction;
                resultDeltas[ri] = (results[ri] - targetValues[ri]) * DerFunc(results[ri]);
            }

            // 重みの更新
            for (var ri = 0; ri < results.Count; ri++)
            {
                for (var hi = 0; hi < sources.Count; hi++)
                {
                    nn.ResultNeurons[ri].InputWeights[hi]
                        -= learningRate * (resultDeltas[ri] * sources[hi].Value);
                }
                nn.ResultNeurons[ri].Bias -= learningRate * resultDeltas[ri];
            }
        }
        public static void AITest_BackPropagation(MultilayerPerceptron nn, IReadOnlyList<double> targetValues, double learningRate)
        {
            // D・・・「∂」は偏微分を示す記号であり、多変数関数の一つの変数に関する微分を表します。
            // Delta・・・「δ」（デルタ）は、ニューラルネットワークや誤差逆伝播（バックプロパゲーション）において、一般的に「誤差項」を表します。

            // 各値へのショートカット
            var results = nn.ResultValues.ToList();
            var hiddens = nn.HiddenNeurons;
            var sources = nn.SourceNeurons;

            // 出力層の誤差：
            var resultDeltas = new double[results.Count];
            for (var ri = 0; ri < results.Count; ri++)
            {
                var DerFunc = nn.ResultNeurons[ri].Activation.DerivativeFunction;
                resultDeltas[ri] = (results[ri] - targetValues[ri]) * DerFunc(results[ri]);
            }

            // 隠れ層の各ノードに逆伝播される誤差：
            double[] hiddenDeltas = new double[0];
            if (hiddens != null)
            {
                hiddenDeltas = new double[hiddens.Count];
                for (var hi = 0; hi < hiddenDeltas.Length; hi++)
                {
                    hiddenDeltas[hi] = 1;
                    var DerFunc = nn.HiddenNeurons[hi].Activation.DerivativeFunction;
                    for (var ri = 0; ri < results.Count; ri++)
                    {
                        hiddenDeltas[hi] *= resultDeltas[ri] * nn.ResultNeurons[ri].InputWeights[hi];
                    }
                    hiddenDeltas[hi] *= DerFunc(hiddens[hi].Value);
                }
            }

            // 重みの更新
            for (var ri = 0; ri < results.Count; ri++)
            {
                for (var hi = 0; hi < hiddens.Count; hi++)
                {
                    nn.ResultNeurons[ri].InputWeights[hi]
                        -= learningRate * (resultDeltas[ri] * hiddens[hi].Value);
                }
                nn.ResultNeurons[ri].Bias -= learningRate * resultDeltas[ri];
            }
            // 隠れ層の重みの更新
            for (var hi = 0; hi < hiddenDeltas.Length; hi++)
            {
                for (var si = 0; si < sources.Count; si++)
                {
                    // 隠れ層の重みに対する勾配=hiddenDeltas[i] * sources[j].Value
                    nn.HiddenNeurons[hi].InputWeights[si] -=
                        learningRate * (hiddenDeltas[hi] * sources[si].Value);
                    nn.HiddenNeurons[hi].Bias -=
                        learningRate * hiddenDeltas[hi];
                }
            }
        }
        public static void AITest2()
        {
            Console.WriteLine($"{nameof(AITest2)}");
            var multilayerPerceptron = new MultilayerPerceptron(2, 2, 2, 1);

            var random = new Random();
            // 入力
            multilayerPerceptron.SourceNeurons[0].Value = 1;
            multilayerPerceptron.SourceNeurons[1].Value = -1;

            // 重みの初期値
            multilayerPerceptron.HiddenNeurons[0].InputWeights[0] = random.Range(-2.0, +2.0);
            multilayerPerceptron.HiddenNeurons[0].InputWeights[1] = random.Range(-2.0, +2.0);
            multilayerPerceptron.HiddenNeurons[1].InputWeights[0] = random.Range(-2.0, +2.0);
            multilayerPerceptron.HiddenNeurons[1].InputWeights[1] = random.Range(-2.0, +2.0);

            multilayerPerceptron.Hidden2Neurons[0].InputWeights[0] = random.Range(-2.0, +2.0);
            multilayerPerceptron.Hidden2Neurons[0].InputWeights[1] = random.Range(-2.0, +2.0);
            multilayerPerceptron.Hidden2Neurons[1].InputWeights[0] = random.Range(-2.0, +2.0);
            multilayerPerceptron.Hidden2Neurons[1].InputWeights[1] = random.Range(-2.0, +2.0);

            multilayerPerceptron.ResultNeurons[0].InputWeights[0] = random.Range(-2.0, +2.0);
            multilayerPerceptron.ResultNeurons[0].InputWeights[1] = random.Range(-2.0, +2.0);

            // 目標値
            var target = 1;
            Console.WriteLine($"{nameof(target)}={target}");
            // 学習率
            var learningRate = 4;
            Console.WriteLine($"{nameof(learningRate)}={learningRate}");

            for (int i = 0; i < 100; i++)
            {
                // 順伝播
                multilayerPerceptron.ForwardPropagation();
                var result = multilayerPerceptron.ResultValues.ElementAt(0);
                // 誤差の計算(二乗誤差)
                var error = ((result - target) * (result - target)) / 2;
                Console.WriteLine($"{i} {nameof(result)}={result} {nameof(error)}={error}");
                if (error > 0)
                {
                    AITest2_Backpropagation(multilayerPerceptron, target, learningRate);
                }
                else
                {
                    break;
                }
            }
        }
        public static void AITest2_Backpropagation(MultilayerPerceptron multilayerPerceptron, double target, double learningRate)
        {
            // 各値へのショートカット

            // 目標値
            var t = target;
            // 学習率
            var η = learningRate;

            var x1 = multilayerPerceptron.SourceNeurons[0].Value;
            var x2 = multilayerPerceptron.SourceNeurons[1].Value;

            var h1 = multilayerPerceptron.HiddenNeurons[0].Value;
            var h2 = multilayerPerceptron.HiddenNeurons[1].Value;
            var w11 = multilayerPerceptron.HiddenNeurons[0].InputWeights[0];
            var w12 = multilayerPerceptron.HiddenNeurons[0].InputWeights[1];
            var w21 = multilayerPerceptron.HiddenNeurons[1].InputWeights[0];
            var w22 = multilayerPerceptron.HiddenNeurons[1].InputWeights[1];

            var h3 = multilayerPerceptron.Hidden2Neurons[0].Value;
            var h4 = multilayerPerceptron.Hidden2Neurons[1].Value;
            var w31 = multilayerPerceptron.Hidden2Neurons[0].InputWeights[0];
            var w32 = multilayerPerceptron.Hidden2Neurons[0].InputWeights[1];
            var w41 = multilayerPerceptron.Hidden2Neurons[1].InputWeights[0];
            var w42 = multilayerPerceptron.Hidden2Neurons[1].InputWeights[1];

            var y = multilayerPerceptron.ResultNeurons[0].Value;
            var w51 = multilayerPerceptron.ResultNeurons[0].InputWeights[0];
            var w52 = multilayerPerceptron.ResultNeurons[0].InputWeights[1];

            // 出力層の誤差項の計算
            var δoutput = (y - t) * y * (1 - y);

            // 出力層の重みの勾配
            var dE_dw51 = δoutput * h3;
            var dE_dw52 = δoutput * h4;

            // 隠れ層2の誤差項の計算
            var δh3 = δoutput * w51 * h3 * (1 - h3);
            var δh4 = δoutput * w52 * h4 * (1 - h4);

            // 隠れ層2の重みの勾配
            var dE_dw31 = δh3 * h1;
            var dE_dw32 = δh3 * h2;
            var dE_dw41 = δh4 * h1;
            var dE_dw42 = δh4 * h2;

            // 隠れ層1の誤差項の計算
            var δh1 = (δh3 * w31 + δh4 * w41) * h1 * (1 - h1);
            var δh2 = (δh3 * w32 + δh4 * w42) * h2 * (1 - h2);

            // 隠れ層1の重みの勾配
            var dE_dw11 = δh1 * x1;
            var dE_dw12 = δh1 * x2;
            var dE_dw21 = δh2 * x1;
            var dE_dw22 = δh2 * x2;

            // 重みの更新
            w11 -= η * dE_dw11;
            w12 -= η * dE_dw12;
            w21 -= η * dE_dw21;
            w22 -= η * dE_dw22;
            w31 -= η * dE_dw31;
            w32 -= η * dE_dw32;
            w41 -= η * dE_dw41;
            w42 -= η * dE_dw42;
            w51 -= η * dE_dw51;
            w52 -= η * dE_dw52;

            multilayerPerceptron.HiddenNeurons[0].InputWeights[0] = w11;
            multilayerPerceptron.HiddenNeurons[0].InputWeights[1] = w12;
            multilayerPerceptron.HiddenNeurons[1].InputWeights[0] = w21;
            multilayerPerceptron.HiddenNeurons[1].InputWeights[1] = w22;

            multilayerPerceptron.Hidden2Neurons[0].InputWeights[0] = w31;
            multilayerPerceptron.Hidden2Neurons[0].InputWeights[1] = w32;
            multilayerPerceptron.Hidden2Neurons[1].InputWeights[0] = w41;
            multilayerPerceptron.Hidden2Neurons[1].InputWeights[1] = w42;

            multilayerPerceptron.ResultNeurons[0].InputWeights[0] = w51;
            multilayerPerceptron.ResultNeurons[0].InputWeights[1] = w52;
        }
    }
}

#pragma warning restore CS0162 // 到達できないコードが検出されました