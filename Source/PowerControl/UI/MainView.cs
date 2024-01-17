using PowerControl.Utility;

namespace PowerControl.UI;

internal sealed class MainView : Form
{
    private readonly ToolStripMenuItem _powerModeSelector = new("Power mode");

    private readonly ToolStripMenuItem _remainingTimeLabel = new("Remaining time");

    private string _currentPowerMode = "CurrentPowerMode";

    public MainView()
    {
        _remainingTimeLabel.Enabled = false;

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(_remainingTimeLabel);
        contextMenu.Items.Add(_powerModeSelector);
        contextMenu.Items.Add(new ToolStripSeparator());
        contextMenu.Items.Add(new ToolStripMenuItem("E&xit", null, (_, _) => Close()));

        var notifyIcon = new NotifyIcon();
        notifyIcon.Icon = Resources.GetTrayIcon();
        notifyIcon.Text = @"Power Control";
        notifyIcon.Visible = true;
        notifyIcon.ContextMenuStrip = contextMenu;

        Opacity = 0;
    }

    public string CurrentPowerMode
    {
        get => _currentPowerMode;
        set
        {
            _currentPowerMode = value;
            CurrentPowerModeChanged?.Invoke();
        }
    }

    public event Action? CurrentPowerModeChanged;

    public event Action? ViewShown;

    public event Action? ViewClosed;

    public void SetRemainingTime(TimeSpan remainingTime)
    {
        _remainingTimeLabel.Text = remainingTime.TotalSeconds <= 0
            ? "Charging"
            : $@"Remaining time {remainingTime:hh\:mm}";
    }

    public void SetPowerModes(IEnumerable<string> powerModes, string currentPowerMode)
    {
        if (_powerModeSelector.HasDropDownItems)
        {
            foreach (ToolStripMenuItem item in _powerModeSelector.DropDownItems)
            {
                item.Click -= OnPowerModeSelected;
            }

            _powerModeSelector.DropDownItems.Clear();
        }

        foreach (var powerMode in powerModes)
        {
            var item = new ToolStripMenuItem(powerMode);
            item.Click += OnPowerModeSelected;

            if (powerMode.Equals(currentPowerMode, StringComparison.OrdinalIgnoreCase))
            {
                _currentPowerMode = currentPowerMode;
                item.Checked = true;
            }

            _powerModeSelector.DropDownItems.Add(item);
        }
    }

    protected override void OnShown(EventArgs e)
    {
        Hide();

        ViewShown?.Invoke();
    }

    protected override void OnClosed(EventArgs e)
    {
        foreach (ToolStripMenuItem item in _powerModeSelector.DropDownItems)
        {
            item.Click -= OnPowerModeSelected;
        }

        ViewClosed?.Invoke();
    }

    private void SelectPowerMode(ToolStripMenuItem selectedItem)
    {
        if (CurrentPowerMode.Equals(selectedItem.Text, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        foreach (ToolStripMenuItem item in _powerModeSelector.DropDownItems)
        {
            item.Checked = false;
        }

        selectedItem.Checked = true;
        CurrentPowerMode = selectedItem.Text!;
    }

    private void OnPowerModeSelected(object? sender, EventArgs e)
    {
        SelectPowerMode((ToolStripMenuItem)sender!);
    }
}