using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;

namespace MyPortfolio.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository, IProjectDataRefreshable
    {
        private readonly IProjectDataStore _dataStore;
        private readonly ILogger<ProjectRepository> _logger;
        private readonly IPluginLoader _pluginLoader;

        public ProjectRepository(
            IProjectDataStore dataStore,
            ILogger<ProjectRepository> logger,
            IPluginLoader pluginLoader,
            IPluginWatcher pluginWatcher)
        {
            _dataStore = dataStore;
            _logger = logger;
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
                _logger.LogInformation("ProjectRepository loaded {Count} plugins initially", plugins.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading initial plugins in ProjectRepository");
            }
        }

        public IReadOnlyCollection<Project> GetAllProjects()
        {
            return _dataStore.GetAllProjects();
        }

        public Project? GetProjectById(Guid id)
        {
            return _dataStore.GetProjectById(id);
        }

        public void SaveProject(Project project)
        {
            _dataStore.SaveProject(project);
            ReloadPlugins();
        }

        public string CreateProject(Project project)
        {
            var safeName = CreateSafeName(project.Title);
            var pluginName = $"ProjectPlugin_{safeName}";
            return _dataStore.CreateProject(project, pluginName);
        }
        
        public void RefreshData()
        {
            _logger.LogInformation("Refreshing project repository data");
            // Repository doesn't need to do anything specific as data store handles refreshing
        }
        
        private static string CreateSafeName(string title)
        {
            return title.Replace(" ", "").Replace("-", "").Replace(".", "");
        }
    }
} 