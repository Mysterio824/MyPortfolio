namespace MyPortfolio.Infrastructure.Plugins.Interfaces;

public interface IPluginWatcher
{
    event EventHandler PluginDirectoryChanged;
    void StartWatching();
    void StopWatching();
}