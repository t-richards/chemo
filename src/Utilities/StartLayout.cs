using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Chemo.Utilities
{
    /// <summary>
    /// Reads the current user's Start pins through the COM class behind the Export-StartLayout cmdlet.
    /// </summary>
    internal static class StartLayout
    {
        [ComImport, Guid("75AB852C-0441-46D4-A205-EF0A33F98255")]
        private class StartLayoutCmdlet
        {
        }

        [ComImport, Guid("0BAC4102-61E9-48A5-93DD-D295ABA65369"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IStartLayoutCmdlet
        {
            void ExportStartLayout([In, MarshalAs(UnmanagedType.LPWStr)] string filePath);
        }

        [DataContract]
        private sealed class Layout
        {
            [DataMember(Name = "pinnedList")]
            public List<Pin> PinnedList { get; set; }
        }

        [DataContract]
        private sealed class Pin
        {
        }

        /// <summary>
        /// Counts the apps and sites pinned to Start for the current user.
        /// </summary>
        public static int CountPins()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
            IStartLayoutCmdlet exporter = (IStartLayoutCmdlet)new StartLayoutCmdlet();

            try
            {
                exporter.ExportStartLayout(path);

                using (FileStream stream = File.OpenRead(path))
                {
                    Layout layout = (Layout)new DataContractJsonSerializer(typeof(Layout)).ReadObject(stream);
                    return layout.PinnedList?.Count ?? 0;
                }
            }
            finally
            {
                Marshal.ReleaseComObject(exporter);
                File.Delete(path);
            }
        }
    }
}
