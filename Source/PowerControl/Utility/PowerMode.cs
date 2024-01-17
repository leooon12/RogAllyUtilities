using System.Runtime.InteropServices;

namespace PowerControl.Utility;

internal static class PowerMode
{
    private static readonly Dictionary<string, Guid> PowerModes = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Power Saver"] = new Guid("961cc777-2547-4f9d-8174-7d86181b8a7a"),
        ["Balanced"] = new Guid("00000000-0000-0000-0000-000000000000"),
        ["High Performance"] = new Guid("ded574b5-45a0-4f42-8737-46345c09c238")
    };

    public static IEnumerable<string> PowerModeNames => PowerModes.Keys;

    public static void SetPowerMode(string name)
    {
        if (SystemInformation.PowerStatus.IsBatterySaverEnabled())
        {
            return;
        }

        var powerMode = PowerModes[name];
        var currentPowerMode = GetPowerMode();

        if (currentPowerMode != powerMode)
        {
            SetPowerMode(powerMode);
        }
    }

    private static Guid GetPowerMode()
    {
        if (PowerGetEffectiveOverlayScheme(out var powerMode) != 0)
        {
            throw new Exception("Failed to get power mode!");
        }

        return powerMode;
    }

    private static void SetPowerMode(Guid powerMode)
    {
        if (PowerSetActiveOverlayScheme(powerMode) != 0)
        {
            throw new Exception("Failed to set power mode!");
        }
    }

    [DllImport("powrprof.dll")]
    private static extern uint PowerGetEffectiveOverlayScheme(out Guid effectiveOverlayGuid);

    [DllImport("powrprof.dll")]
    private static extern uint PowerSetActiveOverlayScheme(Guid overlaySchemeGuid);
}