namespace MyPortfolio.Application.DTOs
{
    public class PersonalInfoDto
    {
        public required string Name { get; set; }
        public required string Title { get; set; }
        public required string Summary { get; set; }
        public required string ProfilePictureUrl { get; set; }
        public required string Email { get; set; }
        public required string LinkedInUrl { get; set; }
        public required string GitHubUrl { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Education> Education { get; set; } = new List<Education>();
    }

    public class Experience
    {
        public required string Company { get; set; }
        public required string Position { get; set; }
        public required string Duration { get; set; }
        public required string Description { get; set; }
    }

    public class Education
    {
        public required string Institution { get; set; }
        public required string Degree { get; set; }
        public required string Duration { get; set; }
    }
} 