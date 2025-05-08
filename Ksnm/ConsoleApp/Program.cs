global using Math = Ksnm.Math;
global using MathD = System.Math;
global using MathF = System.MathF;
global using Float8 = Ksnm.Numerics.FloatingPointNumber8S1E4M3B7;
global using BFloat16 = Ksnm.Numerics.FloatingPointNumber16E8M7;
global using Float16 = System.Half;
global using Float32 = System.Single;
global using Float64 = System.Double;
global using Fixed16 = Ksnm.Numerics.FixedPointNumber16Q8;
global using Fixed32 = Ksnm.Numerics.FixedPointNumber32Q16;
global using Fixed64 = Ksnm.Numerics.FixedPointNumber64Q32;
global using Fraction32 = Ksnm.Numerics.Fraction<System.Int16>;
global using MatrixF64 = Ksnm.Numerics.Matrix<double>;
global using VectorF64 = Ksnm.Numerics.Vector<double>;
using BenchmarkDotNet.Running;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NumericsTest.Run();
            //GraphicsTest.Run();
            //ConjectureTest.Run();
            //MathTest.Run();
            //GenericMathTest.Run();
            //AITest.Run();

            // Benchmark
#if !DEBUG
            //var summary = BenchmarkRunner.Run<StringConcatMesurement>();
            //BenchmarkRunner.Run<NumericsMesurement>();
            //BenchmarkRunner.Run<AIMesurement>();
#endif
        }
    }
}