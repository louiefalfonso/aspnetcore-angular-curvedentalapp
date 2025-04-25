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

        // Get All Patients
        public async Task<IEnumerable<Patient>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            )
        {
            // query
            var patients = dbContext.Patients.AsQueryable();

            //filter
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                patients = patients.Where(
                    x => x.FirstName.Contains(query) ||
                    x.LastName.Contains(query) ||
                    x.Gender.Contains(query)
                );
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    patients = isAsc ? patients.OrderBy(x => x.FirstName) : patients.OrderByDescending(x => x.FirstName);
                }
                if (string.Equals(sortBy, "LasttName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    patients = isAsc ? patients.OrderBy(x => x.LastName) : patients.OrderByDescending(x => x.LastName);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            patients = patients.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await patients.ToListAsync();
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

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Patients.CountAsync();
        }

        
    }
}



