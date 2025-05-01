using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.Services
{
    public class PluginCompilerService(
        ILogger<PluginCompilerService> logger,
        IMapper mapper,
        IProjectRepository projectRepository)  
        : IPluginCompilerService
    {
        public string CompileProject(ProjectDto projectDto, string pluginName)
        {
            var project = mapper.Map<Project>(projectDto);
            
            if(!ValidateProject(projectDto))
            {
                throw new Exception($"Project '{projectDto.Title}' failed validation.");
            }

            try
            {
                
                // Store the plugin using the storage service
                var filePath = projectRepository.CreateProject(project, pluginName);
                
                logger.LogInformation("Created plugin JSON data at {JsonFilePath}", filePath);
                
                return filePath;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating plugin JSON data");
                throw new Exception($"Failed to create plugin: {ex.Message}", ex);
            }
        }

        public bool ValidateProject(ProjectDto project)
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