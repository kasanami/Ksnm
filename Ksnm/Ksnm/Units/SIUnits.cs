/*
The zlib License

Copyright (c) 2021 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
using Ksnm.Numerics;
using Ksnm.Units.SI;

namespace Ksnm.Units
{
    /// <summary>
    /// SI各単位の 1 を定義
    /// 使用例：
    /// var mass = 123 * Gram;
    /// </summary>
    public static class SIUnits<T> where T : IMath<T>
    {
        public static readonly Ampere<T> Ampere = new Ampere<T>(1);
        public static readonly Candela<T> Candela = new Candela<T>(1);
        public static readonly Coulomb<T> Coulomb = new Coulomb<T>(1);
        public static readonly CubicMetre<T> CubicMetre = new CubicMetre<T>(1);
        public static readonly DegreeCelsius<T> DegreeCelsius = new DegreeCelsius<T>(1);
        public static readonly Gram<T> Gram = new Gram<T>(1);
        public static readonly Hour<T> Hour = new Hour<T>(1);
        public static readonly Joule<T> Joule = new Joule<T>(1);
        public static readonly Kelvin<T> Kelvin = new Kelvin<T>(1);
        public static readonly Kilogram<T> Kilogram = new Kilogram<T>(1);
        public static readonly Lumen<T> Lumen = new Lumen<T>(1);
        public static readonly Lux<T> Lux = new Lux<T>(1);
        public static readonly Metre<T> Metre = new Metre<T>(1);
        public static readonly MetrePerSecond<T> MetrePerSecond = new MetrePerSecond<T>(1);
        public static readonly MetrePerSecondSquared<T> MetrePerSecondSquared = new MetrePerSecondSquared<T>(1);
        public static readonly Minute<T> Minute = new Minute<T>(1);
        public static readonly Mole<T> Mole = new Mole<T>(1);
        public static readonly Newton<T> Newton = new Newton<T>(1);
        public static readonly Pascal<T> Pascal = new Pascal<T>(1);
        public static readonly Radian<T> Radian = new Radian<T>(1);
        public static readonly Second<T> Second = new Second<T>(1);
        public static readonly SquareMetre<T> SquareMetre = new SquareMetre<T>(1);
        public static readonly Steradian<T> Steradian = new Steradian<T>(1);
        public static readonly Volt<T> Volt = new Volt<T>(1);
        public static readonly Watt<T> Watt = new Watt<T>(1);
    }
}