namespace CurveDentalManagement.API.Models.DTO
{
    public class BillingDto
    {
        public Guid Id { get; set; }
        public string BillingCode { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public List<TreatmentDto> Treatments { get; set; } = new List<TreatmentDto>();
    }
}
