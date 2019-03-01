using System;
using System.Collections.Generic;
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

            var calendarEvents = GetCalendarEventsFromCsv(allCsvFiles);

            WriteIcsFile(calendarEvents);
        }

        private static List<CalenderEvent> GetCalendarEventsFromCsv(string[] csvFiles)
        {
            var calendarEvents = new List<CalenderEvent>();

            foreach (var csvFile in csvFiles)
            {
                //TODO
            }

            return calendarEvents;
        }

        private static void WriteIcsFile(List<CalenderEvent> calendarEvents)
        {
            //TODO
        }
    }
}
