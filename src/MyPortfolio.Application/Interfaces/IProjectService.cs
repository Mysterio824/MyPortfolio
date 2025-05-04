using MyPortfolio.Application.DTOs;

namespace MyPortfolio.Application.Interfaces
{
    public interface IProjectService
    {
        List<ProjectDto> GetAllProjects();
        ProjectDto? GetProjectById(Guid id);
        void SaveProject(ProjectDto project);
    }
} 