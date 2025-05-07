namespace CurveDentalManagement.API.Models.DTO
{
    public class UpdateTreatmentRequestDto
    {
        public string TreatmentName { get; set; }
        public string TreatmentCode { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Cost { get; set; }
        public string InsuranceCoverage { get; set; }
        public string InsuranceCoverageAmount { get; set; }
        public string FollowUpCare { get; set; }
        public string RiskBenefits { get; set; }
        public string Indications { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
    }
}
