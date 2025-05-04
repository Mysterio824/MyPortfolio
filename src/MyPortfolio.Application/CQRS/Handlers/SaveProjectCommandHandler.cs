using AutoMapper;
using MyPortfolio.Application.CQRS.Commands;
using MyPortfolio.Application.Interfaces.CQRS;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.CQRS.Handlers
{
    public class SaveProjectCommandHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
        : ICommandHandler<SaveProjectCommand>
    {
        public void Handle(SaveProjectCommand command)
        {
            var project = mapper.Map<Project>(command.Project);
            projectRepository.SaveProject(project);
        }
    }
} 