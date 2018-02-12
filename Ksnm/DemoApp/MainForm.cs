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
    }
}
