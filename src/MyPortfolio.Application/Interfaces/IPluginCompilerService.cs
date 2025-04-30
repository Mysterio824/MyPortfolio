using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Interfaces
{
    public interface IPluginCompilerService
    {
        string CompileProjects(List<Project> projects, string pluginName, string outputDirectory);
        bool ValidateProject(Project project);
    }
} 