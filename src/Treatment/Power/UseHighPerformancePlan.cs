using Chemo.Settings;
using Chemo.Utilities;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Chemo.Treatment.Power
{
    internal sealed class UseHighPerformancePlan : SettingsTreatment
    {
        public override string Name => "Use the High performance power plan";

        public override string Description => "Keeps the processor at full speed instead of saving power. Uses more electricity and drains laptop batteries faster.";

        protected override IEnumerable<ISetting> Settings()
        {
            return
            [
                new HighPerformanceActive(),
            ];
        }

        private sealed class HighPerformanceActive : ISetting
        {
            // Built into Windows as SCHEME_MIN, the scheme with the least power saving.
            private static readonly Guid HighPerformance = new("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

            public bool IsApplied()
            {
                uint error = UnsafeNativeMethods.PowerGetActiveScheme(IntPtr.Zero, out IntPtr active);
                if (error != 0)
                {
                    throw new Win32Exception((int)error);
                }

                try
                {
                    return Marshal.PtrToStructure<Guid>(active) == HighPerformance;
                }
                finally
                {
                    UnsafeNativeMethods.LocalFree(active);
                }
            }

            public void Apply()
            {
                Guid scheme = HighPerformance;
                uint error = UnsafeNativeMethods.PowerSetActiveScheme(IntPtr.Zero, ref scheme);
                if (error != 0)
                {
                    throw new InvalidOperationException(
                        $"Couldn't switch to High performance: {new Win32Exception((int)error).Message} Many laptops only offer the Balanced plan.");
                }
            }

            public override string ToString()
            {
                return "High performance power plan is active";
            }
        }
    }
}
