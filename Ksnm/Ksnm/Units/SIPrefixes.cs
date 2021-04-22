using Ksnm.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
