using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Domain.Common;

public class JsonPlugin : IPlugin
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public List<Project> Projects { get; set; } = new List<Project>();

    public List<Project> GetProjects()
    {
        return Projects;
    }
}