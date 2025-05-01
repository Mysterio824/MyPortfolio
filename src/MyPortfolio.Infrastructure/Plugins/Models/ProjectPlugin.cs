using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Plugins.Models;

public class ProjectPlugin
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public Project Project { get; init; } = new Project();
}