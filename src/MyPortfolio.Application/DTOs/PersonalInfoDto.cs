namespace MyPortfolio.Application.DTOs;

public class PersonalInfoDto
{
    public required string Name { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required string ProfilePictureUrl { get; init; }
    public required string Email { get; init; }
    public required string LinkedInUrl { get; init; }
    public required string GitHubUrl { get; init; }
    public List<string> Skills { get; set; } = [];
    public List<ExperienceDto> Experiences { get; init; } = [];
    public List<EducationDto> Education { get; init; } = [];
}