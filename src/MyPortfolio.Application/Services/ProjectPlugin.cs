using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Application.Services
{
    public class ProjectPlugin(
        IProjectPlugin projectPlugin, 
        IPluginLoader pluginLoader,
        IMapper mapper
    ) : IProjectService
    {
    private readonly List<IPlugin> _plugins =
        pluginLoader.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory + "/Plugins");

    public List<ProjectDto> GetAllProjects()
    {
        var projects = projectPlugin.GetAllProjects();

        foreach (var plugin in _plugins)
        {
            projects.AddRange(plugin.GetProjects());
        }

        return mapper.Map<List<ProjectDto>>(projects);
    }

    public ProjectDto? GetProjectById(Guid id)
    {
        var project = projectPlugin.GetProjectById(id);

        if (project != null) return mapper.Map<ProjectDto>(project);
        // Try to find in plugins
        foreach (var plugin in _plugins)
        {
            var pluginProject = plugin.GetProjects().FirstOrDefault(p => p.Id == id);
            if (pluginProject != null)
            {
                return mapper.Map<ProjectDto>(pluginProject);
            }
        }

        return null;
    }

    public void SaveProject(Project project)
        => projectPlugin.SaveProject(project);
    }
} 