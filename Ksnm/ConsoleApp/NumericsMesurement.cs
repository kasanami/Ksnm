using BenchmarkDotNet.Attributes;
using Ksnm.ExtensionMethods.System.Collections.Generic.Enumerable;
using Ksnm.MachineLearning.NeuralNetwork;
using Ksnm.Numerics;
using System.Numerics;
using System.Text;

namespace ConsoleApp
{
    using Float8 = Ksnm.Numerics.FloatingPointNumber8S1E4M3B7;
    using BFloat16 = Ksnm.Numerics.FloatingPointNumber16E8M7;
    using Float16 = System.Half;
    using Float32 = System.Single;
    using Float64 = System.Double;

    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class NumericsMesurement
    {
        [Benchmark]
        public void TestMatrix_Float64()
        {
            TestMatrix<Float64>();
        }
        [Benchmark]
        public void TestMatrix_Float32()
        {
            TestMatrix<Float32>();
        }
        [Benchmark]
        public void TestMatrix_Float16()
        {
            TestMatrix<Float16>();
        }
        [Benchmark]
        public void TestMatrix_BFloat16()
        {
            TestMatrix<BFloat16>();
        }
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
        //[Benchmark]
        //public void TestLogicGate_Float8()
        //{
        //    TestLogicGate<Float8>();
        //}
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

    }
}
