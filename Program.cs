using System;
using System.Collections.Generic;
using System.IO;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

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

                try
                {
                    Directory.CreateDirectory(importDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while trying to create import directory:");
                    Console.WriteLine(e);
                    Console.WriteLine("Press any key ...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            var allCsvFiles = Directory.GetFiles(importDir, "*.csv");

            if (allCsvFiles.Length <= 0)
            {
                Console.WriteLine("No csv-files in import directory found!");
                Console.WriteLine("Press any key ...");
                Console.ReadKey();
            }

            var calendar = CreateCalenderFromCsv(allCsvFiles);
            WriteCalenderToFile(calendar);

            Console.WriteLine("Finished!");
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }

        private static Calendar CreateCalenderFromCsv(string[] csvFiles)
        {
            var calendar = new Calendar();

            foreach (var csvFile in csvFiles)
            {
                using (var reader = new StreamReader(csvFile))
                {
                    reader.ReadLine(); //Skip first row

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        var calendarEvent = new CalendarEvent { Description = values[0], Location = values[1] };

                        if (!DateTime.TryParse(values[2], out var dateStart))
                        {
                            Console.WriteLine($"Error parsing {values[2]} to DateTime! Entry is being skipped.");
                            continue;
                        }

                        if (!DateTime.TryParse(values[3], out var timeStart))
                        {
                            Console.WriteLine($"Error parsing {values[3]} to DateTime! Entry is being skipped.");
                            continue;
                        }
                        calendarEvent.DtStart = new CalDateTime(dateStart.Date.Add(timeStart.TimeOfDay));

                        var dateEnd = DateTime.Parse(values[4]);
                        var timeEnd = DateTime.Parse(values[5]);
                        calendarEvent.DtEnd = new CalDateTime(dateEnd.Date.Add(timeEnd.TimeOfDay));

                        calendar.Events.Add(calendarEvent);
                    }
                }
            }

            return calendar;
        }

        private static void WriteCalenderToFile(Calendar calendar)
        {
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);

            var exportDir = Path.Combine(Directory.GetCurrentDirectory(), "export");
            if (!Directory.Exists(exportDir))
            {
                Console.WriteLine("No export directory found! Creating it now...");
                Directory.CreateDirectory(exportDir);
            }

            File.WriteAllText(Path.Combine(exportDir, "calendar.ics"), serializedCalendar);
        }
    }
}
