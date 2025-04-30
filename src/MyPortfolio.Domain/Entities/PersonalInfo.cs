using System.Collections.Generic;

namespace MyPortfolio.Domain.Entities
{
    public class PersonalInfo
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
} 