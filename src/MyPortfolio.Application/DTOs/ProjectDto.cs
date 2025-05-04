namespace MyPortfolio.Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required string ImageUrl { get; init; }
        public required string GitHubUrl { get; init; }
        public required string LiveDemoUrl { get; init; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<string> Technologies { get; init; } = [];
        public List<string> Features { get; init; } = [];
    }
} 