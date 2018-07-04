using System.Windows.Forms;

namespace Chemo
{
    public sealed class Logger
    {
        private static Logger instance = null;
        private static readonly object padlock = new object();
        private static TextBox target = null;

        public void SetTarget(TextBox textBox)
        {
            target = textBox;
        }

        public void Log(string format, params object[] args)
        {
            format += "\r\n";
            target.AppendText(string.Format(format, args));
        }

        public static Logger Instance
        {
            get
            {
                lock (padlock)
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
