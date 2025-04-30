using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.MappingProfiles
{
    public class PersonalInfoProfile : Profile
    {
        public PersonalInfoProfile()
        {
            CreateMap<PersonalInfo, PersonalInfoDto>().ReverseMap();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
            CreateMap<Education, EducationDto>().ReverseMap();
        }
    }
}