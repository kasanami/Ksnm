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
    /// ニューロンインターフェイス
    /// </summary>
    public interface INeuron<TValue>
        where TValue : INumber<TValue>, IFloatingPointIeee754<TValue>
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 計算結果値
        /// ForwardPropagation()で更新される
        /// </summary>
        TValue Value { get; set; }
        /// <summary>
        /// バイアス
        /// </summary>
        TValue Bias { get; set; }
        /// <summary>
        /// 入力情報
        /// </summary>
        IReadOnlyList<NeuronInput<TValue>> Inputs { get; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IEnumerable<INeuron<TValue>> InputNeurons { get; }
        /// <summary>
        /// 入力の重み
        /// </summary>
        IEnumerable<TValue> InputWeights { get; }
        /// <summary>
        /// 活性化関数
        /// </summary>
        Activation<TValue> Activation { get; }
        /// <summary>
        /// 誤差項
        /// BackPropagation()で更新される
        /// </summary>
        TValue Delta { get; set; }
        #endregion プロパティ

        #region Input
        /// <summary>
        /// 指定したニューロンを持つInputを返す。
        /// 持っていなければnullを返す。
        /// </summary>
        NeuronInput<TValue> FindInput(INeuron<TValue> neuron);
        /// <summary>
        /// 指定したニューロンをInputに持っていれば、そのインデックスを返す。
        /// 持っていなければ-1を返す。
        /// </summary>
        int InputIndexOf(INeuron<TValue> neuron);
        #endregion Input

        #region インスタンス関係
        /// <summary>
        /// 複製を作成
        /// </summary>
        INeuron<TValue> Clone(IReadOnlyList<INeuron<TValue>> inputNeurons);
        #endregion インスタンス関係

        #region 学習関係
        /// <summary>
        /// 重みを指定した値に設定
        /// </summary>
        void ResetWeights(TValue weight);
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        void ResetWeights(Random random, TValue weightRange);
        /// <summary>
        /// 重みをランダムに調整
        /// </summary>
        void Randomization(Random random, TValue weightRange);
        /// <summary>
        /// 重みをランダムに調整
        /// </summary>
        void Randomization(Random random, TValue targetValue, TValue learningRate);
        /// <summary>
        /// フォワードプロパゲーション
        /// </summary>
        void ForwardPropagation();
        /// <summary>
        /// バックプロパゲーションの誤差更新
        /// </summary>
        void BackPropagationDelta(TValue targetValue);
        /// <summary>
        /// バックプロパゲーションの誤差更新
        /// </summary>
        void BackPropagationDelta(ILayer<TValue> beforeLayer);
        /// <summary>
        /// バックプロパゲーションの重み更新
        /// </summary>
        /// <param name="learningRate">学習係数</param>
        void BackPropagationWeight(TValue learningRate);
        /// <summary>
        /// 弱い結合を削除
        /// </summary>
        /// <param name="threshold">Weightの絶対値がこの値以下なら削除</param>
        void Reduce(TValue threshold);
        #endregion 学習関係
    }
}