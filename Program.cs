using System;
using System.IO;

namespace csvToIcs
{
    class Program
    {
        static void Main(string[] args)
        {
            var importDir = Path.Combine(Directory.GetCurrentDirectory(), "import");

            if (!Directory.Exists(importDir))
            {
                Console.WriteLine("No import directory found! Creating it now...");
                Directory.CreateDirectory(importDir);
            }

            var allCsvFiles = Directory.GetFiles(importDir, "*.csv");

            if (allCsvFiles.Length <= 0)
            {
                Console.WriteLine("No csv-files in import directory found!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }
    }
}
