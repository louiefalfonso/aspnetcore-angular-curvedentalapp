using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface ITreatmentRepository
    {
        Task<Treatment> CreateAsync(Treatment treatment);
       
        Task<Treatment?> GetByIdAsync(Guid id);

        Task<IEnumerable<Treatment>> GetAllAsync
        (
            // add filtering, sorting & pagination
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100
        );

        Task<Treatment?> UpdateAsync(Treatment treatment);

        Task<Treatment?> DeleteAsync(Guid id);

        Task<int> GetCount();

    }
}
