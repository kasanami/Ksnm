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

namespace Ksnm.MachineLearning.NeuralNetwork
{
    public static class Utility
    {
        /// <summary>
        /// 活性化関数
        /// </summary>
        /// <param name="value">ニューロンの値に重みをかけたあとの合計値</param>
        /// <returns>結果</returns>
        public delegate double ActivationFunction(double value);
        /// <summary>
        /// そのまま値を返す関数
        /// </summary>
        /// <param name="value">ニューロンの値に重みをかけたあとの合計値</param>
        /// <returns>入力された value そのまま</returns>
        public static double Keep(double value)
        {
            return value;
        }
        /// <summary>
        /// Keepの微分
        /// </summary>
        public static double DifferentiatedKeep(double value)
        {
            return 1;
        }
        /// <summary>
        /// シグモイド関数
        /// </summary>
        public static double Sigmoid(double value)
        {
            return Ksnm.Math.Sigmoid(value);
        }
        /// <summary>
        /// シグモイド関数の微分
        /// </summary>
        public static double DifferentiatedSigmoid(double value)
        {
            var sigmoid = Sigmoid(value);
            return sigmoid * (1.0 - sigmoid);
        }
        /// <summary>
        /// 正規化線形関数
        /// </summary>
        public static double ReLU(double value)
        {
            return System.Math.Max(0, value);
        }
        /// <summary>
        /// 正規化線形関数の微分
        /// </summary>
        public static double DifferentiatedReLU(double value)
        {
            if (value > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}