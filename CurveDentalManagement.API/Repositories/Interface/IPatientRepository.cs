using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task<Patient> CreateAsync(Patient patient);
    }
}
