using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CurveDentalManagement.API.Repositories.Implementation
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TreatmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // add new treatment
        public async Task<Treatment> CreateAsync(Treatment treatment)
        {
            await dbContext.Treatments.AddAsync(treatment);
            await dbContext.SaveChangesAsync();
            return treatment;
        }

        // get treatment by ID
        public async Task<Treatment?> GetByIdAsync(Guid id)
        {
            return await dbContext.Treatments.Include(x=> x.Doctors).FirstOrDefaultAsync(x=> x.Id == id);
        }

        // get all treatments
        public async Task<IEnumerable<Treatment>> GetAllAsync(
            string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100)
        {
            // query
            var treatments = dbContext.Treatments.AsQueryable();

            // filtering 
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                treatments = treatments.Where(x=>
                    x.TreatmentName.Contains(query) ||
                    x.TreatmentCode.Contains(query) ||
                    x.Doctors.Any(d => d.FirstName.Contains(query) || d.LastName.Contains(query)));
            }


            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "treatmentName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    treatments = isAsc ? treatments.OrderBy(x => x.TreatmentName) : treatments.OrderByDescending(x => x.TreatmentName);
                }

                if (string.Equals(sortBy, "treatmentCode", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    treatments = isAsc ? treatments.OrderBy(x => x.TreatmentCode) : treatments.OrderByDescending(x => x.TreatmentCode);
                }

            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            treatments = treatments.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await treatments.Include(x => x.Doctors).ToListAsync();

        }

        // update treatment
        public async Task<Treatment?> UpdateAsync(Treatment treatment)
        {
            // get treatment by Id
            var existingTreatment = await dbContext.Treatments.Include(x => x.Doctors).FirstOrDefaultAsync(x=>x.Id == treatment.Id);

            // check if treatment is null
            if (existingTreatment == null)
            {
                return null;
            }

            // update treatment
            dbContext.Entry(existingTreatment).CurrentValues.SetValues(treatment);

            // update doctor
            existingTreatment.Doctors = treatment.Doctors;

            // save changes
            await dbContext.SaveChangesAsync();

            return treatment;

        }


        // delete treatment
        public async Task<Treatment?> DeleteAsync(Guid id)
        {
           var existingTreatment = await dbContext.Treatments.FirstOrDefaultAsync(x=>x.Id == id);

            if (existingTreatment is null)
            {
                return null;
            }

            dbContext.Treatments.Remove(existingTreatment);
            await dbContext.SaveChangesAsync();
            return existingTreatment;

        }

        // get total count
        public async Task<int> GetCount()
        {
            return await dbContext.Treatments.CountAsync();
        }
    }
}


