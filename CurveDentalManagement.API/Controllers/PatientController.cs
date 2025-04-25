using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Implementation;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CurveDentalManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        // Add New Patient
        [HttpPost]
        public async Task<IActionResult> CreateNewPatientAsync([FromBody] CreatePatientRequestDto request)
        {
            // map dto to domain model
            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                Address = request.Address,
                InsuranceDetails = request.InsuranceDetails,
                InsuranceProvider = request.InsuranceProvider,
                InsurancePolicyNumber = request.InsurancePolicyNumber,
                InsuranceExpiryDate = request.InsuranceExpiryDate,
            };

            // add new patient to repository
            await patientRepository.CreateAsync(patient);


            // map domain model to dto
            var response = new PatientDto
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Age = patient.Age,
                Address = patient.Address,
                InsuranceDetails = patient.InsuranceDetails,
                InsuranceProvider = patient.InsuranceProvider,
                InsurancePolicyNumber = patient.InsurancePolicyNumber,
                InsuranceExpiryDate = patient.InsuranceExpiryDate,
            };

            return Ok(response);
        }


        // Get Patient By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPatientByIdAsync([FromRoute] Guid id)
        {
            // get patient by ID at repository
            var patient = await patientRepository.GetByIdAsync(id);

            // check if staff is null
            if (patient == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Age = patient.Age,
                Address = patient.Address,
                InsuranceDetails = patient.InsuranceDetails,
                InsuranceProvider = patient.InsuranceProvider,
                InsurancePolicyNumber = patient.InsurancePolicyNumber,
                InsuranceExpiryDate = patient.InsuranceExpiryDate,
            };

            return Ok(response);
        }

        // Get All Staffs
        [HttpGet]
        public async Task<IActionResult> GetAllPatientsAsync
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // get all patients from repository
            var patients = await patientRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map Domain model to DTO
            var response = new List<PatientDto>();
            foreach (var patient in patients)
            {
                response.Add(new PatientDto
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    Age = patient.Age,
                    Address = patient.Address,
                    InsuranceDetails = patient.InsuranceDetails,
                    InsuranceProvider = patient.InsuranceProvider,
                    InsurancePolicyNumber = patient.InsurancePolicyNumber,
                    InsuranceExpiryDate = patient.InsuranceExpiryDate,
                });
            }
            return Ok(response);
        }


        // Update Patient
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePatientAsync([FromRoute] Guid id, UpdatePatientRequestDto request)
        {
            // check if staff is null
            if (request == null)
            {
                return BadRequest();
            }

            // convert dto to domain model
            var patient = new Patient
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                Address = request.Address,
                InsuranceDetails = request.InsuranceDetails,
                InsuranceProvider = request.InsuranceProvider,
                InsurancePolicyNumber = request.InsurancePolicyNumber,
                InsuranceExpiryDate = request.InsuranceExpiryDate,
            };

            // update patient in repository
            patient = await patientRepository.UpdateAsync(patient);

            // check if staff is null
            if (patient == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Age = patient.Age,
                Address = patient.Address,
                InsuranceDetails = patient.InsuranceDetails,
                InsuranceProvider = patient.InsuranceProvider,
                InsurancePolicyNumber = patient.InsurancePolicyNumber,
                InsuranceExpiryDate = patient.InsuranceExpiryDate,
            };
            return Ok(response);
        }

        // Delete Patient
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePatientAsync([FromRoute] Guid id)
        {
            // delete patient from repository
            var patient = await patientRepository.DeleteAsync(id);

            //check if staff is null
            if (patient == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Age = patient.Age,
                Address = patient.Address,
                InsuranceDetails = patient.InsuranceDetails,
                InsuranceProvider = patient.InsuranceProvider,
                InsurancePolicyNumber = patient.InsurancePolicyNumber,
                InsuranceExpiryDate = patient.InsuranceExpiryDate,
            };
            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetPatientsTotal()
        {
            var count = await patientRepository.GetCount();
            return Ok(count);
        }

       
    }
}


