namespace DemoApp
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.RandomTabControl = new System.Windows.Forms.TabControl();
            this.MathTabPage = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.silverNumberLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.goldenNumberLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.MosersCircleRegionsLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FibonacciSequenceLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.primeFactorizationLabel = new System.Windows.Forms.Label();
            this.primeFactorization_ParameterTextBox = new System.Windows.Forms.TextBox();
            this.primeFactorizationButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Random_GenerateButton1 = new System.Windows.Forms.Button();
            this.Random_TypeComboBox = new System.Windows.Forms.ComboBox();
            this.Random_SeedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Random_PictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Random_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.IOTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.IO_Directory_CopyDestTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IO_Directory_CopySourceTextBox = new System.Windows.Forms.TextBox();
            this.IO_Directory_CopyButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Random_Param1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.Random_Param2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RandomTabControl.SuspendLayout();
            this.MathTabPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_SeedNumericUpDown)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_PictureBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_Chart)).BeginInit();
            this.IOTabPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_Param1NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Random_Param2NumericUpDown)).BeginInit();
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
            this.MathTabPage.Controls.Add(this.tabControl1);
            this.MathTabPage.Location = new System.Drawing.Point(4, 22);
            this.MathTabPage.Name = "MathTabPage";
            this.MathTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MathTabPage.Size = new System.Drawing.Size(739, 463);
            this.MathTabPage.TabIndex = 0;
            this.MathTabPage.Text = "Math";
            this.MathTabPage.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(725, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "定数";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.silverNumberLabel, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.goldenNumberLabel, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(719, 425);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // silverNumberLabel
            // 
            this.silverNumberLabel.AutoSize = true;
            this.silverNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.silverNumberLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.silverNumberLabel.Location = new System.Drawing.Point(123, 40);
            this.silverNumberLabel.Name = "silverNumberLabel";
            this.silverNumberLabel.Size = new System.Drawing.Size(593, 40);
            this.silverNumberLabel.TabIndex = 5;
            this.silverNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(3, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 40);
            this.label6.TabIndex = 4;
            this.label6.Text = "白銀数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // goldenNumberLabel
            // 
            this.goldenNumberLabel.AutoSize = true;
            this.goldenNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goldenNumberLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goldenNumberLabel.Location = new System.Drawing.Point(123, 0);
            this.goldenNumberLabel.Name = "goldenNumberLabel";
            this.goldenNumberLabel.Size = new System.Drawing.Size(593, 40);
            this.goldenNumberLabel.TabIndex = 3;
            this.goldenNumberLabel.Text = "label4";
            this.goldenNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 40);
            this.label4.TabIndex = 1;
            this.label4.Text = "黄金数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(725, 431);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "数列";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.MosersCircleRegionsLabel, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.FibonacciSequenceLabel, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(719, 425);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // MosersCircleRegionsLabel
            // 
            this.MosersCircleRegionsLabel.AutoSize = true;
            this.MosersCircleRegionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MosersCircleRegionsLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MosersCircleRegionsLabel.Location = new System.Drawing.Point(123, 40);
            this.MosersCircleRegionsLabel.Name = "MosersCircleRegionsLabel";
            this.MosersCircleRegionsLabel.Size = new System.Drawing.Size(593, 40);
            this.MosersCircleRegionsLabel.TabIndex = 3;
            this.MosersCircleRegionsLabel.Text = "label6";
            this.MosersCircleRegionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(3, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 40);
            this.label5.TabIndex = 2;
            this.label5.Text = "モーザー数列";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FibonacciSequenceLabel
            // 
            this.FibonacciSequenceLabel.AutoSize = true;
            this.FibonacciSequenceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FibonacciSequenceLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FibonacciSequenceLabel.Location = new System.Drawing.Point(123, 0);
            this.FibonacciSequenceLabel.Name = "FibonacciSequenceLabel";
            this.FibonacciSequenceLabel.Size = new System.Drawing.Size(593, 40);
            this.FibonacciSequenceLabel.TabIndex = 1;
            this.FibonacciSequenceLabel.Text = "label4";
            this.FibonacciSequenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "フィボナッチ数列";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel8);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(725, 431);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "計算";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.primeFactorizationLabel, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.primeFactorization_ParameterTextBox, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.primeFactorizationButton, 2, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(719, 425);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 40);
            this.label7.TabIndex = 1;
            this.label7.Text = "素因数分解";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // primeFactorizationLabel
            // 
            this.primeFactorizationLabel.AutoSize = true;
            this.primeFactorizationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.primeFactorizationLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.primeFactorizationLabel.Location = new System.Drawing.Point(263, 0);
            this.primeFactorizationLabel.Name = "primeFactorizationLabel";
            this.primeFactorizationLabel.Size = new System.Drawing.Size(453, 40);
            this.primeFactorizationLabel.TabIndex = 2;
            this.primeFactorizationLabel.Text = "label4";
            this.primeFactorizationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // primeFactorization_ParameterTextBox
            // 
            this.primeFactorization_ParameterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.primeFactorization_ParameterTextBox.Location = new System.Drawing.Point(123, 3);
            this.primeFactorization_ParameterTextBox.Name = "primeFactorization_ParameterTextBox";
            this.primeFactorization_ParameterTextBox.Size = new System.Drawing.Size(94, 19);
            this.primeFactorization_ParameterTextBox.TabIndex = 3;
            this.primeFactorization_ParameterTextBox.Text = "2310";
            // 
            // primeFactorizationButton
            // 
            this.primeFactorizationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.primeFactorizationButton.Location = new System.Drawing.Point(223, 3);
            this.primeFactorizationButton.Name = "primeFactorizationButton";
            this.primeFactorizationButton.Size = new System.Drawing.Size(34, 34);
            this.primeFactorizationButton.TabIndex = 4;
            this.primeFactorizationButton.Text = "＝";
            this.primeFactorizationButton.UseVisualStyleBackColor = true;
            this.primeFactorizationButton.Click += new System.EventHandler(this.PrimeFactorizationButton_Click);
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Random_GenerateButton1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.Random_Param2NumericUpDown, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Random_Param1NumericUpDown, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Random_TypeComboBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Random_SeedNumericUpDown, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(687, 74);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Random_GenerateButton1
            // 
            this.Random_GenerateButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_GenerateButton1.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Random_GenerateButton1.Location = new System.Drawing.Point(23, 103);
            this.Random_GenerateButton1.Name = "Random_GenerateButton1";
            this.Random_GenerateButton1.Size = new System.Drawing.Size(687, 39);
            this.Random_GenerateButton1.TabIndex = 1;
            this.Random_GenerateButton1.Text = "乱数生成";
            this.Random_GenerateButton1.UseVisualStyleBackColor = true;
            this.Random_GenerateButton1.Click += new System.EventHandler(this.Random_GenerateButton1_Click);
            // 
            // Random_TypeComboBox
            // 
            this.Random_TypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_TypeComboBox.FormattingEnabled = true;
            this.Random_TypeComboBox.Location = new System.Drawing.Point(3, 23);
            this.Random_TypeComboBox.Name = "Random_TypeComboBox";
            this.Random_TypeComboBox.Size = new System.Drawing.Size(194, 20);
            this.Random_TypeComboBox.TabIndex = 2;
            // 
            // Random_SeedNumericUpDown
            // 
            this.Random_SeedNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_SeedNumericUpDown.Location = new System.Drawing.Point(203, 23);
            this.Random_SeedNumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            0,
            0});
            this.Random_SeedNumericUpDown.Name = "Random_SeedNumericUpDown";
            this.Random_SeedNumericUpDown.Size = new System.Drawing.Size(156, 19);
            this.Random_SeedNumericUpDown.TabIndex = 3;
            this.Random_SeedNumericUpDown.ValueChanged += new System.EventHandler(this.Random_SeedNumericUpDown_ValueChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(23, 148);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 331F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(687, 286);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Random_PictureBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 280);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "点描画";
            // 
            // Random_PictureBox
            // 
            this.Random_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_PictureBox.Location = new System.Drawing.Point(3, 15);
            this.Random_PictureBox.Name = "Random_PictureBox";
            this.Random_PictureBox.Size = new System.Drawing.Size(331, 262);
            this.Random_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Random_PictureBox.TabIndex = 0;
            this.Random_PictureBox.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Random_Chart);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(346, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 280);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "グラフ";
            // 
            // Random_Chart
            // 
            chartArea1.Name = "ChartArea1";
            this.Random_Chart.ChartAreas.Add(chartArea1);
            this.Random_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.Random_Chart.Legends.Add(legend1);
            this.Random_Chart.Location = new System.Drawing.Point(3, 15);
            this.Random_Chart.Name = "Random_Chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Random_Chart.Series.Add(series1);
            this.Random_Chart.Size = new System.Drawing.Size(332, 262);
            this.Random_Chart.TabIndex = 0;
            this.Random_Chart.Text = "chart1";
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
            // IO_Directory_CopyDestTextBox
            // 
            this.IO_Directory_CopyDestTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IO_Directory_CopyDestTextBox.Location = new System.Drawing.Point(103, 28);
            this.IO_Directory_CopyDestTextBox.Name = "IO_Directory_CopyDestTextBox";
            this.IO_Directory_CopyDestTextBox.Size = new System.Drawing.Size(248, 19);
            this.IO_Directory_CopyDestTextBox.TabIndex = 4;
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "乱数の種類";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(203, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 20);
            this.label9.TabIndex = 5;
            this.label9.Text = "シード値";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(365, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 20);
            this.label10.TabIndex = 6;
            this.label10.Text = "パラメータ1";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Random_Param1NumericUpDown
            // 
            this.Random_Param1NumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_Param1NumericUpDown.Location = new System.Drawing.Point(365, 23);
            this.Random_Param1NumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            0,
            0});
            this.Random_Param1NumericUpDown.Name = "Random_Param1NumericUpDown";
            this.Random_Param1NumericUpDown.Size = new System.Drawing.Size(156, 19);
            this.Random_Param1NumericUpDown.TabIndex = 7;
            this.Random_Param1NumericUpDown.ValueChanged += new System.EventHandler(this.Random_Param1NumericUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(527, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 20);
            this.label11.TabIndex = 8;
            this.label11.Text = "パラメータ2";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Random_Param2NumericUpDown
            // 
            this.Random_Param2NumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Random_Param2NumericUpDown.Location = new System.Drawing.Point(527, 23);
            this.Random_Param2NumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            0,
            0});
            this.Random_Param2NumericUpDown.Name = "Random_Param2NumericUpDown";
            this.Random_Param2NumericUpDown.Size = new System.Drawing.Size(157, 19);
            this.Random_Param2NumericUpDown.TabIndex = 9;
            this.Random_Param2NumericUpDown.ValueChanged += new System.EventHandler(this.Random_Param2NumericUpDown_ValueChanged);
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
            this.MathTabPage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_SeedNumericUpDown)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Random_PictureBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Random_Chart)).EndInit();
            this.IOTabPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_Param1NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Random_Param2NumericUpDown)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Random_Chart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label MosersCircleRegionsLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label FibonacciSequenceLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label goldenNumberLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label silverNumberLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label primeFactorizationLabel;
        private System.Windows.Forms.TextBox primeFactorization_ParameterTextBox;
        private System.Windows.Forms.Button primeFactorizationButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Random_Param1NumericUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown Random_Param2NumericUpDown;
        private System.Windows.Forms.Label label11;
    }
}

