using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Implementation;
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

        // Get Doctor By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDoctorByIdAsync([FromRoute] Guid id)
        {
            // get doctor by Id at repository
            var doctor = await doctorRepository.GetByIdAsync(id);

            // check if doctor is null
            if (doctor == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new DoctorDto
            {
                Id = doctor.Id,
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

        // Get All Doctors
        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsAsync
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            //get all doctors from rtepository
            var doctors = await doctorRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map domain model to dto
            var response = new List<DoctorDto>();
            foreach (var doctor in doctors)
            {
                response.Add(new DoctorDto
                {
                    Id = doctor.Id,
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
                });
            }
            return Ok( response );
        }

        // Update Doctor
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateDoctorAsync([FromRoute] Guid id, UpdateDoctorRequestDto request)
        {
            // check if doctor is null
            if ( request == null)
            {
                return BadRequest();
;           }

            // convert dto to domain model
            var doctor = new Doctor
            {
                Id = id,
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

            // update doctor in repository
            doctor = await doctorRepository.UpdateAsync(doctor);

            // check if staff is null
            if (doctor == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new DoctorDto
            {
                Id = doctor.Id,
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

        // Delete Doctor
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteDoctorAsync([FromRoute] Guid id)
        {
            // delet doctor from repository
            var doctor = await doctorRepository.DeleteAsync(id);

            // check if doctor is null
            if (doctor == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new DoctorDto
            {
                Id = doctor.Id,
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

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetDoctorsTotal()
        {
            var count = await doctorRepository.GetCount();
            return Ok(count);
        }
    }
}

