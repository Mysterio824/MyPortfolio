using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Interfaces;

namespace MyPortfolio.Presentation.Controllers
{
    public class ProjectsController(IProjectService projectService) : Controller
    {
        // GET: Projects
        public IActionResult Index()
        {
            var projects = projectService.GetAllProjects();
            return View(projects);
        }

        // GET: Projects/Details/5
        public IActionResult Details(Guid id)
        {
            var project = projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
} 