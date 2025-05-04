namespace MyPortfolio.Application.DTOs
{
    public class ProjectDetailDto : ProjectSummaryDto
    {
        public string FullDescription { get; init; } = "";
        public string GitHubUrl { get; init; } = "";
        public string LiveDemoUrl { get; init; } = "";
        public DateTime CreatedDate { get; init; }
        public List<string> Features { get; init; } = [];
    }
} 