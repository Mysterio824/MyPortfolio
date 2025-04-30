namespace MyPortfolio.Application.DTOs;

public class PersonalInfoDto
{
    public required string Name { get; set; }
    public required string Title { get; set; }
    public required string Summary { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string Email { get; set; }
    public required string LinkedInUrl { get; set; }
    public required string GitHubUrl { get; set; }
    public List<string> Skills { get; set; } = [];
    public List<ExperienceDto> Experiences { get; set; } = [];
    public List<EducationDto> Education { get; set; } = [];
}