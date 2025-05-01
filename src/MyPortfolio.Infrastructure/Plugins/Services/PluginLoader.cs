using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Models;

namespace MyPortfolio.Infrastructure.Plugins.Services
{
    public class PluginLoader(
        ILogger<PluginLoader> logger,
        IPluginStorage pluginStorage
    ) : IPluginLoader
    {
        public List<ProjectPlugin> LoadPlugins()
        {
            var plugins = new List<ProjectPlugin>();
            var pluginFilePaths = pluginStorage.GetAllProjectPluginPaths();
            
            foreach (var filePath in pluginFilePaths)
            {
                var plugin = LoadPlugin(filePath);
                if (plugin != null)
                {
                    plugins.Add(plugin);
                }
            }
            
            logger.LogInformation("Loaded {PluginsCount} plugins", plugins.Count);
            return plugins;
        }

        private ProjectPlugin? LoadPlugin(string filePath)
        {
            try
            {
                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<ProjectPlugin>(jsonContent, options);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading plugin from {FilePath}", filePath);
            }

            return null;
        }
    }
} 