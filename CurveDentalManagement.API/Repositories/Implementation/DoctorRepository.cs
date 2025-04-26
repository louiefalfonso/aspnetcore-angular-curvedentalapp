using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CurveDentalManagement.API.Repositories.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DoctorRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add New Doctor
        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            await dbContext.Doctors.AddAsync(doctor);
            await dbContext.SaveChangesAsync();
            return doctor;
        }

        // Get Doctor by Id
        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get All Doctors
        public async Task<IEnumerable<Doctor>> GetAllAsync
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
            var doctors = dbContext.Doctors.AsQueryable();

            // filter
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                doctors = doctors.Where(
                    x => x.FirstName.Contains(query) ||
                    x.LastName.Contains(query) ||
                    x.Specialization.Contains(query)
                );
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.FirstName) : doctors.OrderByDescending(x => x.FirstName);
                }
                if (string.Equals(sortBy, "LasttName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.LastName) : doctors.OrderByDescending(x => x.LastName);
                }
                if (string.Equals(sortBy, "Specialization", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.Specialization) : doctors.OrderByDescending(x => x.Specialization);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            doctors = doctors.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await doctors.ToListAsync();
        }

        // Update Doctor
        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existingDoctor = dbContext.Doctors.FirstOrDefault( x => x.Id == doctor.Id);

            if (existingDoctor != null) 
            {
                dbContext.Entry(existingDoctor).CurrentValues.SetValues(doctor);
                await dbContext.SaveChangesAsync();
                return doctor;
            }
            return null;
        }

        // Delete Doctor
        public async Task<Doctor?> DeleteAsync(Guid id)
        {
            var existingDoctor = await dbContext.Doctors.FirstOrDefaultAsync( x => x.Id == id);

            if (existingDoctor is null) 
            {
                return null;
            }

            dbContext.Doctors.Remove(existingDoctor);
            await dbContext.SaveChangesAsync();
            return existingDoctor;
        }

        // Get Total Count
        public async Task<int> GetCount()
        {
            return await dbContext.Doctors.CountAsync();
        }
    }
}
