using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.Services
{
    public class ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper
    ) : IProjectService
    {
        public List<ProjectDto> GetAllProjects()
        {
            var projects = projectRepository.GetAllProjects();
            return mapper.Map<List<ProjectDto>>(projects);
        }

        public ProjectDto? GetProjectById(Guid id)
        {
            var project = projectRepository.GetProjectById(id);

            return project != null ? mapper.Map<ProjectDto>(project) : null;
        }

        public void SaveProject(ProjectDto projectDto)
        {
            var project = mapper.Map<Project>(projectDto);
            projectRepository.SaveProject(project);
        }
    }
} 