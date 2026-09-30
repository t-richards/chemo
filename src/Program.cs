using Microsoft.Dism;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Chemo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Microsoft.Dism.dll is embedded in Chemo.exe, so it's loaded from there when first needed.
            AppDomain.CurrentDomain.AssemblyResolve += LoadEmbeddedAssembly;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Run();
        }

        // Kept out of Main so Microsoft.Dism isn't loaded before the handler above is in place.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Run()
        {
            DismApi.InitializeEx(DismLogLevel.LogErrors);
            try
            {
                using MainForm form = new();
                Application.Run(form);
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        private static Assembly? LoadEmbeddedAssembly(object sender, ResolveEventArgs args)
        {
            string resourceName = new AssemblyName(args.Name).Name + ".dll";

            using Stream? resource = typeof(Program).Assembly.GetManifestResourceStream(resourceName);
            if (resource == null)
            {
                return null;
            }

            using MemoryStream assembly = new();
            resource.CopyTo(assembly);
            return Assembly.Load(assembly.ToArray());
        }
    }
}
