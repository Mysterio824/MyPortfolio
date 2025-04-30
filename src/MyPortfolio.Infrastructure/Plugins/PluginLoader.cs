using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Infrastructure.Plugins
{
    public class PluginLoader(ILogger<PluginLoader> logger) : IPluginLoader
    {
        public List<IPlugin> LoadPlugins(string directory)
        {
            var plugins = new List<IPlugin>();
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return plugins;
            }
            
            // Look for both JSON and DLL files in case we have older plugins
            foreach (var file in Directory.GetFiles(directory, "*.json"))
            {
                var plugin = LoadPlugin(file);
                if (plugin != null)
                {
                    plugins.Add(plugin);
                }
            }
            
            logger.LogInformation($"Loaded {plugins.Count} plugins from {directory}");
            return plugins;
        }

        public IPlugin? LoadPlugin(string filePath)
        {
            try
            {
                // Read the JSON file
                var jsonContent = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var pluginData = JsonSerializer.Deserialize<PluginData>(jsonContent, options);
                
                if (pluginData != null)
                {
                    return new JsonPlugin
                    {
                        Name = pluginData.Name,
                        Version = pluginData.Version,
                        Projects = pluginData.Projects
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error loading plugin from {filePath}");
            }
            
            return null;
        }
    }

    // Classes to hold plugin data
    public class PluginData
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty; 
        public List<Project> Projects { get; set; } = new List<Project>();
    }

    public class JsonPlugin : IPlugin
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public List<Project> Projects { get; set; } = new List<Project>();

        public List<Project> GetProjects()
        {
            return Projects;
        }
    }
} 