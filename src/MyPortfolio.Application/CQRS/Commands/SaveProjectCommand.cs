using MyPortfolio.Application.DTOs;

namespace MyPortfolio.Application.CQRS.Commands
{
    public class SaveProjectCommand(ProjectDto project)
    {
        public ProjectDto Project { get; } = project;
    }
} 