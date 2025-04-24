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

        // Get Patient by Id
        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
        }


        // Update Patient
        public async Task<Patient?> UpdateAsync(Patient patient)
        {
            var existingPatient = dbContext.Patients.FirstOrDefault(x => x.Id == patient.Id);

            if (existingPatient != null)
            {
                dbContext.Entry(existingPatient).CurrentValues.SetValues(patient);
                await dbContext.SaveChangesAsync();
                return patient;
            }
            return null;
        }

        // Delete Patient 
        public async Task<Patient?> DeleteAsync(Guid id)
        {
            var existingPatient = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPatient is null)
            {
                return null;
            }

            dbContext.Patients.Remove(existingPatient);
            await dbContext.SaveChangesAsync();
            return existingPatient;
        }
    }
}

