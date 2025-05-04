using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Repositories
{
    public interface IProjectDataStore
    {
        IReadOnlyCollection<Project> GetAllProjects();
        Project? GetProjectById(Guid id);
        void SaveProject(Project project);
        string CreateProject(Project project, string pluginName);
    }
} 