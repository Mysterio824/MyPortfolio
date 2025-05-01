using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Services;
using MyPortfolio.Infrastructure.Repositories;

namespace MyPortfolio.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services
    ) {
        
        // Register repositories
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IPersonalInfoRepository, PersonalInfoRepository>();
        services.AddSingleton<IPluginStorage, PluginStorage>();
        services.AddSingleton<IPluginLoader, PluginLoader>();
        services.AddSingleton<IPluginWatcher, PluginWatcher>();
        
        return services;
    }
} 