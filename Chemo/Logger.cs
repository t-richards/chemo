using System;
using System.Text;

namespace Chemo
{
    public sealed class Logger
    {
        private static StringBuilder data;

        public Logger()
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
            data.AppendFormat(format, args);
        }

        public static Logger Instance
        {
            get { return new Logger(); }
        }
    }
}
