using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Infrastructure.Repositories
{
    public class PersonalInfoRepository : IPersonalInfoPlugin
    {
        private PersonalInfo _personalInfo = new()
        {
            Name = "John Doe",
            Title = "Full Stack Developer",
            Summary = "Passionate software developer with expertise in .NET Core, ASP.NET MVC, and Clean Architecture principles. I enjoy creating efficient, maintainable, and scalable applications.",
            ProfilePictureUrl = "/images/profile.jpg",
            Email = "john.doe@example.com",
            LinkedInUrl = "https://linkedin.com/in/johndoe",
            GitHubUrl = "https://github.com/johndoe",
            Skills = new List<string>
            {
                "C#", ".NET Core", "ASP.NET MVC", "Clean Architecture", "Entity Framework", 
                "HTML", "CSS", "JavaScript", "React", "SQL Server"
            },
            Experiences = new List<Experience>
            {
                new Experience
                {
                    Company = "ABC Corporation",
                    Position = "Senior Developer",
                    Duration = "2021 - Present",
                    Description = "Developing enterprise applications using .NET technologies"
                },
                new Experience
                {
                    Company = "XYZ Solutions",
                    Position = "Software Developer",
                    Duration = "2018 - 2021",
                    Description = "Worked on various web application projects"
                }
            },
            Education = new List<Education>
            {
                new Education
                {
                    Institution = "University of Technology",
                    Degree = "Master of Computer Science",
                    Duration = "2016 - 2018"
                },
                new Education
                {
                    Institution = "City College",
                    Degree = "Bachelor of Computer Science",
                    Duration = "2012 - 2016"
                }
            }
        };

        // Initialize with default personal info

        public PersonalInfo GetPersonalInfo()
        {
            return _personalInfo;
        }

        public void SavePersonalInfo(PersonalInfo personalInfo)
        {
            _personalInfo = personalInfo;
        }
    }
} 