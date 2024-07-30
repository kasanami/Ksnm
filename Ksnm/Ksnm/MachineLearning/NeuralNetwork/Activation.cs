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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 活性化関数とその導関数
    /// </summary>
    public class Activation
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 活性化関数
        /// </summary>
        public Func<double, double> Function { get; private set; }
        /// <summary>
        /// 導関数
        /// </summary>
        public Func<double, double> DerivativeFunction { get; private set; }
        /// <summary>
        /// 指定した値で初期化する。
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="function">活性化関数</param>
        /// <param name="derivativeFunction">導関数</param>
        public Activation(string name, Func<double, double> function, Func<double, double> derivativeFunction)
        {
            Name = name;
            Function = function;
            DerivativeFunction = derivativeFunction;
        }
        #region
        /// <summary>
        /// 恒等関数
        /// </summary>
        public static readonly Activation Identity = new Activation("Identity", Math.Identity, Math.DerIdentity);
        /// <summary>
        /// 単位ステップ関数
        /// </summary>
        public static readonly Activation UnitStep = new Activation("UnitStep", Math.UnitStep, Math.DerUnitStep);
        /// <summary>
        /// ヘヴィサイドの階段関数
        /// </summary>
        public static readonly Activation HeavisideStep = new Activation("HeavisideStep", Math.HeavisideStep, Math.DerHeavisideStep);
        /// <summary>
        /// LeakyReLU
        /// </summary>
        public static readonly Activation LeakyReLU = new Activation("LeakyReLU", Math.LeakyReLU, Math.DerLeakyReLU);
        /// <summary>
        /// シグモイド関数
        /// </summary>
        public static readonly Activation Sigmoid = new Activation("Sigmoid", Math.Sigmoid, Math.DerSigmoid);
        /// <summary>
        /// ソフトプラス関数
        /// </summary>
        public static readonly Activation Softplus = new Activation("Softplus", Math.Softplus, Math.DerSoftplus);
        /// <summary>
        /// 双曲線正接関数
        /// </summary>
        public static readonly Activation Tanh = new Activation("Tanh", Math.Tanh, Math.DerTanh);
        /// <summary>
        /// 正規化線形関数
        /// </summary>
        public static readonly Activation ReLU = new Activation("ReLU", Math.ReLU, Math.DerReLU);
        #endregion
    }
}