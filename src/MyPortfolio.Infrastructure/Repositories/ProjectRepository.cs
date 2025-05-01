using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Models;

namespace MyPortfolio.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = [];
        private readonly IPluginStorage _pluginStorage;
        private readonly ILogger<ProjectRepository> _logger;
        private readonly IPluginLoader _pluginLoader;

        public ProjectRepository(
            ILogger<ProjectRepository> logger,
            IPluginStorage pluginStorage,
            IPluginLoader pluginLoader,
            IPluginWatcher pluginWatcher)
        {
            _logger = logger;
            _pluginStorage = pluginStorage;
            _pluginLoader = pluginLoader;
            
            pluginWatcher.PluginDirectoryChanged += (_, _) => ReloadPlugins();
            pluginWatcher.StartWatching();
            
            ReloadPlugins();
        }
        
        private void ReloadPlugins()
        {
            try
            {
                var plugins = _pluginLoader.LoadPlugins();
                _projects.Clear();
                foreach (var plugin in plugins)
                {
                    _projects.Add(plugin.Project);
                }
                _logger.LogInformation("ProjectRepository loaded {Count} plugins initially", plugins.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading initial plugins in ProjectRepository");
            }
        }

        public List<Project> GetAllProjects()
        {
            return _projects;
        }

        public Project? GetProjectById(Guid id)
        {
            _logger.LogInformation($"Have: {_projects.Count}");
            // Look in local projects first
            if (_projects.FirstOrDefault(p => p.Id == id) is { } project)
            {
                return project;
            }
            
            return null;
        }

        public void SaveProject(Project project)
        {
            var existingProject = _projects.FirstOrDefault(p => p.Id == project.Id);
            if (existingProject != null)
            {
                // Update existing project
                _projects.Remove(existingProject);
            }
            
            _projects.Add(project);
        }

        public string CreateProject(Project project, string pluginName)
        {
            
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var pluginData = new ProjectPlugin
            {
                Name = pluginName,
                Version = "1.0.0",
                Project = project
            };
                
            var jsonString = JsonSerializer.Serialize(pluginData, jsonOptions);
            return _pluginStorage.StorePlugin(pluginName, jsonString);
        }
    }
} 