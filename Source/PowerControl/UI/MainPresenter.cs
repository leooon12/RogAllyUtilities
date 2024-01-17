using System.Timers;
using PowerControl.Utility;
using Timer = System.Timers.Timer;

namespace PowerControl.UI;

internal sealed class MainPresenter(MainView view, Configuration configuration)
{
    private readonly Timer _timer = new(TimeSpan.FromSeconds(configuration.PollingInterval));

    public void Run()
    {
        view.ViewShown += OnViewShown;
        view.ViewClosed += OnViewClosed;

        Application.Run(view);
    }

    private void OnViewShown()
    {
        view.CurrentPowerModeChanged += OnPowerModeChanged;
        view.SetPowerModes(PowerMode.PowerModeNames, configuration.CurrentPowerMode);

        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();

        Update();
    }

    private void OnViewClosed()
    {
        view.ViewShown -= OnViewShown;
        view.ViewClosed -= OnViewClosed;
        view.CurrentPowerModeChanged -= OnPowerModeChanged;

        _timer.Stop();
        _timer.Elapsed -= OnTimerElapsed;
    }

    private void OnPowerModeChanged()
    {
        configuration.CurrentPowerMode = view.CurrentPowerMode;
        SetPowerMode();
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Update();
    }

    private void Update()
    {
        view.SetRemainingTime(TimeSpan.FromSeconds(SystemInformation.PowerStatus.BatteryLifeRemaining));
        SetPowerMode();
    }

    private void SetPowerMode()
    {
        lock (_timer)
        {
            PowerMode.SetPowerMode(view.CurrentPowerMode);
        }
    }
}