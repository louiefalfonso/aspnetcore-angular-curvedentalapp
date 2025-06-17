using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IBillingRepository
    {
        Task<Billing> CreateAsync(Billing billing);
    }
}
