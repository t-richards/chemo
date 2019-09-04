using System.Collections.Generic;
using System.Globalization;

namespace Chemo.Logger
{
    public sealed class ChemoLogger
    {
        public List<ChemoLoggerEvent> Events { get; }
        private readonly object listLock = new object();

        private static ChemoLogger instance = null;
        private static readonly object instanceLock = new object();

        public ChemoLogger()
        {
            Events = new List<ChemoLoggerEvent>();
        }

        /// <summary>
        /// Logs a message to the log target (memory buffer).
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void Log(string format, params object[] args)
        {
            format += "\r\n";
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            ChemoLoggerEvent newEvent = new ChemoLoggerEvent(
                "Foo",
                EventLevel.Information,
                message
            );

            lock (listLock)
            {
                Events.Add(newEvent);
            }
        }

        /// <summary>
        /// Resets the logger to its initial blank state.
        /// </summary>
        public void Reset()
        {
            Events.Clear();
        }

        public static ChemoLogger Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ChemoLogger();
                    }
                    return instance;
                }
            }
        }
    }
}
