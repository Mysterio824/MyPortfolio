using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.BackgroundServices;
using MyPortfolio.Infrastructure.Plugins.Adapters;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Services;
using MyPortfolio.Infrastructure.Repositories;
using MyPortfolio.Infrastructure.Services;

namespace MyPortfolio.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
            
            // Data stores
            services.AddScoped<IProjectDataStore, PluginProjectDataAdapter>();
            
            // Services
            services.AddSingleton<ISerializationService, JsonSerializationService>();
            services.AddSingleton<IPluginMonitoringService, PluginMonitoringService>();
            
            // Plugin system
            services.AddSingleton<IPluginLoader, PluginLoader>();
            services.AddSingleton<IPluginStorage, PluginStorage>();
            services.AddSingleton<IPluginWatcher, PluginWatcher>();
            
            // Background services
            services.AddHostedService<PluginMonitoringBackgroundService>();

            return services;
        }
    }
} 