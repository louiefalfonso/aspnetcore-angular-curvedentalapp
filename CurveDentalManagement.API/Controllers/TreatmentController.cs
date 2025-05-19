using System.Numerics;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurveDentalManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentRepository treatmentRepository;
        private readonly IDoctorRepository doctorRepository;

        public TreatmentsController(
            ITreatmentRepository treatmentRepository,
            IDoctorRepository doctorRepository)
        {
            this.treatmentRepository = treatmentRepository;
            this.doctorRepository = doctorRepository;
        }

        // Add New Treatment
        [HttpPost]
        public async Task<IActionResult> CreateNewTreatmentAsync([FromBody] CreateTreatmentRequestDto request)
        {
            // map dto to domain model
            var treatment = new Treatment
            {
                TreatmentName = request.TreatmentName,
                TreatmentCode = request.TreatmentCode,
                Description = request.Description,
                Duration = request.Duration,
                Cost = request.Cost,
                InsuranceCoverage = request.InsuranceCoverage,
                InsuranceCoverageAmount = request.InsuranceCoverageAmount,
                FollowUpCare = request.FollowUpCare,
                RiskBenefits = request.RiskBenefits,
                Indications = request.Indications,
                Doctors = new List<Doctor>(),
            };

            // add doctor to this treatment
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by Id
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // check if doctor is null
                if (exisitingDoctor is not null)
                {
                    treatment.Doctors.Add(exisitingDoctor);
                }
            }

            // add new treatment to repository
            treatment = await treatmentRepository.CreateAsync(treatment);

            // map domain model to dto
            var response = new TreatmentDto
            {
                Id = treatment.Id,
                TreatmentName = treatment.TreatmentName,
                TreatmentCode = treatment.TreatmentCode,
                Description = treatment.Description,
                Duration = request.Duration,
                Cost = treatment.Cost,
                InsuranceCoverage = treatment.InsuranceCoverage,
                InsuranceCoverageAmount = treatment.InsuranceCoverageAmount,
                FollowUpCare = treatment.FollowUpCare,
                RiskBenefits = treatment.RiskBenefits,
                Indications = treatment.Indications,
                Doctors = treatment.Doctors.Select(x=> new DoctorDto
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

                }).ToList()
            };

            return Ok(response);

        }


        // Get Treatment by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTreatmentByIdAsync([FromRoute] Guid id){

            // get treatment by ID from repository
            var treatment = await treatmentRepository.GetByIdAsync(id);

            // check if treatment is null
            if (treatment == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new TreatmentDto
            {
                Id = treatment.Id,
                TreatmentName = treatment.TreatmentName,
                TreatmentCode = treatment.TreatmentCode,
                Description = treatment.Description,
                Duration = treatment.Duration,
                Cost = treatment.Cost,
                InsuranceCoverage = treatment.InsuranceCoverage,
                InsuranceCoverageAmount = treatment.InsuranceCoverageAmount,
                FollowUpCare = treatment.FollowUpCare,
                RiskBenefits = treatment.RiskBenefits,
                Indications = treatment.Indications,
                Doctors = treatment.Doctors.Select(x => new DoctorDto
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

                }).ToList()
            };

            return Ok(response);
        }

        // Get All Treatments
        [HttpGet]
        public async Task<IActionResult> GetAllTreatmentsAsync
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {

            // get all treatments from repository
            var treatments = await treatmentRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map domain model to DTO
            var response = new List<TreatmentDto>();
            {
                foreach (var treatment in treatments)
                {
                    response.Add(new TreatmentDto
                    {
                        Id = treatment.Id,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentCode = treatment.TreatmentCode,
                        Description = treatment.Description,
                        Duration = treatment.Duration,
                        Cost = treatment.Cost,
                        InsuranceCoverage = treatment.InsuranceCoverage,
                        InsuranceCoverageAmount = treatment.InsuranceCoverageAmount,
                        FollowUpCare = treatment.FollowUpCare,
                        RiskBenefits = treatment.RiskBenefits,
                        Indications = treatment.Indications,
                        Doctors = treatment.Doctors.Select(x => new DoctorDto
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

                        }).ToList()
                    });
                }
            }

            return Ok(response);
        }


        // Update Treatment
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTreatmentAsync([FromRoute] Guid id, UpdateTreatmentRequestDto request)
        {
            // map dto to domain model
            var treatment = new Treatment
            {
                Id = id,
                TreatmentName = request.TreatmentName,
                TreatmentCode = request.TreatmentCode,
                Description = request.Description,
                Duration = request.Duration,
                Cost = request.Cost,
                InsuranceCoverage = request.InsuranceCoverage,
                InsuranceCoverageAmount = request.InsuranceCoverageAmount,
                FollowUpCare = request.FollowUpCare,
                RiskBenefits = request.RiskBenefits,
                Indications = request.Indications,
                Doctors = new List<Doctor>(),

            };

            // add doctor to treatment
            foreach (var doctorGuid in request.Doctors)
            {
                // Get doctor By ID from Doctor Repository
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // Check if Doctor Exists
                if (exisitingDoctor is not null)
                {
                    treatment.Doctors.Add(exisitingDoctor);
                }
            }

            // Call Repository to update Prescription Domain Model
            var updatedTreatment = await treatmentRepository.UpdateAsync(treatment);

            // check if updated treatment is Null
            if (updatedTreatment == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new TreatmentDto
            {
                Id = treatment.Id,
                TreatmentName = treatment.TreatmentName,
                TreatmentCode = treatment.TreatmentCode,
                Description = treatment.Description,
                Duration = treatment.Duration,
                Cost = treatment.Cost,
                InsuranceCoverage = treatment.InsuranceCoverage,
                InsuranceCoverageAmount = treatment.InsuranceCoverageAmount,
                FollowUpCare = treatment.FollowUpCare,
                RiskBenefits = treatment.RiskBenefits,
                Indications = treatment.Indications,
                Doctors = treatment.Doctors.Select(x => new DoctorDto
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

                }).ToList()
            };

            return Ok(response);


        }


        // Delete Treatment
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTreatmentAsync([FromRoute] Guid id)
        {
            // delete treatment
            var deletedTreatment = await treatmentRepository.DeleteAsync(id);

            // Check if Deleted Treatment is Null
            if (deletedTreatment == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new TreatmentDto
            {
                Id = deletedTreatment.Id,
                TreatmentName = deletedTreatment.TreatmentName,
                TreatmentCode = deletedTreatment.TreatmentCode,
                Description = deletedTreatment.Description,
                Duration = deletedTreatment.Duration,
                Cost = deletedTreatment.Cost,
                InsuranceCoverage = deletedTreatment.InsuranceCoverage,
                InsuranceCoverageAmount = deletedTreatment.InsuranceCoverageAmount,
                FollowUpCare = deletedTreatment.FollowUpCare,
                RiskBenefits = deletedTreatment.RiskBenefits,
                Indications = deletedTreatment.Indications,
            };

            return Ok(response);

        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetTreatmentsTotal()
        {
            var count = await treatmentRepository.GetCount();
            return Ok(count);
        }


    }
}

