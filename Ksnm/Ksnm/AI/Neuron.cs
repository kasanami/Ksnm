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
using System.Linq;

namespace Ksnm.AI
{
    /// <summary>
    /// ニューロン
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name;
        /// <summary>
        /// 現在の値
        /// Update()で更新される。
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// バイアス
        /// Update()時に最後に加算される値
        /// 入力ニューロンの場合は、これに入力値を設定する。
        /// </summary>
        public double Bias { get; set; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        public IEnumerable<Neuron> InputNeurons = new Neuron[0];
        /// <summary>
        /// 重み
        /// </summary>
        public double[] InputWeights = new double[0];
        /// <summary>
        /// 活性化関数
        /// </summary>
        public ActivationFunction Activation = DefaultActivationFunction;
        /// <summary>
        /// 活性化関数
        /// </summary>
        /// <param name="value">ニューロンの値に重みをかけたあとの合計値</param>
        /// <returns>結果</returns>
        public delegate double ActivationFunction(double value);
        /// <summary>
        /// デフォルトの活性化関数
        /// </summary>
        /// <param name="value">ニューロンの値に重みをかけたあとの合計値</param>
        /// <returns>入力された value そのまま</returns>
        public static double DefaultActivationFunction(double value)
        {
            return value;
        }
        /// <summary>
        /// 入力ニューロン無しで初期化
        /// </summary>
        public Neuron()
        {
        }
        /// <summary>
        /// 入力ニューロンを指定して初期化
        /// </summary>
        public Neuron(IEnumerable<Neuron> inputNeurons)
        {
            InputNeurons = inputNeurons;
            InputWeights = new double[inputNeurons.Count()];
            for (int i = 0; i < InputWeights.Length; i++)
            {
                InputWeights[i] = 1;
            }
        }
        /// <summary>
        /// Valueを更新
        /// </summary>
        public void Update()
        {
            double sum = 0.0;
            System.Diagnostics.Debug.Assert(InputNeurons.Count() == InputWeights.Count());
            var count = InputNeurons.Count();
            for (int i = 0; i < count; i++)
            {
                sum += InputNeurons.ElementAt(i).Value * InputWeights.ElementAt(i);
            }
            Value = Activation(sum) + Bias;
        }
    }
}