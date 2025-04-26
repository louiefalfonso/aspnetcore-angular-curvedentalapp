using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor doctor);

        Task<Doctor?> GetByIdAsync(Guid id);

        Task<IEnumerable<Doctor>> GetAllAsync
        (
            // add filtering, sorting & pagination
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100
        );

        Task<Doctor?> UpdateAsync(Doctor doctor );

        Task<Doctor?> DeleteAsync(Guid id);

        Task<int> GetCount();
    }
}
