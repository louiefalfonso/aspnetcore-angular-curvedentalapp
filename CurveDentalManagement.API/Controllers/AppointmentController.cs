using Azure.Core;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurveDentalManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;

        public AppointmentsController(
            IAppointmentRepository appointmentRepository, 
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }

        // Add New Appointment
        [HttpPost]
        public async Task<IActionResult> CreateNewAppointmentAsync([FromBody] CreateAppointmentRequestDto request)
        {
            // map dto to domain model
            var appointment = new Appointment
            {
                Status = request.Status,
                Remarks = request.Remarks,
                AppointmentCode = request.AppointmentCode,
                AppointmentDate = request.AppointmentDate,
                AppointmentTime = request.AppointmentTime,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
            };

            // add doctor to this appointment
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by Id
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // check if doctor is null
                if (exisitingDoctor is not null)
                {
                    appointment.Doctors.Add(exisitingDoctor);
                }
            }

            // add patients to this appointment
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is null
                if (exisitingPatient is not null)
                {
                    appointment.Patients.Add(exisitingPatient);
                }
            }

            // add new appointment to repository
            appointment = await appointmentRepository.CreateAsync(appointment);

            // map domain model to dto
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Remarks = appointment.Remarks,
                AppointmentCode = appointment.AppointmentCode,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Specialization = x.Specialization,
                    Department = x.Department,
                    Schedule = x.Schedule,
                    LicenseNumber = x.LicenseNumber,
                    YearsOfExperience = x.YearsOfExperience,
                    DentalSchool = x.DentalSchool,
                    OfficeAddress = x.OfficeAddress,
                    EmergencyContact = x.EmergencyContact,

                }).ToList(),
                Patients = appointment.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    PhoneNumber = x.PhoneNumber,
                    Age = x.Age,
                    Address = x.Address,
                    InsuranceDetails = x.InsuranceDetails,
                    InsuranceProvider = x.InsuranceProvider,
                    InsuranceExpiryDate = x.InsuranceExpiryDate,
                    InsurancePolicyNumber = x.InsurancePolicyNumber,
                }).ToList(),
            };

            return Ok(response);

        }

        // Get Appointment By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAppointmentByIdAsync([FromRoute] Guid id)
        {
            // get appointment by Id
            var appointment = await appointmentRepository.GetByIdAsync(id);

            // Check if Appointment is null
            if (appointment == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Remarks = appointment.Remarks,
                AppointmentCode = appointment.AppointmentCode,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Specialization = x.Specialization,
                    Department = x.Department,
                    Schedule = x.Schedule,
                    LicenseNumber = x.LicenseNumber,
                    YearsOfExperience = x.YearsOfExperience,
                    DentalSchool = x.DentalSchool,
                    OfficeAddress = x.OfficeAddress,
                    EmergencyContact = x.EmergencyContact,

                }).ToList(),
                Patients = appointment.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    PhoneNumber = x.PhoneNumber,
                    Age = x.Age,
                    Address = x.Address,
                    InsuranceDetails = x.InsuranceDetails,
                    InsuranceProvider = x.InsuranceProvider,
                    InsuranceExpiryDate = x.InsuranceExpiryDate,
                    InsurancePolicyNumber = x.InsurancePolicyNumber,
                }).ToList(),
            };

            return Ok(response);


        }


        // Get All Appointments
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // Get All Appointments
            var appointments = await appointmentRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // Map Domain Model to DTO
            var response = new List<AppointmentDto>();
            {
                foreach (var appointment in appointments)
                {
                    response.Add(new AppointmentDto
                    {
                        Id = appointment.Id,
                        Status = appointment.Status,
                        Remarks = appointment.Remarks,
                        AppointmentCode = appointment.AppointmentCode,
                        AppointmentDate = appointment.AppointmentDate,
                        AppointmentTime = appointment.AppointmentTime,
                        Doctors = appointment.Doctors.Select(x => new DoctorDto
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            ContactNumber = x.ContactNumber,
                            Specialization = x.Specialization,
                            Department = x.Department,
                            Schedule = x.Schedule,
                            LicenseNumber = x.LicenseNumber,
                            YearsOfExperience = x.YearsOfExperience,
                            DentalSchool = x.DentalSchool,
                            OfficeAddress = x.OfficeAddress,
                            EmergencyContact = x.EmergencyContact,

                        }).ToList(),
                        Patients = appointment.Patients.Select(x => new PatientDto
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            DateOfBirth = x.DateOfBirth,
                            Gender = x.Gender,
                            PhoneNumber = x.PhoneNumber,
                            Age = x.Age,
                            Address = x.Address,
                            InsuranceDetails = x.InsuranceDetails,
                            InsuranceProvider = x.InsuranceProvider,
                            InsuranceExpiryDate = x.InsuranceExpiryDate,
                            InsurancePolicyNumber = x.InsurancePolicyNumber,
                        }).ToList(),
                    });
                 }
            }

            return Ok(response);
        }

        // Update Appointment
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAppointmentAsync([FromRoute] Guid id, UpdateAppointmentRequestDto request)
        {
            // map dto to domain model
            var appointment = new Appointment
            {
                Id = id,
                Status = request.Status,
                Remarks = request.Remarks,
                AppointmentCode = request.AppointmentCode,
                AppointmentDate = request.AppointmentDate,
                AppointmentTime = request.AppointmentTime,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
            };


            // add doctor to this appointment
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by Id
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // check if doctor is null
                if (exisitingDoctor is not null)
                {
                    appointment.Doctors.Add(exisitingDoctor);
                }
            }

            // add patients to this appointment
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is null
                if (exisitingPatient is not null)
                {
                    appointment.Patients.Add(exisitingPatient);
                }
            }

            // Call Repository to update Appointment Domain Model
            var updatedAppointment = await appointmentRepository.UpdateAsync(appointment);

            // Check if Updated Appointment is Null
            if (updatedAppointment == null)
            {
                return NotFound();
            }


            // map domain model to dto
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Remarks = appointment.Remarks,
                AppointmentCode = appointment.AppointmentCode,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Specialization = x.Specialization,
                    Department = x.Department,
                    Schedule = x.Schedule,
                    LicenseNumber = x.LicenseNumber,
                    YearsOfExperience = x.YearsOfExperience,
                    DentalSchool = x.DentalSchool,
                    OfficeAddress = x.OfficeAddress,
                    EmergencyContact = x.EmergencyContact,

                }).ToList(),
                Patients = appointment.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    PhoneNumber = x.PhoneNumber,
                    Age = x.Age,
                    Address = x.Address,
                    InsuranceDetails = x.InsuranceDetails,
                    InsuranceProvider = x.InsuranceProvider,
                    InsuranceExpiryDate = x.InsuranceExpiryDate,
                    InsurancePolicyNumber = x.InsurancePolicyNumber,
                }).ToList(),
            };

            return Ok(response);


        }

        // Delete Appointment
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id)
        {
            // Delete Appointment
            var deletedAppointment = await appointmentRepository.DeleteAsync(id);

            // Check if Deleted Appointment is Null
            if (deletedAppointment == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new AppointmentDto
            {
                Id = deletedAppointment.Id,
                Status = deletedAppointment.Status,
                Remarks = deletedAppointment.Remarks,
                AppointmentCode = deletedAppointment.AppointmentCode,
                AppointmentDate =deletedAppointment.AppointmentDate,
                AppointmentTime = deletedAppointment.AppointmentTime,
            };

            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAppointmentsTotal()
        {
            var count = await appointmentRepository.GetCount();
            return Ok(count);
        }
    }

}
