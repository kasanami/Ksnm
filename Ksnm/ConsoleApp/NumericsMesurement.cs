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
    public class NumericsMesurement
    {
        [Benchmark]
        public void TestInt64()
        {
            Test<Int64>();
            //TestMatrix<Int64>();
        }
        [Benchmark]
        public void TestFloat64()
        {
            Test<Float64>();
            //TestMatrix<Float64>();
        }
        [Benchmark]
        public void TestFloat32()
        {
            Test<Float32>();
            //TestMatrix<Float32>();
        }
        [Benchmark]
        public void TestFloat16()
        {
            Test<Float16>();
            //TestMatrix<Float16>();
        }
        [Benchmark]
        public void TestBFloat16()
        {
            Test<BFloat16>();
            //TestMatrix<BFloat16>();
        }
        [Benchmark]
        public void TestFloat8()
        {
            Test<Float8>();
        }
        [Benchmark]
        public void TestFixed64()
        {
            Test<Fixed64>();
            //TestMatrix<Fixed32>();
        }
        [Benchmark]
        public void TestFixed32()
        {
            Test<Fixed32>();
            //TestMatrix<Fixed32>();
        }
        [Benchmark]
        public void TestFixed16()
        {
            Test<Fixed16>();
            //TestMatrix<Fixed16>();
        }
#if false
        [Benchmark]
        public void TestMatrix4x4()
        {
            Matrix4x4 matrix1 = new
            (
                1, 2, 3, 4 ,
                1, 2, 3, 4 ,
                1, 2, 3, 4 ,
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
#endif
        static void TestMatrix<T>()
            where T : INumber<T>, IFloatingPointIeee754<T>, IMinMaxValue<T>
        {
            var _0 = T.Zero;
            var _1 = T.One;
            var _2 = T.CreateChecked(2);
            var _3 = T.CreateChecked(3);
            var _4 = T.CreateChecked(4);
            var _5 = T.CreateChecked(5);
            var _6 = T.CreateChecked(6);
            var _7 = T.CreateChecked(7);
            var _8 = T.CreateChecked(8);
            var _9 = T.CreateChecked(9);

            Matrix<T> matrix1 = new T[,]
            {
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
            };
            Matrix<T> matrix2 = new T[,]
            {
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
                { _1, _2, _3, _4 },
            };
            var matrix3 = matrix1 * matrix2;
        }
        static void Test<T>() where T : INumber<T>
        {
            T value1 = T.CreateChecked(1);
            T value2 = T.CreateChecked(2);
            T value3 = value1 * value2;
            value3 *= value2;
        }
    }
}