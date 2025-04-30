using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Application.MappingProfiles;
using MyPortfolio.Application.Services;

namespace MyPortfolio.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddServices();

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectPlugin>();
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IPluginCompilerService, PluginCompilerService>();
    }
    
    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
} 