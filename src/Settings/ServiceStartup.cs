using Chemo.Utilities;
using Microsoft.Win32;
using System.ComponentModel;
using System.ServiceProcess;

namespace Chemo.Settings
{
    public enum ServiceStartType
    {
        Automatic = 2,
        Manual = 3,
        Disabled = 4,
    }

    /// <summary>
    /// A Windows service that should use a specific startup type.
    /// </summary>
    public sealed class ServiceStartup : ISetting
    {
        private const string ServicesKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\";

        public string ServiceName { get; }
        public ServiceStartType StartType { get; }

        public ServiceStartup(string serviceName, ServiceStartType startType)
        {
            ServiceName = serviceName;
            StartType = startType;
        }

        public bool IsApplied()
        {
            // A service that doesn't exist on this edition of Windows has nothing to change.
            object start = Registry.GetValue(ServicesKey + ServiceName, "Start", null);
            return start == null || (int)start == (int)StartType;
        }

        /// <summary>
        /// Determines whether the service is running. Changing the startup type doesn't stop a running service,
        /// so a disabled service keeps running until Windows restarts.
        /// </summary>
        public bool IsRunning()
        {
            using (ServiceController service = new ServiceController(ServiceName))
            {
                try
                {
                    return service.Status != ServiceControllerStatus.Stopped;
                }
                catch (InvalidOperationException)
                {
                    // The service doesn't exist on this edition of Windows.
                    return false;
                }
            }
        }

        public void Apply()
        {
            // Going through the service control manager, rather than writing the registry directly,
            // makes the change take effect without a restart.
            IntPtr manager = UnsafeNativeMethods.OpenSCManager(null, null, UnsafeNativeMethods.SC_MANAGER_CONNECT);
            if (manager == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            try
            {
                IntPtr service = UnsafeNativeMethods.OpenService(manager, ServiceName, UnsafeNativeMethods.SERVICE_CHANGE_CONFIG);
                if (service == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                try
                {
                    if (!UnsafeNativeMethods.ChangeServiceConfig(
                        service,
                        UnsafeNativeMethods.SERVICE_NO_CHANGE,
                        (uint)StartType,
                        UnsafeNativeMethods.SERVICE_NO_CHANGE,
                        null, null, IntPtr.Zero, null, null, null, null))
                    {
                        throw new Win32Exception();
                    }
                }
                finally
                {
                    UnsafeNativeMethods.CloseServiceHandle(service);
                }
            }
            finally
            {
                UnsafeNativeMethods.CloseServiceHandle(manager);
            }
        }

        public override string ToString()
        {
            return $"{ServiceName} service starts {StartType}";
        }
    }
}
