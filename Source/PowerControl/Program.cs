using PowerControl.UI;
using PowerControl.Utility;

namespace PowerControl;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var mutex = new Mutex(true, "RAU_PowerControl", out var createdNew);

        if (!createdNew)
        {
            MessageBox.Show("PowerControl is already running!", "PowerControl", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }
        
        ApplicationConfiguration.Initialize();

        var mainPresenter = new MainPresenter(new MainView(), Configuration.LoadOrCreate());
        mainPresenter.Run();
    }
}