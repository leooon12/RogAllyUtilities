using GyroRestart.Utility;
using Microsoft.Win32;

namespace GyroRestart.UI;

internal sealed class MainPresenter(MainView view, PnpDevice? device)
{
    public void Run()
    {
        view.ViewShown += OnViewShown;
        view.ViewClosed += OnViewClosed;

        Application.Run(view);
    }

    private void OnViewShown()
    {
        SystemEvents.PowerModeChanged += OnPowerModeChanged;

        RestartDevice();
    }

    private void OnViewClosed()
    {
        view.ViewShown -= OnViewShown;
        view.ViewClosed -= OnViewClosed;

        SystemEvents.PowerModeChanged -= OnPowerModeChanged;
    }

    private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        if (e.Mode == PowerModes.Resume)
        {
            RestartDevice();
        }
    }

    private void RestartDevice()
    {
        if (device is null)
        {
            view.Status = "Device was not found!";
            return;
        }

        device.Restart();
        view.Status = $"{device.Name} (restarted {DateTime.Now.ToLongTimeString()})";
    }
}