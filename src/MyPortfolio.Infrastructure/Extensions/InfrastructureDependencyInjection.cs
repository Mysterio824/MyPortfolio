using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Domain.Interfaces;
using MyPortfolio.Infrastructure.Plugins;
using MyPortfolio.Infrastructure.Repositories;

namespace MyPortfolio.Infrastructure.Extensions;

public static class InfrastructureDependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services
    ) {
        
        // Register repositories
        services.AddSingleton<IProjectPlugin, ProjectRepository>();
        services.AddSingleton<IPersonalInfoPlugin, PersonalInfoRepository>();
        services.AddSingleton<IPluginLoader, PluginLoader>();

        return services;
    }
} 