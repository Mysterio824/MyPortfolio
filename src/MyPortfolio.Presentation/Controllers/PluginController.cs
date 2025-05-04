using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Presentation.Models;

namespace MyPortfolio.Presentation.Controllers
{
    public class PluginController(
        IPluginCompilerService pluginCompilerService,
        ILogger<PluginController> logger)
        : Controller
    {
        // GET: Plugin/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PluginCreateViewModel
            {
                Technologies = [""],
                Features = []
            };
            return View(model);
        }

        // POST: Plugin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PluginCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var project = new ProjectDto
                {
                    Technologies = model.Technologies,
                    Features = model.Features,
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = !string.IsNullOrWhiteSpace(model.ImageUrl) ? model.ImageUrl : "/images/projects/default.jpg",
                    GitHubUrl = !string.IsNullOrWhiteSpace(model.GitHubUrl) ? model.GitHubUrl : "https://github.com",
                    LiveDemoUrl = !string.IsNullOrWhiteSpace(model.LiveDemoUrl) ? model.LiveDemoUrl : "#"
                };

                if (!pluginCompilerService.ValidateProject(project))
                {
                    ModelState.AddModelError("", "Project validation failed. Please check all fields.");
                    return View(model);
                }
                
                // Compile the plugin
                var safeName = project.Title.Replace(" ", "").Replace("-", "").Replace(".", "");
                var pluginName = $"ProjectPlugin_{safeName}";
                
                // The plugin compiler will handle storing the file in the correct location
                var pluginFilePath = pluginCompilerService.CompileProject(project);
                
                logger.LogInformation("Created plugin {PluginName} at {PluginFilePath}", pluginName, pluginFilePath);
                
                TempData["SuccessMessage"] = $"Project '{model.Title}' created successfully!";
                return RedirectToAction("Index", "Projects");
            }
            catch (Exception ex)
            {
                // Log the detailed exception
                logger.LogError(ex, "Error creating plugin");
                    
                // Check for inner exception which may have more details
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException, "Inner exception from plugin creation");
                }
                    
                ModelState.AddModelError("", $"Error creating plugin: {ex.Message}");
            }

            return View(model);
        }
    }
} 