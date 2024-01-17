using System.Reflection;

namespace PowerControl.Utility;

internal static class PowerStatusExtensions
{
    private static readonly FieldInfo SystemPowerStatusField;
    private static readonly FieldInfo SystemStatusFlagField;

    static PowerStatusExtensions()
    {
        SystemPowerStatusField =
            typeof(PowerStatus).GetField("_systemPowerStatus", BindingFlags.Instance | BindingFlags.NonPublic)!;
        SystemStatusFlagField =
            SystemPowerStatusField.FieldType.GetField("SystemStatusFlag",
                BindingFlags.Instance | BindingFlags.NonPublic)!;
    }

    public static bool IsBatterySaverEnabled(this PowerStatus powerStatus) =>
        (byte)SystemStatusFlagField.GetValue(SystemPowerStatusField.GetValue(powerStatus))! > 0;
}