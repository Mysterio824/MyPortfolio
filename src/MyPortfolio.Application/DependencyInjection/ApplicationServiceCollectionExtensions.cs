using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application.CQRS.Commands;
using MyPortfolio.Application.CQRS.Handlers;
using MyPortfolio.Application.CQRS.Queries;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Application.Interfaces.CQRS;
using MyPortfolio.Application.Services;

namespace MyPortfolio.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Auto Mapper
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions).Assembly);
            
            // CQRS Handlers
            services.AddScoped<IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult>, GetAllProjectsQueryHandler>();
            services.AddScoped<IQueryHandler<GetProjectByIdQuery, GetProjectByIdQueryResult>, GetProjectByIdQueryHandler>();
            services.AddScoped<ICommandHandler<SaveProjectCommand>, SaveProjectCommandHandler>();
            
            // Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IPluginCompilerService, PluginCompilerService>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            
            return services;
        }
    }
} 