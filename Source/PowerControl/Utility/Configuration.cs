using Newtonsoft.Json;

namespace PowerControl.Utility;

internal sealed class Configuration
{
    private const string Path = "Configuration.json";

    private string _currentPowerMode = PowerMode.PowerModeNames.ElementAt(1);

    private int _pollingInterval = 10;

    public string CurrentPowerMode
    {
        get => _currentPowerMode;
        set
        {
            _currentPowerMode = value;
            Save();
        }
    }

    public int PollingInterval
    {
        get => _pollingInterval;
        set
        {
            _pollingInterval = value;
            Save();
        }
    }

    public static Configuration LoadOrCreate()
    {
        var configuration = Load();

        if (configuration is null)
        {
            configuration = new Configuration();
            configuration.Save();
        }

        return configuration;
    }

    private static Configuration? Load() => File.Exists(Path)
        ? JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Path))
        : null;

    private void Save()
    {
        File.WriteAllText(Path, JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}