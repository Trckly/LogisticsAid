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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.LogisticianId, opt => opt.MapFrom(src => src.Logistician.ContactInfo.Id))
            .ForMember(dest => dest.CarrierId, opt => opt.MapFrom(src => src.Carrier.Contact.Id ))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Contact.Id))
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.Driver.Contact.Id))
            .ForMember(dest => dest.TransportId, opt => opt.MapFrom(src => src.Transport.LicensePlate))
            .ForMember(dest => dest.RoutePoints, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Carrier, opt => opt.Ignore())
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ForMember(dest => dest.Transport, opt => opt.Ignore())
            .ForMember(dest => dest.Logistician, opt => opt.Ignore());

        CreateMap<Trip, TripDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Logistician, opt => opt.MapFrom(src => src.Logistician))
            .ForMember(dest => dest.Carrier, opt => opt.MapFrom(src => src.Carrier))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => src.Driver))
            .ForMember(dest => dest.Transport, opt => opt.MapFrom(src => src.Transport))
            .ForMember(dest => dest.RoutePoints, opt => opt.MapFrom(src => src.RoutePoints));

        
        // Customer
        CreateMap<CustomerDTO, Customer>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Contact.Id))
            .ForMember(dest => dest.Contact, opt => opt.Ignore()); 

        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact));
        
        // Carrier
        CreateMap<CarrierDTO, Carrier>()
            .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Contact.Id))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contact, opt => opt.Ignore());

        CreateMap<Carrier, CarrierDTO>()
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName));
        
        // Driver
        CreateMap<DriverDTO, Driver>()
            .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Contact.Id))
            .ForMember(dest => dest.License, opt => opt.MapFrom(src => src.License)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Contact, opt => opt.Ignore()); 

        CreateMap<Driver, DriverDTO>()
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact)) 
            .ForMember(dest => dest.License, opt => opt.MapFrom(src => src.License)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName));
    
        // Transport
        CreateMap<TransportDTO, Transport>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate)) 
            .ForMember(dest => dest.TruckBrand, opt => opt.MapFrom(src => src.TruckBrand)) 
            .ForMember(dest => dest.TrailerLicensePlate, opt => opt.MapFrom(src => src.TrailerLicensePlate)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName));

        CreateMap<Transport, TransportDTO>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
            .ForMember(dest => dest.TruckBrand, opt => opt.MapFrom(src => src.TruckBrand))
            .ForMember(dest => dest.TrailerLicensePlate, opt => opt.MapFrom(src => src.TrailerLicensePlate))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName));
        
        // RoutePoint
        CreateMap<RoutePointDTO, RoutePoint>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.Id))
            .ForMember(dest => dest.ContactInfoId, opt => opt.MapFrom(src => src.ContactInfo.Id)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName)) 
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence))
            .ForMember(dest => dest.Trip, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.ContactInfo, opt => opt.Ignore());

        CreateMap<RoutePoint, RoutePointDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)) 
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo)) 
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence));
        
        // Address
        CreateMap<AddressDTO, Address>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id))) 
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<Address, AddressDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString())) 
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));
        
        
    }
}