using System.Management;

namespace GyroRestart.Utility;

internal sealed class PnpDevice(ManagementObject device)
{
    public string Name { get; } = device.Properties[nameof(Name)].Value.ToString()!;

    public void Restart()
    {
        Disable();
        Enable();
    }

    private void Disable()
    {
        device.InvokeMethod(nameof(Disable), null, null);
    }

    private void Enable()
    {
        device.InvokeMethod(nameof(Enable), null, null);
    }
}