using BenchmarkDotNet.Running;
using Ksnm.Numerics;
using System.Numerics;

namespace ConsoleApp
{
    using K = Ksnm.Numerics;

    internal class NumericsTest
    {
        const string Separator = "================================================================================";
        public static void Run()
        {
            Console.WriteLine(Ksnm.Debug.GetFilePathAndLineNumber());

            MatrixTest<Float32>();
            MatrixTest2();
            TestMatrix4x4();

            {
                Console.WriteLine($"{nameof(FixedPointDecimal8Q2)}");
                FixedPointDecimal8Q2 a = new(0.01m);
                FixedPointDecimal8Q2 b = new(1.00m);
                NumberBaseTest(a, b);

                a = new(0.50m);
                b = new(0.50m);
                NumberBaseTest(a, b);

                a = new(1.1m);
                b = new(1.1m);
                NumberBaseTest(a, b);

                Console.WriteLine();
            }
            {
                Console.WriteLine($"{nameof(FixedPointDecimal16Q3)}");
                FixedPointDecimal16Q3 a = new(0.001m);
                FixedPointDecimal16Q3 b = new(1.000m);
                NumberBaseTest(a, b);

                a = new(0.50m);
                b = new(0.50m);
                NumberBaseTest(a, b);

                a = new(1.1m);
                b = new(1.1m);
                NumberBaseTest(a, b);

                a = new(1.0m);
                b = new(3.0m);
                NumberBaseTest(a, b);

                Console.WriteLine();
            }
            {
                Console.WriteLine($"{nameof(FixedPointDecimal32Q5)}");
                FixedPointDecimal32Q5 a = new(0.00001m);
                FixedPointDecimal32Q5 b = new(1.00000m);
                NumberBaseTest(a, b);

                a = new(0.50m);
                b = new(0.50m);
                NumberBaseTest(a, b);

                a = new(1.1m);
                b = new(1.1m);
                NumberBaseTest(a, b);

                a = new(1.0m);
                b = new(3.0m);
                NumberBaseTest(a, b);

                Console.WriteLine();
            }
            {
                Console.WriteLine($"{nameof(Ksnm.Numerics.Vector<double>)}");

                Ksnm.Numerics.Vector<double> vector = new(3);

                vector[0] = 1;
                vector[1] = 1;
                vector[2] = 1;

                Console.WriteLine($"{nameof(vector)}={vector}");
                Console.WriteLine($"{nameof(vector.Magnitude)}={vector.Magnitude}");
                Console.WriteLine($"{nameof(vector.MagnitudePow2)}={vector.MagnitudePow2}");

                vector[1] = -1;
                vector[2] = -1;
                Console.WriteLine($"{nameof(vector)}={vector}");
                Console.WriteLine($"{nameof(vector.Magnitude)}={vector.Magnitude}");
                Console.WriteLine($"{nameof(vector.MagnitudePow2)}={vector.MagnitudePow2}");
            }

            {
                Console.WriteLine();
                Matrix<BFloat16> mat1 = new Matrix<BFloat16>(3, 3);
                mat1[0, 0] = (BFloat16)1.0;
                mat1[0, 1] = (BFloat16)2.0;
                mat1[0, 2] = (BFloat16)3.0;
                mat1[1, 0] = (BFloat16)4.0;
                mat1[1, 1] = (BFloat16)5.0;
                mat1[1, 2] = (BFloat16)6.0;
                mat1[2, 0] = (BFloat16)7.0;
                mat1[2, 1] = (BFloat16)8.0;
                mat1[2, 2] = (BFloat16)9.0;
                Matrix<BFloat16> mat2 = new Matrix<BFloat16>(3, 3);
                mat2[0, 0] = (BFloat16)1.0;
                mat2[0, 1] = (BFloat16)1.0;
                mat2[0, 2] = (BFloat16)1.0;
                mat2[1, 0] = (BFloat16)1.0;
                mat2[1, 1] = (BFloat16)1.0;
                mat2[1, 2] = (BFloat16)1.0;
                mat2[2, 0] = (BFloat16)1.0;
                mat2[2, 1] = (BFloat16)1.0;
                mat2[2, 2] = (BFloat16)1.0;

                var mat3 = mat1 * mat2;
            }

            {
                Console.WriteLine($"{nameof(Int128)}");
                Console.WriteLine($"{nameof(Int128.MaxValue)}={Int128.MaxValue}");
                Console.WriteLine($"{nameof(Int128.MinValue)}={Int128.MinValue}");
                Console.WriteLine($"{nameof(Int128.Zero)}={Int128.Zero}");
                Console.WriteLine($"{nameof(Int128.One)}={Int128.One}");
                Console.WriteLine($"{nameof(Int128.NegativeOne)}={Int128.NegativeOne}");
            }

            {
                Console.WriteLine($"NaN {Separator}");
                Float8 f = Float8.NaN;
                ConsoleWriteLine(f);
                ExtendedHalf h = Float16.NaN;
                ConsoleWriteLine(h);
                ExtendedSingle s = Float32.NaN;
                ConsoleWriteLine(s);
                ExtendedDouble d = Float64.NaN;
                ConsoleWriteLine(d);
                Console.WriteLine();
            }

            {
                Console.WriteLine($"One {Separator}");
                Float8 f = Float8.One;
                ConsoleWriteLine(f);
                ExtendedHalf h = Float16.One;
                ConsoleWriteLine(h);
                ExtendedSingle s = 1;
                ConsoleWriteLine(s);
                ExtendedDouble d = 1;
                ConsoleWriteLine(d);
                Console.WriteLine();
            }

            {
                Console.WriteLine($"NegativeOne {Separator}");
                Float8 f = Float8.NegativeOne;
                ConsoleWriteLine(f);
                ExtendedHalf h = Float16.NegativeOne;
                ConsoleWriteLine(h);
                ExtendedSingle s = -1;
                ConsoleWriteLine(s);
                ExtendedDouble d = -1;
                ConsoleWriteLine(d);
                Console.WriteLine();
            }

            //ExtendedSingleTest();
            //ExtendedDoubleTest();
            //ExtendedDecimalTest();
            //CubedDividedNumber8Test();
            //FractionTest();
            FloatingPointNumber16E8M7Test();
            FloatingPointNumber8S1E4M3B7Test();

            // NaNはキャスト可能？→可能
            if (false)
            {
                Half f16 = Half.NaN;
                Console.WriteLine($"{f16} {BitConverter.HalfToUInt16Bits(f16):X4}");
                float f32 = float.NaN;
                Console.WriteLine($"{f32} {BitConverter.SingleToUInt32Bits(f32):X8}");
                double f64 = double.NaN;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");

                f64 = (double)f16;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");

                f64 = (double)f32;
                Console.WriteLine($"{f64} {BitConverter.DoubleToUInt64Bits(f64):X16}");
            }
        }

        public static void MatrixTest<T>()
            where T : INumber<T>, IExponentialFunctions<T>, IRootFunctions<T>
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(MatrixTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            T _0_1 = T.CreateChecked(0.1);
            T _0_2 = T.CreateChecked(0.2);
            T _0_3 = T.CreateChecked(0.3);
            T _0_4 = T.CreateChecked(0.4);
            T _0_5 = T.CreateChecked(0.5);
            T _0_6 = T.CreateChecked(0.6);
            T _0_7 = T.CreateChecked(0.7);
            T _0_8 = T.CreateChecked(0.8);
            T _0_9 = T.CreateChecked(0.9);

            // 入力データ (特徴量数3)
            K.Vector<T> input = new[] { _0_5, _0_3, _0_2 };
            Console.WriteLine($"{nameof(input)}={input}");

            // 第1層の重み (隠れ層ノード数4, 入力層ノード数3)
            K.Matrix<T> W1 = new[,]
            {
                { _0_2, _0_4, _0_1 },
                { _0_1, _0_3, _0_4 },
                { _0_5, _0_2, _0_3 },
                { _0_3, _0_1, _0_2 }
            };
            var W1_T = W1.GetTranspose();
            Console.WriteLine($"{nameof(W1)}={W1}");
            Console.WriteLine($"{nameof(W1_T)}={W1_T}");

            // 第1層のバイアス (隠れ層ノード数4)
            K.Vector<T> b1 = new[] { _0_1, _0_2, _0_3, _0_4 };
            Console.WriteLine($"{nameof(b1)}={b1}");

            // 第2層の重み (出力層ノード数2, 隠れ層ノード数4)
            K.Matrix<T> W2 = new[,]
            {
                { _0_3, _0_1, _0_4, _0_2 },
                { _0_2, _0_4, _0_1, _0_3 }
            };
            var W2_T = W2.GetTranspose();
            Console.WriteLine($"{nameof(W2)}={W2}");
            Console.WriteLine($"{nameof(W2_T)}={W2_T}");

            // 第2層のバイアス (出力層ノード数2)
            K.Vector<T> b2 = new[] { _0_2, _0_1 };
            Console.WriteLine($"{nameof(b2)}={b2}");

            // 順伝播の計算
            // 第1層の計算
            var z1 = W1 * input + b1;
            K.Vector<T> a1 = Ksnm.Math.StandardSigmoid<T>(z1);
            Console.WriteLine("第1層の計算");
            Console.WriteLine($"{nameof(z1)}={z1}");
            Console.WriteLine($"{nameof(a1)}={a1}");

            // 第2層の計算
            var z2 = W2 * a1 + b2;
            K.Vector<T> a2 = Ksnm.Math.StandardSigmoid<T>(z2);
            Console.WriteLine("第2層の計算");
            Console.WriteLine($"{nameof(z2)}={z2}");
            Console.WriteLine($"{nameof(a2)}={a2}");

            // 結果の表示
            Console.WriteLine("隠れ層の出力 (a1):");
            Console.WriteLine(a1);
            Console.WriteLine("出力層の出力 (a2):");
            Console.WriteLine(a2);

            Console.WriteLine();
        }
        static void TestMatrix4x4()
        {
            Matrix4x4 matrix1 = new
            (
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4
            );
            Matrix4x4 matrix2 = new
            (
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4
            );
            var matrix3 = matrix1 * matrix2;
        }

        #region
        // シグモイド関数
        static double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        // 行列とベクトルの掛け算
        static double[] Dot(double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[] result = new double[rows];

            for (int r = 0; r < rows; r++)
            {
                result[r] = 0.0;
                for (int c = 0; c < cols; c++)
                {
                    result[r] += matrix[r, c] * vector[c];
                }
            }
            return result;
        }

        // ベクトルにバイアスを加算する
        static double[] AddBias(double[] vector, double[] bias)
        {
            int length = vector.Length;
            double[] result = new double[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = vector[i] + bias[i];
            }
            return result;
        }

        // ベクトルの各要素にシグモイド関数を適用する
        static double[] ApplySigmoid(double[] vector)
        {
            int length = vector.Length;
            double[] result = new double[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = Sigmoid(vector[i]);
            }
            return result;
        }
        #endregion

        static void MatrixTest2()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(MatrixTest2)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            // 入力データ (特徴量数3)
            double[] input = { 0.5, 0.3, 0.2 };

            // 第1層の重み (隠れ層ノード数4, 入力層ノード数3)
            double[,] W1 = {
                { 0.2, 0.4, 0.1 },
                { 0.1, 0.3, 0.4 },
                { 0.5, 0.2, 0.3 },
                { 0.3, 0.1, 0.2 }
            };

            // 第1層のバイアス (隠れ層ノード数4)
            double[] b1 = { 0.1, 0.2, 0.3, 0.4 };

            // 第2層の重み (出力層ノード数2, 隠れ層ノード数4)
            double[,] W2 = {
                { 0.3, 0.1, 0.4, 0.2 },
                { 0.2, 0.4, 0.1, 0.3 }
            };

            // 第2層のバイアス (出力層ノード数2)
            double[] b2 = { 0.2, 0.1 };

            // 第1層の計算
            double[] z1 = Dot(W1, input);
            z1 = AddBias(z1, b1);
            double[] a1 = ApplySigmoid(z1);

            // 第2層の計算
            double[] z2 = Dot(W2, a1);
            z2 = AddBias(z2, b2);
            double[] a2 = ApplySigmoid(z2);

            // 結果の表示
            Console.WriteLine("隠れ層の出力 (a1):");
            foreach (var value in a1)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("出力層の出力 (a2):");
            foreach (var value in a2)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine();
        }
        static void NumberBaseTest<T>(T a, T b) where T : INumberBase<T>
        {
            Console.WriteLine($"{nameof(a)}={a}");
            Console.WriteLine($"{nameof(b)}={b}");

            Console.WriteLine($"a + b={a + b}");
            Console.WriteLine($"b + a={b + a}");
            Console.WriteLine($"a - b={a - b}");
            Console.WriteLine($"b - a={b - a}");
            Console.WriteLine($"a * b={a * b}");
            Console.WriteLine($"b * a={b * a}");
            Console.WriteLine($"a / b={a / b}");
            Console.WriteLine($"b / a={b / a}");

            Console.WriteLine();
        }

        public static void ExtendedHalfTest()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(ExtendedHalfTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();
        }

        public static void ExtendedSingleTest()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(ExtendedSingleTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            for (long i = 0; i < 0b11111111_111_1111111111_1111111111; i++)
            {
                if (i == 0b11)
                {
                    i = 0b111_1111111111_1111111111;
                }
                else if (i == 0b1_000_0000000000_0000000011)
                {
                    i = 0b10_000_0000000000_0000000000;
                }
                else if (i == 0b10_000_0000000000_0000000011)
                {
                    i = 0b11111110_000_0000000000_0000000000;
                }
                else if (i == 0b11111110_000_0000000000_0000000011)
                {
                    i = 0b11111110_111_1111111111_1111111111;
                }
                else if (i == 0b11111111_000_0000000000_0000000011)
                {
                    i = 0b11111111_111_1111111111_1111111111;
                }
                ExtendedSingle value = i;
                value.Bits = (uint)i;
                ConsoleWriteLine(value);
            }

            for (float i = -10; i <= 10; i += 0.1f)
            {
                ExtendedSingle value = i;
                var reconstruction = value.Coefficient * value.Scale * value.Sign;
                Console.WriteLine($"Value      :{value.Value}");
                Console.WriteLine($"Bits       :0x{value.Bits:X}");
                Console.WriteLine($"UpperBits  :0x{value.UpperBits:X}");
                Console.WriteLine($"LowerBits  :0x{value.LowerBits:X}");
                Console.WriteLine($"Sign       :{value.Sign}");
                Console.WriteLine($"Exponent   :{value.Exponent}");
                Console.WriteLine($"Mantissa   :0x{value.Mantissa:X}");
                Console.WriteLine($"Scale      :{value.Scale}");
                Console.WriteLine($"Coefficient:{value.Coefficient}");
                Console.WriteLine($"復元       :{reconstruction}");
                Console.WriteLine();
            }

            {
                ExtendedSingle f = 1;
                f.Sign = +1;
                ConsoleWriteLine(f);
                f.Sign = -1;
                ConsoleWriteLine(f);

                f.Exponent = +1;
                ConsoleWriteLine(f);
                f.Exponent = -1;
                ConsoleWriteLine(f);
                f.Exponent = 0;
                ConsoleWriteLine(f);

                f.Mantissa = 1;
                ConsoleWriteLine(f);
            }

            {
                ExtendedSingle f;
                ExtendedDouble d;
                f = 0.125f;
                d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, UInt32>(f);
                ConsoleWriteLine(f);
                ConsoleWriteLine(d);
                f = 2;
                d = IFloatingPointNumberBase.ToDouble<ExtendedSingle, UInt32>(f);
                ConsoleWriteLine(f);
                ConsoleWriteLine(d);
            }

            Console.WriteLine();
        }

        public static void ExtendedDoubleTest()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(ExtendedDoubleTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            for (double i = -10; i <= 10; i += 0.1)
            {
                ExtendedDouble value = i;
                var reconstruction = value.Coefficient * value.Scale * value.Sign;
                Console.WriteLine($"Value      :{value.Value}");
                Console.WriteLine($"Bits       :0x{value.Bits:X}");
                Console.WriteLine($"UpperBits  :0x{value.UpperBits:X}");
                Console.WriteLine($"LowerBits  :0x{value.LowerBits:X}");
                Console.WriteLine($"Sign       :{value.Sign}");
                Console.WriteLine($"Exponent   :{value.Exponent}");
                Console.WriteLine($"Mantissa   :0x{value.Mantissa:X}");
                Console.WriteLine($"Scale      :{value.Scale}");
                Console.WriteLine($"Coefficient:{value.Coefficient}");
                Console.WriteLine($"復元       :{reconstruction}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void ExtendedDecimalTest()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(ExtendedDecimalTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            for (decimal i = -10; i <= 10; i += 0.5m)
            {
                ExtendedDecimal d = i;
                ConsoleWriteLine(d);
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void CubedDividedNumber8Test()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(CubedDividedNumber8Test)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            CubedDividedNumber8 cdn = new CubedDividedNumber8();
            for (int i = 1; i <= 128; i += 8)
            {
                CubedDividedNumber8.Divisor = i * i * i;
                Console.WriteLine($"Divisor={CubedDividedNumber8.Divisor}:{CubedDividedNumber8.MinValue}～{CubedDividedNumber8.MaxValue}");
            }
            CubedDividedNumber8.Divisor = 0x8000;
            for (int i = sbyte.MinValue; i <= sbyte.MaxValue; i++)
            {
                cdn.Value = (sbyte)i;
                double d = (double)cdn;
                Console.WriteLine($"{cdn.Value}:{d}");
            }
            for (double d = -2; d <= 2; d += 0.01)
            {
                cdn = (CubedDividedNumber8)d;
                Console.WriteLine($"{cdn.Value}:{d}");
            }
            for (double d = (double)CubedDividedNumber8.MinValue; d <= (double)CubedDividedNumber8.MaxValue; d += 1)
            {
                cdn = (CubedDividedNumber8)d;
                Console.WriteLine($"{d}→{cdn.Value}→{(double)cdn}");
            }

            Console.WriteLine();
        }

        public static void FractionTest()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(FractionTest)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            Console.WriteLine($"double → Fraction");
            for (double i = -10; i <= 10; i++)
            {
                Fraction<long> fraction = new Fraction<long>(i);
                fraction.Reduce();
                Console.WriteLine($"{i} → {fraction}");
            }
            Console.WriteLine($"decimal → Fraction");
            for (decimal i = -10; i <= 10; i++)
            {
                Fraction<long> fraction = new Fraction<long>(i);
                fraction.Reduce();
                Console.WriteLine($"{i} → {fraction}");
            }
            // 数学定数を計算
            if (false)
            {
                var exp = (double)Ksnm.Math.CalculateE<Fraction32>(Fraction32.Epsilon, 5);
                var pi = (double)Ksnm.Math.CalculatePi<Fraction32>();
            }

            {
                var bigFraction = new BigFraction(1, 2);// 1/2
                Console.WriteLine(bigFraction.ToString());
            }

            {
                var bigFraction = new BigFraction(1, 3);// 1/3
                Console.WriteLine(bigFraction.ToString());
            }

            {
                Console.WriteLine($"{nameof(Fraction<Int128>)}");
                Fraction<Int128> fraction = new();

                fraction = (Fraction<Int128>)10.0;
                Console.WriteLine($"10.0  = {fraction}");
                fraction = (Fraction<Int128>)12.3;
                Console.WriteLine($"12.3  = {fraction}");

                fraction = (Fraction<Int128>)10.0m;
                Console.WriteLine($"10.0m = {fraction}");
                fraction = (Fraction<Int128>)12.3m;
                Console.WriteLine($"12.3m = {fraction}");

                fraction = (Fraction<Int128>)Int128.Parse("123456789012345678901234567890123456789");
                Console.WriteLine($"123456789012345678901234567890123456789 = {fraction}");
                fraction /= (Fraction<Int128>)Int128.Parse("100000000000000000000000000000000000000");
                Console.WriteLine($"{fraction}={(double)fraction}");

                fraction = (Fraction<Int128>)Int128.Parse("123");
                fraction /= (Fraction<Int128>)Int128.Parse("100000000000000000000000000000000000000");
                Console.WriteLine($"{fraction}={(double)fraction}");
            }

            Console.WriteLine();
        }

        public static void FloatingPointNumber16E8M7Test()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(FloatingPointNumber16E8M7Test)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            BFloat16 bfp = new();

#if false
            for (float i = -10; i <= 10; i += 0.5f)
            {
                bfp = (BFloat16)i;
                ConsoleWriteLine(bfp);
                Console.WriteLine();
            }

            for (double d = -10; d <= +10; d += 0.01)
            {
                bfp = (BFloat16)d;
                Console.WriteLine($"{d}→{bfp}→{(double)bfp}");
            }
#endif

            {
                BFloat16 a = (BFloat16)2;
                BFloat16 b = (BFloat16)3;
                Console.WriteLine($"a={a}");
                Console.WriteLine($"b={b}");
                Console.WriteLine($"a + b={a + b}");
                Console.WriteLine($"a - b={a - b}");
                Console.WriteLine($"b - a={b - a}");
                Console.WriteLine($"a * b={a * b}");
                Console.WriteLine($"a / b={a / b}");
                Console.WriteLine($"b / a={b / a}");
            }

            Console.WriteLine();
        }

        public static void FloatingPointNumber8S1E4M3B7Test()
        {
            Console.WriteLine($"{Separator}");
            Console.WriteLine($"{nameof(FloatingPointNumber8S1E4M3B7Test)} {Ksnm.Debug.GetFilePathAndLineNumber()}");
            Console.WriteLine();

            FloatingPointNumber8S1E4M3B7 fp8 = new();

            for (int i = 0; i < 0xFF; i++)
            {
                fp8.Bits = (byte)i;
                ConsoleWriteLine(fp8);
            }
            for (int i = 0; i < 0xFF; i++)
            {
                fp8.Bits = (byte)i;
                Console.WriteLine($"{i}\t{fp8}");
            }

            for (float i = -2; i <= 2; i += 0.5f)
            {
                fp8 = (FloatingPointNumber8S1E4M3B7)i;
                ConsoleWriteLine(fp8);
                Console.WriteLine();
            }

            for (double d = -2; d <= +2; d += 0.01)
            {
                fp8 = (FloatingPointNumber8S1E4M3B7)d;
                Console.WriteLine($"{d}→{fp8}→{(double)fp8}");
            }

            Console.WriteLine();
        }

        public static void FixedPointNumber()
        {
            //Algorithm.GaussLegendre
        }

        static void ConsoleWriteLine<TBits>(IFloatingPointProperties<TBits> value)
            where TBits : INumber<TBits>
        {
            var reconstruction = value.Coefficient * value.Scale * value.Sign;
            Console.WriteLine($"Value       :{value}");
            Console.WriteLine($"復元        :{reconstruction}");
            Console.WriteLine($"Bits        :0x{value.Bits:X}");
            Console.WriteLine($"SignBit     :{value.SignBit}");
            Console.WriteLine($"ExponentBits:0x{value.ExponentBits:X}");
            Console.WriteLine($"MantissaBits:0x{value.MantissaBits:X}");
            Console.WriteLine($"Sign        :{value.Sign}");
            Console.WriteLine($"Exponent    :{value.Exponent}");
            Console.WriteLine($"Mantissa    :0x{value.Mantissa:X}");
            Console.WriteLine($"Scale       :{value.Scale}");
            Console.WriteLine($"Coefficient :{value.Coefficient}");
            Console.WriteLine();
        }
    }
}