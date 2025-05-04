using AutoMapper;
using MyPortfolio.Application.CQRS.Queries;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces.CQRS;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.CQRS.Handlers
{
    public class GetProjectByIdQueryHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
        : IQueryHandler<GetProjectByIdQuery, GetProjectByIdQueryResult>
    {
        public GetProjectByIdQueryResult Handle(GetProjectByIdQuery query)
        {
            var project = projectRepository.GetProjectById(query.Id);
            if (project == null)
            {
                return new GetProjectByIdQueryResult(null);
            }
            
            var projectDto = mapper.Map<ProjectDetailDto>(project);
            return new GetProjectByIdQueryResult(projectDto);
        }
    }
} 