using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces;

namespace MyPortfolio.Application.Services
{
    public class PersonalInfoService(
        IPersonalInfoPlugin personalInfoPlugin,
        IMapper mapper
    ) : IPersonalInfoService
    {
        public PersonalInfoDto GetPersonalInfo()
        {
            return mapper.Map<PersonalInfoDto>(personalInfoPlugin.GetPersonalInfo());
        }
        
        public void SavePersonalInfo(PersonalInfo personalInfo)
        {
            personalInfoPlugin.SavePersonalInfo(personalInfo);
        }
    }
} 