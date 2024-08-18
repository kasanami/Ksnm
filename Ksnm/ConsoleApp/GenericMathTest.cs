using Ksnm.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class GenericMathTest
    {
        public static void Run()
        {
            if (false)
            {
                RadixTest<byte>();
                RadixTest<char>();
                RadixTest<int>();
                RadixTest<Half>();
                RadixTest<float>();
                RadixTest<double>();
                RadixTest<decimal>();
                RadixTest<BigInteger>();
                RadixTest<Complex>();
            }

            if (false)
            {
                Console.WriteLine("CreateSaturatingTest");
                CreateSaturatingTest<float>(0.1f);
                CreateSaturatingTest<double>(0.1);
                CreateSaturatingTest<float>(float.MaxValue);
                CreateSaturatingTest<double>(double.MaxValue);
                CreateSaturatingTest<decimal>(decimal.MaxValue);
                CreateSaturatingTest<double, byte>(double.MaxValue);
                CreateSaturatingTest<double, float>(double.Epsilon);
                CreateSaturatingTest<double, float>(double.MaxValue);

                Console.WriteLine("CreateTruncatingTest");
                CreateTruncatingTest<float>(0.1f);
                CreateTruncatingTest<double>(0.1);
                CreateTruncatingTest<float>(float.MaxValue);
                CreateTruncatingTest<double>(double.MaxValue);
                CreateTruncatingTest<decimal>(decimal.MaxValue);
                CreateTruncatingTest<double, byte>(double.MaxValue);
                CreateTruncatingTest<double, float>(double.Epsilon);
                CreateTruncatingTest<double, float>(double.MaxValue);
            }
        }
        public static void RadixTest<T>() where T : INumberBase<T>
        {
            RadixTest<T>(T.Zero);
        }
        public static void RadixTest<T>(T value) where T : INumberBase<T>
        {
            Console.WriteLine($"{value.GetType().Name}.Radix={T.Radix}");
        }
        public static void CreateSaturatingTest<TFrom>(TFrom from) where TFrom : INumberBase<TFrom>
        {
            int intValue = int.CreateSaturating(from);
            Console.WriteLine($"0x{intValue:00000000}<={from}");
        }
        public static void CreateTruncatingTest<TFrom>(TFrom from) where TFrom : INumberBase<TFrom>
        {
            int intValue = int.CreateTruncating(from);
            Console.WriteLine($"0x{intValue:00000000}<={from}");
        }
        public static void CreateSaturatingTest<TFrom, TTo>(TFrom from)
            where TFrom : INumberBase<TFrom>
            where TTo : INumberBase<TTo>
        {
            TTo to = TTo.CreateSaturating(from);
            Console.WriteLine($"{to}<={from}");
        }
        public static void CreateTruncatingTest<TFrom, TTo>(TFrom from)
            where TFrom : INumberBase<TFrom>
            where TTo : INumberBase<TTo>
        {
            TTo to = TTo.CreateTruncating(from);
            Console.WriteLine($"{to}<={from}");
        }
    }
}