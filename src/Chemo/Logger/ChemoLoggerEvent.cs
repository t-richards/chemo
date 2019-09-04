using System;
using System.Globalization;

namespace Chemo.Logger
{
    public enum EventLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical,
        None
    }

    public class ChemoLoggerEvent
    {
        public string Source { get; set; }

        public string Message { get; set; }

        public EventLevel Level { get; set; }

        public DateTime Timestamp { get; set; }

        public ChemoLoggerEvent(string source, EventLevel eventLevel, string message)
        {
            Source = source;
            Level = eventLevel;
            Message = message;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "[{0}] {1} - {2}", Timestamp, Level, Message);
        }
    }
}
