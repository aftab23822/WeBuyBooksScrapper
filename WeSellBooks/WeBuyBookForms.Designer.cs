namespace WeSellBooks
{
    partial class WeBuyBookForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeBuyBookForms));
            openFileDialog1 = new OpenFileDialog();
            lableFilePath = new Label();
            dataGridViewBooks = new DataGridView();
            textBox1 = new TextBox();
            data = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox3 = new TextBox();
            label2 = new Label();
            browseFilePathBtn = new Button();
            btnGo = new Button();
            processing = new Label();
            textBoxProcessing = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lableFilePath
            // 
            lableFilePath.AutoSize = true;
            lableFilePath.Location = new Point(21, 44);
            lableFilePath.Name = "lableFilePath";
            lableFilePath.Size = new Size(101, 20);
            lableFilePath.TabIndex = 0;
            lableFilePath.Text = "Input FilePath:";
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new Point(12, 322);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.RowHeadersVisible = false;
            dataGridViewBooks.RowHeadersWidth = 51;
            dataGridViewBooks.Size = new Size(785, 335);
            dataGridViewBooks.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(165, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(454, 27);
            textBox1.TabIndex = 2;
            // 
            // data
            // 
            data.AutoSize = true;
            data.Location = new Point(12, 289);
            data.Name = "data";
            data.Size = new Size(122, 20);
            data.TabIndex = 3;
            data.Text = "Data Processeed:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(165, 92);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(564, 27);
            textBox2.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 95);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 4;
            label1.Text = "Output FilePath:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(165, 139);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(564, 27);
            textBox3.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 142);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 6;
            label2.Text = "Log FilePath:";
            // 
            // browseFilePathBtn
            // 
            browseFilePathBtn.Location = new Point(635, 34);
            browseFilePathBtn.Name = "browseFilePathBtn";
            browseFilePathBtn.Size = new Size(94, 40);
            browseFilePathBtn.TabIndex = 8;
            browseFilePathBtn.Text = "Browse";
            browseFilePathBtn.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            btnGo.Location = new Point(396, 190);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(94, 29);
            btnGo.TabIndex = 9;
            btnGo.Text = "Go";
            btnGo.UseVisualStyleBackColor = true;
            // 
            // processing
            // 
            processing.AutoSize = true;
            processing.Location = new Point(12, 249);
            processing.Name = "processing";
            processing.Size = new Size(112, 20);
            processing.TabIndex = 10;
            processing.Text = "Processing Info:";
            // 
            // textBoxProcessing
            // 
            textBoxProcessing.Location = new Point(165, 246);
            textBoxProcessing.Name = "textBoxProcessing";
            textBoxProcessing.ReadOnly = true;
            textBoxProcessing.Size = new Size(564, 27);
            textBoxProcessing.TabIndex = 11;
            // 
            // WeBuyBookForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 669);
            Controls.Add(textBoxProcessing);
            Controls.Add(processing);
            Controls.Add(btnGo);
            Controls.Add(browseFilePathBtn);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(data);
            Controls.Add(textBox1);
            Controls.Add(dataGridViewBooks);
            Controls.Add(lableFilePath);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WeBuyBookForms";
            Text = "WeBuyBooks";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Label lableFilePath;
        private DataGridView dataGridViewBooks;
        private TextBox textBox1;
        private Label data;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox3;
        private Label label2;
        private Button browseFilePathBtn;
        private Button btnGo;
        private Label processing;
        private TextBox textBoxProcessing;
    }
}