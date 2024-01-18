using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSellBooks
{
    internal class Runner
    {
        public List<Book> Books = new();
        public List<BookItem> Items = new();
        ApiHelper apiHelper = new ApiHelper();
        Logger logger;
        public int RetryCount = 3;
        private WeBuyBookForms WeBuyBooksControl;
        public string OutputFilePath = string.Empty;
        public string InputFilePath = string.Empty;
        public string LoggerFilePath = string.Empty;

        public Runner(WeBuyBookForms weBuyBooksForm, string? filePath)
        {
            this.WeBuyBooksControl = weBuyBooksForm;
            InputFilePath = filePath;
            var tempOutput = Path.Combine(Path.GetDirectoryName(InputFilePath), "output.csv");
            var tempLogger = Path.Combine(Path.GetDirectoryName(InputFilePath), "logs.txt");
            OutputFilePath = GetUniquePath(tempOutput);
            LoggerFilePath = GetUniquePath(tempLogger);
            logger = new Logger(LoggerFilePath);
            weBuyBooksForm.SetPaths(logger.FilePath, OutputFilePath);
        }

        private string GetUniquePath(string path)
        {
            var directory = Path.GetDirectoryName(path);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            int i = 1;
            string uniquePath = Path.Combine(directory, $"{fileNameWithoutExtension}_{i}{extension}");
            while (File.Exists(uniquePath))
            {
                i++;
                uniquePath = Path.Combine(directory, $"{fileNameWithoutExtension}_{i}{extension}");
            }
            return uniquePath;
        }


        private void SendStatus(string msg = "")
        {
            WeBuyBooksControl.SetStatus(msg);
        }

        public async Task Run()
        {
            try
            {
                if (RetryCount <= 0)
                {
                    await SaveDataToFile(InputFilePath);
                    logger.LogToFile("Exiting Exception count 3 reached");
                    return;
                }
                InputFilePath = InputFilePath.StartsWith("\"") ? InputFilePath.Trim('\"') : InputFilePath;
                InputFilePath = Path.GetFullPath(new System.IO.FileInfo(InputFilePath).FullName);
                if (!File.Exists(InputFilePath))
                {
                    SendStatus("Invalid File Path Cannot Proceed Further...");
                    return;
                }
                SendStatus();
                SendStatus($"Reading File: {InputFilePath}...");
                Books = GetBooksFromFilePath(InputFilePath);
                SendStatus();
                SendStatus($"Reading Complete Records Found: {Books.Count}");

                SendStatus();
                SendStatus("Atempting to Login User...");
                var loginSuccessFull = apiHelper.LoginAndGetToken();
                if (!loginSuccessFull)
                {
                    SendStatus();
                    SendStatus("User Login Failed Cannot Proceed Further...");
                    return;
                }
                SendStatus();
                SendStatus("Login Successful...");

                SendStatus();
                SendStatus("Erasing Old Basket Items...");
                var eraseSuccessFul = await apiHelper.EraseBasket();
                if (!eraseSuccessFul)
                {
                    SendStatus();
                    SendStatus("Erasing Unsuccessfull Cannot Proceed Further...");
                    return;
                }
                SendStatus();
                SendStatus("Basket Erased!");
                SendStatus();
                SendStatus("Attempting to fetch records, This may take sometime...");
                var bookDetails = await GetBooksDetails();
                await SaveDataToFile(InputFilePath);

            }
            catch (Exception ex)
            {
                RetryCount--;
                logger.LogToFile(ex.Message);
                SendStatus(ex.Message);
                //
            }
        }

        private async Task SaveDataToFile(string filePath)
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Books");

                // Add headers to the worksheet
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Asin";
                worksheet.Cells[1, 3].Value = "Title";
                worksheet.Cells[1, 4].Value = "ImageUrl";
                worksheet.Cells[1, 5].Value = "Price";

                // Populate data from the list into the worksheet
                for (int i = 0; i < Items.Count; i++)
                {
                    var bookItem = Items[i];
                    worksheet.Cells[i + 2, 1].Value = bookItem.id;
                    worksheet.Cells[i + 2, 2].Value = bookItem.asin;
                    worksheet.Cells[i + 2, 3].Value = bookItem.title;
                    worksheet.Cells[i + 2, 4].Value = bookItem.imageUrl;
                    worksheet.Cells[i + 2, 5].Value = bookItem.price;
                }

                // Save the Excel package to a stream
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);

                    // Save the stream content to a CSV file
                    File.WriteAllBytes(OutputFilePath, stream.ToArray());
                }
            }
            SendStatus();
            SendStatus("CSV file created successfully.");
            SendStatus();
            SendStatus($"Output Path is:{OutputFilePath}");
            SendStatus();
            SendStatus("Processing Done! Press any key to finish...");
            Console.ReadLine();
        }


        private async Task<List<BookItem>> GetBooksDetails()
        {
            Items ??= new List<BookItem>();
            int i = 1;
            foreach (var book in Books)
            {
                SendStatus();
                SendStatus($"Processing Book {i} with ISBN:{book.ISBN}");
                if (!ISBNValidator.ValidateISBN(book.ISBN))
                {
                    SendStatus();
                    SendStatus($"Book with ISBN: {book.ISBN} has Invalid ISBN, skipping API Call...");
                    SendStatus();
                    continue;
                }
                logger.LogToFile($"{i} => Processing Book with ISBN:{book.ISBN}");
                try
                {
                    var result = await apiHelper.TryApiCall(book.ISBN);
                    if (result.item == null)
                    {
                        logger.LogToFile("Book record not came from website...");
                        SendStatus("");
                        SendStatus("Book record not came from website...");
                        continue;
                    };
                    Items.Add(result.item);
                    WeBuyBooksControl.UpdateDataGridView(result.item, i);
                }
                catch (Exception ex)
                {
                    RetryCount--;
                    logger.LogToFile(ex.Message);
                    SendStatus(ex.Message);
                }
                //if (i == 10) break;
                i++;
            }
            return Items;
        }

        private List<Book> GetBooksFromFilePath(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a list to store objects
            List<Book> books = new List<Book>();
            using (var package = new ExcelPackage(new System.IO.FileInfo(InputFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Assuming there's a header row
                int rowCount = worksheet.Dimension.Rows;

                // Iterate through the rows (skip the header row)
                for (int row = 2; row <= rowCount; row++)
                {
                    // Create a new Book object for each row
                    Book book = new Book();

                    // Assuming there's only one column (e.g., "ean")
                    string ean = worksheet.Cells[row, 1].Value?.ToString();
                    book.ISBN = ean;

                    // Add the book object to the list
                    books.Add(book);
                }
            }
            return books;
        }
    }
}
