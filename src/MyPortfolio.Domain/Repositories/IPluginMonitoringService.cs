namespace MyPortfolio.Domain.Repositories
{
    public interface IPluginMonitoringService
    {
        void StartMonitoring();
        void StopMonitoring();
        event EventHandler PluginsChanged;
    }
} 