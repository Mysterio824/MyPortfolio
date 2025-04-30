using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.MappingProfiles
{
    public class PersonalInfoProfile : Profile
    {
        public PersonalInfoProfile()
        {
            CreateMap<PersonalInfo, PersonalInfoDto>();
            CreateMap<PersonalInfoDto, PersonalInfo>();
        }
    }
}
