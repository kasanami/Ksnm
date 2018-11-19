using Ksnm.Randoms;
using Ksnm.ExtensionMethods.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DemoApp
{
    public partial class MainForm : Form
    {
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
                goldenNumberLabel.Text = Ksnm.Math.GoldenNumber.ToString("R");
                silverNumberLabel.Text = Ksnm.Math.SilverNumber.ToString("R");
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

        #endregion Mathタブ

        #region Randomタブ

        Dictionary<string, Random> randoms = new Dictionary<string, Random>();
        Random systemRandom = new Random();
        Xorshift128 xorshift128 = new Xorshift128();
        IncrementRandom incrementRandom = new IncrementRandom();
        Prototype prototypeRandom = new Prototype();

        Ksnm.Windows.Forms.InterpolatedPictureBox Random_PointPictureBox;
        Ksnm.Windows.Forms.InterpolatedPictureBox Random_PixelPictureBox;

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
        void Random_InitGenerator(Random selected)
        {
            if(ReferenceEquals(selected ,systemRandom))
            {

            }
            else if (ReferenceEquals(selected, xorshift128))
            {
                xorshift128.w = unchecked((uint)Random_SeedNumericUpDown.Value);
                xorshift128.x = unchecked((uint)Random_Param1NumericUpDown.Value);
                xorshift128.y = unchecked((uint)Random_Param2NumericUpDown.Value);
                xorshift128.z = unchecked((uint)Random_Param2NumericUpDown.Value);
            }
            else if (ReferenceEquals(selected, incrementRandom))
            {
                incrementRandom.Current = unchecked((uint)Random_SeedNumericUpDown.Value);
                incrementRandom.Cycle = unchecked((uint)Random_Param1NumericUpDown.Value);
            }
            else if (ReferenceEquals(selected, prototypeRandom))
            {
                prototypeRandom.seed = unchecked((uint)Random_SeedNumericUpDown.Value);
                prototypeRandom.addend = unchecked((uint)Random_Param1NumericUpDown.Value);
                prototypeRandom.multiplier = unchecked((uint)Random_Param2NumericUpDown.Value);
            }
        }

        void Random_UpdateView()
        {
            Random random = null;
            var selected = Random_TypeComboBox.Text;
            if (randoms.ContainsKey(selected))
            {
                random = randoms[selected];
            }
            else
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
                    stringBuilder.AppendLine("0x" + value.ToInt64Bits().ToString("X16") + " " + value.ToString("0.####################################"));
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
                            if (random.NextDouble() <0.5)
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
                const int SampleCount = 100;
                var samples = new List<int>();
                var sampleCounts = new Dictionary<int, int>();
                for (int i = 0; i < SampleCount; i++)
                {
                    samples.Add(random.Next(100));
                    sampleCounts.Add(i, 0);
                }
                for (int i = 0; i < samples.Count; i++)
                {
                    var sample = samples[i];
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
                {
                    var series = new Series("回数");
                    series.ChartType = SeriesChartType.Column;
                    foreach (var item in sampleCounts)
                    {
                        series.Points.AddXY(item.Key, item.Value);
                    }
                    Random_Chart.Series.Add(series);
                }
                Random_PointPictureBox.Refresh();
                Random_PixelPictureBox.Refresh();
            }
        }

        #endregion Randomタブ

        #region IOタブ

        private void IO_Directory_CopyButton_Click(object sender, EventArgs e)
        {
            var sourceDirName = IO_Directory_CopySourceTextBox.Text;
            var destDirName = IO_Directory_CopyDestTextBox.Text;
            Ksnm.IO.Directory.Copy(sourceDirName, destDirName, true);
        }

        private void IO_DeflateFile_RunButton_Click(object sender, EventArgs e)
        {
            var path = IO_DeflateFile_PathTextBox.Text;
            var sourceText = IO_DeflateFile_SourceTextBox.Text;
            Ksnm.IO.DeflateFile.WriteAllText(path, sourceText, Encoding.UTF8);
            var text = Ksnm.IO.DeflateFile.ReadAllText(path, Encoding.UTF8);
            IO_DeflateFile_DecodeTextBox.Text = text;
        }

        private void IO_Directory_MoveButton_Click(object sender, EventArgs e)
        {
            var sourceDirName = IO_Directory_MoveSourceTextBox.Text;
            var destDirName = IO_Directory_MoveDestTextBox.Text;
            Ksnm.IO.Directory.Move(sourceDirName, destDirName);
        }

        #endregion IOタブ
    }
}
