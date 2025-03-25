using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDTO, Logistician>()
            .ForMember(dest => dest.ContactId,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt,
                opt => opt.Ignore())
            .ForMember(dest => dest.HasAdminPrivileges,
                opt => opt
                    .MapFrom(src => src.HasAdminPrivileges));

        CreateMap<Logistician, UserDTO>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => src.ContactId))
            .ForMember(dest => dest.Password,
                opt => opt.Ignore())
            .ForMember(dest => dest.HasAdminPrivileges,
                opt => opt
                    .MapFrom(src => src.HasAdminPrivileges));
        
        CreateMap<UserDTO, ContactInfo>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName,
                opt => opt
                    .MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt
                    .MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone,
                opt => opt
                    .MapFrom(src => src.Phone))
            .ForMember(dest => dest.BirthDate, 
                opt => opt
                    .MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Email,
                opt => opt
                    .MapFrom(src => src.Email));
        
        CreateMap<ContactInfo, UserDTO>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName,
                opt => opt
                .MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt
                    .MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone,
                opt => opt
                    .MapFrom(src => src.Phone))
            .ForMember(dest => dest.BirthDate,
                opt => opt
                    .MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Email,
                opt => opt
                    .MapFrom(src => src.Email));
    }
}