/*
The zlib License

Copyright (c) 2021-2024 Takahiro Kasanami

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
using System.Numerics;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// 情報元のニューロン　Valueは設定後は変化しない。
    /// </summary>
    public class SourceNeuron<TValue> : INeuron<TValue>
        where TValue : INumber<TValue>, IFloatingPointIeee754<TValue>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 現在の値
        /// Update()で更新されない。
        /// </summary>
        public TValue Value { get; set; } = TValue.Zero;
        /// <summary>
        /// バイアス
        /// </summary>
        public TValue Bias { get; set; } = TValue.Zero;
        /// <summary>
        /// 入力無し
        /// </summary>
        public IReadOnlyList<NeuronInput<TValue>> Inputs { get; private set; } = [];
        /// <summary>
        /// 入力無し
        /// </summary>
        public IEnumerable<INeuron<TValue>> InputNeurons { get; private set; } = [];
        /// <summary>
        /// 入力無し
        /// </summary>
        public IEnumerable<TValue> InputWeights { get; private set; } = [];
        /// <summary>
        /// 活性化関数
        /// </summary>
        public Activation<TValue> Activation { get; } = null;
        /// <summary>
        /// 誤差項
        /// BackPropagation()で更新される
        /// </summary>
        public TValue Delta { get; set; }
        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// デフォルト値で初期化
        /// </summary>
        public SourceNeuron()
        {
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public SourceNeuron(SourceNeuron<TValue> source)
        {
            Name = source.Name;
            Value = source.Value;
        }
        #endregion コンストラクタ

        #region Input
        /// <summary>
        /// 指定したニューロンを持つInputを返す。
        /// 持っていなければnullを返す。
        /// </summary>
        public NeuronInput<TValue> FindInput(INeuron<TValue> neuron)
        {
            return null;
        }
        /// <summary>
        /// 指定したニューロンを入力に持っていれば、そのインデックスを返す。
        /// 持っていなければ-1を返す。
        /// </summary>
        public int InputIndexOf(INeuron<TValue> neuron)
        {
            return -1;
        }
        #endregion Input

        /// <summary>
        /// 複製を作成
        /// </summary>
        public INeuron<TValue> Clone(IReadOnlyList<INeuron<TValue>> inputNeurons)
        {
            return new SourceNeuron<TValue>(this);
        }

        public void ResetWeights(TValue weight)
        {
            // 何もしない
        }

        public void ResetWeights(Random random, TValue weightRange)
        {
            // 何もしない
        }

        public void Randomization(Random random, TValue weightRange)
        {
            // 何もしない
        }

        public void Randomization(Random random, TValue expectedValue, TValue learningRate)
        {
            // 何もしない
        }

        public void BackPropagationDelta(TValue targetValue)
        {
            // 何もしない
        }

        public void BackPropagationDelta(ILayer<TValue> beforeLayer)
        {
            // 何もしない
        }
        public void BackPropagationWeight(TValue learningRate)
        {
            // 何もしない
        }

        public void ForwardPropagation()
        {
            // 何もしない
        }
        #region Object
        public override string ToString()
        {
            return $"{{{nameof(Name)}:{Name},{nameof(Value)}:{Value}}}";
        }
        #endregion Object
    }
}