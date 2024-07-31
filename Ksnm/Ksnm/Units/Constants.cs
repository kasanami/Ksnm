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
using Ksnm.Units.GS;
using Ksnm.Units.NonSI;
using Ksnm.Units.SI;
using System.Numerics;

namespace Ksnm.Units
{
    /// <summary>
    /// 各単位の定数を定義
    /// <para>使用例 : var mass = 123 * Gram;</para>
    /// </summary>
    public static class Constants<T> where T : INumber<T>
    {
        #region GS
        public static readonly KilogramForce<T> kilogramForce = new KilogramForce<T>(1);
        public static readonly StandardGravity<T> standardGravity = new StandardGravity<T>(1);
        #endregion GS
        #region NonSI
        public static readonly Calorie<T> calorie = new Calorie<T>(1);
        public static readonly Knot<T> knot = new Knot<T>(1);
        public static readonly NauticalMile<T> nauticalMile = new NauticalMile<T>(1);
        #endregion NonSI
        #region SI
        public static readonly Ampere<T> ampere = new Ampere<T>(1);
        public static readonly Candela<T> candela = new Candela<T>(1);
        public static readonly Coulomb<T> coulomb = new Coulomb<T>(1);
        public static readonly CubicMetre<T> cubicMetre = new CubicMetre<T>(1);
        public static readonly DegreeCelsius<T> degreeCelsius = new DegreeCelsius<T>(1);
        public static readonly Gram<T> gram = new Gram<T>(1);
        public static readonly Hour<T> hour = new Hour<T>(1);
        public static readonly Joule<T> joule = new Joule<T>(1);
        public static readonly Kelvin<T> kelvin = new Kelvin<T>(1);
        public static readonly Kilogram<T> kilogram = new Kilogram<T>(1);
        public static readonly KiloMetrePerHour<T> kiloMetrePerHour = new KiloMetrePerHour<T>(1);
        public static readonly Litre<T> litre = new Litre<T>(1);
        public static readonly Lumen<T> lumen = new Lumen<T>(1);
        public static readonly Lux<T> lux = new Lux<T>(1);
        public static readonly Metre<T> metre = new Metre<T>(1);
        public static readonly MetrePerSecond<T> metrePerSecond = new MetrePerSecond<T>(1);
        public static readonly MetrePerSecondSquared<T> metrePerSecondSquared = new MetrePerSecondSquared<T>(1);
        public static readonly Minute<T> minute = new Minute<T>(1);
        public static readonly Mole<T> mole = new Mole<T>(1);
        public static readonly Newton<T> newton = new Newton<T>(1);
        public static readonly Pascal<T> pascal = new Pascal<T>(1);
        public static readonly Radian<T> radian = new Radian<T>(1);
        public static readonly Second<T> second = new Second<T>(1);
        public static readonly SquareMetre<T> squareMetre = new SquareMetre<T>(1);
        public static readonly Steradian<T> steradian = new Steradian<T>(1);
        public static readonly Volt<T> volt = new Volt<T>(1);
        public static readonly Watt<T> watt = new Watt<T>(1);
        #endregion SI
    }
}