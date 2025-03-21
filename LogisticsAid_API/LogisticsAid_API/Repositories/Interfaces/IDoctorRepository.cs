﻿using LogisticsAid_API.Entities;

namespace LogisticsAid_API.Repositories.Interfaces;

public interface IDoctorRepository
{
    public Task<DoctorModel?> GetDoctorAsync(string email, CancellationToken ct);
    public Task<DoctorModel?> GetDoctorWithPatientsAsync(string email, CancellationToken ct);
    public Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync(CancellationToken ct);
    public Task UpdateDoctorAsync(DoctorModel doctor, CancellationToken ct);
    public Task CreateDoctorAsync(DoctorModel doctor, CancellationToken ct);
    public Task DeleteDoctorAsync(string email, CancellationToken ct);
}