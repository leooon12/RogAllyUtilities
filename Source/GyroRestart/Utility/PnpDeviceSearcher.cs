using System.Management;

namespace GyroRestart.Utility;

internal static class PnpDeviceSearcher
{
    public static PnpDevice? Get(string name)
    {
        using var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity");

        foreach (var device in searcher.Get())
        {
            var deviceName = (string?)device.Properties["Name"].Value;

            if (!string.IsNullOrEmpty(deviceName) && deviceName.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                return new PnpDevice((ManagementObject)device);
            }
        }

        return null;
    }
}