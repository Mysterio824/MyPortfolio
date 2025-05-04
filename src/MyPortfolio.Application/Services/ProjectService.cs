using AutoMapper;
using MyPortfolio.Application.CQRS.Commands;
using MyPortfolio.Application.CQRS.Queries;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Application.Interfaces.CQRS;

namespace MyPortfolio.Application.Services
{
    public class ProjectService(
        IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult> getAllProjectsHandler,
        IQueryHandler<GetProjectByIdQuery, GetProjectByIdQueryResult> getProjectByIdHandler,
        ICommandHandler<SaveProjectCommand> saveProjectHandler,
        IMapper mapper
    ) : IProjectService
    {
        public List<ProjectDto> GetAllProjects()
        {
            var result = getAllProjectsHandler.Handle(new GetAllProjectsQuery());
            return mapper.Map<List<ProjectDto>>(result.Projects);
        }

        public ProjectDto? GetProjectById(Guid id)
        {
            var result = getProjectByIdHandler.Handle(new GetProjectByIdQuery(id));
            return result.Project != null ? mapper.Map<ProjectDto>(result.Project) : null;
        }

        public void SaveProject(ProjectDto projectDto)
        {
            saveProjectHandler.Handle(new SaveProjectCommand(projectDto));
        }
    }
} 