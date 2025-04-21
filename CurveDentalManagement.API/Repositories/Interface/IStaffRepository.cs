using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IStaffRepository
    {
        Task <Staff> CreateAsync(Staff staff);

        Task<Staff?> GetByIdAsync(Guid id);

        Task<IEnumerable<Staff>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Staff?> UpdateAsync(Staff staff);

        Task<Staff?> DeleteAsync(Guid id);
        
        Task<int> GetCount();
    }
}
