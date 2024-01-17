using GyroRestart.Utility;

namespace GyroRestart.UI;

internal sealed class MainView : Form
{
    private readonly ToolStripMenuItem _statusLabel = new("StatusLabel");

    public MainView()
    {
        _statusLabel.Enabled = false;

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(_statusLabel);
        contextMenu.Items.Add(new ToolStripSeparator());
        contextMenu.Items.Add(new ToolStripMenuItem("E&xit", null, (_, _) => Close()));

        var notifyIcon = new NotifyIcon();
        notifyIcon.Icon = Resources.GetTrayIcon();
        notifyIcon.Text = @"Gyro Restart";
        notifyIcon.Visible = true;
        notifyIcon.ContextMenuStrip = contextMenu;

        Opacity = 0;
    }

    public string? Status
    {
        get => _statusLabel.Text;
        set => _statusLabel.Text = value;
    }

    public event Action? ViewShown;

    public event Action? ViewClosed;

    protected override void OnShown(EventArgs e)
    {
        Hide();

        ViewShown?.Invoke();
    }

    protected override void OnClosed(EventArgs e)
    {
        ViewClosed?.Invoke();
    }
}