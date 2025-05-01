using MyPortfolio.Application.DTOs;

namespace MyPortfolio.Application.Interfaces
{
    public interface IPluginCompilerService
    {
        string CompileProject(ProjectDto projectDto, string pluginName);
        
        bool ValidateProject(ProjectDto project);
    }
} 