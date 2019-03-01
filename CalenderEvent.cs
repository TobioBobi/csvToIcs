using System;

namespace csvToIcs
{
    public abstract class CalenderEvent
    {
        public DateTime DateTimeStamp { get; set; }
        public string Uid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
