using AutoMapper;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;

namespace LogisticsAid_API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Logistician
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
        
        // Contact Info
        CreateMap<ContactInfo, ContactInfoDTO>();
        CreateMap<ContactInfoDTO, ContactInfo>();

        // Trip
        CreateMap<TripDTO, Trip>()
            .ForMember(dest => dest.Id, 
                opt => opt
                    .MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.LogisticianId, 
                opt => opt
                    .MapFrom(src => src.Logistician.ContactInfo.Id))
            .ForMember(dest => dest.CarrierCompanyId, 
                opt => opt
                    .MapFrom(src => src.CarrierCompany ))
            .ForMember(dest => dest.CustomerCompanyId, 
                opt => opt
                    .MapFrom(src => src.CustomerCompany))
            .ForMember(dest => dest.DriverId, 
                opt => opt
                    .MapFrom(src => src.Driver.ContactInfo.Id))
            .ForMember(dest => dest.TruckId, 
            opt => opt
                .MapFrom(src => src.Truck.LicensePlate))
            .ForMember(dest => dest.TrailerId, 
            opt => opt
                .MapFrom(src => src.Trailer.LicensePlate))
            .ForMember
            (dest => dest.RoutePoints, 
            opt => opt
                .Ignore())
            .ForMember(dest => dest.CustomerCompany,
            opt => opt.
                Ignore())
            .ForMember(dest => dest.CarrierCompany, 
            opt => opt
                .Ignore())
            .ForMember(dest => dest.Driver, 
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Truck, 
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Trailer, 
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Logistician, 
                opt => opt
                    .Ignore());

        CreateMap<Trip, TripDTO>()
            .ForMember(dest => dest.Id, 
                opt => opt
                    .MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Logistician, 
                opt => opt
                    .MapFrom(src => src.Logistician))
            .ForMember(dest => dest.CarrierCompany, 
                opt => opt
                    .MapFrom(src => src.CarrierCompany))
            .ForMember(dest => dest.CustomerCompany, 
                opt => opt
                    .MapFrom(src => src.CustomerCompany))
            .ForMember(dest => dest.Driver, 
                opt => opt
                    .MapFrom(src => src.Driver))
            .ForMember(dest => dest.Truck, 
                opt => opt
                    .MapFrom(src => src.Truck))
            .ForMember(dest => dest.Trailer, 
                opt => opt
                    .MapFrom(src => src.Trailer))
            .ForMember(dest => dest.RoutePoints, 
                opt => opt
                    .MapFrom(src => src.RoutePoints));

        
        // Customer
        CreateMap<CustomerCompanyDTO, CustomerCompany>()
            .ForMember(dest => dest.CompanyName, 
                opt => opt
                    .MapFrom(src => src.CompanyName))
            
            .ForMember(dest => dest.Contacts, 
                opt => opt
                    .Ignore()); 

        CreateMap<CustomerCompany, CustomerCompanyDTO>()
            .ForMember(dest => dest.CompanyName, 
                opt => opt
                    .MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contacts, 
                opt => opt
                    .MapFrom(src => src.Contacts));
        
        // Carrier
        CreateMap<CarrierCompanyDTO, CarrierCompany>()
            .ForMember(dest => dest.CompanyName, 
                opt => opt
                    .MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contacts, 
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Drivers, 
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Transport, 
                opt => opt
                    .Ignore());

        CreateMap<CarrierCompany, CarrierCompanyDTO>()
            .ForMember(dest => dest.CompanyName,
                opt => opt
                    .MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contacts,
                opt => opt
                    .MapFrom(src => src.Contacts))
            .ForMember(dest => dest.Drivers,
                opt => opt
                    .MapFrom(src => src.Drivers))
            .ForMember(dest => dest.Transport,
                opt => opt
                    .MapFrom(src => src.Transport));
        
        // Driver
        CreateMap<DriverDTO, Driver>()
            .ForMember(dest => dest.ContactId, 
                opt => opt
                    .MapFrom(src => src.ContactInfo.Id))
            .ForMember(dest => dest.License, 
                opt => opt
                    .MapFrom(src => src.License)) 
            .ForMember(dest => dest.CarrierCompanyId, 
            opt => opt
                .MapFrom(src => src.CarrierCompany.CompanyName))
            .ForMember(dest => dest.CarrierCompany, 
            opt => opt
                .Ignore()); 

        CreateMap<Driver, DriverDTO>()
            .ForMember(dest => dest.ContactInfo, 
                opt => opt
                    .MapFrom(src => src.ContactInfo)) 
            .ForMember(dest => dest.License, 
                opt => opt
                    .MapFrom(src => src.License)) 
            .ForMember(dest => dest.CarrierCompany, 
                opt => opt
                    .MapFrom(src => src.CarrierCompany));
    
        // Transport
        CreateMap<TransportDTO, Transport>()
            .ForMember(dest => dest.LicensePlate, 
                opt => opt
                    .MapFrom(src => src.LicensePlate))
            .ForMember(dest => dest.CarrierCompanyId, 
                opt => opt
                    .MapFrom(src => src.CarrierCompany.CompanyName))
            .ForMember(dest => dest.TransportType, 
                opt => opt
                    .MapFrom(src => src.TransportType))
            .ForMember(dest => dest.Brand, 
                opt => opt
                    .MapFrom(src => src.Brand)) 
            .ForMember(dest => dest.CarrierCompany, 
                opt => opt
                    .Ignore());

        CreateMap<Transport, TransportDTO>()
            .ForMember(dest => dest.LicensePlate, 
                opt => opt
                    .MapFrom(src => src.LicensePlate))
            .ForMember(dest => dest.TransportType, 
                opt => opt
                    .MapFrom(src => src.TransportType))
            .ForMember(dest => dest.Brand, 
                opt => opt
                    .MapFrom(src => src.Brand))
            .ForMember(dest => dest.CarrierCompany, 
                opt => opt
                    .MapFrom(src => src.CarrierCompany));
        
        // RoutePoint
        CreateMap<RoutePointDTO, RoutePoint>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.AddressId,
                opt => opt
                    .MapFrom(src => src.Address.Id))
            .ForMember(dest => dest.Type,
                opt => opt
                    .MapFrom(src => src.Type))
            .ForMember(dest => dest.Sequence,
                opt => opt
                    .MapFrom(src => src.Sequence))
            .ForMember(dest => dest.CompanyName,
                opt => opt
                    .MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.AdditionalInfo,
                opt => opt
                    .MapFrom(src => src.AdditionalInfo))
            .ForMember(dest => dest.Address,
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.Trips,
                opt => opt
                    .Ignore())
            .ForMember(dest => dest.RoutePointTrips,
                opt => opt
                    .Ignore());

        CreateMap<RoutePoint, RoutePointDTO>()
            .ForMember(dest => dest.Id,
                opt => opt
                    .MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Type,
                opt => opt
                    .MapFrom(src => src.Type))
            .ForMember(dest => dest.Sequence,
                opt => opt
                    .MapFrom(src => src.Sequence))
            .ForMember(dest => dest.CompanyName,
                opt => opt
                    .MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.AdditionalInfo,
                opt => opt
                    .MapFrom(src => src.AdditionalInfo))
            .ForMember(dest => dest.Address,
                opt => opt
                    .MapFrom(src => src.Address))
            .ForMember(dest => dest.Trips,
                opt => opt
                    .MapFrom(src => src.Trips));
        
        // Address
        CreateMap<AddressDTO, Address>()
            .ForMember(dest => dest.Id, 
                opt => opt
                    .MapFrom(src => Guid.Parse(src.Id))) 
            .ForMember(dest => dest.Number, 
                opt => opt
                    .MapFrom(src => src.Number))
            .ForMember(dest => dest.Street, 
                opt => opt
                    .MapFrom(src => src.Street))
            .ForMember(dest => dest.City, 
                opt => opt
                    .MapFrom(src => src.City))
            .ForMember(dest => dest.Province, 
                opt => opt
                    .MapFrom(src => src.Province))
            .ForMember(dest => dest.Country, 
                opt => opt
                    .MapFrom(src => src.Country))
            .ForMember(dest => dest.Latitude, 
                opt => opt
                    .MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, 
                opt => opt
                    .MapFrom(src => src.Longitude));

        CreateMap<Address, AddressDTO>()
            .ForMember(dest => dest.Id, 
                opt => opt
                    .MapFrom(src => src.Id.ToString())) 
            .ForMember(dest => dest.Number, 
                opt => opt
                    .MapFrom(src => src.Number))
            .ForMember(dest => dest.Street, 
                opt => opt
                    .MapFrom(src => src.Street))
            .ForMember(dest => dest.City, 
                opt => opt
                    .MapFrom(src => src.City))
            .ForMember(dest => dest.Province, 
                opt => opt
                    .MapFrom(src => src.Province))
            .ForMember(dest => dest.Country, 
                opt => opt
                    .MapFrom(src => src.Country))
            .ForMember(dest => dest.Latitude, 
                opt => opt
                    .MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, 
                opt => opt
                    .MapFrom(src => src.Longitude));
    }
}