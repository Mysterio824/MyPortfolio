using MyPortfolio.Application.DTOs;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Interfaces
{
    public interface IPersonalInfoService
    {
        PersonalInfoDto GetPersonalInfo();
        void SavePersonalInfo(PersonalInfo personalInfo);
    }
} 