using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyPortfolio.Infrastructure.Plugins.Interfaces;

namespace MyPortfolio.Infrastructure.Plugins.Services
{
    public class PluginStorage : IPluginStorage
    {
        private readonly string _pluginDirectory;
        private readonly ILogger<PluginStorage> _logger;

        public PluginStorage(
            IConfiguration configuration, 
            IWebHostEnvironment webHostEnvironment,
            ILogger<PluginStorage> logger)
        {
            _logger = logger;
            
            var contentRoot = webHostEnvironment.ContentRootPath;
            var portfolioRoot = Directory.GetParent(contentRoot)?.Parent?.FullName;

            var configPath = configuration["PluginDataPath"] ?? "Data";
            _pluginDirectory = Path.IsPathRooted(configPath) 
                ? configPath 
                : Path.Combine(portfolioRoot ?? contentRoot, configPath);
            
            _logger.LogInformation("Plugin directory set to: {PluginDirectory}", _pluginDirectory);
            
            EnsureDirectoryExists();
        }

        public string GetProjectDirectoryPath()
        {
            return Path.Combine(_pluginDirectory, "Projects");
        }

        public string GetPersonalInfoDirectoryPath()
        {
            return Path.Combine(_pluginDirectory, "PersonalInfo");
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_pluginDirectory)) 
                Directory.CreateDirectory(_pluginDirectory);
            var path = Path.Combine(_pluginDirectory, "Projects");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(_pluginDirectory);
            path =  Path.Combine(_pluginDirectory, "PersonalInfo");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            _logger.LogInformation("Created plugin directory: {PluginDirectory}", _pluginDirectory);
        }

        public string StorePlugin(string pluginName, string serializedContent)
        {
            EnsureDirectoryExists();
            
            // Create the file path
            var path = Path.Combine(_pluginDirectory, "Projects");
            var filePath = Path.Combine(path, $"{pluginName}.json");
            
            // Write the content to the file
            File.WriteAllText(filePath, serializedContent);
            
            _logger.LogInformation("Stored plugin file at: {FilePath}", filePath);
            
            return filePath;
        }

        public List<string> GetAllProjectPluginPaths()
        {
            EnsureDirectoryExists();
            
            // Get all JSON files in the directory
            var path = Path.Combine(_pluginDirectory, "Projects");
            var files = Directory.GetFiles(path, "*.json").ToList();
            _logger.LogInformation("Found {Count} plugin files in {PluginDirectory}", files.Count, _pluginDirectory);
            
            return files;
        }
    }
} 