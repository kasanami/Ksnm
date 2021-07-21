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

namespace Ksnm.MachineLearning.NeuralNetwork
{
    /// <summary>
    /// AIを学習に使用するサンプルデータ
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// 入力ニューロンに設定する値
        /// </summary>
        public double[] SourceValues;
        /// <summary>
        /// 出力ニューロンに期待する値
        /// </summary>
        public double[] ResultValues;
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
        #endregion Set
    }
}