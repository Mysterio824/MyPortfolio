using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;

namespace MyPortfolio.Presentation.Controllers
{
    public class ProjectsController(IProjectService projectService) : Controller
    {
        public IActionResult Index()
        {
            var projects = projectService.GetAllProjects();
            return View(projects);
        }

        public IActionResult Details(Guid id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        
        public IActionResult Edit(Guid id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectDto project)
        {
            if (!ModelState.IsValid && ModelState.ContainsKey("CreatedDate"))
            {
                ModelState.Remove("CreatedDate");
                project.CreatedDate = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return View(project);
            }
            
            try
            {
                projectService.SaveProject(project);
                TempData["SuccessMessage"] = "Project updated successfully!";
                return RedirectToAction(nameof(Details), new { id = project.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating project: {ex.Message}");
                return View(project);
            }
        }
    }
} 