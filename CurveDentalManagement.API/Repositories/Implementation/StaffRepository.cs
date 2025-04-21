using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CurveDentalManagement.API.Repositories.Implementation
{
    public class StaffRepository : IStaffRepository 
    {
        private readonly ApplicationDbContext dbContext;

        public StaffRepository(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        // Add New Staff
        public async Task<Staff> CreateAsync(Staff staff)
        {
           await dbContext.Staffs.AddAsync(staff);
           await dbContext.SaveChangesAsync();
           return staff;
        }

        // Get Staff by Id
        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            return await dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get All Staffs
        public async Task<IEnumerable<Staff>> GetAllAsync
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
            var staffs = dbContext.Staffs.AsQueryable();

            //filter
            if(string.IsNullOrWhiteSpace(query) == false)
            {
                staffs = staffs.Where(
                    x => x.FirstName.Contains(query) ||
                    x.LastName.Contains(query) ||
                    x.StaffRole.Contains(query)
                );
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    staffs = isAsc ? staffs.OrderBy(x => x.FirstName) : staffs.OrderByDescending(x => x.FirstName);
                }
                if (string.Equals(sortBy, "LasttName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    staffs = isAsc ? staffs.OrderBy(x => x.LastName) : staffs.OrderByDescending(x => x.LastName);
                }
                if (string.Equals(sortBy, "StaffRole", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    staffs = isAsc ? staffs.OrderBy(x => x.StaffRole) : staffs.OrderByDescending(x => x.StaffRole);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            staffs = staffs.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await staffs.ToListAsync();

        }

        // Update Staff
        public async Task<Staff?> UpdateAsync(Staff staff)
        {
            var existingStaff = dbContext.Staffs.FirstOrDefault(x => x.Id == staff.Id);

            if (existingStaff != null) 
            { 
                dbContext.Entry(existingStaff).CurrentValues.SetValues(staff);
                await dbContext.SaveChangesAsync();
                return staff;
            }
            return null;


        }

        // Delete Patient & Save Changes
        public async Task<Staff?> DeleteAsync(Guid id)
        {
            var existingStaff = await dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStaff is null)
            {
                return null;
            }

            dbContext.Staffs.Remove(existingStaff);
            await dbContext.SaveChangesAsync();
            return existingStaff;
        }

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Staffs.CountAsync();
        }
    }
}