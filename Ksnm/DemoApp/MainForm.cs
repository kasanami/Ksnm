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

        #region Randomタブ

        Random random = new Random();
        Xorshift128 xorshift128 = new Xorshift128();
        IncrementRandom incrementRandom = new IncrementRandom();

        private void OnLoad_RandomTabPage()
        {
            // 描画先とするImageオブジェクトを作成
            var canvas = new Bitmap(500, 500);
            Random_PictureBox.Image = canvas;
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
            }

            Random_TypeComboBox.Items.Add("Random");
            Random_TypeComboBox.Items.Add("Xorshift128");
            Random_TypeComboBox.Items.Add("IncrementRandom");
            Random_TypeComboBox.SelectedIndex = 0;
        }

        private void Random_GenerateButton1_Click(object sender, EventArgs e)
        {
            Random random = null;
            if (Random_TypeComboBox.Text == "Random")
            {
                random = this.random;
            }
            else if (Random_TypeComboBox.Text == "Xorshift128")
            {
                random = xorshift128;
            }
            else if (Random_TypeComboBox.Text == "IncrementRandom")
            {
                random = incrementRandom;
            }
            else
            {
                return;
            }

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
