using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurveDentalManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        // Add New Doctor
        [HttpPost]
        public async Task<IActionResult> CreateNewDoctorAsync([FromBody] CreateDoctorRequestDto request)
        {
            // map dto to domain model
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber,
                Specialization = request.Specialization,
                Department = request.Department,
                Schedule = request.Schedule,
                LicenseNumber = request.LicenseNumber,
                YearsOfExperience = request.YearsOfExperience,
                DentalSchool = request.DentalSchool,
                OfficeAddress = request.OfficeAddress,
                EmergencyContact = request.EmergencyContact,
            };

            // add new doctor to repository
            await doctorRepository.CreateAsync(doctor);

            // map domain model to dto
            var response = new DoctorDto
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                ContactNumber = doctor.ContactNumber,
                Specialization = doctor.Specialization,
                Department = doctor.Department,
                Schedule = doctor.Schedule,
                LicenseNumber = doctor.LicenseNumber,
                YearsOfExperience = doctor.YearsOfExperience,
                DentalSchool = doctor.DentalSchool,
                OfficeAddress = doctor.OfficeAddress,
                EmergencyContact = doctor.EmergencyContact,
            };

            return Ok(response);
        }
    }
}