using System.Globalization;
using System.Text;

namespace Chemo
{
    public sealed class MemoryLogger
    {
        // Treatments log from a background thread while the details pane reads the log on the UI thread.
        private readonly object dataLock = new object();
        private StringBuilder data;

        public MemoryLogger()
        {
            data = new StringBuilder();
        }

        /// <summary>
        /// Logs a message to the log target (memory buffer).
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void Log(string format, params object[] args)
        {
            format += "\r\n";
            lock (dataLock)
            {
                data.AppendFormat(CultureInfo.InvariantCulture, format, args);
            }
        }

        /// <summary>
        /// Resets the logger to its initial blank state.
        /// </summary>
        public void Reset()
        {
            lock (dataLock)
            {
                data = new StringBuilder();
            }
        }

        public override string ToString()
        {
            lock (dataLock)
            {
                return data.ToString();
            }
        }

        public static MemoryLogger Instance
        {
            get => new MemoryLogger();
        }
    }
}
