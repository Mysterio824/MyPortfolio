using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces
{
    public interface IPersonalInfoPlugin
    {
        PersonalInfo GetPersonalInfo();
        void SavePersonalInfo(PersonalInfo personalInfo);
    }
} 