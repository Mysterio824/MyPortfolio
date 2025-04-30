using System;
using System.Collections.Generic;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces
{
    public interface IProjectPlugin
    {
        List<Project> GetAllProjects();
        Project? GetProjectById(Guid id);
        void SaveProject(Project project);
    }
} 