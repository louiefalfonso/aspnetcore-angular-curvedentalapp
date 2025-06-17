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
    public class BillingsController : ControllerBase
    {
        private readonly IBillingRepository billingRepository;
        private readonly IPatientRepository patientRepository;
        private readonly ITreatmentRepository treatmentRepository;

        public BillingsController
       (
           IBillingRepository billingRepository,
           IPatientRepository patientRepository,
           ITreatmentRepository treatmentRepository
       )

        {
            this.billingRepository = billingRepository;
            this.patientRepository = patientRepository;
            this.treatmentRepository = treatmentRepository;
        }


        // Add New Billing
        [HttpPost]
        public async Task<IActionResult> CreateNewBillingAsync([FromBody] CreateBillingRequestDto request)
        {
            // map dto to domain model
            var billing = new Billing
            {
                BillingCode = request.BillingCode,
                TotalAmount = request.TotalAmount,
                PaymentStatus = request.PaymentStatus,
                PaymentMethod = request.PaymentMethod,
                BillingDate = request.BillingDate,
                PaymentDate = request.PaymentDate,
                Remarks = request.Remarks,
                Patients = new List<Patient>(),
                Treatments = new List<Treatment>(),
            };

            // add patients to this billing
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is null
                if (exisitingPatient is not null)
                {
                    billing.Patients.Add(exisitingPatient);
                }
            }

            // add treatments to this billing
            foreach (var treatmentGuid in request.Treatments)
            {
                // get treatment by Id
                var exisitingTreatment = await treatmentRepository.GetByIdAsync(treatmentGuid);

                // check if treatment is null
                if (exisitingTreatment is not null)
                {
                    billing.Treatments.Add(exisitingTreatment);
                }
            }

            // add new billing to repository
            billing = await billingRepository.CreateAsync(billing);

            // map domain model to dto
            var response = new BillingDto
            {
                Id = billing.Id,
                BillingCode = billing.BillingCode,
                TotalAmount = billing.TotalAmount,
                PaymentStatus = billing.PaymentStatus,
                PaymentMethod = billing.PaymentMethod,
                BillingDate = billing.BillingDate,
                PaymentDate = billing.PaymentDate,
                Remarks = billing.Remarks,
                Patients = billing.Patients.Select(x => new PatientDto
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
                Treatments = billing.Treatments.Select(x=> new TreatmentDto
                {
                    Id = x.Id,
                    TreatmentName = x.TreatmentName,
                    TreatmentCode = x.TreatmentCode,
                    Description= x.Description,
                    Duration = x.Duration,
                    Cost = x.Cost,
                    InsuranceCoverage = x.InsuranceCoverage,
                    InsuranceCoverageAmount = x.InsuranceCoverageAmount,
                    Indications = x.InsuranceCoverageAmount,
                }).ToList()
            };

            return Ok(response);
        }
    }
}


