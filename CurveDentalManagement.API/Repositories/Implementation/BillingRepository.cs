using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;

namespace CurveDentalManagement.API.Repositories.Implementation
{
    public class BillingRepository : IBillingRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BillingRepository( ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        // add new billing
        public async Task<Billing> CreateAsync(Billing billing)
        {
            await dbContext.Billings.AddAsync(billing);
            await dbContext.SaveChangesAsync();
            return billing;
        }
    }
}
