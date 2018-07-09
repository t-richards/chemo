using System;
using System.Windows.Forms;

namespace Chemo
{
    public sealed class Logger
    {
        private static Logger instance = null;
        private static readonly object instanceLock = new object();
        private static TextBox target = null;

        public void SetTarget(TextBox textBox)
        {
            target = textBox;
        }

        /// <summary>
        /// Logs a message to the log target (text box).
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void Log(string format, params object[] args)
        {
            format += "\r\n";

            if (target.InvokeRequired)
            {
                target.Invoke(new Action(() =>
                    target.AppendText(string.Format(format, args))
                ));
            }
            else
            {
                target.AppendText(string.Format(format, args));
            }
        }

        public static Logger Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new Logger();
                    }
                    return instance;
                }
            }
        }
    }
}
