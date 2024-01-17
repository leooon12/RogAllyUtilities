using System.Reflection;

namespace GyroRestart.Utility;

internal static class Resources
{
    public static Icon GetTrayIcon() => new(Assembly.GetExecutingAssembly()
        .GetManifestResourceStream("GyroRestart.Resources.GyroRestart.ico")!);
}