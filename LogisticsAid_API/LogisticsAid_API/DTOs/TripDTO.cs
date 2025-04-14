namespace LogisticsAid_API.DTOs;

public class TripDTO
{
    public required string Id { get; set; }
    public required string ReadableId { get; set; }
    public required DateTime DateCreated { get; set; }
    public required DateTime LoadingDate { get; set; }
    public required DateTime UnloadingDate { get; set; }
    public required LogisticianDTO Logistician { get; set; }
    public required CarrierDTO Carrier { get; set; }
    public required CustomerDTO Customer { get; set; }
    public required DriverDTO Driver { get; set; }
    public required TransportDTO Transport { get; set; }
    public required decimal CustomerPrice { get; set; }
    public required decimal CarrierPrice { get; set; }
    public required string CargoName { get; set; }
    public required decimal CargoWeight { get; set; }
    public required bool WithTax { get; set; }
    public required IEnumerable<RoutePointDTO> RoutePoints { get; set; } = [];
}