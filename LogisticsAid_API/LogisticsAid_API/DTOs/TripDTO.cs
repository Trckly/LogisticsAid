namespace LogisticsAid_API.DTOs;

public class TripDTO
{
    public required string Id { get; set; }
    public required string ReadableId { get; set; }
    public required DateTime DateCreated { get; set; }
    public required DateTime LoadingDate { get; set; }
    public required DateTime UnloadingDate { get; set; }
    public required LogisticianDTO Logistician { get; set; }
    public required CarrierCompanyDTO CarrierCompany { get; set; }
    public required CustomerCompanyDTO CustomerCompany { get; set; }
    public required DriverDTO Driver { get; set; }
    public required TransportDTO Truck { get; set; }
    public required TransportDTO Trailer { get; set; }
    public required decimal CustomerPrice { get; set; }
    public required decimal CarrierPrice { get; set; }
    public required string CargoName { get; set; }
    public required decimal CargoWeight { get; set; }
    public required bool WithTax { get; set; }
    public required ICollection<RoutePointDTO> RoutePoints { get; set; } = [];
}