using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CurveDentalManagement.API.Repositories.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PatientRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Add New Patient
        public async Task<Patient> CreateAsync(Patient patient)
        {
            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();
            return patient;
        }
        
        
    }
}
