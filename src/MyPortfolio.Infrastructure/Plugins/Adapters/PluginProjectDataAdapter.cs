using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Models;

namespace MyPortfolio.Infrastructure.Plugins.Adapters
{
    public class PluginProjectDataAdapter : IProjectDataStore
    {
        private readonly List<Project> _projects = [];
        private readonly IPluginStorage _pluginStorage;
        private readonly ILogger<PluginProjectDataAdapter> _logger;
        private readonly IPluginLoader _pluginLoader;
        private readonly ISerializationService _serializationService;

        public PluginProjectDataAdapter(
            ILogger<PluginProjectDataAdapter> logger,
            IPluginStorage pluginStorage,
            IPluginLoader pluginLoader,
            ISerializationService serializationService)
        {
            _logger = logger;
            _pluginStorage = pluginStorage;
            _pluginLoader = pluginLoader;
            _serializationService = serializationService;
            
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
                _logger.LogInformation("ProjectDataAdapter loaded {Count} plugins initially", plugins.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading initial plugins in ProjectDataAdapter");
            }
        }

        public IReadOnlyCollection<Project> GetAllProjects()
        {
            return _projects.AsReadOnly();
        }

        public Project? GetProjectById(Guid id)
        {
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        public void SaveProject(Project project)
        {
            var safeName = CreateSafeName(project.Title);
            var pluginName = $"ProjectPlugin_{safeName}";
            CreateProject(project, pluginName);
            ReloadPlugins();
        }

        public string CreateProject(Project project, string pluginName)
        {
            var pluginData = new ProjectPlugin
            {
                Name = pluginName,
                Project = project
            };
                
            var jsonString = _serializationService.Serialize(pluginData);
            return _pluginStorage.StorePlugin(pluginName, jsonString);
        }
        
        private static string CreateSafeName(string title)
        {
            return title.Replace(" ", "").Replace("-", "").Replace(".", "");
        }
    }
} 