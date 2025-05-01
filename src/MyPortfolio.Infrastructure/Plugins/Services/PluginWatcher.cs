using Microsoft.Extensions.Logging;
using MyPortfolio.Infrastructure.Plugins.Interfaces;

namespace MyPortfolio.Infrastructure.Plugins.Services
{
    public sealed class PluginWatcher : IPluginWatcher, IDisposable
    {
        private readonly ILogger<PluginWatcher> _logger;
        private readonly IPluginStorage _pluginStorage;
        private FileSystemWatcher? _watcher;
        private bool _isDisposed;
        private readonly Timer _debounceTimer;
        private readonly int _debounceDelayMs = 500;
        
        public event EventHandler? PluginDirectoryChanged;

        public PluginWatcher(
            ILogger<PluginWatcher> logger,
            IPluginStorage pluginStorage)
        {
            _logger = logger;
            _pluginStorage = pluginStorage;
            
            _debounceTimer = new Timer(_ => DebouncedReloadPlugins(), null, Timeout.Infinite, Timeout.Infinite);
        }

        public void StartWatching()
        {
            var directoryPath = _pluginStorage.GetProjectDirectoryPath();
            _logger.LogInformation("Starting plugin watcher for directory: {DirectoryPath}", directoryPath);
            
            StopWatching();

            _watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime,
                Filter = "*.json",
                EnableRaisingEvents = true
            };

            // Add event handlers
            _watcher.Changed += OnPluginDirectoryChanged;
            _watcher.Created += OnPluginDirectoryChanged;
            _watcher.Deleted += OnPluginDirectoryChanged;
            _watcher.Renamed += OnPluginDirectoryChanged;
            _watcher.Error += OnWatcherError;

            _logger.LogInformation("Plugin watcher started successfully");
        }

        public void StopWatching()
        {
            if (_watcher == null) return;
            _watcher.EnableRaisingEvents = false;
            _watcher.Changed -= OnPluginDirectoryChanged;
            _watcher.Created -= OnPluginDirectoryChanged;
            _watcher.Deleted -= OnPluginDirectoryChanged;
            _watcher.Renamed -= OnPluginDirectoryChanged;
            _watcher.Error -= OnWatcherError;
            _watcher.Dispose();
            _watcher = null;
            _logger.LogInformation("Plugin watcher stopped");
        }
        
        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            _logger.LogError(e.GetException(), "Error in FileSystemWatcher");
            
            try
            {
                StopWatching();
                StartWatching();
                _logger.LogInformation("FileSystemWatcher restarted after error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to restart FileSystemWatcher");
            }
        }

        private void OnPluginDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation("Detected change in plugin directory: {ChangeType} {FullPath}", e.ChangeType, e.FullPath);
            
            _debounceTimer.Change(_debounceDelayMs, Timeout.Infinite);
        }
        
        private void DebouncedReloadPlugins()
        {
            PluginDirectoryChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            
            if (disposing)
            {
                StopWatching();
                _debounceTimer.Dispose();
            }

            _isDisposed = true;
        }

        ~PluginWatcher()
        {
            Dispose(false);
        }
    }
} 