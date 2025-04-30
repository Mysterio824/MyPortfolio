using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Common;
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
            
            foreach (var file in Directory.GetFiles(directory, "*.json"))
            {
                var plugin = LoadPlugin(file);
                if (plugin != null)
                {
                    plugins.Add(plugin);
                }
            }
            
            logger.LogInformation("Loaded {PluginsCount} plugins from {Directory}", plugins.Count, directory);
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
                    var distinctProjects = new Dictionary<Guid, Project>();

                    foreach (var project in pluginData.Projects)
                    {
                        if (!distinctProjects.ContainsKey(project.Id))
                        {
                            // If project has no ID, generate a new one
                            if (project.Id == Guid.Empty)
                            {
                                logger.LogInformation("Plugin {PluginDataName} has no id", pluginData.Name);
                                project.Id = Guid.NewGuid();
                            }

                            distinctProjects.Add(project.Id, project);
                        }
                        else
                        {
                            logger.LogWarning("Duplicate project found with Id {ProjectId}. Skipping duplicate.", project.Id);
                            return null;
                        }
                    }

                    logger.LogInformation("Plugin {PluginDataName} loaded with {DistinctProjectsCount} distinct projects", pluginData.Name, distinctProjects.Count);

                    return new JsonPlugin
                    {
                        Name = pluginData.Name,
                        Version = pluginData.Version,
                        Projects = distinctProjects.Values.ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading plugin from {FilePath}", filePath);
            }

            return null;
        }
    }
} 