using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Common;

// Class to hold the plugin data
public class PluginData
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public List<Project> Projects { get; set; } = new List<Project>();
}