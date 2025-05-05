using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface ITreatmentRepository
    {
        Task<Treatment> CreateAsync(Treatment treatment);
    }
}
