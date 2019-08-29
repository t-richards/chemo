using System.Globalization;
using System.Text;

namespace Chemo
{
    public sealed class MemoryLogger
    {
        private StringBuilder data;
        private readonly CultureInfo cultureInfo = CultureInfo.InvariantCulture;

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
            data.AppendFormat(cultureInfo, format, args);
        }

        /// <summary>
        /// Resets the logger to its initial blank state.
        /// </summary>
        public void Reset()
        {
            data = new StringBuilder();
        }

        public override string ToString()
        {
            return data.ToString();
        }

        public static MemoryLogger Instance
        {
            get => new MemoryLogger();
        }
    }
}
