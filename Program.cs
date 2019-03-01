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
                Directory.CreateDirectory(importDir);
            }

            var allCsvFiles = Directory.GetFiles(importDir, "*.csv");

            if (allCsvFiles.Length <= 0)
            {
                Console.WriteLine("No csv-files in import directory found!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }

            var calendar = CreateCalenderFromCsv(allCsvFiles);

            WriteCalenderToFile(calendar);
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

                        var calendarEvent = new CalendarEvent {Description = values[0], Location = values[1]};

                        var dateStart = DateTime.Parse(values[2]);
                        var timeStart = DateTime.Parse(values[3]);
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

            File.WriteAllText("calendar.ics", serializedCalendar);
        }
    }
}
