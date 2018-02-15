﻿namespace DemoApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.RandomTabControl = new System.Windows.Forms.TabControl();
            this.MathTabPage = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Random_PictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Random_GenerateButton1 = new System.Windows.Forms.Button();
            this.Random_TypeComboBox = new System.Windows.Forms.ComboBox();
            this.Random_SeedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.IOTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.IO_Directory_CopySourceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IO_Directory_CopyButton = new System.Windows.Forms.Button();
            this.IO_Directory_CopyDestTextBox = new System.Windows.Forms.TextBox();
            this.RandomTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_PictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_SeedNumericUpDown)).BeginInit();
            this.IOTabPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // RandomTabControl
            // 
            this.RandomTabControl.Controls.Add(this.MathTabPage);
            this.RandomTabControl.Controls.Add(this.tabPage2);
            this.RandomTabControl.Controls.Add(this.IOTabPage);
            this.RandomTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RandomTabControl.Location = new System.Drawing.Point(0, 0);
            this.RandomTabControl.Name = "RandomTabControl";
            this.RandomTabControl.SelectedIndex = 0;
            this.RandomTabControl.Size = new System.Drawing.Size(747, 489);
            this.RandomTabControl.TabIndex = 0;
            // 
            // MathTabPage
            // 
            this.MathTabPage.Location = new System.Drawing.Point(4, 22);
            this.MathTabPage.Name = "MathTabPage";
            this.MathTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MathTabPage.Size = new System.Drawing.Size(739, 463);
            this.MathTabPage.TabIndex = 0;
            this.MathTabPage.Text = "Math";
            this.MathTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(739, 463);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Random";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.Random_PictureBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Random_PictureBox
            // 
            this.Random_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_PictureBox.Location = new System.Drawing.Point(23, 103);
            this.Random_PictureBox.Name = "Random_PictureBox";
            this.Random_PictureBox.Size = new System.Drawing.Size(687, 331);
            this.Random_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Random_PictureBox.TabIndex = 0;
            this.Random_PictureBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.Random_GenerateButton1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Random_TypeComboBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Random_SeedNumericUpDown, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(687, 44);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Random_GenerateButton1
            // 
            this.Random_GenerateButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_GenerateButton1.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Random_GenerateButton1.Location = new System.Drawing.Point(461, 3);
            this.Random_GenerateButton1.Name = "Random_GenerateButton1";
            this.Random_GenerateButton1.Size = new System.Drawing.Size(223, 38);
            this.Random_GenerateButton1.TabIndex = 1;
            this.Random_GenerateButton1.Text = "乱数生成";
            this.Random_GenerateButton1.UseVisualStyleBackColor = true;
            this.Random_GenerateButton1.Click += new System.EventHandler(this.Random_GenerateButton1_Click);
            // 
            // Random_TypeComboBox
            // 
            this.Random_TypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_TypeComboBox.FormattingEnabled = true;
            this.Random_TypeComboBox.Location = new System.Drawing.Point(3, 3);
            this.Random_TypeComboBox.Name = "Random_TypeComboBox";
            this.Random_TypeComboBox.Size = new System.Drawing.Size(223, 20);
            this.Random_TypeComboBox.TabIndex = 2;
            // 
            // Random_SeedNumericUpDown
            // 
            this.Random_SeedNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_SeedNumericUpDown.Location = new System.Drawing.Point(232, 3);
            this.Random_SeedNumericUpDown.Name = "Random_SeedNumericUpDown";
            this.Random_SeedNumericUpDown.Size = new System.Drawing.Size(223, 19);
            this.Random_SeedNumericUpDown.TabIndex = 3;
            // 
            // IOTabPage
            // 
            this.IOTabPage.Controls.Add(this.tableLayoutPanel3);
            this.IOTabPage.Location = new System.Drawing.Point(4, 22);
            this.IOTabPage.Name = "IOTabPage";
            this.IOTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IOTabPage.Size = new System.Drawing.Size(739, 463);
            this.IOTabPage.TabIndex = 2;
            this.IOTabPage.Text = "IO";
            this.IOTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(733, 457);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 222);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directory.Copy";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.IO_Directory_CopyDestTextBox, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.IO_Directory_CopySourceTextBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.IO_Directory_CopyButton, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(354, 204);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "コピー元";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IO_Directory_CopySourceTextBox
            // 
            this.IO_Directory_CopySourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IO_Directory_CopySourceTextBox.Location = new System.Drawing.Point(103, 3);
            this.IO_Directory_CopySourceTextBox.Name = "IO_Directory_CopySourceTextBox";
            this.IO_Directory_CopySourceTextBox.Size = new System.Drawing.Size(248, 19);
            this.IO_Directory_CopySourceTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "コピー先";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IO_Directory_CopyButton
            // 
            this.IO_Directory_CopyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IO_Directory_CopyButton.Location = new System.Drawing.Point(103, 53);
            this.IO_Directory_CopyButton.Name = "IO_Directory_CopyButton";
            this.IO_Directory_CopyButton.Size = new System.Drawing.Size(248, 19);
            this.IO_Directory_CopyButton.TabIndex = 3;
            this.IO_Directory_CopyButton.Text = "コピー実行";
            this.IO_Directory_CopyButton.UseVisualStyleBackColor = true;
            this.IO_Directory_CopyButton.Click += new System.EventHandler(this.IO_Directory_CopyButton_Click);
            // 
            // IO_Directory_CopyDestTextBox
            // 
            this.IO_Directory_CopyDestTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IO_Directory_CopyDestTextBox.Location = new System.Drawing.Point(103, 28);
            this.IO_Directory_CopyDestTextBox.Name = "IO_Directory_CopyDestTextBox";
            this.IO_Directory_CopyDestTextBox.Size = new System.Drawing.Size(248, 19);
            this.IO_Directory_CopyDestTextBox.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 489);
            this.Controls.Add(this.RandomTabControl);
            this.Name = "MainForm";
            this.Text = "Ksnm Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.RandomTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Random_PictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Random_SeedNumericUpDown)).EndInit();
            this.IOTabPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl RandomTabControl;
        private System.Windows.Forms.TabPage MathTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox Random_PictureBox;
        private System.Windows.Forms.Button Random_GenerateButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox Random_TypeComboBox;
        private System.Windows.Forms.NumericUpDown Random_SeedNumericUpDown;
        private System.Windows.Forms.TabPage IOTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IO_Directory_CopySourceTextBox;
        private System.Windows.Forms.TextBox IO_Directory_CopyDestTextBox;
        private System.Windows.Forms.Button IO_Directory_CopyButton;
    }
}

