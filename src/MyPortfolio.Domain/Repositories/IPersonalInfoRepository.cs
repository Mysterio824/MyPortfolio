using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Repositories
{
    public interface IPersonalInfoRepository
    {
        PersonalInfo GetPersonalInfo();
        void SavePersonalInfo(PersonalInfo personalInfo);
    }
} 