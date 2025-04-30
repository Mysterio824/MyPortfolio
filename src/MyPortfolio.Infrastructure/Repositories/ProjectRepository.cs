using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectPlugin
    {
        private readonly List<Project> _projects = new List<Project>();
        
        public List<Project> GetAllProjects()
        {
            return _projects;
        }

        public Project? GetProjectById(Guid id)
        {
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        public void SaveProject(Project project)
        {
            if (project.Id == Guid.Empty)
            {
                project.Id = Guid.NewGuid();
                _projects.Add(project);
            }
            else
            {
                var existingProject = _projects.FirstOrDefault(p => p.Id == project.Id);
                if (existingProject != null)
                {
                    int index = _projects.IndexOf(existingProject);
                    _projects[index] = project;
                }
                else
                {
                    _projects.Add(project);
                }
            }
        }
    }
} 