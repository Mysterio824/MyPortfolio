using System.Collections.Generic;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }
        List<Project> GetProjects();
    }
} 