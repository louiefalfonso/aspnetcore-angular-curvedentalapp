﻿using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task<Patient> CreateAsync(Patient patient);

        Task<Patient?> GetByIdAsync(Guid id);

        Task<IEnumerable<Patient>> GetAllAsync
           (
               // add filtering, sorting & pagination
               string? query = null,
               string? sortBy = null,
               string? sortDirection = null,
               int? pageNumber = 1,
               int? pageSize = 100
           );

        Task<Patient?> UpdateAsync(Patient patient);

        Task<Patient?> DeleteAsync(Guid id);

        Task<int> GetCount();

        
    }
}
