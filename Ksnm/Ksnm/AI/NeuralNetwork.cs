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

namespace Ksnm.AI
{
    /// <summary>
    /// ニューラルネットワーク
    /// </summary>
    public class NeuralNetwork
    {
        /// <summary>
        /// 入力レイヤー
        /// </summary>
        public IEnumerable<Neuron> SourceNeurons { get => sourceNeurons; }
        private List<Neuron> sourceNeurons = new List<Neuron>();

        /// <summary>
        /// 中間レイヤー
        /// </summary>
        public IEnumerable<Neuron> HiddenNeurons { get => hiddenNeurons; }
        private List<Neuron> hiddenNeurons = new List<Neuron>();

        /// <summary>
        /// 出力レイヤー
        /// </summary>
        public IEnumerable<Neuron> ResultNeurons { get => resultNeurons; }
        private List<Neuron> resultNeurons = new List<Neuron>();

        /// <summary>
        /// ニューロン数ゼロで初期化
        /// </summary>
        public NeuralNetwork()
        {
        }
        /// <summary>
        /// 各レイヤーを指定したニューロン数で初期化
        /// </summary>
        public NeuralNetwork(int sourceCount, int hiddenCount, int resultCount)
        {
            for (int i = 0; i < sourceCount; i++)
            {
                Neuron neuron = new Neuron();
                sourceNeurons.Add(neuron);
            }
            for (int i = 0; i < hiddenCount; i++)
            {
                Neuron neuron = new Neuron(SourceNeurons);
                hiddenNeurons.Add(neuron);
            }
            for (int i = 0; i < resultCount; i++)
            {
                Neuron neuron = new Neuron(HiddenNeurons);
                resultNeurons.Add(neuron);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            foreach (var item in sourceNeurons)
            {
                item.Update();
            }
            foreach (var item in hiddenNeurons)
            {
                item.Update();
            }
            foreach (var item in resultNeurons)
            {
                item.Update();
            }
        }
    }
}