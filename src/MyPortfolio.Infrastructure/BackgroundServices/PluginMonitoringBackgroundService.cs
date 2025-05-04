using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Infrastructure.BackgroundServices
{
    public class PluginMonitoringBackgroundService(
        IPluginMonitoringService monitoringService,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<PluginMonitoringBackgroundService> logger
    ) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Plugin monitoring background service is starting");
            
            monitoringService.PluginsChanged += OnPluginsChanged;
            monitoringService.StartMonitoring();
            
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Plugin monitoring background service is stopping");
            
            monitoringService.PluginsChanged -= OnPluginsChanged;
            monitoringService.StopMonitoring();
            
            return base.StopAsync(cancellationToken);
        }

        private void OnPluginsChanged(object? sender, EventArgs e)
        {
            logger.LogInformation("Plugins changed, refreshing project repository");
            
            // Tạo scope mới để truy cập service scoped
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                
                // Repository needs to be notified to reload data
                if (projectRepository is IProjectDataRefreshable refreshable)
                {
                    refreshable.RefreshData();
                }
            }
        }
    }
} 