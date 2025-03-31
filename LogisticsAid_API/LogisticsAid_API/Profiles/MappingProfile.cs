using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LogisticianDTO, Logistician>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt,
                opt => opt.Ignore())
            .ForMember(dest => dest.HasAdminPrivileges,
                opt => opt
                    .MapFrom(src => src.HasAdminPrivileges));

        CreateMap<Logistician, LogisticianDTO>()
            .ForMember(dest => dest.Password,
                opt => opt.Ignore())
            .ForMember(dest => dest.HasAdminPrivileges,
                opt => opt
                    .MapFrom(src => src.HasAdminPrivileges));
        
        CreateMap<ContactInfo, ContactInfoDTO>();
        CreateMap<ContactInfoDTO, ContactInfo>();
    }
}