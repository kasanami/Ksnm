using Ksnm.Randoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void OnLoad_RandomTabPage()
        {
            // 描画先とするImageオブジェクトを作成
            var canvas = new Bitmap(500, 500);
            Random_PictureBox.Image = canvas;
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
            }
            //
            randoms.Add("System.Random", new Random());
            randoms.Add("Xorshift128", new Xorshift128());
            randoms.Add("IncrementRandom", new IncrementRandom());
            randoms.Add("Prototype", new Prototype(1));
            foreach (var item in randoms)
            {
                Random_TypeComboBox.Items.Add(item.Key);
            }
            Random_TypeComboBox.SelectedIndex = 0;
        }

        private void Random_GenerateButton1_Click(object sender, EventArgs e)
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
            // 点描画
            var pen = new Pen(Color.Black, 1);
            var canvas = Random_PictureBox.Image;
            int x, y;
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
                for (int i = 0; i < 5000; i++)
                {
                    x = random.Next(canvas.Width);
                    y = random.Next(canvas.Height);
                    graphics.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            Random_PictureBox.Refresh();
            // グラフ
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
        }

        #endregion Randomタブ

        #region IOタブ

        private void IO_Directory_CopyButton_Click(object sender, EventArgs e)
        {
            var sourceDirName = IO_Directory_CopySourceTextBox.Text;
            var destDirName = IO_Directory_CopyDestTextBox.Text;
            Ksnm.IO.Directory.Copy(sourceDirName, destDirName, true);
        }

        #endregion IOタブ
    }
}
