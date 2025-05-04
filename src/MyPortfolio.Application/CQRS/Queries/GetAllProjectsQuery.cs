using System.Collections.Generic;
using MyPortfolio.Application.DTOs;

namespace MyPortfolio.Application.CQRS.Queries
{
    public class GetAllProjectsQuery
    {
        // Query không cần tham số
    }
    
    public class GetAllProjectsQueryResult(IReadOnlyCollection<ProjectSummaryDto> projects)
    {
        public IReadOnlyCollection<ProjectSummaryDto> Projects { get; } = projects;
    }
} 