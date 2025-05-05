using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;

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
    }
}
