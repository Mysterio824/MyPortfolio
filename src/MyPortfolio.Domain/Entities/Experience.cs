namespace MyPortfolio.Domain.Entities;

public class Experience
{
    public required string Company { get; set; }
    public required string Position { get; set; }
    public required string Duration { get; set; }
    public required string Description { get; set; }
}