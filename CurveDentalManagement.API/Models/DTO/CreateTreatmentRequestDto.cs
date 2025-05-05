namespace CurveDentalManagement.API.Models.DTO
{
    public class CreateTreatmentRequestDto
    {
        public string TreatmentName { get; set; }
        public string TreatmentCame { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Cost { get; set; }
        public string InsuranceCoverage { get; set; }
        public string InsuranceCoverageAmount { get; set; }
        public string FollowUpCare { get; set; }
        public string RiskBenefits { get; set; }
        public string Indications { get; set; }
        public Guid[] Doctors { get; set; }
    }
}
