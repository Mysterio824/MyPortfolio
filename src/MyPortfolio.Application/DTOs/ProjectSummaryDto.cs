namespace MyPortfolio.Application.DTOs
{
    public class ProjectSummaryDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = "";
        public string ShortDescription { get; init; } = "";
        public string ImageUrl { get; init; } = "";
        public List<string> Technologies { get; init; } = [];
    }
} 