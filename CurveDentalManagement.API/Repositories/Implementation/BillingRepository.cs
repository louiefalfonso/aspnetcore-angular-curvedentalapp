using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        // get all billings
        public async Task<IEnumerable<Billing>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100)
        {
            var billings = dbContext.Billings.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                billings = billings.Where(x =>
                    x.BillingCode.Contains(query) ||
                    x.BillingStatus.Contains(query) ||
                    x.PaymentStatus.Contains(query) ||
                    x.Patients.Any(p => p.FirstName.Contains(query) || p.LastName.Contains(query)));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "billingCode", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    billings = isAsc ? billings.OrderBy(x => x.BillingCode) : billings.OrderByDescending(x => x.BillingCode);
                }

                if (string.Equals(sortBy, "billingStatus", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    billings = isAsc ? billings.OrderBy(x => x.BillingStatus) : billings.OrderByDescending(x => x.BillingStatus);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            billings = billings.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await billings.Include(x => x.Treatments).Include(x => x.Patients).ToListAsync();
        }

        // get billing by ID
        public async Task<Billing?> GetByIdAsync(Guid id)
        {
            return await dbContext.Billings.Include(x => x.Treatments).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }

        // update billing
        public async Task<Billing?> UpdateAsync(Billing billing)
        {
            // Fetch billing by ID
            var existingBilling = await dbContext.Billings.Include(x => x.Treatments).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == billing.Id);


            // Check if existing billing is null
            if (existingBilling == null)
            {
                return null;
            }

           // Update Appointment
            dbContext.Entry(existingBilling).CurrentValues.SetValues(billing);

            // Update Treatment
            existingBilling.Treatments = billing.Treatments;

            // Update Patient
            existingBilling.Patients = billing.Patients;

            // Save Changes
            await dbContext.SaveChangesAsync();

            return existingBilling;
        }

        // delete billing
        public async Task<Billing?> DeleteAsync(Guid id)
        {
            // Fetch The Billing By ID
            var existingBilling = await dbContext.Billings.FirstOrDefaultAsync(x => x.Id == id);

            // Check if Existing Billing is Null
            if (existingBilling != null)
            {
                dbContext.Billings.Remove(existingBilling);
                await dbContext.SaveChangesAsync();
                return existingBilling;
            }

            return null;
        }

        // get all billing count
        public async Task<int> GetCount()
        {
            return await dbContext.Billings.CountAsync();
        }
    }
}
