﻿// using System.Text.Json;
// using LogisticsAid_API.Repositories;
// using Hl7.Fhir.Model;
// using Hl7.Fhir.Serialization;
// using LogisticsAid_API.Entities;
// using LogisticsAid_API.Repositories.Interfaces;
// using Task = System.Threading.Tasks.Task;
//
// namespace LogisticsAid_API.Services;
//
// public class ObservationService
// {
//     private readonly IObservationRepository _observationRepository;
//
//     public ObservationService(IObservationRepository observationRepository)
//     {
//         _observationRepository = observationRepository;
//     }
//
//     public async Task<string> GetByIdAsync(Guid observationId)
//     {
//         return await _observationRepository.GetJsonByIdAsync(observationId, CancellationToken.None);
//     }
//     
//     public async Task AddObservationAsync(string clinicalImpressionId, JsonElement observationJson)
//     {
//         var parser = new FhirJsonParser();
//         var observation = await parser.ParseAsync<Observation>(observationJson.GetRawText());
//
//         var observationModel = new ObservationModel
//         {
//             Id = Guid.Parse(observation.Id),
//             ClinicalImpressionId = Guid.Parse(clinicalImpressionId),
//             ObservationContent = observationJson.GetRawText()
//         };
//         
//         await _observationRepository.AddObservationAsync(observationModel, CancellationToken.None);
//     }
// }