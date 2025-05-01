using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Presentation.Models;

namespace MyPortfolio.Presentation.Controllers
{
    public class ProfileController(
        IPersonalInfoService personalInfoService,
        IMapper mapper,
        ILogger<ProfileController> logger)
        : Controller
    {
        // GET: Profile/Edit
        public IActionResult Edit()
        {
            var personalInfo = personalInfoService.GetPersonalInfo();
            return View(personalInfo);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PersonalInfoDto model, string skills)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Process skills from comma-separated string
                if (!string.IsNullOrWhiteSpace(skills))
                {
                    model.Skills = skills.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
                }
                
                // Map from DTO to domain entity
                var personalInfo = mapper.Map<PersonalInfo>(model);
                
                // Save the personal info which will also save it to the plugin
                personalInfoService.SavePersonalInfo(personalInfo);
                
                logger.LogInformation("Personal info updated successfully");
                
                TempData["SuccessMessage"] = "Profile information updated successfully!";
                return RedirectToAction("About", "Home");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating personal info");
                ModelState.AddModelError("", $"Error updating profile: {ex.Message}");
                return View(model);
            }
        }
    }
} 