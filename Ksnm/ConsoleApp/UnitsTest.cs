using Ksnm.Numerics;
using Ksnm.Science.Mathematics;
using System.Numerics;
using static Ksnm.Science.Mathematics.Formula;
using static Ksnm.Science.Mathematics.Theorem;

using Ampere = Ksnm.Units.SI.Ampere<double>;
using Coulomb = Ksnm.Units.SI.Coulomb<double>;
using Joule = Ksnm.Units.SI.Joule<double>;
using Second = Ksnm.Units.SI.Second<double>;
using Volt = Ksnm.Units.SI.Volt<double>;
using Watt = Ksnm.Units.SI.Watt<double>;

#pragma warning disable CS0162 // 到達できないコードが検出されました
namespace ConsoleApp;

internal class UnitsTest
{
    public static void Run()
    {
        //if (false)
        {
            Console.WriteLine("I=Q/t");
            Coulomb q = 2;
            Second t = 1;
            Ampere i = q / t;
            Console.WriteLine($"{i}={q}/{t}");
        }
        {
            Console.WriteLine("V=J/Q");
            Coulomb q = 2;
            Joule j = 6;
            Volt v = j / q;
            Console.WriteLine($"{v}={j}/{q}");
        }
        {
            Console.WriteLine("W=J/t");
            Joule j = 6;
            Second t = 1;
            Watt w = j / t;
            Console.WriteLine($"{w}={j}/{t}");
        }
        {
            Volt v = 6;
            Coulomb q = 2;
            Joule j = v * q;
            Second t = 1;
            Ampere i = q / t;
            Watt w = v * q / t;
            Console.WriteLine("P=V*Q/t");
            Console.WriteLine($"{w}={v}*{q}/{t}");
            Console.WriteLine("P=V*I");
            Console.WriteLine($"{w}={v}*{i}");
        }
    }
}
#pragma warning restore CS0162 // 到達できないコードが検出されました