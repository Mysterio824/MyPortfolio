using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Common;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Services
{
    public class PluginCompilerService(ILogger<PluginCompilerService> logger) : IPluginCompilerService
    {
        public string CompileProjects(List<Project> projects, string pluginName, string outputDirectory)
        {
            // Validate all projects
            foreach (var project in projects.Where(project => !ValidateProject(project)))
            {
                throw new Exception($"Project '{project.Title}' failed validation.");
            }

            // Create output directory if it doesn't exist
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            try
            {
                // Instead of trying to compile a DLL, we'll just serialize the projects to JSON
                // This is a much simpler approach and avoids all the DLL loading issues
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                var pluginData = new PluginData
                {
                    Name = pluginName,
                    Version = "1.0.0",
                    Projects = projects
                };
                
                var jsonString = JsonSerializer.Serialize(pluginData, jsonOptions);
                
                // Write the JSON to a file
                var jsonFilePath = Path.Combine(outputDirectory, $"{pluginName}.json");
                File.WriteAllText(jsonFilePath, jsonString);
                
                logger.LogInformation("Created plugin JSON data at {JsonFilePath}", jsonFilePath);
                
                return jsonFilePath;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating plugin JSON data");
                throw new Exception($"Failed to create plugin: {ex.Message}", ex);
            }
        }

        public bool ValidateProject(Project project)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(project.Title))
                return false;

            if (string.IsNullOrWhiteSpace(project.Description))
                return false;

            // If technologies are empty, add a default one
            if (!project.Technologies.Any())
            {
                project.Technologies.Add("Unknown");
            }

            return true;
        }
    }
} 