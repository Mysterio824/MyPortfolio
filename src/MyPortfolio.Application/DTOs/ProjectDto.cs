namespace MyPortfolio.Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required string GitHubUrl { get; set; }
        public required string LiveDemoUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<string> Technologies { get; set; } = [];
        public List<string> Features { get; set; } = [];
    }
} 