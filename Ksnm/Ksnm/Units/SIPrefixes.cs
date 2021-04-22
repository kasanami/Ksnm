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
namespace Ksnm.Units
{
    /// <summary>
    /// SI接頭辞一覧
    /// 使用例：
    /// using static Ksnm.Units.SIPrefixes;
    /// var mass = 123 * Kilo * Gram;
    /// ※Kilogram には変換されない
    /// </summary>
    public static class SIPrefixes
    {
        /// <summary>
        /// ヨタ
        /// </summary>
        public const decimal Yotta = 1_000_000_000_000_000_000_000_000m;
        /// <summary>
        /// ゼタ
        /// </summary>
        public const decimal Zetta = 1_000_000_000_000_000_000_000m;
        /// <summary>
        /// エクサ
        /// </summary>
        public const decimal Exa = 1_000_000_000_000_000_000m;
        /// <summary>
        /// ペタ
        /// </summary>
        public const decimal Peta = 1_000_000_000_000_000m;
        /// <summary>
        /// テラ
        /// </summary>
        public const decimal Tera = 1_000_000_000_000m;
        /// <summary>
        /// ギガ
        /// </summary>
        public const decimal Giga = 1_000_000_000m;
        /// <summary>
        /// メガ
        /// </summary>
        public const decimal Mega = 1_000_000m;
        /// <summary>
        /// キロ
        /// </summary>
        public const decimal Kilo = 1_000m;
        /// <summary>
        /// ヘクト
        /// </summary>
        public const decimal Hecto = 100m;
        /// <summary>
        /// デカ
        /// </summary>
        public const decimal Deca = 10m;
        /// <summary>
        /// デシ
        /// </summary>
        public const decimal Deci = 0.1m;
        /// <summary>
        /// センチ
        /// </summary>
        public const decimal Centi = 0.01m;
        /// <summary>
        /// ミリ
        /// </summary>
        public const decimal Milli = 0.001m;
        /// <summary>
        /// マイクロ
        /// </summary>
        public const decimal Micro = 0.000_001m;
        /// <summary>
        /// ナノ
        /// </summary>
        public const decimal Nano = 0.000_000_001m;
        /// <summary>
        /// ピコ
        /// </summary>
        public const decimal Pico = 0.000_000_000_001m;
        /// <summary>
        /// フェムト
        /// </summary>
        public const decimal Femto = 0.000_000_000_000_001m;
        /// <summary>
        /// アト
        /// </summary>
        public const decimal Atto = 0.000_000_000_000_000_001m;
        /// <summary>
        /// ゼプト
        /// </summary>
        public const decimal Zepto = 0.000_000_000_000_000_000_001m;
        /// <summary>
        /// ヨクト
        /// </summary>
        public const decimal Yocto = 0.000_000_000_000_000_000_000_001m;
    }
}
