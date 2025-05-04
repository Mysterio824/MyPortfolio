using System;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;

namespace MyPortfolio.Infrastructure.Plugins.Services
{
    public class PluginMonitoringService(
        IPluginWatcher pluginWatcher,
        ILogger<PluginMonitoringService> logger
    ) : IPluginMonitoringService
    {
        private bool _isMonitoring;

        public event EventHandler? PluginsChanged;

        public void StartMonitoring()
        {
            if (_isMonitoring)
                return;

            pluginWatcher.PluginDirectoryChanged += OnPluginDirectoryChanged;
            pluginWatcher.StartWatching();
            _isMonitoring = true;
            logger.LogInformation("Plugin monitoring started");
        }

        public void StopMonitoring()
        {
            if (!_isMonitoring)
                return;

            pluginWatcher.PluginDirectoryChanged -= OnPluginDirectoryChanged;
            _isMonitoring = false;
            logger.LogInformation("Plugin monitoring stopped");
        }

        private void OnPluginDirectoryChanged(object? sender, EventArgs e)
        {
            logger.LogInformation("Plugin directory change detected");
            PluginsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
} 