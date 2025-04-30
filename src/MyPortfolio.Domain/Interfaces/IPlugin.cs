using System.Collections.Generic;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces
{
    public interface IPlugin
    {
        List<Project> GetProjects();
    }
} 