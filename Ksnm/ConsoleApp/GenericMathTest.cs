using Ksnm.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Authentication;
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

            if (true)
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
                CreateSaturatingTest<double, Half>(0.123456789);
                CreateSaturatingTest<double, Half>(123456789);
                Console.WriteLine();

                Console.WriteLine("CreateTruncatingTest");
                CreateTruncatingTest<float>(0.1f);
                CreateTruncatingTest<double>(0.1);
                CreateTruncatingTest<float>(float.MaxValue);
                CreateTruncatingTest<double>(double.MaxValue);
                CreateTruncatingTest<decimal>(decimal.MaxValue);
                CreateTruncatingTest<double, byte>(double.MaxValue);
                CreateTruncatingTest<double, float>(double.Epsilon);
                CreateTruncatingTest<double, float>(double.MaxValue);
                CreateTruncatingTest<double, Half>(0.123456789);
                CreateTruncatingTest<double, Half>(123456789);
                Console.WriteLine();
            }

            if (true)
            {
                Console.WriteLine("byte");

                Console.WriteLine("largeValue");
                // 大きな値を byte 型に変換
                int largeValue = 300;

                byte castedValue = (byte)largeValue;
                Console.WriteLine($"cast            : {castedValue}");

                // CreateTruncating: 下位ビットを使用
                byte truncatedValue = byte.CreateTruncating(largeValue);
                Console.WriteLine($"CreateTruncating: {truncatedValue}"); // 300 の下位 8 ビットだけを取る -> 44

                // CreateSaturating: 最大値にクリップされる
                byte saturatedValue = byte.CreateSaturating(largeValue);
                Console.WriteLine($"CreateSaturating: {saturatedValue}"); // byte の最大値 255

                Console.WriteLine("smallValue");
               int smallValue = -100;

                byte castedSmallValue = (byte)smallValue;
                Console.WriteLine($"cast            : {castedSmallValue}");

                byte truncatedSmallValue = byte.CreateTruncating(smallValue);
                Console.WriteLine($"CreateTruncating: {truncatedSmallValue}"); // -> 156 (-100 の 8 ビット表現)

                byte saturatedSmallValue = byte.CreateSaturating(smallValue);
                Console.WriteLine($"CreateSaturating: {saturatedSmallValue}"); // -> 0
            }

            if (true)
            {
                Console.WriteLine("Half");

                Console.WriteLine("largeValue");
                // 大きな値を byte 型に変換
                double largeValue = 12345;

                Half castedValue = (Half)largeValue;
                Console.WriteLine($"cast            : {castedValue}");

                // CreateTruncating: 下位ビットを使用
                Half truncatedValue = Half.CreateTruncating(largeValue);
                Console.WriteLine($"CreateTruncating: {truncatedValue}");

                // CreateSaturating: 最大値にクリップされる
                Half saturatedValue = Half.CreateSaturating(largeValue);
                Console.WriteLine($"CreateSaturating: {saturatedValue}");

                Console.WriteLine("smallValue");
                double smallValue = -0.12345;

                Half castedSmallValue = (Half)smallValue;
                Console.WriteLine($"cast            : {castedSmallValue}");

                Half truncatedSmallValue = Half.CreateTruncating(smallValue);
                Console.WriteLine($"CreateTruncating: {truncatedSmallValue}");

                Half saturatedSmallValue = Half.CreateSaturating(smallValue);
                Console.WriteLine($"CreateSaturating: {saturatedSmallValue}");
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