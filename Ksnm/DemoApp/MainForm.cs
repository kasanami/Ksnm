using Ksnm.Randoms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ksnm.ExtensionMethods.System.Collections.Generic.Dictionary;
using Ksnm.ExtensionMethods.System.Random;
using Ksnm.ExtensionMethods.System.Decimal;
using Ksnm.ExtensionMethods.System.Double;
using Ksnm.Mathematics;

namespace DemoApp
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Double型を指数形式ではなく普通に表示するためのフォーマット
        /// </summary>
        static string DoubleFormat = "0." + new string('#', 338);

        public MainForm()
        {
            InitializeComponent();
            Random_InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ウィンドウの位置・サイズを復元
            Bounds = Properties.Settings.Default.Bounds;
            WindowState = Properties.Settings.Default.WindowState;

            OnLoad_RandomTabPage();
            // 定数
            {
                goldenNumberLabel.Text = Ksnm.Math.GoldenNumber.ToDecimalString();
                silverNumberLabel.Text = Ksnm.Math.SilverNumber.ToDecimalString();
            }
            // 数列
            {
                var text = new StringBuilder();
                for (int i = 0; i < 20; i++)
                {
                    text.Append(Ksnm.Math.FibonacciNumber(i) + " ");
                }
                FibonacciSequenceLabel.Text = text.ToString();
            }
            {
                var text = new StringBuilder();
                for (int i = 0; i < 20; i++)
                {
                    text.Append(Ksnm.Math.MosersCircleRegions(i) + " ");
                }
                MosersCircleRegionsLabel.Text = text.ToString();
            }
            // 関数
            Math_ChartUpdate();
        }

        /// <summary>
        /// 閉じる前のイベント
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("閉じますか？", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                // ウィンドウの位置・サイズを保存
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.Bounds = Bounds;
                }
                else
                {
                    Properties.Settings.Default.Bounds = RestoreBounds;
                }
                Properties.Settings.Default.WindowState = WindowState;
                Properties.Settings.Default.Save();
            }
        }
        #region Mathタブ

        /// <summary>
        /// 素因数分解を実行
        /// </summary>
        private void PrimeFactorizationButton_Click(object sender, EventArgs e)
        {
            primeFactorizationLabel.Text = "";
            try
            {
                var value = long.Parse(primeFactorization_ParameterTextBox.Text);
                primeFactorizationLabel.Text = string.Join("*", Ksnm.Math.PrimeFactorization(value));
            }
            catch (FormatException)
            {
                primeFactorizationLabel.Text = "形式が無効";
            }
            catch (OverflowException)
            {
                primeFactorizationLabel.Text = "オーバーフロー";
            }
        }

        private void Math_LeibnizFormulaButton_Click(object sender, EventArgs e)
        {
            var count = Math_LeibnizFormula_CountNumericUpDown1.Value.ToClampedInt32();
            Math_LeibnizFormulaLabel1.Text = (Formula.Leibniz(count) * 4).ToDecimalString();
        }

        private void Math_GaussLegendreButton_Click(object sender, EventArgs e)
        {
            var count = Math_GaussLegendre_CountNumericUpDown.Value.ToClampedInt32();
            Math_GaussLegendreLabel1.Text = (Algorithm.GaussLegendre(count)).ToDecimalString();
        }

        private void Math_ChartUpdate()
        {
            var xMin = Math_ChartXMinNumericUpDown.Value.ToClampedInt32();
            var xMax = Math_ChartXMaxNumericUpDown.Value.ToClampedInt32();
            var xDelta = 1.0 / 1000;
            Math_Chart.Series.Clear();
            if (true)
            {
                var series = new Series("Lerp(0, 10, x)");
                Math_Chart.Series.Add(series);
                series.ChartType = SeriesChartType.Line;
                for (double x = xMin; x <= xMax; x += xDelta)
                {
                    series.Points.AddXY(x, Ksnm.Math.Lerp(0, 10, x));
                }
            }
            if (true)
            {
                var series = new Series("LerpInteger(0, 10, x)");
                Math_Chart.Series.Add(series);
                series.ChartType = SeriesChartType.Line;
                for (double x = xMin; x <= xMax; x += xDelta)
                {
                    series.Points.AddXY(x, Ksnm.Math.LerpInteger(0, 10, x));
                }
            }
            if (false)
            {
                var series = new Series("Sigmoid(x, 1)");
                Math_Chart.Series.Add(series);
                series.ChartType = SeriesChartType.Line;
                for (double x = xMin; x <= xMax; x += xDelta)
                {
                    series.Points.AddXY(x, Ksnm.Math.Sigmoid(x, 1));
                }
            }
            if(false)
            {
                var series = new Series("Ramp(x)");
                Math_Chart.Series.Add(series);
                series.ChartType = SeriesChartType.Line;
                for (double x = xMin; x <= xMax; x += xDelta)
                {
                    series.Points.AddXY(x, Ksnm.Math.Ramp(x));
                }
            }
        }

        private void Math_ChartUpdateButton_Click(object sender, EventArgs e)
        {
            Math_ChartUpdate();
        }

        #endregion Mathタブ

        #region Randomタブ

        Dictionary<string, System.Random> randoms = new Dictionary<string, System.Random>();
        System.Random systemRandom = new System.Random();
        Xorshift128 xorshift128 = new Xorshift128();
        IncrementRandom incrementRandom = new IncrementRandom();
        Prototype prototypeRandom = new Prototype();

        Ksnm.Windows.Forms.InterpolatedPictureBox Random_PointPictureBox;
        Ksnm.Windows.Forms.InterpolatedPictureBox Random_PixelPictureBox;

        /// <summary>
        /// 選択中の乱数生成器
        /// </summary>
        System.Random Random_SelectedRandom
        {
            get
            {
                var selected = Random_TypeComboBox.Text;
                if (randoms.ContainsKey(selected))
                {
                    return randoms[selected];
                }
                else
                {
                    return null;
                }
            }
        }

        void Random_InitializeComponent()
        {
            {
                Random_PointPictureBox = new Ksnm.Windows.Forms.InterpolatedPictureBox();
                ((System.ComponentModel.ISupportInitialize)(Random_PointPictureBox)).BeginInit();

                groupBox2.Controls.Add(Random_PointPictureBox);
                Random_PointPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
                Random_PointPictureBox.Location = new System.Drawing.Point(3, 15);
                Random_PointPictureBox.Name = "Random_PointsPictureBox";
                Random_PointPictureBox.Size = new System.Drawing.Size(331, 119);
                Random_PointPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                Random_PointPictureBox.TabIndex = 0;
                Random_PointPictureBox.TabStop = false;
                Random_PointPictureBox.Interpolation = InterpolationMode.NearestNeighbor;

                ((System.ComponentModel.ISupportInitialize)(Random_PointPictureBox)).EndInit();
            }
            {
                Random_PixelPictureBox = new Ksnm.Windows.Forms.InterpolatedPictureBox();
                ((System.ComponentModel.ISupportInitialize)(Random_PixelPictureBox)).BeginInit();

                groupBox4.Controls.Add(Random_PixelPictureBox);
                Random_PixelPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
                Random_PixelPictureBox.Location = new System.Drawing.Point(3, 15);
                Random_PixelPictureBox.Name = "Random_PixelPictureBox";
                Random_PixelPictureBox.Size = new System.Drawing.Size(331, 119);
                Random_PixelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                Random_PixelPictureBox.TabIndex = 0;
                Random_PixelPictureBox.TabStop = false;
                Random_PixelPictureBox.Interpolation = InterpolationMode.NearestNeighbor;

                ((System.ComponentModel.ISupportInitialize)(Random_PixelPictureBox)).EndInit();
            }
        }

        private void OnLoad_RandomTabPage()
        {
            // 描画先とするImageオブジェクトを作成
            var canvas = new Bitmap(100, 100);
            Random_PointPictureBox.Image = canvas;
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
            }
            canvas = new Bitmap(100, 100);
            Random_PixelPictureBox.Image = canvas;
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
            }
            //
            randoms.Add("System.Random", systemRandom);
            randoms.Add("Xorshift128", xorshift128);
            randoms.Add("IncrementRandom", incrementRandom);
            randoms.Add("Prototype", prototypeRandom);
            foreach (var item in randoms)
            {
                Random_TypeComboBox.Items.Add(item.Key);
            }
            Random_TypeComboBox.SelectedIndex = 0;
        }

        private void Random_TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Random_UpdateUI(Random_SelectedRandom);
        }

        private void Random_GenerateButton1_Click(object sender, EventArgs e)
        {
            Random_UpdateView();
        }

        private void Random_SeedNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Random_UpdateView();
        }

        private void Random_Param1NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Random_UpdateView();
        }

        private void Random_Param2NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Random_UpdateView();
        }

        private void Random_Param3NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Random_UpdateView();
        }

        /// <summary>
        /// 現在選択している乱数生成器を初期化
        /// </summary>
        void Random_InitGenerator(System.Random selected)
        {
            if (ReferenceEquals(selected, systemRandom))
            {
                var value = Random_Param0NumericUpDown.Value.ToClampedInt32();
                systemRandom = new System.Random(value);
                randoms["System.Random"] = systemRandom;
            }
            else if (ReferenceEquals(selected, xorshift128))
            {
                xorshift128.w = Random_Param0NumericUpDown.Value.ToClampedUInt32();
                xorshift128.x = Random_Param1NumericUpDown.Value.ToClampedUInt32();
                xorshift128.y = Random_Param2NumericUpDown.Value.ToClampedUInt32();
                xorshift128.z = Random_Param3NumericUpDown.Value.ToClampedUInt32();
            }
            else if (ReferenceEquals(selected, incrementRandom))
            {
                incrementRandom.Current = Random_Param0NumericUpDown.Value.ToClampedUInt32();
                incrementRandom.Cycle = Random_Param1NumericUpDown.Value.ToClampedUInt32();
            }
            else if (ReferenceEquals(selected, prototypeRandom))
            {
                prototypeRandom.seed = Random_Param0NumericUpDown.Value.ToClampedUInt64();
                prototypeRandom.multiplier = Random_Param1NumericUpDown.Value.ToClampedUInt64();
                prototypeRandom.addend = Random_Param2NumericUpDown.Value.ToClampedUInt64();
            }
        }

        /// <summary>
        /// UI更新中ならtrue
        /// </summary>
        bool Random_IsUpdatingUI = false;

        /// <summary>
        /// 現在選択している乱数生成器に合わせてUI更新
        /// </summary>
        void Random_UpdateUI(System.Random selected)
        {
            Random_IsUpdatingUI = true;
            if (ReferenceEquals(selected, systemRandom))
            {
                Random_Param0Label.Text = "seed";
                Random_Param1Label.Text = "-";
                Random_Param2Label.Text = "-";
                Random_Param3Label.Text = "-";
                Random_Param0NumericUpDown.Enabled = true;
                Random_Param1NumericUpDown.Enabled = false;
                Random_Param2NumericUpDown.Enabled = false;
                Random_Param3NumericUpDown.Enabled = false;
            }
            else if (ReferenceEquals(selected, xorshift128))
            {
                Random_Param0Label.Text = "w";
                Random_Param1Label.Text = "x";
                Random_Param2Label.Text = "y";
                Random_Param3Label.Text = "z";
                Random_Param0NumericUpDown.Enabled = true;
                Random_Param1NumericUpDown.Enabled = true;
                Random_Param2NumericUpDown.Enabled = true;
                Random_Param3NumericUpDown.Enabled = true;
                Random_Param0NumericUpDown.Value = xorshift128.w;
                Random_Param1NumericUpDown.Value = xorshift128.x;
                Random_Param2NumericUpDown.Value = xorshift128.y;
                Random_Param3NumericUpDown.Value = xorshift128.z;
            }
            else if (ReferenceEquals(selected, incrementRandom))
            {
                Random_Param0Label.Text = "Current";
                Random_Param1Label.Text = "Cycle";
                Random_Param2Label.Text = "-";
                Random_Param3Label.Text = "-";
                Random_Param0NumericUpDown.Enabled = true;
                Random_Param1NumericUpDown.Enabled = true;
                Random_Param2NumericUpDown.Enabled = false;
                Random_Param3NumericUpDown.Enabled = false;
                Random_Param0NumericUpDown.Value = incrementRandom.Current;
                Random_Param1NumericUpDown.Value = incrementRandom.Cycle;
            }
            else if (ReferenceEquals(selected, prototypeRandom))
            {
                Random_Param0Label.Text = "seed";
                Random_Param1Label.Text = "multiplier";
                Random_Param2Label.Text = "addend";
                Random_Param3Label.Text = "-";
                Random_Param0NumericUpDown.Enabled = true;
                Random_Param1NumericUpDown.Enabled = true;
                Random_Param2NumericUpDown.Enabled = true;
                Random_Param3NumericUpDown.Enabled = false;
                Random_Param0NumericUpDown.Value = prototypeRandom.seed;
                Random_Param1NumericUpDown.Value = prototypeRandom.multiplier;
                Random_Param2NumericUpDown.Value = prototypeRandom.addend;
            }
            Random_IsUpdatingUI = false;
        }

        void Random_UpdateView()
        {
            // UI更新中なら処理しない
            if (Random_IsUpdatingUI)
            {
                return;
            }
            System.Random random = Random_SelectedRandom;
            if (random == null)
            {
                return;
            }
            //
            {
                Random_TextBox.Clear();
                var stringBuilder = new StringBuilder();
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("Next()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.Next();
                    stringBuilder.AppendLine("0x" + value.ToString("X8") + " " + value.ToString());
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("NextDouble()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.NextDouble();
                    stringBuilder.AppendLine("0x" + value.ToInt64Bits().ToString("X16") + " " + value.ToString(DoubleFormat));
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("NextLong()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.NextLong();
                    stringBuilder.AppendLine("0x" + value.ToString("X16") + " " + value.ToString());
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("NextBytes()");
                Random_InitGenerator(random);
                var buffer = new byte[8];
                for (int i = 0; i < 100; i++)
                {
                    random.NextBytes(buffer);
                    var text = string.Join(",", buffer.Select(item => item.ToString()));
                    stringBuilder.AppendLine(text);
                }
                if (random is Ksnm.Randoms.RandomBase)
                {
                    var ksnmRandom = random as Ksnm.Randoms.RandomBase;

                    //
                    stringBuilder.AppendLine("----------------------------------------");
                    stringBuilder.AppendLine("GenerateUInt32()");
                    Random_InitGenerator(random);
                    for (int i = 0; i < 100; i++)
                    {
                        var value = ksnmRandom.GenerateUInt32();
                        stringBuilder.AppendLine("0x" + value.ToString("X8") + " " + value.ToString());
                    }
                    //
                    stringBuilder.AppendLine("----------------------------------------");
                    stringBuilder.AppendLine("GenerateUInt64()");
                    Random_InitGenerator(random);
                    for (int i = 0; i < 100; i++)
                    {
                        var value = ksnmRandom.GenerateUInt64();
                        stringBuilder.AppendLine("0x" + value.ToString("X16") + " " + value.ToString());
                    }
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("NextBool()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.NextBool();
                    stringBuilder.AppendLine(value.ToString());
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("NextLong()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.NextLong();
                    stringBuilder.AppendLine(value.ToString());
                }
                //
                stringBuilder.AppendLine("----------------------------------------");
                stringBuilder.AppendLine("UnitInterval()");
                Random_InitGenerator(random);
                for (int i = 0; i < 100; i++)
                {
                    var value = random.UnitInterval();
                    stringBuilder.AppendLine(value.ToString(DoubleFormat));
                }
                // 
                Random_TextBox.Text = stringBuilder.ToString();
            }
            // 点の座標がランダム
            Random_InitGenerator(random);
            {
                var canvas = Random_PointPictureBox.Image as Bitmap;
                int x, y;
                using (var graphics = Graphics.FromImage(canvas))
                {
                    graphics.SmoothingMode = SmoothingMode.None;
                    graphics.PixelOffsetMode = PixelOffsetMode.None;
                    graphics.Clear(Color.White);
                    int count = canvas.Width * canvas.Height / 2;
                    for (int i = 0; i < count; i++)
                    {
                        x = random.Next(canvas.Width);
                        y = random.Next(canvas.Height);
                        canvas.SetPixel(x, y, Color.Black);
                    }
                }
            }
            // Pixelの色(白/黒)がランダム
            Random_InitGenerator(random);
            {
                var canvas = Random_PixelPictureBox.Image as Bitmap;
                using (var graphics = Graphics.FromImage(canvas))
                {
                    graphics.Clear(Color.White);
                    for (int y = 0; y < canvas.Height; y++)
                    {
                        for (int x = 0; x < canvas.Width; x++)
                        {
                            if (random.NextDouble() < 0.5)
                            {
                                canvas.SetPixel(x, y, Color.Black);
                            }
                        }
                    }
                }
            }
            // グラフ
            {
                Random_InitGenerator(random);
                const int SampleCount = 300;
                const int minValue = 0;
                const int maxValue = 100;
                var samples = new List<int>();
                var sampleCounts = new Dictionary<int, int>();
                for (int i = 0; i < SampleCount; i++)
                {
                    samples.Add(random.Next(minValue, maxValue));
                }
                foreach (var sample in samples)
                {
                    sampleCounts.AddIfKeyNotExists(sample, 0);
                    sampleCounts[sample] += 1;
                }
                Random_Chart.Series.Clear();
                {
                    var series = new Series("生成値");
                    series.ChartType = SeriesChartType.Line;
                    for (int i = 0; i < samples.Count; i++)
                    {
                        var sample = samples[i];
                        series.Points.AddXY(i, sample);
                    }
                    Random_Chart.Series.Add(series);
                }
                Random_CountChart.Series.Clear();
                {
                    var series = new Series("回数");
                    series.ChartType = SeriesChartType.Column;
                    foreach (var item in sampleCounts)
                    {
                        series.Points.AddXY(item.Key, item.Value);
                    }
                    Random_CountChart.Series.Add(series);
                }
            }
            Random_PointPictureBox.Refresh();
            Random_PixelPictureBox.Refresh();
        }

        #endregion Randomタブ

        #region IOタブ

        private void IO_Directory_CopyButton_Click(object sender, EventArgs e)
        {
            var sourceDirName = IO_Directory_CopySourceTextBox.Text;
            var destDirName = IO_Directory_CopyDestTextBox.Text;
            Ksnm.IO.Directory.Copy(sourceDirName, destDirName, true);
        }

        private void IO_Directory_MoveButton_Click(object sender, EventArgs e)
        {
            var sourceDirName = IO_Directory_MoveSourceTextBox.Text;
            var destDirName = IO_Directory_MoveDestTextBox.Text;
            Ksnm.IO.Directory.Move(sourceDirName, destDirName);
        }

        private void IO_DeflateFile_WriteButton_Click(object sender, EventArgs e)
        {
            var path = IO_DeflateFile_PathTextBox.Text;
            var sourceText = IO_DeflateFile_SourceTextBox.Text;
            Ksnm.IO.DeflateFile.WriteAllText(path, sourceText, Encoding.UTF8);
            // 情報表示
            {
                var bytes = Encoding.UTF8.GetBytes(sourceText);
                var info = new System.IO.FileInfo(path);
                IO_DeflateFile_InfoTextBox.Text =
                    "Encoding:UTF8 " +
                    "圧縮前のサイズ:" + bytes.Length.ToString() + "バイト " +
                    "圧縮後のサイズ:" + info.Length.ToString() + "バイト";
            }
        }

        private void IO_DeflateFile_ReadButton_Click(object sender, EventArgs e)
        {
            var path = IO_DeflateFile_PathTextBox.Text;
            var text = Ksnm.IO.DeflateFile.ReadAllText(path, Encoding.UTF8);
            IO_DeflateFile_DecodeTextBox.Text = text;
        }

        #endregion IOタブ

        #region Binaryタブ
        private void Binary_MaxValueNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var bitNum = (int)Binary_MaxValueNumericUpDown.Value;
            Binary_MaxValueLabel.Text = Ksnm.Binary.MaxValues[bitNum].ToString();
        }
        #endregion Binaryタブ

    }
}
