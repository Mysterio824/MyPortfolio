using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyPortfolio.Application.CQRS.Queries;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces.CQRS;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.CQRS.Handlers
{
    public class GetAllProjectsQueryHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
        : IQueryHandler<GetAllProjectsQuery, GetAllProjectsQueryResult>
    {
        public GetAllProjectsQueryResult Handle(GetAllProjectsQuery query)
        {
            var projects = projectRepository.GetAllProjects();
            var projectDtos = mapper.Map<List<ProjectSummaryDto>>(projects);
            
            return new GetAllProjectsQueryResult(projectDtos);
        }
    }
} 