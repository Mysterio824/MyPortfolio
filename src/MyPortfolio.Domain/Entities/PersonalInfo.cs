using System.Collections.Generic;

namespace MyPortfolio.Domain.Entities
{
    public class PersonalInfo
    {
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Summary { get; set; } = "";
        public string ProfilePictureUrl { get; set; } = "";
        public string Email { get; set; } = "";
        public string LinkedInUrl { get; set; } = "";
        public string GitHubUrl { get; set; } = "";
        public List<string> Skills { get; set; } = new List<string>();
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Education> Education { get; set; } = new List<Education>();
    }
} 