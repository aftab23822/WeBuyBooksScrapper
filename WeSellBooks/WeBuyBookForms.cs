using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeSellBooks
{
    public partial class WeBuyBookForms : Form
    {
        string FilePath = string.Empty;
        public WeBuyBookForms()
        {
            InitializeComponent();
            AddColumns();
            browseFilePathBtn.Click += BrowseFilePathBtn_Click;
            btnGo.Click += BtnGo_Click;
        }

        private void AddColumns()
        {
            if (dataGridViewBooks.Columns.Count == 0)
            {
                dataGridViewBooks.Columns.Add("Count", "Count");
                dataGridViewBooks.Columns.Add("Id", "ID");
                dataGridViewBooks.Columns.Add("Asin", "ASIN");
                dataGridViewBooks.Columns.Add("Title", "Title");
                dataGridViewBooks.Columns.Add("ImageUrl", "Image URL");
                dataGridViewBooks.Columns.Add("Price", "Price");
            }
        }

        private void BtnGo_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                var runner = new Runner(this, FilePath);
                Task.Run(() => runner.Run());
                btnGo.Enabled = false;
            }

        }

        private void BrowseFilePathBtn_Click(object? sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            var location = openFileDialog1.ShowDialog();
            if (location == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                FilePath = openFileDialog1.FileName;
                btnGo.Enabled = true;
            }
        }

        public void UpdateDataGridView(BookItem book, int count)
        {
            if (book is null)
            {
                SetStatus("Book record not came from website");
                return;

            };
            if (dataGridViewBooks.InvokeRequired)
            {
                // If called from a non-UI thread, invoke the method on the UI thread
                dataGridViewBooks.Invoke(new Action(() => UpdateDataGridView(book, count)));
                return;
            }

            // Add a new row to the DataGridView
            int rowIndex = dataGridViewBooks.Rows.Add(
                count,
                book.id,
                book.asin,
                book.title,
                book.imageUrl,
                book.price
            );

            // Optionally, you can store the BookItem object in the row's Tag property
            dataGridViewBooks.Rows[rowIndex].Tag = book;
        }

        internal void SetPaths(string logFilePath, string outputFilePath)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetPaths(logFilePath, outputFilePath)));
            }
            else
            {
                this.textBox3.Text = logFilePath;
                this.textBox2.Text = outputFilePath;
            }
        }
        public void SetStatus(string msg)
        {
            if (textBoxProcessing.InvokeRequired)
            {
                textBoxProcessing.Invoke(new Action(() => SetStatus(msg)));
                return;
            }
            this.textBoxProcessing.Text = msg;

        }
    }
}
