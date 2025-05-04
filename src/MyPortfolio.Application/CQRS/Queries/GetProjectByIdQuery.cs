using System;
using MyPortfolio.Application.DTOs;

namespace MyPortfolio.Application.CQRS.Queries
{
    public class GetProjectByIdQuery(Guid id)
    {
        public Guid Id { get; } = id;
    }
    
    public class GetProjectByIdQueryResult(ProjectDetailDto? project)
    {
        public ProjectDetailDto? Project { get; } = project;
    }
} 