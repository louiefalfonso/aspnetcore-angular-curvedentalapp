using CurveDentalManagement.API.Models.Domain;

namespace CurveDentalManagement.API.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync (string? query = null, string? sortBy = null,string? sortDirection = null,int? pageNumber = 1,int? pageSize = 100);
        Task<Appointment?> UpdateAsync(Appointment appointment);
        Task<Appointment?> DeleteAsync(Guid id);
        Task<int> GetCount();
    }
}
