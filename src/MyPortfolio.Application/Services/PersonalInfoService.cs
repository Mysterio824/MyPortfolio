using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Application.Interfaces;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Application.Services
{
    public class PersonalInfoService(
        IPersonalInfoRepository personalInfoRepository,
        IMapper mapper
    ) : IPersonalInfoService
    {
        public PersonalInfoDto GetPersonalInfo()
        {
            return mapper.Map<PersonalInfoDto>(personalInfoRepository.GetPersonalInfo());
        }
        
        public void SavePersonalInfo(PersonalInfo personalInfo)
        {
            personalInfoRepository.SavePersonalInfo(personalInfo);
        }
    }
} 