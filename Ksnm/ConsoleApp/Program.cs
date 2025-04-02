using BenchmarkDotNet.Running;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<StringConcatMesurement>();
            NumericsTest.Run();
            //BenchmarkRunner.Run<NumericsMesurement>();
            //GraphicsTest.Run();
            //ConjectureTest.Run();
            //MathTest.Run();
            //GenericMathTest.Run();
            //AITest.Run();
            //BenchmarkRunner.Run<AIMesurement>();
        }
    }
}