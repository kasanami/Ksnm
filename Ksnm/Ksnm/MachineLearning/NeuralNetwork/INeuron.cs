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

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// ニューロンインターフェイス
    /// </summary>
    public interface INeuron
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
        double Value { get; set; }
        /// <summary>
        /// バイアス
        /// </summary>
        double Bias { get; set; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IReadOnlyList<INeuron> InputNeurons { get; }
        /// <summary>
        /// 入力ニューロン
        /// </summary>
        IList<double> InputWeights { get; }
        /// <summary>
        /// 活性化関数
        /// </summary>
        Activation Activation { get; }
        /// <summary>
        /// 誤差項
        /// BackPropagation()で更新される
        /// </summary>
        double Delta { get; set; }
        #endregion プロパティ

        #region その他
        /// <summary>
        /// 指定したニューロンを入力に持っていれば、そのインデックスを返す。
        /// 持っていなければ-1を返す。
        /// </summary>
        int InputIndexOf(INeuron neuron);
        #endregion その他

        #region インスタンス関係
        /// <summary>
        /// 複製を作成
        /// </summary>
        INeuron Clone(IReadOnlyList<INeuron> inputNeurons);
        #endregion インスタンス関係

        #region 学習関係
        /// <summary>
        /// 重みを指定した値に設定
        /// </summary>
        void ResetWeights(double weight);
        /// <summary>
        /// 重みをランダムに設定
        /// </summary>
        void ResetWeights(Random random, double weightRange);
        /// <summary>
        /// 重みをランダムに調整
        /// </summary>
        void Randomization(Random random, double weightRange);
        /// <summary>
        /// 重みをランダムに調整
        /// </summary>
        void Randomization(Random random, double targetValue, double learningRate);
        /// <summary>
        /// フォワードプロパゲーション
        /// </summary>
        void ForwardPropagation();
        /// <summary>
        /// バックプロパゲーションの誤差更新
        /// </summary>
        void BackPropagationDelta(double targetValue);
        /// <summary>
        /// バックプロパゲーションの誤差更新
        /// </summary>
        void BackPropagationDelta(ILayer beforeLayer);
        /// <summary>
        /// バックプロパゲーションの重み更新
        /// </summary>
        /// <param name="learningRate">学習係数</param>
        void BackPropagationWeight(double learningRate);
        #endregion 学習関係
    }
}