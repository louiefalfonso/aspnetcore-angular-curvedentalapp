using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor doctor);
    }
}
