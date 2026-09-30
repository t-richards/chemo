using Chemo.Utilities;
using System.ComponentModel;

namespace Chemo.Settings
{
    /// <summary>
    /// A date or time format in the current user's regional settings.
    /// </summary>
    /// <remarks>
    /// SetLocaleInfo is used rather than writing the registry, because it also updates the related settings that
    /// older apps read and the copy Windows keeps in memory.
    /// </remarks>
    internal sealed class LocaleFormat : ISetting
    {
        private readonly string description;

        public uint LocaleType { get; }
        public string Format { get; }

        /// <param name="description">What the format is, for the log, such as "Short time format".</param>
        /// <param name="localeType">A LOCALE_ constant, such as LOCALE_SSHORTTIME.</param>
        /// <param name="format">The format string, such as "HH:mm".</param>
        public LocaleFormat(string description, uint localeType, string format)
        {
            this.description = description;
            LocaleType = localeType;
            Format = format;
        }

        public bool IsApplied()
        {
            char[] buffer = new char[128];
            int length = UnsafeNativeMethods.GetLocaleInfoEx(null, LocaleType, buffer, buffer.Length);
            if (length == 0)
            {
                throw new Win32Exception();
            }

            // The length includes the terminating null.
            return string.Equals(new string(buffer, 0, length - 1), Format, StringComparison.Ordinal);
        }

        public void Apply()
        {
            if (!UnsafeNativeMethods.SetLocaleInfo(UnsafeNativeMethods.LOCALE_USER_DEFAULT, LocaleType, Format))
            {
                throw new Win32Exception();
            }
        }

        public override string ToString()
        {
            return $"{description} = {Format}";
        }
    }
}
