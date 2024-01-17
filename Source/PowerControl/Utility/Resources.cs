using System.Reflection;

namespace PowerControl.Utility;

internal static class Resources
{
    public static Icon GetTrayIcon() => new(Assembly.GetExecutingAssembly()
        .GetManifestResourceStream("PowerControl.Resources.PowerControl.ico")!);
}