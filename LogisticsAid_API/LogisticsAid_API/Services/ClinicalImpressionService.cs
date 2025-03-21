using System.Text.Json;
using LogisticsAid_API.Repositories;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath.Sprache;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace LogisticsAid_API.Services;

public class ClinicalImpressionService
{
    private readonly IClinicalImpressionRepository _clinicalImpressionRepository;

    public ClinicalImpressionService(IClinicalImpressionRepository clinicalImpressionRepository)
    {
        _clinicalImpressionRepository = clinicalImpressionRepository;
    }

    public async Task SubmitClinicalImpression(string questionnaireId, JsonElement clinicalImpressionJson)
    {
        var parser = new FhirJsonParser();

        var clinicalImpression = await parser.ParseAsync<ClinicalImpression>(clinicalImpressionJson.GetRawText());

        var clinicalImpressionModel = new ClinicalImpressionModel
        {
            Id = Guid.Parse(clinicalImpression.Id),
            QuestionnaireId = Guid.Parse(questionnaireId),
            PatientId = clinicalImpression.Subject.Reference,
            ClinicalImpressionContent = clinicalImpressionJson.GetRawText(),
        };
        
        await _clinicalImpressionRepository.SubmitClinicalImpressionAsync(clinicalImpressionModel, CancellationToken.None);
    }

    public async Task<string> GetClinicalImpressionContentByPatientAsync(string questionnaireId, string patientId)
    {
        return await _clinicalImpressionRepository.GetClinicalImpressionContentByPatientAsync(patientId, questionnaireId, CancellationToken.None);
    }
}