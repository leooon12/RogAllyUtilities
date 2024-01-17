using System.Security.Principal;
using GyroRestart.UI;
using GyroRestart.Utility;

namespace GyroRestart;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var mutex = new Mutex(true, "RAU_GyroRestart", out var createdNew);

        if (!createdNew)
        {
            MessageBox.Show("GyroRestart is already running!", "GyroRestart", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }
        
        if (!IsAdministrator())
        {
            MessageBox.Show("This application must be run as administrator!", "GyroRestart", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            return;
        }

        ApplicationConfiguration.Initialize();

        var mainPresenter = new MainPresenter(new MainView(), PnpDeviceSearcher.Get("Bosch Accelerometer"));
        mainPresenter.Run();
    }

    private static bool IsAdministrator()
    {
        using var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);

        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}