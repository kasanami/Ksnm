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
using static System.Math;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 活性化関数
        /// </summary>
        /// <param name="x">ニューロンの値に重みをかけたあとの合計値</param>
        /// <returns>結果</returns>
        public delegate double ActivationFunction(double x);
        /// <summary>
        /// 恒等関数関数
        /// </summary>
        public static double Identity(double x)
        {
            return x;
        }
        /// <summary>
        /// 恒等関数関数の導関数
        /// </summary>
        public static double DerIdentity(double x)
        {
            return 1;
        }
        /// <summary>
        /// シグモイド関数
        /// </summary>
        public static double Sigmoid(double x)
        {
            return Ksnm.Math.Sigmoid(x);
        }
        /// <summary>
        /// シグモイド関数の導関数
        /// </summary>
        public static double DerSigmoid(double x)
        {
            return x * (1.0 - x);
        }
        /// <summary>
        /// 双曲線正接関数
        /// </summary>
        public static double Tanh(double x)
        {
            var ePlus = Exp(x);
            var eMinus = Exp(-x);
            return (ePlus - eMinus) / (ePlus + eMinus);
        }
        /// <summary>
        /// 双曲線正接関数の導関数
        /// </summary>
        public static double DerTanh(double x)
        {
            return (1.0 - x * x);
        }
        /// <summary>
        /// 正規化線形関数
        /// </summary>
        public static double ReLU(double x)
        {
            return Max(0, x);
        }
        /// <summary>
        /// 正規化線形関数の導関数
        /// </summary>
        public static double DerReLU(double x)
        {
            if (x > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}