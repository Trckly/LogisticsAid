using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class ObservationRepository : IObservationRepository
{
    private LogisticsAidDbContext _context;

    public ObservationRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }
    
    public async Task<string> GetJsonByIdAsync(Guid observationId, CancellationToken ct)
    {
        var observationJson = await _context.Observations.FirstOrDefaultAsync(o => o.Id == observationId, cancellationToken: ct);
        return observationJson != null ? observationJson.ObservationContent : "";
    }

    public async Task AddObservationAsync(ObservationModel observationModel, CancellationToken ct)
    {
        await _context.Observations.AddAsync(observationModel, cancellationToken: ct);
        await _context.SaveChangesAsync(ct);
    }
}