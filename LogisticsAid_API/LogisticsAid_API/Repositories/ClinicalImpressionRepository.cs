using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Repositories;

public class ClinicalImpressionRepository : IClinicalImpressionRepository
{
    private readonly LogisticsAidDbContext _context;

    public ClinicalImpressionRepository(LogisticsAidDbContext context)
    {
        _context = context;
    }
    
    public async Task SubmitClinicalImpressionAsync(ClinicalImpressionModel clinicalImpressionModel, CancellationToken ct)
    {
        await _context.ClinicalImpressions.AddAsync(clinicalImpressionModel, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<string> GetClinicalImpressionContentByPatientAsync(string questionnaireId, string patientId, CancellationToken ct)
    {
        var clinicalImpression = await _context.ClinicalImpressions.FirstOrDefaultAsync(ci => ci.QuestionnaireId == Guid.Parse(questionnaireId) && ci.PatientId == patientId, ct);

        return clinicalImpression != null ?  clinicalImpression.ClinicalImpressionContent : "";
    }
}