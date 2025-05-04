namespace MyPortfolio.Application.DTOs;

public class ExperienceDto
{
    public required string Company { get; init; }
    public required string Position { get; init; }
    public required string Duration { get; init; }
    public required string Description { get; init; }
}