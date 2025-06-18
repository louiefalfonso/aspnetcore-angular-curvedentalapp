using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IBillingRepository
    {
        Task<Billing> CreateAsync(Billing billing);
        Task<Billing?> GetByIdAsync(Guid id);
        Task<IEnumerable<Billing>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100);
    }
}
