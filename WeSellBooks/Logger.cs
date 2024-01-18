using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSellBooks
{
    internal class Logger
    {
        public string FilePath = string.Empty;
        public Logger(string filePath)
        {
            FilePath = filePath;
        }
        public void LogToFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    // Append a timestamp, event type, and message to the log file
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs while logging, print the error to the console
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
