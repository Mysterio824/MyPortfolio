using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Presentation.Models;

namespace MyPortfolio.Presentation.Controllers;

public class HomeController(
    IPersonalInfoService personalInfoService)
    : Controller
{
    public IActionResult Index()
    {
        var personalInfo = personalInfoService.GetPersonalInfo();
        return View(personalInfo);
    }

    public IActionResult About()
    {
        var personalInfo = personalInfoService.GetPersonalInfo();
        return View(personalInfo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}