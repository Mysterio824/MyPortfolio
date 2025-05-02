using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Plugins.Models;

public class PersonalInfoPlugin
{
    public string Name { get; set; } = string.Empty;
    public PersonalInfo PersonalInfo { get; init; } = new();
}