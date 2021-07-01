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
using System.Collections.Generic;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// ニューロンインターフェイス
    /// </summary>
    public interface INeuron
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 計算結果値
        /// </summary>
        double Value { get; set; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IReadOnlyList<INeuron> InputNeurons { get; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IList<double> InputWeights { get; }
        /// <summary>
        /// 
        /// </summary>
        void Randomization(double expectedValue, double learningRate);
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        void Backpropagation(double expectedValue, double learningRate);
        /// <summary>
        /// バックプロパゲーション
        /// </summary>
        void Backpropagation(double expectedValue, double learningRate, double nextDelta, double nextWeight);
    }
}