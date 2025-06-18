namespace CurveDentalManagement.API.Models.DTO
{
    public class UpdateBillingRequestDto
    {
        public string BillingCode { get; set; }
        public string BillingStatus { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public List<Guid> Patients { get; set; } = new List<Guid>();
        public List<Guid> Treatments { get; set; } = new List<Guid>();
    }
}
