using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;
using MyPortfolio.Infrastructure.Plugins.Interfaces;
using MyPortfolio.Infrastructure.Plugins.Models;

namespace MyPortfolio.Infrastructure.Repositories
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly ILogger<PersonalInfoRepository> _logger;
        private PersonalInfo _personalInfo = new();
        private readonly string _personalInfoPluginName = "PersonalInfoPlugin";
        private readonly string _personalInfoDirectory;

        public PersonalInfoRepository(
            ILogger<PersonalInfoRepository> logger,
            IPluginStorage pluginStorage,
            IPluginWatcher pluginWatcher)
        {
            _logger = logger;

            _personalInfoDirectory = pluginStorage.GetPersonalInfoDirectoryPath();
            
            if (!Directory.Exists(_personalInfoDirectory))
            {
                Directory.CreateDirectory(_personalInfoDirectory);
                _logger.LogInformation("Created personal info directory: {Directory}", _personalInfoDirectory);
            }

            // Subscribe to plugin directory changes
            pluginWatcher.PluginDirectoryChanged += (_, _) => LoadFromStorage();
            
            // Initial load
            LoadFromStorage();
        }

        public PersonalInfo GetPersonalInfo()
        {
            return _personalInfo;
        }

        public void SavePersonalInfo(PersonalInfo personalInfo)
        {
            _personalInfo = personalInfo;
            SaveToStorage(personalInfo);
        }
        
        private void LoadFromStorage()
        {
            try
            {
                var filePath = Path.Combine(_personalInfoDirectory, $"{_personalInfoPluginName}.json");
                
                if (!File.Exists(filePath))
                {
                    _logger.LogInformation("No personal info file found at {FilePath}, using default", filePath);
                    return;
                }
                
                var jsonContent = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var pluginData = JsonSerializer.Deserialize<PersonalInfoPlugin>(jsonContent, options);
                
                if (pluginData?.PersonalInfo != null)
                {
                    _personalInfo = pluginData.PersonalInfo;
                    _logger.LogInformation("Loaded personal info from: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading personal info");
            }
        }
        
        private void SaveToStorage(PersonalInfo personalInfo)
        {
            try
            {
                var pluginData = new PersonalInfoPlugin
                {
                    Name = _personalInfoPluginName,
                    Version = "1.0.0",
                    PersonalInfo = personalInfo
                };
                
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                var jsonString = JsonSerializer.Serialize(pluginData, jsonOptions);
                var filePath = Path.Combine(_personalInfoDirectory, $"{_personalInfoPluginName}.json");
                
                File.WriteAllText(filePath, jsonString);
                
                _logger.LogInformation("Saved personal info to: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving personal info");
            }
        }
    }
} 