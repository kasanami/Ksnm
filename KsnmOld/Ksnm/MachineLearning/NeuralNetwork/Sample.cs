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
using Ksnm.ExtensionMethods.System.Array;
using Ksnm.MachineLearning.NeuralNetwork;
using Ksnm.ExtensionMethods.System.Random;

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// AIを学習に使用するサンプルデータ
    /// </summary>
    public class Sample
    {
        #region フィールド
        /// <summary>
        /// 入力ニューロンに設定する値
        /// </summary>
        public double[] SourceValues;
        /// <summary>
        /// 出力ニューロンに期待する値
        /// </summary>
        public double[] ResultValues;
        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Sample()
        {
        }
        /// <summary>
        /// 指定した値で初期化
        /// </summary>
        public Sample(in double[] sourceValues, in double[] resultValues)
        {
            SourceValues = (double[])sourceValues.Clone();
            ResultValues = (double[])resultValues.Clone();
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public Sample(in Sample source)
        {
            SourceValues = (double[])source.SourceValues.Clone();
            ResultValues = (double[])source.ResultValues.Clone();
        }
        #endregion コンストラクタ

        #region Set
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetSourceValues(IReadOnlyList<double> values)
        {
            SourceValues.CopyFrom(values);
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetSourceValues(in double[,] values)
        {
            SourceValues.CopyFrom(values);
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetSourceValues(in double[,,] values)
        {
            SourceValues.CopyFrom(values);
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetResultValues(IReadOnlyList<double> values)
        {
            ResultValues.CopyFrom(values);
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetResultValues(in double[,] values)
        {
            ResultValues.CopyFrom(values);
        }
        /// <summary>
        /// 値設定
        /// </summary>
        public void SetResultValues(in double[,,] values)
        {
            ResultValues.CopyFrom(values);
        }
        #endregion Set
        /// <summary>
        /// 乱数による調整
        /// </summary>
        public void Randomization(Random random, double range)
        {
            for (int i = 0; i < SourceValues.Length; i++)
            {
                SourceValues[i] += random.Range(-range, range);
            }
        }
    }
}