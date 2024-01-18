using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace WeSellBooks
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create an instance of your main form
            var mainForm = new WeBuyBookForms();

            // Run the application with the main form
            Application.Run(mainForm);
        }
    }
}