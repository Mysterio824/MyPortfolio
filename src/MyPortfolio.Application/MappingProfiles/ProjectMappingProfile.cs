using AutoMapper;
using MyPortfolio.Application.DTOs;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Project, ProjectDto>();
            
            CreateMap<Project, ProjectSummaryDto>()
                .ForMember(dest => dest.ShortDescription, opt => 
                    opt.MapFrom(src => TruncateDescription(src.Description, 150)));
                
            CreateMap<Project, ProjectDetailDto>()
                .ForMember(dest => dest.ShortDescription, opt => 
                    opt.MapFrom(src => TruncateDescription(src.Description, 150)))
                .ForMember(dest => dest.FullDescription, opt => 
                    opt.MapFrom(src => src.Description));
                    
            // DTO to DTO mappings
            CreateMap<ProjectSummaryDto, ProjectDto>();
            CreateMap<ProjectDetailDto, ProjectDto>()
                .ForMember(dest => dest.Description, opt => 
                    opt.MapFrom(src => src.FullDescription));

            // DTO to Entity mappings
            CreateMap<ProjectDto, Project>();
            CreateMap<ProjectDetailDto, Project>()
                .ForMember(dest => dest.Description, opt => 
                    opt.MapFrom(src => src.FullDescription));
        }
        
        private static string TruncateDescription(string description, int maxLength)
        {
            if (string.IsNullOrEmpty(description) || description.Length <= maxLength)
                return description;
            
            return description.Substring(0, maxLength) + "...";
        }
    }
} 